# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:54:32  uptime=23717s  mem=1758MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_075422.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 627-610-162-620-261-126-162-316-103-132-130-132-326-321-301-103-301-310-103-312-103-203-267-672-601-607-761-176-143-361-672-524-123-132-143-170-715-132-176-103-103 576-610-672-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 570-750-754-705-162-167-162-621-621-126-123-213-360-321-306-136-162-621-362-306-326-574-560-650-756-625-206-320-312-310-301-302-302-320-623-236-206-236-312-106-162-612-162-617-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 231-213-251-153-235-532-235-231-521-537-517-527-725-721-256-625-623-623-251-527-573-351-327-375-312-327-237-130-167-521-351-352-762-321-213-123-132-132-312-321-316-321-321-327-3_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 657-601-103-756-376-617-617-631-561-603-167-167-167-267-260-620-613-617-162-612-607-612-657-673-627-261-612-621-135-610-601-136-261-631-172-106-106-163-162-173-671-163-671-120-1_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 136-617-167-671-621-106-103-157-263-172 163-136-103 136-152-120-172-106-316-316-157-361-103-671-617-176-103-126-162-163-106-136-261-321-301-542-532-547-320-123-106-102-675-612-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 657-567-167-601-167-261-326-231-201-326-236-672-267-231-132-123-124-167-756-175-175-716-713-103-150-751-107-103-137-652-615-160-576-706-752-756-132-160-160-106-167-107-103-627-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 765-675-756-756-735-756-765-762-765-756-756_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 162-106-132-213 165-617-136-132-132-136-143-135-164-143-102-107-102-761-763-762-762-763-762-762-762-765-762-765-762-762-762-762-765-672-675-765-765-762-672-673-672-675-765-675-7_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 527-267-765-765-607-160-123-175-135-723-137-102-273-127-561-571-547-546-574_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 567-657-627-652-315-576-216-125-652-756-107-137-160-316-671-167-671-167-136-316-235-237-267-263-173-176-261-326-236-236-362-301-132-310-301-301-302-312-132-236-261-236-106-107-1_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 162 612-362 316 316-637-627-617-612 167-167-657 312-756-675-167-163-136 673-761-673-620-627-623-623-672-367-763-670-627-362-672-263-675-672-135-651-570-753-765-134-136-107-617-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 753-671-603-132-103-130-160-103-126-510-316-613-103-103-162-107-102-165-126-132-312-610-617-607-627-612-621-167-176-371-103-107-613-653-613-106-136-163-123-610-167-160-160-167-1_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 567-564-652-265-267-276-726 657-627-627-625-652-675-567_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 261-172-265-175-167-167-672-621-260-135-160-103-106-176-657-167-132-136-132-107-160-167-103-125-167-120-136-106-675-160-106-136-132-103-136-127-172-173-763-163 263-103-136-106-3_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 126-132-130-162-102-102-132-231-123-321-162-306-321-326-163-130-310-130-103_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 627-267-706-576-675-267-216-126-126-173-652-126-163-602-620-261-675-302-326-620-625-627-310-361-602-621-261-167-176-750-570-756-756-375-612-627-162-750-750-751-705-760-237-376-3_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 627-321-621-261-631-261-326-613-136-627-612-621-621-126-163-362-623-126-162-126-126-623-602-120-362-106-103-316-620-621-162-123-132-163-126-163-136-132-136-316-621-623-320-321-6_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 163-361-631-760-760-765-756-526-625-257-546-257-675-257-567-457-362-637-651-567-765-765-765-761-574-756-567-657-562-625-657-261-567-653-625-672-567-165-316-576-163-263-160-136-1_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 176-567-136-106-652-612-365-162-123-107-163-127-271-102-217-173-103-167-173-160-163-134-106-613-106-126-216-261-160-103-126-102-654-216-567-147-106-103-163-106-167-176-106-671-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 637-163-260-672-103-167-675-621-612-632-601-103-103-136-103-561-132-512-162-167 263-106-163-576-157-165-751-516-162-123-156-517-152-136-165-567-657-621-671-605-601-652-103-316-1_V.mp3)
  Segment 21: 31520ms ~ 32420ms (file: 152-651-176-752-675-675-675-672-675-756-154-156-327-327-137-103-130-627-576-576-375-371-103-310-165-257-120-273-217-132-673-623-230-123-534-132-315-765-672-267-362-132-631-361-6_W.mp3)
  Segment 22: 33420ms ~ 33996ms (file: 162-236-261-617-265-627-126-163-136-132 167-617-571_W.mp3)
  Segment 23: 34996ms ~ 35572ms (file: 123-105-167-120-106-103-137-103-321-123-126-160-160-136-157-176-617-167-617-617-136-102-527-176-123-162-132-657-617 136_V.mp3)
  Segment 24: 36572ms ~ 37148ms (file: 136-361-326-235-253-127-132-574-357-357-573-537-237-216-537-576-537-652-675-625-654-625-536-316-536-326-236-156-210-102-160-216-621-130-103-304-310-236-261-632-216-126-326-123-2_W.mp3)
  Segment 25: 38148ms ~ 38724ms (file: 627-167-176-167-103-312-136-316-651-162-650-165-162-130-136-103-103-136-316-106-316-136-106-362-162-136-630-627-613-130-160-612-162-136-160-136-103-316-103-176-261-103-761-106-1_V.mp3)
  Segment 26: 39724ms ~ 40300ms (file: 106-162-103-671-167-163-106-130-612-601-162-163-607-601-607-670-610-106-167-162-213-162-103-162-761-157-103-103-301-301-310-301-103-301-103-301-357-132-312-532-156-163-316-176-1_V.mp3)
  Segment 27: 41300ms ~ 41876ms (file: 137-176-106-160-126-126-326-162-263-671-612-106-236-163-126-650-675-607-617-612-506-506-560-153-163-102-120-130-103-602-605-152-105-170-150-102-103-657-160-106-126-160-612-106-1_V.mp3)
  Segment 28: 42876ms ~ 44316ms (file: 132-123-136-316-362 613 361-176-374-132-251-613-312-632-174-164-413-143 761-761-236 637-276-263_V.mp3)
  Segment 29: 45316ms ~ 45892ms (file: 123-132-126-132 716-176-176-761 126 126-135-130-176-126-130-312-263-135-306-362-216-162-126-103-132-163-165-136-541-715-156-372-160-317-137-103-156-103-316-136-612-675-162-167-1_V.mp3)
  Segment 30: 46892ms ~ 47468ms (file: 130-102-136-123-103-163-136-163-312-123_W.mp3)
  Segment 31: 48468ms ~ 49044ms (file: 127-106-106 260-236-206-236-672-762-276-275-103-301-312-213-132 670-670-620-263-627-362-326-260-267-762-610-160-162-106-126-657-605-607-106-106-106-163-106-106-163-620-260-206-1_V.mp3)
  Segment 32: 50044ms ~ 50620ms (file: 175-351-257-570-756-574-574-526-265-265-265-625-265-371-237-327-321-213-321-231-321-372-362-326-257-357-253-754-327-236 167-165-175-156 156-156-156-517-251-215-215-251-251-251-2_W.mp3)
  Segment 33: 51620ms ~ 52196ms (file: 267-126-103-136-137-207-612-106-167-610-130-210-127-210-136-130-257-327-627-617-607-362-137-103-106-150-621-103-167-561-106-165-162-672-236-165-621-627-574-630-163-267-132-713-5_V.mp3)
  Segment 34: 53196ms ~ 53772ms (file: 153-571-265-275-123-216-130-312-231-302-302 357-621-756-675-120-120-621-326-627-623-627-623 671-627-130-132-310-320_V.mp3)
  Segment 35: 54772ms ~ 55348ms (file: 237-210-210-126-627-267-261-206-650-206-576-567-327-126-213-261-123-213-321-312-261-210-231-273-132-312-261-236-216-126-123-301-132-210-123-302-301-132-712-276-261-672-765-756-6_V.mp3)
  Segment 36: 56348ms ~ 56924ms (file: 617-603-617 672-672-673-673 756-736-321-321-321-312-132-123_W.mp3)
  Segment 37: 57924ms ~ 58500ms (file: 573-357-735-537-257-257-312-321-123-625-267-261-312-261-623-632-316-367-327-157-621-263-625-265-265-265-265-326-321-320-230-325-532-517-517-357-375-317-317-317-235-213-215-213-7_W.mp3)
  Segment 38: 59500ms ~ 60076ms (file: 675-657-576-567-576-756-573-576-576-567-576-576-573-536-573-567-576-675-516-751-756-576-576-765-675-756-132-136-615-675-657-625-526-652-657-657-567-657-163-617-672-617-671-217-6_W.mp3)
  Segment 39: 61076ms ~ 61652ms (file: 326-273-231-625-621-652-672-627-621-265-672-762-627-762-657-657-652-675-625-627-627-627-267-623-627-652-635-625-236-623-627-267-236-326-652-127-632-675-567-675-653-265-672-627-2_W.mp3)
  Segment 40: 62652ms ~ 63228ms (file: 103-613-136-657-136-236-167-726 163-316-136-132-231-153-137-132-130-315-132-312-136-103-103-176-136-315-137-167-637-623-621_V.mp3)
  Segment 41: 64228ms ~ 64804ms (file: 265 612-617 627-623-132-230-715-201-316-126-173-136-175-157-136-175-165-127-132-103-176-106-162-576-316-675-106-136-546-623-162-216-762-162-163-137-167-106-156-231-136-610-136-1_V.mp3)
  Segment 42: 65804ms ~ 66380ms (file: 516-716-516-547-312-132-312-312-130-103-136-312-310-561-176-162 632-632-103-103-132-160-504-510-165-613-103-106-136-675-156-615-165-175-576-216-130-361-562-576-167-263-762-213 3_W.mp3)
  Segment 43: 67380ms ~ 67956ms (file: 301-306-361-301-306-632-623-261-213-236-130-675-607-263-132-657-675-106-326-267-263-106-167-312-320-230-263-126-105-605-267-276-327-326-320 316-612-562-106-132-320-263-623-130-1_W.mp3)
  Segment 44: 68956ms ~ 69532ms (file: 263-261-267-263-261-627-621-627-261-267-632-362-625-267-175-562-572-167-561-567-571-167-165-517-157-517-514-574-567-165-163-165-517-167-176-167-517-617-163-631-361-163-567-517-5_W.mp3)
  Segment 45: 70532ms ~ 71108ms (file: 103-137-103-162-125-513-612-160-103-316-137-160-167-561-371-162-126-130-103-132-106-103-157-125-126-615-506-612-627-103-132-102-103-136-215-157-765-102-761-102-176-132-173-574-1_V.mp3)
  Segment 46: 72108ms ~ 72684ms (file: 670-167-263-671-123-623-103-312-701-107-107-136-560-567-571-173-761-612-675-675 623-617-165-163-136-105-167-106-106-106-627-623-126-362-312-126-260-162-267-130-103-165-162-731-1_V.mp3)
  Segment 47: 73684ms ~ 74260ms (file: 132-136 263-320-301-312-312-231-236-312-231-321-312-321-132-321-571-153-521-521-321-123-356-325-321-263-526-362-326-326-236-765-765-765-362-321_W.mp3)
  Segment 48: 75260ms ~ 75836ms (file: 706-762-106-610-651-167-130-162-621-623-126-123-603-316-130-103-162-156-503-162-136-126-167-106-167-136-507-165-106-103-130-671-675-167-106-621-617-103-316-160-162-106-106-162-1_V.mp3)
  Segment 49: 76836ms ~ 77412ms (file: 136-357-753-753-107-132-137-136-163-301-320-132-175-317-130-310-103-126-310-130-301-302-302-306-130-102-103-102-562-563-567-576-576-567 573-326-326-574-576-571-136-132-136-312 1_W.mp3)
  Segment 50: 78412ms ~ 78988ms (file: 312-216-216-216-103-632-134-132-103-310-301-376-753-167-103-136-136-217-706-176-167-136-163-235-132-103-106-613-126-602-306-607-607-570-506-260-605-106-162-612-627-576-562-625-6_V.mp3)
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
> **Time**: 2026-04-03 07:55:47
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 공부할 내용은 무엇인가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "공부할", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "내용은", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "무엇인가요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "한국어 문법과 회화를 연습할 거예요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "문법과", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "회화를", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "연습할", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "거예요", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "어떤 문법을 가장 먼저 배울까요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "문법을", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "배울까요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "조사와 어미의 활용을 알아봅시다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "조사와", "start_ms": 20, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "어미의", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "활용을", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "알아봅시다", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "예문을 통해서 설명해 주시겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "예문을", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "통해서", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "설명해", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "주시겠어요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "사과가 있습니다 라는 문장이 있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "사과가", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "있습니다", "start_ms": 185, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "라는", "start_ms": 325, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "문장이", "start_ms": 425, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "있어요", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 5.0}
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
"transcript": "여기서 가는 주격 조사입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "여기서", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "가는", "start_ms": 180, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "주격", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "조사입니다", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 5.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "은과 는의 차이점은 무엇인가요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "은과", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "는의", "start_ms": 160, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "차이점은", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "무엇인가요", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "받침의 유무에 따라 쓰임이 달라져요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "받침의", "start_ms": 25, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "유무에", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "따라", "start_ms": 275, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "쓰임이", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "달라져요", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 5.0}
]
},
{
"segment": 12,
"transcript": "학생은 받침이 있어서 은을 써요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "학생은", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "받침이", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "있어서", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "은을", "start_ms": 430, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "써요", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 5.0}
]
},
{
"segment": 13,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 14,
"transcript": "가수는 받침이 없으니까 는을 쓰죠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "가수는", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "받침이", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "없으니까", "start_ms": 285, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "는을", "start_ms": 435, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "쓰죠", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "이제 확실히 이해가 되네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 40, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "확실히", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이해가", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되네요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "그럼 실전 회화 연습을 해볼까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 65, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "실전", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "회화", "start_ms": 295, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "연습을", "start_ms": 395, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "해볼까요", "start_ms": 515, "dur_ms": 60, "phase_lr_deg": 5.0}
]
},
{
"segment": 17,
"transcript": "식당에서 주문하는 상황입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "식당에서", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "주문하는", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "상황입니다", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 5.0}
]
},
{
"segment": 18,
"transcript": "어서 오세요 무엇을 드릴까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어서", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "오세요", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "무엇을", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "드릴까요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "비빔밥 하나와 김치찌개 주세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "비빔밥", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하나와", "start_ms": 185, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "김치찌개", "start_ms": 315, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 475, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "네 알겠습니다 잠시만 기다려 주세요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "알겠습니다", "start_ms": 70, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "잠시만", "start_ms": 210, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "기다려", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "주세요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 5.0}
]
},
{
"segment": 21,
"transcript": "여기 앞접시 좀 주실 수 있나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 55, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "앞접시", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 285, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "주실", "start_ms": 345, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 445, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있나요", "start_ms": 495, "dur_ms": 405, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "네 여기 있습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "여기", "start_ms": 105, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "있습니다", "start_ms": 225, "dur_ms": 350, "phase_lr_deg": 5.0}
]
},
{
"segment": 23,
"transcript": "아주 잘하셨어요 자연스러웠습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "아주", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "잘하셨어요", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "자연스러웠습니다", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 5.0}
]
},
{
"segment": 24,
"transcript": "격식 있는 표현도 배워보고 싶어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "격식", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 160, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "표현도", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "배워보고", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "하십니까 체와 하십시오 체를 알아볼까요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "하십니까", "start_ms": 70, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "체와", "start_ms": 210, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "하십시오", "start_ms": 310, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "체를", "start_ms": 450, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "알아볼까요", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 5.0}
]
},
{
"segment": 26,
"transcript": "주로 공식적인 자리에서 사용해요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "주로", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "공식적인", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "자리에서", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "사용해요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 27,
"transcript": "뉴스와 발표에서 많이 들었어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "뉴스와", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "발표에서", "start_ms": 175, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 315, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들었어요", "start_ms": 425, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "처음 뵙겠습니다 안녕하십니까 처럼요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "처음", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "뵙겠습니다", "start_ms": 175, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "안녕하십니까", "start_ms": 415, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "처럼요", "start_ms": 1230, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "맞아요 아주 정확한 예시입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 25, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "아주", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "정확한", "start_ms": 275, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "예시입니다", "start_ms": 415, "dur_ms": 160, "phase_lr_deg": 5.0}
]
},
{
"segment": 30,
"transcript": "고맙습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "고맙습니다", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "질문이 더 있다면 언제든지 물어보세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 60, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "더", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "있다면", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "언제든지", "start_ms": 380, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "물어보세요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 5.0}
]
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "받침이 있는 단어를 더 알고 싶어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "받침이", "start_ms": 50, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "단어를", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "알고", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "책 연필 창문 같은 단어들이 있죠",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "책", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "연필", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "창문", "start_ms": 250, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "같은", "start_ms": 370, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "단어들이", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 5.0}
]
},
{
"segment": 35,
"transcript": "그럼 각각 은을 붙여서 연습해 보세요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 70, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "각각", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "은을", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "붙여서", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "연습해", "start_ms": 530, "dur_ms": 40, "phase_lr_deg": 5.0}
]
},
{
"segment": 36,
"transcript": "책은 연필은 창문은 맞나요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "책은", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "연필은", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "창문은", "start_ms": 300, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "맞나요", "start_ms": 450, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "완벽합니다 발음도 아주 좋아요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "완벽합니다", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "발음도", "start_ms": 230, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "아주", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "좋아요", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 5.0}
]
},
{
"segment": 39,
"transcript": "받침이 없는 단어들도 해볼게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "받침이", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "단어들도", "start_ms": 285, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "해볼게요", "start_ms": 435, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "학교 의자 모자 는을 붙여보세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "학교", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "의자", "start_ms": 170, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "모자", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "는을", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "붙여보세요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 5.0}
]
},
{
"segment": 41,
"transcript": "학교는 의자는 모자는",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "학교는", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "의자는", "start_ms": 185, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "모자는", "start_ms": 335, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "참 잘했어요 응용력이 뛰어나시네요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "참", "start_ms": 25, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "잘했어요", "start_ms": 95, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "응용력이", "start_ms": 245, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "뛰어나시네요", "start_ms": 415, "dur_ms": 160, "phase_lr_deg": 5.0}
]
},
{
"segment": 43,
"transcript": "칭찬해 주셔서 힘이 나요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "칭찬해", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 185, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "힘이", "start_ms": 315, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "나요", "start_ms": 415, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "다음 시간에는 무엇을 배울까요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시간에는", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배울까요", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "동사의 과거형을 공부할 계획이에요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "동사의", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "과거형을", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "공부할", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "계획이에요", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 5.0}
]
},
{
"segment": 47,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 48,
"transcript": "재미있겠네요 기대됩니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "재미있겠네요", "start_ms": 60, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "기대됩니다", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "오늘 수고 많으셨습니다 안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "수고", "start_ms": 145, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "많으셨습니다", "start_ms": 245, "dur_ms": 180, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 435, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 5.0}
]
},
{
"segment": 50,
"transcript": "선생님도 안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "선생님도", "start_ms": 25, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 195, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 355, "dur_ms": 220, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:55:47*
