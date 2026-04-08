# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=7628  time=07:12:44  uptime=11488s  mem=482MB  system_processes=415"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_071158.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 367-135-371-137-670-167-103-132-135-105-156-156-516-510-156-516-156-150-310-130-130-310-137-317-317-371-317-371-307-301-307-730-370-307-130-130-103-317-103-310-130-103-130-103-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 506-563-357-537-376-367-507-371-326-753-317-573-576-754-375-570-176-531-561-503-270-237-576-502-276-752-627-570-312-132-715-276-276-621-350-257-750-236-267-263-573-267-236-705-5_V.mp3)
  Segment 3: 3152ms ~ 8876ms (file: 136-163-165-136-320-263-320-312-162-263-106-120-136-213-123-320-320-352-320-132-130-321-120-715-132-275-327-537-317-123-162-612-236-326-326-536-573-235-231-102-102-237-102-126-1_V.mp3)
  Segment 4: 9876ms ~ 10452ms (file: 326-132-560-153-365-652-537-537-573-375-751-376-571-756-573-536-576-351-357-576-576 507-712-573-103-762-251-526-675-576-152-261-275-752-167-265-537-132-137-537-127-153-172-157-6_W.mp3)
  Segment 5: 11452ms ~ 17032ms (file: 320-715-317-130-130-123-216-301-137-361-130-310-713-712-712-120-201-163-103-102-107-120-321-130-102-106-103-107-127-172-320-203-206-301-310-107-372-123-130-137-167-160-130-130-3_W.mp3)
  Segment 6: 18032ms ~ 23900ms (file: 310-267-302-326-123-236-260-263-572-172-203-572-572-260-231-231-260-267-267-206-523-215-213-236-712-372-213-507-203-753-574-756-261-236-263-276-326-705-302-316-760-607-326-267-5_V.mp3)
  Segment 7: 24900ms ~ 25476ms (file: 571-157-137-137-137-137-172-732-376-613 765-762-632-371-713-657-321-637-215-235-126-132-267-127-237-627 715-173-735-371-173-715 576-574-510-513-157-157-157-153-315-153-153-157-1_W.mp3)
  Segment 8: 26476ms ~ 27052ms (file: 317-750-627-627-321-713-631-621-123-761-750-132-130-132-725-137-156-267-570-132-317-321-257-275-271-267-273-726-236-327-256-621-703-237-265-574-327-523-527-602-235-267-273-321-2_V.mp3)
  Segment 9: 28052ms ~ 28628ms (file: 612-231-762-675-763-675-675-260-326-321-763-375-367 627-672_W.mp3)
  Segment 10: 29628ms ~ 37800ms (file: 105-107-236-136-213-130-260-713-107-173-705-265-152-672-702-137-751-275-715-527-276-567-251-570-627-321-102-125-152-765-106-102-325-130-135-103-123-132-521-326-213-312-123-376-5_V.mp3)
  Segment 11: 38800ms ~ 39376ms (file: 720-726-237-602-132-275-576-657-261-257-126-672-705-236-527-175-173-537-675-627-506-276-365-235-675-315-325-567-235-257-326-326-362-321-713-371-312-316-361-316-361-361-316-361-3_W.mp3)
  Segment 12: 40376ms ~ 51572ms (file: 312-132-310-312-301-216-213-623-102-103-173-627-712-271-261-672-175-702-132-310-632-132-312-610-620-261-201-203-103-105-510-315-360-173-351-361-213-236-321-213-321-231-765-106-1_W.mp3)
  Segment 13: 52572ms ~ 59232ms (file: 561-567-165-571-103-167-617-614-706-137-136-103-176-756-167-716-105-671-675-230-236-726-176-617-167-625-601-675-617-167-263-573-107-176-510-276-267-236-236-576-367-267-623-362-5_W.mp3)
  Segment 14: 60232ms ~ 60808ms (file: 236-276-520-236-276-507-562-725-672-265-316-321-231-357-312-302-652-206-706-627-627-627-316-625-627-237-237-263-675-567-621-231-726-570-156-670-765-256-231-312-271-752-756-561-6_V.mp3)
  Segment 15: 61808ms ~ 62384ms (file: 261-265-752-265-275-675-265-736-152-205-726-263-236-702-326-267-132-672-672-576-567-576-267-627-627-632-673-670-672-265-216-562-516-632-321_V.mp3)
  Segment 16: 63384ms ~ 69576ms (file: 206-321-310-361-312-530-176-256-160-326-123-132-316-237-231-356-573-356-312-362-106-132-123-716-165-130-167-375-517-302-751-576-653-657-576-301-321-132-312-130-705-316-135-132-7_W.mp3)
  Segment 17: 70576ms ~ 71152ms (file: 547-574-517-657-571-162-567-315-317-715-750-102-136-270-132-567-167-756-753-136-371-173-276-317-216-326-216-501-352-127-263-312-321-312-321-765-623-527-752-726-132-570-675-765-6_V.mp3)
  Segment 18: 72152ms ~ 72728ms (file: 506-567-573-536-731-760-310-317-136-163-106-637-631-623-271-567-672-217-701-357-673-673-367-736-137-731-137-236-317-132-310-162-507-312-136-361-305-316-310-513-132-132-317-132-1_W.mp3)
  Segment 19: 73728ms ~ 74304ms (file: 726-257-573-675-123-756-267-265-570-725-257-524-726-570-263-762-276-125-763-672-765-325-237-267-713-576-765-576-371-675-726-356-372-573-276-532-576-312-267-276-602-327-627-763-3_V.mp3)
  Segment 20: 75304ms ~ 75880ms (file: 325-257-275-167-527-235-276-753-372-256-627-574-267-253-326-132-217-326-276-136-520-253-567-263-754-256-312-276-231-507-756 716-720-132-756-267-754-725-352-326-705-627-765-235-2_V.mp3)
  Segment 21: 76880ms ~ 77456ms (file: 265-172-726-571-517 576-726-327-765-576-572-257-253-236-572-130-627-605-127-362-236-352-215-235-256-265-506-326-753-236-267-267-267-632 756-750-137-103-317-135-375-725-570-672-2_V.mp3)
  Segment 22: 78456ms ~ 79032ms (file: 627-605-357-173-315-265-165-512-571-526-613-673-763-631-163-312-317-315-130_W.mp3)
  Segment 23: 80032ms ~ 85792ms (file: 317-312-132-123-326-321-216-126-261-125-123-371-730-231-203-320-230-132-715-275-132-103-231-312-361-627-276-523-257-752-756-261-675-326-326-310-132-213-536-107-170-105-510-123-2_W.mp3)
  Segment 24: 86792ms ~ 94712ms (file: 236-752-756-756-756-126-102-120-265-265-362-132-321-572-236-574-130-574-574-576-130-753-765-130-310-310-137-231-130-132-136-756-267-261-310-357-213-321-103-617-275-260-310-756-1_W.mp3)
  Segment 25: 95712ms ~ 96288ms (file: 652-562-172-173-175-175-701-576-571-562-132-103-312-316-316-203-320_V.mp3)
  Segment 26: 97288ms ~ 103444ms (file: 706-137-201-750-750-276-267-726-725-257-750-576-217-107-716-127-710-267-317-712-752-725-310-267-627-527-107-756-275-257-713-756-271-736-623-627-761-106-672-620-572-271-507-302-3_W.mp3)
  Segment 27: 104444ms ~ 105020ms (file: 765-673-326-367-376-312-376-357-316-316-321-613-163-106-713-571-573-763 160-106-160-136-130 103-261-521-501-357-327-231-327-673-310-106-671-617-137-716-761-716-761-761-761-716_W.mp3)
  Segment 28: 106020ms ~ 106596ms (file: 653-574-574-517-137-371-173-367-325-735-371_W.mp3)
  Segment 29: 107596ms ~ 114652ms (file: 130-627-754-701-507-162-236-267-261-572-312-263-273-263-576-237-237-267-217-531-103-710-103-231-572-236-276-267-706-275-765-276-103-756-167-712-715-267-271-210-231-103-167-162-5_W.mp3)
  Segment 30: 115652ms ~ 116228ms (file: 231-513-752-765-623-265-263-257 136-761-372-570-213-570-576-725-235-326-725-765-257-572-275-273-576-752-756-235-275-256-527-657_V.mp3)
  Segment 31: 117228ms ~ 117804ms (file: 271-321 764-756-756-365-540-574-130-310-307-157-310-132-301-317-130-756-753-536-675-357-632-635-613-675-753-325-362-276-213-657-765-361-632-130-103-130-130-130-310-130-103-173-1_V.mp3)
  Segment 32: 118804ms ~ 123916ms (file: 176-261-731-750-275-765-756-702-132-675-576-756-675-752-136-260-312-103-751-570-756-321-206-107-201-257-310-163-257-712-217-327-375-273-273-752-752-752-176-201-267-263-120-206-2_W.mp3)
  Segment 33: 124916ms ~ 125492ms (file: 756-710-710-175-715-715-710-750-751-715-751-750-750 617-617-165-615-715-167-170-170-715-715-701-715-710-750-751-751-170-710-701-705-750-715-150-715-751-750-705 617-675-657-576-5_V.mp3)
  Segment 34: 126492ms ~ 134160ms (file: 652-631-615-615-627-631-261-672-316-736-326-362-627-627-726-672-362-763-130-301-310-765-735-726-267-267-267-312-137-267-263-201-260-261-216-312-103-307-137-136-317-230-206-216-2_W.mp3)
  Segment 35: 135160ms ~ 135736ms (file: 671-657-675-673-671-675-675-675-671-651-615-637-361-631-621-612-312-162-167-165-613-635-765-675-567-651-567-567-615-657-567-657-637-671-615-561-651-613-657-651-617-615-617-635-6_W.mp3)
  Segment 36: 136736ms ~ 137312ms (file: 376-723-731-672-237-532-307-762-507-312-275-126-627-570-235-657-762-635-673-631-657-615-637-137-576-751-175-516-756-657-736-675-657-567-567-576-756-567-567_W.mp3)
  Segment 37: 138312ms ~ 145008ms (file: 715-507-275-762-261-217-132-675-762-271-621-207-320-310-531-715-570-123-260-260-375-173-706-571-263-623-317-517-526-261-675-310-607-375-720-236-260-263-213-315-310-273-523-271-2_W.mp3)
  Segment 38: 146008ms ~ 150292ms (file: 103-103-312-316 132-132-231-206-620-213-132-306-623-261-260-130_V.mp3)
  Segment 39: 151292ms ~ 157988ms (file: 260-320-627-236-321-263-135-130-231-326-357-735-705-276-765-136-103-231-263-765-613-627-126-102-625-326-312-132-672-103-621-120-210-163-167-167-167-761-627-762-120-103-106-216-3_W.mp3)
  Segment 40: 158988ms ~ 163344ms (file: 317-723-726-267-236-321-321-320-267-206-326-326-321-321-267-321-321-321-721-732-726-712-632-312-361-230-237-271-302-230-312-326-236-231-236-632-632-270-732-321-302-263-321-237-3_W.mp3)
  Segment 41: 164344ms ~ 164920ms (file: 321-750-362-321-570-126-267-751-176-137-723-532-572-213-236-236-263-137-762-312-625-326_V.mp3)
  Segment 42: 165920ms ~ 166496ms (file: 571-570-517-576-675-765-765-261-705-675-736-756-705-321-572-207-123-752-375-132-317-310-321-310-315-130-130-130-316-631-316-310-132-103-712-327-236-216-312-567-312-235-312-357-1_V.mp3)
  Segment 43: 167496ms ~ 168072ms (file: 735-765-735 560-751-127-672-752-127-752-623-572-175-576-513-627-263-257-675-612-627-725-527-527-507-132-103-763-132-231-572-571-276-502-126-267-721-576-572-517-624-267-752-507-6_V.mp3)
  Segment 44: 169072ms ~ 169648ms (file: 362-263-735-675-765-765-637-136-137 713-765-761-731-176_W.mp3)
  Segment 45: 170648ms ~ 171224ms (file: 567-657-675-675-617-673-617-761-716-167-167-617-615-615-615-576-765-765-675-765-761-672-673-671-675-765-576-765-673-761-675-675-672-671-631-613-167-637-576-675-765-675-675-657-6_W.mp3)
  Segment 46: 172224ms ~ 172800ms (file: 567-536-315-563-276-567-351-563-756-536-357-132-356-351-317-756-635-316-312-312-657-175-675-657 754-167-625-137-301-316-312-321-231-321-301-136-361-361-301-132-132-301-612-327-3_V.mp3)
  Segment 47: 173800ms ~ 180856ms (file: 276-170-756-267-261-163-106-105-170-573-175-176-105-601-571-276-762-267-276-236-574-752-275-107-137-267-756-756-675-350-751-106-103-653-162-576-156-510-236-136-162-321-237-327-2_W.mp3)
  Segment 48: 181856ms ~ 192728ms (file: 102-103-302-231-135-231-713-137-327-317-301-320-321-207-263-132-130-576-156-136-163-613-132-701-132-136-270-103 120-103-103-102-120-126-103-132-103-167-715-753-751-312-123-527-5_V.mp3)
  Segment 49: 193728ms ~ 194304ms (file: 576-576-706-657-201-623-250-215-231-215-267-570-256-725-627-236-726-265-213-273-263-231-327-327-527-627-326-761-572-572 261-275 573-207-673-273-623-540-230-527-257-207-531-213-6_V.mp3)
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
> **Time**: 2026-04-06 07:14:22
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "Hello, everybody! Welcome to the show!",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "Hello,", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "everybody!", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "Welcome", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 470, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "show!", "start_ms": 510, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "포커스 양보가",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "섰던 거죠 그게 현대차였다는 겁니다 구글 소프트웨어 기술 가 차례로 인수했다가 다시 매각한 것도 이런 문제와 무관하지가 않은데 그런데 최근에는 약간 바뀌고 있지요 현대차가 아틀라스를 일정 궤도에 올려",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "섰던", "start_ms": 10, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 460, "dur_ms": 170, "phase_lr_deg": 0.0},
{"word": "현대차였다는", "start_ms": 640, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 1100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "구글", "start_ms": 1400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "소프트웨어", "start_ms": 1610, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 2020, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가", "start_ms": 2210, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "차례로", "start_ms": 2270, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "인수했다가", "start_ms": 2530, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 2940, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "매각한", "start_ms": 3150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 3410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 3570, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "문제와", "start_ms": 3760, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "무관하지가", "start_ms": 4020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "않은데", "start_ms": 4380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 4640, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "최근에는", "start_ms": 4850, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "약간", "start_ms": 5140, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "바뀌고", "start_ms": 5330, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있지요", "start_ms": 5540, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "God, look at that. Some more extra fresh, fresh, fresh stuff. Extra fresh stuff. Yeah, from the Minari. What do you think, Chef?",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "God,", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "look", "start_ms": 90, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 150, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "that.", "start_ms": 190, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "Some", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 320, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "extra", "start_ms": 370, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "fresh,", "start_ms": 420, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "fresh,", "start_ms": 460, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "fresh", "start_ms": 500, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "stuff.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "숨겨둔 곳이 있답니다 아 여기는 신랑 어쨌든 나무꾼 놀이터여갖고 신랑 공간이고 신랑 집이고 저의 공간은 또 따로 옆에 아파트",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "숨겨둔", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "곳이", "start_ms": 390, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있답니다", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 1100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여기는", "start_ms": 1250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "신랑", "start_ms": 1550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어쨌든", "start_ms": 1810, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나무꾼", "start_ms": 2120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "놀이터여갖고", "start_ms": 2380, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "신랑", "start_ms": 2900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "공간이고", "start_ms": 3160, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "신랑", "start_ms": 3520, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "집이고", "start_ms": 3780, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "저의", "start_ms": 4100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "공간은", "start_ms": 4310, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 4620, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 4730, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "옆에", "start_ms": 4940, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아파트", "start_ms": 5150, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "펭귄마을 유튜브 유튜브예요 펭귄마을 오니까 펭귄 같아요 펭귄처럼 생겼어요 너무 귀여워요 예 펭귄마을 차가 안 돌아다니니까 아이들이랑",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "펭귄마을", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유튜브", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "유튜브예요", "start_ms": 710, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "펭귄마을", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오니까", "start_ms": 1510, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "펭귄", "start_ms": 1770, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1980, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "펭귄처럼", "start_ms": 2240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생겼어요", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2910, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "귀여워요", "start_ms": 3120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 3480, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "펭귄마을", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "차가", "start_ms": 3960, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 4170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "돌아다니니까", "start_ms": 4280, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아이들이랑", "start_ms": 4790, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "발매 성공하고 양산이 시작되자 지금에 와서 후회를 하며 그 모든 걸 되돌려보려고 하고 있습니다 며칠 뒤",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "발매", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "성공하고", "start_ms": 230, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "양산이", "start_ms": 590, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시작되자", "start_ms": 850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지금에", "start_ms": 1160, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "와서", "start_ms": 1420, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "후회를", "start_ms": 1630, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하며", "start_ms": 1890, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 2210, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 2420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되돌려보려고", "start_ms": 2530, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 3040, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "뿐이 아니라 더 큰 흐름의 일부라고 분석했다 단국대학교의 천희판 교수는 많은 국가들이 양안 관계를 고려해 모호하거나 절충적인 표현을 사용하고 있다고 설명했다 그는 이러한 표현이 복잡한 양안 관계 속에서 균형을 맞춰",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "뿐이", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 240, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "흐름의", "start_ms": 770, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "일부라고", "start_ms": 1030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "분석했다", "start_ms": 1390, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "단국대학교의", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "천희판", "start_ms": 2310, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "교수는", "start_ms": 2620, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 2930, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "국가들이", "start_ms": 3140, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "양안", "start_ms": 3500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "관계를", "start_ms": 3710, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "고려해", "start_ms": 4020, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "모호하거나", "start_ms": 4280, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "절충적인", "start_ms": 4690, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 5100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사용하고", "start_ms": 5410, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 5770, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "설명했다", "start_ms": 6030, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "Does muscles like this growing? Look at everything up. Steve, Steve, oh. Chef, this is the onion.",
"speaker": "speaker_4",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Does", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "muscles", "start_ms": 90, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 150, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 190, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "growing?", "start_ms": 230, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "Look", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 340, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "everything", "start_ms": 370, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "up.", "start_ms": 430, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "Pleasantly surprised. Yeah. Married with the spring. Yeah. Very tough, chef. Very unique. Okay, I'm going to start with this now. Okay, just the onion. Okay.",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Pleasantly", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "surprised.", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "Yeah.", "start_ms": 250, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "나타날 것이라는 주장입니다 실제로 이란과의 전쟁 기간 동안 북한은 한국을 가장 적대적인 국가로 공인한다며 우리를 위협했고 일본과",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "나타날", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것이라는", "start_ms": 280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주장입니다", "start_ms": 590, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이란과의", "start_ms": 1310, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전쟁", "start_ms": 1720, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "기간", "start_ms": 1930, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 2140, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "북한은", "start_ms": 2350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 2660, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2920, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "적대적인", "start_ms": 3130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "국가로", "start_ms": 3540, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "공인한다며", "start_ms": 3800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리를", "start_ms": 4260, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "위협했고", "start_ms": 4520, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "인데요 땅값은 굳었지만은 웬걸 첫 삽 뜨기도 전에 백만 원은 뭐 개 눈 감추듯",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "인데요", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "땅값은", "start_ms": 260, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "굳었지만은", "start_ms": 620, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "웬걸", "start_ms": 1130, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 1390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "삽", "start_ms": 1500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뜨기도", "start_ms": 1610, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 1870, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "백만", "start_ms": 2080, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "원은", "start_ms": 2340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 2500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "개", "start_ms": 2610, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "눈", "start_ms": 2720, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "감추듯", "start_ms": 2830, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "을 지명 구매했고 그 뒤를 이어 이천이십사년 구월에는 이라크도 이십팔억 달러를 내놓고 한 번에 천궁 투 여덟 개 포대를 사들였습니다 한국의 방공 미사일은 이제 중동 국가들이 미국에 대한 극단적인 의존에서 벗어나는 데 가장 적합한 대안이 된 셈입니다 한국은",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "을", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "지명", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "구매했고", "start_ms": 310, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 670, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뒤를", "start_ms": 780, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이어", "start_ms": 990, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이천이십사년", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "구월에는", "start_ms": 1810, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이라크도", "start_ms": 2170, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이십팔억", "start_ms": 2530, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "달러를", "start_ms": 2890, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내놓고", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 3510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번에", "start_ms": 3620, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "천궁", "start_ms": 3830, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "투", "start_ms": 4090, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여덟", "start_ms": 4200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "개", "start_ms": 4410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "포대를", "start_ms": 4520, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사들였습니다", "start_ms": 4830, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 5440, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "방공", "start_ms": 5700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미사일은", "start_ms": 5960, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 6320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 6530, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "국가들이", "start_ms": 6790, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미국에", "start_ms": 7150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 7410, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "극단적인", "start_ms": 7620, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "의존에서", "start_ms": 8030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "벗어나는", "start_ms": 8390, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 8750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 8860, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "적합한", "start_ms": 9070, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "대안이", "start_ms": 9380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "된", "start_ms": 9640, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "셈입니다", "start_ms": 9750, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "트럼프가 이번 사안을 관세 후속 협상이나 핵 추진 잠수함 도입과 연",
"speaker": "speaker_12",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "트럼프가", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 390, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사안을", "start_ms": 600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "관세", "start_ms": 860, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "후속", "start_ms": 1120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "협상이나", "start_ms": 1380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "핵", "start_ms": 1740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "추진", "start_ms": 1850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잠수함", "start_ms": 2060, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "도입과", "start_ms": 2370, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "튀르키예 선박 두 척도 호르무즈 해협을 통과했는데요 CNN 튀르키예 등은 튀르키예 정부가 외교력 노력을 기울였고 해당 선박들이 이란을 오가는 물자 운송 중이었다는",
"speaker": "speaker_13",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "튀르키예", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선박", "start_ms": 380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 640, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "척도", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 960, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해협을", "start_ms": 1320, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "통과했는데요", "start_ms": 1630, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "CNN", "start_ms": 2140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "튀르키예", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "등은", "start_ms": 2810, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "튀르키예", "start_ms": 3020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정부가", "start_ms": 3380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "외교력", "start_ms": 3690, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "노력을", "start_ms": 4050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기울였고", "start_ms": 4360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해당", "start_ms": 4720, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "선박들이", "start_ms": 4980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이란을", "start_ms": 5340, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오가는", "start_ms": 5600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "물자", "start_ms": 5860, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "운송", "start_ms": 6120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "중이었다는", "start_ms": 6380, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "그거는 반대죠 어쨌든 너무 외진 곳에 갑자기 젊은 나이에 그냥 여기 와서 제가 농사짓는 것도 아니고 그냥 오는 건 그거는 아닌 거 같아갖고 신랑이 하고 싶어하는 거는 적극 도와주고 밀어주고 하지만 저 그 생활 공간은 따로 옆에",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그거는", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "반대죠", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어쨌든", "start_ms": 610, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 920, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "외진", "start_ms": 1130, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "곳에", "start_ms": 1390, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "갑자기", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "젊은", "start_ms": 1910, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "나이에", "start_ms": 2120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 2380, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 2590, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "와서", "start_ms": 2800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 3010, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "농사짓는", "start_ms": 3170, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 3530, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아니고", "start_ms": 3740, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 4000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "오는", "start_ms": 4210, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 4420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그거는", "start_ms": 4530, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아닌", "start_ms": 4790, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 5000, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아갖고", "start_ms": 5110, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "구독 설정도 부탁드리고요 원래 프랑스에서는 천구백육십년대 후반까지 교복을 입는 학교가 꽤 있었다고 합니다 많은 학교가 인",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "구독", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "설정도", "start_ms": 240, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "부탁드리고요", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "원래", "start_ms": 960, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "프랑스에서는", "start_ms": 1170, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "천구백육십년대", "start_ms": 1630, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "후반까지", "start_ms": 2240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "교복을", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "입는", "start_ms": 2910, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "학교가", "start_ms": 3120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "꽤", "start_ms": 3430, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있었다고", "start_ms": 3540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 3900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 4160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "학교가", "start_ms": 4370, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "이후 영상은 기네스북 구독자 자분들의 편의를 위해 한국어로 다시 더빙했음을 알려드립니다 한국 군수사",
"speaker": "speaker_15",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이후", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "영상은", "start_ms": 230, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기네스북", "start_ms": 540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "구독자", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자분들의", "start_ms": 1210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "편의를", "start_ms": 1570, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 1830, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한국어로", "start_ms": 2040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 2400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더빙했음을", "start_ms": 2610, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "알려드립니다", "start_ms": 3020, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "What's happening here? If you get this now, the chances of your success is exponentially higher. It really",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "What's", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "happening", "start_ms": 90, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "here?", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "People go to the Jun restaurant to get this. And then also have Makgeolli. Do you remember? Yes, and they'll have it together. This was the first time I made this.",
"speaker": "speaker_16",
"confidence": 0.93,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "People", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 110, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 160, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "생초보인데 이 정도 실수는 애교지 뭐 그래서 선을 다시 긋고 다행히 더 보강하는 게 아니었기 때문에 보고",
"speaker": "speaker_17",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "생초보인데", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 570, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "실수는", "start_ms": 780, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "애교지", "start_ms": 1090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 1350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1460, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "선을", "start_ms": 1720, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 1930, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "긋고", "start_ms": 2140, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다행히", "start_ms": 2350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2660, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보강하는", "start_ms": 2770, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 3130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니었기", "start_ms": 3240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 3600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 3910, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "어떻게 얘기하는 거구나 맞아요 한복 입고 전 세계를 다니고 있는데 혹시 외국에서 오셨어요 미국",
"speaker": "speaker_18",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떻게", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "얘기하는", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거구나", "start_ms": 610, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맞아요", "start_ms": 870, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한복", "start_ms": 1130, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "입고", "start_ms": 1340, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 1550, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "세계를", "start_ms": 1660, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다니고", "start_ms": 1970, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 2280, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "혹시", "start_ms": 2540, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "외국에서", "start_ms": 2750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오셨어요", "start_ms": 3110, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 3470, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "So you've really mastered this sauce. This sauce. Yeah. Is it like a mustard-y",
"speaker": "speaker_4",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "So", "start_ms": 50, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "you've", "start_ms": 90, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "추운 겨울에 따뜻한 대피처가 되어준다는 데요 음 트리하우스도 아",
"speaker": "speaker_19",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "추운", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "겨울에", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "따뜻한", "start_ms": 510, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "대피처가", "start_ms": 770, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되어준다는", "start_ms": 1130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "데요", "start_ms": 1540, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 1700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "트리하우스도", "start_ms": 1810, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "눈치 챘어 어 이게 뭐야 진짜 여러분 고생 많다",
"speaker": "speaker_20",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "눈치", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "챘어", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 580, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뭐야", "start_ms": 740, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 1160, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 1470, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "많다", "start_ms": 1680, "dur_ms": 250, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 07:14:22*
