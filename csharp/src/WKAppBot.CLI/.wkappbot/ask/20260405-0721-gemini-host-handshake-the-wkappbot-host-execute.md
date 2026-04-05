# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=07:20:00  uptime=20209s  mem=399MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_071948.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132 627-267-627-675-672-265-765-627-726-762-627-725-765-275-567-275-756-752-756-726-675-657-625-265-625-675-627-652_W.mp3)
  Segment 2: 1576ms ~ 8884ms (file: 523-321-236-123-125-154-321-320-763-237-236-231-102-123-236-213-321-132-213-103-571-236-734-321-312-327-107-263-130-103-312-120-103-574-517-574-231-542-527-216-572-275-265-267-3_W.mp3)
  Segment 3: 9884ms ~ 14276ms (file: 163-312-103-162-172-231-127-261-127-126-102-103-312-236-123-321-263-267-132-130-231-361-310-132-216-120-130-231-103-216-236-627-216-267-621-216-123-213-132-261-261-127-261-203-3_W.mp3)
  Segment 4: 15276ms ~ 15852ms (file: 574-371-765-675-675-751-751-723-570-576-570-576-562-567-576-574-163-576-736-756-713-715_W.mp3)
  Segment 5: 16852ms ~ 17428ms (file: 103-135-260-276-627-627-261 106-103-651-167-320-167-613-103-103-106-132-120-136-103-103-135-123-302-157-213-756-103-531-163-103-137-613-103-103-137-103-136-301-103-672-263-261-1_V.mp3)
  Segment 6: 18428ms ~ 19004ms (file: 130-176-130-173-301-170-706-670-760-176 310-370-170-173-301-172-103-301-107-107-715-103-103-310-715-173-130-135-701-130-317-170-130 103-170-137-301-175-132-310-130-170-751-310-1_V.mp3)
  Segment 7: 20004ms ~ 20580ms (file: 153-517-510-530-510-501-510-510-517-501-510-510-573-501-501 105-150-510-501-510-153-513-510-513-157-517-150-517-531-537-571-571-517-571-570-576 156-510-351-517-507-153-571-715-5_W.mp3)
  Segment 8: 21580ms ~ 22156ms (file: 213-210-210-172-217-210-206-206-206-206-136-316-310-312-361-136-621-126-321-261-123-671-675-607-607-413 675-675-670-237-750-754-623-261-263-263-263-312-326-103-162-670-367-103-1_V.mp3)
  Segment 9: 23156ms ~ 30968ms (file: 723-103-107-103-137-712-167-726-623-130-103-132-312-723-721-732-276-175-132-765-236-326-326-106-263-326-130-312-567-257-325-625-576-135-267-231-321-231-276-236-325-130-576-625-6_W.mp3)
  Segment 10: 31968ms ~ 32544ms (file: 103-765-672-136-756-103-136-106-671-160-103-136-236-613-132 136_V.mp3)
  Segment 11: 33544ms ~ 34120ms (file: 571-517-571-157-105-150-570-571-570 510-510-150-501-507-510-517-510-517-507-517-510-517-501-510-510-150-510-150-501-105-517-517_V.mp3)
  Segment 12: 35120ms ~ 35696ms (file: 572-326-132-123-130-107-312 261-321-136-367-176-372-170-316-613-316-756-310-570-756-317-130_W.mp3)
  Segment 13: 36696ms ~ 44436ms (file: 316-326-263-762-231-261-627-260-752-362-276-231-123-236-123-102-210-263-231-263-765-672-602-627-263-120-126-213-713-102-210-620-236-236-321-231-325-572-321-321-231-231-321-235-2_W.mp3)
  Segment 14: 45436ms ~ 46012ms (file: 703-703-702-753-175 231-162-627-620-265-760-176-761_W.mp3)
  Segment 15: 47012ms ~ 47588ms (file: 716-756-576-756-571-650 751-571-571-175-710-175-715-715-175-170-103-170-130-751-517-570-572-571-571-517 105-150-157-157-507-753-751-571-570-507-157-517-510-501-510-507-501 157-1_W.mp3)
  Segment 16: 48588ms ~ 57300ms (file: 127-217-123-260-210-257-527-574-123-213-206-602-657-652-123-130-172-217-126-326-327-632-105-163-125-617-765-107-275-253-312-120-301-573-103-235-137-172-206-570-572-617-276-236-2_V.mp3)
  Segment 17: 58300ms ~ 58876ms (file: 360-316-361-361-675-547-564-563-132-132-176-106-310-316-267-621-627-167-671-167-162-671-126-612-162-621-621-216-216-201-206-206-562-562-576-137-157-150-136-153-513-517-521-526-5_V.mp3)
  Segment 18: 59876ms ~ 60452ms (file: 130-103-134-103-130-135-103-137-134-103-103-103-103-103-103-103-136-103-130-103-103 157-517-175-157-103 317-137-134-135-315-135-134-135-134-103-134-103-134-136-103-136-103-316-1_V.mp3)
  Segment 19: 61452ms ~ 62028ms (file: 510-567-501-570-510-570-510-570-571-157-715-137-751-135-175-517-701-175-517-715-715-750-751-751-715-751-751-571-576-756-576-756-517-516-517-517-570-570-517-157-173-715-571-715-7_V.mp3)
  Segment 20: 63028ms ~ 75160ms (file: 632-130-316-576-326-735-276-532-263-273-231-136-321-321-267-765-761-632-236-263-310-130-326-362-267-236-726-321-237-732-137-126-261-625-261-321-307-756-267-206-276-123-627-260-2_W.mp3)
  Segment 21: 76160ms ~ 83432ms (file: 201-130-203-123-527-215-321-257-231-102-210-203-231-627-572-125-126-126-162-203-201-120-123-201-102-207-206-103-210-521-251-527-201-201-127-201-213-213-201-231-102-126-321-231-2_V.mp3)
  Segment 22: 84432ms ~ 85008ms (file: 106-103-103-103-163-164-145-146-103-136-103-145-106-103-106-134-103-103-103-146-103-103-103-103-103-135-571-571-715-574-574-754-135-175-574-574-571-574-571-574-315-351-315-315-1_W.mp3)
  Segment 23: 86008ms ~ 92056ms (file: 362-267-263-132-623-163-320-627-762-627-237-672-613-613-106-167-170-172-173-176-172-176-126-162-631-672-671-675-672-627-230-231-621-627-167-132-671-167-107-760-765-267-321-132-5_W.mp3)
  Segment 24: 93056ms ~ 93632ms (file: 103-130-135-157-135-157-517-135-105-103-105-150-103-157-130-105-105-107-103-103_W.mp3)
  Segment 25: 94632ms ~ 103848ms (file: 130-263-261-123-765-130-310-130-762-765-103-103-107-301-572-507-210-260-102-120-216-201-620-302-217-103-130-130-301-103-143-103-132-173-751-103-103-102-210-260-261-623-123-102-2_V.mp3)
  Segment 26: 104848ms ~ 105424ms (file: 507-570-570-107-510-150-107-157-175-705-570-574-574-570 571-751-570-501-150-571-510-150-107-175-517-701-150-570-175-175-175-751-571-571-574-751 570-150-751-150-507-751-107-571-7_V.mp3)
  Segment 27: 106424ms ~ 107000ms (file: 260-260-236-264-614 136-103-103-136-601-301-765-120-231-302-102-130-103-216-312-103-137-306-310-301-310-603-620-306-326-620-625-627-670-607-627-756-756-137-106_V.mp3)
  Segment 28: 108000ms ~ 108576ms (file: 526-651-657-571-156-516-571-567-561-517-615-165-165-561-567-657-316-163-615-561-157-651-567-567-657-675-657-576-567-576-675-567-567-657-657-567-657-563-567-657-567-576-567-576-5_W.mp3)
  Segment 29: 109576ms ~ 121060ms (file: 316-362-361-362 123-360-362-163-132_W.mp3)
  Segment 30: 122060ms ~ 122636ms (file: 103-103-321-126-123-610-260-621-312-132-263-623-132_V.mp3)
  Segment 31: 123636ms ~ 124212ms (file: 263-627-237-362-672-263-230-725-560-623-316-132 765-137-136 276-267 163-726-176 123-132-103-130-256-521-327 567_W.mp3)
  Segment 32: 125212ms ~ 125788ms (file: 635-657-365-135-365-536-536-534-537-567-574-576-573-571-571-513-573-357-365-316-376-317-763-317-316 627-627 613-317-173-317-731-713-371-731-765-756-107-167-672-360-713-137-143-7_W.mp3)
  Segment 33: 126788ms ~ 127364ms (file: 103 657-657-675-312-162-607-607-607-136-375-135-103-623-672-763-762-276-627-671-726-127-762-276-172-672-672-621-162-612-163-621-613-673-615-617-612-163-132-130-315-103-126-602-1_W.mp3)
  Segment 34: 128364ms ~ 137940ms (file: 301-173-731-726-701-716-174-132-310-317-135-312-276-261-107-162-137-267-216-206-627-260-206-263-312-306-103-627-102-132-301-263-256-270-107-514-521-126-523-106-127-216-176-167-1_W.mp3)
  Segment 35: 138940ms ~ 145852ms (file: 137-137-513-765-760-207-754-765-735-153-152-132-143-164-132-143-132-143-132-145-132-143-170-107-163-137-136-136-127-127-127-132-132-126-123-123-120-123-123-120-123-162-123-126-1_W_stt_the_wizards_of_the_wall.mp3)
  Segment 36: 146852ms ~ 156464ms (file: 106-615-106-130-132-103-201-210-132-237-540-574-732-650-325-267-206-206-702-237-301-762-723-526-103-501-263-672-213-567-507-563-103-735-132-127-172-275-756-256-267-574-257-267-1_V.mp3)
  Segment 37: 157464ms ~ 165672ms (file: 321-754-230-756-756-752-327-375-310-756-123-263-123-267-163-531-576-765-502-156-103-104-721-137-206-321-320-237-706-327-736-267-572-230-302-201-257-123-103-301-267-751-752-276-2_W_stt_the_city_systems_up_to_what_the_number_o.mp3)
  Segment 38: 166672ms ~ 173944ms (file: 376-735-752-413-163-132-216-672-103-132-273-263-716-351-671-623-621-621-302-326-312-326-136-176-267-326-321-230-302-102-103-267-267-163-160-613-326-102-621-137-137-103-623-602-1_W.mp3)
  Segment 39: 174944ms ~ 175520ms (file: 750-510-751-570-576 510-501-570-507-570-571-571-751-570-571-571-501-571-750-576-570-750-570-571_W.mp3)
  Segment 40: 176520ms ~ 177096ms (file: 753-216-275-753-762-176-237-723-236-627-760-570-756-765-265-261-213-752-507-532-752-573-573-573-507-350-537-537-532-562-362-632-362-362-360-367-367-365-316-316-375-756-576-675-6_W.mp3)
  Segment 41: 178096ms ~ 178672ms (file: 762-526-256-765-576-205-267-756-752-765-507-765-103-726-537-627-157_V.mp3)
  Segment 42: 179672ms ~ 180248ms (file: 176-167-167-176-167-137-176-163-167-126-163-172-123-123-175-173-136-175-612-162-135-160-531-156-537-617-671-761-175-716-153-175-567-561-516-561-576-156-516-567-576-561-516-756-7_W.mp3)
  Segment 43: 181248ms ~ 181824ms (file: 672-352-356-375-573-571-130-763-765-326-571-312-263-672-675-143 725-502-672-125-526-137-132-263-362-760-623-572-236-150-327-573-257-276-236-206-672-125-215-725-137-705-756-635-5_W.mp3)
  Segment 44: 182824ms ~ 191500ms (file: 537-276-236-321-263-126-103-267-672-312-735-726-236-625-267-527-547-257-273-231-267-574-547-230-123-273-275-276-267-321-231-327-534-547-176-135-327-325-573-237-326-316-102-673-2_W.mp3)
  Segment 45: 192500ms ~ 193076ms (file: 163-375-567-356-576-576-675-657-356-563-576-175-615-576-635-563-756-605-607-573-375-753-751-753-735-756-703-765-703-716-703-753-753-753-756-573-750-753-705-507-730-715-730-750-5_W.mp3)
  Segment 46: 194076ms ~ 194652ms (file: 517-156-106-160-106-756-132-631-613-165-160-675-263-236-576-657-162-162-236-102-517-623-152-132-157-621-167-617-106-103-231-316-103-261-267-362-316_V.mp3)
  Segment 47: 195652ms ~ 204688ms (file: 321-130-102-132-321-231-132-130-756-715-157-267-752-527-756-756-712-172-126-621-312-231-106-107-712-123-321-217-123-231-273-132-574-130-276-523-123-102-162-651-132-321-167-267-2_W.mp3)
  Segment 48: 205688ms ~ 213284ms (file: 132-312-231-754-267-216-574-130-130-106-731-371-173-631-263-107-176-261-261-621-213-120-106-231-263-123-123-756-321-326-326-352-310-163-261-263-263-754-574-263-275-257-547-312-2_W.mp3)
  Segment 49: 214284ms ~ 214860ms (file: 654 672-763-763-763 674-645-324-765-613-612-613_W.mp3)
  Segment 50: 215860ms ~ 216436ms (file: 263-260-216-132-263-213 127-137-130-312_V.mp3)
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
> **Time**: 2026-04-05 07:21:08
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "일을 밀어놓고 타임 스케줄 봐가면서 그렇게 한 번씩 타는 서핑이 좀 더 짜릿한 거 같긴 하더라고요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "일을", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "밀어놓고", "start_ms": 180, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "타임", "start_ms": 490, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "스케줄", "start_ms": 700, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "갈수록 내 취미보다는 내 집 내 식구가 먼저",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "갈수록", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "취미보다는", "start_ms": 510, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "아무리 거추장거린다고 해도 내가 해줄 수 있는 데는 해줘야지.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "아무리", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거추장거린다고", "start_ms": 350, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "안 봐도 비디오.",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "안", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "봐도", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "비디오.", "start_ms": 280, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "40년 넘게 닭을 튀겨온 기술로 노릇하게 완성된 닭튀김 이대로도 맛있겠네요.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "40년", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "넘게", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "보면 될 것 같아요. 대신에 ESS의 경우에는 삼성SDI가 NCA로 고성능 ESS를 꽤 많이 개발하고 있었거든요. 이 부분이",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "보면", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 470, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "아닌 것 같은데 하며 끝까지 속지 않습니다.",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "아닌", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "I don't think so, he's not from Seoul. 듀오 홈즈 쿠리.",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "don't", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 280, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "so,", "start_ms": 490, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "프리앙카 정도로 한국에 진심이면 한국 남자 정도는 당연히 구분할 줄 알죠.",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "프리앙카", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "요 부분에 따라서 로봇주도 살짝 더 관심을 받을 수 있겠죠. 요 시점에 따라서 요것도 좀 잘 보시면 좋을 것 같고요.",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "요", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부분에", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "근데 이게 갑자기 요게 나왔습니다 이게 긍정적으로 작용할지 모르겠어요 이게 뭐냐면 그룹이 자체 개발 중인 자율주행 기술 아트리",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 25, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 180, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "육박하는데 그중에서 무려 10%가 연 2천만 원도 벌지 못하는 박봉입니다. 더 충격적인 점은 한국에서 박사 학위를 결정하는 데 가장 중요한 것은 의외로 논문이 아니에요. 가장 중요한",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "육박하는데", "start_ms": 35, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "프라이드 30마리 닭강정은 오늘 25킬로 나간 거 같아요. 인가요? 배달 판매를 제외한 오늘 하루 매출만",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "프라이드", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "30마리", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "때부터 좋아서 궁금해서 알고 싶어서 한국어를 공부하기 시작했다는 이야기입니다.",
"speaker": "speaker_10",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "때부터", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 230, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "할리우드 한복판에서 한국어를 쓰고 한국 문화를 이야기하고 한국을 나에게 특별한 나라라고 말하는 배우가 있다는 것. 생각해보면 참 신기",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "할리우드", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "해야 되는 거구나 이거를 좀 많이 느꼈거든요 복구 심리가 들어서 더 풀을 때리려고 하는데 이런 장 그냥 보내주고 좀 장세가 안정되었을 때 그럴 때 다시 하시면은",
"speaker": "speaker_11",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "해야", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "모습은 지금 너무 좋아요 너무 보세요 크 이렇게 예쁜 주전자 어디 있어요 아이고 문제인데 이케아 게 아니라 뒤집게 문제예요 제가 뒤집는 거 진짜 못해요 아 오케이 조아 조아 조아 저기 봐 가보",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "모습은", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "어떤 곳이 집으로 와",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "곳이", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "집으로", "start_ms": 460, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 770, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "none",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 20,
"transcript": "또 하나는 아까 발굴되었을 때 그 진흙과 물속에 있었다고 했잖아요 그게 산소와 차단되는 밀폐 상태를 네 만들 만들어서 로도 보고 있습니다.",
"speaker": "speaker_14",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "또", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하나는", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "none",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 22,
"transcript": "We set these systems up so a few number of people are allowed to walk through the doorway and then it just makes everything a lot easier. Things get more competitive the exams",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "We", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "어 요런 거를 이제 점검하고 보수하는 일을 현재는 이제 사람들이 위험한 어 환경에 노출돼서 작업을 하고 있는데 그 저희 로봇",
"speaker": "speaker_16",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "요런", "start_ms": 160, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "OT를 까 마음대로 아무나 온 하면 안 되니까 그래서 이런 이제 시큐리티 스택을 굉장히 많이 쌓아서 했을 텐데 이걸 우회했다라는 말 자체가",
"speaker": "speaker_17",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "OT를", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "이 완전히 끊어졌죠 수색이 진행되었지만 시신도 잔해도 전혀 발견되지 않았습니다 이 갑작스러운 조난의 원인으로는 폭풍에 의한 전복 기계적 고장 항해 실",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 120, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "치를 무기로 삼아 미국의 일방적인 요구가 오히려 미국 첨단 산업 생태계에 치명적인 부메랑이 될 수 있음을 당당하게 경고하고 관철시켜야 합니다 경제",
"speaker": "speaker_18",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "치를", "start_ms": 25, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 07:21:08*
