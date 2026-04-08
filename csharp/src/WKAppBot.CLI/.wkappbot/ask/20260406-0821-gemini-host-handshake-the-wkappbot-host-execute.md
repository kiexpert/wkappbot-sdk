# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30344  time=08:17:57  uptime=16693s  mem=420MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_081238.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 765-763-671-765-756-263-263-263-623-623-632-632-657-675-675-623-756-753-735-571-517-576-573-576-567-312-653-671-637-673-367-617-613-637-607-630-167-607-571-756-751-756-375-132-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 752-137-312-301-316-135-705-756-572-572-620 275-750-316-570-163-213-175-321-726-213-571-762-571-312-752-237-560-752-752-356-635-573-572-765-576-672-326-732-327-516-312-570-237-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 126-217-125-125-132-132-136-132-132-123-132-123-132-163-162-163-162-157-153-150-175-571-715-176-157-571-157-175-157-517-175-157-751-571-753-531-571-571-175-123-126-162-126-123-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 750-750-752-537 751-751-751-715-751-751-753-576-537-175-137-175-375 576-752-573-571-710-105-705-573-705-703-170-170-123-175-175-170-175-753-375-752 571-570-570-752-570-517-753-5_V.mp3)
  Segment 5: 6304ms ~ 15880ms (file: 267-217-136-316-752-510-130-531-132-310-320-103-103-301-312-137-716-136-136-617-167-670-627-236-136-132-312-260-316-312-231-256-215-570-206-316-120-326-213-236-203-260-201-627-2_W.mp3)
  Segment 6: 16880ms ~ 17456ms (file: 125-652-123-625-672-625-236-635-126-762-675-672-563-675-657-627-257-526-526-236-657-756-576-675-572-632-675-765-657-765-625-257-625-256-635-625-326-537-543-546-540-504-504-514-6_W.mp3)
  Segment 7: 18456ms ~ 27960ms (file: 620-621-632-231-216-216-263-263-260-236-326-321-267-127-127-372-213-107-215-257-273-213-236-267-260-725-132-123-320-213-201-216-372-726-210-765-760-107-765-763-230-135-312-731-1_W.mp3)
  Segment 8: 28960ms ~ 35008ms (file: 623-320-362-267-176-751-372-675-236-263-627-310-236-231-612-136-675-120-267-267-123-102-120-216-273-237-273-372-316-321-325-574-514-361-263-537-130-160-617-167-267-162-270-257-2_W.mp3)
  Segment 9: 36008ms ~ 36584ms (file: 135-107-713-751-763-761-756-763-765-735-172-573-756-576-576-675-671-761-617-601-760-761-756-657-107-162-316-137-376-362-126-132-213-126-752-617-167-167-167-135-716-710-170-731-1_V.mp3)
  Segment 10: 37584ms ~ 45000ms (file: 762-326-761-672-607-106-130-765-671-231-230-213-162-123-361-631-163-361-267-162-163-107-310-672-102-621-267-376-326-756-735-706-176-736-321-321-237-326-362-321-573-576-672-726-1_W.mp3)
  Segment 11: 46000ms ~ 58924ms (file: 321-132-143-130-132-310-320-326-320-201-230-301-132-123-102-302-321-236-132-302-236-230-320-132-312-132-326 260-236-102-260-157-263-260-260-321-102-312-612-261-230-123-230-320-2_V.mp3)
  Segment 12: 59924ms ~ 60500ms (file: 310-317-321-327-327-312-327-372 735-370 250-357-157-157-153-153-631_W.mp3)
  Segment 13: 61500ms ~ 62076ms (file: 570-267-256-267-572-213-705-752-507-301-321-765-625-567 162-527-706-675-275-326-576-206-267-502-251-267-572-627-312-312-762-756-213-267-125-235-256-750-627-316-317-371-367-317-3_V.mp3)
  Segment 14: 63076ms ~ 63652ms (file: 631-752-753-230-132-132-570-312-137-756-721-710-317-132-137-723-271-213-175-537 576-132-341-572-327-203-235-127-213-276-273-267-321-570-247-570-302-750-572 574-765-267-237-570-3_V.mp3)
  Segment 15: 64652ms ~ 70664ms (file: 537-657-563-537-315-231-213-752-261-172-176-761-706-267-263-267-627-716-310-130-312-321-321-752-327-230-623-103-126-321-123-716-321-753-357-213-762-756-231-675-725-752-312-672-6_W.mp3)
  Segment 16: 71664ms ~ 85992ms (file: 765-627-351-716-176-761-621-216-207-312-103-617-675-651-651-167-156-576-657-567-657-675-137-317-137-675-132-705-312-651-567-657-547-537-573-576-573-567-573-537-312-567-576-576-5_W.mp3)
  Segment 17: 86992ms ~ 87568ms (file: 675-765-715-316-673-361-361-361 321-312-260-630-623-612-621-236-316-312-156-376-367-625-765-657-657-762-627-617-637-765-627-761-673-637-675-576-576-576 765-763-763-736-316-361-6_W.mp3)
  Segment 18: 88568ms ~ 89144ms (file: 126-162-130-120-102-102-130-103-130-102-130-103-103-120-102-102-102-102-132-103-123-102-123-126-123-120-102-120-132-152-156-517-152-152-153-152-156-513-510-103-130-103-102-103-1_V.mp3)
  Segment 19: 90144ms ~ 90720ms (file: 517-506-517-157-710-123-132-312-132-712-715-176-675-167-367-671-753-371-153-316-165-675-617-763-163-312-126-167-107-130-163-716-173-716-375-237-371-637-375-327-326-376-327-326-3_W.mp3)
  Segment 20: 91720ms ~ 98776ms (file: 316-310-132-137-135-156-107-103-321-527-413-752-321-312-301-256-125-120-123-265-312-206-126-302-130-675-263-310-103-301-237-325-567-657-501-573-765-217-321-236-237-321-201-267-2_W.mp3)
  Segment 21: 99776ms ~ 100352ms (file: 620-736-736-716-715-716-761-176-176-671-163 765-763-761-716_W.mp3)
  Segment 22: 101352ms ~ 110316ms (file: 130-103-103-574-571-321-276-236-321-753-756-756-570-752-756-230-132-301-143-163-162-213-756-320-370-135-637-756-573-310-312-357-206-572-754-260-620-621-612-236-276-623-263-127-2_W.mp3)
  Segment 23: 111316ms ~ 120424ms (file: 754-754-574-130-753-107-317-273-217-102-312-130-103 103-765-163-143-413-132-203-167-107-675-657-154-105-213-173-107-106-137-132-103-173-170-725-135-576-632-267-265-326-301-175-1_V.mp3)
  Segment 24: 121424ms ~ 130496ms (file: 236-260-260-132-136-203-103-136-260-102-103-260-153-312-132-103-137-132-760-317-301-316-302-103-301-312-106-312-106-103-132-103-173-175-135-326-320-260-136-321-231-103-132-132-2_V.mp3)
  Segment 25: 131496ms ~ 138408ms (file: 130-103-106-107-236-236-312-263-625-256-236-123-132-320-321-310-316-267-621-265-527-271-267-216-213-102-213-203-320-273-276-260-713-176-163-237-201-132-120-130-103-631-310-612-1_W.mp3)
  Segment 26: 139408ms ~ 146716ms (file: 321-132-326-173-164-326-320-320-326-320-735-237-127-201-237-230-670-652-321-310-130-213-725-752-753-752-610-162-570-130-132-275-273-270-236-236-576-327-237-273-123-102-102-123-1_W.mp3)
  Segment 27: 147716ms ~ 157472ms (file: 132-162-267-312-126-326-765-716-752-326-312-132-106-753-231-261-132-132-120-207-310-130-173-675-756-765-213-173-137-236-236-236-321-321-263-236-752-725-271-175-103-103-731-207-1_W.mp3)
  Segment 28: 158472ms ~ 168084ms (file: 760-136-216-107-675-106-201-203-103-230-102-260-120-102-135-132-162-106-130-231-321-230-230-320-106-126-106-102-267-256-216-312-321-210-123-106-206-201-130-235-312-102-312-321-3_V.mp3)
  Segment 29: 169084ms ~ 169660ms (file: 523 652-635 610-613 326-561 632-162-167 567 675-627-621 613-163-163-612-613-615-613-631-613-673 621-315-615 623 627_W.mp3)
  Segment 30: 170660ms ~ 177140ms (file: 260-261-132-210-267-301-321-263-312-213-260-267-206-316-310-361-301-236-263-263-237-237-316-307-601-165-517-375-236-763-260-762-716-107-106-107-157-352-176-107-137-216-731-710-2_W.mp3)
  Segment 31: 178140ms ~ 178716ms (file: 574-547-574-574-517-157-157-571-571_W.mp3)
  Segment 32: 179716ms ~ 187780ms (file: 210-216-754-751-205-201-162-162-176-176-103-321-230-203-320-320-675-230-302-130-316-130-103-106-375-103-136-751-457-317-321-103-132-137-173-136-170-173-310-126-162-621-627-623-1_V.mp3)
  Segment 33: 188780ms ~ 196952ms (file: 103-260-620-312-602-210-103-260-230-203-270-275-106-206-607-276-130-675-237-203-273-123-260-302-260-206-726-276-320-760-260-762-754-130-627-612-102-310-231-327-123-201-271-765-2_W.mp3)
  Segment 34: 197952ms ~ 204720ms (file: 657-675-602-605-601-751-761-236-217-671-271-273-176-173-160-267-237-327-756-756-573-361-762-712-176-103-137-327-576-576-617-716-106-130-103-176-106-710-167-301-327-372-130-672-6_W.mp3)
  Segment 35: 205720ms ~ 212740ms (file: 751-261-763-576-756-103-176-753-310-301-315-321-312-132-317-671-765-316-312-132-760-261-576-756-756-267-263-320-320-207-213-725-765-675-317-756-576-312-103-231-607-705-765-705-1_W.mp3)
  Segment 36: 213740ms ~ 214316ms (file: 571-517-137-123-136-130-136-153-172-321-123-251-157-130-137-125-103-153-130-130-130-150-130-123-213-153-312-310-130-132-153-752-765-675-130-153-130-315-302-130-237-135-150-137-1_V.mp3)
  Segment 37: 215316ms ~ 221760ms (file: 236-765-267-236-576-321-267-751-106-571-267-103-316-315-713-176-106-751-231-231-321-375-756-123-167-750-754-716-750-312-123-210-572-752-107-762-230-237-130-237-326-327-750-715-5_W.mp3)
  Segment 38: 222760ms ~ 223336ms (file: 756-126-751-675-627-163-673-167-571-156-136-137-170-376-175-167-126-137-763-761-763-756-756-751-672-167-675-625-621-316-564-365-651-605-163-163-617-615-567-671-612-106-765-362-3_W.mp3)
  Segment 39: 224336ms ~ 224912ms (file: 175-316-672-625-326-523-574-574-574-750-631-361-137-136-137-136-316-136_W.mp3)
  Segment 40: 225912ms ~ 226488ms (file: 627-762-765-765-760-567-657-657-576-675-756_W.mp3)
  Segment 41: 227488ms ~ 228064ms (file: 170-705-150-175-715-750-710-705-751-105-570-751 574-574-574-571-576-576-571_W.mp3)
  Segment 43: 230064ms ~ 230640ms (file: 631-136-137-176-167-716-136-765-675-675-673-675-673-637-327-371-216-312-320 213-507-310-376-237-672-572-672-576-756-723-562-672-273-257-613-671-621-271-216-257-132-132-132-261-2_W.mp3)
  Segment 44: 231640ms ~ 232216ms (file: 765-165-162-570-135-351-315-762 657-607-301-321-316-316-631-136-637-136-167-675-312-312-132-765-765-765-102_W.mp3)
  Segment 45: 233216ms ~ 239912ms (file: 106-167-106-175-576-130-267-102-263-236-756-725-236-753-137-132-103-263-263-126-321-261-123-167-756-765-756-517-106-753-106-267-137-103-176-237-123-103-103-527-267-327-130-263-1_W.mp3)
  Segment 46: 240912ms ~ 241488ms (file: 763-765-675-176-751-675-765-673-765-736-631-716-763-761_W.mp3)
  Segment 47: 242488ms ~ 243064ms (file: 123 107-175-107-105-105 135-301-317-713-716-761-765-137-176-763-712-176-765-765-752-675-671-671-675-675-713-673-716-673-736-163-712-167-157-163-163-173-376-672-765-762-736-675-3_W.mp3)
  Segment 48: 244064ms ~ 244640ms (file: 675-672-762-763-574-574-510-571-657-675-576-175-156-165-617-156-123 612-627-672-672-672_W.mp3)
  Segment 49: 245640ms ~ 246216ms (file: 576-657-652-156-162-705-657-167-156-756-356-143-176-173-170-107-516-570-715-170-715-751-715-175-715-751-675-756-567-561-157-165-156-571-657-625-256-267-256-652-561-657-756-356-1_V.mp3)
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
> **Time**: 2026-04-06 08:21:15
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "준비해주고 싶어 싶게 올라",
"speaker": "speaker_1",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "준비해주고", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "싶어", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "싶게", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "올라", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "요청했다는 보도도 이어지고 있는 상황이죠 이런 상황에서 방공망이 일부 뚫리는 사례까지 나오면서 아이언 돔 신화에도 균열이 생기고 있습니다 이스라엘은 여전히 방공망에는 문제가 없다는 입장을 유지",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "요청했다는", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보도도", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이어지고", "start_ms": 910, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1370, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "상황이죠", "start_ms": 1580, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상황에서", "start_ms": 2460, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "방공망이", "start_ms": 3050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 3560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뚫리는", "start_ms": 3870, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사례까지", "start_ms": 4280, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나오면서", "start_ms": 4850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아이언", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "돔", "start_ms": 5810, "dur__ms": 150, "phase_lr_deg": 0.0},
{"word": "신화에도", "start_ms": 5970, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "균열이", "start_ms": 6480, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생기고", "start_ms": 6890, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 7350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이스라엘은", "start_ms": 7850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여전히", "start_ms": 8460, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방공망에는", "start_ms": 8870, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "문제가", "start_ms": 9480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없다는", "start_ms": 9840, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "입장을", "start_ms": 10200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유지", "start_ms": 10560, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "지원을 하면서 굉장히 빠르게 메꾸고 올리면 진짜 빠르거든요 근데 이런 것들을 이제 모바일에 탑재해서 그냥 수행을 할 수가 있고 이 작은 모델들도",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "지원을", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하면서", "start_ms": 470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 1340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "메꾸고", "start_ms": 1750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "올리면", "start_ms": 2210, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 2620, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "빠르거든요", "start_ms": 2930, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 3580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 3840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4460, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "모바일에", "start_ms": 4720, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "탑재해서", "start_ms": 5230, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 5840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수행을", "start_ms": 6100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 6510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 6620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 6880, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 7230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 7340, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "모델들도", "start_ms": 7650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "한 건가요 여기서? 어 진짜 놀랍네요. 저희가 권하는 30만원 이하까지는 에이전트가 스스로 마음껏 쓸 수 있게끔 만들어놨습니다.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "건가요", "start_ms": 310, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "여기서?", "start_ms": 720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1410, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "놀랍네요.", "start_ms": 1770, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 2350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "권하는", "start_ms": 2760, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "30만원", "start_ms": 3220, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이하까지는", "start_ms": 3830, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "에이전트가", "start_ms": 4540, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "스스로", "start_ms": 5200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마음껏", "start_ms": 5660, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 6120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 6280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있게끔", "start_ms": 6390, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만들어놨습니다.", "start_ms": 6900, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "아 엄마 아이 아파한 거야? 레오 너무 맘 보고 싶어? 보고 싶다. 아빠가 아이디어 있는데 엄마 지금 병원에 있잖아",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "엄마", "start_ms": 290, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아이", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아파한", "start_ms": 960, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거야?", "start_ms": 1470, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "레오", "start_ms": 1880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2240, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맘", "start_ms": 2550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 2760, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싶어?", "start_ms": 3120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 3580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싶다.", "start_ms": 3940, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아빠가", "start_ms": 4500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아이디어", "start_ms": 4960, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 5570, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "엄마", "start_ms": 6130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 6540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "병원에", "start_ms": 6900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있잖아", "start_ms": 7410, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "잘하셨네요 가장 중요한 거는 내가 하고 싶은 거 그리고 나이가 들어서도 계속 할 수 있는 그런 게 가장 좋은 것 같아요 제가 좋아하는 거를 계속 할 수 있다라는",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잘하셨네요", "start_ms": 50, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 1160, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 1670, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 1980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 2340, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 3060, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3220, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나이가", "start_ms": 3630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "들어서도", "start_ms": 4090, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 5060, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 5740, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 6100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 6260, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 6620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 6980, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 7140, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 7650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 8010, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "거를", "start_ms": 8570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 8930, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 9340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 9500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다라는", "start_ms": 9660, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "산모분들께서 쉬는 것도 많이 하고 있는 건데 다행히도 자궁이 열려 있는데 막 나올 것 같지는 않거든요 근데 만약에 애가 지금 나오면은 미숙아로 태어나가지고 바로 여기",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "산모분들께서", "start_ms": 70, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "쉬는", "start_ms": 830, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 1190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 1910, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 2630, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다행히도", "start_ms": 2990, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자궁이", "start_ms": 3550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "열려", "start_ms": 4010, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 4370, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 4830, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "나올", "start_ms": 5040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 5400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같지는", "start_ms": 5560, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않거든요", "start_ms": 6020, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 6580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 6890, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "애가", "start_ms": 7350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 7660, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나오면은", "start_ms": 8020, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "미숙아로", "start_ms": 8580, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "태어나가지고", "start_ms": 9140, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 9950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 10310, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "프랑스 영상 하나가 프랑스를 넘어 유럽 전역을 발칵 뒤집어놓은 일이었습니다 파리에서 한 식당 가맹",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "프랑스", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "영상", "start_ms": 520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하나가", "start_ms": 880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "프랑스를", "start_ms": 1340, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "넘어", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유럽", "start_ms": 2210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전역을", "start_ms": 2570, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "발칵", "start_ms": 3030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뒤집어놓은", "start_ms": 3390, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "일이었습니다", "start_ms": 4050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "파리에서", "start_ms": 4810, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 5370, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "식당", "start_ms": 5580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가맹", "start_ms": 5940, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "That the AI assistant is going to behave just like a really trustworthy human personal assistant. That this assistant really",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 410, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "assistant", "start_ms": 720, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 1230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 1390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "behave", "start_ms": 1810, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 2220, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 2480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 2740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "trustworthy", "start_ms": 3160, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "human", "start_ms": 3770, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "personal", "start_ms": 4130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "assistant.", "start_ms": 4590, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "That", "start_ms": 5200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 5460, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "assistant", "start_ms": 5720, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 6230, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "시제품 출연 이후 겨우 5년 만에 양산에 들어갔다는 뜻입니다 완전히 새로 개발한 첨단 전투기라는 점을 감안하면 이 개발 및",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "시제품", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "출연", "start_ms": 510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이후", "start_ms": 870, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "겨우", "start_ms": 1230, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "5년", "start_ms": 1540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만에", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "양산에", "start_ms": 2210, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들어갔다는", "start_ms": 2620, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "뜻입니다", "start_ms": 3230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 3740, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 4200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "개발한", "start_ms": 4560, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "첨단", "start_ms": 5020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전투기라는", "start_ms": 5380, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "점을", "start_ms": 6040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "감안하면", "start_ms": 6400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 6960, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "개발", "start_ms": 7070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "및", "start_ms": 7430, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "저도 정확하게 쏘니는 여전히 프리미어 리그에서 뛸 수 있어 내 생각에는 쏘니가 없었다면 부항가는 해트트릭할 수 없었을 것 같아 쏘니의 존재감이 엘",
"speaker": "speaker_11",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 70, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정확하게", "start_ms": 380, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "쏘니는", "start_ms": 940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "여전히", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "프리미어", "start_ms": 1810, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "리그에서", "start_ms": 2320, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "뛸", "start_ms": 2880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3040, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 3510, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "생각에는", "start_ms": 3720, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "쏘니가", "start_ms": 4230, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "없었다면", "start_ms": 4690, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "부항가는", "start_ms": 5250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해트트릭할", "start_ms": 5760, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 6420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없었을", "start_ms": 6580, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 7040, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아", "start_ms": 7200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쏘니의", "start_ms": 7560, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "존재감이", "start_ms": 8020, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "엘", "start_ms": 8580, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "어? 뭐 아빠도 먹어봐 아빠 열어줘 숟가락 좀 더 물어다",
"speaker": "speaker_12",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "어?", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아빠도", "start_ms": 520, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "먹어봐", "start_ms": 930, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아빠", "start_ms": 1390, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "열어줘", "start_ms": 1750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "숟가락", "start_ms": 2210, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2670, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2880, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "물어다", "start_ms": 3090, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "매튜가 할 일은 단 하나 간밤에 에이전트가 해놓은 일을 확인하는 것이다 그동안 에이전트는 바다 건너",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "매튜가", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일은", "start_ms": 680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "단", "start_ms": 990, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "간밤에", "start_ms": 1560, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "에이전트가", "start_ms": 2020, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "해놓은", "start_ms": 2680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일을", "start_ms": 3140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "확인하는", "start_ms": 3450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "것이다", "start_ms": 4010, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그동안", "start_ms": 4470, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "에이전트는", "start_ms": 4930, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "바다", "start_ms": 5590, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "건너", "start_ms": 5950, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "단열재도 기준보다 10센티미터 더 두껍게 시공했고요 뜨거운 물 펑펑 쓰도록",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "단열재도", "start_ms": 80, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "기준보다", "start_ms": 640, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "10센티미터", "start_ms": 1200, "dur_ms": 8

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:21:15*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=42684  time=08:19:58  uptime=15216s  mem=471MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_081940.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 267-267-652-236-315-132-652-527-762-615-576-273-732-573-362-627-327-231-126-265-256-571-501-123-705-271-765-256-625-237-752-205-752-756-236-671-753-765-761-765-673-765-673-765-7_W.mp3)
  Segment 2: 1576ms ~ 10144ms (file: 206-276-276-267-276-276-260-267-260-260-310-312-123-203-425-310-132-231-756-754-752-260-276-276-267-206-201-206-206-267-627-576-301-260-206-675-547-572-562-526-310-105-130-572-2_W.mp3)
  Segment 3: 11144ms ~ 11720ms (file: 167-761-765-765-765-765-176-671-176-617 576-573-576-576-715-756-576-756-751-675-371-173-170-751-170-150-510-170-175-170-507-157-671-716-167_V.mp3)
  Segment 4: 12720ms ~ 13296ms (file: 510-517-571-571-157-517-175-157-751-570-751-517-750-753-750-750-576-571 571-570-510-571-571-157-751-750-150-157-571-751-751-751-570-571-175 574-574-574-574-574-574-574-574-570-5_W.mp3)
  Segment 5: 14296ms ~ 22792ms (file: 103-203-210-132-357-276-271-316-260-267-207-207-627-267-602-123-237-217-765-627-627-260-103-102-120-517-260-203-260-126-276-542-201-762-316-201-236-216-547-514-271-572-126-574-1_W.mp3)
  Segment 6: 23792ms ~ 32072ms (file: 732-712-273-217-120-321-132-267-270-217-267-271-276-524-237-321-217-236-263-276-310-206-231-206-275-276-273-762-163-574-275-123-125-726-276-276-206-712-320-267-527-310-627-362-1_W.mp3)
  Segment 7: 33072ms ~ 40524ms (file: 302-273-156-137-107-175-712-627-327-137-132-210-201-320-301-312-576-627-130-302-320-201-302-231-236-203-206-207-312-132-317-326-320-301-210-213-132-310-260-123-132-321-130-176-5_V.mp3)
  Segment 8: 41524ms ~ 42100ms (file: 571-571-571-571-751-571-517-571-751-571-571-517-571-751 571-571-157-571-157-571-175-157-157-517-571-157-517-571-157-153-157-175-751-571 574-570-571-571-570-570-571-571-576-571-5_W.mp3)
  Segment 9: 43100ms ~ 43676ms (file: 275-527-257-236-256-756-623-231-507-265-675-632-312-357-351-315-653-657-316-657-567-657-675-365-637-367-635_W.mp3)
  Segment 10: 44676ms ~ 45252ms (file: 571 571-571-571-571-571-571-573-573-571-573-517-576-576 574-576-576-571-576-571-571-571-571-571-571-571-571-571-571 574-571-576-574-571-576-571-517-517-517-571-517-571-570 571-5_W.mp3)
  Segment 11: 46252ms ~ 46828ms (file: 751-570-750-705-705-750-750-750-735-705-750-715-750-715-705-750-705-750 123-163-132-130-130-135-175-157-571-135-170-157-105-175-571-713-173-130-731-713-710-713-713-107 126-120-1_V.mp3)
  Segment 12: 47828ms ~ 48404ms (file: 361-163-136-316-316-135-315-135 132-102-321-162-261-725-261-126-321-312-162-376-103-127-765-625-527-726-756-765-765-671-671-657-567-567-576-576-132-236-301-567-307-705-307-730-7_W.mp3)
  Segment 13: 49404ms ~ 49980ms (file: 513-513-156-156-536-537-576-753-756-576-657-567-675-537-576-537-573-175-173-713-710-715-170-137-170-753-753-705-170-107-715-701-530-576-576-576-571-537-571-576-576-573-576-576-5_W.mp3)
  Segment 14: 50980ms ~ 51556ms (file: 157-571-571-751-571-715-517-715-517-157-517-571-571-571 571-570-570-571-571-150-571-517-571-157-507-157-751-715-715-751-571-751-157-571-517-157-571-570-750-576-571-751-576-751-5_V.mp3)
  Segment 15: 52556ms ~ 53132ms (file: 765-571-175-752-572-761-756-761-167-172-163-367-315-361-362-162-126-132-312-136-627-761-763-327-361-276-237-613-132-361-715-375-736-671-617-176-312-136-137-316-167-671-317-136-1_W.mp3)
  Segment 16: 54132ms ~ 62016ms (file: 102-216-263-123-102-762-123-213-261-321-102-312-132-715-237-230-201-213-263-236-152-523-102-175-326-120-123-102-120-132-321-267-160-120-162-512-261-132-312-721-276-527-627-612-2_W.mp3)
  Segment 17: 63016ms ~ 63592ms (file: 173-673-365-367-326-365-360-360-672-367-301-362-652-360-362-375-351-352-325-321_V.mp3)
  Segment 18: 64592ms ~ 73628ms (file: 756-756-276-675-726-267-276-572-271-260-267-230-103-137-173-130-206-320-203-520-276-260-132-573-375-103-103-207-137-754-250-372-310-207-276-572-312-321-213-137-137-761-761-217-3_W.mp3)
  Segment 19: 74628ms ~ 75204ms (file: 756 573-572-751-756-570-576-617-670-675-670-627-172-261-502-523-236-267-236-762-276-157-265-271-720-261-321-235-215-162-312-217-231-235-275-137-671-572-752-312-271-507-756-756-2_V.mp3)
  Segment 20: 76204ms ~ 81496ms (file: 132-705-230-572-230-230-302-162-713-213-756-726-126-126-321-751-173-132-137-132-167-267-273-123-271-137-310-731-703-731-317-351-170-572-231-352-325-257-351-153-135-130-315-327-1_W.mp3)
  Segment 21: 82496ms ~ 90704ms (file: 762-236-167-102-271-725-317-726-107-751-762-715-320-132-756-756-231-671-176-371-723-732-267-216-137-762-371-372-372-276-627-326-756-753-576-321-103-271-726-756-627-172-103-106-6_W.mp3)
  Segment 22: 91704ms ~ 92280ms (file: 326-362-132-316-627-315-135-317-310 362-371-371-315_W.mp3)
  Segment 23: 93280ms ~ 99112ms (file: 172-206-271-103-173-765-301-103-310-130-103-526-276-201-270-276-203-275-750-270-267-271-130-327-317-136-103-103-231-120-756-725-130-301-623-706-256-567-315-310-256-276-301-573-6_W.mp3)
  Segment 24: 100112ms ~ 100688ms (file: 317 563-735-317-317-130-136-761-675-675-627-317-163-317-136-167-135-715-625-652-612-162-125-612-763-751-752-731-317-726-532-316-132-126-162-231-132-126-132-721-627-712-756-176-6_W.mp3)
  Segment 25: 101688ms ~ 112272ms (file: 320-132-325-123-231-132-260-230-102-102-162-624-216-236-320-263-320-321-160-210-321-231-321-321-236-312-126-263-236-123-321-123-310-231-213-321-316-321-231-321-267-267-267-267-2_W.mp3)
  Segment 26: 113272ms ~ 124432ms (file: 372-574-527-256-576-761-630-273-236-576-573-173-317-276-327-321-273-236-230-326-263-173-571-236-326-302-237-127-137-103-327-315-321-163-231-325-130-574-301-351-320-173-236-752-3_W.mp3)
  Segment 27: 125432ms ~ 126008ms (file: 153-316-321-317-617-263-573-731-263-261-621-632-623-632-621-620-627-623_W.mp3)
  Segment 28: 127008ms ~ 127584ms (file: 175-136-160-715-657-625-165-612-567-156-567-617-156-157-175-567-517-106-567-516-156-157-175-671-176-657-256-215-216-216-256-265-261-351-310-301-136-135-517-136-516-165-576-576-6_W.mp3)
  Segment 29: 128584ms ~ 129160ms (file: 631-261-716-765-763-731-736-276-316-630-132_W.mp3)
  Segment 30: 130160ms ~ 130736ms (file: 726-276-273-752-235 716-713-176-761-163-176-167-173-317-173-261-203-153-107-173-137-136-136-132-130-130-103-106-132-543-574-574-574_W.mp3)
  Segment 31: 131736ms ~ 132312ms (file: 517-175-150-571 571-571-571-517-157-510-517-150-157-571-571-751-753-517-751-571-517-570-517-571-517-571-576 571-517-751-517-571-571-135-517-517-751-157-571-751-571-571-157-571-5_V.mp3)
  Segment 32: 133312ms ~ 137956ms (file: 213-153-135-123-103-123-315-310-130-215-123-301-362 132-103-317-123-102-103-102-173-107-103-726-231-132-103-102-372-301-132-231-120-127-321-312-130-132-130-123-120-210-126-102-1_W_stt_the_off_by.mp3)
  Segment 33: 138956ms ~ 146876ms (file: 610-237-267-106-601-306-103-271-273-236-637-130-103-310-103-163-160-613-276-627-160-716-201-273-307-215-216-216-217-237-251-253-215-213-231-213-216-213-217-201-215-257-265-216-2_W.mp3)
  Segment 34: 147876ms ~ 148452ms (file: 675-675-675-756-675-265-765-573-457-563-531-517-156-561-576-257-523-527-245-572-527-571-572-517-725-275-275-726-256-245-267-572-576-725-275-752-752-765-256-527-576-527-256-257-7_W.mp3)
  Segment 35: 149452ms ~ 150028ms (file: 176-527-361-162-120-237-276-562-152-756-725-237-267-270-627-637-267-276-576-756-726-267-251-136-536-602-265-257-706-256-652-321-750-761-750-562-627-276-623-207-236-321-326-263-2_V.mp3)
  Segment 36: 151028ms ~ 151604ms (file: 207-127-756-263-562-572-312-257-235-570-123-572-237-517-317-172-570-312-237-216-317-275-716-527-265-756-726-275-672-573-576-672-306-170-576-756-671-167-763-132-123_V.mp3)
  Segment 37: 152604ms ~ 158760ms (file: 271-276-762-267-261-162-102-120-312-271-762-261-726-271-172-126-276-267-627-723-627-672-236-672-627-276-627-267-736-267-672-267-120-571-216-261-621-627-261-267-260-136-261-263-2_W.mp3)
  Segment 38: 159760ms ~ 164152ms (file: 312-316-312-506-612-756-567-612-162-726-572-752-217-125-216-172-167-172-263-126-215-726-257-136-321-205-321-261-217-267-573-135-537-165-167-130-163-167-167-513-756-573-357-315-5_W.mp3)
  Segment 39: 165152ms ~ 165728ms (file: 236-315-125-310-567-367-756-376-156-635-162-352-325-236-625-125-126-651-526-625-167-657-526-361-316-130-657-362-165-750-567 672-516-351_W.mp3)
  Segment 40: 166728ms ~ 172308ms (file: 672-256-256-267-267-302-237-260-201-321-320-213-203-670-627-256-276-271-267-267-627-670-607-126-102-310-130-162-760-762-132-173-132-321-132-123-312-312-123-123-132-371-713-675-7_W.mp3)
  Segment 41: 173308ms ~ 173884ms (file: 576-576-573-571-571-132-123-261-623-136 613-632-537-357_W.mp3)
  Segment 42: 174884ms ~ 175460ms (file: 107-637-371-716-763-730-731-761-176-716 312-365-620-130-120 650-102-632 607-237 360 672-762-137-761-371-756-731-751-765-763-765-756-765-675-735-574-137-751-157-157-167-173-137-1_W.mp3)
  Segment 43: 176460ms ~ 186684ms (file: 317-175-130-312-312-576-312-762-672-672-312-106-103-630-132-103-312-107-107-321-213-123-231-203-123-320-103-312-267-267-265-567-612-312-763-137-123-301-103-620-623-130-167-103-1_W.mp3)
  Segment 44: 187684ms ~ 188260ms (file: 574-570-571 571-574-573-574-574-574-570-570-570-570-571-571-571-571-517-507-137-370-157-701-570 571-570-571-157-573-157-573-105-107-150-517-571-517-751-513-135-710-175-507-175-5_V.mp3)
  Segment 45: 189260ms ~ 189836ms (file: 571-571-317-132-317-657-263-765-765-671-317_W.mp3)
  Segment 46: 190836ms ~ 197856ms (file: 254-231-132-574-540-352-230-627-267-236-534-574-321-127-761-507-132-236-613-326-231-501-540-130-736-157-312-276-273-327-263-623-236-236-265-753-375-372-573-210-230-126-254-527-1_W.mp3)
  Segment 47: 198856ms ~ 199432ms (file: 574-571-572-574-574-574-573-573-572-574-576-573-576 561-517-571-517-517-576-513-157-753-573-153-571-573-576-571-715-571-571-751-571-751-753-751-571-751-756 571-572-576-572-572-5_W.mp3)
  Segment 48: 200432ms ~ 204140ms (file: 123-321-230-231-231-302-123-210-231-260-613-320-130-306-213-213-302-301-321-312-132-130-132-132-301-312-316-132-263-260-267-216-132-126-231-132-321-326-213-236-120-102-216-312-2_W.mp3)
  Segment 49: 205140ms ~ 205716ms (file: 106-167-516-156-126-517-657-651-716-576-675-567-567-516-567-167-153-130-165-126-156-136-160-157-761-176-176-163-271-167-657-167-137-130 756-715-173_W.mp3)
  Segment 50: 206716ms ~ 207292ms (file: 317-130-367-736-763-310-571-176-765-675-675-675-673-765-567-756-236-765-107-351-570-576-756-756-572-576-756-560-752-672-627 273-132-576-256-527-276-673-126-231-731-361-175-530-1_W.mp3)
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
> **Time**: 2026-04-06 08:21:30
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "준비해주고 싶어 싶게 올라",
"speaker": "speaker_1",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "준비해주고", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "싶어", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "싶게", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "올라", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "요청했다는 보도도 이어지고 있는 상황이죠 이런 상황에서 방공망이 일부 뚫리는 사례까지 나오면서 아이언 돔 신화에도 균열이 생기고 있습니다 이스라엘은 여전히 방공망에는 문제가 없다는 입장을 유지",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "요청했다는", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보도도", "start_ms": 550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이어지고", "start_ms": 910, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1370, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "상황이죠", "start_ms": 1580, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상황에서", "start_ms": 2460, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "방공망이", "start_ms": 3050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 3560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뚫리는", "start_ms": 3870, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사례까지", "start_ms": 4280, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나오면서", "start_ms": 4850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아이언", "start_ms": 5400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "돔", "start_ms": 5810, "dur__ms": 150, "phase_lr_deg": 0.0},
{"word": "신화에도", "start_ms": 5970, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "균열이", "start_ms": 6480, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생기고", "start_ms": 6890, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 7350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이스라엘은", "start_ms": 7850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "여전히", "start_ms": 8460, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방공망에는", "start_ms": 8870, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "문제가", "start_ms": 9480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없다는", "start_ms": 9840, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "입장을", "start_ms": 10200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유지", "start_ms": 10560, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "지원을 하면서 굉장히 빠르게 메꾸고 올리면 진짜 빠르거든요 근데 이런 것들을 이제 모바일에 탑재해서 그냥 수행을 할 수가 있고 이 작은 모델들도",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "지원을", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하면서", "start_ms": 470, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 1340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "메꾸고", "start_ms": 1750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "올리면", "start_ms": 2210, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 2620, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "빠르거든요", "start_ms": 2930, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 3580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 3840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4460, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "모바일에", "start_ms": 4720, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "탑재해서", "start_ms": 5230, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 5840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수행을", "start_ms": 6100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 6510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 6620, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 6880, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 7230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "작은", "start_ms": 7340, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "모델들도", "start_ms": 7650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "한 건가요 여기서? 어 진짜 놀랍네요. 저희가 권하는 30만원 이하까지는 에이전트가 스스로 마음껏 쓸 수 있게끔 만들어놨습니다.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "건가요", "start_ms": 310, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "여기서?", "start_ms": 720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1410, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "놀랍네요.", "start_ms": 1770, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 2350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "권하는", "start_ms": 2760, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "30만원", "start_ms": 3220, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이하까지는", "start_ms": 3830, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "에이전트가", "start_ms": 4540, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "스스로", "start_ms": 5200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마음껏", "start_ms": 5660, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 6120, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 6280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있게끔", "start_ms": 6390, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만들어놨습니다.", "start_ms": 6900, "dur_ms": 1000, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "아 엄마 아이 아파한 거야? 레오 너무 맘 보고 싶어? 보고 싶다. 아빠가 아이디어 있는데 엄마 지금 병원에 있잖아",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 80, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "엄마", "start_ms": 290, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아이", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아파한", "start_ms": 960, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거야?", "start_ms": 1470, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "레오", "start_ms": 1880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2240, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맘", "start_ms": 2550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 2760, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싶어?", "start_ms": 3120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 3580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싶다.", "start_ms": 3940, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아빠가", "start_ms": 4500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아이디어", "start_ms": 4960, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 5570, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "엄마", "start_ms": 6130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 6540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "병원에", "start_ms": 6900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있잖아", "start_ms": 7410, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "잘하셨네요 가장 중요한 거는 내가 하고 싶은 거 그리고 나이가 들어서도 계속 할 수 있는 그런 게 가장 좋은 것 같아요 제가 좋아하는 거를 계속 할 수 있다라는",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잘하셨네요", "start_ms": 50, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 1160, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 1670, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 1980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 2340, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 2700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 3060, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3220, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나이가", "start_ms": 3630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "들어서도", "start_ms": 4090, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 5060, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 5740, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 6100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 6260, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 6620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 6980, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 7140, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 7650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 8010, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "거를", "start_ms": 8570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 8930, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 9340, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 9500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다라는", "start_ms": 9660, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "산모분들께서 쉬는 것도 많이 하고 있는 건데 다행히도 자궁이 열려 있는데 막 나올 것 같지는 않거든요 근데 만약에 애가 지금 나오면은 미숙아로 태어나가지고 바로 여기",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "산모분들께서", "start_ms": 70, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "쉬는", "start_ms": 830, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 1190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 1910, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 2630, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다행히도", "start_ms": 2990, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자궁이", "start_ms": 3550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "열려", "start_ms": 4010, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 4370, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 4830, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "나올", "start_ms": 5040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 5400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같지는", "start_ms": 5560, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "않거든요", "start_ms": 6020, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 6580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 6890, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "애가", "start_ms": 7350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 7660, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나오면은", "start_ms": 8020, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "미숙아로", "start_ms": 8580, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "태어나가지고", "start_ms": 9140, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 9950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 10310, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "프랑스 영상 하나가 프랑스를 넘어 유럽 전역을 발칵 뒤집어놓은 일이었습니다 파리에서 한 식당 가맹",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "프랑스", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "영상", "start_ms": 520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하나가", "start_ms": 880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "프랑스를", "start_ms": 1340, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "넘어", "start_ms": 1850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유럽", "start_ms": 2210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전역을", "start_ms": 2570, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "발칵", "start_ms": 3030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뒤집어놓은", "start_ms": 3390, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "일이었습니다", "start_ms": 4050, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "파리에서", "start_ms": 4810, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 5370, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "식당", "start_ms": 5580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가맹", "start_ms": 5940, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "That the AI assistant is going to behave just like a really trustworthy human personal assistant. That this assistant really",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 410, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "assistant", "start_ms": 720, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 1230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "going", "start_ms": 1390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "behave", "start_ms": 1810, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 2220, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 2480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 2740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 2850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "trustworthy", "start_ms": 3160, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "human", "start_ms": 3770, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "personal", "start_ms": 4130, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "assistant.", "start_ms": 4590, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "That", "start_ms": 5200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 5460, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "assistant", "start_ms": 5720, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 6230, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "시제품 출연 이후 겨우 5년 만에 양산에 들어갔다는 뜻입니다 완전히 새로 개발한 첨단 전투기라는 점을 감안하면 이 개발 및",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "시제품", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "출연", "start_ms": 510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이후", "start_ms": 870, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "겨우", "start_ms": 1230, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "5년", "start_ms": 1540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "만에", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "양산에", "start_ms": 2210, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들어갔다는", "start_ms": 2620, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "뜻입니다", "start_ms": 3230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 3740, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "새로", "start_ms": 4200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "개발한", "start_ms": 4560, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "첨단", "start_ms": 5020, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전투기라는", "start_ms": 5380, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "점을", "start_ms": 6040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "감안하면", "start_ms": 6400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 6960, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "개발", "start_ms": 7070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "및", "start_ms": 7430, "dur_ms": 200, "phase_lr_deg": 0.0}
]
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
"transcript": "저도 정확하게 쏘니는 여전히 프리미어 리그에서 뛸 수 있어 내 생각에는 쏘니가 없었다면 부항가는 해트트릭할 수 없었을 것 같아 쏘니의 존재감이 엘",
"speaker": "speaker_11",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 70, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정확하게", "start_ms": 380, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "쏘니는", "start_ms": 940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "여전히", "start_ms": 1400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "프리미어", "start_ms": 1810, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "리그에서", "start_ms": 2320, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "뛸", "start_ms": 2880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3040, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 3200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 3510, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "생각에는", "start_ms": 3720, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "쏘니가", "start_ms": 4230, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "없었다면", "start_ms": 4690, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "부항가는", "start_ms": 5250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "해트트릭할", "start_ms": 5760, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 6420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없었을", "start_ms": 6580, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 7040, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같아", "start_ms": 7200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쏘니의", "start_ms": 7560, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "존재감이", "start_ms": 8020, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "엘", "start_ms": 8580, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "어? 뭐 아빠도 먹어봐 아빠 열어줘 숟가락 좀 더 물어다",
"speaker": "speaker_12",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "어?", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아빠도", "start_ms": 520, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "먹어봐", "start_ms": 930, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아빠", "start_ms": 1390, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "열어줘", "start_ms": 1750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "숟가락", "start_ms": 2210, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2670, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2880, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "물어다", "start_ms": 3090, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "매튜가 할 일은 단 하나 간밤에 에이전트가 해놓은 일을 확인하는 것이다 그동안 에이전트는 바다 건너",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "매튜가", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일은", "start_ms": 680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "단", "start_ms": 990, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "간밤에", "start_ms": 1560, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "에이전트가", "start_ms": 2020, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "해놓은", "start_ms": 2680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "일을", "start_ms": 3140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "확인하는", "start_ms": 3450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "것이다", "start_ms": 4010, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그동안", "start_ms": 4470, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "에이전트는", "start_ms": 4930, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "바다", "start_ms": 5590, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "건너", "start_ms": 5950, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "단열재도 기준보다 10센티미터 더 두껍게 시공했고요 뜨거운 물 펑펑 쓰도록",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "단열재도", "start_ms": 80, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "기준보다", "start_ms": 640, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "10센티미터", "start_ms": 1200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2010, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "두껍게", "start_ms": 2220, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시공했고요", "start_ms": 2680, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "뜨거운", "start_ms": 3340, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "물", "start_ms": 3800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "펑펑", "start_ms": 4010, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쓰도록", "start_ms": 4370, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "떨어지는 모습을 보였는데 저는 이렇게 지수가 급락할 때를 주도주를 매수할 수 있는 굉장히 좋은 기회",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "떨어지는", "start_ms": 50, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "모습을", "start_ms": 560, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보였는데", "start_ms": 970, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1790, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지수가", "start_ms": 2150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "급락할", "start_ms": 2610, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때를", "start_ms": 3070, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주도주를", "start_ms": 3380, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "매수할", "start_ms": 3940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 4870, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 5330, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기회", "start_ms": 5690, "dur_ms": 400, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-06 08:21:30*
