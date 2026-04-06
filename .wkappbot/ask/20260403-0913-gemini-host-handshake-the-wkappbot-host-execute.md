# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:12:30  uptime=28395s  mem=1806MB  system_processes=449"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_091053.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 657-576-652 120-103-701-176-136-263-317-312-132-136-132-213-132-126-103-761-123-106-103-163-136-102-612-103-213-136-314-103-761-152-165-617-615-103 152-136-627-612-316-160-317-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 162-103-312-570-751-157-320-302-716-136-106-621-701-326-762-765-213-612-561-206-657-236-267-167-765-610-601-160-610-310-321-201-216-206-206-260-260-620-627-672-106-152-315-130-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 631-106-630-167-106-617-621-162-126-306-623-726-620-630-123-310-316-136-617-617-136-175-263-615-137-617-163-103-175-617-162-617-316-136-136-175-167-136-103-136-136-130-761-163-6_V.mp3)
  Segment 4: 4728ms ~ 15348ms (file: 132 507-157-603-103-132-123-562-136-132-175-126-613-137-162-136-123_W.mp3)
  Segment 5: 16348ms ~ 16924ms (file: 751-752-750-705-627-627 173-135-176-571-627-574-574-574-576-576 213-126-321-231-326 327-326-362-136 672_V.mp3)
  Segment 6: 17924ms ~ 18500ms (file: 317-137-130-301-310-130-103-103-132-103_V.mp3)
  Segment 7: 19500ms ~ 20076ms (file: 763-765-675-617-167-613-103-107-105-126-137-132-613-103-160-103-163-651-165-165-136-167-612-162-167-630-167-632-672-132-671-251-764-671-136-136-103-106-372-167-162-106-106-136-1_V.mp3)
  Segment 8: 21076ms ~ 21652ms (file: 570-627-216-263-672-637-361-362-572-312-163-612-236-310-312-231-261-312-103-312 326-362-126 623-236-326-312-326-312-263-231-236-263-632-260-260-620-263-362-102-231-630-132-362-1_V.mp3)
  Segment 9: 22652ms ~ 23228ms (file: 316-316-267-527-126-162-213-216-213-216-267-627-627-136-137-637-701-762-607-360-617-136-570-765-670-176-163-260-601-610-617-670-670-672-675-672-673-671-613-675-567-675-675-576-7_V.mp3)
  Segment 10: 24228ms ~ 24804ms (file: 162-175-103-176-176-735-176-103-163-751-617-103-617-172-376-172-756-167-103-157-103-126-162-516-601-136-163-160-154-514-165-130-631-210-135-130-136-156-672-562-627-617 103-136-1_V.mp3)
  Segment 11: 25804ms ~ 26380ms (file: 132-672-627-260-627-602-310-132-103-310-103-310-301-310-130-301-130-261-260-130-103-167-106-206-621-260-201-310-312-321-301-750-305-103-302-150-573-574-756-175-213-320-130-751-7_V.mp3)
  Segment 12: 27380ms ~ 27956ms (file: 617-206-261-162-620-206-326-620-102-760-765-670-761-175-126-612-602-216-620-621-163-321-361-675-670-620-625-652-527-106-106-610-671-167-167-126-126-136-627-627-623-261-162-237-5_W.mp3)
  Segment 13: 28956ms ~ 29532ms (file: 267-620-620-260-263-260-601-631-306-327-360-306-362-762-173-123-106-107-163-132-632-673-602-316-672-237-231-712-302-302-310-327-320-327-321-163-312-123-567-567-621-602-620-607-6_W.mp3)
  Segment 14: 30532ms ~ 31108ms (file: 316-632-627-162-162-612-507-507-231-652-576-675-675-630-623-302-576-673-763-671-627-301-106-301-130-310-132-103-136-713-164-627-627 132-103-614-136-136 376-713-316-137-163-613-1_V.mp3)
  Segment 15: 32108ms ~ 32684ms (file: 213-325-756-524-627 162-213-103-301-316-321-276-762-672-627-267-762_W.mp3)
  Segment 16: 33684ms ~ 34260ms (file: 107-162-132-106-130-132-126-103-163-130-126-167-163-103-136-103-137-310-217-130-132-216-163-162-103-162-103-612-130-123-675-136-103-670-157-163-103-106-602-623-602-160-103-613-6_V.mp3)
  Segment 17: 35260ms ~ 35836ms (file: 132-103-572-270-276-261-126-312-216-263-302-705-701-306-307-573-213-203-231-267-620-260-206-260-321-627-267-251-157-213-570-327-302-237-201-237-275-270-275-572-132-134-123-213-1_V.mp3)
  Segment 18: 36836ms ~ 37412ms (file: 312-312-623-326-312-132-310-310-136-103-317-317-132-312-132-123-102-130-612-362-316-276-162-123-102-136-312-362-132-120-615-613-130-367-263-123-132-136-601-213 312-310-671-172-1_V.mp3)
  Segment 19: 38412ms ~ 38988ms (file: 310-360-263-610-672-760-607-675-675-621-163-261-216-261-231-612-132-312-123-132-301-306-213-571-576-657-567-567-576-756-261-613 327-127 756-761-705-567-567-670-607-176-675-670-7_W.mp3)
  Segment 20: 39988ms ~ 49456ms (file: 675-167-106-106-103-160-163-761-160-657-670-715-163-103-163-103-135-610-602-651-136-607-612-160-623-316-632-103-167-163-106-132-103-136-162-127-123-612-160-261-216-173-103-123-3_V.mp3)
  Segment 21: 50456ms ~ 51032ms (file: 103-103-215-671-623-153-367-651-315-103-106-130-156-126-215-106-162-301-103-103-136-106-130-127-136-103-103-751-106-637-521-126-136-103-163-163-130-103-103-126-176-137-516-562-1_V.mp3)
  Segment 22: 52032ms ~ 52608ms (file: 623-615-130-317-752-327-617-135-132-613-103-567-517-576-571-756-615-176-130-107-167-514-541-541-541-540-541-541-514-576-163-763-162 134-106-167-163-617-706-705-760-756-615-637-6_W.mp3)
  Segment 23: 53608ms ~ 54184ms (file: 236-206-203-203-203-236-320-236-326-263-203-352-320-320-132-130-306-326-236-263-310-106-612-136-157-504-306 123-206-203-520-125-362-361-103-136-103-132-103-312-316-326-206-263-3_W.mp3)
  Segment 24: 55184ms ~ 55760ms (file: 762-672-673-736-716-627-623-126-623-162-621-132-132-716-671-761 671-761_W.mp3)
  Segment 25: 56760ms ~ 57336ms (file: 627-267-263-267-705-750-103-130-756-571-136-312-326-162-136-301-762-316-132-761-106-670-675-671-167-167-162-126-236-326-326-320-673-236-310-203-176-623-627-367-163-316-362-736-7_W.mp3)
  Segment 26: 58336ms ~ 58912ms (file: 176-607-136-561-312-173-162-106-765-261-176-126-127-132-103-167-103-173-137-143-316-523-176-160-162-675-610-617-130-617-167-103-103-167-607-167-165-632-613-106-143-165-130-153-7_V.mp3)
  Segment 27: 59912ms ~ 60488ms (file: 617-160-103-165-761-163-176-163-167-514-167-671-137-160-671-637-163-316-162-103-103-576-576-576-576-576-765-576-657-576-576-576-576-576-576-576-567-157-756-567-576-157-156_W.mp3)
  Segment 28: 61488ms ~ 62064ms (file: 216-123-637-162-167-765-160-267-236-326-230-612-613-162-103-103-165-715-617-160-163-615-136-167-163-106-613-351-312-103-103-132-156-152-103-136-152-103-126-167-176-126 613 761-1_V.mp3)
  Segment 29: 63064ms ~ 63640ms (file: 756-657-506-567-756-657-650-567-756-567-537-567-175-160-132-312-102-102-132-123-162-102-126-163-617-167-162-136-162-136-362-126-213-216-236-216-237-260-321-263-130-136-120-321-3_W.mp3)
  Segment 30: 64640ms ~ 65216ms (file: 156-165-156-176-156-126-156-615-165-165-615-215-135-152-615-152-157-153-512-157-152-157-157-172-152-156-512-513-521-157-156-125-153-152-152-156-156-157-136-517-152-651-651-526-5_V.mp3)
  Segment 31: 66216ms ~ 66792ms (file: 162-631-613-612-162-106-317-163-162-216-362-306-756-236-372-570-753-306-632-362-302-357-613-613-615-567-567-507-576-576-574-576-576-576-507-576-570-750-576-570-105-135-103-501-5_W.mp3)
  Segment 32: 67792ms ~ 68368ms (file: 130-106-167-136-617-160-162-103-136-617-163-671-261-126-136-106-671-167-316-156-106-321-123_V.mp3)
  Segment 33: 69368ms ~ 69944ms (file: 136-127-163-613-612-132-125-132-236-103-132-765-615-176-316-106-136-103-103-130-132-136-526-527-657-162-105-756-136-162-723-601-623-601-176-623 103-132-163-135-102-163-236-631-1_V.mp3)
  Segment 34: 70944ms ~ 71520ms (file: 260-623-632-632-163-201-756-672-620-620-162-160-102-126-102-263-203-253-106-615-612-106-601-613-760-376-136-103-132-175-136-162-602-602-620-602-102-163-612-756-726-321-102-103-1_V.mp3)
  Segment 35: 72520ms ~ 73096ms (file: 130-130-103-136-163-106-106-167-132_V.mp3)
  Segment 36: 74096ms ~ 74672ms (file: 260-260-672-627-627-602-620-627-167-167-130-207-310-321-301-136-570-712-621-167-107-103-671-302-326-216-670-162-103-136-120-120-213-207-602-267-260-627-176-127-301-137-607-607-6_V.mp3)
  Segment 37: 75672ms ~ 76248ms (file: 651-576-576-574-576-576-103-103-627_W.mp3)
  Segment 38: 77248ms ~ 77824ms (file: 125-251-256-261-263-261-521-125-123-132-567-235-215-251-152-152-516-517-276-627-276-267-231-132-321-312-163-136-136-132-312-571-135-126-216-326-216-213-153-123-621-126-165-312-3_W.mp3)
  Segment 39: 78824ms ~ 79400ms (file: 270-501-165-165-615-506-730-675-621-206-136-103-162-132-136-321-213-216-320-312-301-753-765-602-236-102-136-231-710-706-507-572-703-756-762-750-756-750-375-132-136-302-302-136-1_V.mp3)
  Segment 40: 80400ms ~ 80976ms (file: 627-621-162-103-103-315-123-136-132-167-631-125 312-136-106-654-652-617-160-670-103-106-160-107-167-617-165-136-167-106-163-106-173-103-126-132-167-670-176-102-602-103-160-126-1_V.mp3)
  Segment 41: 81976ms ~ 82552ms (file: 574-627-627-132-163-103-120-312-201-657-627-162-326-162-621-632-607-627-627-172-621-367-362-263-673-623-672-617-672-627-627-672-617-132-126-617-627-216-123-673-672-617-612-672-6_W.mp3)
  Segment 42: 83552ms ~ 84128ms (file: 765-754-756-760-317-317-307-301-326-321-317-163-103-132-312-103-132-136-132-103-103-103-132-123-132-701-130-316-137-132-102-132-132 540-547-267-623-125-127-750-574-135-163-316-2_V.mp3)
  Segment 43: 85128ms ~ 85704ms (file: 260-126-132-201-123 312 627-621-621 310-130-103-132-120-201-201-206-206-267-260 130-102-621-260-261-261 627-620-206_V.mp3)
  Segment 44: 86704ms ~ 87280ms (file: 362-320-670-623-273-136-236-326-763 326-216-213-326-231-326-725-257-316 321-230-261-236-263-625_W.mp3)
  Segment 45: 88280ms ~ 88856ms (file: 673-621-362-316-167-126-132-132-167-613-167-613-671-621-672-612-613-623-213-316-261-263-312-321-675-631-762-671-361-627-672-351-716-137-103-130-137-706-276-716-763-763-675-765-6_W.mp3)
  Segment 46: 89856ms ~ 90432ms (file: 576-576-576-567-263-263-261-260-657-671-276-267-571-576-576-276-263-267-267-260-265-263-267-657-567-657-236-620-263-605-657-627-602-567-637-576-576-657-237-672-321-132-576-576-5_W.mp3)
  Segment 47: 91432ms ~ 92008ms (file: 275-237-715-751-761-574-754-130-263-217-712-517-206-321-271-237-630-621-172-173-132-206-162-326-172-327-206-321-327-762-207-217-237-236-762-672-276-267-263-723-754-237-123-203-2_W.mp3)
  Segment 48: 93008ms ~ 93584ms (file: 507-576-547-571-513-542-276-326-261-236-231-312-321-231-132-312-756-716-763-524-235-263-621-375-172-372-317-317-326-362-326-327-327-731-372-326-316-635-613-361-163-321-231-532-3_W.mp3)
  Segment 49: 94584ms ~ 95160ms (file: 675-167-317-103-361-756-672-620-621-602-627-675-316-571-257-267-612-236-362-632-627-206-620-620-627-632-612-163-570-576-567-320-623-265-207-765-765-576-517-652-607-675-675-672-6_W.mp3)
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
> **Time**: 2026-04-03 09:13:23
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "양보가 필요합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "양보가", "start_ms": 40, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 330, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "어디서 내려요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어디서", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내려요", "start_ms": 310, "dur_ms": 260, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "이거 얼마예요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "이거", "start_ms": 30, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "얼마예요", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "영수증 주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "영수증", "start_ms": 60, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 410, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "화장실 어디예요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "화장실", "start_ms": 45, "dur_ms": 290, "phase_lr_deg": 0.0},
{"word": "어디예요", "start_ms": 345, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "도와주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "도와주세요", "start_ms": 80, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "조심하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "조심하세요", "start_ms": 35, "dur_ms": 540, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "길을 잃었어요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "길을", "start_ms": 70, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잃었어요", "start_ms": 260, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "배고파요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "배고파요", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "목말라요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "목말라요", "start_ms": 40, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "졸려요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "졸려요", "start_ms": 90, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "피곤해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "피곤해요", "start_ms": 65, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "아파요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "아파요", "start_ms": 100, "dur_ms": 470, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "괜찮아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "괜찮아요", "start_ms": 45, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "알겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "알겠어요", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "모르겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "모르겠어요", "start_ms": 50, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "맞아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "맞아요", "start_ms": 80, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "아니요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "아니요", "start_ms": 75, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "좋아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 65, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "싫어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "싫어요", "start_ms": 40, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "기뻐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "기뻐요", "start_ms": 110, "dur_ms": 460, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "슬퍼요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "슬퍼요", "start_ms": 50, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "화나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "화나요", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "심심해요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "심심해요", "start_ms": 70, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "재밌어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "재밌어요", "start_ms": 55, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "맛있어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "맛있어요", "start_ms": 45, "dur_ms": 525, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "맛없어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "맛없어요", "start_ms": 90, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "비싸요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "비싸요", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "싸요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 85,
"audio_type": "speech",
"words": [
{"word": "싸요", "start_ms": 85, "dur_ms": 485, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "커요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "커요", "start_ms": 100, "dur_ms": 470, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "작아요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "작아요", "start_ms": 40, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "무거워요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "무거워요", "start_ms": 75, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "가벼워요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "가벼워요", "start_ms": 55, "dur_ms": 515, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "뜨거워요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "뜨거워요", "start_ms": 65, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "차거워요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "차거워요", "start_ms": 45, "dur_ms": 525, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "빠르다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "빠르다", "start_ms": 110, "dur_ms": 460, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "느리다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "느리다", "start_ms": 80, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "멀다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "멀다", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "가깝다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 95,
"audio_type": "speech",
"words": [
{"word": "가깝다", "start_ms": 95, "dur_ms": 475, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "쉽다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "쉽다", "start_ms": 70, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "어렵다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어렵다", "start_ms": 50, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "깨끗하다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "깨끗하다", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "더럽다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "더럽다", "start_ms": 65, "dur_ms": 505, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "조용하다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "조용하다", "start_ms": 80, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "시끄럽다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "시끄럽다", "start_ms": 45, "dur_ms": 525, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "밝다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 115,
"audio_type": "speech",
"words": [
{"word": "밝다", "start_ms": 115, "dur_ms": 455, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "어둡다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 75,
"audio_type": "speech",
"words": [
{"word": "어둡다", "start_ms": 75, "dur_ms": 495, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "강하다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "강하다", "start_ms": 60, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "약하다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "약하다", "start_ms": 90, "dur_ms": 480, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:13:23*
