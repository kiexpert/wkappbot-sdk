# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=12:15:32  uptime=28617s  mem=439MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_121348.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 275-725-273-756-765-756-756-765-675-167-651-156-153-156-162-167-617-671-675-675-671-576-567-567-576-576-576-527-576-567-576-165-615-156-516-156-513-561-531-156-561-531-156-165-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 236-631-260-260-213-637-620-516-567-517-523-325-620-162-326-123-230-176-301-162-671-173-213-175-617-106-167-106-163-312-137-103-106-103-163-136-632-627-513-107-106-162-162-216-6_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 605-167-103-137-137-137-127-176-172-715-761-137-176-613-137-571-657-675-623-130-310-137-130-301-137-607-312-372-507-276-176-213-725-765-316-507-156-263-176-726-103-315-301-301-3_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 672-637-763-316-317-617-136 673 167-176-136-316-671-176-376-167-371_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 126-236-126-321-312-367-310-162-237-123-620-612-316-302-103-763-362-617-162-603-167-176-301-103-317-731-320-720-137-103-302-130-312-263-167-132-176-167-657-675-762-672-762-107-5_V.mp3)
  Segment 6: 7880ms ~ 15764ms (file: 136-312-327-627-230-275-256-752-107-123-276-327-132-675-130-103-310-327-357-763-631-130-176-263-267-261-103-106-106-103-270-102-107-167-123-320-301-103-107-102-120-261-176-132-1_W.mp3)
  Segment 7: 16764ms ~ 21876ms (file: 103-312-631-123-125-135-102-230-236-106-675-736-723-261-276-126-316-672-267-103-261-236-325-135-103-310-137-321-327-123-213-123-103-130-136-170-102-123-123-126-102-321-123-210-1_W.mp3)
  Segment 8: 22876ms ~ 23452ms (file: 316-105-150 105-105-105-103-150-150-103-105-150-150-105-105-105-105 715-751-105-516-156-150-106-106-105-105-105-105-105-105-105-156-105-150-105-157-105-165-150-507-571-157-107-7_V.mp3)
  Segment 9: 24452ms ~ 25028ms (file: 546-546-501-512-521-502-521-321-531-517_V.mp3)
  Segment 10: 26028ms ~ 26604ms (file: 150-105-157-153-105-170-715-751-715-715-175-175-715-175-571-715-715-751-715-715-715-751-751-715 570-157-517-517-517-571-751-753-751-756-751-751-751-751-715-751-715 570-510-175-7_W.mp3)
  Segment 11: 27604ms ~ 28180ms (file: 103-130-103-105-514-154-105-103-106-130-136-167-617-716-715-675-657-657-102-176-176-716-176-761-761-671-167-761-176-761-716-761-716-761-617-176-716-176-671-671-617-617-617-716-1_W.mp3)
  Segment 12: 29180ms ~ 38720ms (file: 107-130-372-175-756-267-321-125-256-203-261-265-162-163-236-574-276-326-263-263-263-361-236-750-123-175-260-125-321-236-260-625-317-316-236-206-617-301-261-316-130-673-106-103-1_V.mp3)
  Segment 13: 39720ms ~ 40296ms (file: 143-134-104-143-104-401-130-140-104-534-134-105-134-104-104-103-147-143-104-103-134 435-431-314-457-154-405-104-103-154-451-314-153-104-107-130-315-103-145-103-143-154-134-135-1_V.mp3)
  Segment 14: 41296ms ~ 47632ms (file: 102-162-263-263-216-162-213-261-752-307-574-260-725-130-102-163-103-106-160-756-267-752-267-123-263-765-367-236-106-102-216-756-756-236-120-756-260-120-103-132-106-261-620-620-7_W.mp3)
  Segment 15: 48632ms ~ 57128ms (file: 236-756-627-103-213-136-213-765-312-263-107-725-132-573-261-312-237-576-567-756-123-576-103-216-261-321-132-106-213-263-261-102-321-231-132-312-261-312-573-567-123-213-236-752-5_W.mp3)
  Segment 16: 58128ms ~ 58704ms (file: 576-576-576-576-576-571-517-571-574-572-527-520_W.mp3)
  Segment 17: 59704ms ~ 66508ms (file: 165-726-310-312-702-236-206-705-760-267-162-230-725-157-130-361-761-527-765-301-765-231-706-763-276-271-201-376-753-267-571-760-267-126-162-312-132-210-267-730-316-206-263-615-2_W.mp3)
  Segment 18: 67508ms ~ 77768ms (file: 213-102-754-574-752-725-321-574-574-237-203-236-203-123-102-321-103-762-327-102-167-574-571-107-672-123-132-123-127-326-751-102-276-203-271-123-627-321-231-276-276-253-125-135-1_W.mp3)
  Segment 19: 78768ms ~ 79344ms (file: 751-630-261-627-670-106-726-762-527-275-627-621-627-621-263 614-162-163-106-765-650-167-617 132-260-260-607-607-670-260-260-620-261-126-130-310-630-321-602-731-267-627-263-263-3_W.mp3)
  Segment 20: 80344ms ~ 80920ms (file: 312-623-261-132-126-103-123-316-312-132-130-136-156-103-156-137-136-103-136-136-106-130-137-132_V.mp3)
  Segment 21: 81920ms ~ 82496ms (file: 275-520-571-570-570-576-576-756-637-513-516-517-570-257-527-572-257-752-756-752-725-275-725-572-752-725-726-752-756-752-572-527-502-512-570-573-571-576-576-751-576-752-756-765-3_W.mp3)
  Segment 22: 83496ms ~ 97176ms (file: 127-132-123-236-132-216 310-765-137-572-257-325-136-263-206-321-201-267-260-672-213-236-132-312-753-123-207-175-326-263-765-206-236-372-763-176-176-532-126-217-573-367-267-675-6_V.mp3)
  Segment 23: 98176ms ~ 98752ms (file: 126-136-136-163-310-631-613-136-167-716-236-623-316-317-310-371-136-126-312-132-126-162-612-613-316-163-613-163-613-136-136-631-627-631-672-621-162 162-632-132_V.mp3)
  Segment 24: 99752ms ~ 100328ms (file: 130-170-173-137 517-175-126-716-715-361-173-165-517-572-136 253-276-627-267-625-301-765-276-572-321-567-132-205-235-571-537-756-527-312-751-517-216 123-765-261-207-123-267-127-2_V.mp3)
  Segment 25: 101328ms ~ 109680ms (file: 126-301-210-310-260-276-213-627-210-260-321-321-130-230-206-123-312-132-130-210-102-257-310-530-126-761-765-670-572-103-123-260-135-123-231-752-102-157-231-312-573-310-527-210-2_W.mp3)
  Segment 26: 110680ms ~ 115864ms (file: 231-320-123-513-320-231-231-321-321-326-312-312-371-612-103-125-216-123-213-615-231-216-120-126-310-310-136-361-736-763-367-621-263-267-273-317-302-302-321-132-321-321-352-735-3_W.mp3)
  Segment 27: 116864ms ~ 125720ms (file: 267-123-154-321-312-623-263-231-102-103-751-231-132-547-267-103-132-271-154-102-301-103-207-276-102-175-276-132-702-127-627-267-160-160-130-607-253-312-130-253-236-752-263-103-2_V.mp3)
  Segment 28: 126720ms ~ 127296ms (file: 527-126-216-261-321-712-756-576-756-765-507-571 752-756-576-756-576-756-576-576-576-576-576-576-756-756-572-574-507-572-576-502-527-527-502-507-502-507-571-506-507-517-501-350-3_V.mp3)
  Segment 29: 128296ms ~ 137692ms (file: 672-657-570-750-752-132-235-354-326-567-132-107-765 310-237-275-456-765-162-261-752-137-130-512-213-231-357-273-231-321-312-715-126-213-231-321-321-753-106-106-715-107-132-154-5_V.mp3)
  Segment 30: 138692ms ~ 139268ms (file: 715-175-715-175-715-751-715-751-751-751-751-751 150-150-157-517-571-570-573-571-571-157-175-715-175-175-175-175-715-175-715-715-715-175-107 150-510-517-517-517-571-751-751-751-7_W.mp3)
  Segment 31: 140268ms ~ 140844ms (file: 236-312-612-621-623 321 672-263-621-362-623-263-362-357-361-312-316-576-765-647-326-326-326 750-637-631-675-673-216-126-264-267 613-361-326-756-765-567-657-567-617-570-756-657-5_W.mp3)
  Segment 32: 141844ms ~ 142420ms (file: 574-576-712-157-756-756-576-576-672-627-632-672-263-106-671-672-261-261-312-103-623-261-132-136-610-627-762-627-132-671-106-106-136-103-153-612-362-726-721-762-721-132-103 132 1_W.mp3)
  Segment 33: 143420ms ~ 143996ms (file: 762-237-672-570-274-231-276-215-570-267-136-136-725-502-152-273-261-601-175-763-217-175 275-732-524-573-257-672-736-627-672-326-235-326 576 206-357-235-312-732-236-617-765-732-5_V.mp3)
  Segment 34: 144996ms ~ 145572ms (file: 375-524-513-326-160-213-635-705-756-236-327-362-367-317-163-137-137-167-136-571-165-167-316-617-376-637-631-167-167-317-173-310-137-137-136-137-132-137-135-671-637-316-360-631-3_W.mp3)
  Segment 35: 146572ms ~ 147148ms (file: 632-623-216-326-157-570-563-573-315-653-567-671-376-637-367-763-367-675-736-356-573-356-632-675-576-576-507-163-362-162-612-765-762-671-675-675-671-617-673-675-675-612-612 762_W.mp3)
  Segment 36: 148148ms ~ 148724ms (file: 751-571-513-751-751-571-173-137-175-172-157-571-157-751-517-715-751-751-571-751-751-571-571-571-517-751-571-571-573-751-715 751-573-571-571-571-751-517-574-571-751-571-571-751-5_W.mp3)
  Segment 37: 149724ms ~ 150300ms (file: 567-653-562-526-652-657-625 620-621-625-627-621-627-267-627-621-261-261-261-261-261-620 263-621-623-627-625-627-621-627-625-267-627-621-261-263-261-261 623-625-621-627-627-261-6_W.mp3)
  Segment 38: 151300ms ~ 151876ms (file: 217-250-216-725-270-215-207-217-725-572-670 570-570-573-574-574-576-750-527-205-752-730-725-756-725-725-572-762-765-572-576-572-357-275 570-570-574-570-507-507-576-762-706-572-6_V.mp3)
  Segment 39: 152876ms ~ 159680ms (file: 654-632-126-136-673-657-625-136-306-637_W.mp3)
  Segment 40: 160680ms ~ 161256ms (file: 574-576-165-756-574-576-756-567-576-516-157-157-167-612-165-153-627-160-716-716-715-157-517-547-175-715-516-576-576-136-576-572-162-671-760 103-610-516-716-516-106-130-756-573-3_W.mp3)
  Segment 41: 162256ms ~ 162832ms (file: 754-571-537-523-256-236-236-263-265-263-132-514-512-257-526-315-125-123-613-675-675-567-657-651-567-765-751-675-657-567-657-675-675-517-157-157-157-237-752-756-265-625-625-326-2_W.mp3)
  Segment 42: 163832ms ~ 164408ms (file: 756-675-752-756-765-765-765-765-765-765-756-675-675-765-675-762-763-376-132-657-756-351-375-761-137-213-201-216-612-261-321-132-137-132-136-317-153-516-567-156-571-517-516-567-5_W.mp3)
  Segment 43: 165408ms ~ 174012ms (file: 275-321-231-317-356-325-723-527-136-312-210-216-275-627-231-162-516-526-216-231-230-762-213-162-130-657-267-673-652-102-754-753-753-126-162-106-132-106-162-127-132-312-371-261-2_W.mp3)
  Segment 44: 175012ms ~ 175588ms (file: 765-756-375-675-672-137-106-103-123-271-216-671-675-136-167-751-576-735-756-275-312-362-326-126-756-765-675-617-675-106-136-715-701-706-270-762-765-170-137-317-107-103-167-162-5_W.mp3)
  Segment 45: 176588ms ~ 185012ms (file: 310-130-132-130-120-301-102-132-320-130-132-103-102-106-103-762-302-623-260-620-102-103-210-216-267-276-726-627-267-267-126-162-236-102-167-326-120-612-136-106-367-136-106-106-1_V.mp3)
  Segment 46: 186012ms ~ 186588ms (file: 172-127-162-312-126-136-361-312-103-301-103-213-201-312-132-301-312-321-302-321-301-367-306-327-361-315-305-526-567-135-316-160-675-672-761-162-617-167-162-612-162-326-263-267-2_V.mp3)
  Segment 47: 187588ms ~ 193420ms (file: 315-310-310-301-301-130-301-137-570-132-130-132-312-107-173-176-312-103-130-301-120-126-361-163-123-276-132-103-103-107-136-136-617-361-571-135-631-652-316-132-302-321-132-263-6_W.mp3)
  Segment 48: 194420ms ~ 198668ms (file: 261-216-213-102-216-261-126-276-260-236-203-267-623-217-276-127-107-136-132-103-267-627-316-132-130-216-326-263-127-627-231-631-627-617-267-276-261-126-726-521-617-675-601-567-6_W_stt_if_1000_of_the.mp3)
  Segment 49: 199668ms ~ 207084ms (file: 132-102-106-162-610-136-312-123-260-265-136-326-312-260-213-236-267-362-231-106-610-102-260-261-260-627-236-231-236-567-765-102-103-320-123-130-210-352-326-236-623-627-321-612-6_W.mp3)
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
> **Time**: 2026-04-05 12:17:26
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "있는 거잖아요? 근데 이게 USB를 꽂고 이렇게 하면은",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "있는", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거잖아요?", "start_ms": 170, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "USB를", "start_ms": 570, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "꽂고", "start_ms": 770, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 920, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하면은", "start_ms": 1070, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "무선으로 우리가 쓸 수 있다. 이거 때문에 어떤 나라별로",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "무선으로", "start_ms": 10, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 410, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있다.", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 610, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 860, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나라별로", "start_ms": 960, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "FSD 기능을",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "FSD", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "기능을", "start_ms": 270, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "좋은데 이건 싼 거 이건 비싼 거. 1달러",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "좋은데", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "싼", "start_ms": 310, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 360, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "비싼", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거.", "start_ms": 610, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "1달러", "start_ms": 660, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "기름을 예열하는데 1, 2, 3, 4 그 중에서도 삼성중공업이 수혜를 볼 수 있다 요런 내용도 이제 계속 나오고 있는 것 같습니다.",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "기름을", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "예열하는데", "start_ms": 230, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "1,", "start_ms": 580, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "2,", "start_ms": 680, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "3,", "start_ms": 780, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "4", "start_ms": 880, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 980, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "중에서도", "start_ms": 1030, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "삼성중공업이", "start_ms": 1330, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수혜를", "start_ms": 1780, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 1980, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2080, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 2130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "요런", "start_ms": 2280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "내용도", "start_ms": 2430, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2630, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 2730, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 2930, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 3130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3230, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "같습니다.", "start_ms": 3280, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "그다음에 미사일 대신에 레이저다. 중동 드론전에 한화의 천광이 뜬다. 천광을 레이저로 막 쏘는 건데 여기에서 한화",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그다음에", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "대신에", "start_ms": 570, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "레이저다.", "start_ms": 820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 1170, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "드론전에", "start_ms": 1370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한화의", "start_ms": 1720, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "천광이", "start_ms": 1970, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뜬다.", "start_ms": 2220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "천광을", "start_ms": 2420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "레이저로", "start_ms": 2670, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 2970, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쏘는", "start_ms": 3070, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 3220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여기에서", "start_ms": 3370, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한화", "start_ms": 3670, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "미국은 우크라이나 전쟁 때처럼 호르무즈 해협의 책임을 전적으로 지지 않을 것이며 한국과 일본의 군이 순찰 임무를 나누어 맡게 될 것이라는",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "미국은", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "우크라이나", "start_ms": 340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전쟁", "start_ms": 740, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때처럼", "start_ms": 990, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 1290, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해협의", "start_ms": 1640, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "책임을", "start_ms": 1940, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전적으로", "start_ms": 2240, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지지", "start_ms": 2590, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "않을", "start_ms": 2790, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것이며", "start_ms": 2990, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "한국과", "start_ms": 3290, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "일본의", "start_ms": 3590, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "군이", "start_ms": 3890, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "순찰", "start_ms": 4090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "임무를", "start_ms": 4340, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나누어", "start_ms": 4640, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맡게", "start_ms": 4940, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 5140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것이라는", "start_ms": 5240, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "보시면 알파 트렌드가 파란색으로 전환됐다는 건 매수세가 들어왔다는 신호이죠. 그런데 중요한 건 그 다음이라고 합니다. 그 캔들이 얼마나 강하게 닫히느냐 그게 첫 번째 기준이에요. 이게 제일 중",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "보시면", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "알파", "start_ms": 330, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "트렌드가", "start_ms": 580, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "파란색으로", "start_ms": 980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전환됐다는", "start_ms": 1430, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 1930, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "매수세가", "start_ms": 2030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들어왔다는", "start_ms": 2430, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "신호이죠.", "start_ms": 2880, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 3280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 3580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 3880, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3980, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다음이라고", "start_ms": 4080, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 4530, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 4780, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "캔들이", "start_ms": 4880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 5230, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "강하게", "start_ms": 5530, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "닫히느냐", "start_ms": 5880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 6330, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "첫", "start_ms": 6530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 6630, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "기준이에요.", "start_ms": 6830, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 7330, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 7530, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "중", "start_ms": 7730, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "어? 여기서! 여기서! 여기서!",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "어?", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기서!", "start_ms": 210, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "여기서!", "start_ms": 510, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "여기서!", "start_ms": 810, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "turnips I use turnips if I have um you know gochu I'll use gochu and stuff like that. So yeah have to be creative.",
"speaker": "speaker_7",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "turnips", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "use", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "turnips", "start_ms": 750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "if", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 1400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "um", "start_ms": 1600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 1950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "gochu", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "I'll", "start_ms": 2450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "use", "start_ms": 2650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "gochu", "start_ms": 2850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 3200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "stuff", "start_ms": 3350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 3600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "that.", "start_ms": 3750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "So", "start_ms": 3950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "yeah", "start_ms": 4100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 4300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 4500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 4600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "creative.", "start_ms": 4750, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "주어진 재료로 최상의 맛",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "주어진", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "재료로", "start_ms": 420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "최상의", "start_ms": 770, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "맛", "start_ms": 1120, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "인도 사람이 한국에서 차별을 받았다는 이야기를 들은 적이 있었기 때문이에요. 아무리 좋아하는 나라라도 혹시 내가 상처받게 되면 어떡하지 이런 생각이 계속 마음에 남았던 거죠. 게다가 영국에서 유학 중인 언니가 현지에서 차별을 겪었다는 이야기를 들려주면서 해외에 혼자 가는 게 더 무섭게 느껴졌다고 합니다. 그",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "인도", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 340, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국에서", "start_ms": 690, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "차별을", "start_ms": 1090, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "받았다는", "start_ms": 1440, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이야기를", "start_ms": 1890, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들은", "start_ms": 2290, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "적이", "start_ms": 2540, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있었기", "start_ms": 2740, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문이에요.", "start_ms": 3090, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아무리", "start_ms": 3690, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 4040, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나라라도", "start_ms": 4440, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "혹시", "start_ms": 4890, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 5190, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "상처받게", "start_ms": 5440, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "되면", "start_ms": 5840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어떡하지", "start_ms": 6090, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 6540, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 6790, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 7140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "마음에", "start_ms": 7440, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "남았던", "start_ms": 7790, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거죠.", "start_ms": 8140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "게다가", "start_ms": 8440, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "영국에서", "start_ms": 8840, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유학", "start_ms": 9290, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중인", "start_ms": 9590, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "언니가", "start_ms": 9840, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "현지에서", "start_ms": 10190, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "차별을", "start_ms": 10640, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "겪었다는", "start_ms": 10990, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이야기를", "start_ms": 11440, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "들려주면서", "start_ms": 11840, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해외에", "start_ms": 12440, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "혼자", "start_ms": 12790, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가는", "start_ms": 13090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 13340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 13440, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "무섭게", "start_ms": 13540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "느껴졌다고", "start_ms": 13940, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "합니다.", "start_ms": 14540, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 14890, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "믿었던 사람한테 사기까지 당하면서 그야말로 바닥까지 떨어집니다. 이게 정말 내가 꿈꾸던 한국 맞아? 라며 좌절하던 순간 영화는 진짜 이야기를 꺼내기 시작합니다. 화려한 도시 뒤",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "믿었던", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사람한테", "start_ms": 380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사기까지", "start_ms": 830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "당하면서", "start_ms": 1280, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그야말로", "start_ms": 1780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바닥까지", "start_ms": 2180, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "떨어집니다.", "start_ms": 2630, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 3280, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 3530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 3830, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "꿈꾸던", "start_ms": 4080, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 4480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맞아?", "start_ms": 4780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "라며", "start_ms": 5130, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좌절하던", "start_ms": 5380, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "순간", "start_ms": 5880, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "영화는", "start_ms": 6180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 6530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이야기를", "start_ms": 6830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "꺼내기", "start_ms": 7280, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시작합니다.", "start_ms": 7680, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "화려한", "start_ms": 8380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "도시", "start_ms": 8830, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뒤", "start_ms": 9130, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "많은 갤럭시 사용자들이 약간 뒤통수가 얼얼할만한 소식이 최근에 좀 들려와요. 그건 바로",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "많은", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "갤럭시", "start_ms": 320, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사용자들이", "start_ms": 720, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "약간", "start_ms": 1270, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뒤통수가", "start_ms": 1520, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "얼얼할만한", "start_ms": 2020, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "소식이", "start_ms": 2620, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "최근에", "start_ms": 3020, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 3420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "들려와요.", "start_ms": 3570, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그건", "start_ms": 4070, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 4320, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "버티어 왔습니다. 그러나 이란은 항복하지 않았습니다. 오히려 전쟁의 방식을 바꿨습니다. 한 번에 쏟아붓던 미사일 파상공세 대신 상대의 방공망 재고를 갉아먹는 드론과 분산",
"speaker": "speaker_11",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "버티어", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "왔습니다.", "start_ms": 490, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그러나", "start_ms": 1040, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이란은", "start_ms": 1440, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "항복하지", "start_ms": 1840, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "않았습니다.", "start_ms": 2340, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "오히려", "start_ms": 2990, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전쟁의", "start_ms": 3390, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방식을", "start_ms": 3740, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "바꿨습니다.", "start_ms": 4140, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4790, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번에", "start_ms": 4940, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "쏟아붓던", "start_ms": 5190, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 5690, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "파상공세", "start_ms": 6140, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "대신", "start_ms": 6740, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "상대의", "start_ms": 7090, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방공망", "start_ms": 7440, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "재고를", "start_ms": 7890, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갉아먹는", "start_ms": 8290, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "드론과", "start_ms": 8790, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "분산", "start_ms": 9240, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "자! 이렇게 지금까지 해양수산부와 함께 차세대 쇄빙 연구선에 대해서 알아봤습니다. 쇄빙 연구",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "자!", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지금까지", "start_ms": 520, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해양수산부와", "start_ms": 970, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 1620, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "차세대", "start_ms": 1920, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "쇄빙", "start_ms": 2320, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "연구선에", "start_ms": 2620, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대해서", "start_ms": 3120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "알아봤습니다.", "start_ms": 3520, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "쇄빙", "start_ms": 4220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "연구", "start_ms": 4520, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "싸는 포장지도 우리 택배 박스 보낼 때 여러가지로 싸잖아. 신문으로 한번 싸고",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "싸는", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "포장지도", "start_ms": 280, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 730, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "택배", "start_ms": 930, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "박스", "start_ms": 1180, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보낼", "start_ms": 1430, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1680, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여러가지로", "start_ms": 1780, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "싸잖아.", "start_ms": 2280, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "신문으로", "start_ms": 2680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 3130, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "싸고", "start_ms": 3380, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(mixed)",
"speaker": "speaker_14",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "Grab", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 210, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "little", "start_ms": 260, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "자, 그런데 여러분 쇄빙선 아시죠? 이 녀석들은 다릅니다. 얼음판을 정면으로 들이받고 얼음을 그냥 가로",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 20, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쇄빙선", "start_ms": 870, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아시죠?", "start_ms": 1320, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1720, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "녀석들은", "start_ms": 1820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다릅니다.", "start_ms": 2270, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "얼음판을", "start_ms": 2820, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정면으로", "start_ms": 3320, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "들이받고", "start_ms": 3820, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "얼음을", "start_ms": 4370, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 4770, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가로", "start_ms": 5070, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "교수님이 저한테 주신 로봇 주제가 손바닥에 올라갈 수 있는 그런 보행 로봇 시스템을 만들어 봐라 이랬습니다. 그래서 로봇",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "교수님이", "start_ms": 20, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저한테", "start_ms": 470, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주신", "start_ms": 820, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 1120, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "주제가", "start_ms": 1420, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "손바닥에", "start_ms": 1820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "올라갈", "start_ms": 2270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2620, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2720, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 2920, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보행", "start_ms": 3170, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 3420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시스템을", "start_ms": 3720, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "만들어", "start_ms": 4170, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "봐라", "start_ms": 4570, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이랬습니다.", "start_ms": 4870, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 5520, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 5920, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "진짜 맛있거든요. 이건 그냥 진짜 그냥 던졌어요. 진짜 와 이건 꼭 가셔야 해요. 와!",
"speaker": "speaker_16",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 10, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "맛있거든요.", "start_ms": 310, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 860, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1360, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1660, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "던졌어요.", "start_ms": 1910, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 2360, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 2660, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 2860, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 3110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가셔야", "start_ms": 3260, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해요.", "start_ms": 3660, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와!", "start_ms": 3960, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "구울 때 사라락 피어오르는 게 일품이었거든요. 소스 두 개를 사용해 하나는",
"speaker": "speaker_17",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "구울", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사라락", "start_ms": 420, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "피어오르는", "start_ms": 820, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 1370, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "일품이었거든요.", "start_ms": 1470, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "소스", "start_ms": 2270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 2620, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "개를", "start_ms": 2720, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "사용해", "start

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:17:26*
