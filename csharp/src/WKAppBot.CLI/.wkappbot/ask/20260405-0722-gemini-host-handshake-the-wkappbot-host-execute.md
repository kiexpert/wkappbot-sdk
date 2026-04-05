# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=07:20:59  uptime=11520s  mem=479MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_072013.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 574-570-574-574-571-570-571-570 574-574-574-574-574-574 574-574-547-574-574-517-517-574-574-574-570-571-570-574-574-571-571-507-750-571-570-751-157-750-570-571 571-574-574-574-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 567-657-657-176-163-162-167 263 540-105-103-103-103-130-312-301-103 132-132-123-201-267-326-136-531-576-260-620-261-216-542-546-576-312-123-312-316-320-362-627-620-620-260-267-1_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 137-103-132-136-173-105-761-130-107-106-610-135-362-165-136-103-106-316-103-136-502-156-163-163-135-136-615-136-106-301-564-673-167-651-176-172-103-651-136-107-176-671-736-103 7_V.mp3)
  Segment 4: 4728ms ~ 8328ms (file: 206-765-675-167-167-672-627-672-267-726-627-621-621-612-672-231-216-123-756-725-267-627-126-126-236-206-623-627-362-627-216-263-261-627-672-612-216-627-267-671-617-261-261-216-2_W_stt_if.mp3)
  Segment 5: 9328ms ~ 16312ms (file: 127-136-102-267-627-236-571-267-316-136-725-627-672-627-621-260-216-126-136-510-215-752-206-276-612-305-213-172-102-126-106-201-312-275-137-321-107-103-137-715-173-312-127-132-1_V.mp3)
  Segment 6: 17312ms ~ 17888ms (file: 750-107-701-710-701-750-701-750-701-715-705-751-175-175-157-756-751-750 517-751-571-710-170-710-701-710-705-701-710-701-705-701-701-751-705-750-751-751-715-751-715-750-750 570-5_V.mp3)
  Segment 7: 18888ms ~ 35304ms (file: 752-206-261-562-132-130-762-265-261-173-361-361-672-637-367-361-317-176-317 756-576-273-675-216-572-723-260-275-327-506-261-267-672-132-572-527-263-620-312-157-137-362-263-132-5_W.mp3)
  Segment 8: 36304ms ~ 36880ms (file: 157-157-517-571-157-517-157-517-571-150-517-571-157-157-157-157-154 103-105-105-105-150-157-103-107-157-157-157-157-157-517-157-157-517-157-570-517-517-510-175 103-105-105-105-1_V.mp3)
  Segment 9: 37880ms ~ 38456ms (file: 501-571-153-107-105-157-153-501-510-501 105-150-150-510-517-150-517-150-105-150-510-501-510-507-501-150-105-150-150-107-105-150-517-150-510-150-510-517-570 150-150-517-150-150-5_V.mp3)
  Segment 10: 39456ms ~ 40032ms (file: 162-123-152-153-756-715-156-263-123-637-267-702-162-162-132-276-617 207-273-231-271_V.mp3)
  Segment 11: 41032ms ~ 49348ms (file: 321-316-136-312-753-267-572-167-571-574-576-157-160-153-261-756-103-261-213-261-576-261-261-527-576-106-316-132-261-126-267-216-263-310-576-267-310-261-267-267-321-326-657-567-2_W.mp3)
  Segment 12: 50348ms ~ 50924ms (file: 713-627 517-571-571-517-157-175-157-701-137-107-710-137-130-170_V.mp3)
  Segment 13: 51924ms ~ 67620ms (file: 675-123-263-103-130-103-103-267-123-276 756-276-261-261-267-627-627-267-231-276-210-261-231-102-126-102-120-102-765-276 276-756-127-231-263-103-236-213-102-765-267-201-216-120-2_W.mp3)
  Segment 14: 68620ms ~ 73696ms (file: 206-230-231-236-321-537-312-236-726-752-705-172-176-752-627-103-327-502-206-206-130-316-230-312-657-617-507-570-352-573-571-537-132-756-320-236-135-103-312-320-132-136-301-321-3_V_stt_his_loving_and_supplied_a_i_system_got_t.mp3)
  Segment 15: 74696ms ~ 81392ms (file: 573-130-547-326-756-267-236-236-574-321-321-236-527-132-537-236-263-542-236-310-170-231-325-235-253-542-327-537-137-217-231-321-235-321-573-507-257-267-237-275-265-126-137-312-2_W.mp3)
  Segment 16: 82392ms ~ 82968ms (file: 157-120-153-170-153-315-536-165-135-135-152-123-165-173-163-135-156-127-627-631-753-312-351-153-157-352-371-317-135-136-316-105-571-750-157-175-715-173-157-137-157-715-571-571-5_W.mp3)
  Segment 17: 83968ms ~ 89404ms (file: 256-267-157-175-705-751-671-210-132-153-201-132-257-127-762-376-520-574-267-317-672-157-627-326-705-207-103-576-657-371-512-572-312-130-657-261-573-152-132-132-216-702-712-671-2_V.mp3)
  Segment 18: 90404ms ~ 99512ms (file: 236-132-130-276-317-736-312-132-321-237-102-132-372-326-267-263-361-130-102-123-573-132-210-327-756-326-321-104-106-210-267-236-763-320-362-326-263-236-132-267-231-236-136-236-2_W.mp3)
  Segment 19: 100512ms ~ 101088ms (file: 675-675-753-756-671-671-317-312-715-672-756-672-376-726-765-675-762-672-672-762-763-627-675-765-756-576-567-573-675 765-763-765-716-726-763-763-761-672-763-671-762-762-765-765-6_W.mp3)
  Segment 20: 102088ms ~ 102664ms (file: 135-153-153-154-153-315-135-351-153-153-136-135-516-213-237-213-231-236-231-236-236-263-235-236-253-527-576-352-257-236-576-615-361-261-213-216-261-231-326-623-621-132-361-657-3_W.mp3)
  Segment 21: 103664ms ~ 110432ms (file: 260-260-627-206-210-261-206-120-102-231-260-312-201-206-517-130-302-206-273-123-630-257-201-310-130-310-103-137-103-132-547-257-267-547-512-127-130-103-627-261-230-610-213-120-2_V.mp3)
  Segment 22: 111432ms ~ 120216ms (file: 617-761-672-627-162-230-326-132-106-721-231-271-237-237-123-271-167-267-167-230-257-275-106-162-132-316-672-627-620-671-613-612-621-162-163-136-102-231-672-106-106-236-236-213-3_W.mp3)
  Segment 23: 121216ms ~ 129028ms (file: 237-276-702-176-137-570-107-762-576-574-263-326-753-761-756-371-132-413-153-175-760-750-107-167-167-730-312-760-327-574-570-574-507-176-163-750-570-765-320-102-574-574-136-312-1_W.mp3)
  Segment 24: 130028ms ~ 130604ms (file: 571-570 517-517-531-517-153-715-751-571-517-157-751-715-571-571-570-576-570-570-570-507-507-507-576-510-570-576-576-570-570-570-570-507-501-501-503-501-510-501-507-510-510-570-5_V.mp3)
  Segment 25: 131604ms ~ 132180ms (file: 321 176-136-310 706 574-576-576-756-756-756-123-126-571-136-632-261-326-302-326-576-756-765-765-762-126-231-321_W.mp3)
  Segment 26: 133180ms ~ 142612ms (file: 162-261-612-316-120-267-251-162-261-263-260-267-136-213-267-756-231-231-765-120-316-534-123-572-623-652-267-365-357-126-172-261-261-316-531-326-231-216-562-621-136-216-267-216-2_W.mp3)
  Segment 27: 143612ms ~ 153512ms (file: 267-276-753-103-321-716-523-574-571-765-162-357-123-312-162-612-157-172-275-107-275-720-127-125-675-267-672-256-210-217-267-120-231-123-120-267-236-267-217-213-120-613-162-123-1_W.mp3)
  Segment 28: 154512ms ~ 155088ms (file: 216-160-620-201-765-762-723-160-162-136-165-167-351-613-103-106-672-132-713-130-162-517-106-176-103-137-160-203-320-106-617-103-610-106-123-162-263-130-103-610-763-132-261-316-3_V.mp3)
  Segment 29: 156088ms ~ 156664ms (file: 375-375-375-731-375-317-103-315-137-136-316-163-135-316-317-173-713-371-367-705-750-715-753-137-170-713-175-173-715-173-173-713-715-132-317-137-175-130-132-132-127-132-351-731-1_W.mp3)
  Segment 30: 157664ms ~ 158240ms (file: 510-150-150-501-157-750-175-107-570-750-715-157-170-571-715 571-504-510-504-501-504-510-150-501-751-105-157-105-175-571-175-107 570-510-571-517-517-570-571-570-517-510-517-507-7_W.mp3)
  Segment 31: 159240ms ~ 159816ms (file: 602-673-367-627-672-627-362-260-267-672-236-570-250-312-263-627-627-627-627-627-263-263-620-672-627-672-765-627-765-762 675-675-675-756-675-675-765-675-765_W.mp3)
  Segment 32: 160816ms ~ 173344ms (file: 754-756-310-310-103-301-627-547-235-103-210-126-130-201-574-312-542-547-310-213-351-312-106-106-130-201-125-165-132-230-310-613-316-534-135-301-130-165-130-103-213-102-103-320-5_W.mp3)
  Segment 33: 174344ms ~ 178808ms (file: 672-267-276-607-106-126-701-267-765-675-156-136-671-675-213-163-620-567-210-276-261-126-210-321-627-271-726-107-107-126-126-162-132-753-762-671-260-602-102-102-217-127-210-623-6_V.mp3)
  Segment 34: 179808ms ~ 184272ms (file: 504-504-510-501-157-501-571-547-547 157-154-150-150-105-517-517-157-105-105-105-157-175-175-105 510-570 103-136-312 123-102-512-203-206-602-231-123-132-623-260-260-213-130-175-5_V.mp3)
  Segment 35: 185272ms ~ 185848ms (file: 612-167-372-376-127-632-627-672-276-632-631-627-637-632-367-621-126-126-167-137-160-263-136-103-163-167-631-613-326-612-765-675-612-621-612-657-675-673-765-765-671-736-621-571-1_W.mp3)
  Segment 36: 186848ms ~ 187424ms (file: 756-756-576-750-756-751-103-130-672-316-613-367-327-327-372-372-237-732-732-327-723-723-237-723-736-736-162-132-135-103-135-107-157-632-571-316-130-136-316-317-135-351-103-106-1_W.mp3)
  Segment 37: 188424ms ~ 198036ms (file: 203-360-130-316-157-517-513-130-310-276-754-267-267-750-321-362-136-260-236-562-617-702-574-317-103-172-627-210-705-216-301-326-765-765-623-263-316-263-326-756-230-527-302-623-2_W.mp3)
  Segment 38: 199036ms ~ 207388ms (file: 623-170-137-312-726-762-130-751-762-762-106-162-105-103-163-132-102-371-130-263-256-102-125-230-237-276-756-326-137-230-167-107-132-103-716-731-732-123-157-107-237-576-276-167-1_W.mp3)
  Segment 39: 208388ms ~ 208964ms (file: 130-310-130-306-306-310-316-310-360-301-362-603-162-163-163-301-162-123-132-162-612-321-123-312-236-213-302-130-126-102-201-206-231-103-136-106-103-130-103-130-574-576-637-756-3_V.mp3)
  Segment 40: 209964ms ~ 210540ms (file: 130-103-310-130-175-157-157-153-351-576-573-576 703-751-157-315-137-135-137-173-361-130-607-613-361-637-631-617-610-601-730-301-310-103-371-310-130-130-137-130-170-317-370 730-7_V.mp3)
  Segment 41: 211540ms ~ 212116ms (file: 627-263-617-672-627-602-632-603-612-320-263-230-236-201-236-567-567-567-106-613-620-260-201-276-756-750-361-576-160-613-163-601-360-603-602-203-206-130-730-750-752-270-752-206-3_W.mp3)
  Segment 42: 213116ms ~ 217724ms (file: 103-103-310-130-103-103-320-231-123-102-210-132-120-276-236-762-372-372-276-726-237-670-627-201-267-263-120-236-216-260-206-216-612-260-216-126-267-216-126-260-126-206-126-120-2_W.mp3)
  Segment 43: 218724ms ~ 227220ms (file: 310-132-236-762-236-132-317-302-312-753-237-231-326-275-216-216-321-267-132-230-263-312-263-630-160-106-316-326-267-261-206-267-267-267-312-201-261-213-326-310-236-267-260-276-3_W_stt_the_giveaway_with_a_10_ze_day_of_the_bla.mp3)
  Segment 44: 228220ms ~ 228796ms (file: 134-136-136-136 316-312-301-132-301-312-321-312-236-216-231-231-574-157-167-106-163-607-670-617-631-716-371-176-106-167-617-167-631-361-316-637-607-637-671-617-671-327-617-627-6_V.mp3)
  Segment 45: 229796ms ~ 236924ms (file: 267-136-263-572-267-267-576-150-206-260-263-326-276-573-102-310-130-132-312-130-231-132-750-756-260-132-310-527-251-123-752-312-107-103-103-310-130-310-310-312-361-130-316-130-2_W.mp3)
  Segment 46: 237924ms ~ 238500ms (file: 756-130-135-135-317-103-312-317-371-371-163-316-316-130-137-735-753-371-713-710_W.mp3)
  Segment 47: 239500ms ~ 240076ms (file: 710-570-751-715-750-570-571-705-571-570-750-750-750-571-570-751 574-574-574-574-574-570-574-570-574-574-574-547-547-574-574-574-574-547-574 574-574-574-574-574-574-574-547 571-5_W.mp3)
  Segment 48: 241076ms ~ 241652ms (file: 236-507-726-753-567-567-326-321-750-275-362-263-326-257-267-236-237-231-701-135-261-517-765-167-310-726-205-625-103-310-315-135-132-172-327-107-127-270 231-276-127-537-136-316-1_V.mp3)
  Segment 49: 242652ms ~ 243228ms (file: 153-103-134-301-314-310-130-301-103-134-134-130-103-143-134-134-134-134-103-134 374-154-130-310-130-310-134-103-143-314-314-314-341-413-103-140-430-104-134-310-103-130-314-314 4_W.mp3)
  Segment 50: 244228ms ~ 248152ms (file: 230-207-260-321-306-310-316-317-137-317-137-753-567-375-753-753-576-275-256-265-261-132-657-150-126-123-263-132-132-123-135-576-265-236-215-261-203-273-572-230-231-360-360-362-3_W_stt_f-fifth_of.mp3)
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
> **Time**: 2026-04-05 07:22:11
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘의 장점은 [music]",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "mixed",
"words": [
{"word": "오늘의", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "장점은", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "부터가 다리 관절을 움직이게 하는 건데요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "부터가", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "다리", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "관절을", "start_ms": 390, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "움직이게", "start_ms": 620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "건데요", "start_ms": 1040, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "이때 로봇 제어의",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이때", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 190, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제어의", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "청산했네? 네, 어제 일단 청산했습니다. 아, 제가 미수 동결이 3월 23일 날 되었어요. 그러면서 이제 강제로 미수가",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "청산했네?", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": -10.0},
{"word": "네,", "start_ms": 460, "dur_ms": 120, "phase_lr_deg": 10.0},
{"word": "어제", "start_ms": 590, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "일단", "start_ms": 800, "dur_ms": 180, "phase_lr_deg": 10.0}
]
},
{
"segment": 5,
"transcript": "미수가 막히고 수익을 훨씬 잘 내셨군요. 신용도 안 쓰려고요. 한 번 맞으면 이게 또 올라오기가",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "미수가", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "막히고", "start_ms": 300, "dur_ms": 220, "phase_lr_deg": -5.0}
]
},
{
"segment": 6,
"transcript": "낼 수 있었습니다. 보신 바와 같이 롱 포지션 매매의 핵심은 바뀐 신호가 아니라 반응이 확정된 눌림에서 진입하는 것",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "낼", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있었습니다.", "start_ms": 210, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "이게 가장 중요한 포인트라고 합니다. 노아는 이 타점만으로도 하루에 40",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이게", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "이 회사의 한 사람당 1만큼 생산하고 이렇게 곱하기 10해서 총 10만큼 생산한 거죠. 그런데 만약에 앞으로 직원들이",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 15, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "회사의", "start_ms": 100, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "AI를 이용해서 한 사람당 10만큼의 생산을 해낸다고 쳐요. 그러면 기업들은 직원을 하나로 줄이는 선택을 하는 게 아니라",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "AI를", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "직원 열 명을 그대로 유지하고 100만큼 생산할 거란 얘기예요. 기업들은 어떻게든",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "직원", "start_ms": 25, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "So here's what I'm thinking. So Claude AI system got five out of five and it did it in just a little",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "So", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "here's", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "한 명과 담당 기술진 다섯 명을 급파했습니다. 한화시스템 관계자는 UAE와의 계약 사항을 책임지고 이행하기 위해 불가피하게 출장을 보내기로 결정했다고 밝혔습니다.",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "명과", "start_ms": 140, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "입니다. 사막의 가장 큰 약점인 물 부족을 기술로 해결하자 사막의 강점이 그대로 살아났습니다. 사막이라는 특수한 환경이 오히려 아군이 된 겁니다. 이 결과가 중동 왕실에 어떻게 받아들여졌을지는 이후의 움직임을 보면 알 수 있습니다.",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "입니다.", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "진짜 오랜만이다 보고 싶었던 친구예요. 야, 너도 잘 지냈어? 아, 진짜 보고 싶었어. 물로도 그리면",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "데 현재 지역이 이제 허용된 구역이다 혹은 운전자 상태가 지금 정상이다 라는 정보를 쏘아 올려서 보내면 그걸 믿고 거기",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "데", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "S+를 국내에서 팔고 있는 S6+랑 직접 비교했을 때 일부 앱에서 좀 급속도로 발열",
"speaker": "speaker_11",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "S+를", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "제공됩니다. 실제로 문명의 초기 확산 경로를 보면 이 논리가 더 설득력을 얻습니다. 유라시아 대륙은 동서 방향으로 길게 뻗어 있어서 같은 위도선을 따라 작물과 가축이 수평으로 이동했으며 비슷한",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제공됩니다.", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "수준 센서가 필요하거든요. 관성 센서라고 부르는데 그 센서도 여기 들어가 있고요. 그리고 이제 배터리가 들어가 있습니다. 아.",
"speaker": "speaker_12",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "수준", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "번째 전투기라고 강조하며 잔해 사진을 공개했습니다. 5세대 스텔스 전투기 F-35는 미 공군력의 상징과도 같은 핵심",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "번째", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "고 소중히 간직하고 있습니다. 앞으로도 더 흥미로운 시선으로 찾아올게요. 다음 영상에서 뵙겠습니다. 감사합니다.",
"speaker": "speaker_13",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "고", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "소중히", "start_ms": 130, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "가 차트 지나가면서 이제 좋아질 일만 남지 않았을까 라고 보는 거 같아요. 그래서 시장은 조금 하방이 좀 제한적인 모습이 나오고 오히려 이렇게 반등이 좀 나왔다 이렇게 보시면 될 것 같습니다. 그래서 외인도 순매수 마감했다. 그래서 여러분들도",
"speaker": "speaker_14",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "기 결정한대요. 왜냐하면 해외 시청자들도 이제 알아버렸거든요. 한국 콘텐츠의 진짜 무서운 부분은 이야기만이 아니라 그걸 만들어내는 방식 자체라",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "기", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "히 왔을 때도 그렇고 또 이전에 수익 손실을 봐 본 경험이 있으면 그만큼 그릇이 저는 커진다고 생각하거든요. 그래서 앞으로 또 조금 더 성장하실 수 있는 그런 계기가 아마 올해가 되지 않",
"speaker": "speaker_16",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "히", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "[music]",
"speaker": "none",
"confidence": 0.0,
"language": "none",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 25,
"transcript": "1월달에 이제 그릇이",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "1월달에", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 430, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그릇이", "start_ms": 590, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "어 우리나라 선박은 이제 26척이고 한국인 선원은 이제 외국적 선박 포함해서 175명이 고립되어 있습니다. 네, 선원들의 안전이",
"speaker": "speaker_17",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": -10.0}
]
},
{
"segment": 27,
"transcript": "히 반영할 것입니다. 새로 지어지는 주택 도면에는 이런 공간들이 추가되었습니다. 주방 옆 세탁실, 1층 복도 랜더리 룸, 안방 드레스룸과 연결된 세탁 공간. 이런 새로운 공간들이 등장하기 시작한",
"speaker": "speaker_18",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "히", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "축하고 대비하는 거예요. 자, 재밌는 건 적이 어떻게 나올지 치열하게 고민하다 보면 자연스럽게 우리의 공격력도 더 날카로워진다는 거예요. 자, 이런 식으로 이렇게 공격과 방어가",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "축하고", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "행동 하나가 30년의 꿈을 날려버릴 수도 있었으니까요. 트럭이 목적지에 도착했습니다. 베이스 스탭 장치를 내렸고요. 놀랍게도 반 양성자들은 소멸하지 않고 그대로 살아있었습니다. 베이스 팀은 즉시 실험을 재개",
"speaker": "speaker_19",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "행동", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "도록 초벌로 튀겨 식혀줍니다. 한 70마리 80마리씩 튀겨나도",
"speaker": "speaker_20",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "도록", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "초벌로", "start_ms": 180, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "포커스 양보가 필요합니다.",
"speaker": "speaker_21",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "필요합니다.", "start_ms": 680, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "따라서 모두 반짝 움직일 수도 있겠죠. 자, 그 다음에 또 4월 9일 날 기아의 인베스터 데이 또 현대차 그룹의 또 중요한 이벤트가 있죠. 최근에 현대차 기아 뭐 육가도 계속 상승하고 있고 그래가지고",
"speaker": "speaker_14",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "따라서", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "가 미국산에 비해 훨씬 저렴한 가격으로 우수한 성능을 보여주고 있다면서 미국 방위산업체들이 수십 년 누려온 독점 체제가 무너질 수 있다고 보도했습니다. 이 매체가 가장 먼저 주목한 건",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "none",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 35,
"transcript": "게 보기보다 자율주행 이 뇌가 믿고 있는 것들이 좀 다르게 이렇게 유도를 해가지고 자율주행 그냥 판단은 제대로 하고 있었",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "게", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "확인할 수 있는 정말 말도 안 되는 매매법이었습니다. 여러분, 시드가 부족하다고요? 제이슨 노아 역시 처음부터 돈이 많았던 사람이 아니었습니다. 오히려 우리보다 더 가난",
"speaker": "speaker_22",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "확인할", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "하네요. 지현과 함께 가자 우리 [music]",
"speaker": "speaker_23",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "mixed",
"words": [
{"word": "하네요.", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지현과", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 760, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가자", "start_ms": 1080, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 1450, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "입니다. 수색이 계속되었지만 상황이 이렇다 보니까 배들이 이렇게 위험한 곳을 피해서 저기 멀리 아프리카 희망봉으로 삥삥 돌아가고 있",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "입니다.", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "만을 다 후추 고춧가루 [noise] 자, 고춧가루도 넣고 얼큰하게 조차",
"speaker": "speaker_24",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "mixed",
"words": [
{"word": "만을", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "죠. 프리앙카가 이 정도 반했으니 인도 반응도 장난이 아니에요. 이 영화가 터진 이후 인도 현지 반응은 그냥 흥행했다 수준이 아닙니다. 인도 대표 매체인 퍼스트 포스트는 아예 메인 기사로 이 현상을 다루면서 이건 기록이 아니라 사건",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "죠.", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "이죠. 손절 구간은 방금까지 반등했던 고점 위로 잡아주시고 익절은 직전 저점 구간에서 캔들 하나 아래로 잡아주시면 됩니다. 손익비는 동일하게 1:1.5를 유지하면서 진입",
"speaker": "speaker_25",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이죠.", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 07:22:11*
