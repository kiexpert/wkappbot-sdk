# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=36536  time=08:10:23  uptime=8104s  mem=570MB  system_processes=408"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_080753.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 135-137-751-756-574-756-756-751-715-175-175-751-157-175-156-175 136-136-173-176-175-175-715-175-715-715-175-157-175-157-571-157-751-175-175-175 132-137-173-175-176-173-175-176-1_W.mp3)
  Segment 2: 1576ms ~ 12016ms (file: 312-731-713-310-751-267-263-627-627-130-312-762-263-325-136-361-132-261-675-102-312-130-260-130-107-126-231-275-310-267-162-106-263-260-236-756-361-163-176-103-236-756-675-751-1_W.mp3)
  Segment 3: 13016ms ~ 13592ms (file: 315-231-612-163-675-675-756-673-576-576-310-713-120-321-165-517-576-156-156-351-517-537-576-156-576-576-567-576-576-574-576-576-567-652-675-756-753-157-536 213-726-625-567-567-5_W.mp3)
  Segment 4: 14592ms ~ 21972ms (file: 675-230-725-217-132-103-103-132-571-512-102-310-310-130-132-310-103-135-130-132-103-312-312-301-573-754-137-312-130-103-275-120-103-132-301-513-312-120-572-150-517-207-765-265-3_W.mp3)
  Segment 5: 22972ms ~ 28804ms (file: 627-206-236-607-627-625-627-362-263-236-236-320-326-765-725-672-236-263-163-260-516-362-327-576-576-103-163-231-623-236-263-312-321-312-132-236-236-265-236-260-230-102-123-721-1_W.mp3)
  Segment 6: 29804ms ~ 30380ms (file: 170-135-371-173-173-102-103-175-107-103-370-372-175-715-317 157-157-153-175-170-751-517-175-170-170-173-170-317-715-317-173-173-175-175-325-731 571-571-571-571-751-517-157-157-7_V.mp3)
  Segment 7: 31380ms ~ 37788ms (file: 217-205-231-572-572-573-215-213-215-750-752-207-217-210-217-726-736-621-512-126-107-310-320-127-127-752-257-170-206-210-201-217-213-132-137-173-715-513-130-176-150-105-120-217-2_W.mp3)
  Segment 8: 38788ms ~ 39364ms (file: 217-623-273-236-526-267-236-621-362-267-306_V.mp3)
  Segment 9: 40364ms ~ 47924ms (file: 230-206-210-750-207-310-301-302-260-123-176-102-176-103-317-310-230-203-203-203-310-573-257-756-310-127-572-754-312-103-136-103-310-201-103-761-106-730-201-230-103-302-203-230-7_V.mp3)
  Segment 10: 48924ms ~ 49500ms (file: 672-507-567-765-275-537-153-570-302-567-756-372-732-673-672-327-627-130-547-503-531-357-530-537-375-751-153-165-365-536-367-361-310-321-315-361_W.mp3)
  Segment 11: 50500ms ~ 51076ms (file: 150-105-130-170-107-715 571-517-527-571-150-517-173-153-150-150-517-517-517-517-571-517-517-157-517-753-517-157-715-570-105-153-501-153_W.mp3)
  Segment 12: 52076ms ~ 59960ms (file: 267-173-762-276-762-726-756-267-321-132-632-107-103-216-267-276-273-321-231-206-267-132-761-175-627-216-326-302-302-103-312-326-206-736-276-237-320-106-167-103-160-236-321-321-2_W.mp3)
  Segment 13: 60960ms ~ 65928ms (file: 763-763-716-137-317-376-327-312-673-367-632-630-635-326-362-536-236-652-160-657-567-675-765-267-362-726-673-236-267-362-573-237-327-275-327-573-276-237-267-756-726-310-723-231-2_W.mp3)
  Segment 14: 66928ms ~ 72508ms (file: 271-601-620-654-645-267-264-260-107-106-162-216-213-213-132-217-217-132-127-721-170-672-572-726-103-120-310-137-126-627-107-321-725-175-752-130-301-637-672-672-672-672-672-723-2_W.mp3)
  Segment 15: 73508ms ~ 74084ms (file: 751-765-107-710-710-175-715-710-715-170-176-170-175-715-710-716-716-710-716-735-713-736-731-375-176-715-751 170-706-107-705-175-170-107-130-137-170-170-715-715-173-175-173-713-7_V.mp3)
  Segment 16: 75084ms ~ 87144ms (file: 102-130-320-320-230-203-260-230-301-260-260-206-203-310-230-321-230-302-103-103-312-302-103-132-301-241-132-102-320-310-123-576-125-261-123-612-175-517-217-210-517-230-130-325-2_W_stt_the_planning_committee_hasn't_enabled_me.mp3)
  Segment 17: 88144ms ~ 88720ms (file: 672 731-173-173-137-371-571-513-153-132-317-537-531-531-316-301-370-736-612-765-567-326-256-317-670-126-756-726-132-316-216-213-213-570-621-261-751-276-173-761-263-123-312-573-3_W.mp3)
  Segment 18: 89720ms ~ 97460ms (file: 231-216-216-123-126-132-236-132-321-526-231-267-726-216-132-231-732-235-213-230-231-203-267-231-312-132-213-572-517-256-263-216-231-126-751-106-102-517-276-263-236-120-126-762-2_W.mp3)
  Segment 19: 98460ms ~ 99036ms (file: 310-317-321-327-327-312-327-372 735-370 250-357-157-157-153-153-631_W.mp3)
  Segment 20: 100036ms ~ 108352ms (file: 312-312-326-362-356-136-756-106-103-310-203-125-301-236-321-321-132-102-326-261-126-126-123-213-210-213-236-103-213-123-172-621-256-574-602-621-672-126-572-572-527-261-321-762-7_W.mp3)
  Segment 21: 109352ms ~ 121628ms (file: 316-302-623-326-302-357-327-302-315-305-301-312-312-321-613-325-132-102-105-236-301-763-326-102-201-260-132-102-326-302-236-213-102-572-320-103-103-230-236-263-236-527-132-301-7_W.mp3)
  Segment 22: 122628ms ~ 123204ms (file: 710-710-170-173-137-173-715-172-173-172 751-175-517-517-175-571-715-715-751-731-751-175-715-751-715 572-571-517-571-571-574-574-571-571-570_W.mp3)
  Segment 23: 124204ms ~ 124780ms (file: 756-752-572-731-752-576-576-173-765-731-705-736-713-705-735-752-756-756-753 574-576-576-576-571-571-571-576-573-574-571-517_V.mp3)
  Segment 24: 125780ms ~ 126356ms (file: 756-326-502-135-507-126-702-326-726-625-362-705-756-507-375-372-352-736-263-627-236-263-316-763 371_V.mp3)
  Segment 25: 127356ms ~ 135204ms (file: 725-236-260-231-275-756-754-573-263-623-236-201-756-236-265-675-267-267-267-627-265-763-763-376-236-123-260-627-120-123-216-762-627-627-236-762-607-120-627-762-236-320-572-351-2_W_stt_the_increased_spending_2.5%_of_gdp_numbe.mp3)
  Segment 26: 136204ms ~ 143980ms (file: 321-103-130-236-103-367-320-205-705-627-560-352-321-674-760-671-271-216-231-203-276-276-103-357-273-126-376-103-231-620-106-230-302-103-126-157-130-216-327-620-567-563-371-267-1_W.mp3)
  Segment 27: 144980ms ~ 145556ms (file: 613-613-163-632-136-316-106-130-560-105-657-157-561-162-162-653-657-376-103-130-516-315-137-631-536-537-327-136-167-321-136-615-173-316-362-517-531-563-573-563-315-752-576-547-5_W.mp3)
  Segment 28: 146556ms ~ 147132ms (file: 567-567-567-576-576-576-576-567-562-657-576-567-567-657-567-652-576-756-562-576-563-652-576-672-567-652-752-527-562-567-567-567-526-567-752-576-562-576-576-756-567-567-571-567-5_V.mp3)
  Segment 29: 148132ms ~ 148708ms (file: 130-150-715-751-215-716-157-310-130-317-176-173 753-735-317-652-630-175-175-715-165-126-237-163-137-206-167-137-205-261-705-173-570-175-172-170-173-675-571-153-571-671-275-762-7_W.mp3)
  Segment 30: 149708ms ~ 150284ms (file: 102-103-103 135-130-103-317-756-576-657-756-751-127-167-176-736-367-317-137-173-765-672-672-672-712-136-375-357-312-132-163-136-176-137-137-132-137-137-176-173-361-135-312-153-7_W.mp3)
  Segment 31: 151284ms ~ 157332ms (file: 502-125-321-260-602-205-125-236-320-123-703-735-123-316-573-206-751-126-732-132-236-231-236-570-607-103-125-326-207-206-231-627-576-675-750-750-275-763-103-362-231-123-130-672-2_W.mp3)
  Segment 32: 158332ms ~ 167368ms (file: 132-213-216-123-261-213-213-163-130-103-310-307-126-372-327-132-123-275-275-527-256-123-257-256-267-251-267-216-216-213-103-675-672-265-270-123-572-231-137-213-126-213-103-132-1_W.mp3)
  Segment 33: 168368ms ~ 168944ms (file: 571-751-571-571-571-517-570-571-750-571 751-571-751-571-576-571-571-576-571-571-571-751-573-715-571-571 573-573-751-572-570-571-570-157-751-572-517-576-571 576-574-571-571-573-5_W.mp3)
  Segment 34: 169944ms ~ 170520ms (file: 753-753-375-357-573-753-731-715-751-756-750-750 751-754-571-570-570-750-570-750-570-570-570-705-750-750-513-571-571-571-517-571 576-576-570-751-570-751-517-571-571-517-517-571-7_V.mp3)
  Segment 35: 171520ms ~ 172096ms (file: 527-267-261-706-263-267-206-236-765-301-316-613-362-621-163-261-527-315-736-167-502-573-750-136-237-267-263-265-236-276-523-137-276-576-521-623-576-623-572-765-513-325-236-317-2_V.mp3)
  Segment 36: 173096ms ~ 183392ms (file: 267-236-631-167-715-316-672-162-163-263-237-231-132-317-273-170-267-236-273-236-326-310-751-761-312-231-321-260-263-326-103-123-231-231-201-102-123-130-103-137-621-103-263-230-1_W.mp3)
  Segment 37: 184392ms ~ 191700ms (file: 765-675-276-267-107-756-276-372-173-230-756-107-720-107-230-276-276-275-137-103-163-176-106-130-210-271-237-267-201-230-217-120-320-231-210-210-267-137-127-137-574-527-107-750-7_W.mp3)
  Segment 38: 192700ms ~ 199576ms (file: 173-127-231-123-102-253-321-217-270-120-216-103-137-102-102-231-301-321-671-762-716-203-321-307-713-371-307-765-263-123-123-136-102-260-236-231-136-326-236-362-237-236-175-126-1_V.mp3)
  Segment 39: 200576ms ~ 201152ms (file: 361 316-326-317-327-317-312-317-362-361-312-312-317-316-316-312-657-365-506-652-635-365-365-365-653-635-365-362-325-367-653-356-536-321 762-623-321-235-302-657-631 267-567-562-1_W.mp3)
  Segment 40: 202152ms ~ 202728ms (file: 670-675-675-675-675-670-765-765-675 320-625-625-625-627-625-625-620-620-657-652-652-173-765-765-756-756-765-765-675-576-567-576-572-576-751-576-517-375-175-735-137-135-150-105-1_W.mp3)
  Segment 41: 203728ms ~ 211792ms (file: 216-237-267-572-231-230-127-765-132-263-672-267-267-176-160-267-167-762-120-627-263-762-627-176-267-267-267-276-756-527-672-231-137-276-235-276-765-273-326-267-672-617-751-267-1_W.mp3)
  Segment 42: 212792ms ~ 222692ms (file: 627-751-201-310-612-206-127-762-105-510-105-510-153-170-132-132-143-132-103-570-570-517-150-102-123-132-143-103-104-236-301-301-216-302-126-143-123-135-134-132-167-132-143-132-1_V.mp3)
  Segment 43: 223692ms ~ 224268ms (file: 136-316-167-163-106-163-176-176-571-172-372-172-132-153-132-167-617-613-167-167-163-160-163-167-167-765-675-765-763-132-163-156-135-135-317-137-137-136-136-136-135-176-175-135-1_W.mp3)
  Segment 44: 225268ms ~ 225844ms (file: 617-726-237-203-632-765-756-756-756-756-752-756-735-735-723-675-765-761-136-637-316-631-163-167-136-163-136-137-137-173-756-756-765-756-756-756-756-756-756-176-137-137-167-136-1_W.mp3)
  Segment 45: 226844ms ~ 237788ms (file: 321-265-230-230-231-231-263-216-210-102-132-213-132-106-106-130-170-316-310-103-102-120-216-203-210-230-203-231-231-320-130-231-130-123-413-132-310-231-230-103-310-267-321-310-1_V.mp3)
  Segment 46: 238788ms ~ 245628ms (file: 301-312-312-302-312-310-710-130-130-130-312-312-316-320-203-213-135-327-263-230-260-310-631-230-213-261-260-173-103-320-230-103-102-310-320-130-312-763-732-263-326-731-132-137-3_V.mp3)
  Segment 47: 246628ms ~ 247204ms (file: 631-567-765-657-561-657-657-623-613-756-635-756-763-761-760-671-671-570-632-167-137-316-173-312-756-257-560-713-137-125-570-231-265-621-321-751-136-123-672-706-275-132-263-562-1_W.mp3)
  Segment 48: 248204ms ~ 258464ms (file: 203-213-137-132-176-132-143-103-301-103-310-312-715-706-276-207-320-216-612-623-362-102-201-201-103-102-103-301-301-103-132-137-301-130-301-310-361-301-213-263-620-213-625-352-3_V.mp3)
  Segment 49: 259464ms ~ 260040ms (file: 672-267-263-507-327-572-321-527-312-367-576-162-276-372-157-760-275-316-315-137-137-173-735-375-753-713-617-753-315-316-653-657-635-136-315-361-361-163-367-236-367-673-637-635-6_W.mp3)
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
> **Time**: 2026-04-06 08:12:39
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "그런 제조기업이 가져가야 되는 거죠. 그리고 실제 산업 현장에 적용하고 양산 단계로 끌어올릴 수 있는 역량을 보유해야 되는데 휴머노이드의",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그런", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "제조기업이", "start_ms": 350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가져가야", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 1500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실제", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "현장에", "start_ms": 3450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "적용하고", "start_ms": 3950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "양산", "start_ms": 4550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단계로", "start_ms": 4950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "끌어올릴", "start_ms": 5450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 6000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 6200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "역량을", "start_ms": 6500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보유해야", "start_ms": 7000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "되는데", "start_ms": 7600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "휴머노이드의", "start_ms": 8100, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "진정한 목적은 사람을 대신한다든지 사람이 필요한 곳에",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "진정한", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "목적은", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사람을", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대신한다든지", "start_ms": 1450, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "필요한", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "곳에", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "채팅 GPT 이후 가장 큰 충격이라고",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "채팅", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "GPT", "start_ms": 500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이후", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 1350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 1750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "충격이라고", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "야 씨 나 혼자 왔을 때는 진짜 반응이 생난리였는데 여기 반응이 좋아 이거 맛있네",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "야", "start_ms": 80, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "씨", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "나", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "혼자", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "왔을", "start_ms": 1000, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "때는", "start_ms": 1300, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "진짜", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "반응이", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "생난리였는데", "start_ms": 2450, "dur_ms": 750, "phase_lr_deg": -5.0},
{"word": "여기", "start_ms": 3300, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "반응이", "start_ms": 3650, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "좋아", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "이거", "start_ms": 4500, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "맛있네", "start_ms": 4850, "dur_ms": 450, "phase_lr_deg": -5.0}
]
},
{
"segment": 6,
"transcript": "뭐예요?",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "뭐예요?", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "유리한 공간이라는 당연한 진리를 부부를 보며 다시금 깨닫게 되네요. 남은 1년도 파이팅입니다",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "유리한", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "공간이라는", "start_ms": 550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "당연한", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "진리를", "start_ms": 1750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부부를", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보며", "start_ms": 2650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다시금", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "깨닫게", "start_ms": 3550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되네요.", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "남은", "start_ms": 4500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "1년도", "start_ms": 4900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "파이팅입니다", "start_ms": 5400, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "할 수 있다고 경고했다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "할", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "경고했다", "start_ms": 600, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "대만 외교부는 또한 덴마크에 대해서도 비판을 제기했다. 2024년부터 덴마크의 거주 허가증에서 대만인을 중국으로 표기하고 있기 때문이다. 대만은 이 문제가 시정되기를",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "대만", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "외교부는", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "또한", "start_ms": 950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "덴마크에", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대해서도", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비판을", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제기했다.", "start_ms": 2750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "2024년부터", "start_ms": 3350, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "덴마크의", "start_ms": 4200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거주", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "허가증에서", "start_ms": 5050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "대만인을", "start_ms": 5700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중국으로", "start_ms": 6200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "표기하고", "start_ms": 6700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 7200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때문이다.", "start_ms": 7550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대만은", "start_ms": 8150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 8600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "문제가", "start_ms": 8800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시정되기를", "start_ms": 9250, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "주식 주식 하잖아요. 어메이징 그리고 밖에 껍질이가 고소하다 고소한데",
"speaker": "speaker_6",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "주식", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주식", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하잖아요.", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어메이징", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "밖에", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "껍질이가", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "고소하다", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고소한데", "start_ms": 3950, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "야 너무 맛있다",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "야", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맛있다", "start_ms": 500, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "그거는 차이가 안 느껴지면 약간 차이는 있어도 근데 이 용량의 차이 48기가 24기가의 차이는 확실히 나는 게 여러 번 얘기를 많이",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그거는", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "차이가", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "느껴지면", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "약간", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "차이는", "start_ms": 1950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있어도", "start_ms": 2350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 2800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "용량의", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "차이", "start_ms": 3750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "48기가", "start_ms": 4100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "24기가의", "start_ms": 4750, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "차이는", "start_ms": 5500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "확실히", "start_ms": 5950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 6450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 6750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 6950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 7300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "얘기를", "start_ms": 7500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 7900, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "인데요. 땅값은 굳어지지만은 웬걸 첫 삽 뜨기도 전에 100만 원은 뭐 개 눈 감추듯",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "인데요.", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "땅값은", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "굳어지지만은", "start_ms": 950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "웬걸", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 2100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "삽", "start_ms": 2300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "뜨기도", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 2900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "100만", "start_ms": 3250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "원은", "start_ms": 3750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 4050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "개", "start_ms": 4250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "눈", "start_ms": 4450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "감추듯", "start_ms": 4650, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "But it really hasn't enabled me to do I do better work in less time I do more work in less time",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "But", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "hasn't", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "enabled", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "me", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2000, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 2150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 2350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 2500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "better", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "work", "start_ms": 3100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 3450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "less", "start_ms": 3650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 3950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 4350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 4500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 4700, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "work", "start_ms": 5000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 5350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "less", "start_ms": 5550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "time", "start_ms": 5850, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "하루 30분 일하는데 한 달에 보도에 따르면 이스라엘은 요격 미사일 재고 압박을 받으면서 최고 성능 미사일의 사용을 줄이고 있는 것으로 나타났습니다. 실제로 디모나와 아라드 지역 공격 당시 이스라엘",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "하루", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "30분", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일하는데", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "달에", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보도에", "start_ms": 2000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "따르면", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이스라엘은", "start_ms": 2850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "요격", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 3850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "재고", "start_ms": 4300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "압박을", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "받으면서", "start_ms": 5100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "최고", "start_ms": 5650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "성능", "start_ms": 6000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "미사일의", "start_ms": 6350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "사용을", "start_ms": 6950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "줄이고", "start_ms": 7400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 7850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 8150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나타났습니다.", "start_ms": 8550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 9350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "디모나와", "start_ms": 9850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "아라드", "start_ms": 10450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지역", "start_ms": 10900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "공격", "start_ms": 11250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "당시", "start_ms": 11600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이스라엘", "start_ms": 12000, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "어떠셨어요? 나는 항상 오는 곳이지만 어떤 사람하고 오느냐에 따라서 또 달라지는 것 같아요. 그래서 마야하고 같이 와봤는데 또 다른 거야. 내가 아는 광주지만 그래서 통맥 축제나 나중에 광주에 이렇게 축제로 오실 때 사람들이 같이 오시는 분들하고 같이 즐기면 광주",
"speaker": "speaker_11",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "어떠셨어요?", "start_ms": 90, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오는", "start_ms": 1500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "곳이지만", "start_ms": 1800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 2350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사람하고", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오느냐에", "start_ms": 3200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따라서", "start_ms": 3700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 4150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "달라지는", "start_ms": 4350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 4850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 5050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 5450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "마야하고", "start_ms": 5850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 6350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와봤는데", "start_ms": 6700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 7250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 7450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거야.", "start_ms": 7800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 8100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아는", "start_ms": 8400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "광주지만", "start_ms": 8700, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "increase spending to 3.5% of GDP and purchase $25 billion of US military equipment by 2030 is important",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "increase", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "spending", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "3.5%", "start_ms": 1150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 1900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "GDP", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 2550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "purchase", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "$25", "start_ms": 3350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "billion", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 4450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "US", "start_ms": 4650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "military", "start_ms": 4950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "equipment", "start_ms": 5500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "by", "start_ms": 6100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "2030", "start_ms": 6350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 7000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "important", "start_ms": 7200, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "좋아 가야지 어디서 안녕하세요. 아 그래요? 좋아하세요? 예뻐졌는데",
"speaker": "speaker_12",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "가야지", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "어디서", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "안녕하세요.", "start_ms": 1450, "dur_ms": 700, "phase_lr_deg": 5.0},
{"word": "아", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "그래요?", "start_ms": 2400, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "좋아하세요?", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "예뻐졌는데", "start_ms": 3600, "dur_ms": 700, "phase_lr_deg": -5.0}
]
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
"transcript": "기업이 미국 기업을 인수하거나 지분 투자할 때 국가 안보에 위협이 되는지 심사하는 역할을 합니다. 참여 기관은 재무부 국방부",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "기업이", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기업을", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "인수하거나", "start_ms": 1250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "지분", "start_ms": 1950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "투자할", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "국가", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "안보에", "start_ms": 3350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위협이", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되는지", "start_ms": 4250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "심사하는", "start_ms": 4700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "역할을", "start_ms": 5250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 5700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "참여", "start_ms": 6200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기관은", "start_ms": 6550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "재무부", "start_ms": 7000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "국방부", "start_ms": 7450, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 26,
"transcript": "마른 지기 중년의 놀이터라는 이 정도는 돼야지요",
"speaker": "speaker_14",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "마른", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지기", "start_ms": 400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중년의", "start_ms": 750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "놀이터라는", "start_ms": 1250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정도는", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "돼야지요", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "일부 선박이 호르무즈 해협을 빠져나온 가운데 적대국만 제외 방침도 거듭 밝혔습니다",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "일부", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선박이", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "해협을", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "빠져나온", "start_ms": 2050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가운데", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "적대국만", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제외", "start_ms": 3750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방침도", "start_ms": 4150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거듭", "start_ms": 4650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "밝혔습니다", "start_ms": 5100, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(speech in Arabic)",
"speaker": "speaker_16",
"confidence": 0.85,
"language": "ar",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "أن", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "الملاحة", "start_ms": 250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "بشكل", "start_ms": 750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "مستمر", "start_ms": 1150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "مستثنى", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "من", "start_ms": 2100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "أي", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "비용이 들어가는 정화조 매트에서는 온 가족이 달려들어 하루 만에 끝냈고요. 거푸집은 10년 쓴다 생각하고 목순",
"speaker": "speaker_17",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "비용이", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들어가는", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정화조", "start_ms": 1100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매트에서는", "start_ms": 1550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "온", "start_ms": 2250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "가족이", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "달려들어", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만에", "start_ms": 4000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "끝냈고요.", "start_ms": 4350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "거푸집은", "start_ms": 5000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "10년", "start_ms": 5600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "쓴다", "start_ms": 6050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "생각하고", "start_ms": 6400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "목순", "start_ms": 7000, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "병원 갔어? 응 엄마 병원 가. 엄마 여기 병원 어디야? 엄마 이거 뭐야?",
"speaker": "speaker_18",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "병원", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "갔어?", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "응", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "엄마", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "병원", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "가.", "start_ms": 1850, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "엄마", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "여기", "start_ms": 2550, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "병원", "start_ms": 2900, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "어디야?", "start_ms": 3300, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "엄마", "start_ms": 3850, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "이거", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "뭐야?", "start_ms": 4550, "dur_ms": 400, "phase_lr_deg": 5.0}
]
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
"transcript": "서프라이즈 어땠어? 나는 느낌이 드는 것입니다. 그러니 우리에게 손흥민 선수의 멋진 경기들을 지켜볼 수 있는 시간이 얼마나 더 남았을지는 알 수 없지만 우리가 그의 축구 경력 시",
"speaker": "speaker_19",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "서프라이즈", "start_ms": 70, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어땠어?", "start_ms": 750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "느낌이", "start_ms": 1700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "드는", "start_ms": 2150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것입니다.", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그러니", "start_ms": 3200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리에게", "start_ms": 3700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "손흥민", "start_ms": 4250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "선수의", "start_ms": 4750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "멋진", "start_ms": 5200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "경기들을", "start_ms": 5600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지켜볼", "start_ms": 6200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 6700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 6900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 7200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 7700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 8200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "남았을지는", "start_ms": 8400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "알", "start_ms": 9100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 9300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없지만", "start_ms": 9500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 10000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그의", "start_ms": 10450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "축구", "start_ms": 10800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "경력", "start_ms": 11200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시", "start_ms": 11600, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "To pursue their objectives successfully but building systems that have as their objective the goal to help human beings in whatever it is that humans want the future to be like",
"speaker": "speaker_20",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "To", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "pursue", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "their", "start_ms": 600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "objectives", "start_ms": 900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "successfully", "start_ms": 1650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "but", "start_ms": 2500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "building", "start_ms": 2800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "systems", "start_ms": 3350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 3900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "as", "start_ms": 4550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "their", "start_ms": 4800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "objective", "start_ms": 5100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 5750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "goal", "start_ms": 5950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 6350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "help", "start_ms": 6550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "human", "start_ms": 6900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "beings", "start_ms": 7300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 7750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "whatever", "start_ms": 7950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 8450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 8650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 8850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "humans", "start_ms": 9100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "want", "start_ms": 9600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 9950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "future", "start_ms": 10150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 10650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 10850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 11100, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "게 됐는데 가만히 보니까 지금 이거 온돌 안 하셨나 봐요 방학 때 난방을 바닥까지 난방 공사를 하게 되면 조금 그 공사 비용이나 그런 것들도 많이 좀 차이가 나고 질적인 것도 있기 때문에 우리의 목적에 맞게",
"speaker": "speaker_21",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "게", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "됐는데", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가만히", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "온돌", "start_ms": 2300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하셨나", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 3350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "방학", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 4100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "난방을", "start_ms": 4300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바닥까지", "start_ms": 4750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "난방", "start_ms": 5300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공사를", "start_ms": 5700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하게", "start_ms": 6200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 6550, "dur_ms": 300, "phase_lr_deg": 0.0}
]
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
"transcript": "더 잡아 잡아 어유 너무 매워 매워 아니 매운 것만 총인 어유 두 개만 있으면 러시아까지 가시겠네",
"speaker": "speaker_6",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "더", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잡아", "start_ms": 300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잡아", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어유", "start_ms": 1000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매워", "start_ms": 1800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매워", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것만", "start_ms": 3350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "총인", "start_ms": 3750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어유", "start_ms": 4200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 4600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "개만", "start_ms": 4800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 5200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "러시아까지", "start_ms": 5700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가시겠네", "start_ms": 6450, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(speech in Japanese)",
"speaker": "speaker_22",
"confidence": 0.96,
"language": "ja",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "ありがとうございます。", "start_ms": 50, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "お疲れ様でした。", "start_ms": 1250, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "おやすみなさい。", "start_ms": 2350, "dur_ms": 900, "phase_lr_deg": 0.0}
]
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:12:39*
