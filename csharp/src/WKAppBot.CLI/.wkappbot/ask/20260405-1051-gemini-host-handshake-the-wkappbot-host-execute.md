# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=1700  time=10:50:17  uptime=22484s  mem=1918MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_105003.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 312-321-231-263-231-312-321-312-213-316-261-236-312-236-321-312-137-326-213-231-132-213-317-137-317-312-273-153-726-762-672-673-637-632-625-623-517-751-752-712-542-547-253-321-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 576-657-657-627-623-105 753-765-675-765-567-576-567-567-576-631-367-361-136-167-761-376-136-671-165-316-163-316-136-136-136-136-213-132-123-132-312-136-361-136-312-136-316-310-3_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 610-610-621-621-657-765-625-312-157-357-765-756-576-627-675-217-361-725-310-102-130-326-613-136-673-761-157-531-501-573-576-567-576-567-576-576-567-576-576-625-657-651-365-675-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 517-517-501-357-105-165-153-157-571-750-106-107 154-501-157 501-571-517-507-571-105-153-561-516-165-106 570-576-576-576-170-170-507-705-570-570-701-750-105-765-576-571-574-574-5_V.mp3)
  Segment 5: 6304ms ~ 10588ms (file: 726-276-276-762-276-326-236-132-213-627-236-267-276-267-176-312-267-762-672-276-762-675-672-763-617-267-675-617-675-762-762-675-765-762-672-627-326-136-675-762-760-672-312-367-3_W.mp3)
  Segment 6: 11588ms ~ 12164ms (file: 547-547-574-547-547-547-547-547-574-574-507 501-510-105-105-501-517-105-153-510-150-510-570-157-570-517-510-506-501-510-571-570-517-574-570-571-576-576-576-574-574-574-574-547-5_V.mp3)
  Segment 7: 13164ms ~ 13740ms (file: 123-103-312-132-623-526-521-576-532-251-756-327-321-321-156-631-310-657-132-103-132-106-210-176-601-136-123-136-103-316-136-132-136-132-130-103-136-103-163-132-103-106-126-526-1_V.mp3)
  Segment 8: 14740ms ~ 25612ms (file: 126-261-216-201-132-527-574-527-205-754-612-260-576-201-576-103-260-312-123-521-103-132-256-267-765-627-127-521-236-213-103-132-236-102-301-153-216-216-103-310-231-206-132-231-5_V.mp3)
  Segment 9: 26612ms ~ 27188ms (file: 415-103-130-134-103-103-134-103-134-104-103-134-103-103-134-104 136-134-415-145-143-410-146-143-103-135-143-136-163-103-104-146-103-163-136-103-105-146-137-157-571-574-571-574-1_V.mp3)
  Segment 10: 28188ms ~ 28764ms (file: 625-652-326-657-657-653-123-752-752-576-756-576-756-576-752-752 651-657-657-657-675-657-657-657-657-657-657-657-657-675-657-621-612-621-341-312-471-647-547-542-543-504-501-542-5_W.mp3)
  Segment 11: 29764ms ~ 30340ms (file: 752-572-562-756-750 756-705-705-752-207-752-572-576-672-756-572-507-702-576-507-570-572-752-507-756 576-570-507-572-507-572-527-572-750-502-725-572-657-502-570-567-572-570-567-5_V.mp3)
  Segment 12: 31340ms ~ 31916ms (file: 157-513-507-510-153-157-517-501-150-510-510-510-501-514-501-507-510-510-510-501 150-510-501-507-501-501-501-517-153-513-150-105-530-501-105-510-510-517-507-501-501-507-501-510-5_W.mp3)
  Segment 13: 32916ms ~ 42132ms (file: 327-320-276-326-362-756-236-310-371-103-107-123-231-263-321-327-765-765-230-230-267-230-672-321-207-762-230-320-103-725-237-267-236-312-756-726-127-210-120-267-263-762-267-267-2_W.mp3)
  Segment 14: 43132ms ~ 43708ms (file: 371-105-310-137-130-173-310-756-756-371-173 703-307-301-317-735-173-317-617-671-316-167-136-317-731-317-716-716-317-103-130-317-103-310-103-310-107-123-301-107-371 751-371-370-3_V.mp3)
  Segment 15: 44708ms ~ 45284ms (file: 120-715-175-165-156-132-601-326-123-163-612_V.mp3)
  Segment 16: 46284ms ~ 46860ms (file: 567-576-576-715-752-760-657-756-712-756-574-576 547-574-572-574-547-573-543-354-576-517-135-513-531-517-576-576-537-576-574-571-756-570-173-130-310-310-312-310-310-516-517-513-6_W.mp3)
  Segment 17: 47860ms ~ 48436ms (file: 571-157-715-135-175-137-153-127-317-317-175-517-175-735-735-175-756-751-715-175-173-751-713-175-173-175-751-571-135-317-315-317-132-137-132-153-517-571-571-517-175-375-513-175-7_W.mp3)
  Segment 18: 49436ms ~ 54980ms (file: 102-103-103-761-762-726-712-217-172-726-213-261-261-321-312-163-263-321-103-210-231-102-102-163-261-163-617-276-213-260-267-312-213-260-216-216-312-321-620-260-260-267-621-672-1_W.mp3)
  Segment 19: 55980ms ~ 65304ms (file: 210-216-261-213-216-216-276-217-213-216-213-213-216-213-261-273-261-213-201-210-216-213-217-217-216-210-210-251-230-312-752-765-752-756-257-275-273-217-572-251-132-172-316-367-2_W.mp3)
  Segment 20: 66304ms ~ 70588ms (file: 612-167-172-173-327-130-165-307-132-176-315-215-167-107-103-167-137-675-157-136-675-267-160-162-356-362-653-316-132-356-362-356-326-261-327-365-157-576-675-657-756-675-756-275-7_W.mp3)
  Segment 21: 71588ms ~ 72164ms (file: 623-231-623-376-627-671-672-362 673-167-627-612-312-763-632-320-671-263-167-162-160-321-127-123-675-672-765-736-670-176-132-602-671-620-621-157-163-765-167-132-165-671-136-136-2_W.mp3)
  Segment 22: 73164ms ~ 73740ms (file: 756-765-765-760-764-674-765-760-765-764-762-765-756-762-765-765-762-765-762-765-725-752-765-725-756-762-756-765-726-765-675-672-657-652-657-675-675-675_W.mp3)
  Segment 23: 74740ms ~ 82552ms (file: 213-216-103-761-765-753-236-573-216-261-321-326-627-201-231-230-506-321-132-132-132-132-312-123-157-205-267-752-120-215-302-165-210-213-102-157-130-165-213-326-102-231-237-203-2_W.mp3)
  Segment 24: 83552ms ~ 89564ms (file: 321-230-213-237-213-320-230-125-125-132-321-102-105-120-253-231-573-237-103-230-103-235-231-203-267-123-213-316-231-206-230-237-576-203-573-230-320-257-201-316-102-217-203-320-1_V.mp3)
  Segment 25: 90564ms ~ 96108ms (file: 571-327-543-570-321-301-362-132-157-625-726-327-327-567-574-512-172-705-132-321-267-352-321-301-316-103-507-531-574-235-317-725-627-237-130-621-573-273-263-273-267-513-236-327-5_W.mp3)
  Segment 26: 97108ms ~ 106252ms (file: 612-321-231-213-321-326-231-267-256-167-136-236-321-237-572-210-312-267-672-627-123-716-175-231-513-547-527-542-267-260-236-326-267-627-261-231-236-543-542-236-231-267-267-236-2_W.mp3)
  Segment 27: 107252ms ~ 107828ms (file: 351-513-230-213-213-574-675-762-672-607-672-675-621-670-602-607-130-103-106-106-167-167-165-167-165-302-712-652-106-163-103-135-302-301-312-130-573-573-574-507-570-537-157-103-2_W.mp3)
  Segment 28: 108828ms ~ 109404ms (file: 753-237-751-532-753-576-365-735-570-132-721-267-572-375-735-731-375-753-765-736-567-237-756-507-672-162-753-326-257-572-675-512-257-752-570-754-375-675-756-576-367-267-165-231-2_V.mp3)
  Segment 29: 110404ms ~ 110980ms (file: 157-153-157-153-136-631-613-167-163-671-675-765-756-756-572-572-572-572-576-576-160-130-103-135-150-153-106-160-160-103-106-160-163-715-751-715-715-753-756-756-756-752-571-157-5_W.mp3)
  Segment 30: 111980ms ~ 112556ms (file: 621-137-326-625-264-726-172 213-132-637-163 103-102-130-132-102-312_V.mp3)
  Segment 31: 113556ms ~ 114132ms (file: 623-126-675-657-672-672-627-765-675-657-617-675-675-675-675-657_W.mp3)
  Segment 32: 115132ms ~ 115708ms (file: 504 750-750-572-572-753-315-361-316-163-136-162-132-123-276-216-267-276-216-126-621-162-671-627-267-765-725-752-572-756-512-576-756-576-761-157-675-652-765-657-576-752-517-506-5_W.mp3)
  Segment 33: 116708ms ~ 117284ms (file: 126-162-631-163-126-726-726-132-267_W.mp3)
  Segment 34: 118284ms ~ 118860ms (file: 571-157-157-173-157-153-156-165-156-365-135-165-135-165-165-175-175-157-157-150-157-165-165-165-156-156-150-371-312-615-623-610-651-615-650-316-531-135-160-165-160-160-160-106-1_W.mp3)
  Segment 35: 119860ms ~ 120436ms (file: 602-627 176-167-167-612-165-165-615-672-726-762-765-675-765-765-762-762-762-726-672-765-762-675-725-762-715-157-576-756-657-675-765-576-756-765-657-765-625-765-765-756-765-675-7_W.mp3)
  Segment 36: 121436ms ~ 122012ms (file: 627-621-672-675-675-675-657-657-651-657-657-671-675-657-657-675-265-154-354-456-512-456-756-653-570-526-562-652-657-657-657-657-567-657-657-621-265-265-625-657-652-265-256-475-5_W.mp3)
  Segment 37: 123012ms ~ 123588ms (file: 625-267-625-625-627 620-625-267-267-625-267-627-627-261-627-621-625-621-263-263-625-652-657-652-657-653-675 625-627-620-627-623-267-627-625-625-625-627-625-265-267-621-625-657-2_W.mp3)
  Segment 38: 124588ms ~ 125164ms (file: 275-670-607 301-312-315-315-310-135-312-312-312-312-130-135-310-103-312-316-316-312-163-165-135-136-135-574-754-754-576-576-570-541-513-571-567-567-576-756-756-765-756-765-765-7_W.mp3)
  Segment 39: 126164ms ~ 126740ms (file: 576-572-576-567-576-576-572-576 752-572-756-576-572-576-576-576-572-576-752-752 542-541-546-526-542_W.mp3)
  Segment 40: 127740ms ~ 136272ms (file: 675-157-312-132-312-132-123-132-301-632-236-321-237-321-312-132-163-627-613-623-612-613-612-613-673-162-623-123-670-163-547-762-267-230-203-201-623-213-615-623-321-213-631-167-6_W.mp3)
  Segment 41: 137272ms ~ 137848ms (file: 652 652-562-651-657-657-654-651-657-657-657-657-657 562-657-654-657-567-654-657-654-657-657-651-657-567-657-657 652-657-657-657-657-657-564-567-657-675-657-567-657-657-657-657 6_W.mp3)
  Segment 42: 138848ms ~ 139424ms (file: 163-135-514-135-615-162-163-657-572-263-163-567-726-715-712-721-267-306-162-167-671-327-127-326-570-501-356-176-167-103-317-167-764_W.mp3)
  Segment 43: 140424ms ~ 141000ms (file: 130-157-156-157-751-571-571-571-756-716-371-716-763-716-731-715-735-716-371-176-173-175-176-713-716-731-371-756-756-675-672-657-526-672-673-631-765-765-756-751-756-675-675-716-1_W.mp3)
  Segment 44: 142000ms ~ 149236ms (file: 612-312-753-627-301-130-267-213-574-267-627-160-130-130-231-120-637-267-162-130-123-201-261-173-267-326-371-321-213-612-672-617-176-230-236-320-312-135-120-130-103-163-627-123-1_W.mp3)
  Segment 45: 150236ms ~ 150812ms (file: 160-612-130-261-136-621-106-617-165-160-167-167-167-103-126-571-617-167-102-136-163-163-103-162-136-567-620-162-571-715-175-123-257-617-103-167-136-316-137-765-671-601-671-167-1_V.mp3)
  Segment 46: 151812ms ~ 161784ms (file: 321-123-213-213-312-213-627-137-654-630-605-173-761-170-120-267-625-362-627-321-672-632-231-623-672-236-213-103-126-731-170-130-517-126-213-167-263-276-271-270-136-107-106-326-1_W.mp3)
  Segment 47: 162784ms ~ 163360ms (file: 657-617-106-160-160-657-567-657-675-675-671-675-162-267-762-267-267-267-623-265-263-623-625-652-265-263-263-213-167-631-213-312-132-130-672-627-625-765 627-675-103-632-603-607-6_W.mp3)
  Segment 48: 164360ms ~ 164936ms (file: 715-157-715-175-157-751-157-175-751-751 570-571-571-571-175-751-715-175-173-175-715-157-751-170-175-517-715-715-157-751-157-751-715-751-571-130-150-135-150-105-105-150-105-173-6_V.mp3)
  Segment 49: 165936ms ~ 166512ms (file: 173-315-130-713-310-135-317-173-312-107-715-107-310 135-130-301-130-103-103-132-137-137-317-130-310-731-301-130-137-130-370-310-527-175-715-130-310-371-137-120-130-173 135-103-3_V.mp3)
  Segment 50: 167512ms ~ 168088ms (file: 163-135-572-756-576-756-756-576-574-513-315-175-736-762-736-673-763-376-715-573-375-103-107-513-107-317-137-132-163-615-165-165-167-165-167-165-167-361-136-576-756-574-135-375-7_W.mp3)
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
> **Time**: 2026-04-05 10:51:45
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "준비 시간만 3일입니다 3일 이대로 뚜껑을 닫고 무거운 것으로",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "준비", "start_ms": 150, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "시간만", "start_ms": 470, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "3일입니다", "start_ms": 880, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "3일", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이대로", "start_ms": 1750, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "뚜껑을", "start_ms": 2130, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "닫고", "start_ms": 2550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "무거운", "start_ms": 2870, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 3260, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "연기하면서도 같은 진실을 쫓게 되는 상황이 펼쳐지죠 영화 제목인 휴민트는 휴먼 인텔리전스 즉 기계나 장비가 아니라 사람을 통해 얻는 정보를 의미하는데요 영화를 보고 나면 단어의 뜻이 완전히 다르게 느껴집니다 영화에서",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "연기하면서도", "start_ms": 20, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 740, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "진실을", "start_ms": 1020, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "쫓게", "start_ms": 1400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 1650, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "상황이", "start_ms": 1870, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "펼쳐지죠", "start_ms": 2220, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 2770, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "제목인", "start_ms": 3070, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "휴민트는", "start_ms": 3450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "휴먼", "start_ms": 3970, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "인텔리전스", "start_ms": 4280, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "즉", "start_ms": 4960, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "기계나", "start_ms": 5140, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "장비가", "start_ms": 5520, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 5900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사람을", "start_ms": 6250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 6650, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "얻는", "start_ms": 6930, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "정보를", "start_ms": 7180, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "의미하는데요", "start_ms": 7560, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "영화를", "start_ms": 8280, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 8660, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "나면", "start_ms": 8910, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "단어의", "start_ms": 9190, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뜻이", "start_ms": 9540, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 9820, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "다르게", "start_ms": 10240, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "느껴집니다", "start_ms": 10620, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "영화에서", "start_ms": 11300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "이 맑은 지하수와 매년 겨울 시베리아 한기가 해충과 병균을 리셋하는 효과가 복합적으로 작용했기 때문입니다 지형이 만들어낸 수질이 결과적으로 이 땅에 사는 사람들의 생존율을 높이는 방향으로 작",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "맑은", "start_ms": 120, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "지하수와", "start_ms": 400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "매년", "start_ms": 950, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "겨울", "start_ms": 1270, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시베리아", "start_ms": 1570, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "한기가", "start_ms": 2090, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "해충과", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "병균을", "start_ms": 2950, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "리셋하는", "start_ms": 3370, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "효과가", "start_ms": 3920, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "복합적으로", "start_ms": 4330, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "작용했기", "start_ms": 5010, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 5530, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "지형이", "start_ms": 6180, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "만들어낸", "start_ms": 6590, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "수질이", "start_ms": 7110, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "결과적으로", "start_ms": 7520, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 8200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "땅에", "start_ms": 8280, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "사는", "start_ms": 8560, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "사람들의", "start_ms": 8840, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "생존율을", "start_ms": 9360, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "높이는", "start_ms": 9910, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "방향으로", "start_ms": 10360, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "작", "start_ms": 10880, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "저도 할머니한테 김치 하나 배우고 나서 애초 시작할 때 이제 들고 이제 경험을 삼아서 해봤지",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "저도", "start_ms": 30, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "할머니한테", "start_ms": 310, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "김치", "start_ms": 860, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 1160, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "배우고", "start_ms": 1460, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "나서", "start_ms": 1840, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "애초", "start_ms": 2140, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "시작할", "start_ms": 2460, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2910, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 3060, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "들고", "start_ms": 3340, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 3660, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "경험을", "start_ms": 3940, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "삼아서", "start_ms": 4360, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "해봤지", "start_ms": 4740, "dur_ms": 520, "phase_lr_deg": 0.0}
]
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
"transcript": "와 미쳤다 와 그냥 이렇게",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "와", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미쳤다", "start_ms": 300, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 820, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1070, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1390, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "지시멘터리 네이버 닷컴으로 제보해 주시면 소중히 제작하겠습니다",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "지시멘터리", "start_ms": 10, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "네이버", "start_ms": 660, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "닷컴으로", "start_ms": 1080, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "제보해", "start_ms": 1630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주시면", "start_ms": 2080, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "소중히", "start_ms": 2500, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "제작하겠습니다", "start_ms": 2910, "dur_ms": 850, "phase_lr_deg": 0.0}
]
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
"transcript": "있을까 어떤 종목 컨퀘스트가 됐을까 이런 것들을 매일매일 분류하는 그런 과정들도 좀 즐기시면서 공부를 해나가는 게 저는 굉장히 너무 재미있더라고요 저를 제가 이제 그거를 억지로 만들 수가 없는 게",
"speaker": "speaker_7",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "있을까", "start_ms": 40, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 420, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "종목", "start_ms": 730, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "컨퀘스트가", "start_ms": 1050, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "됐을까", "start_ms": 1730, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 2440, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "매일매일", "start_ms": 2820, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "분류하는", "start_ms": 3340, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 3890, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "과정들도", "start_ms": 4190, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 4710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "즐기시면서", "start_ms": 4860, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "공부를", "start_ms": 5510, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "해나가는", "start_ms": 5920, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 6440, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 6560, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 6870, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 7320, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "재미있더라고요", "start_ms": 7640, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "저를", "start_ms": 8460, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 8770, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 9080, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "그거를", "start_ms": 9360, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "억지로", "start_ms": 9740, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "만들", "start_ms": 10150, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "수가", "start_ms": 10460, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 10770, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 11070, "dur_ms": 150, "phase_lr_deg": 0.0}
]
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
"transcript": "리포트들과 하이젠버그의 변호사 출신 연구원인 성병님 그리고 실제 기업 현장의 최전선인 에즈 에이아이 리서치의 타임원님의 심층 리포트",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "리포트들과", "start_ms": 10, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "하이젠버그의", "start_ms": 730, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "변호사", "start_ms": 1510, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "출신", "start_ms": 1930, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "연구원인", "start_ms": 2280, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "성병님", "start_ms": 2830, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 3350, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "실제", "start_ms": 3730, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "기업", "start_ms": 4050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "현장의", "start_ms": 4350, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "최전선인", "start_ms": 4770, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "에즈", "start_ms": 5320, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "에이아이", "start_ms": 5630, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "리서치의", "start_ms": 6150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "타임원님의", "start_ms": 6700, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "심층", "start_ms": 7380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "리포트", "start_ms": 7700, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "있습니다 이란은 중동 전역으로 반격을 가하고 있습니다 혁명수비대 해군은 오늘 성명을 통해",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "있습니다", "start_ms": 25, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "이란은", "start_ms": 545, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 955, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전역으로", "start_ms": 1305, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "반격을", "start_ms": 1855, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가하고", "start_ms": 2305, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 2725, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "혁명수비대", "start_ms": 3245, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "해군은", "start_ms": 3995, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 4445, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "성명을", "start_ms": 4745, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 5195, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "that I've ever been in my life I live in a place that I really really love my friends are great my dog is happy like there's no real crime compared to the states things are cheaper here than the states everything is convenient at 2:00 a.m. if you're hungry you can go out and find somewhere or at least",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "that", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I've", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "ever", "start_ms": 285, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "been", "start_ms": 465, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 615, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 735, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "life", "start_ms": 885, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1235, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "live", "start_ms": 1335, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1585, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 1705, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "place", "start_ms": 1805, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 2125, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 2275, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 2375, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 2695, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 3015, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 3395, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "friends", "start_ms": 3545, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 3965, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "great", "start_ms": 4145, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 4595, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "dog", "start_ms": 4745, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 5065, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "happy", "start_ms": 5245, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 5665, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "there's", "start_ms": 5915, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "no", "start_ms": 6265, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "real", "start_ms": 6465, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "crime", "start_ms": 6785, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "compared", "start_ms": 7235, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 7755, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 7905, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "states", "start_ms": 8055, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "things", "start_ms": 8605, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "are", "start_ms": 8985, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "cheaper", "start_ms": 9165, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "here", "start_ms": 9685, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "than", "start_ms": 9985, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 10235, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "states", "start_ms": 10385, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "everything", "start_ms": 10935, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 11655, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "convenient", "start_ms": 11835, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 12585, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "2:00", "start_ms": 12735, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "a.m.", "start_ms": 13055, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "if", "start_ms": 13355, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you're", "start_ms": 13505, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "hungry", "start_ms": 13805, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 14325, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 14475, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 14695, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "out", "start_ms": 14945, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 15195, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "find", "start_ms": 15375, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "somewhere", "start_ms": 15695, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "or", "start_ms": 16345, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "at", "start_ms": 16525, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "least", "start_ms": 16675, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "면서 소통이란 이런 거다라는 것을 조금 보여줬으면 좋겠습니다 요즘 스마트폰은요 진짜 성능이 옛날보다 많이 상향 평준화가 되었지만 가격이 진짜 마",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "면서", "start_ms": 30, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "소통이란", "start_ms": 310, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 860, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거다라는", "start_ms": 1160, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 1680, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 1960, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "보여줬으면", "start_ms": 2280, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "좋겠습니다", "start_ms": 2960, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "요즘", "start_ms": 3610, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "스마트폰은요", "start_ms": 3930, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 4650, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "성능이", "start_ms": 4970, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "옛날보다", "start_ms": 5380, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 5900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상향", "start_ms": 6200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "평준화가", "start_ms": 6550, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "되었지만", "start_ms": 7130, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가격이", "start_ms": 7680, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 8090, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "마", "start_ms": 8410, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "실까요 그 이유를 지금부터 알아보겠습니다 여러분은 가전에도 주연과 조연이 있다는 거 아시나요 여기서 주연과 조연의 기준은 가전의 효율이",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "실까요", "start_ms": 35, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 445, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이유를", "start_ms": 545, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "지금부터", "start_ms": 925, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "알아보겠습니다", "start_ms": 1475, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "여러분은", "start_ms": 2325, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "가전에도", "start_ms": 2845, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주연과", "start_ms": 3295, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "조연이", "start_ms": 3715, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 4095, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 4445, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아시나요", "start_ms": 4565, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 5045, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "주연과", "start_ms": 5425, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "조연의", "start_ms": 5845, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "기준은", "start_ms": 6225, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "가전의", "start_ms": 6635, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "효율이", "start_ms": 7015, "dur_ms": 410, "phase_lr_deg": 0.0}
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
"transcript": "여러분 풀촌집 성공하기 위하여 짠",
"speaker": "speaker_11",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "여러분", "start_ms": 50, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "풀촌집", "start_ms": 570, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "성공하기", "start_ms": 1020, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "위하여", "start_ms": 1570, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "짠", "start_ms": 1990, "dur_ms": 250, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 10:51:45*
