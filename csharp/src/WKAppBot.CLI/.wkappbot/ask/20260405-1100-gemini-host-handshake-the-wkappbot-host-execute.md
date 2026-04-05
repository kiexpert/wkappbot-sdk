# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17708  time=10:58:02  uptime=24543s  mem=441MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_105750.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 501-157-150-501-510-150-153-105-105-150-105-105-150-150-501-150-510-157-510-150 150-517-153-175-513-157-517-150-501-517-501-501-150-105-103-153-501-150-157-517-501-150-517-507-5_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 625-625-652-615-651-651-612-625-625-621-625-651-657-651-615-625-615-651-651-615-615-612-613-162 125-250-625-621-612-625-651-615-621-651-651-654-651-651-657-651-657-654-657-652-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 126-132-103-132-103-312 132-132-132-103_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 362-136-163-163-136-613-163-160-132-132-312-316-136-316-165-126-167-136-103-631-167-103-126-162-126-162-126-132-136-310-132-136-312-125-632-367-136-263-103-603-612-216-162-160-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 130-103-143-314-103-104-137-103-103-134-134 315-413-143-310-314-134-304-103-134-134-103-103-130-310-130-134-143-134-301-134-134-103-134-314-134-341 357-537-135-401-143-301-143-1_V.mp3)
  Segment 6: 7880ms ~ 18572ms (file: 231-102-103-236-526-267-136-135-206-260-135-103-103-201-203-526-267-217-312-617-762-762-706-601-176-250-320-261-237-132-627-317-130-310-573-321-267-132-631-263-237-576-576-137-1_W.mp3)
  Segment 7: 19572ms ~ 20148ms (file: 756-517 736-106-175-176-167-137-167-162-126-632-267-627-625-635-326-672-763-167-327-723-762-736-576-763-671-671-675-316-673-672-657-136-163-574-627-627-167-126-162-615-137-672-6_W.mp3)
  Segment 8: 21148ms ~ 30328ms (file: 130-312-237-132-123-210-260-216-261-123-120-267-102-163-102-210-120-673-123-312-312-231-130-231-672-260-102-201-260-216-102-230-267-120-210-612-260-103-267-503-260-312-201-213-7_W.mp3)
  Segment 9: 31328ms ~ 33236ms (file: 123-321-230-213-126-162-103-102-130-103-103-136-106-157-627-260-621-162-612-136-310-123-103-176-127-137-132-123-132-167-162-162-163-162-132 576-576-135-765-576-576-576-602-160-1_V.mp3)
  Segment 10: 34236ms ~ 34812ms (file: 231-213-123-213-235-265-132-123-321-231-312-213-132-213-123-126-126-123-132-132-321-312-321-352-312-315-321-315-321-315-315-537-357-253-327-532-567-657-562-523-256-351-315-357-3_W.mp3)
  Segment 11: 35812ms ~ 36388ms (file: 760-276-726-236-235-726-627-217-325-123-137-312-153-275-507-572-753-327-321-312-507-132-152-753-752-215-265-725-573-571-632-502-327-532-675-137-570-367-570-215-275-276-762-206-5_V.mp3)
  Segment 12: 37388ms ~ 44624ms (file: 310-320-753-107-173-327-237-715-760-260-321-752-726-206-126-362-312-756-312-752-107-273-310-263-267-213-102-230-267-267-320-120-754-270-207-230-321-310-120-102-102-102-207-260-3_W.mp3)
  Segment 13: 45624ms ~ 46200ms (file: 761-763-203-527-702-372-132-312-531-312-130-310-321-325-126-236-123-132-217-126-761-103-132-207-327-213-231-312-130-732-263-623-327-362-216-130-103-302-312-326-236-321-312-312-3_W.mp3)
  Segment 14: 47200ms ~ 57712ms (file: 320-321-123-276-132-102-132-627-267-310-627-627-267-172-203-127-102-310-231-213-257-235-273-325-231-263-267-231-236-267-672-210-231-327-732-712-237-237-321-120-276-321-231-120-3_W.mp3)
  Segment 15: 58712ms ~ 68072ms (file: 261-316-216-715-702-362-541-130-267-326-271-267-726-725-362-367-362-721-762-671-621-217-752-176-576-756-756-756-756-327-316-573-273-751-675-123-312-326 106-702-706-317-671-673-1_W.mp3)
  Segment 16: 69072ms ~ 75516ms (file: 237-260-607-103-306-326-321-137-167-160-103-361-316-163-316-371-316-612-637-367-675-637-236-123-236-321-326-316-327-713-123-176-216-125-257-253-263-236-162-362-236-236-231-231-3_W.mp3)
  Segment 17: 76516ms ~ 83356ms (file: 276-201-542-301-216-176-751-756-176-263-216-751-167-517-103-163-216-103-261-102-612-261-230-201-107-176-107-302-132-312-301-312-763-267-103-132-216-670-765-103-756-761-756-716-7_W.mp3)
  Segment 18: 84356ms ~ 84932ms (file: 567-564-561-567-567-561-567-567-567-567-567-576-567-576-567 516-516-561-567-516-567-576-567-576-567-576-574-576 615-561-516-567-567-576-576-576-576-574-576-576-574-576-574-576-5_W.mp3)
  Segment 19: 85932ms ~ 86508ms (file: 571-517-571-571-571-517-571-517 501-157-175-571-517-517-157-517-517-571-571-517-517-157-510-157-510-517-517 157-571-715-173-715-715-175-150-507-570-570-175-571-570-507-507-510-5_V.mp3)
  Segment 20: 87508ms ~ 88084ms (file: 231-132-362-263-321-572-567-563 316_W.mp3)
  Segment 21: 89084ms ~ 89660ms (file: 260-236 625-615-625-651-652-651-615-625-652-615-625-612-651-652 625-652-657-615-657-651-654-654-625-612-621-621-621-621-625-625-621-260 621-625-652-652-652-652-652-625-625-625-6_W.mp3)
  Segment 22: 90660ms ~ 95340ms (file: 132-726-312-317-765-627-627-602-301-172-362-326-621-263-261-326-231-216-276-126-123-216-132-132-312-132-126-756-126-236-261-132-326-217-137-132-213-231-152-236-312-625-206-132-1_W.mp3)
  Segment 23: 96340ms ~ 96916ms (file: 106-106-103-163-136-126-162-126-162-106-102-106-102-162-163-136-132-135-163_V.mp3)
  Segment 24: 97916ms ~ 106736ms (file: 267-236-752-267-762-765-260-230-236-126-263-267-216-167-213-123-126-617-231-102-756-273-256-572-265-236-267-167-271-607-326-206-213-207-237-120-231-572-532-273-230-263-267-260-2_W.mp3)
  Segment 25: 107736ms ~ 108312ms (file: 263-632-621-617-712-573-753-576-753-126-120-265-527-572-572-507-132-310-230-236-230-263-612-162-132-613-674 310-137-160-103-103-301-302-136-621-301-130-310-673-571-132-123-120-7_W.mp3)
  Segment 26: 109312ms ~ 109888ms (file: 526-257-572-527-572-576-576-765-276-562-257-752-652-625_V.mp3)
  Segment 27: 110888ms ~ 111464ms (file: 570-150-751-750-571-750-750 574-574-574-574-547-574-574-574-574-574-574-574-574-574 574-574-574-573-574-570-570-105-570-574-547 574-574 547-547-574_W.mp3)
  Segment 28: 112464ms ~ 120816ms (file: 163-136-165-163-136-371-275-756-576-265-567-657-567-675-326-307-652-653-160-675-236-326-675-657-265-120-756-657-210-213-237-176-576-130-203-310-321-312-126-125-270-231-103-136-1_W.mp3)
  Segment 29: 121816ms ~ 122392ms (file: 501-571 105-157-510-105-105-510-105-510-501-517-507-510-157-570-507-571-501-501-507 105-105-105-510-571-105-501-150-501-105-157-150-150-510-501-150-510-501-150-501-150-507-501-5_V.mp3)
  Segment 30: 123392ms ~ 123968ms (file: 571-571-765 153-153-175-150-175-175-175-157-157-105 150-105-150-507-517-570-571-517-517-517-105-175-175-107-107-157-105-175-170-751-750-574 153-150-510-157-510-517-571-571-571-5_W.mp3)
  Segment 31: 124968ms ~ 131448ms (file: 210-132-132-123-312-235-236-236-206-136-123-132-231-210-321-236-132-312-310-132-213-123-130-312-230-231-120-120-127-213-127-123-213-132-312-132-132-312-123-231-321-213-321-103-1_V.mp3)
  Segment 32: 132448ms ~ 139144ms (file: 312-231-135-106-137-103-107-103-130-136-107-103-271-236-126-762-317-536-267-120-126-316-316-312-132-132-231-213-123-130-137-173-130-132-132-312-136-136-132-316-130-762-326-326-3_W.mp3)
  Segment 33: 140144ms ~ 140720ms (file: 751-753-571-751-753-751-715-715-173-751-175-715-751-751-752-751-751-731-751 573-751-751-371-713-753-753-751-571-137-173-751-753-751-710-753-756-753-756-751-715-751-715-756-573 1_V.mp3)
  Segment 34: 141720ms ~ 142296ms (file: 613-613-612-621-632-136-261-361-631-312-321-312-312-317-316-152-671-627-571-216-102-201-120-126-136-517-132-571-135-705-127-231-157-701-143-257-752-627-132-137-172-126-316-713-7_V.mp3)
  Segment 35: 143296ms ~ 154600ms (file: 675-765-637-167-106 126 625-652-163-176-621-216 132 162-657-162 510-617-627-675-762-162-261-756-765-765-612-167-675-631-621-106-301-356-163-657-651 173-321 625-265-261-173 651-1_W.mp3)
  Segment 36: 155600ms ~ 156176ms (file: 750-751-571-756-570-751-571-715-571-750-756-756 750-570-750-750-750-751-570-751-756-750-751-750-750-751-750-751-751-715-751-715-756 570-750-751-750-750-751-750-750-751-751-750-7_V.mp3)
  Segment 37: 157176ms ~ 168912ms (file: 103-312-130-763-750-107-130-132-102-163-176-167-260-267-321-236-230-537-312-361-123-213-312-261-752-270-230-261-527-256-236-267-236-627-321-126-231-123-321-175-762-675-267-210-1_W.mp3)
  Segment 38: 169912ms ~ 179704ms (file: 237-326-130-267-627-267-132-236-267-263-236-574-106-236-236-257-326-361-361-103-256-236-263-312-231-732-642-267-132-316-761-231-103-210-627-231-236-261-326-326-123-236-201-263-3_W.mp3)
  Segment 39: 180704ms ~ 181280ms (file: 136-175-316-132-137-136-156-617-102-106-627-623-106-617-163-103-105-132-167-130-136-103-261-153-165-163-103-123-103-103-106-103-107-137-102-362-106-156-316-153-173-136-132-765 6_V.mp3)
  Segment 40: 182280ms ~ 182856ms (file: 517-517-157-175-173 105-105-105-105-170-715-175-176-175-175-175-175-517-570-157-150-157-157-157-517-175-175-157-571-157-137 154-105-105-153-105-105-105-107-170-157-175-157-516-1_V.mp3)
  Segment 41: 183856ms ~ 193000ms (file: 250-102-130-206-312-130-526-265-756-675-132-105-170-270-735-210-230-203-571-756-237-230-123-173-673-103-753-107-572-103-157-157-153-152-103-172-103-107-230-203-320-230-206-123-5_W.mp3)
  Segment 42: 194000ms ~ 194576ms (file: 235-507-762-750-567-570-237-756-267-257-325-235-276-750-236-623-275-316-751-236-726-706-612-765-713-570-362-607-672-263-705-367-375-627-576-762-276-763-576-276-750-237-325-321-3_V.mp3)
  Segment 43: 195576ms ~ 196152ms (file: 132-103-132-130-236-321-625-265-632-265-524-756-576-750-765-267-267-627-265-260-267-326-675-675-756-756-756-132-172 576-657-765-321-103-162-617-561-102-213-236-601-167-632-320-1_W.mp3)
  Segment 44: 197152ms ~ 197728ms (file: 103-137-137-136-136-103-753-756-736-375-756-765-635-756-635-637-657-653-675-375-736-376-736-173-173-321-675-627-657 623-745_W.mp3)
  Segment 45: 198728ms ~ 199304ms (file: 167-167-762-765-762-267-623-163-163-623-352-261-310-137-103-103-162-572-701-173-136-132-316-726-310-137-710-765-106-671-106-126-576-762-316-612-106-761-576-157-107-156-532-630-3_W.mp3)
  Segment 46: 200304ms ~ 209196ms (file: 106-126-267-216-130-312-357-176-132-267-321-231-265-326-236-231-130-312-213-260-263-312-263-130-627-546-312-302-263-263-206-231-236-263-230-267-263-260-671-132-312-261-362-312-3_W.mp3)
  Segment 47: 210196ms ~ 210772ms (file: 103-103-162-603-132-601-106-651-165-317-103-154-370-162-165-162-132-613-106-517-576-157-107-103-675-126-162-162-106-132-103-103-136-167-132-103-104-126-261-172-627-620-107-106-1_V.mp3)
  Segment 48: 211772ms ~ 212348ms (file: 751-715-751-751-751-715-751-756-751-756-751-751-756-751-751-756-753-751 150-510-510-150-501-510-751-750-715-715-175-715-715-751-715-715-715-751-756-756-756-751-751-756-751 150-1_W.mp3)
  Segment 49: 213348ms ~ 219504ms (file: 103-510-320-370-574-756-576-756-736-103-276-270-765-675-567-361-123-132-231-753-576-601-756-750-751-576-507-607-570-312-316-617-156-301-130-310-106-165-310-175-165-106-675-167-1_W.mp3)
  Segment 50: 220504ms ~ 227056ms (file: 136-103-123-517-517-257-513-517-517-517-517-135-153-517-571-137-157-135-316-316-163-136-312-301-365-572-574-574-574-574-751-571-576-574-320-103-510-137-103-627-170-102-102-150-1_W.mp3)
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
> **Time**: 2026-04-05 11:00:07
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "그 당시 발굴 당시 모습을 보면 뚜껑과 몸체가 분리되어서 나왔습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 501,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 501, "dur_ms": 157, "phase_lr_deg": 0.0},
{"word": "당시", "start_ms": 658, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "발굴", "start_ms": 808, "dur_ms": 501, "phase_lr_deg": 0.0},
{"word": "당시", "start_ms": 1309, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "모습을", "start_ms": 1819, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 1969, "dur_ms": 153, "phase_lr_deg": 0.0},
{"word": "뚜껑과", "start_ms": 2122, "dur_ms": 105, "phase_lr_deg": 0.0},
{"word": "몸체가", "start_ms": 2227, "dur_ms": 105, "phase_lr_deg": 0.0},
{"word": "분리되어서", "start_ms": 2332, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나왔습니다.", "start_ms": 2482, "dur_ms": 105, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "네, 그 당시 몸체와 뚜껑을 결합했는지는 전혀 알 수가 없습니다.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 625,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 625, "dur_ms": 625, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1250, "dur_ms": 652, "phase_lr_deg": 0.0},
{"word": "당시", "start_ms": 1902, "dur_ms": 615, "phase_lr_deg": 0.0},
{"word": "몸체와", "start_ms": 2517, "dur_ms": 651, "phase_lr_deg": 0.0},
{"word": "뚜껑을", "start_ms": 3168, "dur_ms": 651, "phase_lr_deg": 0.0},
{"word": "결합했는지는", "start_ms": 3819, "dur_ms": 612, "phase_lr_deg": 0.0},
{"word": "전혀", "start_ms": 4431, "dur_ms": 625, "phase_lr_deg": 0.0},
{"word": "알", "start_ms": 5056, "dur_ms": 625, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 5681, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "없습니다.", "start_ms": 6302, "dur_ms": 625, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "근데 다만 저희가 전시를 이렇게 하고 있는 이유는...",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 126,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 126, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "다만", "start_ms": 258, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 361, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "전시를", "start_ms": 493, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 596, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 908, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1040, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "이유는...", "start_ms": 1172, "dur_ms": 132, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "그 봉황의 가슴 중앙에 두 개의 날된 구조를 가지고 있다는 겁니다.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 362,
"audio_type": "speech",
"words": [
{"word": "그", "start_ms": 362, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "봉황의", "start_ms": 498, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "가슴", "start_ms": 661, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "중앙에", "start_ms": 824, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 960, "dur_ms": 613, "phase_lr_deg": 0.0},
{"word": "개의", "start_ms": 1573, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "날된", "start_ms": 1736, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "구조를", "start_ms": 1896, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 2028, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 2160, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "겁니다.", "start_ms": 2472, "dur_ms": 316, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "이런 구조의 차이는 각 나라의 환경과 계절, 그리고 라이프스타일이 반영되어 발생한 것인데요.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 130, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "구조의", "start_ms": 233, "dur_ms": 143, "phase_lr_deg": 0.0},
{"word": "차이는", "start_ms": 376, "dur_ms": 314, "phase_lr_deg": 0.0},
{"word": "각", "start_ms": 690, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "나라의", "start_ms": 793, "dur_ms": 104, "phase_lr_deg": 0.0},
{"word": "환경과", "start_ms": 897, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "계절,", "start_ms": 1034, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1137, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "라이프스타일이", "start_ms": 1240, "dur_ms": 134, "phase_lr_deg": 0.0},
{"word": "반영되어", "start_ms": 1374, "dur_ms": 134, "phase_lr_deg": 0.0},
{"word": "발생한", "start_ms": 1508, "dur_ms": 315, "phase_lr_deg": 0.0},
{"word": "것인데요.", "start_ms": 1823, "dur_ms": 413, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "각 나라의 사람들은 오랫동안 이어져 온 이런 집 구조에 익숙해...",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "각", "start_ms": 231, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "나라의", "start_ms": 333, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "사람들은", "start_ms": 436, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "오랫동안", "start_ms": 672, "dur_ms": 526, "phase_lr_deg": 0.0},
{"word": "이어져", "start_ms": 1198, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "온", "start_ms": 1465, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 1601, "dur_ms": 135, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 1736, "dur_ms": 206, "phase_lr_deg": 0.0},
{"word": "구조에", "start_ms": 1942, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "익숙해...", "start_ms": 2202, "dur_ms": 135, "phase_lr_deg": 0.0}
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
"transcript": "1946년 국제사법재판소의 첫 판례 이후에 일관되게 유지해 왔습니다.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 130,
"audio_type": "speech",
"words": [
{"word": "1946년", "start_ms": 130, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "국제사법재판소의", "start_ms": 442, "dur_ms": 237, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 679, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "판례", "start_ms": 811, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "이후에", "start_ms": 934, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "일관되게", "start_ms": 1144, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "유지해", "start_ms": 1404, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "왔습니다.", "start_ms": 1620, "dur_ms": 261, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "이란이 다른 나라 선박의 호르무즈 해협 통과를 방해하면...",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 123,
"audio_type": "speech",
"words": [
{"word": "이란이", "start_ms": 123, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 444, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "나라", "start_ms": 674, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "선박의", "start_ms": 887, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 1013, "dur_ms": 162, "phase_lr_deg": 0.0},
{"word": "해협", "start_ms": 1175, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "통과를", "start_ms": 1278, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "방해하면...", "start_ms": 1380, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "척박한 땅 위에 누군가 무심코 버리고 간 갈색 맥주병들 주변으로...",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "척박한", "start_ms": 231, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "땅", "start_ms": 444, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "위에", "start_ms": 567, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "누군가", "start_ms": 780, "dur_ms": 235, "phase_lr_deg": 0.0},
{"word": "무심코", "start_ms": 1015, "dur_ms": 265, "phase_lr_deg": 0.0},
{"word": "버리고", "start_ms": 1280, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "간", "start_ms": 1412, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "갈색", "start_ms": 1535, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "맥주병들", "start_ms": 1856, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "주변으로...", "start_ms": 2087, "dur_ms": 312, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "호주 비단벌레 수컷들이 새카맣게 몰려들고 있었던 겁니다.",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 760,
"audio_type": "speech",
"words": [
{"word": "호주", "start_ms": 760, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "비단벌레", "start_ms": 1036, "dur_ms": 726, "phase_lr_deg": 0.0},
{"word": "수컷들이", "start_ms": 1762, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "새카맣게", "start_ms": 1998, "dur_ms": 235, "phase_lr_deg": 0.0},
{"word": "몰려들고", "start_ms": 2233, "dur_ms": 726, "phase_lr_deg": 0.0},
{"word": "있었던", "start_ms": 2959, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "겁니다.", "start_ms": 3586, "dur_ms": 217, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "수컷 벌레들은 뜨겁게 달궈진 맥주병이 암컷인 줄 알고...",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 310,
"audio_type": "speech",
"words": [
{"word": "수컷", "start_ms": 310, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "벌레들은", "start_ms": 630, "dur_ms": 753, "phase_lr_deg": 0.0},
{"word": "뜨겁게", "start_ms": 1383, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "달궈진", "start_ms": 1490, "dur_ms": 173, "phase_lr_deg": 0.0},
{"word": "맥주병이", "start_ms": 1663, "dur_ms": 327, "phase_lr_deg": 0.0},
{"word": "암컷인", "start_ms": 1990, "dur_ms": 237, "phase_lr_deg": 0.0},
{"word": "줄", "start_ms": 2227, "dur_ms": 715, "phase_lr_deg": 0.0},
{"word": "알고...", "start_ms": 2942, "dur_ms": 760, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "으악! 이 맛이에요!",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 761,
"audio_type": "speech",
"words": [
{"word": "으악!", "start_ms": 761, "dur_ms": 763, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1524, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "맛이에요!", "start_ms": 1727, "dur_ms": 527, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "거기서도 자세히 설명했는데 참고해 주시고요. 좀 있다 또 설명드릴 거예요.",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 320,
"audio_type": "speech",
"words": [
{"word": "거기서도", "start_ms": 320, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "자세히", "start_ms": 641, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "설명했는데", "start_ms": 764, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "참고해", "start_ms": 1040, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "주시고요.", "start_ms": 1172, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1274, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 1406, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2033, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "설명드릴", "start_ms": 2300, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 2610, "dur_ms": 627, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "근데 우리가 이게 그냥 미사일 공격을 직접 목격하기도 했는데...",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 261,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 261, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 577, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 793, "dur_ms": 715, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1508, "dur_ms": 702, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 2210, "dur_ms": 362, "phase_lr_deg": 0.0},
{"word": "공격을", "start_ms": 2572, "dur_ms": 541, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 3113, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "목격하기도", "start_ms": 3243, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "했는데...", "start_ms": 3510, "dur_ms": 326, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "얼마나 위험한 상황이었나요?",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 237,
"audio_type": "speech",
"words": [
{"word": "얼마나", "start_ms": 237, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "위험한", "start_ms": 497, "dur_ms": 607, "phase_lr_deg": 0.0},
{"word": "상황이었나요?", "start_ms": 1104, "dur_ms": 103, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "학창 시절에 함께 추억했던 밤새워가며...",
"speaker": "speaker_8",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 276,
"audio_type": "mixed",
"words": [
{"word": "학창", "start_ms": 276, "dur_ms": 201, "phase_lr_deg": 0.0},
{"word": "시절에", "start_ms": 477, "dur_ms": 542, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 1019, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "추억했던", "start_ms": 1320, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "밤새워가며...", "start_ms": 1536, "dur_ms": 176, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "37달러, 우리 돈 약 6만 원을 더 내야 채울 수 있습니다.",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 231,
"audio_type": "speech",
"words": [
{"word": "37달러,", "start_ms": 231, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 363, "dur_ms": 362, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 725, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "약", "start_ms": 988, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "6만", "start_ms": 1309, "dur_ms": 572, "phase_lr_deg": 0.0},
{"word": "원을", "start_ms": 1881, "dur_ms": 567, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2448, "dur_ms": 563, "phase_lr_deg": 0.0},
{"word": "내야", "start_ms": 3011, "dur_ms": 316, "phase_lr_deg": 0.0}
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
"transcript": "이런 상황에서도 백악관은 트럼프 대통령이 관세 폭탄을 발표한 자칭 해방의 날 일주년을 자축했습니다.",
"speaker": "speaker_9",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 132,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 132, "dur_ms": 726, "phase_lr_deg": 0.0},
{"word": "상황에서도", "start_ms": 858, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "백악관은", "start_ms": 1170, "dur_ms": 317, "phase_lr_deg": 0.0},
{"word": "트럼프", "start_ms": 1487, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "대통령이", "start_ms": 2252, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "관세", "start_ms": 2879, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "폭탄을", "start_ms": 3506, "dur_ms": 602, "phase_lr_deg": 0.0},
{"word": "발표한", "start_ms": 4108, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "자칭", "start_ms": 4409, "dur_ms": 172, "phase_lr_deg": 0.0},
{"word": "해방의", "start_ms": 4581, "dur_ms": 362, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 4943, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "일주년을", "start_ms": 5269, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "자축했습니다.", "start_ms": 5890, "dur_ms": 263, "phase_lr_deg": 0.0}
]
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
"transcript": "국제 시장에 존재하는 거대한 공백 지대를 정확히 붙잡았기 때문입니다.",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 267,
"audio_type": "speech",
"words": [
{"word": "국제", "start_ms": 267, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "시장에", "start_ms": 503, "dur_ms": 752, "phase_lr_deg": 0.0},
{"word": "존재하는", "start_ms": 1255, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "거대한", "start_ms": 1522, "dur_ms": 762, "phase_lr_deg": 0.0},
{"word": "공백", "start_ms": 2284, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "지대를", "start_ms": 3049, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "정확히", "start_ms": 3309, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "붙잡았기", "start_ms": 3539, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "때문입니다.", "start_ms": 3775, "dur_ms": 126, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "많은 나라들은 5세대 전투기를 살 돈이 없거나 혹은 다른 나라들이 아예 팔지 않기 때문에 구할 수 없습니다. 하지만 이들 국가는 낡은...",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "많은", "start_ms": 263, "dur_ms": 632, "phase_lr_deg": 0.0},
{"word": "나라들은", "start_ms": 895, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "5세대", "start_ms": 1516, "dur_ms": 617, "phase_lr_deg": 0.0},
{"word": "전투기를", "start_ms": 2133, "dur_ms": 712, "phase_lr_deg": 0.0},
{"word": "살", "start_ms": 2845, "dur_ms": 573, "phase_lr_deg": 0.0},
{"word": "돈이", "start_ms": 3418, "dur_ms": 753, "phase_lr_deg": 0.0},
{"word": "없거나", "start_ms": 4171, "dur_ms": 576, "phase_lr_deg": 0.0},
{"word": "혹은", "start_ms": 4747, "dur_ms": 753, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 5500, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "나라들이", "start_ms": 5626, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아예", "start_ms": 5746, "dur_ms": 265, "phase_lr_deg": 0.0},
{"word": "팔지", "start_ms": 6011, "dur_ms": 527, "phase_lr_deg": 0.0},
{"word": "않기", "start_ms": 6538, "dur_ms": 572, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 7110, "dur_ms": 572, "phase_lr_deg": 0.0},
{"word": "구할", "start_ms": 7682, "dur_ms": 507, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 8189, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "없습니다.", "start_ms": 8321, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 8631, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "이들", "start_ms": 8861, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "국가는", "start_ms": 9097, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "낡은...", "start_ms": 9327, "dur_ms": 263, "phase_lr_deg": 0.0}
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
"transcript": "만든 적이 있었고요. 자 그런데 더 무서운 건 AI를 가장 잘 알고 있는 세계 최고의 AI 전문가들조차 이런 경고를 쏟아내고 있다는 거예요.",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 163,
"audio_type": "speech",
"words": [
{"word": "만든", "start_ms": 163, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "적이", "start_ms": 299, "dur_ms": 165, "phase_lr_deg": 0.0},
{"word": "있었고요.", "start_ms": 464, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 627, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 763, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1134, "dur_ms": 275, "phase_lr_deg": 0.0},
{"word": "무서운", "start_ms": 1409, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 2165, "dur_ms": 576, "phase_lr_deg": 0.0},
{"word": "AI를", "start_ms": 2741, "dur_ms": 265, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 3006, "dur_ms": 567, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3573, "dur_ms": 657, "phase_lr_deg": 0.0},
{"word": "알고", "start_ms": 4230, "dur_ms": 567, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4797, "dur_ms": 675, "phase_lr_deg": 0.0},
{"word": "세계", "start_ms": 5472, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "최고의", "start_ms": 5798, "dur_ms": 307, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 6105, "dur_ms": 652, "phase_lr_deg": 0.0},
{"word": "전문가들조차", "start_ms": 6757, "dur_ms": 653, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 7410, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "경고를", "start_ms": 7570, "dur_ms": 675, "phase_lr_deg": 0.0},
{"word": "쏟아내고", "start_ms": 8245, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 8481, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 8807, "dur_ms": 675, "phase_lr_deg": 0.0}
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
"transcript": "클로드를 만든 앤트로픽의 CEO 다리오 아모데이는 이렇게 말했습니다. AI는 앞으로 5년 안에...",
"speaker": "speaker_11",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 571,
"audio_type": "speech",
"words": [
{"word": "클로드를", "start_ms": 571, "dur_ms": 571, "phase_lr_deg": 0.0},
{"word": "만든", "start_ms": 1142, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "앤트로픽의", "start_ms": 1907, "dur_ms": 153, "phase_lr_deg": 0.0},
{"word": "CEO", "start_ms": 2060, "dur_ms": 153, "phase_lr_deg": 0.0},
{"word": "다리오", "start_ms": 2213, "dur_ms": 175, "phase_lr_deg": 0.0},
{"word": "아모데이는", "start_ms": 2388, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 2538, "dur_ms": 175, "phase_lr_deg": 0.0},
{"word": "말했습니다.", "start_ms": 2713, "dur_ms": 175, "phase_lr_deg": 0.0},
{"word": "AI는", "start_ms": 2888, "dur_ms": 175, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 3063, "dur_ms": 157, "phase_lr_deg": 0.0},
{"word": "5년", "start_ms": 3220, "dur_ms": 157, "phase_lr_deg": 0.0},
{"word": "안에...", "start_ms": 3377, "dur_ms": 105, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "왜냐하면 미국이 이란에 대한 총 공세를 예고를 했습니다. 그래서 이번 주 주말 또는 다음 주에 예고가 되어 있고요. 기존에 예고했던 데드라인 있죠. 데드라인 연기를 했죠. 그게 4월 6일이에요. 월요일입니다. 자 그렇기 때문에 그때까지...",
"speaker": "speaker_12",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "왜냐하면", "start_ms": 210, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "미국이", "start_ms": 342, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "이란에", "start_ms": 474, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 597, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "총", "start_ms": 909, "dur_ms": 235, "phase_lr_deg": 0.0},
{"word": "공세를", "start_ms": 1144, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "예고를", "start_ms": 1380, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "했습니다.", "start_ms": 1616, "dur_ms": 206, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 1822, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 1958, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "주", "start_ms": 2081, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 2213, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "또는", "start_ms": 2444, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 2654, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "주에", "start_ms": 2975, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "예고가", "start_ms": 3211, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "되어", "start_ms": 3343, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "있고요.", "start_ms": 3655, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "기존에", "start_ms": 3965, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "예고했던", "start_ms": 4097, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "데드라인", "start_ms": 4310, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "있죠.", "start_ms": 4433, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "데드라인", "start_ms": 4563, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "연기를", "start_ms": 4875, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "했죠.", "start_ms": 5105, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 5336, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "4월", "start_ms": 5456, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "6일이에요.", "start_ms": 5576, "dur_ms": 127, "phase_lr_deg": 0.0},
{"word": "월요일입니다.", "start_ms": 5703, "dur_ms": 213, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 5916, "dur_ms": 127, "phase_lr_deg": 0.0},
{"word": "그렇기", "start_ms": 6043, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 6166, "dur_ms": 213, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "나라 급이 달라. 이 소감 댓글 보다가는 맨투맨 맵십에도 좋다고 하는 사람이 있어서 바로 타.",
"speaker": "speaker_13",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 312,
"audio_type": "speech",
"words": [
{"word": "나라", "start_ms": 312, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "급이", "start_ms": 543, "dur_ms": 135, "phase_lr_deg": 0.0},
{"word": "달라.", "start_ms": 678, "dur_ms": 106, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 784, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "소감", "start_ms": 921, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "댓글", "start_ms": 1024, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "보다가는", "start_ms": 1131, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "맨투맨", "start_ms": 1234, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "맵십에도", "start_ms": 1364, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "좋다고", "start_ms": 1500, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1607, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 1710, "dur_ms": 271, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 1981, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 2217, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "타.", "start_ms": 2343, "dur_ms": 762, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "하이, 고레가 니혼진카라 미따 이태원 쿠라스타 토 롯폰기 쿠라스타.",
"speaker": "speaker_14",
"confidence": 0.95,
"language": "ja",
"silence_before_ms": 573,
"audio_type": "speech",
"words": [
{"word": "하이,", "start_ms": 573, "dur_ms": 751, "phase_lr_deg": 0.0},
{"word": "고레가", "start_ms": 1324, "dur_ms": 751, "phase_lr_deg": 0.0},
{"word": "니혼진카라", "start_ms": 2075, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "미따", "start_ms": 2446, "dur_ms": 713, "phase_lr_deg": 0.0},
{"word": "이태원", "start_ms": 3159, "dur_ms": 753, "phase_lr_deg": 0.0},
{"word": "쿠라스타", "start_ms": 3912, "dur_ms": 753, "phase_lr_deg": 0.0},
{"word": "토", "start_ms": 4665, "dur_ms": 751, "phase_lr_deg": 0.0},
{"word": "롯폰기", "start_ms": 5416, "dur_ms": 571, "phase_lr_deg": 0.0},
{"word": "쿠라스타.", "start_ms": 5987, "dur_ms": 137, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "뿐이라고는 단정하실 수는 없습니다. 시대가 발전하면서 찾아온 생활 패턴의 변화, 맞벌이 가정의 증가, 가사 분담과 관련된 인식 변화 등 여러 요인이 함께 적용되었을 것입니다.",
"speaker": "speaker_15",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 613,
"audio_type": "speech",
"words": [
{"word": "뿐이라고는", "start_ms": 613, "dur_ms": 613, "phase_lr_deg": 0.0},
{"word": "단정하실", "start_ms": 1226, "dur_ms": 612, "phase_lr_deg": 0.0},
{"word": "수는", "start_ms": 1838, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "없습니다.", "start_ms": 2459, "dur_ms": 632, "phase_lr_deg": 0.0},
{"word": "시대가", "start_ms": 3091, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "발전하면서", "start_ms": 3227, "dur_ms": 261, "phase_lr_deg": 0.0},
{"word": "찾아온", "start_ms": 3488, "dur_ms": 361, "phase_lr_deg": 0.0},
{"word": "생활", "start_ms": 3849, "dur_ms": 631, "phase_lr_deg": 0.0},
{"word": "패턴의", "start_ms": 4480, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "변화,", "start_ms": 4792, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "맞벌이", "start_ms": 5113, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "가정의", "start_ms": 5425, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "증가,", "start_ms": 5737, "dur_ms": 317, "phase_lr_deg": 0.0},
{"word": "가사", "start_ms": 6054, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "분담과", "start_ms": 6370, "dur_ms": 152, "phase_lr_deg": 0.0},
{"word": "관련된", "start_ms": 6522, "dur_ms": 671, "phase_lr_deg": 0.0},
{"word": "인식", "start_ms": 7193, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "변화", "start_ms": 7820, "dur_ms": 571, "phase_lr_deg": 0.0},
{"word": "등", "start_ms": 8391, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 8607, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "요인이", "start_ms": 8709, "dur_ms": 201, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 8910, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "적용되었을", "start_ms": 9030, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "것입니다.", "start_ms": 9156, "dur_ms": 136, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "그럼에도 불구하고...",
"speaker": "speaker_15",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 675,
"audio_type": "speech",
"words": [
{"word": "그럼에도", "start_ms": 675, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "불구하고...", "start_ms": 1440, "dur_ms": 637, "phase_lr_deg": 0.0}
]
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
"transcript": "이제 분할 매수를 철저하게 비중 조절을 해가지고 어제 손절이 안 나가셨잖아요.",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 103,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 103, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "분할", "start_ms": 415, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "매수를", "start_ms": 545, "dur_ms": 763, "phase_lr_deg": 0.0},
{"word": "철저하게", "start_ms": 1308, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "비중", "start_ms": 2058, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "조절을", "start_ms": 2165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "해가지고", "start_ms": 2295, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "어제", "start_ms": 2427, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "손절이", "start_ms": 2529, "dur_ms": 163, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 2692, "dur_ms": 176, "phase_lr_deg": 0.0},
{"word": "나가셨잖아요.", "start_ms": 2868, "dur_ms": 167, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "야 나도 물렸어. 월요일 날 종베했어.",
"speaker": "speaker_17",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 237,
"audio_type": "speech",
"words": [
{"word": "야", "start_ms": 237, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "나도", "start_ms": 563, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "물렸어.", "start_ms": 693, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "월요일", "start_ms": 960, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 1587, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "종베했어.", "start_ms": 1854, "dur_ms": 132, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "네, 지금보다 훨씬 정밀하게 비교할 수 있습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 136,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 136, "dur_ms": 175, "phase_lr_deg": 0.0},
{"word": "지금보다", "start_ms": 311, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "훨씬", "start_ms": 627, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "정밀하게", "start_ms": 759, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "비교할", "start_ms": 896, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1032, "dur_ms": 156, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 1188, "dur_ms": 617, "phase_lr_deg": 0.0}
]
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
"transcript": "만약 이 두 입자 사이에서 아주 미세한 차이라도 발견...",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "만약", "start_ms": 250, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 352, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 482, "dur_ms": 206, "phase_lr_deg": 0.0},
{"word": "입자", "start_ms": 688, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "사이에서", "start_ms": 1000, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1130, "dur_ms": 526, "phase_lr_deg": 0.0},
{"word": "미세한", "start_ms": 1656, "dur_ms": 265, "phase_lr_deg": 0.0},
{"word": "차이라도", "start_ms": 1921, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "발견...", "start_ms": 2677, "dur_ms": 675, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 11:00:07*
