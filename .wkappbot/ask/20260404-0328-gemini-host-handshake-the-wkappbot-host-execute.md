# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17688  time=03:27:38  uptime=7741s  mem=170MB  system_processes=364"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_032647.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 617-162-206-160-130-675-613-632-632 326-631-316-361-571-136-136-321-216-320-236-362-103-136-153-537-213-631-103-236-126-230-213-321-310-103-106-120-230-316-216-163-105-230-217-7_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 627-617-163-163-145-132-276-267-312-314-126-261-326-123-132-132-135-310-132-132-312-132-312-123-136-312-136-316-103-632-672-623-632-267-632-263-623-627-612-603-103-126-613-103-2_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 652-106-106-526-536-265-621-263-213-136-216-765-670-162-163-126-621-612-362-601-301-312-672-672-627-206-213-123-126-216-123-162-612-126-163-302-302-213-106-136-163-102-321-236-3_V.mp3)
  Segment 4: 4728ms ~ 15384ms (file: 574-735 326-756-756-615-753-267-237-317-310-713-137-157-751-751-210-267-276-231-132-271-123-216-753-273-210-260-261-260-230-267-261-512-301-261-127-206-216-271-102-573-276-276-2_W.mp3)
  Segment 5: 16384ms ~ 16960ms (file: 316-316-106-610-160-160-167-310-312-316-671-615-567-576-567-361-672-312-361-167-631-713-137-163-236-637-362-163-236-623-367-653-567-567-761-675-657-623-632-625-632-362-316-362 1_W.mp3)
  Segment 6: 17960ms ~ 18536ms (file: 371-172-324-365-361-136-316-136-317-651-274-674-623-620-612-163_V.mp3)
  Segment 7: 19536ms ~ 20112ms (file: 124-102-103-103-102-123-132-627-261-126-136-103-132-123-126-215-126-132-123-216-136-301-132-123 216-167-617 236-231-657-651-726-267-765-672-567-675-627-621-601-670-756-672-163-1_V.mp3)
  Segment 8: 21112ms ~ 21688ms (file: 173 103-136-132-165-532-136-103-132-351-136-102-312-102-123-106-103-136-136-136-163-103-132-136-317-657-610-561-316-106-132-106-321-263-102-126-103-163-615-263-261-162-132-153-7_V.mp3)
  Segment 9: 22688ms ~ 23264ms (file: 612-136-136-103-610-136-126-160-130-136-130-106-103-160-521-163-136-610-103-126-236-621-103-136-326-237-263-267-376-327-726-762-672-627-327-635-367-275-267-732-672-267-625-263-2_W.mp3)
  Segment 10: 24264ms ~ 24840ms (file: 167-163-136-103-326-106-103-136-265 162-130-103-103-136-135-135-103-167-136-102-156-365-361-163-103-136-135-176-106-136-561-530-176-103-136-103-137-103-653-103-163-103-103-103-6_V.mp3)
  Segment 11: 25840ms ~ 26416ms (file: 216-321-573-573-576-576-573-573-574-571-312-132-571-612-576-316-613-103-132-125-173-371-753-207-270-271-207-123-120-130-123-312-132-701-132-137-237-607-132-705-312-317-310-231-3_V.mp3)
  Segment 12: 27416ms ~ 27992ms (file: 206-725-207-260-102-216-612-263-316-326-316-167-136-736-602-621-602-326-263-261-206-201-231-321-761-706-675-627-261-206-260-601-620-260-620-261-627-765-765-627-206-201-230-206-2_V.mp3)
  Segment 13: 28992ms ~ 29568ms (file: 625-627-627-132 362-153-130-130-163-576-163-231-136-301-132-316-103-316-103-751-167-106-231-162-312-671-263-361 613-130-103-103-132-136-103-315-132-135-103-132-560-167-132-106-1_V.mp3)
  Segment 14: 30568ms ~ 31144ms (file: 231-302-230-302-320-326-125-625 132-413-341-173-123-157-103-130-173-137-173-627-632-207-260-263-620-632-230-320-312-130-610-105-527-162-561-326-120-132-102-132-103-132-165-136-1_V.mp3)
  Segment 15: 32144ms ~ 32720ms (file: 167-106-617-167-163-617-617-163-612-132-152-617-617-103-716-623-160-613-657-672-607-103-103-756-106 326-162_V.mp3)
  Segment 16: 33720ms ~ 34296ms (file: 160-162-672-162-106-312-163-130 765-256-263-235-267-756-276-276-625-635-623-263 627-267-216-127-216-261-261-231-231-123-132-130 756-576-576-576-574-574-574 763-672_W.mp3)
  Segment 17: 35296ms ~ 43180ms (file: 365-573-675-230-267-263-765-672-236-362-632-325-256-261-623-326-321-372-307-310-321-236-756-765-267-160-761-716-765-761-176-610-106-765-120-762-765-756-765-752-754-276-312-130-1_W.mp3)
  Segment 18: 44180ms ~ 44756ms (file: 632-326-631-132-103-310-320-312-672-567-671-617-301-130-103-132-326-302-326-162-612-162-321-120-210-160-103-130-123-102-301-102 132-312-123-102-103-130-132-136-316-132-136-107-1_V.mp3)
  Segment 19: 45756ms ~ 46332ms (file: 103-103-163-315-103-176-576-167-103-130-163-316-517-136-126-167-136-136-130-312-103-315-310-103-326-136-137-103-106-137-673-136-103-126-546-612-156-125-617-136-105-561-162-156-6_V.mp3)
  Segment 20: 47332ms ~ 47908ms (file: 137-173-312-103-612-106-102-103-576-657-715-103-103-137-132-672-674-132-302-316-263-105-301-362-106-126-123-302-372-307-371-326-327-756-765-765-765-760-325-325-327-132-312-132-1_V.mp3)
  Segment 21: 48908ms ~ 49484ms (file: 425-372-315-153-103-103-312-261-621 671-612-671_V.mp3)
  Segment 22: 50484ms ~ 51060ms (file: 627-136-163-136-132-132-316-136-132-126-361-132-236-123-136-567-163-136-623-236-136-107-106-130-103-316-103-103-103-136-103-103-160-136-317-103-670-361-130-132-316-106-106-316-6_V.mp3)
  Segment 23: 52060ms ~ 52636ms (file: 206-506-567-206-265-206-650-726 260-620-260-620-672-670-672-672-267-672-602-672 267-627-620-260-620-627-267-260-267-620-627-672-206-267 260-260-620-260-260-260-602-620-260-267-2_W.mp3)
  Segment 24: 53636ms ~ 54212ms (file: 615-360-326-136-106-106-132-103-167-756_V.mp3)
  Segment 25: 55212ms ~ 55788ms (file: 574-574-573-756-574-576-574-571-751-721-176-574-574-573-361-637 657-623-362-176-137_W.mp3)
  Segment 26: 56788ms ~ 57364ms (file: 617-601-160-165-103-651-316-103-106-136-106-157-136-617-365-167-765-360-623-103-103-163-136-132-231-216-613-162-160-106-103-103-163-160-103-126-576-312-362-615-675-361-102-103-3_V.mp3)
  Segment 27: 58364ms ~ 58940ms (file: 130-162-301-126-102-132-103-136-103-103-103-312-263-621-263-261 134-103-137-132-162-165-260-617-561-103-167-136-316-103-136-175-136-657-135-103-312-316-106-103-167-136-132-137-1_V.mp3)
  Segment 28: 59940ms ~ 60516ms (file: 751-765-751-157-271-756-271-657-265-625-725-265-756-512-325-357-357-513-135-526-567-356-275-275-257-576-756-165-576-731-372-165-106-536-357-153-315-351-574-576-567-657-257-267-2_W.mp3)
  Segment 29: 61516ms ~ 67348ms (file: 231-623-261-216-163-631-237-631-517-572-127-261-123-261-316-236-267-125-210-132-210-203-127-123-127-120-213-123-231-236-123-215-320-203-217-210-263-620-627-675-576-627-217-267-6_W_stt_i_think_things.mp3)
  Segment 30: 68348ms ~ 68924ms (file: 172-631-136-163-637-601-613-173-671-623-263-673-123-132-172_V.mp3)
  Segment 31: 69924ms ~ 70500ms (file: 321-327-327-327-325-350-350-730-701-370-376-376-367-367-137-673-672-367-325-367-375-375-375-375-675-673-637-361-367-362-765-370-175-371-731-175-253-724-726-275-276-237-327-273-2_W.mp3)
  Segment 32: 71500ms ~ 72076ms (file: 160-136-167-617-671-621-675-623-162-163-672-175-156-263-163-173-216-126-216-102-162-167-163-267-126-162-765-325-106-621-163-106-165-106-103-167-106-517-175-175-167-167-765-167-6_V.mp3)
  Segment 33: 73076ms ~ 73652ms (file: 576-236-756-132-260-276-617-607-756-137-637-576-123-657-267-753-760-714-576-653-752-250-235-725-570-507-231-256-162-372-137-760-132-752-321-175-513-706-576-357-573-672-261-632-5_V.mp3)
  Segment 34: 74652ms ~ 75228ms (file: 176-271-672-270-632-316-126-751-752-263-321-102-716-312-127-273-207-203-321-326-132-531-157-723-327-276-276-720-726-270-361-126-501-217-236-230-123-123-231-671-127-203-237-327-3_V.mp3)
  Segment 35: 76228ms ~ 76804ms (file: 621-261-621-623-260-625-625-526-652-265 621-621-621-621-625-623-652-652-625-625_W.mp3)
  Segment 36: 77804ms ~ 78380ms (file: 576-103-103-130-132-163-103-103-317-103-106-136-653-132-136-652-137-167-130-163-136-312-102-103-715-167-156-635-516-316-106-103-547-157-153-625-103-130-103-564-136-167-103-103-3_V.mp3)
  Segment 37: 79380ms ~ 89892ms (file: 267-103-216-216-210-207-120-102-231-206-201-620-103-206-206-216-126-206-103-102-765-276-267-172-123-260-670-216-231-672-627-260-620-102-301-201-132-620-206-103-120-132-103-172-2_V.mp3)
  Segment 38: 90892ms ~ 91468ms (file: 136-136-361-315-302-106-103-750-765-362-176-316-103-130-516-316-312-103-136-132-312-103-517-617-123-120-623-673-261-573-576-573-576 103-671-102-260-260-316-630-136-516-162-153-3_V.mp3)
  Segment 39: 92468ms ~ 93044ms (file: 302-302-302-234-134-143-507-352-302-206-302-524-153-103-125-765-672-307-756-726-542-105-231-137-316-236-102-167-315-513-267-627-623-620-326-132-130-305-530-103-310-713-137-321-1_V.mp3)
  Segment 40: 94044ms ~ 94620ms (file: 216-613-103-263-632-261-163-316-132-165-132-136-126-712-321-103-176-162_V.mp3)
  Segment 41: 95620ms ~ 102712ms (file: 312-301-760-210-752-231-123-765-753-752-175-321-213-136-203-260-130-201-203-231-675-167-103-137-103-571-205-102-571-130-103-517-756-170-130-210-250-765-576-150-135-257-752-571-2_W.mp3)
  Segment 42: 103712ms ~ 104288ms (file: 276-137-123-136-312-763-137-163-167-312-320-312-123-260-251-520-316-326-321-320-132-106-261-127-126-316-213-136-216-216-301-312-103-102-260-230-320-130-136-103-102-602-260-126-1_V.mp3)
  Segment 43: 105288ms ~ 118788ms (file: 657-607-675-136-136-132-671-163-103-170-716-617-570-137-162-103-162-261-741-675-675-564-547-574-574_W.mp3)
  Segment 44: 119788ms ~ 120364ms (file: 163 316-136-316-631-106-362-620 672-762-623-362-361-362-316-136-163-136-631-163-132-163-127-136-312-312-126-126-162-617-176-617-671-167-617-615-165-167-615-617-176-617-165-167-6_W.mp3)
  Segment 45: 121364ms ~ 121940ms (file: 132-165-716-126-126 215-103-203-102-301-516-153-567-671-621-267-132-132-103-103-763-137-103-103-162-103-316-132-310-130-103-106-106-312-132-756-705-753-103-310-312-160-106-167-1_V.mp3)
  Segment 46: 122940ms ~ 123516ms (file: 675-562 137-136 672-716-312-372-376-317-752-571-753-753_W.mp3)
  Segment 47: 124516ms ~ 125092ms (file: 260-206-106-106-670-507-136-132-231-213-261-621-213-241-172-261-621-625-265-103-132-602-652-564-561-236-213-321-312-261-627-215-230-531-570-123-213-263-672-627-672-276-756-126-1_V.mp3)
  Segment 48: 126092ms ~ 126668ms (file: 576-527-261-261-261-261 132-132-132-312-136-132 132-312-613-615 756-765-756-765-675-675-670-672-675-760-763 206-216-216-213-120-230-321-312 213_W.mp3)
  Segment 49: 127668ms ~ 128244ms (file: 321-302-756-756-756-756-726-320-230-302-206-203-236-136-103-106-301-136-312-203-201-213-261-136-301-107-713-703-316-321-312-523-301-103-302-302-302-302-302-126-162-126-132-312-1_V.mp3)
  Segment 50: 129244ms ~ 129820ms (file: 162-167-120-106-167-176-163-612-167-617-167-160-167-671-762-167-316-163-617-167-671-167-613-617-162-167-162-167-167-167-167-167-167-176-615-165-617-165-561-751-175-125-561-156-1_W.mp3)
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
> **Time**: 2026-04-04 03:28:30
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "많이 노력을 하신 것 같아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "많이", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "노력을", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하신", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 400, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "그런데 또 제가 어떤 아이디어를",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그런데", "start_ms": 30, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "또", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "제가", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "어떤", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 5.0},
{"word": "아이디어를", "start_ms": 410, "dur_ms": 166, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "가지고 왔을 때 만약에 선배님",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "가지고", "start_ms": 20, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "왔을", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "때", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "만약에", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "선배님", "start_ms": 430, "dur_ms": 146, "phase_lr_deg": 5.0}
]
},
{
"segment": 4,
"transcript": "이때 시장이 급락을 하면서 같이 빠졌고 전저점을 일시적으로 깼습니다 그런데 다음 날 상한가를 가면서",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이때", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "시장이", "start_ms": 260, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "급락을", "start_ms": 560, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하면서", "start_ms": 960, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 1260, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "빠졌고", "start_ms": 1510, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전저점을", "start_ms": 1910, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일시적으로", "start_ms": 2410, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "깼습니다", "start_ms": 3010, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 3510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 3860, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 4060, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "상한가를", "start_ms": 4210, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가면서", "start_ms": 4710, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "했던 학부생 시절",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "했던", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "학부생", "start_ms": 200, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "시절", "start_ms": 420, "dur_ms": 156, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "그의 노력이 무색하게",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그의", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "노력이", "start_ms": 160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "무색하게", "start_ms": 360, "dur_ms": 216, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "돌아온 답변은 불가능이었습니다",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "돌아온", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "답변은", "start_ms": 235, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "불가능", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이었습니다", "start_ms": 515, "dur_ms": 61, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "고 가볍지 않기 때문입니다 이와 관련해 글로벌 외교 매체로 잘 알려진 더 컨버세이션은",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "고", "start_ms": 45, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "가볍지", "start_ms": 85, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "않기", "start_ms": 265, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 365, "dur_ms": 211, "phase_lr_deg": 0.0},
{"word": "이와", "start_ms": 576, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "관련해", "start_ms": 676, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "글로벌", "start_ms": 826, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "외교", "start_ms": 1026, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "매체로", "start_ms": 1176, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1326, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "알려진", "start_ms": 1406, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1546, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "컨버세이션은", "start_ms": 1606, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "최근 프랑스 정부가 미국 편향적인 외교 노선에서 탈피해 한국을 전략적 파트너로 선정하는 동시에",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "최근", "start_ms": 55, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "프랑스", "start_ms": 205, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정부가", "start_ms": 405, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 605, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "편향적인", "start_ms": 755, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "외교", "start_ms": 1055, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "노선에서", "start_ms": 1205, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "탈피해", "start_ms": 1455, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 1655, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "전략적", "start_ms": 1835, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "파트너로", "start_ms": 2055, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "선정하는", "start_ms": 2305, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "동시에", "start_ms": 2585, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "올해 자국에서 개최되는 G7 정상 회의에 한국을",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "올해", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "자국에서", "start_ms": 160, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "개최되는", "start_ms": 380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "G7", "start_ms": 630, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정상", "start_ms": 810, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "회의에", "start_ms": 960, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 1160, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "은 이 열차는 무수한 주문을 싣고 전통적인 군수 강국들의 이해 관계를 흔들고 있습니다",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "은", "start_ms": 50, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 90, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "열차는", "start_ms": 150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "무수한", "start_ms": 330, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주문을", "start_ms": 530, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "싣고", "start_ms": 730, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전통적인", "start_ms": 880, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 1160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "강국들의", "start_ms": 1310, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이해", "start_ms": 1560, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "관계를", "start_ms": 1680, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "흔들고", "start_ms": 1860, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 2040, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "종합해 보면 오늘처럼 전 세계의 충돌이 심화되는 시대에는 한 나라 군수 산업의 부상이",
"speaker": "speaker_4",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "종합해", "start_ms": 60, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오늘처럼", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 650, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "세계의", "start_ms": 710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "충돌이", "start_ms": 860, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "심화되는", "start_ms": 1060, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "시대에는", "start_ms": 1310, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1550, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "나라", "start_ms": 1610, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "군수", "start_ms": 1730, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "산업의", "start_ms": 1870, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "부상이", "start_ms": 2070, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:28:30*
