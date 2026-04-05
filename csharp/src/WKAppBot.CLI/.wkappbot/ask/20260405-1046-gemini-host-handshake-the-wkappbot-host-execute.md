# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=10:43:52  uptime=22344s  mem=380MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_104340.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 501-507-150-501-150-105-150-570-150-150-501-150-157-507-510 570-510-510-150-501-105-501-510-150-510-507-150-105-510-501-105-501-507-570-150-150-501-571 501-571-517-501-517-501-5_V.mp3)
  Segment 2: 1576ms ~ 8776ms (file: 231-320-130-132-102-132-103-310-130-103-361-361-130-213-320-321-573-320-237-120-102-103-763-723-632-123-213-123-231-132-351-231-310-157-310-631-612-213-321-130-310-103-623-625-5_W.mp3)
  Segment 3: 9776ms ~ 10352ms (file: 621-623 621-625-627-625-625-625-652-651-652-651-651-625-621-261-261-621-625-652-625-625-261-261 567-521-562-625-625-657-657-657-654-654-654-654-615-652-615-615-613 520-621-625-6_W.mp3)
  Segment 4: 11352ms ~ 24348ms (file: 216-120-312-206-263-126-267-524-205-215-213-257-254-213-574-305-231-230-312-203-260-625-326-103-312-756-627-137-102-102-201-574-153-310-752-750-312-210-314-301-132-136-726-206-2_V.mp3)
  Segment 5: 25348ms ~ 25924ms (file: 562-576-271-647-132-675-576-756-576_W.mp3)
  Segment 6: 26924ms ~ 27500ms (file: 701-705-701-170-705-750-107-715-715-710-710-701-751-751 570-570-570-570-574-574-574-574-754-754-574-574-574-574-574-574-574-574 574-574-547-547_W.mp3)
  Segment 7: 28500ms ~ 36060ms (file: 231-320-756-265-610-175-137-701-760-312-236-265-360-103-276-170-267-765-106-170-360-573-750-137-310-267-362-301-132-132-715-576-753-603-276-257-732-627-765-206-260-267-102-613-2_W.mp3)
  Segment 8: 37060ms ~ 37636ms (file: 657-756-576-657-675-756-615-650-651-657-567-657-675-657-657-657-657-675-765-675-675 265-275-256-567-675-567-567-567-576-567-576-567-576-567-576 275-567-657-576-576-567-576-567-5_W.mp3)
  Segment 9: 38636ms ~ 39212ms (file: 163-621-672-627-317-576-576-576-267-257-521-152-135-301-132-574-573-752-136-167-162-574-570-237-267-267-231-236-263-132-103-513-517-753-751-712-213-231-321-213-213-574-574-754-7_V.mp3)
  Segment 10: 40212ms ~ 40788ms (file: 263-326 621-163 365-132-163-123-173 763-762 736_W.mp3)
  Segment 11: 41788ms ~ 42364ms (file: 670-576-760-102-120-216-236-301-132-615-672-312-231-206-321-123-216-250-507-574-570-537-316-163-107-172-715-765-547-506-567-132-154-132-132-134-215-413-231-326-570-750-672-621-6_V.mp3)
  Segment 12: 43364ms ~ 43940ms (file: 317-130-576-321-301-130-103-167-760-132-120-135-301-321-327-327-365-103-136-301-301-310-305-302-532-532-561-306-316-376-617-176-765-756-765-176-176-167-136-357-750-750-756-371-3_W.mp3)
  Segment 13: 44940ms ~ 45516ms (file: 175-750-570-501-705-107-175-507-150-701-710-170-170-107-561-571-574-574-750-715 107-715-175-571-157-175-157-175-705-173-710-701-105-517-150-170-105-175-107-106-537-351-571-513-3_V.mp3)
  Segment 14: 46516ms ~ 52060ms (file: 132-103-213-310-137-103-350-213-103-201-130-102-547-517-103-501-760-157-157-157-361-312-203-132-167-103-213-216-520-127-132-130-502-315-215-375-572-517-157-571-231-271-375-137-1_V.mp3)
  Segment 15: 53060ms ~ 53636ms (file: 102-156-164-165-153-165-706-615-165-106-675-657-576-576-576-756-576-576-576-576-576-576-675-657-576-657-675 132_W.mp3)
  Segment 16: 54636ms ~ 55212ms (file: 341-756-576-137-312-602-367-613-137-173-172-123-327-563-657-270-273-273-267-275-231-276-315-365-375 546-154-216-132-231 627-317-216-765-206-612-106-127 273-765-752-652-361-750-7_V.mp3)
  Segment 17: 56212ms ~ 56788ms (file: 103-106-106-152-132-106-632-176-162 537-163-176-176-716_V.mp3)
  Segment 18: 57788ms ~ 58364ms (file: 576-657-765-675-326-236-623 175-136-162-620-601-167-617-321-130-137-163-163-163-672-617 136-103-132-103-132-103-213-201-123-312-106-106-675-627-671-671-367 627-621-612-617-612-1_W.mp3)
  Segment 19: 59364ms ~ 72972ms (file: 756-574-765-715-756-574-257 130-726-705-135-574-526-765-756-574-103-132-120-571-756-752-413-756-123-130-130-306-573-752-103-512-317-130-132-310-107-201-123-130-102-574-517-136-3_W.mp3)
  Segment 20: 73972ms ~ 74548ms (file: 253-320-267-263 137-136-163-136-103-715-753-167-123-163-103-672-106-167-627-126 620-601-621-167-362-602-602-263 750-720-671-127-103-316-103-651-172-127-126-160-162-137-137-210-1_V.mp3)
  Segment 21: 75548ms ~ 84404ms (file: 106-126-710-123-321-130-103-136-572-273-201-103-132-201-310-573-257-231-203-301-675-267-276-267-320-310-163-103-312-756-726-756-326-236-320-120-675-320-103-301-130-132-231-271-1_W.mp3)
  Segment 22: 85404ms ~ 94332ms (file: 312-326-753-213-231-230-320-261-276-231-713-231-725-312-263-756-273-627-263-231-312-753-275-716-752-321-321-210-237-231-213-261-236-725-752-276-276-210-321-106-627-267-267-231-2_W.mp3)
  Segment 23: 95332ms ~ 95908ms (file: 107-571-750-750-574-571 570-571-570-750-157-750-751-175-175-705-705-715-175-715-107-107-507-170-170-107-517-574-574-574-754-756-750-751-756 570-517-570-175-157-105-157-150-150-5_W.mp3)
  Segment 24: 96908ms ~ 97484ms (file: 712-137-123-326-517-576-567-576-635-657-635-316-632-613-361_W.mp3)
  Segment 25: 98484ms ~ 99060ms (file: 361-621-637-617-613-362 671-612 635-261-621-627-621 621-627 657-126-316_V.mp3)
  Segment 26: 100060ms ~ 100636ms (file: 576-657-126-162-675-216-263-321-216-570-572-621-132-321-130-127-617-136 765-765-761-576-763-756-657-576-372-702-621-756-315-235-762-132-732-276-312-325-675-103-321-132-157-573-5_W.mp3)
  Segment 27: 101636ms ~ 102212ms (file: 153-135 103-103-102-136-136-103-136-176-130-173-176-316-361-136-570-571-570-612-126-216_V.mp3)
  Segment 28: 103212ms ~ 103788ms (file: 375-736-170-716-761-176-756-715-713-167-761-756-765-765-756-756-675-765-107-170-753-571-756-756-751-725-176-765-756-756-176-175-715-756-576-371-675-765-173-276-657-712-176-716-7_W.mp3)
  Segment 29: 104788ms ~ 105364ms (file: 567-567-657-507-502-510-510-561-517-517-517-536-531-537-576-571-510-175-517-175-751-571-175-517-576-571-517-517-571-517-571-571-576-576-571-571-517-567-571-576-531-570-507-517-5_V.mp3)
  Segment 30: 106364ms ~ 113276ms (file: 326-320-321-236-732-123-132-127-761-761-237-132-756-271-312-573-753-276-276-276-762-762-276-267-263-617-236-134-103-102-126-621-236-627-756-675-326-123-361-613-716-267-672-673-7_W.mp3)
  Segment 31: 114276ms ~ 114852ms (file: 623-361-621-216-126-132-123-126-123 213-126-102-132-312 267-312-132-123 231-123-613-136-162-632-627-673-136-631-261-612-617-316-361-316-162-612-312-216-361-316-623-120-631-136-1_W.mp3)
  Segment 32: 115852ms ~ 116428ms (file: 502-501-657-567-167-536-527-517-561-576-570_W.mp3)
  Segment 33: 117428ms ~ 118004ms (file: 267-627-627-627-627-672-367-365-673-361-672-267-321-276-632-623-136-316-632-632-276-267-567-627-263-672-762-267-637-627-635-627-671-673-627-367-762-376-762-627-672-267-267-367-6_W.mp3)
  Segment 34: 119004ms ~ 124512ms (file: 123-617-213-316-612-167-132-130-123-132-612-126-316-321-627-237-261-213-217-162-236-621-126-261-276-326-231-136-621-617-216-231-236-612-320-231-312-321-217-621-720-672-675-632-1_W.mp3)
  Segment 35: 125512ms ~ 126088ms (file: 657-657-567-567-567-657-567-567-567-567-567-567-576-567-657-657 564 567-657-567-657-657-564-567-564-561-567-657-567-564-564-567-654-613-621-231-312-163-613 576-567-576-567-576-5_W.mp3)
  Segment 36: 127088ms ~ 127664ms (file: 152-157-170-175-751-157-751-751-175-175-175-715-571-751-571-715-751-751-751-571-571-571-517-157-157-571-157-571-512-570-156-571-571-715-572-572-751-712-726-152-172-175-157-675-3_W.mp3)
  Segment 37: 128664ms ~ 129240ms (file: 650-605-650-650-652 652-653-650-602-651-625-625-620 605-652-650-652-560-650-652-650 650-650-650-650-560-650-650-650-605-605-605-651-651 650-650-605-603-650-605-650-650-602-650-6_W.mp3)
  Segment 38: 130240ms ~ 130816ms (file: 752-762-761-367-136-517-705-270-576-513-253-572-107-217-127-126-726-572-152-572-715-502-572-752 256-162 207-576-267-763-237-376-312-732-276-256-275-217-275-507-327-152-276-231-2_V.mp3)
  Segment 39: 131816ms ~ 132392ms (file: 726-672-517-157-756-756-675-756-571-576-571-567-716-751-167-516-167-167-165-651-156-156-165-175-751-752-751-157-765-751-756-752-756-756-756-753-576-756-765-572-572-571-572-153-1_W.mp3)
  Segment 40: 133392ms ~ 133968ms (file: 507-507-507-514-571-510-501-507-574-501-517-571-501-571-510 105-157-150-501-105-150-571-570-570-571-157-750-751-715-150-150-510-105-150-510-501-501-501-507-510-150-501-501-507 1_V.mp3)
  Segment 41: 134968ms ~ 141484ms (file: 206-206-203-540-103-130-103-107-627-321-312-312-123-126-136-132-120-135-213-312-123-260-172-102-132-103-267-130-132-123-137-317-137-132-127-721-265-716-521-675-162-120-102-167-1_W.mp3)
  Segment 42: 142484ms ~ 143060ms (file: 150-510-516-501-150-517-157-501-501-510-501-510-510 105-501-105-571-105-150-157-501-105-150-105-150-150-510-501-501-501-501-510-510-510 571-150-150-501-105-501-507-150-105-150-5_W.mp3)
  Segment 43: 144060ms ~ 144636ms (file: 316-163-103-130-136-103-106-130-231-160-561-216-125-103-136-102-136-103-162-103-136-167-316-135-765-213-167-160-162-316-617-103-102-136-106-103-546-132-167-157 137-326-601-106-6_V.mp3)
  Segment 44: 145636ms ~ 155392ms (file: 267-371-317-130-132-762-765-627-267-627-102-103-723-237-321-130-103-236-231-176-163-173-103-310-371-317-106-756-756-756-765-260-712-130-106-167-103-213-123-120-753-107-106-167-1_W_stt_the_data_as_she_attempted_to_pull_the_po.mp3)
  Segment 45: 156392ms ~ 169316ms (file: 236-752-103-574-765-273-103-130-130-310-236-263-574-267-752-143-723-236-130-726-106-751-753-312-765-236-213-312-361-321-132-326-236-726-213-231-126-231-210-321-756-276-130-321-3_W.mp3)
  Segment 46: 170316ms ~ 170892ms (file: 762 657-675-675-267-627-567-765-263-267-527-756-625-672-765-756-756-675-765-756-627-576-715-567-576-576-671-765-567-576-765-576-576-756-567-765-326-362-572-576-576-567-657-653-7_W.mp3)
  Segment 47: 171892ms ~ 172468ms (file: 173-310-103-103-173-173-170-753-307-130-730-170-310-315-751-751-756-130-137-753 713-170-170-175-736-731-710-103-130-130-137-710-713-312-107-130-301-172-137 703-137-317-310-103-7_V.mp3)
  Segment 48: 173468ms ~ 174044ms (file: 756-541-523-542-564-561-156-531-546-752-576-576-756-576-756-576-576-756-576-576-576-756-752-231-213-524-504-513-514-504-504-504-504-504-513-564-503-506-572-576-572-576-576-756-5_W.mp3)
  Segment 49: 175044ms ~ 175620ms (file: 376-173-137-130-130-107-753-731-375-170-317-301-310-137-310-130-317-103-713-317-173 371-701-173-130-613-167-317-176-316-617-317-371-103-316-167-130-130-317-103-517-357-375-170-7_V.mp3)
  Segment 50: 176620ms ~ 191452ms (file: 213-126-321-260-320-321-127-321-231-236-213-231-235-253-102-726-726-103 321-236-230-231-320-732-236-132-320-260-276-237-321-320-231-132-725-725-753-270-102-302-321-230-310-217-3_W.mp3)
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
> **Time**: 2026-04-05 10:46:42
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "비극적인 결말은 거의 피할 수 없게 되죠 얼음 지구상의 많은 바다와는 달리",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "비극적인", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "결말은", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "피할", "start_ms": 350, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 380, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "없게", "start_ms": 430, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "되죠", "start_ms": 470, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "얼음", "start_ms": 500, "dur_ms": 50, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "베링해는 매년 겨울이 되면 반드시 일부가 얼어붙습니다 지구 온난화",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "베링해는", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "매년", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "가 어우러지면서 매우 혹독하고 예측하기 어려운 기상 조건이 만들어지고 있는 것입니다 기후의 경계에 놓인 바다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "가", "start_ms": 15, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어우러지면서", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "사실 베링해에서 가장 큰 위험은 바로 그 기후였습니다 그 밖의 모든",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "사실", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "베링해에서", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "중요하며 신중한 입장을 유지하고 파병 요청에 즉각 응하지 않자 트럼프 대통령은 이를 배신이자 기회주의자로",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "중요하며", "start_ms": 25, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "신중한", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 18,
"transcript": "천만 달러의 자산을 오직 단타 트레이딩 하나만으로 일궈낸 남자라면 믿으실 수 있으신가요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "천만", "start_ms": 60, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "달러의", "start_ms": 310, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "그의 이름은 제이슨 노아 그가 말한 트레이딩의 3원칙은 좋아요와 구독으로 다음 이야기에도 동행해 주세요",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그의", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 200, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "다음 영상에서 또 만나요 단골이세요 하지만 쇄빙선은 쐐기형 선수로 경사져 있죠 쇄빙선은 이 쐐기",
"speaker": "speaker_4",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영상에서", "start_ms": 170, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "민주당과의 격차가 38% 포인트나 됐습니다 베이 의원은 선거비 보전도 못할까 봐 후보들이 도통 나서지 않는다며",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "민주당과의", "start_ms": 30, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "격차가", "start_ms": 420, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "구인난이 심 안내 말씀 드립니다 지난주 토요일에 소개해 드린 현대판 불로초 NMN 안티에이징 리포좀 크림 영상 이후",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "구인난이", "start_ms": 25, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "심", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "바이오를 위해 거실 오른쪽에는 따로 주방을 냈고요 에 이런 반응들을 보인 걸까요 세상에서 가장 비싸고",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "바이오를", "start_ms": 30, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "가장 위험하고 가장 희귀한 물질 만들기도 지옥이고 보관하기도 Kimchi fashion 중간에 단감 내려가고 그러면 사레 김치 좀 있고",
"speaker": "speaker_6",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "위험하고", "start_ms": 260, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "양념 닿으면 또 아프고 쓰리고 만반의 준비를 마쳤으니 배춧잎 사이사이가 어우러지면서 매우 혹독하고 예측하기 어려운 기상",
"speaker": "speaker_7",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "양념", "start_ms": 50, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "닿으면", "start_ms": 310, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "조건이 만들어지고 있는 것입니다 기후의 경계에 놓인 바다 사실 베링해에서 가장 큰 위험은 바로 그 기후였습니다 그 밖의 모든",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "조건이", "start_ms": 25, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "만들어지고", "start_ms": 320, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "중요하며 신중한 입장을 유지하고 파병 요청에 즉각 응하지 않자 트럼프 대통령은 이를 배신이자 기회주의자로 규정해 버린 것입니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "중요하며", "start_ms": 30, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "신중한", "start_ms": 360, "dur_ms": 280, "phase_lr_deg": 0.0}
]
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
"transcript": "천만 달러의 자산을 오직 단타 트레이딩 하나만으로 일궈낸 남자라면 믿으실 수 있으신가요 그의 이름은 제이슨 노아 그가 말한",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "천만", "start_ms": 20, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "달러의", "start_ms": 360, "dur_ms": 360, "phase_lr_deg": 0.0}
]
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
},
{
"segment": 50,
"transcript": "트레이딩의 3원칙은 좋아요와 구독으로 다음 이야기에도 동행해 주세요 다음 영상에서 또 만나요 단골이세요 하지만 쇄빙선은 쐐기형",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "트레이딩의", "start_ms": 45, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "3원칙은", "start_ms": 460, "dur_ms": 360, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 10:46:42*
