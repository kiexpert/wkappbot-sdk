# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=27304  time=07:35:53  uptime=10930s  mem=147MB  system_processes=410"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_073541.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 765-617-617-167-136-163-176-176-176-163-765-673-675-673-673-673-761-671-671-671-761-673-761-675-671-675-675-675-657-675-751-517-516-561-576-576-574-574-517-576_W.mp3)
  Segment 2: 1576ms ~ 11692ms (file: 213-236-237-723-130-312-301-302-276-267-726-267-267-632-710-671-627-302-123-765-310-103-721-765-267-267-276-267-273-127-267-267-316-132-217-136-716-361-321-302-310-317-103-317-7_W.mp3)
  Segment 3: 12692ms ~ 20216ms (file: 310-103-541-103-310-103-260-524-524-236-326-217-126-172-167-143-524-205-203-103-107-302-206-231-321-263-230-132-103-134-213-201-132-301-310-123-132-103-103-102-102-103-102-120-1_V.mp3)
  Segment 4: 21216ms ~ 21792ms (file: 176-167-176-761-713-173 135-137-317-173-175-761-176-173-173-175-175-175-157-716-715-176-176-176 135-175-173-175-176-173-173-175-716-176-167-176-176-716-176-176-176 135-135-173-1_V.mp3)
  Segment 5: 22792ms ~ 23368ms (file: 362-352-312-215-571-754-136-321-237-325-315-106-206-326-316-312-316-130-312-130-173-725-567-176-217-413 572-103-312-132-132-572-572-573-152-126-514-570-251-261-260-625-216-231-7_W.mp3)
  Segment 6: 24368ms ~ 28328ms (file: 132 762 163-253-637-576-716-657-253-136-376-716-173-317-316-736-763-673-367-357-163-761-236-527-617-623-375-576-607-376-367-763-673-176-235-716-316-572-273-507-723-325-762-705-1_W.mp3)
  Segment 7: 29328ms ~ 29904ms (file: 715-570-750 517-570-517-157-715-175-107-103-175-751-105-130-132-123-136-163-136 130-103-107-105-157-107-310-103-170-170-157-170-710-105-175-103-175-170-170-175-715-137-120-120-1_V.mp3)
  Segment 8: 30904ms ~ 38032ms (file: 627-267-310-172-127-371-702-702-603-162-102-321-531-306-213-132-102-103-106-103-230-130-216-126-210-203-210-203-203-203-231-267-316-206-216-132-103-230-216-316-263-312-361-106-2_W.mp3)
  Segment 9: 39032ms ~ 39608ms (file: 312-307-317-357-316-752-235-574-736-321-762-572-531-523-765-267-526-261-765-620-652-726-625-560-675-765-756-627-507-321-312-123-576-765-570-265-257-365-507-253-756-705-326-371-1_V.mp3)
  Segment 10: 40608ms ~ 41184ms (file: 763-763-756-765-756-763-657-675-675-367-612-651 543-653 175 362_W.mp3)
  Segment 11: 42184ms ~ 48448ms (file: 230-217-267-260-321-752-265-620-236-231-102-230-321-130-213-201-203-126-201-216-263-572-542-567-675-675-627-321-376-725-725-123-371-723-756-765-756-623-312-173-130-103-765-637-1_W.mp3)
  Segment 12: 49448ms ~ 50024ms (file: 137-130-103-130-132-132-123-123-123_W.mp3)
  Segment 13: 51024ms ~ 51600ms (file: 307-507-573-576-357-135-315 137-573-576-135-301-301 675-650-602-605-603-630-360-316-356-657_W.mp3)
  Segment 14: 52600ms ~ 58900ms (file: 210-267-231-126-120-732-376-320-362-321-267-267-725-132-213-317-153-163-163-136-631-631-136-136-316-163-136-316-136-263-625-613-236-310-516-536-316-361-321-310-136-207-123-261-2_W.mp3)
  Segment 15: 59900ms ~ 66488ms (file: 310-315-237-310-231-130-321-213-321-301-231-261-302-326-260-621-261-236-302-320-312-106-103-162-126-301-103-213-321-637-316-263-106-106-105-137-130-751-216-127-216-213-132-136-1_W.mp3)
  Segment 16: 67488ms ~ 68064ms (file: 175-107-170-107-715-170-710-170-735-175-175-175-175-173-175-173-175-173-715-175-175-715 705-710-175-571-710-710-716-170-715-701-175-175-710-175-173-716-175-715-175-173-175-175-1_V.mp3)
  Segment 17: 69064ms ~ 77596ms (file: 203-321-164-106-136-310-102-206-230-312-310-231-120-102-263-162-162-754-130-130-103-571-625-756-132-751-761-136-312-231-236-236-236-765-267-726-236-627-276-754-576-765-507-756-2_W.mp3)
  Segment 18: 78596ms ~ 89720ms (file: 132-132-320-316-230-103-371-132-106-176-570-260-206-205-620-123-523-753-132-216-267-312-573-267-261-206-261-123-320-230-102-126-126-103-760-130-301-756-102-236-231-236-231-106-3_W.mp3)
  Segment 19: 90720ms ~ 98172ms (file: 137-175-103-137-301-312-132-103-107-312-710-170-301-751-207-136-310-372-273-653-130-317-230-230-201-275-120-170-130-137-750-143-153-301-173-312-230-106-260-320-163-375-123-132-2_V.mp3)
  Segment 20: 99172ms ~ 99748ms (file: 652-132-126-352-325 253-126-316-213-173-764-264-215-326_V.mp3)
  Segment 21: 100748ms ~ 101324ms (file: 163 765 316-316-316-631-361-631-613-631-361-576-316-276-672-762-726-236-216-176-236-273-327-126-152-257-215-267-216-217-206-127-216-312-213-136-132-576-271-267-127-216-152-527-2_W.mp3)
  Segment 22: 102324ms ~ 102900ms (file: 356-573-536-571-537 576-571-136-137 671 136-136-362-267-632-206-267-620-276-562-671-213-315-703-752-236-362-137-752-321-705-237-562-273-756-627-257-325-261-617-134-123-276-627-2_V.mp3)
  Segment 23: 103900ms ~ 104476ms (file: 751-675-675-675-765-765-675-765-765-675-765-765-675-765-675-675-657-675-675-675-675-675-675 675_W.mp3)
  Segment 24: 105476ms ~ 106052ms (file: 570-576-510-527-572-527-652-517-157 571-571-513-517-517-574-576-576-571-576-571-751-751-576-517 567-567-576-315-715-571-570-507-703-173-716-310-175-710-130-136-327-372-710-137-3_W.mp3)
  Segment 25: 107052ms ~ 114036ms (file: 576-205-205-701-206-257-120-210-120-102-125-167-216-726-126-167-715-175-216-261-137-217-716-106-576-203-627-276-705-370-531-137-102-213-514-267-157-127-276-276-102-627-267-102-7_W.mp3)
  Segment 26: 115036ms ~ 121624ms (file: 126-712-321-712-321-327-132-273-327-132-312-103-106-316-316-132-123-132-231-120-103-136-307-613-756-315-361-312-321-326-317-173-173-312-356-126-230-215-120-601-152-106-123-132-1_W.mp3)
  Segment 27: 122624ms ~ 129392ms (file: 107-310-312-301-106-231-236-263-103-310-361-263-326-602-316-316-317-312-132-310-362-316-320-632-263-267-623-263-267-321-310-315-236-326-132-130-236-107-231-316-136-361-367-130-3_W.mp3)
  Segment 28: 130392ms ~ 136728ms (file: 167-176-106-571-130-563-706-167-106-726-576-350-173-103-723-275-136-273-725-570-276-352-163-107-137-132-125-217-573-275-235-612-106-231-765-267-567-612-570-571-652-627-105-123-2_V.mp3)
  Segment 29: 137728ms ~ 138304ms (file: 126-132-120-123-120-132-132-132-123-102-123-130-315-135-130-103-175-175-107-175-701-705-170-150-175-157-175-715-103-120-123-123-132-132-312-123-132-312-132-130-132-102-301-751-5_W.mp3)
  Segment 30: 139304ms ~ 139880ms (file: 571-572-572-572-750-750-750-570-570-571-715-517 571-571-517-571-571-570-570-574-574-570-574-570-571-573-574-571-574-571-574-571-571-570-576-750-571-571-572 574-571-574-570-571-5_W.mp3)
  Segment 31: 140880ms ~ 141456ms (file: 731-673-657-675-671-761-675-675-675-675-765-765-675-675-670-157-103-132-301-310-132-132-312 715-123-120-132-130-312-361-132-312-132 163-163-163-165-163-137-135-153-157-153-312-1_W.mp3)
  Segment 32: 142456ms ~ 149404ms (file: 130-103-103-267-231-237-123-301-137-276-267-213-231-312-236-123-235-325-231-213-213-236-230-203-213-213-231-267-213-203-123-102-210-102-213-275-235-321-302-126-213-103-217-257-2_W.mp3)
  Segment 33: 150404ms ~ 156560ms (file: 231-176-126-167-671-627-201-120-165-673-162-165-571-501-357-672-627-762-217-672-267-763-312-571-160-317-237-672-673-756-715-360-631-632-361-612-371-546-762-312-312-102-132-721-3_W.mp3)
  Segment 34: 157560ms ~ 158136ms (file: 156-517-157-715 153-157-175-157-157-615-157-751-157-157-517-157-175-657-657-657-156-156-156-157-153-157-175-157-517-157-517-157-157-517-517-517-517-517-715-751-105-103-130-130-1_V.mp3)
  Segment 35: 159136ms ~ 164716ms (file: 572-726-726-756-637-173-730-713-273-276-276-573-715-721-721-317-756-237-321-621-276-267-126-765-237-726-321-261-137-672-672-672-756-672-756-312-672-672-261-710-160-267-263-362-2_W.mp3)
  Segment 36: 165716ms ~ 173060ms (file: 105-610-612-652-726-756-276-276-572-135-756-370-761-617-627-213-716-756-267-267-756-752-672-231-106-237-163-103-163-276-321-106-263-273-321-103-136-765-263-376-106-617-601-756-1_W.mp3)
  Segment 37: 174060ms ~ 180504ms (file: 163-705-705-756-710-107-172-715-103-176-132-127-153-105-173-103-326-126-102-170-137-102-276-275-531-513-270-270-120-276-312-326-276-230-715-132-270-267-203-107-541-524-726-524-3_W.mp3)
  Segment 38: 181504ms ~ 182080ms (file: 625-567-726-762-765-756-736-627-276-276-276-275-275-257-267-327-302-237-326-237-720-752-275-237-327-207-270-267-270-237-372-321-327-237-273-732-752-732-317-273-276-726-762-267-2_W.mp3)
  Segment 39: 183080ms ~ 190352ms (file: 726-236-267-260-570-257-715-270-276-507-376-263-267-207-326-637-672-236-267-572-312-312-365-321-231-517-736-267-627-137-136-260-670-267-103-276-703-720-310-206-213-514-276-267-7_W.mp3)
  Segment 40: 191352ms ~ 191928ms (file: 326-276-326-137-572-132-712-572-621-257-752-723-527-261-256-502-750-267-261-532-652-132-315-132-312-265-531-172-725-267-536-321-263-376-357-570-735-572-756-573-236-326-376-315-5_V.mp3)
  Segment 41: 192928ms ~ 193504ms (file: 752-752-752-752-752-752-751-752-752-752-752-752-751-752-752 753-751-751-751-751-752-752-752-752-752-752-753-753-571_W.mp3)
  Segment 42: 194504ms ~ 195080ms (file: 762-625-671-672-576-257-265-267-506-236-576-271-702-562-570-670-573-726-765-621-762-267-621-672-236-267-673-320-263-263-627-672-762-163-372-123-263-507-671-265-675-653-623-675-6_V.mp3)
  Segment 43: 196080ms ~ 196656ms (file: 175-175-751-751-570-573-751-517-751-570-750-573 574-574-572-573-574-574-576_V.mp3)
  Segment 44: 197656ms ~ 200860ms (file: 123-213-102-150-203-602-206-201-312-620-103-132_V.mp3)
  Segment 45: 201860ms ~ 208304ms (file: 576-103-320-123-103-106-106-103-106-310-103-206-326-250-213-765-732-720-217-167-107-310-201-206-231-102-132-236-130-135-103-120-210-102-210-102-132-210-231-352-750-320-157-130-1_V.mp3)
  Segment 46: 209304ms ~ 209880ms (file: 572-267-326-275-756-702-756-265-725-523-752-572-507-576-572-576-267-762-572-275-351-354-572 275-253-176-726-236-502-657-726-725-217-276-230-312-607-267-572-327-507-576-571-726-5_V.mp3)
  Segment 47: 210880ms ~ 219340ms (file: 317-137-321-312-713-307-310-213-132-123-231-326-321-213-102-106-576-547-106-132-176-765-756-715-756-765-675-672-210-132-213-576-231-213-123-230-261-172-716-310-132-321-751-751-7_W.mp3)
  Segment 48: 220340ms ~ 225344ms (file: 765-362-217-123-576-265-175-157-672-213-120-261-213-321-612-102-210-231-321-217-102-612-327-267-237-126-123-213-123-126-102-103-102-172-173-317-206-126-326-312-213-123-123-102-1_W.mp3)
  Segment 49: 226344ms ~ 230628ms (file: 132-701-130-136-715-321-276-271-302-170-103-132-123-132-317-310-137-173-127-302-320-236-301-237-162-765-325-752-103-132-623-270-127-123-167-310-213-321-123-132-102-103-103-321-3_W_stt_tough_love_of_life_of.mp3)
  Segment 50: 231628ms ~ 232204ms (file: 726-762-726-762-216-673-123-657-756-576-576-657-623-326-316-312-175-136-167-165-156-165-167-165-167-136_W.mp3)
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
> **Time**: 2026-04-06 07:36:54
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 2,
"transcript": "해서 감사합니다 저 이게 정말 누구나 조금 편하게 접근이 가능하다면 어 진짜 이게 엄청난 변화가 일어날 것 같아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 10, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 320, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1110, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "누구나", "start_ms": 1780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 2190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "편하게", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "접근이", "start_ms": 2960, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가능하다면", "start_ms": 3420, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 4030, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 4190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 4550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "엄청난", "start_ms": 4860, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "변화가", "start_ms": 5320, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일어날", "start_ms": 5780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 6190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 6350, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "전혀 없었다 우리는 지금 급격하게 요동치는 변화의 물결 한가운데 놓여 있다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "전혀", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없었다", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리는", "start_ms": 860, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 1270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "급격하게", "start_ms": 1630, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "요동치는", "start_ms": 2240, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "변화의", "start_ms": 2800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "물결", "start_ms": 3260, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한가운데", "start_ms": 3670, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "놓여", "start_ms": 4230, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 4590, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "음 맛있어요 가야 할 예정인데 그렇지 그렇지 근데 이건 이게 올 것 같아 원래 만 원이었는데 네 분이니까 어이구 감사합니다 오",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "음", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 230, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "의 취향과 의도에 따라 모든 될 수 있다 사로 처럼 당뇨가 있어 가지고 맥주 마시면",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "의", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "취향과", "start_ms": 140, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "요 하면은 결국은 API를 계속 호출하면서 그만큼 돈이 굉장히 많이 들어가는 구조인데 맥북 프로 16인치라든가 맥 미니 요런 것들 비교해 보면은 기본적으로 메모리 대역폭이",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "요", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하면은", "start_ms": 210, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "10년에 아 그때 말했지 아 집 지은 것 축하해요 진짜로 고마워 고마워 이게 이제 우리 집 있으니까 남편 잘 해줘요",
"speaker": "speaker_6",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "10년에", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "결과라고 보는 게 더 정확하다고 볼 수 있죠 보스턴 다이내믹스의 핵심적인 기술을 핵심 전략 자산으로 만들 역할의 기업",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "결과라고", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "다니까 아 내 취재가 헛되지 않고 나름대로 일리가 있었던 거구나 라는 생각을 하게 됐습니다 그리고 이번 검찰 개혁은 결론을 말씀드리면 검사의 권한을 제한하는 게 결국은 핵심입니다",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "다니까", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "상상이 안 되죠 어 저기 끝에 그렇지 완전 초 대박인데 지금 저 끝에 막 연예인 분들 오시는 그런 스테이지 생기고 막 공연도 하고 술도 먹고",
"speaker": "speaker_9",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "상상이", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "그런데 집 짓기가 어디 그렇게 만만한 일이던가요 이런 일도 있었답니다 콩고르드 42",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그런데", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "엄마 바이바이 너희들 들어와 일로와 뭐 바이바이",
"speaker": "speaker_11",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "엄마", "start_ms": 10, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "아니 아니야 아직이에요 아직 아직 아직이요 연습 많이 해서 아 감사합니다 감사합니다",
"speaker": "speaker_12",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아니", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "구로 하는 사람들은 뭐 담보 대출도 아니니까 있는 게 있어야 뭐 담보를 맡기죠 그럴 게 없으니까 2천만 원도 겨우",
"speaker": "speaker_13",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "구로", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "AI가 일상을 파고드는 속도는 가파르다 올 상반기만 해도 새로운 서비스가 잇따라 출시될",
"speaker": "speaker_14",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "AI가", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "생태 생태 구우면 동태 말리면 코다리 오 전문가시네요 영어는 선생님 야 근데 밥이랑 같이",
"speaker": "speaker_15",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "생태", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "80년 가까이 서방의 가치를 공유해 온 북대서양 조약 기구는 이제 철저히 비용과 편익을 따지는 거래형 관계로",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "80년", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "하고 9시 30분 정도에 확실히 이제 주도 테마랑 대장주를 확인하고 매수했고 대한 광통신을 비롯해서 통신 관련주가 아침에 굉장히",
"speaker": "speaker_17",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "하고", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "장소에 구애받지 않고 자유롭게 일하는 모습은 여전하지만 지금의 발리는 예전",
"speaker": "speaker_18",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "장소에", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "중국인 특유의 위생 문제를 일으키는 모습까지 담겨 있었기 때문인데요 당연히 프랑스 여론은 폭발합니다 사실 우리나라도 나설 때였습니다 앞서 말",
"speaker": "speaker_19",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "중국인", "start_ms": 30, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "서로 양보가 필요합니다",
"speaker": "speaker_20",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "서로", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 810, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "와 제대로 해결해 주고 집보다 더 비싼 거 아니에요 어 예 제 로망입니다 예 요거 하나",
"speaker": "speaker_21",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "응 언니 저기 안에 포도도 안에 포도도 있어 아기 언제 나와 아기 나오기 싫은데 왜 아기 나오기 싫어 나도 몰라 지금",
"speaker": "speaker_22",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "응", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0}
]
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
"transcript": "오오 진짜 맛있다",
"speaker": "speaker_23",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오오", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "맛있다", "start_ms": 660, "dur_ms": 550, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:36:54*
