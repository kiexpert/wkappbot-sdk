# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=06:38:15  uptime=9231s  mem=472MB  system_processes=351"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_063437.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 135-132-312-132-312-135-136-670-607-203-263-312-103-756-301-130-103-315-730-103-136-510-321-301-603-623-630-673-602-261-206-236-203-203-236-203-230-302-321-230-326-213-312-213-7_V.mp3)
  Segment 2: 1576ms ~ 9928ms (file: 576-201-132-132-213-574-756-103-756-574-132-176-267-163-167-136-130-573-265-130-103-103-754-756-576-620-167-674-130-103-160-123-130-132-261-261-123-136-316-236-574-263-236-316-2_W.mp3)
  Segment 3: 10928ms ~ 24320ms (file: 301-103-130-163-135-103-106-135-765-176-765-765-637-672-751-570-135-312-315-301-310-103-513-573-571 164-376-103-523-231-201-231-102-102-123-201-230-236-120-201-231-213-201-320-2_W.mp3)
  Segment 4: 25320ms ~ 39108ms (file: 263-213-312-130-136-547-356-310-103-216-523-210-130-103-126-126-236-361-127-236-267-260-126-321-327-162-730-321-301-731-103-362-130-102-271-301-120-127-120-263-120-762-163-132-3_V.mp3)
  Segment 5: 40108ms ~ 47992ms (file: 267-261-107-574-263-627-267-765-123-753-213-106-275-267-231-263-576-576-256-312-261-267-623-106-617-261-260-236-321-123-267-765-216-173-103-672-672-103-572-132 132-132-265-253-5_W.mp3)
  Segment 6: 48992ms ~ 63284ms (file: 703-312-260-361-257-623-627-750-673-527-276-531-137 576-576-725-312-750-750-620-576-507-267-236-507-107-321-762-261-607-321 267-276-705-326-567-560-312-321-627-231-760-670-321-2_V.mp3)
  Segment 7: 64284ms ~ 64860ms (file: 537-153-153-153-517-517-571-157-513 153-137-153-153-130-157-153-135-315-361-135-315-135-537-513-135-351-573-571-517-537-531-537-517 135-130-153-135-153-157-137-135-175-135-103-1_W.mp3)
  Segment 8: 65860ms ~ 74176ms (file: 173-103-321-231-230-132-627-257-216-123-267-127-120-761-102-102-267-126-176-236-321-230-257-524-236-273-127-102-127-102-102-671-671-107-176-267-267-102-756-206-231-263-236-326-3_W.mp3)
  Segment 9: 75176ms ~ 83600ms (file: 231-675-106-163-136-267-267-756-310-132-326-237-627-632-613-137-756-765-267-321-102-106-632-263-123-267-265-213-756-675-312-261-576-572-751-237-236-267-130-102-136-167-163-736-2_W.mp3)
  Segment 10: 84600ms ~ 85176ms (file: 750-751-750-715-750-751-705-750-751-705-750-705-751-750-750-751 570-754-750-570-751-570-751-750-570-570-750-750-570-715-750-751-750-574-570-750-754-754-751 574-574-754-754-756-7_W.mp3)
  Segment 11: 86176ms ~ 86752ms (file: 213-126 574-257-517 653-160-136-763-132-317-207-326-237-256-276-236-237-627-270-726-263-231-165-675-652-657-520-321-231-367-671-561-617-726-627-726-632-123-136-217-152-263-670-1_V.mp3)
  Segment 12: 87752ms ~ 101360ms (file: 216-237-103-231-210-216-216-267-260-250-127-250-572-261-213-230-265-256-201-231-267-260-527-130-267-210-276-206-627-261-267-261-217-276-267-301-312-251-257-231-203-321-213-321-2_W.mp3)
  Segment 13: 102360ms ~ 102936ms (file: 157-175-571-150-157 510-105-105-510-501-510-150-517-150-510-150-510-150-157-157-517 517-570-517-105-513-517-501-150-510-510-150-510-150-510-150-105-510-510-150-501-150-510-157 1_V.mp3)
  Segment 14: 103936ms ~ 119920ms (file: 261-257-256-216-206-201-760-574-572-213-504-625-260-132-130-215-267-627-206-276-103-231-230-206-627-267-756-275-132-512-103-103-765-103-102-326-206-752-765-143-132-130-103-316-2_W.mp3)
  Segment 15: 120920ms ~ 121496ms (file: 635-163-167-267-671-671-672-617-765-762-612-672-675-675-162-126 627-627-627_W.mp3)
  Segment 16: 122496ms ~ 123072ms (file: 716-567-546-547-564-165-675-617-137_W.mp3)
  Segment 17: 124072ms ~ 135016ms (file: 123-263-102-213-230-312-102-102-103-102-132-167-106-102-102-163-130-132-103-321-103-760-103-103-201-126-301-163-670-154-736-370-203-127-103-765-236-671-672-320-130-136-103-230-1_V.mp3)
  Segment 18: 136016ms ~ 141848ms (file: 107-316-103-231-137-103-261-263-260-162-126-267-675-216-136-102-125-132-103-103-301-670-201-103-123-213-132-261-231-126-513-213-213-231-321-231-265-123-321-321-312-132-312-130-3_W.mp3)
  Segment 19: 142848ms ~ 153468ms (file: 321-231-132-136-765-267-263-310-765-613-327-276-123-312-623-126-765-267-123-127-103-126-102-102-132-265-576-627-261-127-657-627-261-231-756-267-267-123-132-267-756-275-123-213-3_W.mp3)
  Segment 20: 154468ms ~ 162784ms (file: 126-213-120-102-602-126-231-103-102-617-167-132-362-156-526_W.mp3)
  Segment 21: 163784ms ~ 164360ms (file: 307-106-160-631-310-301-103-137-310-103-310-301-310-317-137-130-713-317-137-310-137 173-751-571-715-103-137-715-175-673-607-603-630-361-631-613-605-176-167-173-367-163-361-367-6_V.mp3)
  Segment 22: 165360ms ~ 165936ms (file: 126-136-162-361-126-132-163-103-160-563-136-103-157-160-163-106-136-103-136-103-132-613-106-152-167-136-312-761-103-103-136-162-106-130-136_V.mp3)
  Segment 23: 166936ms ~ 167512ms (file: 175-107-107-175-107-175-107-175-705-517-175-107-107-105-107-701 105-157-751-175-107-175-157-107-175-107-701-175-107-107-175-710-175-170-107-175-173_V.mp3)
  Segment 24: 168512ms ~ 175676ms (file: 170-267-267-267-761-216-236-236-132-351-316-316-136-631-213-107-721-712-126-361-716-267-203-203-623-213-362-132-132-153-102-726-261-672-213-102-162-302-167-162-617-612-217-310-2_V.mp3)
  Segment 25: 176676ms ~ 184344ms (file: 625-265-756-756-321-765-203-524-547-130-413-132-107-210-201-210-130-312-103-270-263-235-130-310-327-236-627-236-312-132-103-627-627-562-547-213-674-670-670-627-321-261-756-752-7_W.mp3)
  Segment 26: 185344ms ~ 185920ms (file: 735-173-702-752-675-756-765-567-526-265-126-236-361-316-672-132-153-137-132-312-603-625-263-302-135-135-312-312-213-627-620-136-760-756-765-136 361-136-126-213-173-103-103-123-1_W.mp3)
  Segment 27: 186920ms ~ 187496ms (file: 315-153-157-135-130-103-153-157-157-175-157 105-153-537-157-130-135-157-153-103-130-135-153-153-157-153-157-157-157-175-175-157-175-175-157-517-175 501-150-153-517-513-157-105-1_V.mp3)
  Segment 28: 188496ms ~ 189072ms (file: 176-716-137-713-316-127-137-310-132-312-123-123 750-571-750-715-137-137-137-316 751-137-130-137-173-321-213-312_W.mp3)
  Segment 29: 190072ms ~ 199612ms (file: 123-627-132-130-107-106-237-130-753-612-765-672-236-267-613-132-106-103-675-765-132-267-236-376-632-765-136-672-367-675-672-765-765-316-236-103-762-765-317-213-123-162-263-267-2_W.mp3)
  Segment 30: 200612ms ~ 201188ms (file: 103-175-310-137-107-103-137-317-310-173-137-715-137-137-371-103-137-173-105-175-301-107-132-173-732 135-315-137-103-317-137-731-375-135-173-317-137-173-513-103-317-137-371-170-1_V.mp3)
  Segment 31: 202188ms ~ 208740ms (file: 173-713-132-174-301-263-267-206-230-103-107-753-372-103-267-275-267-267-213-310-267-132-132-261-123-103-230-203-275-752-725-276-267-237-205-230-320-230-326-320-547-265-203-312-1_W.mp3)
  Segment 32: 209740ms ~ 228136ms (file: 132-312 715-371-173-123-132-710-126-610-216-751-731-231-173-572-213-163-371-317-126 136-326-163-326-163-753-267-173-761-173-137-731-362 126-236_W.mp3)
  Segment 33: 229136ms ~ 236876ms (file: 375-372-123-321-762-236-263-362-236-123-230-231-132-312-316-631-613-361-310-312-136-103-263-263-126-320-102-230-321-263-103-130-237-261-231-236-527-236-312-302-267-231-231-263-2_W_stt_but_we_do_in_and_out_into_a_worldwide_wh.mp3)
  Segment 34: 237876ms ~ 238452ms (file: 567-501-576-571-576-350-316-315-315-153 156-312-312-130-510-516-517-315-531-135-135-507-751-157-715-517-531-571-317-137-571-157 103-130-310-130-301-312-103-126-312-517-135-537-3_V.mp3)
  Segment 35: 239452ms ~ 240028ms (file: 123-236-236-327-726-623-726-762-263-267-236-263-126-267-261-265-256-631-267-326-752-213-123-321-231-326-123-312-162-602-672-736-672-726-725-273-231 672_W.mp3)
  Segment 36: 241028ms ~ 241604ms (file: 130-146-106-236-106-621-132-610-162-162-132-301-130-123-136-316-103-106-136-310-154-136-162-153-136-160-156-135-751-106-362-103-301-312-132-103-154-163-261-123-627-627-567-541-1_V.mp3)
  Segment 37: 242604ms ~ 243180ms (file: 135-153-135-173-135-137 105-105-153-750-570-153-573-517-501-317-135-135-135-317-130-135-153-351-317-315 105-153-105-310-315-571-150-135-150-153-107-150-130-315-135-137-135-135-1_V.mp3)
  Segment 38: 244180ms ~ 244756ms (file: 501-105-517-105-510-105-150-157-510-150-501-571-105-510-517-517-507-571-570-501-537 507-105-105-150-105-105-571-501-510-150-150-501-150-150-517-150-574-571-157-157-157-517-507-1_W.mp3)
  Segment 39: 245756ms ~ 246332ms (file: 573-572-572-571-517-153-627-263-263-327-321-231-713-721-517-527-526-627-657-725-132-257-521-152-235-231-257-352-253-123-257-572-573-571-572-537-325-263-623-572-572-527-527-576-2_W.mp3)
  Segment 40: 247332ms ~ 254172ms (file: 276-510-105-175-275-276-127-106-120-216-172-715-751-276-715-756-257-310-301-132-752-275-271-273-132-710-130-267-726-231-263-326-321-123-132-231-213-123-173-756-102-132-102-231-1_W.mp3)
  Segment 41: 255172ms ~ 255748ms (file: 150-105-510-715-105-150-157-571-751-175-751-175-715-137-713-751-715-715-715-175-103-105-105-107-137-107-173-107-107-175-103-107-751-761-715-751-716-175-105-105-103-107-175-107-1_V.mp3)
  Segment 42: 256748ms ~ 257324ms (file: 713-571-571-751-750-570-517-750-175-750-750-751-750-157-751-571-571-570 751-750-751-173-750-571-570-571-715-751-750-571-750-571-751-751-753-751-751-753-576 750-756-751-751-175-7_V.mp3)
  Segment 43: 258324ms ~ 258900ms (file: 652-675-625-675-265-657-672-625-657-675-675-652-657-675-652 657-657-650-652-657-657-652-675-657-657-675-657-672-657-675-625-652-675-672 567-657-625-627-657-765-675-657-675-657-6_W.mp3)
  Segment 44: 259900ms ~ 260476ms (file: 571-570-517-157-157-517-175-157-751-517-715-175-715-157-175-170-571-175 571-571-517-517-517-571-570-517-517-150-517-157-517-751-175-157-175-175-175-175-175-157-175-157-175-715 5_V.mp3)
  Segment 45: 261476ms ~ 262052ms (file: 137-632-631-162-517-765-756-316-176-675-107-172-610 573-173-176-271-123-576-237-760-136-163-217-753-362-325-237-752-261-237-367-673-725-657-321-203-263-627-527-730-765-507-736-2_W.mp3)
  Segment 46: 263052ms ~ 263628ms (file: 713-173 162-127-176-163-316-127-120-172-167-167-176-172-172-176-127-136-756-765-756-762-675-627-706-765-725-702-271-267-672-276-706-206-362-261-273-672-637-627-276-237-726-267-2_W.mp3)
  Segment 47: 264628ms ~ 265204ms (file: 162-316-312-231-236-106-301-310-301-253-176-261-672-320-362-310-362-657-675-306-263-312-123-106-362-621-263-620-263-326-236-623-652-162-621-627-625-625-612-216-567-567-571-627-1_W.mp3)
  Segment 48: 266204ms ~ 273224ms (file: 607-260-620-260-237-201-206-257-250-125-123-132-103-750-576-576-137-103-132-301-231-230-572-572-570-312-531-571-572-103-132-312-132-302-132-136-134-301-573-231-213-276-672-547-2_V.mp3)
  Segment 49: 274224ms ~ 274800ms (file: 163-162-236-163-302-123-132-167-612-316-326-321-163-123-132-136-123-123-162-132-165-137-631-312-162-361-132-613-316-132-312-312-126-136-320-132-231-312-316-126-106-632-326-123-3_W.mp3)
  Segment 50: 275800ms ~ 276376ms (file: 130-163-263-126-103-137-136-316-312-316-132-316-312-130-130-317-130-130-130-103-130-103-130-103-103-130-371-132-317-132-316-367-172-312-132-132-362-317-120-310-123-312 123-312-1_W.mp3)
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
> **Time**: 2026-04-05 06:40:43
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
"transcript": "안녕하세요 반갑습니다 오늘 수업 시작할게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 1400, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 2600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "수업", "start_ms": 3300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "시작할게요", "start_ms": 4100, "dur_ms": 1500, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "먼저 지난 시간에 배운 문법들을 복습해 보겠습니다 다들 기억하시나요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 220, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "지난", "start_ms": 1100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 1900, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "문법들을", "start_ms": 3600, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "복습해", "start_ms": 4900, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "보겠습니다", "start_ms": 6000, "dur_ms": 1400, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 7600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "기억하시나요", "start_ms": 8400, "dur_ms": 1800, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "동사의 활용과 격조사에 대해서 질문 있으신 분 계신가요 자유롭게 말씀해 주세요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "동사의", "start_ms": 180, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "활용과", "start_ms": 1200, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "격조사에", "start_ms": 2300, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "대해서", "start_ms": 3600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "질문", "start_ms": 4500, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "있으신", "start_ms": 5300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "분", "start_ms": 6300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "계신가요", "start_ms": 6800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "자유롭게", "start_ms": 8200, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "말씀해", "start_ms": 9500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 10500, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "네 선생님 저는 아직 을과 를의 구분이 조금 헷갈려요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "선생님", "start_ms": 800, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "저는", "start_ms": 1700, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "아직", "start_ms": 2400, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "을과", "start_ms": 3200, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "를의", "start_ms": 4100, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "구분이", "start_ms": 4800, "dur_ms": 900, "phase_lr_deg": -5.0},
{"word": "조금", "start_ms": 5800, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "헷갈려요", "start_ms": 6600, "dur_ms": 1200, "phase_lr_deg": -5.0}
]
},
{
"segment": 6,
"transcript": "아 그건 받침의 유무를 확인하면 됩니다 받침이 있으면 을 없으면 를을 써요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그건", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "받침의", "start_ms": 1300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "유무를", "start_ms": 2200, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "확인하면", "start_ms": 3200, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 4400, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "받침이", "start_ms": 5500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 6400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "을", "start_ms": 7300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "없으면", "start_ms": 7800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "를을", "start_ms": 8800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "써요", "start_ms": 9400, "dur_ms": 700, "phase_lr_deg": 0.0}
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
"transcript": "예를 들어 수박은 받침이 있으니까 수박을이 되고 사과는 없으니까 사과를이 되죠",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "예를", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "들어", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "수박은", "start_ms": 1400, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "받침이", "start_ms": 2400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "있으니까", "start_ms": 3300, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "수박을이", "start_ms": 4400, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 5600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "사과는", "start_ms": 6200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "없으니까", "start_ms": 7100, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "사과를이", "start_ms": 8100, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "되죠", "start_ms": 9200, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "Ah I see now It is much clearer than before Thank you teacher",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "Ah", "start_ms": 200, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "I", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "see", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "now", "start_ms": 1400, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "It", "start_ms": 2100, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 2500, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "much", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "clearer", "start_ms": 3600, "dur_ms": 900, "phase_lr_deg": -5.0},
{"word": "than", "start_ms": 4600, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "before", "start_ms": 5100, "dur_ms": 800, "phase_lr_deg": -5.0},
{"word": "Thank", "start_ms": 6200, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 6800, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "teacher", "start_ms": 7200, "dur_ms": 1000, "phase_lr_deg": -5.0}
]
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
"transcript": "천만에요 자 그럼 이제 교재 45페이지를 펴 볼까요 새로운 단어들이 나옵니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "천만에요", "start_ms": 150, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 2000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "교재", "start_ms": 3500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "45페이지를", "start_ms": 4400, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "펴", "start_ms": 6000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 6500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "새로운", "start_ms": 7800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "단어들이", "start_ms": 8800, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "나옵니다", "start_ms": 10000, "dur_ms": 1300, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "오늘의 주제는 여행입니다 해외여행을 가본 적이 있나요 있다면 어디를 가봤나요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "오늘의", "start_ms": 180, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "주제는", "start_ms": 1000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "여행입니다", "start_ms": 1900, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "해외여행을", "start_ms": 3500, "dur_ms": 1300, "phase_lr_deg": 0.0},
{"word": "가본", "start_ms": 4900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "적이", "start_ms": 5600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있나요", "start_ms": 6200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "있다면", "start_ms": 7500, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "어디를", "start_ms": 8400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가봤나요", "start_ms": 9200, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 17,
"transcript": "저는 작년에 일본에 다녀왔어요 도쿄와 오사카를 여행했는데 정말 재미있었어요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 250, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "작년에", "start_ms": 1000, "dur_ms": 800, "phase_lr_deg": 10.0},
{"word": "일본에", "start_ms": 1900, "dur_ms": 800, "phase_lr_deg": 10.0},
{"word": "다녀왔어요", "start_ms": 2800, "dur_ms": 1200, "phase_lr_deg": 10.0},
{"word": "도쿄와", "start_ms": 4400, "dur_ms": 800, "phase_lr_deg": 10.0},
{"word": "오사카를", "start_ms": 5300, "dur_ms": 1000, "phase_lr_deg": 10.0},
{"word": "여행했는데", "start_ms": 6400, "dur_ms": 1400, "phase_lr_deg": 10.0},
{"word": "정말", "start_ms": 7900, "dur_ms": 700, "phase_lr_deg": 10.0},
{"word": "재미있었어요", "start_ms": 8700, "dur_ms": 1500, "phase_lr_deg": 10.0}
]
},
{
"segment": 18,
"transcript": "오 일본 여행이라니 좋았겠네요 일본어로 음식 주문도 해봤나요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "오", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일본", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여행이라니", "start_ms": 1500, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "좋았겠네요", "start_ms": 2700, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "일본어로", "start_ms": 4000, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "음식", "start_ms": 5000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "주문도", "start_ms": 5700, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "해봤나요", "start_ms": 6600, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "네 간단한 일본어는 할 수 있어서 오이시데스라고 말했어요 다들 웃어주시더라고요",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 220, "dur_ms": 400, "phase_lr_deg": 10.0},
{"word": "간단한", "start_ms": 800, "dur_ms": 800, "phase_lr_deg": 10.0},
{"word": "일본어는", "start_ms": 1700, "dur_ms": 1000, "phase_lr_deg": 10.0},
{"word": "할", "start_ms": 2800, "dur_ms": 300, "phase_lr_deg": 10.0},
{"word": "수", "start_ms": 3200, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "있어서", "start_ms": 3500, "dur_ms": 800, "phase_lr_deg": 10.0},
{"word": "오이시데스라고", "start_ms": 4500, "dur_ms": 1500, "phase_lr_deg": 10.0},
{"word": "말했어요", "start_ms": 6100, "dur_ms": 900, "phase_lr_deg": 10.0},
{"word": "다들", "start_ms": 7400, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "웃어주시더라고요", "start_ms": 8100, "dur_ms": 1800, "phase_lr_deg": 10.0}
]
},
{
"segment": 20,
"transcript": "That is so cute Using the local language is a great way to connect",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 300, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "is", "start_ms": 800, "dur_ms": 200, "phase_lr_deg": -15.0},
{"word": "so", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": -15.0},
{"word": "cute", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": -15.0},
{"word": "Using", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": -15.0},
{"word": "the", "start_ms": 3200, "dur_ms": 200, "phase_lr_deg": -15.0},
{"word": "local", "start_ms": 3500, "dur_ms": 700, "phase_lr_deg": -15.0},
{"word": "language", "start_ms": 4300, "dur_ms": 900, "phase_lr_deg": -15.0},
{"word": "is", "start_ms": 5300, "dur_ms": 300, "phase_lr_deg": -15.0},
{"word": "a", "start_ms": 5700, "dur_ms": 200, "phase_lr_deg": -15.0},
{"word": "great", "start_ms": 6000, "dur_ms": 500, "phase_lr_deg": -15.0},
{"word": "way", "start_ms": 6600, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "to", "start_ms": 7100, "dur_ms": 200, "phase_lr_deg": -15.0},
{"word": "connect", "start_ms": 7400, "dur_ms": 800, "phase_lr_deg": -15.0}
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
"transcript": "맞아요 외국어를 배우는 가장 큰 즐거움 중 하나죠 자 그럼 다음 단어로 넘어가 볼게요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "외국어를", "start_ms": 1000, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "배우는", "start_ms": 2000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 3600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "즐거움", "start_ms": 4000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "중", "start_ms": 4900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하나죠", "start_ms": 5300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 6500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 7000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 7700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어로", "start_ms": 8400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "넘어가", "start_ms": 9300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "볼게요", "start_ms": 10300, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "숙소 예약하기입니다 호텔이나 게스트하우스를 예약할 때 쓰는 표현들을 배워볼 거예요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "숙소", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "예약하기입니다", "start_ms": 900, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "호텔이나", "start_ms": 2800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "게스트하우스를", "start_ms": 3800, "dur_ms": 1400, "phase_lr_deg": 0.0},
{"word": "예약할", "start_ms": 5300, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 6200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "쓰는", "start_ms": 6600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "표현들을", "start_ms": 7200, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "배워볼", "start_ms": 8400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 9300, "dur_ms": 800, "phase_lr_deg": 0.0}
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
"transcript": "먼저 빈 방 있나요라는 말은 방이 있어요라고 물어보면 됩니다 아주 쉽죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "빈", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "방", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있나요라는", "start_ms": 1800, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "말은", "start_ms": 3100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방이", "start_ms": 3900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있어요라고", "start_ms": 4500, "dur_ms": 1300, "phase_lr_deg": 0.0},
{"word": "물어보면", "start_ms": 5900, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 7000, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 8100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "쉽죠", "start_ms": 8800, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "그럼 하룻밤에 얼마예요는 어떻게 말하나요 가격을 물어보는 게 제일 중요하니까요",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 180, "dur_ms": 500, "phase_lr_deg": 20.0},
{"word": "하룻밤에", "start_ms": 800, "dur_ms": 900, "phase_lr_deg": 20.0},
{"word": "얼마예요는", "start_ms": 1800, "dur_ms": 1100, "phase_lr_deg": 20.0},
{"word": "어떻게", "start_ms": 3000, "dur_ms": 700, "phase_lr_deg": 20.0},
{"word": "말하나요", "start_ms": 3800, "dur_ms": 1000, "phase_lr_deg": 20.0},
{"word": "가격을", "start_ms": 5200, "dur_ms": 800, "phase_lr_deg": 20.0},
{"word": "물어보는", "start_ms": 6100, "dur_ms": 900, "phase_lr_deg": 20.0},
{"word": "게", "start_ms": 7100, "dur_ms": 300, "phase_lr_deg": 20.0},
{"word": "제일", "start_ms": 7500, "dur_ms": 600, "phase_lr_deg": 20.0},
{"word": "중요하니까요", "start_ms": 8200, "dur_ms": 1500, "phase_lr_deg": 20.0}
]
},
{
"segment": 32,
"transcript": "네 맞습니다 가격 질문은 필수죠 하룻밤에 얼마예요라고 하거나 일박에 얼마예요라고 합니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 220,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 220, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맞습니다", "start_ms": 800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "가격", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "질문은", "start_ms": 2600, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "필수죠", "start_ms": 3500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "하룻밤에", "start_ms": 4800, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "얼마예요라고", "start_ms": 5900, "dur_ms": 1400, "phase_lr_deg": 0.0},
{"word": "하거나", "start_ms": 7400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "일박에", "start_ms": 8200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "얼마예요라고", "start_ms": 9100, "dur_ms": 1400, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 10600, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "But we do in and out into a worldwide wh",
"speaker": "speaker_4",
"confidence": 0.85,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "But", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "we", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": -15.0},
{"word": "do", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "in", "start_ms": 1500, "dur_ms": 300, "phase_lr_deg": -15.0},
{"word": "and", "start_ms": 1900, "dur_ms": 400, "phase_lr_deg": -15.0},
{"word": "out", "start_ms": 2400, "dur_ms": 500, "phase_lr_deg": -15.0},
{"word": "into", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": -15.0},
{"word": "a", "start_ms": 3700, "dur_ms": 200, "phase_lr_deg": -15.0},
{"word": "worldwide", "start_ms": 4000, "dur_ms": 1000, "phase_lr_deg": -15.0},
{"word": "wh", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": -15.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "자 지금까지 배운 단어들로 간단한 역할극을 해볼까요 제가 호텔 직원 여러분이 손님입니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금까지", "start_ms": 600, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "단어들로", "start_ms": 2300, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "간단한", "start_ms": 3400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "역할극을", "start_ms": 4300, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "해볼까요", "start_ms": 5500, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 6700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "호텔", "start_ms": 7300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "직원", "start_ms": 8000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여러분이", "start_ms": 8800, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "손님입니다", "start_ms": 9800, "dur_ms": 1200, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "어서 오세요 무엇을 도와드릴까요 방이 필요하신가요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "어서", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오세요", "start_ms": 700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 1600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "도와드릴까요", "start_ms": 2300, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "방이", "start_ms": 3800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "필요하신가요", "start_ms": 4400, "dur_ms": 1100, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-05 06:40:43*
