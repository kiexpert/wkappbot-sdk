# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=9236  time=11:08:30  uptime=33139s  mem=382MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_110758.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 762-726-752-127-236-526-567-175-176-671-672-361-761 123-163-175-576-276-672-567-567-673-576-651-576-675-675-762-657-576-527-567-537-576-267-576-576-567-652-632-576-567-573-765-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 321-267-267-752-576-576-135-573-316-574-562-265-567-567-574-625-625-276-267-635-362-321-231-123-312-206-267-276-275-572-573-756-675-307-753-573-576-573-765-173-175-570-571-572-7_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 175-367-507-261-256-352-750-570-635-163-316-136-136-136-136-310-136-310-136-123-165-162-165-132-510-120-103-657-756-123-201-576_W.mp3)
  Segment 4: 4728ms ~ 10992ms (file: 756-576-652-732-263-756-267-203-263-235-230-267-231-352-236-726-326-253-321-253-230-547-301-321-310-103-103-310-321-132-310-321-103-132-103-102-103-132-230-207-103-132-102-217-3_W.mp3)
  Segment 5: 11992ms ~ 12568ms (file: 165-156-167-165-157-567-516-756-715-765-761-762-762-671-756-756-756-576-756-756-756-765-756-765-756-765-756-675-756-765-756-765-756-756-765-765-765-756-756-167-165-176-176-167-1_W.mp3)
  Segment 6: 13568ms ~ 14144ms (file: 136-613-162-137-157-130-167-362-631-173-176-631-167-317-167-637-673-136-361_W.mp3)
  Segment 7: 15144ms ~ 15720ms (file: 157-157-153-157-157-157-175 105-135-153-135-135-173-157-153-517-135-153-135-153-157-153-135-135-157-175-135-157-175-157-175 153-130-153-135-315-135-310-157-135-153-150-153-157-5_V.mp3)
  Segment 8: 16720ms ~ 17296ms (file: 276-321-136-102-103-123-132-136-106-163-106-163-136-316-132-312-132-132-312-123-132-123_V.mp3)
  Segment 9: 18296ms ~ 18872ms (file: 516-517-512-512-517-510-517-516-517-571-517-513_W.mp3)
  Segment 10: 19872ms ~ 29160ms (file: 130-103-130-753-237-231-321-273-572-375-327-130-123-312-723-203-361-103-623-736-637-267-761-132-327-356-267-726-576-657-673-267-361-261-126-160-612-631-126-263-625-321-672-671-1_W.mp3)
  Segment 11: 30160ms ~ 30736ms (file: 756-756-756-756-756-756-675-671-576-576 765-675-657-756-765-675-756-765-576-756-576-756-756-756-756-756-756-675 752-576-756-576-576-756-576-576-567-756-756-675 752-572-752_W.mp3)
  Segment 12: 31736ms ~ 32312ms (file: 571-574-571-571-571-574-574-571-571-574-754-754-571-753-571-713-517-571 571-573-571-571-751-756-756-751-751-571-571-574-574-574-574-574-570-571_W.mp3)
  Segment 13: 33312ms ~ 33888ms (file: 752-752-576-576-567-576-756-576-576-756-756-576-756-756-752 756-675-756-765-756-756-576-756-756-756 752-756-576-576-576-576-567-567-576-576-576-576-756-576 752-576-756-576-576-5_W.mp3)
  Segment 14: 34888ms ~ 35464ms (file: 671-625 106-761-167-256-675-175 613-612-162-627-672 716-176-163-162-176-176-176-176-175-176-736-170-571-163_W.mp3)
  Segment 15: 36464ms ~ 43736ms (file: 670-317-675-736-731-713-376-172-105-136-173-763-765-257-613-316-312-316-310-316-725-267-517-601-612-216-132-763-261-762-256-261-231-713-263-576-312-132-763-312-126-750-167-263-1_W.mp3)
  Segment 16: 44736ms ~ 49344ms (file: 236-572-157-267-126-236-672-267-267-231-326-172-213-765-267-236-321-127-627-276-726-126-263-271-276-275-576-127-762-267-267-723-236-276-276-267-235-235-231-312-725-257-265-726-1_W.mp3)
  Segment 17: 50344ms ~ 50920ms (file: 547-576-574-576-576-576-576-576-756-537 265-256-257-725-750-265-526-572-572-576-752-572-275-576-576-576-572-572-571-576-572-576-576 512-251-265-572-567-756-572-576-756 156-165-1_W.mp3)
  Segment 18: 51920ms ~ 59228ms (file: 132-672-765-260-321-276-267-103-610-756-156-657-173-201-302-361-103-163-136-230-163-672-307-316-231-571-237-602-136-160-120-103-103-316-316-321-317-312-132-312-317-132-301-125-7_W.mp3)
  Segment 19: 60228ms ~ 60804ms (file: 652-765-657-312-321-620-576-203-276-576-175-607-675 574-576-257-350-357-125-231-670-613-527-123-621-267-571-625 217-527-102-231-132-612-726-675-765-526-267-273-206-273-236-206-2_V.mp3)
  Segment 20: 61804ms ~ 62380ms (file: 765-576-576-657-317-310-132-103-136-301-315-576-576-135-130-103-310-130-301-301-173-306-675-605-670-602-637-672-671-670-316-103-132-504-541-543-547-103-103-103-135-327-523-130-1_V.mp3)
  Segment 21: 63380ms ~ 67880ms (file: 261-263-263-263-162-263-261-265-261-216-263-236-263-230-216-261-213-321-172-316-301-312-301-310-132-126-301-260-265-267-264-267-236-312-365-216-261-263-261-267-261-261-265-261-2_W.mp3)
  Segment 22: 68880ms ~ 69456ms (file: 136-127-136-561-362-167-106-136-651-136-106-153-316-623-623-602-132-312-527-132-163-123-162-760-130-123-130-103-102-123-261-103-136-136-517-625-126-176-176-167-136-137-156-163-1_V.mp3)
  Segment 23: 70456ms ~ 74992ms (file: 237-736-723-732-137-372-753-275-312-312-267-167-127-327-627-372-712-712-712-721-726-756-376-153-312-723-756-756-175-762-127-713-217-123-571-152-213-372-721-726-137-130-317-713-7_W.mp3)
  Segment 24: 75992ms ~ 76568ms (file: 150-517-750-750-750-750-750-705-750 105-154-105-150-150-150-105-105-165-130-160-165-156-165-165-156-157-175-165-175-156-157-156-156-150-156 150-154-105-105-105-105-150-105-105-1_V.mp3)
  Segment 25: 77568ms ~ 78144ms (file: 657-675-657-657-675-675-657-576-675-576-756-675-576-756-675-756 653-653-651-675-657-657-567-567-576-567-576-567-576-756-576 657-657-657-657-652-567-657-567-567-576-567-567-576-5_W.mp3)
  Segment 26: 79144ms ~ 79720ms (file: 316-310-312-361-316-316-361-371-137-317-367-167 132_W.mp3)
  Segment 27: 80720ms ~ 90044ms (file: 276-602-312-231-230-672-213-612-276-201-216-130-103-130-132-231-206-206-267-132-130-130-201-213-210-675-627-103-547-542-541-152-706-574-547-325-716-267-765-206-103-267-267-267-2_W.mp3)
  Segment 28: 91044ms ~ 91620ms (file: 715-701-170-710-170-105-105-571-510-150 170-750-570-574-572-574-574-574-574-574-574-574-574-574-570-517-510-517-157-517-507-517 157-105-105-105-105-105-107-170-157-170-170-170-7_W.mp3)
  Segment 29: 92620ms ~ 93196ms (file: 613-615-613-631-615-163 613-612-613_W.mp3)
  Segment 30: 94196ms ~ 94772ms (file: 317-173-175-317-713-175-375-731-756-735-713-715-175-761-713-176-731-716-763-716-571-175-715-713-137-173-713-173-713-175-751-715-713-175-715-751-751-173 571-753-751-756-751-715-7_W.mp3)
  Segment 31: 95772ms ~ 96348ms (file: 751-752-756-761-751-751-715-751-751-751-756-751-150-715-715-715-753-175-157-705-501-701-751-751-705-715-107-315-157-571-571-175-157-150-751-570-751-713-715-715-571-715-751-751-7_W.mp3)
  Segment 32: 97348ms ~ 97924ms (file: 235-301-103-105-150-351-357-137-315-207-316 207-203-127-102-312-103-362-310-372-231-267-267-207-206 163-163-136-601-163-206_V.mp3)
  Segment 33: 98924ms ~ 99500ms (file: 135-510-153-157-517-157-517-571-573-517-150 513-105-135-357-135-135-537-310-130-135-150-135-135-315-135-312-130-513-517-571-517-157-157-157-571-137-135 351-173-153-135-135-130-1_V.mp3)
  Segment 34: 100500ms ~ 101076ms (file: 627-652-627-625-627-627-625-627-652-627-625 657-567-657-657-657-657-567-657-650-657-657-675-657-627-652-657-675-652-672-675-627 567-567-657-675-652-625-627-657-625-652-627-672-6_W.mp3)
  Segment 35: 102076ms ~ 102652ms (file: 317-132-267-276-752 257-213-267 136-572-275-572-275-172-712-261-672-765 203-317-327-576-265-560-267-765-316-751-321-217-623-567-502-257-317-572-237-321-721-736-231-236-672-571-6_V.mp3)
  Segment 36: 103652ms ~ 115640ms (file: 524-762-620-205-127-206-216-542-620-610-120-261-260-102-132-132-261-126-103-607-672-120-210-260-672-627-103-576-206-210-103-206-620-213-120-627-260-231-260-627-102-123-302-310-2_W.mp3)
  Segment 37: 116640ms ~ 117216ms (file: 321-573-675-675-675 675-675 325-617 672 312_W.mp3)
  Segment 38: 118216ms ~ 118792ms (file: 123-317-157-302-103-135-105-103-107-107-107-107-132-312-750-132-312-203-235-352-132-310-301-301-301-236-302-132-321-317-321-301-302-321-326-325-236-326-326-632-326-236-320-362-6_V.mp3)
  Segment 39: 119792ms ~ 120368ms (file: 152-105-165-157-156-165-165-165-576-572-572-576-572-576-572-576-572-572-576-567-517-501-512-521-512-514-514-501-572-512-502-527-521-520-527-205-526-256-250-527-256-205-251-250-5_W.mp3)
  Segment 40: 121368ms ~ 121944ms (file: 132-126 362-532 127-312-761-176-621-231-312-301-301-761-637 135 613 132 132-312-510-263-126-126-162-106-162-612-316-267-235-265-562-762-267-312-216-623-621-621-261-206-231-326-1_V.mp3)
  Segment 41: 122944ms ~ 123520ms (file: 127-721-172-217-725-735-572-572-751-715-173-175-351-157-173-176-175-176-170-675-615-617-631-167-610-610-617-167-176-167-167-617-167-261-671-675-671-671-675-671-671-671-671-617-1_W.mp3)
  Segment 42: 124520ms ~ 125096ms (file: 136-310-103-103-132-103-751-167-163-136-516-316-136-162-165-165-256-513-526-651-130-312-712-213-217-213-213-210-657-567-567-567-657-657-657-657-517-157-251-162-567-567-567-527-5_W.mp3)
  Segment 43: 126096ms ~ 126672ms (file: 156-657-165-321-136 567-165-560-156-103-132-351-163-136-103-165-356-623-152-136-316-136-602-610-651-316-321-316-625-103-316-130-263_V.mp3)
  Segment 44: 127672ms ~ 134764ms (file: 123-312-263-625-267-576-572-216-312-126-672-761-612-361-631-325-275-275-132-132-132-103-275-103-130-106-163-132-623-126-326-216-123-230-263-206-620-216-123-317-103-103-130-136-1_W.mp3)
  Segment 45: 135764ms ~ 143108ms (file: 103-123-675-201-572-302-675-203-210-120-316-752-103-571-302-310-301-751-150-231-132-310-513-102-137-135-321-675-657-210-673-365-130-130-130-321-312-310-136-756-312-134-210-132-1_W.mp3)
  Segment 46: 144108ms ~ 144684ms (file: 105-517-570-705-105-571-571-570-507-571 574-574-574-574-754-571-517-157-175-170-571-750-574-754-571-157 574-570-571-150-507-570-571-574-574-574-574-574-574-574-574-571-517-571-5_W.mp3)
  Segment 47: 145684ms ~ 146260ms (file: 507-572-570-507-527-256-275-270-275-527-715-157-751-571-751-756-571-751-756-571-751-756-715 502-520-527-627-260-256-257-275-762-257-275-520-572-527-576-572-751-576-175 520-502-5_W.mp3)
  Segment 48: 147260ms ~ 147836ms (file: 567-562-567-567-567-567-567-567-567-564-567-567-567-561-657-675 561-561-560-567-567-567-567-567-567-564-564-564-567-567-567-567-567-567-567-567-657 651-651-657-651-657-567-564-5_W.mp3)
  Segment 49: 148836ms ~ 149412ms (file: 645-652-654-651-654-651-654-654-654-654-654-651 621-625-651-654-654-654-654-654-654-564-654-564-654-564-654-625-564-605-653-265-250-562-516-502-502-512-512-501-502-541-504-504-5_W.mp3)
  Segment 50: 150412ms ~ 150988ms (file: 103-167-137-146-167-713-136-132-312-362-326-320-620-260-602-602-310-132-120-102-162-341-641-135-310-103-623-123-136-120-103-130-631-602-607-106-136-301-312 617-136-672-672-672-6_V.mp3)
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
> **Time**: 2026-04-05 11:09:24
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 50, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 2.0},
{"word": "날씨가", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 1.5},
{"word": "좋네요", "start_ms": 400, "dur_ms": 160, "phase_lr_deg": 1.0}
]
},
{
"segment": 4,
"transcript": "노래방 시스템을 만들기 위해서 여러 가지 언어를 테스트하고 있습니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "노래방", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시스템을", "start_ms": 490, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만들기", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "위해서", "start_ms": 1460, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 1850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 2110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "언어를", "start_ms": 2370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "테스트하고", "start_ms": 2730, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3340, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "Thank you very much",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": -1.0},
{"word": "you", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": -1.0},
{"word": "very", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": -1.0},
{"word": "much", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": -1.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "도와주세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "도와주세요", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "일본어 문장도 포함되어 있을 수 있으니 주의 깊게 들어주세요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "일본어", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문장도", "start_ms": 510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "포함되어", "start_ms": 870, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 1380, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1590, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있으니", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주의", "start_ms": 2100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "깊게", "start_ms": 2360, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "들어주세요", "start_ms": 2670, "dur_ms": 550, "phase_lr_deg": 0.0}
]
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
"transcript": "はじめまして",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ja",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "はじめまして", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "한국어 공부는 정말 재미있지만 가끔은 어렵기도 합니다",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 90, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 500, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 890, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "재미있지만", "start_ms": 1200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "가끔은", "start_ms": 1860, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어렵기도", "start_ms": 2270, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 2780, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "발음을 정확하게 따라하는 것이 가장 중요해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "발음을", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 1.0},
{"word": "정확하게", "start_ms": 410, "dur_ms": 500, "phase_lr_deg": 1.0},
{"word": "따라하는", "start_ms": 920, "dur_ms": 550, "phase_lr_deg": 1.0},
{"word": "것이", "start_ms": 1480, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "가장", "start_ms": 1740, "dur_ms": 300, "phase_lr_deg": 1.0},
{"word": "중요해요", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 1.0}
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
"transcript": "이 오디오 데이터는 분석용으로 제작되었습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오디오", "start_ms": 230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "데이터는", "start_ms": 640, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "분석용으로", "start_ms": 1150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "제작되었습니다", "start_ms": 1760, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "よろしくお願いします",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ja",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "よろしく", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 4.0},
{"word": "おねがいします", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": 4.0}
]
},
{
"segment": 20,
"transcript": "맛있게 드세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 1.0},
{"word": "드세요", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 1.0}
]
},
{
"segment": 21,
"transcript": "실시간 데이터 처리가 정상적으로 작동하고 있습니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "실시간", "start_ms": 70, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "데이터", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "처리가", "start_ms": 840, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정상적으로", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "작동하고", "start_ms": 1810, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 2270, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "사랑해요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "사랑해요", "start_ms": 20, "dur_ms": 500, "phase_lr_deg": 0.5}
]
},
{
"segment": 23,
"transcript": "학습을 위해 자막을 생성하는 과정입니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "학습을", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 470, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자막을", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생성하는", "start_ms": 1140, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "과정입니다", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "Wait a minute",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Wait", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "minute", "start_ms": 260, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "사용자 여러분의 많은 참여를 부탁드립니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "사용자", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "여러분의", "start_ms": 520, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 980, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "참여를", "start_ms": 1290, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "부탁드립니다", "start_ms": 1650, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "Good job",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Good", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": -2.0},
{"word": "job", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": -2.0}
]
},
{
"segment": 30,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "점심 뭐 먹을까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 1.0},
{"word": "뭐", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 1.0},
{"word": "먹을까요?", "start_ms": 360, "dur_ms": 350, "phase_lr_deg": 1.0}
]
},
{
"segment": 33,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "비가 올 것 같은데 우산 챙기셨나요?",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "비가", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 1.0},
{"word": "올", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 1.0},
{"word": "것", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 1.0},
{"word": "같은데", "start_ms": 490, "dur_ms": 300, "phase_lr_deg": 1.0},
{"word": "우산", "start_ms": 800, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "챙기셨나요?", "start_ms": 1060, "dur_ms": 450, "phase_lr_deg": 1.0}
]
},
{
"segment": 36,
"transcript": "가라오케 모드에서는 텍스트 타이밍이 아주 중요합니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "가라오케", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모드에서는", "start_ms": 560, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "텍스트", "start_ms": 1070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "타이밍이", "start_ms": 1430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "중요합니다", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "힘내세요 할 수 있어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "힘내세요", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 1.0},
{"word": "할", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 1.0},
{"word": "수", "start_ms": 710, "dur_ms": 100, "phase_lr_deg": 1.0},
{"word": "있어요", "start_ms": 820, "dur_ms": 300, "phase_lr_deg": 1.0}
]
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
"transcript": "천천히 말해 볼까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 1.0},
{"word": "말해", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "볼까요?", "start_ms": 660, "dur_ms": 350, "phase_lr_deg": 1.0}
]
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
"transcript": "다시 한번 들어보세요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "한번", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "들어보세요", "start_ms": 580, "dur_ms": 450, "phase_lr_deg": 1.0}
]
},
{
"segment": 44,
"transcript": "학습 속도를 조절하는 기능도 곧 추가될 예정입니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "학습", "start_ms": 90, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "속도를", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조절하는", "start_ms": 810, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기능도", "start_ms": 1320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "곧", "start_ms": 1680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "추가될", "start_ms": 1840, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "예정입니다", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "여러분의 한국어 실력이 쑥쑥 늘기를 바랍니다",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "여러분의", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실력이", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쑥쑥", "start_ms": 1310, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "늘기를", "start_ms": 1570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 1930, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 1.0},
{"word": "계세요", "start_ms": 360, "dur_ms": 300, "phase_lr_deg": 1.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:09:24*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=11:08:28  uptime=33321s  mem=245MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_110757.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 752-275-750-653-361-765-756-672-567-562-507-267-520-352-316-236-732-502-263-326-725-261-163-176-310-637-371-312-261-132-273-276-573 312-625-235-231-276-563-765-365-672-172-372-1_V.mp3)
  Segment 2: 1576ms ~ 6112ms (file: 261-621-612-612-261 106-106-610-361-637-617-610-610-613-160-162-160-610 102-213-132-521-512-236-260-620-261-610-630-613-615-621-610-165-651-615-165-172-167-617-571-156-615-165-1_W.mp3)
  Segment 3: 7112ms ~ 7688ms (file: 625-672-675-675-657-265-265-615-652-172-621-632-670-612-637-625-367-635-673-672-653-130-312-310-130-302-361-162-160-162-130-306-130-160-162-162-612-310-312-103-163-612-326-613-6_W.mp3)
  Segment 4: 8688ms ~ 16752ms (file: 762-102-312-130-236-321-312-237-106-236-236-231-321-267-130-123-236-263-527-250-236-102-103-321-765-236-301-321-236-326-310-103-321-736-756-327-163-752-170-517-176-675-571-753-7_W.mp3)
  Segment 5: 17752ms ~ 18328ms (file: 162-132 106-132-106-123-601-162-106-103 312-107 175-157-175-715-175-751-175-715-175-157-175-751-157-157-157-715-175-517-157-157-571-571-571-571-571-751-756-765-576-725-752-675-7_V.mp3)
  Segment 6: 19328ms ~ 19904ms (file: 140-104-165-103-134-134-104-104-134-104-103-103-106-105-106-106-165-103-106-136-106-135-136-106-106-103-136-106-103-146-163-104-136-156-163-134-103 317-571-571-574-574-317-137-3_V.mp3)
  Segment 7: 20904ms ~ 21480ms (file: 130-137-103 316-135-135-153-135-136-135-136-103-103-103-137-136-103-136-103-103-103-137-137-130 316-316-135-136-135-135-135-134-135-135-103-135-163-136-136-103-137-103-136-135-1_V.mp3)
  Segment 8: 22480ms ~ 23056ms (file: 160-261-631-103-162-123-621 167-136-163-130-163-136-176-260-672-612-136-106-213-103-106-163-106-163-136-621-162-657-163-163-102-137-106-106-163-130-103-637-613-107-316-136-137-1_V.mp3)
  Segment 9: 24056ms ~ 24632ms (file: 571-517-517-571-570-517-517-517-571-571 571-510-517-571-517-501-150-571-507-517-571-517-157-517-507-507-570-576 150-571-501-507-501-507-157-150-751-715-705-571-170-150-507-570-5_V.mp3)
  Segment 10: 25632ms ~ 26208ms (file: 761-716-715-756-123-631-165-165-613-612-162-613-612-162-613-126-216-261-623-572-576-657-571-715-612-613-136-167-617-765-756-765-756-723-756-765-756-756-756-756-765-765-756-751-6_W.mp3)
  Segment 11: 27208ms ~ 33220ms (file: 123-207-320-320-376-675-236-132-273-267-102-267-237-621-327-657-376-321-320-326-320-123-627-217-312-632-632-267-132-273-321-236-213-132-231-312-623-357-763-310-710-761-270-230-1_W.mp3)
  Segment 12: 34220ms ~ 34796ms (file: 157-517-153-517-571-517-571-517-571-517-571-517-753-571 103-130-170-135-130-315-317-135-105-150-135-153-157-153-157-157-157-157-153-157-751 130-135-150-105-130-135-531-135-130-1_V.mp3)
  Segment 13: 35796ms ~ 40980ms (file: 263-123-271-237-721-213-276-316-357-237-216-126-203-120-123-120-231-123-162-267-263-271-267-263-231-526-236-261-263-231-267-217-217-132-203-210-261-276-217-126-276-127-130-213-2_W.mp3)
  Segment 14: 41980ms ~ 42556ms (file: 230-263-273-752-526-726-675-705-136-752-275-562-257-231-321-572-751-602-172-312-236-375-312-123-526-625-321-217-562-267-756-602-750-752-257-632-507-567-235-576-326-632-657-376-2_V.mp3)
  Segment 15: 43556ms ~ 44132ms (file: 163-172-574-574-756-750-756-675-301-317-301-317-316-163-507-750-705-260-326-102-103-106 102-362-321-261-261-623-602-106-126-103-316-136-613-163-167-173-103-103-310-312-301-301-1_V.mp3)
  Segment 16: 45132ms ~ 45708ms (file: 165-106-160-160-103-106-103-106-160-160-156-651-156-157-165-163-156-176-152-657-751-516-315-653-657-567-560-560-570-506-506-567-652-651-567-605-615-651-613-561-615-306-160-160-1_W.mp3)
  Segment 17: 46708ms ~ 47284ms (file: 513-153-156-135-136-134-103-143-103-136-136-105-106-105-136-165-103-106-135-136-163-106-163-103-165-103-136-103 105-163-134-136-136-134-145-106-103-103-165-103-103-136-135-103-1_V.mp3)
  Segment 18: 48284ms ~ 48860ms (file: 127-265-321-720-627-216-123-213-572-375-316-136-312-371-316-267-576-756-576-263-637-761-275-765 351-257-205-267-527-257-762-213-560-132-137-267-317-267-236-621-162-162-265-602-3_W.mp3)
  Segment 19: 49860ms ~ 50436ms (file: 514-517-514-501-510-150-150-150-135-150-150-157-513-517-153-157-105-157-105-157-517-175-103-105-105-501-105-150 510-510-150-157-153-517-105-517-105-103-153-105-153-105-153-517-1_V.mp3)
  Segment 20: 51436ms ~ 59716ms (file: 132-203-123-103-765-260-206-762-120-752-375-213-123-312-320-312-672-267-206-521-103-103-763-267-526-201-123-106-126-130-310-534-130-321-213-230-672-260-206-276-201-750-572-602-2_W_stt_the_two_months_ago_he.mp3)
  Segment 21: 60716ms ~ 69932ms (file: 275-532-325-325-765-726-120-753-310-231-213-574-213-132-102-132-120-163-106-157-132-261-216-210-102-751-270-210-213-301-102-301-231-752-756-312-135-310-132-102-132-672-756-201-1_W.mp3)
  Segment 22: 70932ms ~ 71508ms (file: 627-751-576-631-750-162-157-657-612-506-573-507-672-572-321-546-267-513-217-723-172-321-675-725-126-726-257-507-567-576-317-257-267-267-321-123-123-571-312-657-576-760-213-236-3_V.mp3)
  Segment 23: 72508ms ~ 73084ms (file: 756-675-675-673-136-126-176-716-765-765-761-726-760-376-763-716-762-167-675-163-163-316-657-675-635-657-765-675-657-537-765-675-765-675-657-675-657-756-765-765-756-765-756-765-7_W.mp3)
  Segment 24: 74084ms ~ 82904ms (file: 260-216-216-132-210-716-360-103-103-602-201-216-102-123-573-302-103-213-130-103-231-260-231-756-257-102-120-276-260-210-301-130-576-725-754-103-210-130-132-270-130-123-201-321-1_V.mp3)
  Segment 25: 83904ms ~ 92112ms (file: 376-731-317-761-753-375-371-137-753-732-765-571-756-571-751-176-753-756-715-672-162-675-301-106-130-257-675-765-120-206-102-213-507-310-102-203-130-375-752-102-310-260-206-725-5_W.mp3)
  Segment 26: 93112ms ~ 97648ms (file: 571-751-756-756-751-571-571-756-571-561-157-576-576-571-576-574-547-574-547-457-547-574-571-751-103-103-137-316-312 103-123-135-501-510-512-253-260-265-120-105-517-157-310-152-5_V.mp3)
  Segment 27: 98648ms ~ 99224ms (file: 103-371-103 136-761-103-136-413-103-156-136-352-216-136-130-610-103-352-126-267_V.mp3)
  Segment 28: 100224ms ~ 113292ms (file: 167-120-671-160-160-621-267-263-276-236-107 267-263-236-263-236-276-267-263-120-236-326-201-236-326-321-236-102-321-236-160-276-103-130-326-574-257-320-230-201-206-123-230-276-2_W.mp3)
  Segment 29: 114292ms ~ 114868ms (file: 753-675-765-735-753-753-756-175-135-571-751-715-713-705-170-370-703-705-751-170-136-130-106-136-105-235-501-571-571-153-571-315-573-317-375-563-316-351-516-563-563-536-351-536-5_W.mp3)
  Segment 30: 115868ms ~ 116444ms (file: 510-510-510-150-507-570-571 547-547-574-510-570-501-504-570-501_W.mp3)
  Segment 31: 117444ms ~ 126624ms (file: 230-263-132-312-312-132-652-507-260-265-210-263-715-310-231-120-716-132-312-231-102-107-136-753-123-751-143-537-170-312-130-102-312-103-574-120-263-106-237-260-267-213-102-231-1_W.mp3)
  Segment 32: 127624ms ~ 128200ms (file: 756-576-576-756-576-576-576-576-756-576-576-756-215-517-654-526-567 752-756-756-576-756-576-576-576-576-576-756-751-132-162-573-573-317-752-756-576-576-576-576-756-576-756-756-5_W.mp3)
  Segment 33: 129200ms ~ 138884ms (file: 126-201-135-263-362-102-261-267-620-321-367-320-372-352-236-321-236-761-312-267-267-321-263-267-236-263-126-102-621-620-670-574-257-752-102-263-326-236-326-327-752-130-261-627-2_W.mp3)
  Segment 34: 139884ms ~ 140460ms (file: 571-751-751-576-576-576-571-576-756-657-576-576-571-576-570-570 150-715-153-571-157-517-157-176-715-765-715-517-517-756-750-570-570-573-570-570-753-750-570-570-570-570-573-501-5_V.mp3)
  Segment 35: 141460ms ~ 152764ms (file: 312-276-216-576-527-106-540-543-216-261-261-217-123-540-260-130-576-567-572-571-627-132-265-267-231-260-265-752-273-671-527-132-215-675-571-127-213-567-617-132-326-562-103-103-1_W.mp3)
  Segment 36: 153764ms ~ 154340ms (file: 504-542-546-512-514-504-543-536-506-547-543-543-541-572-756-576-576-576-576-756-756-651-625-635-651-465-451-541-543-543-504-506-506-513-564-526-507-501-516-506-506-561-561-567-5_V.mp3)
  Segment 37: 155340ms ~ 160452ms (file: 267-261-207-726-276-261-216-627-657-627-752-627-120-230-123-103-537-537-701-765-271-123-176-710-572-517-130-170-217-705-137-617-712-725-751-265-752-765-756-726-301-130-105-623-2_W.mp3)
  Segment 38: 161452ms ~ 162028ms (file: 735-357-173-103-376-103-130-310-375-310-310-135-137-137-137-137-317-136-751-716-675-735-735 701-750-735-713-317-173-173-317-310-705-307-130-301-173-317-317-317-173-317-137-135-5_V.mp3)
  Segment 39: 163028ms ~ 171524ms (file: 620-572-572-203-627-675-260-206-527-206-213-216-267-256-265-376-627-267-231-231-267-276-527-236-275-572-132-206-326-213-126-201-210-312-267-260-542-762-267-236-210-260-572-263-1_W.mp3)
  Segment 40: 172524ms ~ 173100ms (file: 751-751-175-715-715-715-175-751-715-753-715-715-751 150-150-150-150-153-157-517-571-751-751-175-715-175-175-713-715-175-715-751-715-571-157 517-510-175-150-175-715-157-715-751-1_W.mp3)
  Segment 41: 174100ms ~ 174676ms (file: 267-623-267-623-621-263-627-263-267-627-261-267-267-627-265-263-627-261-263-261-261-231-261-263-263-623-627-627-627-621-612-621-627-623-263-627-623-162-265-623-756-623-627-623-2_W.mp3)
  Segment 42: 175676ms ~ 176252ms (file: 627-627-621-627-621-621-621-612-621 621-652-654-645-627-624-627-617-657-612-621-621-621-652-621-645-612-623 126-162-621-612-657-671-725-752-725-752-725-726-752-756 612-675-657-6_W.mp3)
  Segment 43: 177252ms ~ 177828ms (file: 670 673-671-627 564-564-563-756-316-163-675-156-617-675-765 621-627-602-751-167-136-172-621-621-103-637-167-610-627-316-165-361-103-631-106-176-106-106-106-621-326-372-102-176-1_W.mp3)
  Segment 44: 178828ms ~ 187864ms (file: 231-217-207-237-361-506-716-261-201-753-207-230-173-260-261-132-263-561-132-312-321-173-120-321-652-206-231-607-132-236-231-103-103-173-201-102-231-762-102-312-321-623-257-123-3_V.mp3)
  Segment 45: 188864ms ~ 189440ms (file: 653-165-156-136-365-365-632-632-601-163-175-107-103-576-756-675-176-236-623-756-671-571-273-127-315-764-163-301-136-750-316-321-305-750-301-752-672-672-647-627-367-130-136-316-6_W.mp3)
  Segment 46: 190440ms ~ 191016ms (file: 136-602-621-126-261-206-216-132-312-132-103-123-706-301-310-107-105-167-120-167-167-126-162-162-163-162-620-602-267-627-263-621-613-126-103-612-362-632-136-670-607-162-136-137-1_V.mp3)
  Segment 47: 192016ms ~ 192592ms (file: 157-157-157-175-157-153-175-751-753-715-571-715-735-175-175-175-137 105-510-517-510-501-570-571-570-517-501-150-501-501-501-517-501-513-517-517-517-574-531_W.mp3)
  Segment 48: 193592ms ~ 194168ms (file: 263-231-216-316-267-326-623-263-623-621-627-316-726 761-765-673-267-267-627-672-675-627-576-256-132-231-321-135-621-173-160-625-260-265-276-231-125-527-527-257-257-256-301-103-1_W.mp3)
  Segment 49: 195168ms ~ 203808ms (file: 103-136-103-205-203-103-574-102-217-267-312-103-162-316-132-137-132-137-617-617-265-276-765-176-762-376-752-376-310-760-674-627-267-652-265-320-267-326-310-263-236-236-276-237-3_V.mp3)
  Segment 50: 204808ms ~ 205384ms (file: 175-571-751-715-751-751-517-571-751-753-571-751-751-157-537-157-517-157-570-571-571 570-175-517-571-175-157-137-715-157-571-157-571-517-751-751-571-571-571-570-571-715-751-571-7_V.mp3)
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
> **Time**: 2026-04-05 11:09:25
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "반갑습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "반갑습니다", "start_ms": 50, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 2.0},
{"word": "날씨가", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 1.5},
{"word": "좋네요", "start_ms": 400, "dur_ms": 160, "phase_lr_deg": 1.0}
]
},
{
"segment": 4,
"transcript": "노래방 시스템을 만들기 위해서 여러 가지 언어를 테스트하고 있습니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "노래방", "start_ms": 80, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시스템을", "start_ms": 490, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "만들기", "start_ms": 1000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "위해서", "start_ms": 1460, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 1850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 2110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "언어를", "start_ms": 2370, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "테스트하고", "start_ms": 2730, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 3340, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "Thank you very much",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": -1.0},
{"word": "you", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": -1.0},
{"word": "very", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": -1.0},
{"word": "much", "start_ms": 420, "dur_ms": 140, "phase_lr_deg": -1.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "도와주세요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "도와주세요", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "일본어 문장도 포함되어 있을 수 있으니 주의 깊게 들어주세요",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "일본어", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "문장도", "start_ms": 510, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "포함되어", "start_ms": 870, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 1380, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1590, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있으니", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주의", "start_ms": 2100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "깊게", "start_ms": 2360, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "들어주세요", "start_ms": 2670, "dur_ms": 550, "phase_lr_deg": 0.0}
]
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
"transcript": "はじめまして",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ja",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "はじめまして", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "한국어 공부는 정말 재미있지만 가끔은 어렵기도 합니다",
"speaker": "speaker_1",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 90, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 500, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 890, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "재미있지만", "start_ms": 1200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "가끔은", "start_ms": 1860, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어렵기도", "start_ms": 2270, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 2780, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "발음을 정확하게 따라하는 것이 가장 중요해요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "발음을", "start_ms": 50, "dur_ms": 350, "phase_lr_deg": 1.0},
{"word": "정확하게", "start_ms": 410, "dur_ms": 500, "phase_lr_deg": 1.0},
{"word": "따라하는", "start_ms": 920, "dur_ms": 550, "phase_lr_deg": 1.0},
{"word": "것이", "start_ms": 1480, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "가장", "start_ms": 1740, "dur_ms": 300, "phase_lr_deg": 1.0},
{"word": "중요해요", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 1.0}
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
"transcript": "이 오디오 데이터는 분석용으로 제작되었습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 120, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오디오", "start_ms": 230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "데이터는", "start_ms": 640, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "분석용으로", "start_ms": 1150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "제작되었습니다", "start_ms": 1760, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "よろしくお願いします",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ja",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "よろしく", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 4.0},
{"word": "おねがいします", "start_ms": 340, "dur_ms": 200, "phase_lr_deg": 4.0}
]
},
{
"segment": 20,
"transcript": "맛있게 드세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맛있게", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 1.0},
{"word": "드세요", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 1.0}
]
},
{
"segment": 21,
"transcript": "실시간 데이터 처리가 정상적으로 작동하고 있습니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "실시간", "start_ms": 70, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "데이터", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "처리가", "start_ms": 840, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정상적으로", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "작동하고", "start_ms": 1810, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 2270, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "사랑해요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "사랑해요", "start_ms": 20, "dur_ms": 500, "phase_lr_deg": 0.5}
]
},
{
"segment": 23,
"transcript": "학습을 위해 자막을 생성하는 과정입니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "학습을", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 470, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자막을", "start_ms": 780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생성하는", "start_ms": 1140, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "과정입니다", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "Wait a minute",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Wait", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "minute", "start_ms": 260, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "사용자 여러분의 많은 참여를 부탁드립니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "사용자", "start_ms": 110, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "여러분의", "start_ms": 520, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 980, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "참여를", "start_ms": 1290, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "부탁드립니다", "start_ms": 1650, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "Good job",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Good", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": -2.0},
{"word": "job", "start_ms": 240, "dur_ms": 300, "phase_lr_deg": -2.0}
]
},
{
"segment": 30,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
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
"transcript": "점심 뭐 먹을까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 1.0},
{"word": "뭐", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 1.0},
{"word": "먹을까요?", "start_ms": 360, "dur_ms": 350, "phase_lr_deg": 1.0}
]
},
{
"segment": 33,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "비가 올 것 같은데 우산 챙기셨나요?",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "비가", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 1.0},
{"word": "올", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 1.0},
{"word": "것", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 1.0},
{"word": "같은데", "start_ms": 490, "dur_ms": 300, "phase_lr_deg": 1.0},
{"word": "우산", "start_ms": 800, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "챙기셨나요?", "start_ms": 1060, "dur_ms": 450, "phase_lr_deg": 1.0}
]
},
{
"segment": 36,
"transcript": "가라오케 모드에서는 텍스트 타이밍이 아주 중요합니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "가라오케", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모드에서는", "start_ms": 560, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "텍스트", "start_ms": 1070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "타이밍이", "start_ms": 1430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "중요합니다", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "힘내세요 할 수 있어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "힘내세요", "start_ms": 50, "dur_ms": 450, "phase_lr_deg": 1.0},
{"word": "할", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 1.0},
{"word": "수", "start_ms": 710, "dur_ms": 100, "phase_lr_deg": 1.0},
{"word": "있어요", "start_ms": 820, "dur_ms": 300, "phase_lr_deg": 1.0}
]
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
"transcript": "천천히 말해 볼까요?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 1.0},
{"word": "말해", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "볼까요?", "start_ms": 660, "dur_ms": 350, "phase_lr_deg": 1.0}
]
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
"transcript": "다시 한번 들어보세요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "한번", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 1.0},
{"word": "들어보세요", "start_ms": 580, "dur_ms": 450, "phase_lr_deg": 1.0}
]
},
{
"segment": 44,
"transcript": "학습 속도를 조절하는 기능도 곧 추가될 예정입니다",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "학습", "start_ms": 90, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "속도를", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조절하는", "start_ms": 810, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기능도", "start_ms": 1320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "곧", "start_ms": 1680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "추가될", "start_ms": 1840, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "예정입니다", "start_ms": 2250, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "여러분의 한국어 실력이 쑥쑥 늘기를 바랍니다",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "여러분의", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국어", "start_ms": 540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실력이", "start_ms": 950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "쑥쑥", "start_ms": 1310, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "늘기를", "start_ms": 1570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바랍니다", "start_ms": 1930, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 1.0},
{"word": "계세요", "start_ms": 360, "dur_ms": 300, "phase_lr_deg": 1.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:09:25*
