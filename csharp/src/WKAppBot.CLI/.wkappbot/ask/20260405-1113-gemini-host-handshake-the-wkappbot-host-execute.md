# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=11:10:59  uptime=33715s  mem=409MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_111047.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 507-510-751-571-570-571-571-571-571-571-715-657-576-725-752-756-753 507-570-157-157-517-157-157_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 103 132-136-751-765-310-137-376-312-312-612-123 237-236-326-273-615-157-765-765-756-675-627-162-762-762-762-762-716-761-762-762-672-765-765-763-765_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 231-137-512-753-753-173-675-765-761-756_V.mp3)
  Segment 4: 4728ms ~ 13512ms (file: 267-263-725-752-721-237-102-710-260-216-267-276-267-276-673-761-637-102-231-267-213-217-217-327-762-130-321-537-236-263-160-230-267-267-210-102-312-312-236-263-267-267-236-326-5_W.mp3)
  Segment 5: 14512ms ~ 15088ms (file: 715-175-571-756-576-571-567-576-567-567-756-576-506 105-105-107-107-170-170-175-715-750-175-715-175-175-175-715-751-715-751-573 507-510-501-150-105-510-175-170-175-170-170-107-1_V.mp3)
  Segment 6: 16088ms ~ 16664ms (file: 602-267-265-261-623-231-261-321-102-120-312-570-267-267-621-602-263-602-265-627-260-623-261-261-216-213-236-167_W.mp3)
  Segment 7: 17664ms ~ 18240ms (file: 175-715-175-571-570-570-571-751-571-573-571-576-576-576-576-576-576-576-576-576-571-576-567-576-573 715-517-517-513-517-157-571-517-571-571-751-576-571-751-570-571-576-576-567-5_V.mp3)
  Segment 8: 19240ms ~ 26548ms (file: 267-267-260-267-263-260-267-625-625-267-306-375-751-750-301-260-260-620-267-263-612-213-127-153-357-375-261-261-627-627-201-123-260-132-153-617-756-612-632-132-123-627-612-216-6_W.mp3)
  Segment 9: 27548ms ~ 28124ms (file: 105-150-150-105-150-170-165-105-170-175-157-157-517-517-157-157-517-154-157-517-157-574-517-517-157-175-750-750-154-105-105-150-150-510-105-157-105-150-105-157-175-157-157-156-1_V.mp3)
  Segment 10: 29124ms ~ 29700ms (file: 576-567-576-657-561 567-657-657-657-657-657-576-657-576-657-675-657-675-657-675-576-657-765-675-657-657-671-561-520-625-567-657-567-657-576-567-657-567-654-657-657-654-657-675-6_W.mp3)
  Segment 11: 30700ms ~ 31276ms (file: 765-756-175-165-657-715-715-765-651-165-173-736-751-576-573-765-175-157-175-571-175-576-576-751-751-715-751-751-715-710-107-715-175-715-571-751-176-716-756-175-176-761-763-765-1_W.mp3)
  Segment 12: 32276ms ~ 32852ms (file: 145-314-135-134-314-103-134-130-134-130-103-134-103-134-143-104-143-103-104-103 310-453-130-103-143-105-145-315-130-103-130-135-143-105-105-134-135-135-314-134 341-134-130-134-1_W.mp3)
  Segment 13: 33852ms ~ 34428ms (file: 271-761-671-163-761-627-617-716-536-516-156-156-735-357-716-361-167-167-617-176-361-716-176-167-671-673-376-367-163-673-671-612-631-317-176-316-753-316-137-137-172-751-715-731-1_W.mp3)
  Segment 14: 35428ms ~ 39568ms (file: 213-123-132-123-123-127-137-132-132-231-213-123-213-123-213-123-213-213-123-213-213-120-123-213-312-132-120-213-132-321-321-321-321-326-362-362-326-312-236-236-236-326-326-231-2_W_stt_if.mp3)
  Segment 15: 40568ms ~ 41144ms (file: 615-615-652-651-615-657-657-657-657-657-657-657-657-657-657-654-625 612-612-612-615-657-657-657-657-567-567-657-657 621-612-621-612-645-645-654-657-657-654-654-645-654-654-645_W.mp3)
  Segment 16: 42144ms ~ 42720ms (file: 572-125-320-236-326-351-312-103-132-256-623-236-321-213-263-231-236-320-236-312-321-213-136-321-132-132-321-216-326-261-312-316-163-126-216-123-320-312-132-162-136-162-312-136-3_W.mp3)
  Segment 17: 43720ms ~ 44296ms (file: 671-672-670-123-162-163-670-613 631_W.mp3)
  Segment 18: 45296ms ~ 51632ms (file: 302-320-231-132-321-236-523-267-267-217-102-231-230-312-732-176-301-765-176-102-260-321-103-130-372-320-201-321-103-103-723-103-276-237-201-203-127-107-312-206-260-231-652-723-1_V.mp3)
  Segment 19: 52632ms ~ 58032ms (file: 201-245-520-263-231-102-206-621-301-325-541-265-102-130-103-130-130-312-731-135-102-123-312-130-130-123-132-103-130-106-203-123-130-103-312-270-273-725-132-310-321-230-103-103-3_V.mp3)
  Segment 20: 59032ms ~ 67492ms (file: 572-120-637-672-320-237-276-371-236-103-310-130-102-236-273-130-276-236-372-173-673-327-237-236-732-372-627-137-320-320-672-327-736-273-213-107-762-132-762-276-725-761-123-123-3_W.mp3)
  Segment 21: 68492ms ~ 69068ms (file: 712-172-137-673-673-631-267-203-120-132-102-326-327-230-123-267-625-256-623-763-762-327-713-173-703-317-302-326-316-361-132-317-137-725-672-627-263-763-756-123-172-371-271-172-1_W.mp3)
  Segment 22: 70068ms ~ 70644ms (file: 172-120-172-216-106-162-216-127-210-651-106-165-521-516-105-562-610 752-217-120-120-215-526-625-675-675-765-672-672-672-675-672-721-275-217-275-721-712-275-172-162-126-162-126-1_W.mp3)
  Segment 23: 71644ms ~ 72220ms (file: 135-137-156-507-725-627-570-723-526-765 302-756-173-705-725-762-570-653-507-132-263-762-721-671-273-215-517-102-317-276-120-752-130-752-765-765-237-750-263-276-506-153-765-235-5_V.mp3)
  Segment 24: 73220ms ~ 73796ms (file: 102-162-612-612-162-612-621-621-263-261-216-126-103-162-162-163-136-126-312-236-321-123-201-213-302-750-730-756-576-675-613-361-621-126-201-216-602-612-617-610-106-675-107-230-2_V.mp3)
  Segment 25: 74796ms ~ 75372ms (file: 501-510-517-501-517-510-507-657-756-765-765-765-765-765-756 501-510-510-501-571-507-501-150-150-157-175-751_V.mp3)
  Segment 26: 76372ms ~ 76948ms (file: 106-167-206-261-162-620-136-631-763-763-760-706-756-756-753-751-230-236-726-762-367-632-132-162-613-367-670-301-106-175-162-132-746-136-163-103-152-162-136-102-156-163-106-617-1_V.mp3)
  Segment 27: 77948ms ~ 78524ms (file: 160-130-137-713-370-317-730-705-170-710-173-317-310-701-173-731 310-103-137-730-137-317-371-735-315-371-137-301-317-137-137-130-301-103-701-371-170-103-173-103-310-713-730-710-3_V.mp3)
  Segment 28: 79524ms ~ 86688ms (file: 312-301-263-216-132-203-206-206-123-132-672-612-231-325-310-123-210-236-320-572-320-103-321-753-157-310-123-237-120-120-107-120-103-753-375-310-736-627-236-201-756-170-713-273-1_W.mp3)
  Segment 29: 87688ms ~ 88264ms (file: 510-510-150-510-105-510-105-150-513-150-501-150-510-501-510-513-501-501 105-154-514-105-105-510-150-153-501-510-510-510-510-510-510-150-150-510-510-157-153-510 105-157-105-105-1_V.mp3)
  Segment 30: 89264ms ~ 99380ms (file: 201-210-213-106-301-321-157-261-263-267-216-132-123-132-530-123-362-563-651-271-251-231-231-106-105-507-153-571-263-267-213-261-230-267-236-263-267-237-326-573-675-617-267-263-2_W.mp3)
  Segment 31: 100380ms ~ 101100ms (file: 263 310-613-263-326-632-132-103-137-165-163-651-162-361-572-576-651-325-612-613-631-615-672-162-106-162-601-163-160-602-351-261_W.mp3)
  Segment 32: 102100ms ~ 102676ms (file: 275-217-275-725-520 521-265-572-725-527-756-275-752-572-752-756-750-570-507-502 570-572-752-572-752-752-752-751-576-527-756-576 510-501-520-571-251-257-527-560-572-530-537-605-6_V.mp3)
  Segment 33: 103676ms ~ 110516ms (file: 273-312-375-263-762-312-130-126-617-621-263-261-167-705-267-267-276-230-720-756-132-273-317-136-260-263-206-237-754-230-273-712-710-127-130-257-103-753-267-321-132-176-570-576-1_V.mp3)
  Segment 34: 111516ms ~ 112092ms (file: 135-136-136-103-134-135-136-103-136-163-136-103-136-135-135-103-103-135-136-573-573-573-753-571-715 316-314-134-314-134-317-103-135-103-301-103-136-130-314-134-134-314 314-314-3_V.mp3)
  Segment 35: 113092ms ~ 121840ms (file: 725-765-231-132-312-715-317-763-765-675-267-725-327-162-267-267-237-173-130-130-213-127-316-312-132-317-172-107-103-176-763-765-762-627-627-627-267-137-163-276-237-371-762-276-2_W.mp3)
  Segment 36: 122840ms ~ 123416ms (file: 156-156-156-165-156-156 507-265-257-275-207-270-237-705-237-251-152-156-156-165-156-156-516 572-507-526-750-752-270-257-637-627-520-752-152-153-156-156-561-156-516-156-156-156-1_V.mp3)
  Segment 37: 124416ms ~ 124992ms (file: 257-520-507-257-257-572-572-756-576-576-756-576-756 502-572-507-502-502-750-572-257-752-576-572-752-527-570-527-576-754-756-574 502-520-570-256-506-562-560-570-576-567-752-572-5_V.mp3)
  Segment 38: 125992ms ~ 126568ms (file: 675-312-132-312-530-103-162-670-632-132-136-231-103-316-132-126-103-106-301-613-107-316-312-321-230-615-672-672-261-261-132-321-320-231-132-301-306-306-361-316-613-361 167-107-1_V.mp3)
  Segment 39: 127568ms ~ 128144ms (file: 517-517-150-150-150-153-150-571-157-157-175-103-107-175_W.mp3)
  Segment 40: 129144ms ~ 134472ms (file: 762-627-612-621-263-623-267-263-260-106-230-216-271-261-216-237-267-213-267-217-627-263-316-301-317-623-602-267-173-137-621-126-127-261-176-273-267-326-237-127-137-310-671-167-1_W.mp3)
  Segment 41: 135472ms ~ 136048ms (file: 571-573-157-103-173-175-107-150-137-507-571-530-175-753-705-571-751-571-715-157-517-103-701-175-103-175-105-501-137-753-751-130-103-103-753-710-571-510-571-507 571-157-157-510-1_V.mp3)
  Segment 42: 137048ms ~ 137624ms (file: 135-157-153-517-153-157-513-513 513-105-517-153-513-517-103-715-135-315-175-375 135-130-153-150-175-135-517-137-310-130-135-137-137-135-310-315-135 153-130-157-751-153-105-130-1_V.mp3)
  Segment 43: 138624ms ~ 139200ms (file: 163-156-165-165-165-715-571-531-513-537-573-527-563-517-517-536-573-536-571-150-156-175-106-510-150-157-157-150-105-710-175-502-125-152-125-576-576-576-576-576-576-567-567-567-6_W.mp3)
  Segment 44: 140200ms ~ 140776ms (file: 152-135-315-615-315-356-356-136-265-216-216-163-261-216-653-513-567-567-651-561-265-157-152-157-175-157-351-321-312-326-236-263-231-231-236-216-625-256-265-325-367-367-627-267-6_W.mp3)
  Segment 45: 141776ms ~ 142352ms (file: 316-267-706-326-561-203-752-316-175-327-126-163-760-375-572-316-523-671-532-627-750-627-632-301-361-612-176-317-136-637-137-315-103-310-316-736-613-376-670-761-137-371-167-361_W.mp3)
  Segment 46: 143352ms ~ 153468ms (file: 672-276-276-302-130-123-670-206-201-120-572-627-601-130-231-125-572-103-130-623-260-760-267-254-312-201-572-752-207-120-230-123-201-620-267-276-671-675-260-267-213-235-103-105-5_W.mp3)
  Segment 47: 154468ms ~ 162784ms (file: 231-201-301-260-206-206-126-136-136-120-213-263-230-170-103-210-312-321-103-102-137-213-230-231-267-206-260-107-103-201-210-206-231-206-260-261-230-312-310-102-210-103-160-206-1_W.mp3)
  Segment 48: 163784ms ~ 164360ms (file: 163-176-176-317-731-765-763-736-763-731-763-736-567-567-756-576-715-756-375-701-570-751-705-706-671-752-750-751-761-376-765-765-752-765-756-576-576-576-673-631-637-367-671-637-6_W.mp3)
  Segment 49: 165360ms ~ 165936ms (file: 574-574-574-574-574-574-574-547-574-574-574-574 574-574-574-574-574-574-574-576-576-574-574-572-574-574-547-574-574-574 576-574-574-574-574-574-574-574-574-574-574-574-570-574-5_W.mp3)
  Segment 50: 166936ms ~ 174100ms (file: 756-267-260-261-260-106-261-130-625-265-620-267-260-736-263-310-265-572-361-521-572-136-132-157-265-623-261-267-561-216-731-136-231-126-612-107-102-157-172-162-132-156-156-125-1_V.mp3)
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
> **Time**: 2026-04-05 11:13:17
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "실제로는 제 영향이 뭐 1% 0.5% 뭐 거의 영향이 없다시피 할 수도 있겠지만",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "실제로는", "start_ms": 0, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 130, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "영향이", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 260, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "1%", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "0.5%", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 390, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 420, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "영향이", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "없다시피", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 550, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 560, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "있겠지만", "start_ms": 570, "dur_ms": 6, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "제가 이 일본에서 팔고 있는 스냅드래곤이 들어간 S20 시리즈",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 0, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 100, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "일본에서", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "팔고", "start_ms": 270, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 330, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "스냅드래곤이", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "들어간", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "S20", "start_ms": 520, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "시리즈", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "얘기 꺼내기가 어려웠을 것 같아요. 근데 선배님이",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "얘기", "start_ms": 0, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "꺼내기가", "start_ms": 80, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어려웠을", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "선배님이", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "아니 아니 너 하고 싶은 거 한번 다 해 봐. 그러고 이제 선배님 리액션이",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "아니", "start_ms": 0, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "너", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "봐.", "start_ms": 1400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그러고", "start_ms": 2500, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4000, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "선배님", "start_ms": 5500, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "리액션이", "start_ms": 7000, "dur_ms": 1784, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "손님들은 다들 봤을 때 이거를 와 저리네라고 보잖아요. 근데 저희 눈에는 어 기와가 있고 단청이 있어",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "손님들은", "start_ms": 0, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "다들", "start_ms": 50, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "봤을", "start_ms": 80, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 110, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "이거를", "start_ms": 130, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 160, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "저리네라고", "start_ms": 180, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "보잖아요.", "start_ms": 250, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 320, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "저희", "start_ms": 350, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "눈에는", "start_ms": 380, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 430, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "기와가", "start_ms": 450, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 490, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "단청이", "start_ms": 520, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "그러니까 이게 직업병이지 타일입니다 전체가",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "그러니까", "start_ms": 0, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "직업병이지", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "타일입니다", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전체가", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "아 이거 다루기 어렵지 않아요? 어려워요. 이거 운반하다가 많이 깨지고 막 난리가 나는데 사실 밑에 실금이라도",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 0, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 30, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "다루기", "start_ms": 70, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어렵지", "start_ms": 130, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "않아요?", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "어려워요.", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "운반하다가", "start_ms": 500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "깨지고", "start_ms": 800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 900, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "난리가", "start_ms": 950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나는데", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 1200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "밑에", "start_ms": 1300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "실금이라도", "start_ms": 1400, "dur_ms": 5908, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "하고 수출국들이 국경을 닫아 버리면 사막 위에서 속수무책이 된다는 것을 말이죠. 아일랜드가 식량 통제권이 없이 무너진 것처럼 중동도 공포에 떨었습니다. 정 그들은 생존을 위해",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "하고", "start_ms": 0, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "수출국들이", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "국경을", "start_ms": 90, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "닫아", "start_ms": 150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "버리면", "start_ms": 190, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "사막", "start_ms": 230, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "위에서", "start_ms": 270, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "속수무책이", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "된다는", "start_ms": 370, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 410, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "말이죠.", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "아일랜드가", "start_ms": 480, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "식량", "start_ms": 510, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "통제권이", "start_ms": 530, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "없이", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(mixed)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": []
},
{
"segment": 11,
"transcript": "내려갈 때 잠깐 삐끗하면 내가 정신을 빨리 차리고 다시 올라간다는 점이에요. 이거는 진짜 중요한 거거든요 외환 시장에서. 그래서 제가 봤을 때는요 지금 너무 잘하고",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "내려갈", "start_ms": 0, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 50, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "잠깐", "start_ms": 70, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "삐끗하면", "start_ms": 100, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 150, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "정신을", "start_ms": 180, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 230, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "차리고", "start_ms": 260, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 300, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "올라간다는", "start_ms": 330, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "점이에요.", "start_ms": 380, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이거는", "start_ms": 420, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 450, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 480, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "거거든요", "start_ms": 510, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "외환", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "사장님! 낯선 땅에서 고생하다가 만나서 더 감동이 큰 걸까요? 체리는 깜짝 방문에 너무 기쁜 나머지 놀라운 표정인데요.",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "사장님!", "start_ms": 0, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "낯선", "start_ms": 40, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "땅에서", "start_ms": 80, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "고생하다가", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 280, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "감동이", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 350, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "걸까요?", "start_ms": 370, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "체리는", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "깜짝", "start_ms": 450, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "방문에", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "움직이고 있다라고 하더라고요 저도 이번 기회를 통해서 좀 공부를 많이 해 봤는데 제조사가 일단 차량 수명 주기 전반에",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "움직이고", "start_ms": 0, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있다라고", "start_ms": 80, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하더라고요", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 240, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 300, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "기회를", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "통해서", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 480, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 570, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 630, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "봤는데", "start_ms": 660, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "제조사가", "start_ms": 1200, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "차량", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수명", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "주기", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "전반에", "start_ms": 4000, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "그러면은 3월 19일에도 대장주로 움직였던 거고요 3월 12일에도 또 대장주로 움직였죠.",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "그러면은", "start_ms": 0, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "3월", "start_ms": 80, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "19일에도", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "대장주로", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "움직였던", "start_ms": 280, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거고요", "start_ms": 360, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "3월", "start_ms": 400, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "12일에도", "start_ms": 440, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 520, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "대장주로", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "이 밑에 2월 1",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 0, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "밑에", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "2월", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "1", "start_ms": 300, "dur_ms": 276, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "그냥 먹어도 될 거 같은데 네 그냥 먹어도 맛있죠. 쌤 해물 튀기면 다른 집하고 차별화가 안 돼 있잖",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "그냥", "start_ms": 0, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "먹어도", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 100, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 130, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "같은데", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 210, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 240, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "먹어도", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "맛있죠.", "start_ms": 340, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "쌤", "start_ms": 410, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "해물", "start_ms": 440, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "튀기면", "start_ms": 480, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 520, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "집하고", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "새로 부임한 단장이 아무도 예상 못한 방식으로 팀을 바꿔 가며 완전히 다른 시즌을 만들어 가는 이야기. 야구 이야기지만 사실은 사람 이야기이자 인생 이야기라서 엄청난 공감을 불러일으켰던 작품이죠. 이번 일본판 리메이크는 일본",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "새로", "start_ms": 0, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "부임한", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "단장이", "start_ms": 100, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "아무도", "start_ms": 160, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "예상", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "못한", "start_ms": 260, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "방식으로", "start_ms": 300, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "팀을", "start_ms": 370, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "바꿔", "start_ms": 410, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "가며", "start_ms": 450, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "이거보다 해지해지 해주고 가세요. 5년 동안 성장 계속 했잖아 그러면요 앞으로 또 1년 2년 뒤에 또 성장하고요. 그래서 제가 기대된다는 말씀을 드리는 거예요 예. 여기서 오히려 뭘 하려고 한다 계속해서 더 막 비중 실으려고 한다 이런",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "이거보다", "start_ms": 0, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "해지해지", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "해주고", "start_ms": 140, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가세요.", "start_ms": 200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "5년", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 310, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "성장", "start_ms": 350, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 400, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "했잖아", "start_ms": 450, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "그러면요", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "준비는 모두 끝. 이제 로봇의 움직임을 제어해 보려는데요.",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "준비는", "start_ms": 0, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "모두", "start_ms": 80, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "끝.", "start_ms": 140, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 170, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "로봇의", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "움직임을", "start_ms": 310, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "제어해", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보려는데요.", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "몇 병 묶여 있는가 11.8kg 정도 됩니다. 이 유물 하나가 이렇게 넓은 공간인데도 불구하고 이게 넘쳐흐르는 거 같은 그런 저도 비슷한 느낌",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "몇", "start_ms": 0, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "병", "start_ms": 30, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "묶여", "start_ms": 60, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있는가", "start_ms": 100, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "11.8kg", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 300, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "됩니다.", "start_ms": 360, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 430, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "유물", "start_ms": 460, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "하나가", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "숏 포지션의 핵심 또한 예측이 아니라 리듬 포착이었습니다. 빠르게 꺾이고 잠시 쉬었다가 다시 떨어지는 그 타이밍 바로 여기가 노아의 조",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "숏", "start_ms": 0, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "포지션의", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "또한", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "예측이", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "리듬", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "이라는 거죠. 기존에 많이 하던 게 이제 스타링크죠. 데이터 사용량으로 보면 스타링크가 더 많을 거라고 지금 보더라고요. 위성을 쌓아 놓은 겁니다 이거 하나씩 여러 개가 한꺼번에",
"speaker": "speaker_16",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "이라는", "start_ms": 0, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "거죠.", "start_ms": 60, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "기존에", "start_ms": 100, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 150, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "하던", "start_ms": 200, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 240, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 260, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "스타링크죠.", "start_ms": 300, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "데이터", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "사용량으로", "start_ms": 430, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "포커스 양보가 필요합니다.",
"speaker": "speaker_17",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 0, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "필요합니다.", "start_ms": 300, "dur_ms": 276, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "Maybe for a few weeks and then go to see some other cities and towns maybe",
"speaker": "speaker_18",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 0,
"audio_type": "speech",
"words": [
{"word": "Maybe", "start_ms": 0, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 100, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 150, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "few", "start_ms": 170, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "weeks", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 300, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "then", "start_ms": 340, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 380, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 410, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "see", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "some", "start_ms": 470, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "other", "start_ms": 500, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "cities", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": null,
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:13:17*
