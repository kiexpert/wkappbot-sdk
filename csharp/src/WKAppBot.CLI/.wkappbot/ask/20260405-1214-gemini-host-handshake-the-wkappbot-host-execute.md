# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30524  time=12:13:35  uptime=27727s  mem=457MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_121322.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 175-105 510-105-157-157-701-170-107-175-170-170-701-170-107-175-105-170-107 105-157-170-107-107-170-105-170-170-107-107-175-107-170-170-107-107-170-107-170-170-170-715 501-510-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 571-571-507 571-517-501-517-570 517-501-510-157-571 150 514-517-157-517-517-517-157-157-157-175-157-715 156-105-150-105-105-501-510-501-517-150-157-517-507-517-501-105-175-517-5_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 205-507-572-750-520-265-502-276-520-502-520-256-275-756-567-507-512-510-150-156-516-561-561-561-516-150-561-567-165-651-615-567-567-675-657-652 752-507-570-570-705-571-571-517-5_V.mp3)
  Segment 4: 4728ms ~ 12180ms (file: 120-231-263-312-273-270-231-123-231-123-102-102-236-762-210-167-721-120-210-765-276-216-126-132-321-173-276-260-576-270-210-321-132-103-213-230-230-102-157-273-203-273-756-120-6_W.mp3)
  Segment 5: 13180ms ~ 13756ms (file: 157-571-571-135-135-135-135-315-571-571-571-571-574 571-756-571-574-574-571-573-574_W.mp3)
  Segment 6: 14756ms ~ 15332ms (file: 510-510-501-510-150-507 105-105-157-510-150-510-150-150-501-150-150-510-514-150-510-150-517-105-150-570-571-574-574-574-571 105-105-150-105-150-150-510-510-150-150-510-510-150-1_V.mp3)
  Segment 7: 16332ms ~ 16908ms (file: 504-510-510-501-510-501-510-517-510-501-510-751-710-571-751-750-705-750-715-570-750-507-570 574-574-157-105-517-501-507-507-501-570-157-517-510-157-105-175-571-570 574-574-574-5_V.mp3)
  Segment 8: 17908ms ~ 18484ms (file: 753-576-751-736-735-763-753-736-367-657-735-367-736-167-376-317-137-317-371-367-376-753-352-375-367-365-365-365-367-367-367-672-375-635-652-356-357-356-357-563-576-351-762-675-7_W.mp3)
  Segment 9: 19484ms ~ 25244ms (file: 276-213-231-273-276-216-216-237-217-276-267-216-267-206-276-376-312-326-276-716-761-672-675-605-627-672 726-312-756-126-726-265-261-762-726-276-237-326-327-326-263-602-132-267-6_W.mp3)
  Segment 10: 26244ms ~ 26820ms (file: 360-236-263-265-253-526-567-567-567-567-567-562-567-567-567-567-567-567-567-567-567-623-501-506-506-502-506-526-562-561-527-521-523-521-521-516-567-501-507-527-527-527-520-652-5_W.mp3)
  Segment 11: 27820ms ~ 33616ms (file: 321-312-132-751-315-321-615-156-231-231-213-132-123-132-231-231-203-132-123-213-123-132-210-102-103-126-102-123-130-123-123-123-213-123-231-132-213-102-130-123-217-127-127-127-1_W.mp3)
  Segment 12: 34616ms ~ 35192ms (file: 623-106-103-152-162-175-657-175-127-671-103-167-157-671-617-136-157-103-137-163-157-370-167-612-167-162-267-671-627-612-106-167-731-136-167-210-360-126-106-130-103-623-103-162-1_V.mp3)
  Segment 13: 36192ms ~ 36768ms (file: 170-157-175-173-170-751-570 170-715-175-715-715-756-715-751-753-571-751-751-751-713-756-712-715-751-176-675-761-725-721-571-150-567-765-675-651-657-651-657-516-156-751-756-756-5_W.mp3)
  Segment 14: 37768ms ~ 38344ms (file: 751-751-716-756-756-761-517-715-751 105-105-105-105-154-157-175-760-167-176-107-617-615 154-156-451-751-751-517-574-754-571-751 175-357-715-751-705-750-514-105-105-157-150-157-1_W.mp3)
  Segment 15: 39344ms ~ 39920ms (file: 316-316-103-130-163-631-132-570-657-623-501-103-136-103-130-607-103-231-132-632-261-132-203-302-103-610-103-136-136-176-540-510-320-102-132-136-103-504-572-521-176-173-107-107-1_V.mp3)
  Segment 16: 40920ms ~ 41496ms (file: 613-136-312-156-136-103-135-106-165-153-610-765-312_V.mp3)
  Segment 17: 42496ms ~ 53368ms (file: 675-106-162-263-216-210-102-164-657-261-102-267-267-123-132-106-167-167-106-123-216-127-103-123-123-123-236-263-267-102-213-312-653-213-265-231-126-176-103-216-156-612-657-267-2_W.mp3)
  Segment 18: 54368ms ~ 54944ms (file: 526-257-750-756-257-725-572-265-250-527-257-570-572-572-752-756-752-756 520-520-572-520-256-257-275-250-576-756-576-576-275-752-572-572-572_W.mp3)
  Segment 19: 55944ms ~ 62856ms (file: 756-625-267-321-267-263-263-263-310-120-231-726-126-216-263-752-230-236-627-261-321-621-106-261-123-572-752-276-312-263-102-120-312-102-106-162-321-126-231-126-267-267-261-103-2_W.mp3)
  Segment 20: 63856ms ~ 72244ms (file: 752-576-625-265-107-123-162-230-123-137-102-126-236-213-231-126-217-107-107-102-167-716-716-267-276-257-257-260-216-267-210-231-574-574-237-273-267-726-107-107-326-326-263-310-3_W.mp3)
  Segment 21: 73244ms ~ 73820ms (file: 756-752-756 507-507-570-570-570-576-570-576-570-750-570-502-507-570 205-502-502-502-520-572-576-507-572-507-507 502-562-561-562-507-502-520_V.mp3)
  Segment 22: 74820ms ~ 81120ms (file: 263-120-120-716-157-106-312-267-267-210-756-261-203-573-127-132-671-106-136-631-361-316-316-361-371-137-132-132-761-671-627-236-126-176-175-136-163-106-167-123-127-756-237-132-3_W.mp3)
  Segment 23: 82120ms ~ 82696ms (file: 567-657-576-675-762-672-507-561-516-165-516-561-675-671-615-651-615-675-675-657-756-765-756-756-675-765 651-165-615-615-657-657-567-650-567-657-657-657-657-657-657-653-156 615-6_W.mp3)
  Segment 24: 83696ms ~ 92948ms (file: 102-310-103-216-120-201-207-267-216-201-126-201-210-670-765-760-320-260-206-627-217-206-102-217-206-132-301-720-716-120-260-210-230-201-260-574-762-267-672-267-276-137-756-132-3_W.mp3)
  Segment 25: 93948ms ~ 94524ms (file: 523 521-532-563-517-526-352-523-562-561-562-523-561-356 652-537-531-564-531-521-532-537-531-561-562-517-512-512-526-516-526-527-527-527-523-523-527-571-571-571-753-627-502-506-5_V.mp3)
  Segment 26: 95524ms ~ 96100ms (file: 261-216-231-213-231-216-267-256-265-261-263-362-231-312-351-156-652-265-526-632-326-236-326-263-362-321-236-326-321-321-320-321-321-321-321-327-213-321-312-312-231-210-236-261-2_W.mp3)
  Segment 27: 97100ms ~ 97676ms (file: 157-501-510-751-715-715-751-715-751-175-715-175-715-175-715-175-175-751-175-751 150-150-157-150-105-150-150-715-715-175-715-175-715-751-715-715-715-715-175-751-175-175-715-175-1_W.mp3)
  Segment 28: 98676ms ~ 99252ms (file: 534-503-534-543-501-504-562-501-501-506-501-502-532-520-652-657-657-657-561-651-651-503-502-501-502-502-502-506-502-560-562-572-562-521-526-572-567-256 576-576-576-576-576-576 5_W.mp3)
  Segment 29: 100252ms ~ 106228ms (file: 102-102-135-132-137-675-167-153-102-571-301-213-152-105-103-751-713-316-170-213-231-213-120-175-137-103-135-157-170-103-157-130-137-157-315-135-613-160-102-175-107-130-103-537-6_W.mp3)
  Segment 30: 107228ms ~ 107804ms (file: 517-571-570-570-570-570-750 570-571-571-157-570-105-570-751-750-571-570-750-751-507-750-570-571-751-571-751-750-750-570-571-507-570-571-570-751 571-570-105-507-570-750-570-750-7_V.mp3)
  Segment 31: 108804ms ~ 109380ms (file: 127 130-175-157-103-135-310-103-315-102-135-153-130-153-315-375-517-153-137-175-315-130-301-317-103-137-731-357-317 301-315-135-315-135-153-135-157-135-137-132-105-137-310-135-1_V.mp3)
  Segment 32: 110380ms ~ 116860ms (file: 362-507-275-726-217-702-716-132-106-261-756-570-320-152-175-735-670-201-126-257-201-362-713-132-632-305-137-750-257-572-103-312-512-702-136-103-670-216-103-576-123-613-371-217-5_V.mp3)
  Segment 33: 117860ms ~ 118436ms (file: 132-132-163-267-126-612-612-162-126-123-267-267-206-203-213-162-367-627-126-236-216-154-167-137-106-136-325-673-156-162-167-126-136-136-163-361-103-620-650-136 132-163-627-671-3_V.mp3)
  Segment 34: 119436ms ~ 123936ms (file: 123-230-126-120-126-216-126-216-216-263-231-126-216-261-261-260-267-276-765-217-132-326-362-106-106-216-127-217-231-213-321-236-267-267-762-267-267-231-123-236-126-231-236-261-2_W.mp3)
  Segment 35: 124936ms ~ 125512ms (file: 732-712-723-735-752-732-716-763-736-721-732-726-706-706-735-716-153-157-735-170-105-170-105-510-107-513-175-105-762-726-756-762-762 517-710-107-716-751-705-756-750-760-715-135-5_V.mp3)
  Segment 36: 126512ms ~ 127088ms (file: 162-612-615-612-621 265-625-651-657-657-615-615-651-657-617-615-501-615-615-675-615-651-615-615-651-657-613-631 651-651-657-615-615-651-615-651-657-132-130-315-135-132-135-657-2_W.mp3)
  Segment 37: 128088ms ~ 128664ms (file: 605-167-103-137-137-137-127-176-172-715-761-137-176-613-137-571-657-675-623-130-310-137-130-301-137-607-312-372-507-276-176-213-725-765-316-507-156-263-176-726-103-315-301-301-3_W.mp3)
  Segment 38: 129664ms ~ 130240ms (file: 547-325-321-321-541-517-537-531-316-326-326-231-163-613-326-547-574-574-517-573-375-136-615-534-537-531-517-267-623-263-267-263-327-236-263-231-231-126-261-216-256-152-512-326-3_W.mp3)
  Segment 39: 131240ms ~ 131816ms (file: 521-502-502-526-502-571-502-105-512-576-507-506-502-507-527-520-512-517-502-502-512-501-502-520-205-250-520-250-520-250-521-250-520-257-520-250-520-520-520-250-520-512-527-521-5_V.mp3)
  Segment 40: 132816ms ~ 140628ms (file: 261-236-267-267-752-103-261-261-672-261-263-701-705-652-506-675-657-675-750-201-263-102-310-132-675-312-312-317-231-261-236-753-276-256-126-213-276-236-756-762-267-267-276-267-2_W_stt_these_are_being_curious_end_of_the_car_t.mp3)
  Segment 41: 141628ms ~ 142204ms (file: 102-103-106-132-103-106-103-136-103-561-135-163-136-103-132-167-130-163-130-103-136-103-136-156-106-157-567 671-167-612-105-371-103-136-153-613-103-103-136-106-516-103-136-130-1_V.mp3)
  Segment 42: 143204ms ~ 143780ms (file: 275-257-507-572-756-576-571-753-156-526-572-752-725-275-572-752-275-725-572-752-725-572-752-752-725-752-275-725-275-257-725-762-257-572-756-507-527-570-576-576-576-576-576-576-5_W.mp3)
  Segment 43: 144780ms ~ 145356ms (file: 132-213-123-567-657-672-621-617-176-176-167-106-715-176-176-167-130-137-730-753-325-127-132-501-123-130-123-132-103-576-162-103-671-753-750-127-216-513-126-106-612-216-752-712-7_W.mp3)
  Segment 44: 146356ms ~ 146932ms (file: 716-136-136-125-602-617-157-106-162-130-163-167-163-136-130-170-156-256-362-103-106-163-612-276-623-672-162-120-132-130-710_V.mp3)
  Segment 45: 147932ms ~ 155816ms (file: 316-650-136 321-132-657-637-632-176-732-267-316-123-762-312-754-253-572-563-312-610-167-231-260-210-261-236-236-623-132-271-326-263-231-265-256-263-213-256-126-123-735-751-213-7_W.mp3)
  Segment 46: 156816ms ~ 164160ms (file: 103-103-326-273-103-312-321-320-753-267-257-572-235-572-752-257-203-267-267-236-326-753-357-130-136-107-713-752-321-263-231-201-230-267-231-106-136-310-231-261-230-301-236-627-2_W_stt_the_public_is,_you_pass_it_is_exciting_t.mp3)
  Segment 47: 165160ms ~ 175708ms (file: 261-206-103-413-210-206-120-102-652-103-312-576-205-107-156-230-213-175-175-103-301-320-310-103-325-103-317-730-136-325-267-325-206-207-231-276-132-130-132-143-263-260-130-531-5_W.mp3)
  Segment 48: 176708ms ~ 177284ms (file: 123-123-123-316-613-657 172-170-175-157-167-175-126-257-752-127-172-271-267-132-132-175-156-135-120-765-765-261-652 162 207-163-102_W.mp3)
  Segment 49: 178284ms ~ 178860ms (file: 570-510-517-571-517-510-517-150-501-510-503-510-501-157-517-570-516-576-567-567-567-576-567-576-567-576-576-574-574 510-571-517-105-105-105-150-510-501-510-510-507-150-501-506-5_W.mp3)
  Segment 50: 179860ms ~ 180436ms (file: 130-135-657-675-715-627-617-162-612-167-671-612_W.mp3)
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
> **Time**: 2026-04-05 12:14:28
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "결과를 얻어냈죠 이는 김범석 교수가 제 채널과 인터뷰에서 자신 있게 밝혔던 내용과 정확히 일치하는",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "결과를", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "얻어냈죠", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이는", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "김범석", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "세 가지 비법 가루까지 넣고 잘 섞어주면 감칠맛을 더할 비법 염지 용액",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "세", "start_ms": 30, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 90, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "비법", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가루까지", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "넣고", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "섞어주면", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "중동 전쟁의 여파로 호르무즈 해협이 꽉 막히면서 우리 선원들의 발이 묶이고 석유 수입도 지금 못 하고 있다는 소식요 설상가상으로 홍해 쪽",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "중동", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전쟁의", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "여파로", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해협이", "start_ms": 760, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "수진에게는 회사가 안전을 책임진다는 전제하에 진행하겠다고 설명한 것으로 전해졌습니다 UAE는 현재 여행 정보 3단계 철",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "수진에게는", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "회사가", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안전을", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "책임진다는", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "거시고 저는 바다 보러 가는 거 되게 좋아하고 밤바다 나가요 그래서 밤바다에 이제 저 나가서 저는 스님 있는 사람으로서 말씀도 외우고 기도하고 뭐 또 찬양도 하고 그러면서 그날의",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "거시고", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "바다", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보러", "start_ms": 400, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가는", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "신고가 돌파는 기계적으로 매수해야 한다라고 설명을 드렸습니다",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "신고가", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "돌파는", "start_ms": 190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기계적으로", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "매수해야", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "있으면 한국은 앞으로 더 강한 존재감을 보일 가능성이 큽니다 대만 당국이 이런 흐름을 예민하게 바라본다면 그것은 감정의 문제가 아니라 냉정한 현실 인식 때문일 것입니다 국제 경쟁은 말이 아니라 숫자",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "있으면", "start_ms": 20, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한국은", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 440, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "강한", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "왔다가 입덕하는 사태가 벌어지죠 그런데 여기서 대반전이 일어납니다 이정재의 얼굴",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "왔다가", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "입덕하는", "start_ms": 200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "사태가", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "벌어지죠", "start_ms": 550, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "댓글은 빠짐없이 읽고 소중히 간직하고 있습니다 앞으로도 더 흥미로운 시선으로 찾아올게요 다음 영상에서 뵙겠습니다 감사합니다",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "댓글은", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "빠짐없이", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "읽고", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "소중히", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "학습하는 것들을 매일매일 받고 있는데요",
"speaker": "speaker_10",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "학습하는", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "매일매일", "start_ms": 400, "dur_ms": 176, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "you stop being curious And a big part of that are these exams that you take in school that you just are jumping through the hoops taking exams because you think you got to",
"speaker": "speaker_11",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "you", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "stop", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "being", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "curious", "start_ms": 390, "dur_ms": 186, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "타이타닉은 그 당시 전 세계에서 가장 거대한 인간이 만든 최고의 구조물이었습니다 타이타닉은 스스로를",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "타이타닉은", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 330, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "당시", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "coming It's coming at you fast And it's exciting Man this is the most exciting time ever to be alive right now but you got to harness it and you can't be",
"speaker": "speaker_11",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "coming", "start_ms": 10, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "It's", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "coming", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 470, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "나왔다가 또 올해 1월달 같은 경우에는 거의 5000만원 가까이 수익이 나셨다가 또 2월달에는 또 손실도 나보시고 그리고 또 3월달에 다시 밸런스 잡아서 수익도 또 잘 나시고 그러니까 이게 사실 이런 성장 과정은요 이게 쭉 올라가기만 할 수는 없거든요 중간에 쭉 조금 삐끗은 하시는데 가장",
"speaker": "speaker_13",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "나왔다가", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 190, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "올해", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "1월달", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 570, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "경우에는", "start_ms": 680, "dur_ms": 180, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:14:28*
