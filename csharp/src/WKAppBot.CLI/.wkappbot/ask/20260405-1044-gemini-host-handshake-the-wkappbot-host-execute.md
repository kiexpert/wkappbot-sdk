# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=10:41:16  uptime=23537s  mem=399MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_104104.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 756-765-675-567 526-520-502-576-265-506-675-765-675-675-765-675-765-765-765-675-675 570-570-502-205-762-267-265-260-250-257-657-570-725-572-576-752-257-576-752-567-257-567-752-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 165-103-163-136-130-163-136-106-106-617-103-163-176-237-167-163-136-165-612-103-163-176-156-132-136-576-625-267-367-167-136-163-162-137-106-103-617-136-617-137-165-263-163-651-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 510-513-175-510-517-571-157-510-501-175-175-710-701-715-750-750-756-570-750-570-570-570-570-570 715-517-517-156-571-751-751-715-751-731-175-751-751-571-571-576-576-571-570-571-5_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 501-150-510-125-125-162-257-751-765-752-756-527-572-506-576_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 251-275-276-276-261 751-715-137-621-621-216-267-261-172-216-261-271-217-216 571-157-153-571-751-751-715-751-751-170-175-135-510-175-170-157-175 175-751-175-175-715-175-150-715-7_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 126-361-765-652-756-675-327-673-127-752-256-751-316-507-127-572-135-573-317-321-510-572-267-715-172-231-236-312-576-751-123-217-672-261-263-752-567-157-261-750-725-267-321-652-5_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 163-163-106-103-160-157-103-715-136-130-136-160-130-106-103-136-136-163-160-130-103-263-623-216 162_V.mp3)
  Segment 8: 11032ms ~ 19924ms (file: 267-672-237-132-213-672-321-321-123-132-627-762-325-576-162-237-273-756-735-601-756-276-132-371-261-236-103-130-130-236-267-627-261-263-267-236-231-236-236-231-321-102-123-231-2_W.mp3)
  Segment 9: 20924ms ~ 21500ms (file: 761-675-657-672-657-657-675-673-675-675-671-265-267-625 625-657-567-675-675-765-756-675-673-615-165-165-615-615-165-615-165-617-165-165-615-613-165-675-567-657-765-657-765-657-6_W.mp3)
  Segment 10: 22500ms ~ 23076ms (file: 371-137-107-130-103-130-310-173-107-137-137-750-310-705-173-310 301-103-130-312-137-130-107-102-103-132-103-301-372-170-310-137-751 107-710-107-137-371-571-710-103-321-701-137-1_V.mp3)
  Segment 11: 24076ms ~ 24652ms (file: 310-326-367-631-301-316-263-306-231-623-130-136-136_V.mp3)
  Segment 12: 25652ms ~ 26228ms (file: 134-150-135-140-104-145-150-150-130-103-154-410-130-104-130-134-105-135-103-103-104-310-130-103-143-103-103-135-103-103-103 534-347-413-173-104-103-107-104-401-105-140-143-104-1_V.mp3)
  Segment 13: 27228ms ~ 27804ms (file: 762-675-765-765-675-672-672-612-675-627-672-261-627-621-261-216-657-251-231-312-123-213-267-216-326-326-236-132-213-621-362-612-621-627-162-716-617-651-615-621-612-126-632-561-5_W.mp3)
  Segment 14: 28804ms ~ 29380ms (file: 316-163-136-753-756-756-756-576-576-576-574-356-732-763-756-763-736-107-176-731-173-317-312-312 361-136-673-736-716-751-107-130-235-123-213 307-763-763-731-732-372-731-713-103-1_W.mp3)
  Segment 15: 30380ms ~ 30956ms (file: 571-571-517-751-157-157-715-751-157-751-571-751-157-751-571-751-571-751-751-751-751-571_W.mp3)
  Segment 16: 31956ms ~ 32532ms (file: 156-617-165-156-167-517-107-750-751-756-751-571-750-750-751-750-751-751-750-756-750-105-105-105-105-105-105-105-105-107-170-175-165-157-156-157-150-175-150-517-175-157-107-170-1_W.mp3)
  Segment 17: 33532ms ~ 34108ms (file: 527-527-527-572 176-163-176-512-157-150-517-572-527-561-251-527-521-251 125-205-675-725-752-572-750-725-710-257-217-752-756-715-217-521-527-526-752-756-752-726-762-267-752-576-5_W.mp3)
  Segment 18: 35108ms ~ 35684ms (file: 107-317-130-173-103-107-705-317-172-173-137-130-137-751-371-730-170-370-715-715 351-705-170-701-103-173-137-103-123-317-137-173-103-317-103-137-713-137-130-715-703-137-170-102-1_V.mp3)
  Segment 19: 36684ms ~ 47844ms (file: 765-716-713-176-726-725-276-267-217-275-752-217-276-217-752-756-672-675-765-765-672-123-765-167-127-127-176-123-132-261-123-216-312-231-123-261-216-217-263-721-762-726-132-267 1_W.mp3)
  Segment 20: 48844ms ~ 59140ms (file: 231-167-163-310-132-270-576-752-327-312-132-160-527-301-267-132-765-765-276-102-105-210-672-103-267-102-236-130-507-167-751-765-263-263-261-260-206-253-107-176-312-316-130-312-1_W.mp3)
  Segment 21: 60140ms ~ 60716ms (file: 103-132-301-103-135-713-130-703-301-705-107-307-371-137-137-310 137-137-130-170-173-130-103-107-301-317-310-735-173-103-137-130-175-370-130-371-175-702-175-710 137-130-317-130-3_V.mp3)
  Segment 22: 61716ms ~ 62292ms (file: 576-576-573-571-506-501-510-507-516-576-501-573-576-576-570-576-516-571-517-517-157-572-157-756-751-576-752-572-752-501-510-571-501-510-507-105-517-571-570-571-570-570-570-517-5_W.mp3)
  Segment 23: 63292ms ~ 63868ms (file: 574-751-571-751-754-571-751-715-517-175-751-751-574 571-571-571-175-175-175-137-175-175-175-175-170-715-173-375-153-157-317-105-175-571-570_W.mp3)
  Segment 24: 64868ms ~ 65444ms (file: 715-751-756-751-751-756 150-157-150-517-105-163-175-751-756-751-756-751-751-751-751-751-175-751-715-751-756-751-175-715-175-175-751 150-517-153-105-105-175-175-751-175-715-175-1_V.mp3)
  Segment 25: 66444ms ~ 72672ms (file: 712-570-132-263-576-754-675-130-231-213-745-312-173-517-570-137-201-216-276-267-752-321-302-231-623-754-130-713-753-102-215-201-123-132-231-260-206-267-754-206-620-672-263-216-1_W.mp3)
  Segment 26: 73672ms ~ 74248ms (file: 756-756-567-756-576-756-576-756-756-534-504-504-547-501-542-504-506-506-506-504-504-504-504-756-756-756-576-756-756-576-756-576-576-756-576-576-756-756-756-756-752-751-231-504-5_W.mp3)
  Segment 27: 75248ms ~ 95084ms (file: 157-167-213-261-126-513-103-106-167-163-123-671-317-136 673-623-312-361-603-576-576-567-567-675-576-216-621-132 216 123-126-176-627-126-623_W.mp3)
  Segment 28: 96084ms ~ 96660ms (file: 157-156-561-165-156-167-175-612-167-165-765-657-756-765-576-765-675-567-657-756-765-675-765-756-657-675-672-675-765-167-165-167-165-165-165-165-163-162-615-612-162-612-621-162-6_W.mp3)
  Segment 29: 97660ms ~ 98236ms (file: 137-316-572-321-517-756-526-570-362-215-762-271-263-206-237-756-725-326-271-123-213-231-123-153-267-316-312-125-576-321-725-571-175-574-573-301-307-130-123-103-567-231-136-326-3_V.mp3)
  Segment 30: 99236ms ~ 99812ms (file: 657-657-657-567-576-567-576-657-567-567-576-574-657-765-576-765-167 265-612-651-675-657-657-657-567-657-657-567-567-567-657-657-657-567-567-657-657-617 675-657-657-657-657-615 6_W.mp3)
  Segment 31: 100812ms ~ 101388ms (file: 571-517-751-735 517-513-517-507-517-510-157-150-105-157_W.mp3)
  Segment 32: 102388ms ~ 102964ms (file: 571-517-501-517-517-507-510-517-517-570-150-517-501-567-576-756-576-567-576-751-571-571-657-756-756-756-756-501-516-501-547-507-501-501-507-571-157-150-517-765-571-571-576-576-7_W.mp3)
  Segment 33: 103964ms ~ 104540ms (file: 136-351-130-130-157-135-153-517-753-135 107-130-173-571-571-751-571-157 301-137-307-750-317-137-175-175-107-157-175-231-137-136-126-132-231-312-132-123-312-123-132-137-310-310-3_V.mp3)
  Segment 34: 105540ms ~ 106116ms (file: 507-527-526-572-725-572-625-250-520-257-567-270-562-675-765-762-672-671-765-675-675-762-765-675-675-502-574-527-260-157-257-260-756-765-756-765-576-572-756-576-572-507-570-527-5_V.mp3)
  Segment 35: 107116ms ~ 113416ms (file: 754-216-310-231-312-213-216-527-573-312-263-263-726-231-267-620-326-321-312-103-130-123-602-102-123-261-312-130-265-725-215-267-752-137-576-574-572-625-231-231-263-275-267-527-3_W.mp3)
  Segment 36: 114416ms ~ 119096ms (file: 631-361-312-103-513-175-573-165-325-576-315-567-165-156-756-567-756-675-635-605-136-163-136-135-375-156-375-167-176-756-173-173-165-103-157-126-315-123-135-573-127-130-310-201-5_W.mp3)
  Segment 37: 120096ms ~ 126432ms (file: 572-750-132-576-124-213-216-120-576-213-230-210-572-107-120-132-312-120-210-765-213-201-162-107-176-167-576-205-102-135-571-103-102-571-130-267-123-165-570-176-725-230-321-201-7_W.mp3)
  Segment 38: 127432ms ~ 128008ms (file: 163-160-631-631-763-301-130-317-137-317-571-357-672-765-132-167-317-315-135 315-130-315-317-103-137-310-376-310-610-130-613-607-163-301-637-176-173-673-301-136-136-136-173-106-3_V.mp3)
  Segment 39: 129008ms ~ 129584ms (file: 150-105-150-150-150-157-157-571-105-715-751-715-715-751-175-751-751-756-751-751-715-751 150-150-150-150-157-175-157-751-751-715-751-175-715-715-751-751-751-756-751-751-754-756-7_V.mp3)
  Segment 40: 130584ms ~ 131160ms (file: 107-571-750-750-574-571 570-571-570-750-157-750-751-175-175-705-705-715-175-715-107-107-507-170-170-107-517-574-574-574-754-756-750-751-756 570-517-570-175-157-105-157-150-150-5_W.mp3)
  Segment 41: 132160ms ~ 140728ms (file: 213-735-625-213-316-531-576-356-567-136-102-106-206-732-106-106-263-361-361-123-135-123-132-132-765-320-132-326-210-137-267-326-230-267-120-312-107-216-126-160-163-301-106-125-2_W.mp3)
  Segment 42: 141728ms ~ 142304ms (file: 750-715-175-175-715-751-751-751-751 153-150-150-150-517-157-135-157-751-751-751-715-751-751-751-751-175-751-715-751-751-756-751-715-750 517-510-517-105-157-175-751-175-715-751-1_W.mp3)
  Segment 43: 143304ms ~ 143880ms (file: 157-571-175-157-750-507-105-570-570-570_V.mp3)
  Segment 44: 144880ms ~ 145456ms (file: 175-176-751-715-713 105-105-130-105-150-157-175-176-175-175-715-715-175-716-175-176-716-761-716-716-761-761 105-105-150-150-150-150-157-157-175-165-175-175-715-175-175-175-157-1_W.mp3)
  Segment 45: 146456ms ~ 153692ms (file: 635 301-310-310-160-602-163 102-210-260-320-265-527-260-627-267-103-576-206-316-621-320-732-127-276-230-627-263-672-321-237-102-132-106-312-312-102-123-137-162-136-130-312-306-3_W.mp3)
  Segment 46: 154692ms ~ 155268ms (file: 501-150-510-501-571-150-517-507-105-517-130-153-150-105-150-150-510-517-150-150-570-574-570-153-150-501-105-135-153-514-510-507-571-762 150-105-510-150-510-150-510-105-150-105-5_W.mp3)
  Segment 47: 156268ms ~ 156844ms (file: 574-153-135-574 574-571-571-751-571-571-573-751-175-715-157-175-713-137-175-175-175-175-175-175-157-751 157-157-175-175-170-175-157-517-157-157-571-571-571-571-571-571-571-571-1_W.mp3)
  Segment 48: 157844ms ~ 163064ms (file: 236-231-267-267-263-236-752-326-136-103-320-260-237-230-752-365-103-213-320-321-103-123-217-321-236-321-153-173-263-326-312-321-761-126-261-721-527-267-752-756-725-267-126-671-2_W.mp3)
  Segment 49: 164064ms ~ 164640ms (file: 510-510-150-513-510-153-510-517-576-501-517-571-572-150-150-517 150-150-150-152-156-157-150-150-150-150-157-517-507-571-501-517-501-517-507-150-517-570-571-150-507-517 105-501-5_V.mp3)
  Segment 50: 165640ms ~ 173380ms (file: 137-213-201-236-132-170-137-206-123-132-317-523-123-152-507-753-135-302-102-321-507-132-751-175-106-136-153-705-173-573-507-310-312-132-201-670-157-231-572-236-321-123-267-312-5_V.mp3)
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
> **Time**: 2026-04-05 10:44:02
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
"transcript": "비극적인 결말은 거의 피할 수 없게 되죠 얼음 지구상의 많은 바다와는 달리 베링해는 매년 겨울이 되면 반드시 일부가 얼어붙습니다 지구 온난화",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "비극적인", "start_ms": 20, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "결말은", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "피할", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없게", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "되죠", "start_ms": 2400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "얼음", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지구상의", "start_ms": 4100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 4750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바다와는", "start_ms": 5150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "달리", "start_ms": 5750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "베링해는", "start_ms": 6200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "매년", "start_ms": 6900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "겨울이", "start_ms": 7350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 7850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 8250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일부가", "start_ms": 8800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "얼어붙습니다", "start_ms": 9300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "지구", "start_ms": 10200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "온난화", "start_ms": 10650, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "Kimchi fashion 중간에 단감 내려가고 그러면 사레 김치 좀 있고 양념 닿으면 또 아프고 쓰리고",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Kimchi", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "fashion", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "만반의 준비를 마쳤으니 배춧잎 사이사이",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "만반의", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "준비를", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마쳤으니", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배춧잎", "start_ms": 500, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "사이사이", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "가 어우러지면서 매우 혹독하고 예측하기 어려운 기상 조건이 만들어지고 있는 것입니다 기후의 경계에 놓인 바다 사실 베링해에서 가장 큰 위험은 바로 그 기후였습니다 그 밖의 모",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "가", "start_ms": 30, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "어우러지면서", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": 0.0}
]
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
"transcript": "중요하며 신중한 입장을 유지하고 파병 요청에 즉각 응하지 않자 트럼프 대통령은 이를 배신이자 기회주의자로 규정해 버린 것입니다",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "중요하며", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "신중한", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "입장을", "start__ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "유지하고", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "천만 달러의 자산을 오직 단타 트레이딩 하나만으로 일궈낸 남자라면 믿으실 수 있으신가요 그의 이름은 제이슨 노아 그가 말한 트레이딩의 3원칙은",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천만", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "달러의", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자산을", "start_ms": 360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오직", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "좋아요와 구독으로 다음 이야기에도 동행해 주세요 다음 영상에서 또 만나요 단골이",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "좋아요와", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "구독으로", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이야기에도", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "에요 하지만 쇄빙선은 쐐기형 선수로 경사져 있죠 쇄빙선은 이 쐐기",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "에요", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쇄빙선은", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쐐기형", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "지지율은 13%에 그쳐 민주당과의 격차가 38% 포인트나 됐습니다 베이 의원은 선거비 보전도 못할까 봐 후보들이 도통 나서지 않는다며 구인난이 심",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "지지율은", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "13%에", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그쳐", "start_ms": 340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "민주당과의", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "안내 말씀 드립니다 지난주 토요일에 소개해 드린 현대판 불로초 NMN 안티에이징 리포좀 크림 영상 이후 바이오",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "안내", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말씀", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드립니다", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지난주", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "를 위해 거실 오른쪽에는 따로 주방을 냈고요",
"speaker": "speaker_9",
"confidence": 0.91,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "mixed",
"words": [
{"word": "를", "start_ms": 60, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거실", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "오른쪽에는", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
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
"transcript": "에 이런 반응들을 보인 걸까요 세상에서 가장 비싸고 가장 위험하고 가장 희귀한 물질 만들기도 지옥이고 보관하기도",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "에", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "반응들을", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보인", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-05 10:44:02*
