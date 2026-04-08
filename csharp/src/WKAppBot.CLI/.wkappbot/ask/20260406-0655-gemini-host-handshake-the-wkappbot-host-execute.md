# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=39080  time=06:54:30  uptime=12018s  mem=213MB  system_processes=411"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_065413.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 6876ms (file: 267-273-572-517-213-267-260-237-312-321-367-163-237-725-267-207-237-301-327-167-375-601-617-756-267-263-263-351-620-136-103-123-321-237-276-627-230-236-132-216-621-236-263-217-1_W.mp3)
  Segment 2: 7876ms ~ 8452ms (file: 574-375-231-231-321-316-362-361-316-315-327-321-362-312-326-316-327-362-623-263-316-632-361-312-301-103-312-756-751-751-175-751-175_W.mp3)
  Segment 3: 9452ms ~ 15716ms (file: 675-617-167-167-276-671-623-261-617-571-615-216-263-762-672-672-260-163-762-756-260-271-236-627-763-726-762-670-516-106-571-130-231-567-170-765-176-753-175-267-756-267-672-756-2_W.mp3)
  Segment 4: 16716ms ~ 22800ms (file: 607-316-673-673-603-632-326-671-675-360-671-103-725-127-136-531-706-321-107-137-231-261-267-260-501-132-203-267-276-265-261-267-206-567-627-206-273-570-572-123-103-103-137-206-2_V.mp3)
  Segment 5: 23800ms ~ 32836ms (file: 276-230-263-326-103-103-210-123-106-670-321-327-361-130-103-613-136-167-106-601-106-106-130-103-143-130-134-103-130-321-206-267-657-621-236-132-136-132-206-620-123-132-102-302-2_V.mp3)
  Segment 6: 33836ms ~ 34412ms (file: 102-132 167 173 623-725-231-157-231-572-352-125-231-216-126-217-136-163-130-310-137-137-125-137-512-312-527-751-135-152-715-715-572-132-217-572-315-231-713-675-670-715-152-125-5_V.mp3)
  Segment 7: 35412ms ~ 35988ms (file: 653-625-652-631-625-623-625-652-657-675-657-657-675-675-652-625-623 632-623-623-623-623-623-621-263-263 160_W.mp3)
  Segment 8: 36988ms ~ 37564ms (file: 672-256-670-136-263-725-325-312-572-236-256-256-257-752-621-237-763-236-763-523-752-613-275-231-267-527-327-176-723-317-175-170-736-173-107-312-310-731-153-132-312-163_V.mp3)
  Segment 9: 38564ms ~ 48716ms (file: 236-675-326-573-574-651-263-126-765-672-362-312-576-273-320-321-326-321-537-756-713-301-231-312-675-765-574-571-517-670-236-602-230-123-627-263-257-576-236-267-267-617-316-620-2_W.mp3)
  Segment 10: 49716ms ~ 59472ms (file: 621-230-310-351-326-263-632-752-527-271-261-160-675-203-263-267-276-527-106-237-132-103-132-130-173-276-327-276-257-512-315-276-573-537-547-572-570-547-541-713-751-103-302-732-6_W.mp3)
  Segment 11: 60472ms ~ 69508ms (file: 102-751-756-756-762-270-326-321-132-627-753-102-102-102-132-267-276-217-271-275-207-102-102-201-215-264-134-153-102-127-752-753-576-120-162-312-103-123-137-310-107-761-701-123-3_W.mp3)
  Segment 12: 70508ms ~ 71084ms (file: 672-672-375-276-263-753-571 754-571-367-716-132-241-312-263-125-762-672-672-215-267-126-362-273-267-765-670-263-203-237-275-267-506-327-267-672-576-273-276-317-267-576-753-752-7_V.mp3)
  Segment 13: 72084ms ~ 72660ms (file: 172-760-170-765-621-167 132-326-130-312-163-312-160-132_W.mp3)
  Segment 14: 73660ms ~ 74236ms (file: 267-216-506-231-607-231-237-627-506-265-265 351-265-132-257-571-162-752-257-576-763-251-263-256-276-265-650-652-256-173-275-276-276-502-756-605-752 275-527-537-620-176-271-267-2_V.mp3)
  Segment 15: 75236ms ~ 75812ms (file: 137-571-753-751-375-751-137-176-567-136-316-217-615-126-261-216-213-527-127-173-216-152-271-125-570-752-715-316-561-172-561-132-137-761-162-153 732-215-231-523-132-231-321-236-2_V.mp3)
  Segment 16: 76812ms ~ 82104ms (file: 526-235-206-261-263-526-267-162-623-532-216-103-216-231-236-206-213-321-321-713-132-127-123-732-127-172-172-721-712-123-107-261-216-327-276-620-576-167-123-326-263-236-203-263-6_W.mp3)
  Segment 17: 83104ms ~ 83680ms (file: 357-316-320-375-751-756-375-513-537-361-537-310-316-315-136-153-365-561-576-576-357-315-357-367-627-627-675-236-126-157-576-315-130-123-263-326-537-516-356-632-317-137-537-157-5_W.mp3)
  Segment 18: 84680ms ~ 96128ms (file: 267-321-267-273-237-310-130-362-236-132-263-574-132-135-326-327-236-267-310-352-316-231-562-130-130-176-273-213-135-572-312-532-321-236-326-276-327-574-163-273-213-317-351-576-3_W.mp3)
  Segment 19: 97128ms ~ 103824ms (file: 765-310-326-325-723-206-312-326 132-327-263-260-103-231-275-132-315-231-103-236-236-537-562-163-352-312-310-357-132-136-326-573-627-576-562-267-276-574-527-325-325-230-235-320-3_W.mp3)
  Segment 20: 104824ms ~ 105400ms (file: 613-126-132-675-675-671-673-637-675-673-631-673-631-637-361-637-632-621-637-673-632-632-617-162-613_W.mp3)
  Segment 21: 106400ms ~ 106976ms (file: 670-165-675 216 613-610-136-160-137-153-361-736-762-672-307-170-751-761-637-605-761-361-130-165-617-167-610-612-570-215-231-126-752-631-613-126-301-103-517-726-126-250-216-125-2_V.mp3)
  Segment 22: 107976ms ~ 114132ms (file: 130-103-123-126-106-126-130-273-527-574-103-671-617-761-617-671-163-750-627-267-327-617-671-276-230-230-167-326-301-123-326-237-267-765-173-172-312-726-267-237-572-270-160-372-3_W.mp3)
  Segment 23: 115132ms ~ 115708ms (file: 570-751-756-753-751-765-751-753-752-756-573-231-716-173-361-615-132-576-570-675-136-132-162-617-362-136-362-136-310-136-132-362-265-517-761-317-576-507-537-317-137-132-132-312-3_W.mp3)
  Segment 24: 116708ms ~ 117284ms (file: 316-361-316-132-736-361-367-327-302-362-376-376-765-613-753-762-736-671-365-673-367-375_W.mp3)
  Segment 25: 118284ms ~ 118860ms (file: 570-576-760-276-712-132-507-127-576-750-627-750-276-270-312-571-567-267-372-750-127-752-215-132-576-276-703-762-573-765-765-275-572-570-123-325-651-623-276-507-236-756-367-736-7_V.mp3)
  Segment 26: 119860ms ~ 127816ms (file: 703-106-310-103 206-627-102-627-267-267-270-572-175-163-107-756-107-271-107-103-621-276-726-175-260-765-103-102-173-672-570-106-671-167-271-276-752-756-571-756-106-216-261-716-1_W.mp3)
  Segment 27: 128816ms ~ 129392ms (file: 731-367 312 127-132 623-716-715-751-756-751-756_W.mp3)
  Segment 28: 130392ms ~ 137340ms (file: 617-167-132-765-261-107-137-326-573-672-670-760-672-765-625-267-627-263-106-752-217-216-762-263-321-327-673-173-210-621-326-273-267-362-275-276-213-123-267-263-732-317-132-716-1_W.mp3)
  Segment 29: 138340ms ~ 138916ms (file: 637-316-136-130-103-136-136-106-315-765-637-367-673-635-613-371-360-637-670-670-765_W.mp3)
  Segment 30: 139916ms ~ 146108ms (file: 527-573-752-137-275-260-260-723-752-756-753-267-270-321-237-752-756-263-321-263-623-607-753-321-753-173-261-276-312-321-573-320-327-762-102-761-761-276-217-273-172-301-123-732-7_W.mp3)
  Segment 31: 147108ms ~ 147684ms (file: 752-756-176-751-725-567-517-126-751-765-257-376-156-167-253-761-361-167-653-213-365-726-162-132-756-157-732-576-751-175-516-271-132-725-721-753-735-173-765-675-176-167-675-625_V.mp3)
  Segment 32: 148684ms ~ 157036ms (file: 362-613-312-132-130-302-321-360-760-236-306-132-230-312-756-576-175-306-130-203-312-312-132-132-620-670-320-132-230-731-673-631-302-765-750-572-761-753-376-675-130-127-302-126-1_W.mp3)
  Segment 33: 158036ms ~ 158612ms (file: 163-312-631-163-126-632-612-617-657-165-176-526-167-163-670-603-637-623-612-361-613-361-637-362-321-362-326-316-362-362-316-631-637-617-612-162-612-653-610-163-605-610-716-671-6_V.mp3)
  Segment 34: 159612ms ~ 168216ms (file: 206-153-316-316-362-123-123-301-312-267-310-132-231-312-507-206-210-327-312-321-236-260-206-120-301-207-132-107-312-203-302-352-107-206-572-263-263-132-312-710-137-260-261-710-1_W.mp3)
  Segment 35: 169216ms ~ 177604ms (file: 275-106-726-312-765-751-761-106-310-315-310-501-157-103-135-567-570-537-210-132-126-312-312-321-123-103-132-170-312-157-320-267-271-217-672-672-617-673-127-123-102-123-120-123-2_W.mp3)
  Segment 36: 178604ms ~ 179180ms (file: 716-763-763-763-765-736-761-731-765-675-675-673-763-675-736-673-673-763-763-671-736-763-617-637-167_W.mp3)
  Segment 37: 180180ms ~ 186696ms (file: 532-132-310-620-236-203-271-261-126-210-103-617-126-126-126-132-326-726-217-126-126-173-267-763-716-302-716-102-756-527-230-207-123-627-201-201-574-231-706-120-720-670-103-754-3_W.mp3)
  Segment 38: 187696ms ~ 188272ms (file: 562-763-571-165-167-637-613-765-632-637-673-765-271-532-576-751-576-752-756-756-756-756-756-576-576-576-756-576-756-756-765-756-576-576-752-567-572-756-673-356-752-567-756-574-5_W.mp3)
  Segment 39: 189272ms ~ 189848ms (file: 756-725-527-123-756-672-527-527-625-250-253-325-301-526-326-136-362-635 657-675-361-316-310-712-107-370_V.mp3)
  Segment 40: 190848ms ~ 191424ms (file: 675-567-762-762-765-672-765 751-715-716_W.mp3)
  Segment 41: 192424ms ~ 200596ms (file: 357-625-753-571-263-752-625-327-231-125-103-731-716-517-173-672-172-705-276-362-213-762-376-702-762-612-312-132-327-321-267-375-162-710-175-765-176-756-750-306-602_W.mp3)
  Segment 42: 201596ms ~ 210200ms (file: 672-546-305-257-203 732-230-123-756-705-107-752-713-320-257-275-276-170-102-271-765-760-107-217-370-730-735-765-107-756-275-576-751-705-756-567-576-657-567-753-753-750-750-756-7_W.mp3)
  Segment 43: 211200ms ~ 222108ms (file: 153-156-153-752-153-754-210-130-132-710-572-520-312-312-136-163-267-756-257-210-231-163-572-572-257-210-123-130-103-261-260-123-132-231-102-312-210-132-310-301-321-231-120-321-1_W.mp3)
  Segment 44: 223108ms ~ 237652ms (file: 326-320-672-267-623-167-213-562-103-103-301-263-263-670-236-672-321-231-312-326-132-752-571-105-572-572-127-126-123-167-572-752-752-751-576-725-572-257-750-570-267-263-572-572-7_W.mp3)
  Segment 45: 238652ms ~ 239228ms (file: 130-621-627-623-362-316-263 624-263_V.mp3)
  Segment 46: 240228ms ~ 240804ms (file: 316-307-130-150-571-756-571-715-765-716-576-517-571-675-261-263-362-351-361-316-103-127-137-136-136-231-163-136-163-163-637-675-163-165 123 612-671-672-126-630-136-130-107-276-3_W.mp3)
  Segment 47: 241804ms ~ 242380ms (file: 253-632-365-507-275-762-573-132-173-760-327-753-235-257-570-351-217-502-215-762-576-756-562-523-165-706-627-576-602-267-573-136-231-623-217-273-562-672-267-725-576-376-532 275-2_V.mp3)
  Segment 48: 243380ms ~ 243956ms (file: 175-157-157-157-175-157-175-157-157-175-175-157-157-175-157-756 135-571-175-175-175-517-517-571-175-157-175-175-517-175-175 573-157-571-571-571-571-574-175-576-571-157-751-571-7_W.mp3)
  Segment 49: 244956ms ~ 245532ms (file: 574-756-267-762-312-362-123-753-731-731-735-753-756-705-701-107-713-710-763-701-136-136-307-360-301-310-317-371-307-317-310-310-751-607-751-137-761-137-371-173-175-576-675-316-5_W.mp3)
  Segment 50: 246532ms ~ 247108ms (file: 701-701-170-751-175-701-170-175-710-173-170-710-701-706-170-710-715 715-701-760-751-175-751-705-710-710-710-170-715-751-170-176-731-170-715-175-715-716-716 175-170-107-167-107-7_V.mp3)
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
> **Time**: 2026-04-06 06:55:49
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "해서 그런 수순을 굉장히 좀 유리하게 가져간 부분도 있겠죠 근데 이 네 가지를 다 분할을 시켜 놓은 것이 내가 쓰고 있는 모바일 환경이나 랩탑 환경 그리고 각각의 디램이라든가 대역폭 이런",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 50, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 310, "dur_ms": 270, "phase_lr_deg": 0.0},
{"word": "수순을", "start_ms": 580, "dur_ms": 570, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 1150, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1660, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "유리하게", "start_ms": 1870, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "가져간", "start_ms": 2130, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "부분도", "start_ms": 2390, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "있겠죠", "start_ms": 2620, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 2930, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3250, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 3610, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "가지를", "start_ms": 3770, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 4000, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "분할을", "start_ms": 4720, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "시켜", "start_ms": 4980, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "놓은", "start_ms": 5180, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 5410, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 5710, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "쓰고", "start_ms": 6030, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 6190, "dur_ms": 370, "phase_lr_deg": 0.0},
{"word": "모바일", "start_ms": 6560, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 3,
"transcript": "성치까지 진행할 계획입니다 이재명 대통령이 조사 인력 확충과 실효성 확보를 직접 주문한 만큼 정부는 이를 반영해",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "성치까지", "start_ms": 80, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "진행할", "start_ms": 750, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "계획입니다", "start_ms": 1360, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "이재명", "start_ms": 2030, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "대통령이", "start_ms": 2650, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "조사", "start_ms": 3260, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "인력", "start_ms": 3470, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "확충과", "start_ms": 3730, "dur_ms": 760, "phase_lr_deg": 0.0},
{"word": "실효성", "start_ms": 4490, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "확보를", "start_ms": 5160, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 5830, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "주문한", "start_ms": 6090, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "예쁘다며 전체적으로 프레피 스타일을 화려하게 믹솔로지한 디자인이라 극찬했습니다 또한 여고생들이 목에 맨 체크무늬 매듭이 너무 예뻐",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "예쁘다며", "start_ms": 100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전체적으로", "start_ms": 700, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "프레피", "start_ms": 1370, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "스타일을", "start_ms": 2040, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "화려하게", "start_ms": 2640, "dur_ms": 630, "phase_lr_deg": 0.0},
{"word": "믹솔로지한", "start_ms": 3270, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "디자인이라", "start_ms": 3590, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "극찬했습니다", "start_ms": 4260, "dur_ms": 670, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "짜내는 구조잖아요 중동에 맞춰서 설비 투자가 이루어졌던 거고요 근본적으로는 민간 기업들이 최적화된 블렌딩 수율을 다 바꿔야 되는 구조기 때문에",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "짜내는", "start_ms": 120, "dur_ms": 270, "phase_lr_deg": 0.0},
{"word": "구조잖아요", "start_ms": 390, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "중동에", "start_ms": 620, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "맞춰서", "start_ms": 880, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "설비", "start_ms": 1200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "투자가", "start_ms": 1300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이루어졌던", "start_ms": 1400, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "거고요", "start_ms": 1610, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "중동의 석유를 무기화한",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "중동의", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "석유를", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "무기화한", "start_ms": 280, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "that this reaches different continents different cultures",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "that", "start_ms": 20, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 690, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "reaches", "start_ms": 940, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "different", "start_ms": 1610, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "continents", "start_ms": 1740, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "반응은 뜨거웠다 So on my computer today all of a sudden Henry gives me a call he just starts calling",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "mixed",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "반응은", "start_ms": 40, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "뜨거웠다", "start_ms": 270, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "So", "start_ms": 940, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 1260, "dur_ms": 570, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 1830, "dur_ms": 570, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "emphasize the taste emphasize the taste oh with the rice like that oh innovates the taste",
"speaker": "speaker_7",
"confidence": 0.90,
"language": "en",
"silence_before_ms": 30,
"audio_type": "mixed",
"words": [
{"word": "emphasize", "start_ms": 30, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 650, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "taste", "start_ms": 880, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "타다닥 치면 그 약속을 기억해서 알려준다거나 혹은 대화 중에 바로 물어보게 되고 답을 바로 올 수 있다 이 부분이 굉장히 핵심적이었습니다",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "mixed",
"words": [
{"word": "타다닥", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "치면", "start_ms": 160, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 910, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "약속을", "start_ms": 1660, "dur_ms": 760, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이란이 영공 통과를 불허하며 자국 영공이 전쟁에 사용되는 걸 거부했습니다 유럽",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이란이", "start_ms": 40, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "영공", "start_ms": 710, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "통과를", "start_ms": 1380, "dur_ms": 370, "phase_lr_deg": 0.0},
{"word": "불허하며", "start_ms": 1750, "dur_ms": 270, "phase_lr_deg": 0.0}
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
"transcript": "기인한 고유가 여파는 이제 물가 통계로도 확인됩니다 지난달 3월 소비자 물가는 1년 전보다 2.2% 올랐습니다 경유가 17% 뛰는 등 석유류가 10%가량 치솟았습니다 그나마 생산량이 늘면서 농산물 가격이 떨어졌고 석유 최고",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "기인한", "start_ms": 30, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "고유가", "start_ms": 290, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "여파는", "start_ms": 500, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "과거 85%에 달했던 우리나라의 중동산 원유 의존도는 2021년 59%까지 떨어졌습니다 한 곳에 의존",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "과거", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "85%에", "start_ms": 170, "dur_ms": 570, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "들여다보도록 하겠습니다 먼저 황금키에서 캘린더를 봤을 때 4월 1일 수요일에는 통신 관련주가 종가 기준",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "들여다보도록", "start_ms": 20, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 540, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "이란과 미국이 서로 상대방인 휴전을 구걸한다고 하면서 고도의 심리전을 벌이고 있는 상황으로 보입니다 이란 파라스",
"speaker": "speaker_13",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이란과", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미국이", "start_ms": 380, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "지원하는 컨텍스트 길이가 128K까지 가니까 우리 요즘 에이전팅 AI를 많이 돌리잖아요 뭐 오픈 클로드도 그렇고 모바일로도 클로드를 막 돌리고 하는 일들이 많이 있기 때문에 충분",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "지원하는", "start_ms": 50, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "컨텍스트", "start_ms": 310, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "길이가", "start_ms": 630, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "128K까지", "start_ms": 890, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "전투기인 KF-21의 개발이 지난 2015년 12월 처음 개발을 시작한 이후 불과 단 10년 만에 그 모든 개발",
"speaker": "speaker_14",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "전투기인", "start_ms": 40, "dur_ms": 760, "phase_lr_deg": 0.0},
{"word": "KF-21의", "start_ms": 800, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "페이베이커리에서 나오는 아가 진짜 맛있잖아 그런 테이스트 나오면 좋겠다",
"speaker": "speaker_15",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "mixed",
"words": [
{"word": "페이베이커리에서", "start_ms": 100, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "나오는", "start_ms": 710, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "유행하고 있다고 하죠 오늘은 마크롱 프랑스 대통령 내외의 한국 방문을 계기로 더 주목받고 있는 프랑스 내 한국 문화의 침투에 대한 상황 이모저모를 전해드리려고 합니다 먼저 잠시 후 달 광화문",
"speaker": "speaker_16",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "유행하고", "start_ms": 60, "dur_ms": 670, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 730, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "와 나 진짜 한 번도 안 했네 none of them okay none of them none of them 100%",
"speaker": "speaker_17",
"confidence": 0.93,
"language": "mixed",
"silence_before_ms": 20,
"audio_type": "mixed",
"words": [
{"word": "와", "start_ms": 20, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "나", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "영향력을 가진 쪽으로 기울게 되는 것입니다 이것이 바로 지금 대만이 직면한 구조적인 현실입니다 더욱 충격적인 부분은 일부 국가에서 이미 그 결과가",
"speaker": "speaker_18",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "영향력을", "start_ms": 40, "dur_ms": 570, "phase_lr_deg": 0.0},
{"word": "가진", "start_ms": 610, "dur_ms": 750, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "어느 날 문을 탁 열고 예쁜 아가씨가 이렇게 들어오는데 형도 딱 봤어요 근데 몸이",
"speaker": "speaker_19",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어느", "start_ms": 50, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 360, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "작년에 건강검진 받으니까 약간 당 수치가 좀 높고 가족력이 있어서 좀 어떤 음식",
"speaker": "speaker_20",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "작년에", "start_ms": 30, "dur_ms": 570, "phase_lr_deg": 0.0},
{"word": "건강검진", "start_ms": 600, "dur_ms": 570, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "두 분의 어떤 그 새로운 시선에 대한 관점이 이 집을 좀 만든 것 같고 두 분의 그 지구력 인내력이 정말 끝내주는 것 같아요 감사합니다 감사합니다",
"speaker": "speaker_21",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "두", "start_ms": 40, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "분의", "start_ms": 740, "dur_ms": 100, "phase_lr_deg": 0.0}
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
"transcript": "와 나 진짜 한 번도 안 했네",
"speaker": "speaker_17",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "mixed",
"words": [
{"word": "와", "start_ms": 20, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "나", "start_ms": 630, "dur_ms": 160, "phase_lr_deg": 0.0}
]
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
"transcript": "527 573 752 137 275 260 260",
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
"transcript": "362 613 312 132 130 302 321",
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
"transcript": "206 153 316 316 362 123 123",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "275 106 726 312 765 751 761",
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
"transcript": "532 132 310 620 236 203 271",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "562 763 571 165 167 637 613",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "756 725 527 123 756 672 527",
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
"transcript": "357 625 753 571 263 752 625",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "672 546 305 257 203 732 230",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "153 156 153 752 153 754 210",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "326 320 672 267 623 167 213",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "130 621 627 623 362 316 263",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "316 307 130 150 571 756 571",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "253 632 365 507 275 762 573",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "175 157 157 157 175 157 175",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "574 756 267 762 312 362 123",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "701 701 170 751 175 701 170",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 06:55:49*
