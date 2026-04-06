# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:38:28  uptime=26354s  mem=1847MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_083819.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 621-326-625-265-216-321-213-351-514-531-316-316-136-132-312-312-326-316-312-123-321-326-312-216-613-621-136-621-625-625-621-362-627-263-216-236-216-312-126-532-572-652-236-326-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 136-162-761-362-263-213-321-361-312-102-321-120 513-431 312-162-126-531-574-310-103-326-562-103-102-165-301-316-316-162-176-167-610-163-162-163-167-125-302-162-361-132-601-136-3_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 263-236-326-673-367-613-617-617-613-672-632-261-621-617-672-627-163-126-216-312-276-672-672-361-621-162-623-326-176-127-627-162-162-621-617-632-627-672-672-263 106-312-136-163-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 371-316-623-632-176-316-172-673-765-716-706-716-701-710-710-170-701-170-160-310-371-731-621-172-123-162-317-312-367-657-137-326-126-120-716-165-657-102-105-201-106-152-517-517-7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 517-135-317-573-325-312-123-361-316-316-173-512-625-631-512-526-265-563-375-351-547-534-537-357-351-570-574-576-573-357-376-176-657-657-675-673-673-376-376-376-574-567-657-675-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 167-167-623-130-765-765-765-756-765-763-756-675-607-651-617-176-106-132-602-361-306-301-317-310-310-301_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 650-360-632-362-136-267-265-132-136-107-230-263-236-236-107-750-561-165-571-756-173-627-123-132-310-312-326-321-527-356-672-617-163-613-106-106-106-231-320-362-310-132-162-106-1_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 123-617-263-126-162 653-547-165-715-162-126-106-162-152-167-156-162-103-163-172-162-671-267_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 213-261-167-137-172-162-671-612-612-163-160-103-617-725-152-167-316-132-103-567-571-571-610-613-176-675 176-137-132-213-625-263-126-132-154-126-132-574-156-163_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 156-261-216-102-127-312-163-176-132-103-106-173-167-137-160-652-265-103-132-621-172-103-613-163-167-176-103-103-103-102-132-316-671-106-716-106-130-130-172-176-617-106-103-130-1_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 152-576-567-257-617-617-631-316-136-756-765-675-756-675-765-675-765-762-763-761-756-756-765-765_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 302-230-236-263-602-203-263-276-275-762-213-263-267-726-765-263-236-267-206-762-756-267-267-102-750-167-236-206-261-136-613-163-162-261-126-103-130-631-623-632-621-631-725-263-6_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 563-567-567-576-567-567-567-567-516-517-516-571-516-567-561-516-517-516-156-517-571-567-157-517-576-563-536-351-536-576-576-567-537-536-356-516-563-567-156-516-567-516-572-516-1_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 162-263-761-107-165 103-137-103-160-650-167-132-135-136-130-175-126-162-162-316-163-106-132-576-126-712-163-136-617-136-716-736-637-756-612-206-201-102-126-123-103-163-136-671-6_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 136-712 136-167-167-130-103-130-165-106-106-167-671-617-165-105-165-136-517-162-176-165-761 106_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 136-326-321-132-125-256-527-253-156-261-271-237-326-231-162-361-632-356-135-356-315-132-362-263-260-632-567-562-526-654-357-356-352-325-365-320-326-326-326-316-361-136-321-612-1_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 627-620-610-130-176-657-163-617-106-106-160-160-126-136-617-165-316-610-671-621-175-651-367-136-106-163-167-160-170-756-106-571-176-657-316-123-751-672-132-136-716-167-267-627-2_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 257-132-127-132-130-132-627-357-567-657-675-652-675-675-627-627-756-675-675-672-756-765-675-675-765-675-765-675-657-576-675-567-657-652-607-675-675-576-675-132-167-236-612-615-3_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 567-657-365-576-563-623-627-765-762-763-675-167-570-165-163-136-316-136-362-376-672-675-632-263-261-623-265-621-267-263-627-621-627-123-326-120-130-160-612-132-130-623-632-623-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 675-675-675-672-621-627-362-321-216_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 103-175-132-134-213-230-203-230-302-754-720-735-321-312-306-301-316-301-301-231-756-307-753-756-720-762-231-127-320-732-162-621-167-301-675-617-163-612-107-106-103-126-321-263-2_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 236-123-163-102-310-326-261-523-567-235-263-321-320-357-513-567-213-317-326-312-326-312-236-236-310-231-213-623-216-160-162-623-320-320-263-123-310-362-312-213-130-316-301-123-3_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 106-610-160-506-576-315-137-657-675-617-106-103-310-310-306-316-736-763-130-321-612-126-106-162-267-201-102-106-651-625-607-672-672-756-132-617-610-617-130-106-103-106-120-213-1_W.mp3)
  Segment 24: 36248ms ~ 52160ms (file: 612-261-613-136-623-672-103-103-103-103-103-103-130-136-516-163-176-710-163-162-163-162-130-617-217-167-617-623-103-132-103-132-163-132-602-620-602-621-263-102-507-750-132-615-1_V.mp3)
  Segment 25: 53160ms ~ 53736ms (file: 651-561-651-562-657-516-576-562-576-627-375-651-156-157-125-516-167-156-612-152-165-153-165-617-165-167-163-163-125-136-176-162-726-162-237-216-652-562-752-620-206-152-326-312-1_W.mp3)
  Segment 26: 54736ms ~ 55312ms (file: 216-621-267-623-236-267-723-126-132-126-132-302-301-630-267-627-316-136-625-625-675-367-620-627-602-620-627-627-672-765-756-576-765-765-670-675-675-670-376-637-736-316-301-136-3_W.mp3)
  Segment 27: 56312ms ~ 56888ms (file: 312-302-301-231-312-132-306-301-106-213-216-326-312-301-132-130-361-137-603-320-326-106-316-162-612-136-261-206-260-206-326-201-310-123-703-526-231-123-103-302-203-362-602-620-3_W.mp3)
  Segment 28: 57888ms ~ 58464ms (file: 103-517-615-167-167-231-126-621-361-132-130-106-137-176-176-136-103-125-572-216-157-623-106-216-210-567-206-625-126-652-670-615-162-637-671-163-167-165-625-601-276-610-165-130-1_V.mp3)
  Segment 29: 59464ms ~ 60040ms (file: 267-623-623 301-321-321-216-236-231-123-132-123-120-130-310-162-106-132-130-103-132-164-316-360-132-103 671-627-613-126-175-321-567-136-136-132-674-672-132-237 312-312-123-652-3_W.mp3)
  Segment 30: 61040ms ~ 61616ms (file: 621-261-621 316-361-236-213-263-236-321-312 607-602-267-360-107-167-761-103-310-613-162-163-167-760-760-706-107-167-176-260-261-620-106-123-312-136 130-136-163-326-103-312-671-7_W.mp3)
  Segment 31: 62616ms ~ 63192ms (file: 136-267-632-326-263-260-236-673-621-627-620-762-163-617-167-621-316-103-312-132-302-306-721-321-136-163-162-201-672-361-625-631-167-263-261-126-675-765-263-263-362-326-302-203-6_W.mp3)
  Segment 32: 64192ms ~ 64768ms (file: 675-675-675-672-672-627-126-623-126 762_W.mp3)
  Segment 33: 65768ms ~ 66344ms (file: 176-601-627-617-102-160-136-162-126-102-135-136-671-103-136-176-621-162-136-163-167-103-167-106-136-106-517-162-675-136-132-103-126-175-160-365-267_V.mp3)
  Segment 34: 67344ms ~ 67920ms (file: 627 123-321-126-316-103-136-106-136-615-135-316-103-715-103-321-506-163-650-163-162 132-103-165-165-162-261-137-617-675-106-751-213 167-137-126-576-615-106-154-127-716-126-103-7_V.mp3)
  Segment 35: 68920ms ~ 69496ms (file: 312-306-576-576-576-576-531-756-572-571-753-567-567-517-573-576-567-576-573-517-537-567-576-537-516-527-567-576-657-576-572-576-572-507-526-537-365-675-576-573-576-675-357-132-1_W.mp3)
  Segment 36: 70496ms ~ 71072ms (file: 261-167-162-137-136-261-126-162-126-103 567-576-750-751-765-675-765-763-675-675-765-675-765-672-670-607-106-672-671-621-260-132-306-360_W.mp3)
  Segment 37: 72072ms ~ 72648ms (file: 130-103-132-126-623-267-263-627-263-632-362-312-132-103 623-632-632-261-621-623 705-753-732 326-260-263-261_W.mp3)
  Segment 38: 73648ms ~ 74224ms (file: 167-132-132-163-136-123-137-127-126-126-162_V.mp3)
  Segment 39: 75224ms ~ 75800ms (file: 153-315-125-312-315-135-135-312-531-231-132-312-352-325-312-351-512-135-352-523-315-312-351-235-251-315-213-513-531-513-315-321-213-532-351-351-325-315-153-312-351-135-312-351-1_W.mp3)
  Segment 40: 76800ms ~ 80040ms (file: 203-520-315-132-213-132-206-236-103-132-162-102-103-103-231-132-610-630-260-620-260-206-524-520-250-150-510-157-137-574-607-627-630-620 620-620-602-620-602-612-167-163-167-601-6_W.mp3)
  Segment 41: 81040ms ~ 81616ms (file: 321-625-623-152-351-257-725-173-764-762-762-257-675-265-625-627-627-536-153-321-325-543-540-514-517-175-257-257-172-725-735-731-701-716-173-317-312-567-564-564-567-563-534-540-6_W.mp3)
  Segment 42: 82616ms ~ 83192ms (file: 173-351-153-125-127-526-536-524-352-257-532-253-352-253-352-315-376-357-507-531-275-257-275-532-543-547-534-532-357-372-372-375-371-175-751-751-375-375-315-351-671-316-537-517-5_W.mp3)
  Segment 43: 84192ms ~ 84768ms (file: 320-231-675-601-603-627-162-321-372-625-263-235-756-672-201-620-601-602-620-136-321-521-763-302-163-561-103-160-162-102-123-162-675-102-123-302-326-623-132-301-316-132-160-653-1_V.mp3)
  Segment 44: 85768ms ~ 86344ms (file: 567-567-576-567-576-657-576-567-567-567-576-571-567-576-576-567-567-657-567-756-576-756-756-627-167-673-765-567-657-567-657-675-615-617-672-637-617-765-671-675-761-675-657-675-6_W.mp3)
  Segment 45: 87344ms ~ 87920ms (file: 310-103-136-310 132-301-312-132-574-574-567-567-576-572 576_V.mp3)
  Segment 46: 88920ms ~ 89496ms (file: 765-103-213-127-612-167-132-163-627-612-621-167-267 576-126-167-167-106-612-627-625-162-765-765-623-621-620-160-162-163-107-635-127-765-756-756-675-627-263 106-176-167-176-615-2_W.mp3)
  Segment 47: 90496ms ~ 91072ms (file: 263-607-627-620-260-602-653-567-576-156-625-623-102-126-130-132-275-765-630-306-637-306-620-216-670-320-306-267-672-675-623-627-620-670-267-367-603-130-672-236-326-312-205-263-6_W.mp3)
  Segment 48: 92072ms ~ 92648ms (file: 156-521-215-123-125-153-512-251-213-312-321-132-123-563-516-152-265-126-125-152-275-267-213-312-321-312-136-136-136-136-132-351-135-312-132-312-132-132-562-265-256-256-136-136-1_W.mp3)
  Segment 49: 93648ms ~ 94224ms (file: 675-675-671-615-361-631-365-365-365-365-365-365-635-635-365-365-635-365-365-365-365-365-365-365-365-365-365-635-365-365-540-547-574-573-351-256-235-620-624 540-204-207-207-207-2_W.mp3)
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
> **Time**: 2026-04-03 08:39:35
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이 옷은 세탁기에 돌려도 되나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "옷은", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "세탁기에", "start_ms": 260, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "돌려도", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "되나요?", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "아니요, 이건 드라이클리닝을 맡기셔야 해요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아니요,", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "드라이클리닝을", "start_ms": 250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "맡기셔야", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해요.", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "소재가 실크라서 그런가요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "소재가", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "실크라서", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그런가요?", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "네, 물에 젖으면 변형될 수 있거든요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "물에", "start_ms": 110, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "젖으면", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "변형될", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있거든요.", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "그렇군요. 세탁 라벨을 확인해 볼걸 그랬어요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그렇군요.", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "세탁", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "라벨을", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "확인해", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "볼걸", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그랬어요.", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "손세탁도 위험할까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "손세탁도", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "위험할까요?", "start_ms": 230, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "웬만하면 전문가에게 맡기는 게 안전해요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "웬만하면", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전문가에게", "start_ms": 210, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "맡기는", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 470, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "안전해요.", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "알겠습니다. 내일 세탁소에 가야겠네요.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "알겠습니다.", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 170, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "세탁소에", "start_ms": 260, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가야겠네요.", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "비싼 옷이니까 아껴서 입어야죠.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "비싼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "옷이니까", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아껴서", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "입어야죠.", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "Is it okay to use a dryer for this?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "Is", "start_ms": 70, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 160, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "okay", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 280, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "use", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "dryer", "start_ms": 470, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "this?", "start_ms": 580, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "No, you should air dry it instead.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "No,", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "should", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "air", "start_ms": 270, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "dry", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "instead.", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "햇빛에 말려도 될까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "햇빛에", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말려도", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "될까요?", "start_ms": 310, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "그늘에서 말리는 게 색이 안 변해요.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그늘에서", "start_ms": 55, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "말리는", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 320, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "색이", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "변해요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "까다로운 옷이네요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "까다로운", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "옷이네요.", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "그만큼 예쁘니까 괜찮아요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그만큼", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "예쁘니까", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "괜찮아요.", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "잘 관리해서 오래 입어야겠어요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "관리해서", "start_ms": 120, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오래", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "입어야겠어요.", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "그게 좋겠네요. 다음에 또 봐요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그게", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋겠네요.", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 370, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "안녕히 가세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "오늘 컨디션 어때요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "컨디션", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 310, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "잠을 못 자서 조금 피곤해요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "잠을", "start_ms": 55, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 140, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "자서", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "피곤해요.", "start_ms": 390, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "커피 한 잔 마시면 나아질 거예요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "마시면", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나아질", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "지금 마시러 가려고요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마시러", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가려고요.", "start_ms": 270, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "제가 같이 가 드릴까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가", "start_ms": 240, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 290, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "네, 같이 가면 좋죠. 가요.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가면", "start_ms": 210, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋죠.", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "시원한 아이스 라떼 어때요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "시원한", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아이스", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "라떼", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 380, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "좋은 생각이네요. 전 그걸로 할게요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "생각이네요.", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 300, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "그걸로", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "할게요.", "start_ms": 460, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "다 왔네요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "왔네요.", "start_ms": 130, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "금방 나올 거예요. 잠시만 기다려요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "금방", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나올", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잠시만", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "기다려요.", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:39:35*
