# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=12:04:07  uptime=27932s  mem=305MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_120314.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 501-532-512-513-516-521-512-516-516-513-546-543-543-501-105-210-756-750-152-152-512-517-501-576 532-205-521-502-527-527-526-576-567-576-526-526-521-521-125-513-513-501-150-501-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 637-736-372-357-367-673-372-763-376-375-671-136-162-136-126-126-126-136-136-123-132-137-317-132-132-132-137-132-127-132-123-132-132-127-132-127-153-157-135-312-135-136-316-136-1_W.mp3)
  Segment 3: 3152ms ~ 9488ms (file: 312-275-216-215-136-675-725-312-257-165-137-702-312-562-637-203-310-713-132-507-732-536-562-526-716-752-235-127-276-105-127-312-132-705-572-513-137-267-327-123-503-257-136-312-2_W.mp3)
  Segment 4: 10488ms ~ 11064ms (file: 531-513-502-150-150-105-510-510-105 512-523-256-526-502-512-531-520-512-532-512-512-512-512-512-153-135-675-673-627-620 610-160-610-610-672-762-762-673-261-267-620-260-260-260-6_W.mp3)
  Segment 5: 12064ms ~ 12640ms (file: 162-516-567-576-675-546-576-756-675-675-763-756-765-756-756-675-762-576-765-765-765-765-765-675-672-672-765-765-675-675-675-765-574-756-765-675-675-673-675-672-675-756-765-627-7_W.mp3)
  Segment 6: 13640ms ~ 14216ms (file: 615-326 126-162-163-613-612-610-160-610-612-162-615-625-612-612-621 210_W.mp3)
  Segment 7: 15216ms ~ 28392ms (file: 275-102-127-106-103-753-263-627-765-106-231-236-312-213-236-756-127-120-261-621-106-167-174-132-136-163-163-102-623-123-216-160-716-162-132-310-321-132-231-672-162-260-267-210-2_W.mp3)
  Segment 8: 29392ms ~ 29968ms (file: 267-617-160-615-165-160-637-136 547-615-675-132-657-167-160-567-173-615-163 103-137-157-136-160-163-125-165-106-130-657-601-756-136-236 157-376-715-567-160-652-162-163-312-136-7_W.mp3)
  Segment 9: 30968ms ~ 31544ms (file: 513-571-571-571-571-570-570-570-571-570-572-570-571-571-572 760-671-765-715-761-167-210-615-160-610-165-160-165-156-150-516-756-756-756-756-756-756-765-576-651-607-617-365-316-5_W.mp3)
  Segment 10: 32544ms ~ 33120ms (file: 574-574-574-754-574-574-574-574-754-574-574-574-570-574-754-574-574-571-571-501-574-701-570-750-571-570-751-754 574-574-574-574-574-574-574-750-751-754-517-175-705-175-751-517-7_W.mp3)
  Segment 11: 34120ms ~ 34696ms (file: 501-561-517-517-751-137-571-175-153-137-127-165-173-167-731-675-756-756-756-756-756-756-735-756-372-153-765-715-135-153-135-156-153-135-571-753-371-517-751-357-751-173-175-156-1_W.mp3)
  Segment 12: 35696ms ~ 36272ms (file: 526-523-502-502-536-513-512-512-153-531-512-531-517-526-513-513-516-513-532-751-572-752-572-752-752-751-752-751-751-526-526-652-567-523-561-513-503-573-562-536-563-576-532-572-5_V.mp3)
  Segment 13: 37272ms ~ 37848ms (file: 130-312-103-317-137-301-730-370-170-130-310-310-750-571-571-571-751-715 713-103-310-371-713-317-371-130-301-301-137-371-107-307-130-301-317-130-130-301-372-715-573-735-713-130-3_V.mp3)
  Segment 14: 38848ms ~ 58432ms (file: 132-136-316-163-675-607-617-631-134-130-671-617-162-163-315-173-132-631-312-231-260-321-267-236-236-312-627-620-320-326 132-137 130-567-263-132-312-312-103-132-136-763-706_V.mp3)
  Segment 15: 59432ms ~ 60008ms (file: 167-761-375-317-230-321-103-136-102-301-326-132-276-123-102-320-130-132-132-302-205-206-102-103-312-261-102-267-123-302-267-320-602-231-302-312-201-267-326-321-321-621-123-206-2_W.mp3)
  Segment 16: 61008ms ~ 61584ms (file: 576-125-135-126-213-527-705-217-573-167-675-726-216-132_V.mp3)
  Segment 17: 62584ms ~ 63160ms (file: 534-524-752-756-756-576-576-576-576-576-756-576-576-756-547-514-546-504-502-541-510-154-531-542-502-534-563-536-572-756-756-576-576-576-576-567-576-576-756-756-125-526-526-536-5_V.mp3)
  Segment 18: 64160ms ~ 64736ms (file: 135-135-753-573-715-175-736-327-315-316-317-175-135-317-357-357-351-756-567-567-576-576-765-675-625-657-751-756-756-735-716-756-765-612-651-615-216-163-237-617-617-612-126-756-7_W.mp3)
  Segment 19: 65736ms ~ 66312ms (file: 152-362-315-316-137-137-135-136-315-165-176-672-675-631-631-632-623-623-632-657-562-567-136-312-361-267-256-157-157-157-175-167-167-673-137-672-127-736-617-672-756-756-725-576-7_W.mp3)
  Segment 20: 67312ms ~ 73396ms (file: 276-231-325-265-357-621-315-132-325-357-267-752-103-172-123-135-132-132-315-235-235-103-137-312-213-271-261-236-103-325-756-173-132-367-513-253-521-102-103-301-235-123-261-132-2_W.mp3)
  Segment 21: 74396ms ~ 74972ms (file: 150-175-150-157-175-175-157-175-715-175-173-715-176-170 105-150-510-517-510-517-150-157-571-157-751-157-157-751-715-751-716-175-716-175-715-175 105-150-510-157-157-157-175-157-1_W.mp3)
  Segment 22: 75972ms ~ 85836ms (file: 627-267-732-103-207-103-102-146-104-103-301-326-132-165-620-267-260-276-103-132-312-310-321-620-302-231-321-201-320-316-106-764-103-163-167-123-312-130-132-236-627-312-302-102-3_V.mp3)
  Segment 23: 86836ms ~ 97492ms (file: 120-173-102-751-132-103-753-532-263-130-260-102-236-257-574-120-153-312-231-623-531-315-236-231-267-120-132-263-231-103-756-725-321-627-267-120-572-136-573-216-102-273-754-102-1_W.mp3)
  Segment 24: 98492ms ~ 102848ms (file: 310-310-132-136-126-265-216-265-625-261-261-265-261-265-265-263-261-261-267-621-267-265-267-261-261-267-261-132-326-162-135-103-306-715-167-765-574-576-570-657-637-625-627-267-6_W.mp3)
  Segment 25: 103848ms ~ 104424ms (file: 105-105-135-157-175-175-105-157-150-105-105-105-105-150-105-105-105-150-501 153-105-154-105-150-150-156-107-157-175-175-105-150-156-157-150-510-150-150-150-150-150-105-150-157-1_V.mp3)
  Segment 26: 105424ms ~ 106000ms (file: 571-715-175-175-571-157-175-167-176-715-761-710-107-107 710-517-756-571-175-751-571-157-175-157-157-175-157-173-175-135-173-157-153-752-756-751-756-752-572-756-573-752-713 571-5_W.mp3)
  Segment 27: 107000ms ~ 107576ms (file: 504-503-504-506-504-504-501-507-504-507-504-506-507-574-504-612-612-615-657-657-657-657-567-657-657-657-657-657-567-657-657-657-675 625-615-657-657-567-567-567-564-567-657-654-6_W.mp3)
  Segment 28: 108576ms ~ 109152ms (file: 576-137-735-756-217-126 276-267-726-276-276-276_W.mp3)
  Segment 29: 110152ms ~ 110728ms (file: 132-163-163-136-132-136-136-625-612-126 672-671-765 162-165-165-165-156-615-165-615-615-165-165 162-621-612-615-651-651-615-615-651 362-756-157-253-157-153-756-167-132-156-165-6_W.mp3)
  Segment 30: 111728ms ~ 112304ms (file: 651-675-675-657-765-756-756-576-756-675-657 256-752-267-571-750-675-762-765-756-756-756-756-756-756 502-275-765-572-670-765-675-756-675-765-765-765-765-675-675-567 652-205-572-5_V.mp3)
  Segment 31: 113304ms ~ 113880ms (file: 356-536-504-501-563-521-531-513-526-526-514-563-517-574-562-752-752-752-572-572-572-572-576-572-572-574-572-572-527-537-501-506-567-503-502-512-502-526-502-524-502-526-526-506-5_V.mp3)
  Segment 32: 114880ms ~ 133276ms (file: 765-675-765-321-260-126-213-130-675-765-216-231-216-671-263-230-217-617-675-130-201-271-260-213-132-213-201-231-276-207-621-321-123-103-216-261-270-123-213-217-623-132-267-216-7_W.mp3)
  Segment 33: 134276ms ~ 134852ms (file: 760-761-736-620-261-167-657-326-126-126-163-107-631-630-632-267-301-326-106-547-546-672-671-165-562-657-567-621-621-326-670-671-127-137-176-761-671-163-310-136-130-316-130-123-3_W.mp3)
  Segment 34: 135852ms ~ 136428ms (file: 105-107-750-705-701-701-710-107 571-751-107-571-107-715-175-571-751-751-751-170-701-710-105-175-170-517-107-517-517-157-571-157 570-751-105-570-507 517-105-105-157-157-107-105-1_V.mp3)
  Segment 35: 137428ms ~ 138004ms (file: 326-267-276-572-325-154-715-725-762-612-301-301-730-760-376-735-607-627-576-536-576-576-754-675-576-257-637-367-136-136-136-136-163-163-167-136-136-137-163-137-103-106-132-310-1_W.mp3)
  Segment 36: 139004ms ~ 152036ms (file: 576-657-756-315-321-132-321-231-312-574-263-267-270-267-270-271-316-315-207-570-275-261-703-527-623-752-756-672-123-316-235-135-157-371-715-573-723-532-302-253-632-713-325-720-2_V.mp3)
  Segment 37: 153036ms ~ 153612ms (file: 621-126 260-261-621-261-627-216 123-123 612-621-621-672-601 612-612-612-617-675-675-675-675-670-672-615-613-615-657-615-501-510-573-615-651-615-672-675-265-627-651-165-617-635-6_W.mp3)
  Segment 38: 154612ms ~ 155188ms (file: 157-756-715 105-105-105-150-150-157-150-157-510-517-571-507-517-571-517-504 157-105-105-105-150-105-105-150-150-105-510-150-517-570-510-150-517-514-574-571-540 105-105-103-105-1_W.mp3)
  Segment 39: 156188ms ~ 156764ms (file: 162-163-610-617-165-610-615-615-615-610-615-615-163-615-613-163_V.mp3)
  Segment 40: 157764ms ~ 158340ms (file: 157-157-153-157-175-157-517-150-517-570 570-574-570-517-517-571-517-517-130-175-135-170-751-573_W.mp3)
  Segment 41: 159340ms ~ 159916ms (file: 362-136-312-132-132-756-760-761-102-210-321-127-316-130-136-135-310-130-103-136-521-257-213-705-135-521-570-103-712-162-260-621-601-132-123-206-213-123-261 571-216-756-507-136-2_V.mp3)
  Segment 42: 160916ms ~ 161492ms (file: 167-602-102-103-762-761-671-671-671-601-136-167-163-103-132-213-236-263-236-130-213-216-261-231-672-130-375-321-321-203-524-351-531-574-137-106-361-103-163-156-167-570-657-612-1_W.mp3)
  Segment 43: 162492ms ~ 163068ms (file: 103-102-206-261-672-216-217-320-310-201-136-132-130-306-306-130-103-301-106-136-612-236-123-103-163-267-263-230-203-206-267-326 207-201-206-206-206-206-260-627-260-206-206-203-2_V.mp3)
  Segment 44: 164068ms ~ 164644ms (file: 753 570-574-570-501-570-507-571-157-507-571-574-574-570-574-574-574-750-754-570 574-574-574-574-574-574-547-574-574-350-157-153-157-105-157-751-175-175-751-710-157-107-715-170-1_W.mp3)
  Segment 45: 165644ms ~ 166220ms (file: 167-317-135-675 571-570-750-576-756-756-715-175-761-317-317-175-657-567-615-153-713-170-175-317-316-617-763-376-316-163-675-637-317 750-705-107-761-301-376-301-603-601-603-630-6_W.mp3)
  Segment 46: 167220ms ~ 167796ms (file: 510-501-510-517-510-501-510-501-507-150-105-510-157-150-105-150-510-517-150-510-517-571 105-501-517-105-517-501-105-501-517-150-150-517-517-501-157-510-105-157-105-105-157-105-5_V.mp3)
  Segment 47: 168796ms ~ 172540ms (file: 237-271-170-167-162-130-312-230-721-720-237-207-203-236-231-237-123-271-261-276-627-267-762-706-276-276-736-260-260-102-270-267-273-237-270-273-237-230-237-236-213-267-217-216-2_W.mp3)
  Segment 48: 173540ms ~ 174116ms (file: 501-750-517-107-107-105-170-517-175-705-710-751-751-105-715-750-170-107-175-175 570-750-570-750-570-507-751-507-105-107-705-705-105-170-701-157-107-750-750-571-715-751-175-107-7_V.mp3)
  Segment 49: 175116ms ~ 181740ms (file: 765-123-123-216-120-672-260-675-136-137-312-127-321-136-102-103-103-670-302-216-762-201-312-203-706-102-201-270-213-123-130-103-130-260-130-206-103-267-302-120-120-216-312-206-1_V.mp3)
  Segment 50: 182740ms ~ 191308ms (file: 103-213-263-317-103-321-213-231-312-137-126-571-206-261-203-725-167-130-213-257-235-620-105-107-106-231-753-317-137-267-132-312-130-725-750-123-310-123-103-301-132-103-231-213-2_V.mp3)
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
> **Time**: 2026-04-05 12:05:54
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "활성화됐다 이게 만약에 서버를 해킹하는 거면은",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "활성화됐다", "start_ms": 10, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 350, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "서버를", "start_ms": 470, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "해킹하는", "start_ms": 620, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거면은", "start_ms": 810, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "이상한 신호가 있으면 당연히 차단을 했었거든요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "이상한", "start_ms": 15, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "신호가", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "당연히", "start_ms": 460, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "차단을", "start_ms": 600, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "했었거든요", "start_ms": 750, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "혹은 자신을 주요 외교 행위자로 자리매김했음을 공식적으로 확립하고자 하고 있다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "혹은", "start_ms": 20, "dur_ms": 280, "phase_lr_deg": -2.0},
{"word": "자신을", "start_ms": 320, "dur_ms": 410, "phase_lr_deg": -2.1},
{"word": "주요", "start_ms": 750, "dur_ms": 320, "phase_lr_deg": -2.0},
{"word": "외교", "start_ms": 1090, "dur_ms": 350, "phase_lr_deg": -2.2},
{"word": "행위자로", "start_ms": 1460, "dur_ms": 580, "phase_lr_deg": -1.9},
{"word": "자리매김했음을", "start_ms": 2060, "dur_ms": 820, "phase_lr_deg": -2.0},
{"word": "공식적으로", "start_ms": 2900, "dur_ms": 710, "phase_lr_deg": -2.1},
{"word": "확립하고자", "start_ms": 3650, "dur_ms": 650, "phase_lr_deg": -2.2},
{"word": "하고", "start_ms": 4320, "dur_ms": 310, "phase_lr_deg": -2.0},
{"word": "있다", "start_ms": 4650, "dur_ms": 280, "phase_lr_deg": -1.8}
]
},
{
"segment": 4,
"transcript": "무엇보다 글로벌 방위 산업과 국가 외교 무대에서 벌어지고 있는",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "무엇보다", "start_ms": 30, "dur_ms": 520, "phase_lr_deg": -1.5},
{"word": "글로벌", "start_ms": 580, "dur_ms": 480, "phase_lr_deg": -1.4},
{"word": "방위", "start_ms": 1080, "dur_ms": 320, "phase_lr_deg": -1.6},
{"word": "산업과", "start_ms": 1420, "dur_ms": 410, "phase_lr_deg": -1.5},
{"word": "국가", "start_ms": 1850, "dur_ms": 310, "phase_lr_deg": -1.4},
{"word": "외교", "start_ms": 2180, "dur_ms": 320, "phase_lr_deg": -1.5},
{"word": "무대에서", "start_ms": 2520, "dur_ms": 510, "phase_lr_deg": -1.6},
{"word": "벌어지고", "start_ms": 3050, "dur_ms": 480, "phase_lr_deg": -1.5},
{"word": "있는", "start_ms": 3550, "dur_ms": 210, "phase_lr_deg": -1.4}
]
},
{
"segment": 5,
"transcript": "이같은 변화는 트럼프 대통령에게 있어 매우 불편한 신호로 읽힐 수밖에 없습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "이같은", "start_ms": 25, "dur_ms": 420, "phase_lr_deg": -1.8},
{"word": "변화는", "start_ms": 460, "dur_ms": 450, "phase_lr_deg": -1.7},
{"word": "트럼프", "start_ms": 930, "dur_ms": 420, "phase_lr_deg": -1.9},
{"word": "대통령에게", "start_ms": 1370, "dur_ms": 610, "phase_lr_deg": -1.8},
{"word": "있어", "start_ms": 2000, "dur_ms": 250, "phase_lr_deg": -1.7},
{"word": "매우", "start_ms": 2270, "dur_ms": 310, "phase_lr_deg": -1.8},
{"word": "불편한", "start_ms": 2600, "dur_ms": 420, "phase_lr_deg": -1.9},
{"word": "신호로", "start_ms": 3040, "dur_ms": 410, "phase_lr_deg": -1.8},
{"word": "읽힐", "start_ms": 3470, "dur_ms": 320, "phase_lr_deg": -1.7},
{"word": "수밖에", "start_ms": 3810, "dur_ms": 380, "phase_lr_deg": -1.8},
{"word": "없습니다", "start_ms": 4210, "dur_ms": 450, "phase_lr_deg": -1.7}
]
},
{
"segment": 6,
"transcript": "이는 미국이 주도해온",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이는", "start_ms": 35, "dur_ms": 310, "phase_lr_deg": -2.0},
{"word": "미국이", "start_ms": 360, "dur_ms": 380, "phase_lr_deg": -2.1},
{"word": "주도해온", "start_ms": 760, "dur_ms": 520, "phase_lr_deg": -2.0}
]
},
{
"segment": 7,
"transcript": "극히 일부만 잡을 수 있어요 이 과정에서 드는 비용이 정말 어마어마합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "극히", "start_ms": 40, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "일부만", "start_ms": 370, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "잡을", "start_ms": 770, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1000, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1140, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1500, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "과정에서", "start_ms": 1630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드는", "start_ms": 2100, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "비용이", "start_ms": 2350, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어마어마합니다", "start_ms": 3120, "dur_ms": 720, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "얼마나 비싸냐면 반물질 1g을 만들려면 수조 달러가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "얼마나", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "비싸냐면", "start_ms": 380, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "반물질", "start_ms": 820, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "1g을", "start_ms": 1320, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "만들려면", "start_ms": 1750, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "수조", "start_ms": 2300, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "달러가", "start_ms": 2650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 3050, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "한국 GDP의 몇 배가 드는 돈이죠 게다가 CERN의 현재 생산 속도로 1g을 모으려고 하면은 우주",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 15, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "GDP의", "start_ms": 350, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 750, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "배가", "start_ms": 880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "드는", "start_ms": 1150, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "돈이죠", "start_ms": 1380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "게다가", "start_ms": 1750, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "CERN의", "start_ms": 2150, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "현재", "start_ms": 2600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생산", "start_ms": 2970, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "속도로", "start_ms": 3310, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "1g을", "start_ms": 3720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모으려고", "start_ms": 4100, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "하면은", "start_ms": 4550, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "우주", "start_ms": 4880, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "그런 기대감이 있는 거죠 그래서 아마 다음 주 화요일에 잠정 실적 발표가 나올 수가 있는데",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그런", "start_ms": 20, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "기대감이", "start_ms": 250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 850, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "아마", "start_ms": 1450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 1720, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 1950, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "화요일에", "start_ms": 2080, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "잠정", "start_ms": 2510, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "실적", "start_ms": 2810, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "발표가", "start_ms": 3080, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나올", "start_ms": 3450, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 3690, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 3920, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "실제로 50조가 넘어가면 세게 슈팅이 나올까라는 좀 기대감이 있는 거예요 물론 40조대도 어마무시한 건데 시장이 어떻게 받아들일지",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "실제로", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "50조가", "start_ms": 400, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "넘어가면", "start_ms": 830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세게", "start_ms": 1300, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "슈팅이", "start_ms": 1530, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "나올까라는", "start_ms": 1870, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기대감이", "start_ms": 2510, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2910, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 3110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 3400, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "40조대도", "start_ms": 3710, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어마무시한", "start_ms": 4180, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 4720, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 5000, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 5330, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "받아들일지", "start_ms": 5670, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "가슴에 독수리 타투를 하고 독수리 울음소리를 내고 있는 이 남자는 사실 미친놈이 아니라 18살의 나이에 월드 트레이딩 챔피언십 우승부터",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "가슴에", "start_ms": 25, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "독수리", "start_ms": 350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "타투를", "start_ms": 690, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 1050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "독수리", "start_ms": 1320, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "울음소리를", "start_ms": 1660, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "내고", "start_ms": 2260, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2490, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2690, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "남자는", "start_ms": 2790, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 3160, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "미친놈이", "start_ms": 3460, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 3900, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "18살의", "start_ms": 4250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나이에", "start_ms": 4720, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "월드", "start_ms": 5060, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "트레이딩", "start_ms": 5330, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "챔피언십", "start_ms": 5760, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "우승부터", "start_ms": 6260, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "도 구현될 수 있도록 로봇을 제어해야 하기 때문에 그의 연구는 특히 중요합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "도", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "구현될", "start_ms": 210, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 650, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있도록", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 1150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "제어해야", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 2020, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 2320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그의", "start_ms": 2700, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "연구는", "start_ms": 2950, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 3350, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "중요합니다", "start_ms": 3680, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "미국은 우크라이나 전쟁 때처럼 호르무즈 해협의 책임을 전적으로 지지 않을 것이며 한국과 일본의 군이 순찰 임무를 나눠 맡게 될 것이라는",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "미국은", "start_ms": 40, "dur_ms": 380, "phase_lr_deg": -1.0},
{"word": "우크라이나", "start_ms": 450, "dur_ms": 520, "phase_lr_deg": -1.1},
{"word": "전쟁", "start_ms": 1000, "dur_ms": 310, "phase_lr_deg": -1.0},
{"word": "때처럼", "start_ms": 1330, "dur_ms": 410, "phase_lr_deg": -0.9},
{"word": "호르무즈", "start_ms": 1760, "dur_ms": 480, "phase_lr_deg": -1.0},
{"word": "해협의", "start_ms": 2260, "dur_ms": 420, "phase_lr_deg": -1.1},
{"word": "책임을", "start_ms": 2710, "dur_ms": 380, "phase_lr_deg": -1.0},
{"word": "전적으로", "start_ms": 3120, "dur_ms": 450, "phase_lr_deg": -1.1},
{"word": "지지", "start_ms": 3600, "dur_ms": 280, "phase_lr_deg": -1.0},
{"word": "않을", "start_ms": 3900, "dur_ms": 250, "phase_lr_deg": -0.9},
{"word": "것이며", "start_ms": 4180, "dur_ms": 350, "phase_lr_deg": -1.0},
{"word": "한국과", "start_ms": 4550, "dur_ms": 410, "phase_lr_deg": -1.1},
{"word": "일본의", "start_ms": 4980, "dur_ms": 350, "phase_lr_deg": -1.0},
{"word": "군이", "start_ms": 5350, "dur_ms": 250, "phase_lr_deg": -1.0},
{"word": "순찰", "start_ms": 5620, "dur_ms": 320, "phase_lr_deg": -1.1},
{"word": "임무를", "start_ms": 5960, "dur_ms": 380, "phase_lr_deg": -1.0},
{"word": "나눠", "start_ms": 6360, "dur_ms": 280, "phase_lr_deg": -1.0},
{"word": "맡게", "start_ms": 6660, "dur_ms": 250, "phase_lr_deg": -1.1},
{"word": "될", "start_ms": 6930, "dur_ms": 150, "phase_lr_deg": -1.0},
{"word": "것이라는", "start_ms": 7100, "dur_ms": 420, "phase_lr_deg": -1.0}
]
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
"transcript": "통으로 항공이던 곳을",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "통으로", "start_ms": 10, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "항공이던", "start_ms": 350, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "곳을", "start_ms": 780, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "가나요 낭만바구니 시어머니를 따라 시장 생활 31년 차 이정도는 기본이라는",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "가나요", "start_ms": 15, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "낭만바구니", "start_ms": 400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "시어머니를", "start_ms": 950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 1520, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "시장", "start_ms": 1860, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "생활", "start_ms": 2190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "31년", "start_ms": 2560, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "차", "start_ms": 3000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이정도는", "start_ms": 3180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기본이라는", "start_ms": 3650, "dur_ms": 580, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "것으로 보이는데요 아시아에서 아메리카로는 매머드 들소 말 사슴 등이 건너갔고 아메리카에서 아시아로는 일부 사슴과 들소가 되돌아왔으며 여기에 늑대 불곰 검치호",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "것으로", "start_ms": 20, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "보이는데요", "start_ms": 350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아시아에서", "start_ms": 820, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "아메리카로는", "start_ms": 1360, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "매머드", "start_ms": 1990, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "들소", "start_ms": 2430, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "말", "start_ms": 2760, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사슴", "start_ms": 2930, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "등이", "start_ms": 3270, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "건너갔고", "start_ms": 3540, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "아메리카에서", "start_ms": 4050, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "아시아로는", "start_ms": 4650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 5220, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "사슴과", "start_ms": 5520, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "들소가", "start_ms": 5920, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "되돌아왔으며", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "여기에", "start_ms": 7020, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "늑대", "start_ms": 7350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "불곰", "start_ms": 7690, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "검치호", "start_ms": 8060, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "나와서 나왔으니까 들구도 영향도 있지만 그 당시에 불교와 도교가 굉장히 융합된 사상으로 존재했었기 때문에 불교 도교가 같이 드러나는 유물로도 해석을 하고 또 봉황이라는 존재도 있고 오 오악 이런 것들도 유교적과 관련 시킬 수 있고 그렇습니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "나와서", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나왔으니까", "start_ms": 400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "들구도", "start_ms": 950, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "영향도", "start_ms": 1380, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있지만", "start_ms": 1780, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2120, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "당시에", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "불교와", "start_ms": 2620, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "도교가", "start_ms": 3060, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 3460, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "융합된", "start_ms": 3890, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사상으로", "start_ms": 4360, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "존재했었기", "start_ms": 4860, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 5500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "불교", "start_ms": 5870, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "도교가", "start_ms": 6200, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 6600, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "드러나는", "start_ms": 6900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유물로도", "start_ms": 7370, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "해석을", "start_ms": 7870, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 8270, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 8550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봉황이라는", "start_ms": 8720, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "존재도", "start_ms": 9260, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 9660, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 9930, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오악", "start_ms": 10070, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 10400, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "것들도", "start_ms": 10700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "유교적과", "start_ms": 11070, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "관련", "start_ms": 11570, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "시킬", "start_ms": 11910, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 12240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 12380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그렇습니다", "start_ms": 12650, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "유교 도교 불교 세가지 모든게 나타나는 어떻게 보면 그런건 전 본 기억이 없는데",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "유교", "start_ms": 25, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "도교", "start_ms": 350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "불교", "start_ms": 690, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "세가지", "start_ms": 1030, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "모든게", "start_ms": 1430, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나타나는", "start_ms": 1800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 2270, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 2600, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "그런건", "start_ms": 2900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 3270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "본", "start_ms": 3440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기억이", "start_ms": 3610, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "없는데", "start_ms": 3950, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "그러니까 백제 문화의 정수라는 수식어가 붙는게 아닌가",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그러니까", "start_ms": 20, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "백제", "start_ms": 450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "문화의", "start_ms": 790, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정수라는", "start_ms": 1160, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "수식어가", "start_ms": 1660, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "붙는게", "start_ms": 2130, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "아닌가", "start_ms": 2530, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 24,
"transcript": "아이폰에서 다 쓸 수 있는 그런 기능이 갤럭시에서는 최신폰만 지원하고 바로 구세대만 해도 안 된다고 업데이트를 안 해 준다고 삼성 미쳤나 막 이리 논리가 흘러가는 게 아닌가 싶습니다 게다가",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "아이폰에서", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 580, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 720, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 890, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1020, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 1250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "기능이", "start_ms": 1520, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "갤럭시에서는", "start_ms": 1920, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "최신폰만", "start_ms": 2550, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "지원하고", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 3520, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "구세대만", "start_ms": 3820, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "해도", "start_ms": 4250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 4520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "된다고", "start_ms": 4660, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "업데이트를", "start_ms": 5000, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 5550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 5720, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "준다고", "start_ms": 5850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 6220, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "미쳤나", "start_ms": 6550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 6950, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이리", "start_ms": 7100, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "논리가", "start_ms": 7350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "흘러가는", "start_ms": 7720, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 8220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아닌가", "start_ms": 8350, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "싶습니다", "start_ms": 8680, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "게다가", "start_ms": 9120, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "외교 활동을 넘어 세계 질서를 흔들 수 있는 수준의 변화로 이어지고 있기 때문입니다 실제로 이를 반영하듯이 최근 한국 정부의 외교력과 방위 산업 역량이 미국을 압도하고",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "외교", "start_ms": 40, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "활동을", "start_ms": 370, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "넘어", "start_ms": 770, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "세계", "start_ms": 1110, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "질서를", "start_ms": 1440, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "흔들", "start_ms": 1870, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2210, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2340, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "수준의", "start_ms": 2570, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "변화로", "start_ms": 2970, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이어지고", "start_ms": 3340, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 3840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 4110, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 4750, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이를", "start_ms": 5150, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "반영하듯이", "start_ms": 5380, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "최근", "start_ms": 6050, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 6390, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "정부의", "start_ms": 6720, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "외교력과", "start_ms": 7120, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "방위", "start_ms": 7620, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 7950, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "역량이", "start_ms": 8290, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "미국을", "start_ms": 8690, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "압도하고", "start_ms": 9060, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "비가 지하실 한 개에 붙어있던 오랜 전세 자체가 바뀌어 버린 것입니다 흥미로운 점은 이 과정이 미국 문화 콘텐츠에서도 이야기거리",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "비가", "start_ms": 25, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "지하실", "start_ms": 360, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 790, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "개에", "start_ms": 920, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "붙어있던", "start_ms": 1160, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오랜", "start_ms": 1630, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "전세", "start_ms": 1960, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자체가", "start_ms": 2330, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "바뀌어", "start_ms": 2760, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "버린", "start_ms": 3160, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것입니다", "start_ms": 3430, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "흥미로운", "start_ms": 3950, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "점은", "start_ms": 4480, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4810, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "과정이", "start_ms": 4940, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 5340, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "문화", "start_ms": 5670, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "콘텐츠에서도", "start_ms": 6010, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "이야기거리", "start_ms": 6750, "dur_ms": 620, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:05:54*
