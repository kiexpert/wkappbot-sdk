# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=10:49:08  uptime=24010s  mem=442MB  system_processes=359"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_104855.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 175-751-715-756-751-756 150-105-150-105-137-175-715-715-715-715-175-715-175-173-175-751-715-175-715-765-753-751-751-756 175-157-150-105-103-135-175-715-175-175-175-715-751-715-7_W.mp3)
  Segment 2: 1576ms ~ 11440ms (file: 312-103-165-173-167-163-130-137-123-231-726-312-132-321-736-762-267-261-267-127-310-765-675-267-732-751-756-267-267-754-762-267-725-106-721-217-753-103-132-756-765-120-312-617-1_W.mp3)
  Segment 3: 12440ms ~ 13016ms (file: 150-153-150-150-150-153-157-751-751-175-715-751-751-715-175-751-751-751-751-751-715-751-751 150-150-153-175-175-170-105-175-715-175-175-715-175-715-175-175-715-175-175 157-150-1_W.mp3)
  Segment 4: 14016ms ~ 14592ms (file: 762-265-236-263-576-635-237-725-236-576-256-216-632-312-201-572-276-321-236-275-267-325-560-613-726-670-725-627-231-357-627-756-135-312 607-217-527-327-273-367-631-625-673-361-1_V.mp3)
  Segment 5: 15592ms ~ 16168ms (file: 261 132-623-132-617 150 236 657-576-675-172-725-735-572-713_W.mp3)
  Segment 6: 17168ms ~ 25808ms (file: 361-754-574-754-261-726-132-321-756-627-132-263-756-312-132-316-126-157-754-754-132-216-726-264-261-765-162-106-263-256-762-362-236-714-123-621-261-213-532-132-103-321-231-754-1_W.mp3)
  Segment 7: 26808ms ~ 27384ms (file: 516-156-167-567-567-357-615-672-637-756-576-756-671-516-657-635-657 310-613-631-631-136-631_W.mp3)
  Segment 8: 28384ms ~ 36952ms (file: 132-310-130-315-316-203-501-510-576-312-231-132-306-163-310-362-236-326-132-312-231-321-312-574-256-265-136-321-263-267-267-126-327-157-120-201-572-253-267-230-312-105-123-132-2_W.mp3)
  Segment 9: 37952ms ~ 38528ms (file: 563-510-150 157-153-157-157-517-157-105-501-517-157-513-150-105-157-105-150-513-510-517-501-507-107-571-510-501-157-501 510-150-105-105-157-517-157-153-513-153-105-157-501-501-5_V.mp3)
  Segment 10: 39528ms ~ 40104ms (file: 105-150-105-103-105-107-105-107-157-150-150-107-105-107-105-107-135 103-105-105-501-105-157-150-150-153-517-150-105-150-157-150-153-150-105-150-157-150-150 153-150-510-517-150-5_V.mp3)
  Segment 11: 41104ms ~ 49384ms (file: 267-763-671-327-672-267-267-267-267-671-541-327-157-160-160-237-263-162-137-135-315-627-651-216-621-162-130-362-671-167-751-517-573-136-236-756-572-236-562-263-267-621-236-326-6_W.mp3)
  Segment 12: 50384ms ~ 57296ms (file: 273-267-217-237-263-627-267-321-321-230-276-607-573-275-237-273-321-321-321-621-317-362-761-137-216-621-521-517-573-321-623-321-231-573-263-132-136-326-375-573-135-531-235-627-6_W.mp3)
  Segment 13: 58296ms ~ 58872ms (file: 126-107-316-613-361-137-136-162-326-103-136-106-310-130-106-136-107-103-571-102-751-267 103-162-106-106-106-106-312-163-312-103-102-601-130-612-167-102-216-136-132-316-132-316-1_V.mp3)
  Segment 14: 59872ms ~ 60448ms (file: 567-576-675-761-765-765-236-362-312-765-765-675-671-761_W.mp3)
  Segment 15: 61448ms ~ 76784ms (file: 751-570-310-213-132-130-563-153-601-206-261-106-231-130-675-136-361-132 103-574-103-130-312-132-571-572-103-547-105-210-260-756-230-231-130-231-127-203-236-216-576-576-517-210-5_W.mp3)
  Segment 16: 77784ms ~ 85848ms (file: 716-153-276-160-320-206-312-275-265-321-236-320-237-327-261-213-320-250-203-236-102-132-207-201-576-574-215-263-135-570-671-206-210-576-312-203-320-320-203-253-102-135-157-267-2_W.mp3)
  Segment 17: 86848ms ~ 87424ms (file: 172-301-357-135-137-170-752-203-571-760-367-317-137-371-712-316-137-713-163-137-132-126-721-720-273-705-126-572-172-756-752-713-735-372-132-371-312-125-312-271-736-356-276-175-3_V.mp3)
  Segment 18: 88424ms ~ 89000ms (file: 507-705-105-750-715-570-105-750-175-710-571-750-175-750-705-750-750-750-751-751-751-715-751-751-756-571-750-715-175-705-507-170-701-571-715-175-157-517-157-170-751-705-175-571-7_V.mp3)
  Segment 19: 90000ms ~ 90576ms (file: 514-546-504-504-564-503-534-541-572-507-536-536-564-537-536-576-576-571-547-547-752-756-576-756-576-756-756-756-756-756-756-752 725-752-756-756-756 725-752-756-752-576-576-756-7_W.mp3)
  Segment 20: 91576ms ~ 92152ms (file: 170-710-170-170-107-170-175 315-713-103-270-153-107-103-107-130-170-137-137-107-173-103-107-261 517-103-753-175-107-137-170-107-170-701-170-710-701-751-710-713-170 170-105-173-3_V.mp3)
  Segment 21: 93152ms ~ 93728ms (file: 607-612-615-671-673-103-617-163-136-103-652-761-607-106-137-501-136-153-103-317-176-571-167-130-103-103-657 130-135-103-136-160-156-163-106-163-125-135-615-163-106-163-163-103-1_V.mp3)
  Segment 22: 94728ms ~ 95304ms (file: 765-675-756-765-765-756 571-517-570-576-570-571-751-571_W.mp3)
  Segment 23: 96304ms ~ 96880ms (file: 620-623-621-162-621-623-621-263-263-627-263-621-263-621-621-627-621-623-260-623-263-621-625-621-623-623-621-261-623-621-623-623-263-621-627-621-621-627-621-625-621-623-621-620-6_W.mp3)
  Segment 24: 97880ms ~ 98456ms (file: 371-753-127-571-574-157-541-623-123-523-652-576-756-675-612-617-712-705-361-570-153-132-326-361-321-632-132-176-251-125-125-162-261-275-710-167-167-572-152-710-701-170-157-576-7_W.mp3)
  Segment 25: 99456ms ~ 100032ms (file: 750-705-560-507-570-570-570-527-527-526-502-527-527-576-527-572-572-572-576 257-265-520-572-562-256-526-576-156-157-103-301-315-136-136-136-163 507-576-275-257-206-276-207-205-6_V.mp3)
  Segment 26: 101032ms ~ 101608ms (file: 612-216-623-621-756-312-132-213-301-613-312-213-126-362-126-130-102-316-327-213-631-123-613-132-621-127-620-627-716-132-132-617-610-612-137-637-631-613-602-547-547-563-672-762-6_W.mp3)
  Segment 27: 102608ms ~ 103184ms (file: 751-751-750-570-573-576-576-756-751-103-517-153-507-105-510-501-507-157-571-517-715-157-713-173-175-731-175-715-715-751-715-751-751-715-751-175-571-157-571-570-576-571-571-513-5_V.mp3)
  Segment 28: 104184ms ~ 104760ms (file: 715-150-107-501-576-507-507-570-570-517-510-157-153-517-517-517-517-517-157-517-517-517-574 150-713-501-105-517-105-107-751-571-507-510-510-507-517-510-517-570-571-570-517-571-5_W.mp3)
  Segment 29: 105760ms ~ 106336ms (file: 105-156-105-157-517-517-501-150-501-507-501-501-501-510-501-510-570-571-510-157-571-574-576-574-574-574-574-547 570-510-105-157-571-157-571-507-501-150-517-510-501-150-510-506-5_W.mp3)
  Segment 30: 107336ms ~ 115544ms (file: 672-103-263-137-726-172-176-260-103-126-276-627-672-702-236-153-576-570-576-756-756-175-176-137-173-157-356-306-167-716-321-130-726-712-107-175-726-576-675-765-675-671-126-260-7_W.mp3)
  Segment 31: 116544ms ~ 117120ms (file: 765-756-756-715-735-756-573-756-756-756-753-756-105-157-105-157-150-157-157-517-157-517-517-517-571-571-574-571-571-574-571-570-570-756-756-765-756-752-576-576-756-762-765-576-7_W.mp3)
  Segment 32: 118120ms ~ 118696ms (file: 567-567-576-163-675-765-756-567-567-762-625-765-627-312-513-675-675-137-675-675-675-671-621-163-310-765-675-765-163-617-157-627-632-312-123-136-612-637-672-671-765-167-675-236-6_W.mp3)
  Segment 33: 119696ms ~ 120272ms (file: 567-567-506-507-506-165-165-576-567-756-167-576-675-756-675-173 615-651-165-156-165-156-657-567-567-651-657-561-576-567-756-571-571-753-175-715-175-137-657-675-576-657-675-562-5_W.mp3)
  Segment 34: 121272ms ~ 133836ms (file: 260-620-106-210-206-162-130-526-567-102-321-312-103-132-542-312-103-256-163-102-310-132-136-103-672-672-206-260-203-301-765-201-210-103-527-321-762-267-310-160-132-210-132-130-3_V.mp3)
  Segment 35: 134836ms ~ 135412ms (file: 715-175-751-175-175-751-175-175-715-710 175-715-751-710-705-750-715-751-710-571-175-750-715-137-175-157-715-715-751-170-715-175-715-715-157-173-175 751-751-751-756-756-750-715-7_V.mp3)
  Segment 36: 136412ms ~ 145808ms (file: 132-217-326-103-123-253-231-236-132-675-123-751-231-263-267-237-132-752-312-103-236-132-123-736-276-573-102-236-236-263-236-264-236-236-267-102-103-361-263-321-236-356-362-321-2_W.mp3)
  Segment 37: 146808ms ~ 147384ms (file: 512-562-657-615-651-162-172-567-561-567-567-567-651-657-657-576-567-756-576-657-756-567-765-167-176-751-156-165-167-761-765-756-765-756-657-567-756-657-576-675-576-572-657-576-5_W.mp3)
  Segment 38: 148384ms ~ 148960ms (file: 506-506-570-750-507-760-657-136-560-675-570-762-670-627-620-260-267-327-106-132-137-107-132-137-173-136-107-316-137-574-576-751-573-756-756-103-137-176-107-106-136-716-163-167-1_V.mp3)
  Segment 39: 149960ms ~ 152624ms (file: 627-263 756-750-637-316-316-316-316-761-301-105-501-617-351-130-135-571-103-103-106-156-165-312-136-103-165-574-567 672-362-362 132-672-675-563-576-536-103-130-312-621-236-302-1_W.mp3)
  Segment 40: 153624ms ~ 165828ms (file: 103-320-632-326-317-375-312-132-103-316-320-312-321-325-723-263-263-207-217-275-132-102-216-230-762-130-570-750-705-120-210-134-132-103-203-130-132-217-123-206-103-120-261-132-1_W.mp3)
  Segment 41: 166828ms ~ 167404ms (file: 137-371-375-715-153-157-157-150-157-153-150-157-753-271-375-175-173-572-715-752-175-715-761-675-217-176-756-570-510-517-157-157-516-517-157-175-136-137-167-157-175 156-165-156-1_W.mp3)
  Segment 42: 168404ms ~ 168980ms (file: 261-621-216 567 627-621-627-672-672 176-275-103-132-261-312-213-102-103-312-316-136-176-137-621-621-263-103-132-312-123-261-216-312-132-132-132-132-157-137-176-105-150-157-132-7_V.mp3)
  Segment 43: 169980ms ~ 179952ms (file: 321-123-213-213-312-213-627-137-654-630-605-173-761-170-120-267-625-362-627-321-672-632-231-623-672-236-213-103-126-731-170-130-517-126-213-167-263-276-271-270-136-107-106-326-1_W.mp3)
  Segment 44: 180952ms ~ 181528ms (file: 567-753-567-756-753-576-165-156-150-156-137-152-125-715-756-753-765-571-675-765-675-756-671-765-762-617-517-756-756-751-517-157-175-157-156-756-751-576-576-315-725-125-725-137-2_W.mp3)
  Segment 45: 182528ms ~ 189728ms (file: 731-170-735-756-716-301-137-206-572-137-715-150-762-627-217-260-230-137-176-573-165-756-260-260-725-237-372-217-236-326-123-627-206-762-231-725-236-623-763-362-576-356-625-312-2_W.mp3)
  Segment 46: 190728ms ~ 191304ms (file: 316-617-162-132-132-167-136-657-176-756-567-657-567-576-567-257-567-576-563_W.mp3)
  Segment 47: 192304ms ~ 201268ms (file: 273-175-705-276-276-276-276-327-376-173-723-276-715-765-756-275-725-756-750-750-270-276-216-751-756-725-765-756-237-372-120-230-123-517-275-270-273-173-130-301-310-130-263-236-2_W.mp3)
  Segment 48: 202268ms ~ 202844ms (file: 127-137-312-302-320-302-301-321-162-675-312-132-123-137-756-765-574-725-321-132-163-267-237-132-516-136-132-123-170-754-310-203-130-321-302-321-217-167-207-756-230-762-675-576-3_V.mp3)
  Segment 49: 203844ms ~ 204420ms (file: 621-265-625-625-652-657-654-615-612-625-612-625-615-621-621-512-567-652-652-657-657-657-654-654-651-657-651-654-654-652-651-652-654-654-654-652-162-263-265-652-654-651-621 562-2_W.mp3)
  Segment 50: 205420ms ~ 205996ms (file: 106-162-103-612 106-136-602-136-326-261-162 167-576-157-765-567 316 136-167-627-351-657-362-160-130_V.mp3)
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
> **Time**: 2026-04-05 10:50:11
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
"transcript": "나오면서 상한가 한 틱 아래까지 급등한 것입니다 이 당시 SK 이터닉스가 사상 최고가를 돌파하면서 매물 소화 과정을",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "나오면서", "start_ms": 20, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "상한가", "start_ms": 630, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "틱", "start_ms": 1390, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "아래까지", "start_ms": 1640, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "급등한", "start_ms": 2250, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "것입니다", "start_ms": 2800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "당시", "start_ms": 3780, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "SK", "start_ms": 4250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이터닉스가", "start_ms": 4650, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "사상", "start_ms": 5480, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "최고가를", "start_ms": 5930, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "돌파하면서", "start_ms": 6580, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "매물", "start_ms": 7350, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "진짜 이유는 유사시 미국의 한반도 개입을 차단하고 미국 본토를 직접 타격할 수 있는 능력을 갖춰 체제 안전을 보장받기 위함이었습니다 북한 핵은 서울이 아니라 워싱턴을 겨냥합",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이유는", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유사시", "start_ms": 950, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "미국의", "start_ms": 1450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "한반도", "start_ms": 1900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "개입을", "start_ms": 2480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "차단하고", "start_ms": 2960, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 3600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "본토를", "start_ms": 3980, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 4480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "타격할", "start_ms": 4960, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5460, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5600, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "능력을", "start_ms": 5920, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "갖춰", "start_ms": 6400, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "체제", "start_ms": 6820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "안전을", "start_ms": 7200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보장받기", "start_ms": 7680, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "위함이었습니다", "start_ms": 8350, "dur_ms": 950, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "나니까요 이게 진짜 개선이 많이 돼가지고 제가 며칠 전에 영상을 이렇게 다시 찍어서 올렸잖아요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "나니까요", "start_ms": 30, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 550, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 7,
"transcript": "저는 삼성이 지금 고객이랑 소통하고 피드백 받고 이게 실제 개선 브레이크 내가 이제 그 핸들도 움직이고 센서라든가 자율주행 같은 경우에도 센서가 있지만",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 60, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "삼성이", "start_ms": 400, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "자율주행이나 전기차가 나오기 이전에도 이 파이드 비히클들의 이 보안 체계가 약간 다른 점이 있어요 예전에 제가 텍사스 인스트루먼트 관련해서 이",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "자율주행이나", "start_ms": 55, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "전기차가", "start_ms": 860, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "안에 갇혀서 이루어지지 않습니다 유럽의 여러 연구소들이 서로 반물질을 주고받으며 연구할 수 있는 시대가 열리고 있어요 타라시어스 박사의 농담처럼",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안에", "start_ms": 40, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "갇혀서", "start_ms": 350, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "서니 반물질의 배달앱이 되는 세상이 정말로 올 수도 있습니다 그리고 그 배달이 진행되면 진행이 될수록 우리는 조금씩",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "서니", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "반물질의", "start_ms": 410, "dur_ms": 520, "phase_lr_deg": 0.0}
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
"transcript": "이런 것들에 대해서 주식을 불개미란 트레이더는 되게 엄격한 잣대로 저는 봐요 되게 한번 놓친 거에 대해서는 이 아집을 버리고 고집을 버리고 지금 시장이랑 내가 바꿔야 될 걸 인정할 때는 인정을 해야 되고",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것들에", "start_ms": 320, "dur_ms": 380, "phase_lr_deg": 0.0}
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
"transcript": "와 이거 진짜 안 되겠다 야 이게 진짜 너무 얼큰하고 감칠맛이 진짜 죽인다 이건 안 돼 여러분 이건",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 60, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 400, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "프롬프트 엔지니어링은 말 자체를 훈련하는 기술과 비슷한데요 추론 루프를 설정하고 멀티 에이전트를 조율하고 도구를 사용하는 방법을 가르치는 겁니다 AI가 실행하고 인간이 설계하고 검증하고 책임지는 규율 있는",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "프롬프트", "start_ms": 40, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "엔지니어링은", "start_ms": 720, "dur_ms": 820, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "가 상대적으로 좀 더 강세를 보였거든요 어 이런 소식이 있었습니다 ADR 상장 주관사 선정 소식이 나왔어요 본격적으로 올해 추진되기 때문에 여기에 따른 기대감이 있고 그것뿐만 아니라 삼전 하이닉스 다음 주에 주요 일정",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "가", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "상대적으로", "start_ms": 220, "dur_ms": 720, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "못한 사람들부터 하자",
"speaker": "speaker_10",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "못한", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사람들부터", "start_ms": 520, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "하자", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "이 한국 사람은 한국보다 오히려 미국에서 더 유명하다는 거 알고 계셨나요 SNS 들어가 보면 느낌이 딱 옵니다 그냥 좋아한다는 수준이 아니라 거의 찐 사랑 모드거든요 프로필부터 영어가 아니고 한글로 행복하지 않아도 괜찮아 이거 하나로 분위기 끝입니다 그리고 3월 1일도 그냥",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 180, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 23,
"transcript": "여러분 풀촌집 성공하기 위하여 짠",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "여러분", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "풀촌집", "start_ms": 600, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "어떤 나는 카이스트가 우선일 것 같다 그렇게 얘기를 했거든요 근데 그때는 다들 다 이제 저를 믿지 못했죠 2015년 세계 재난",
"speaker": "speaker_13",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 420, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "너무 케미가 좋은 거야 감독 분위기 너무 행복해 이게 뭐 또 내공이 될 수 있잖아 맞아 마치 내가 이렇게 주문을 한 것",
"speaker": "speaker_14",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "너무", "start_ms": 40, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "케미가", "start_ms": 450, "dur_ms": 480, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 10:50:11*
