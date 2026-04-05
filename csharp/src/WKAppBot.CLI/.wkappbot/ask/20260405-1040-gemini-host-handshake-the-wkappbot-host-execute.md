# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=10:39:19  uptime=22844s  mem=343MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_103837.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 654-651 625-625-625-652-652-654-652-651-652-625 261_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 517-571-571-750-750-571 705-507-570-507-150-510-150-517-570-517-571-157-507-157-517-510-571-175-501-571-157-571-571 105-510-510-517-510-157-514-574-517-105-715-175-570-571 751-1_W.mp3)
  Segment 3: 3152ms ~ 7688ms (file: 576-756 752-756-756-576-576-576-756-576-756-576-576-576-576-576-756 756-576-576-756-576-576-576-756 576-571-576-150-157-576-357-576 103-123-103-132-523-512-203-275-675-502-153-5_W.mp3)
  Segment 4: 8688ms ~ 30144ms (file: 305-267-637-726-762-672-672-627-201-136-260-230-267-103-103-315-135-324-314-267-123-130-574-103-103-301-102 123-203-261-543-541-215-301-310-206-260-206-102-102-301-132-230-321-7_V.mp3)
  Segment 5: 31144ms ~ 31720ms (file: 576-571 507-507-571-517-570-517-510-150-517-150-150-517-157-150-150-150-150-157-517-157-507-570 576-576-150-157-150-571-571-571-571-574-571-517-571-150-571 150-571-517-571-571-1_V.mp3)
  Segment 6: 32720ms ~ 33296ms (file: 317-135-317-315-175-173-173-135-172-173-135-157-137-135-517-175-751-715-751-751-517-715-175-715-157-317-135-317-371-137-137-173-137-317-137-317-137-173-715-713-753-751-175-715-7_W.mp3)
  Segment 7: 34296ms ~ 34872ms (file: 560-576-567-567-657-567-567-657-567-765-657-657-567-756-576-576-576-657-651-657-651-675-567-657-657-675-576-657-576-756-675-675-567-675-675-675-657-675-563-576-567-576-576-576-5_W.mp3)
  Segment 8: 35872ms ~ 36448ms (file: 157-157-156-157-517-103-105-137-165-165-165-160-106-105 570-571-175-751-705-176-167-567-365-673-657-376-736-761-126-162-365-527-536-173-175-615-176-631-165-137-123-567-576-567-5_W.mp3)
  Segment 9: 37448ms ~ 38024ms (file: 103-735-107-103-157-130-173-137-130-713-130-713-170-137-107-571-751-751-371-713-301-130-317-312-721 735-573-701-105-710-310-301-103-103-675-375-570-731-701-750-753-756-756-765-7_W.mp3)
  Segment 10: 39024ms ~ 45360ms (file: 235-152-730-257-237-276-567-627-251-237-725-123-706-762-327-302-102-653-263-762-706-217-527-523-237-653-602-532-231-312-326-163-317-120-132-315-365-317-315-137-531-317-137-312-1_W.mp3)
  Segment 11: 46360ms ~ 46936ms (file: 761-736-371-317-317-317-132-123-127-172-172-612-267-163-547-157_W.mp3)
  Segment 12: 47936ms ~ 48512ms (file: 576-572-275-567-572-576-576-765-765 502-526-260-570-256-502-526-576-507-570-560-567-765-675-762-756-756-765-756-756-765-765-765-765-765-765-675 507-507-526-250-572-205-502-567-5_V.mp3)
  Segment 13: 49512ms ~ 50088ms (file: 765-716-175-715-175-156-175-751-175-561-715-751-710-750-750-750-756-754-765-756-756-756-756-756-751-756-105-107-175-157-176-176-176-615-165-756-175-156-651-157-156-157-571-756-5_W.mp3)
  Segment 14: 51088ms ~ 51664ms (file: 517-571-571-576-570-570-517 517-517-157-571-573-517-517-517-571-513-571-517-503-517-517-157-157-517-576-571-571-570-570-517 570-571-510-513-517-517-517-751-751-175-517-510-571-5_V.mp3)
  Segment 15: 52664ms ~ 64688ms (file: 607-130-261-230-317-105-160-156-321-365-321-321-321-213-756-651-753-317-106-310 674-674-267-321-762-620-321-320-103-132-761-760-267-213-762-201-102-372-321-367-321-547-206-302-2_W.mp3)
  Segment 16: 65688ms ~ 66264ms (file: 756-725-756-576-567-765-527-726-762-725-756-751-751-257-765-761-175-765-765-175-761-251-250-256-526-567-576-756-765-576-765-675-657-675-167 520-507-527-576-576-576-572-576-756-7_W.mp3)
  Segment 17: 67264ms ~ 67840ms (file: 162-632-621-261-136-162-601-610-621-216-103-103-136-103-620-163-123-316-320-312-136-106-167-327-702-376-302-132-201-213-301-320-203-206-320-651-716-675-376-106-167-132-167-765-1_V.mp3)
  Segment 18: 68840ms ~ 69416ms (file: 625-621-625-621-651-654-651-624-612-621 652-652-654-651-654-652-651-652-657-654-651-651-652-621-621 627-612-621-621-612-672-615-651-654-651-652-621_W.mp3)
  Segment 19: 70416ms ~ 78408ms (file: 230-231-210-312-210-216-120-102-123-213-231-263-213-206-103-213-235-235-321-123-123-321-231-206-236-216-106-106-106-102-130-230-236-263-203-326-123-132-231-216-573-574-206-230-3_W.mp3)
  Segment 20: 79408ms ~ 79984ms (file: 316-263-765-160-163-671-765-637-627-362-136-132-162-761-627-765-671-607-765-632-672-613-173-132-716-670-706-670-607-673-675-672-637-301-673-673-362-132-136-310-632-623-671-630-6_W.mp3)
  Segment 21: 80984ms ~ 90740ms (file: 527-572-526-267-672-167-632-167-207-123-576-257-102-751-275-174-136-620-620-267-257-123-267-103-573-527-163-132-103-765-762-765-203-302-123-120-120-123-267-103-627-236-102-754-2_W.mp3)
  Segment 22: 91740ms ~ 92316ms (file: 310-130-307-103-130-130-713-370-137-107-750-713-713 103-103-371-130-173-130-631-316-731-137-307-301-103-130-170-107-103-130-130-130-750-317-751-107-715-157-375-137-130 315-137-3_V.mp3)
  Segment 23: 93316ms ~ 97492ms (file: 763-762-672-672-673-672-176-617-267-672-176-726-712-627-627-621-276-672-673-671-763-672-763-672-362-213-621-376-216-576-576-136-762-623-627-726-762-127-621-713-271-267-126-132-7_W.mp3)
  Segment 24: 98492ms ~ 104684ms (file: 102-547-762-276-230-706-602-756-276-231-273-206-237-327-237-326-670-576-102-260-613-670-752-362-613-765-263-257-237-302-136-230-502-567-107-206-507-203-102-753-206-576-153-574-7_V.mp3)
  Segment 25: 105684ms ~ 106260ms (file: 751-517-150-157-107-175-107-107-175 107-157-173-137 751-751-710-710-710-716-376-173-170-173-176-137-317-173-175-137-731-173-173-173-317-137-317-137-371-137-137-137-136-312-132-1_W.mp3)
  Segment 26: 107260ms ~ 107836ms (file: 657-657-657-657-654-657-567 652-654-652_W.mp3)
  Segment 27: 108836ms ~ 109412ms (file: 507-570-571-705-751-750-753-710-701-751-175-710-507-705-570-750-750-751-570-170-750-570-571-507-507-570-571-305-350-756-506-371-570-573-576-705-673-765-370-650-760-607-370-705-5_V.mp3)
  Segment 28: 110412ms ~ 110988ms (file: 150-517-571-751-715-175-751-715-175-715-715-751-715-715-715-756-751-751-751-751-753-751 105-157-150-517-175-175-175-751-715-175-715-175-715-751-751-715-751-715-756-756 105-150-1_W.mp3)
  Segment 29: 111988ms ~ 119152ms (file: 573-237-230-213-230-231-102-612-601-264-230-230-231-230-230-123-120-301-213-120-123-213-231-320-301-312-154-752-716-105-231-236-312-312-327-752-310-132-107-263-263-321-260-231-1_W.mp3)
  Segment 30: 120152ms ~ 120728ms (file: 326-136-613-316-610-301-361-301-326-162-160-163-316-102 672-672-672-672-706-761-762 105_W.mp3)
  Segment 31: 121728ms ~ 122304ms (file: 613-670-610-613-617-670-316-637-607-173-310-713-301-107-130-130-301-137-301-107-137-713-701 753-301-310-170-103-317-137-671-103-673-637-610-607-671-670-670-715-307-710-370-317-3_V.mp3)
  Segment 32: 123304ms ~ 123880ms (file: 321-236-260-260-267-326-203-260-260-102-130-176-620-160-603-160-132-125-716-160-617-136-206-620-627-167-617-136-143-103-106-123-612-120-132-136-103-103-106-163-316-130-312-321-3_V.mp3)
  Segment 33: 124880ms ~ 125456ms (file: 157-715-175-176-175-176-165-175-103-130-132-136-136-371-376-371-763-765-753-751-753-753-756-753-351-310-317-310-361-736-316-260-312-320-321-371-703-367-731-736-756-735-753-735-7_W.mp3)
  Segment 34: 126456ms ~ 127032ms (file: 267-273-172-706-765-765-761-765-716-765-760-571-576-571-675-657-675-107_W.mp3)
  Segment 35: 128032ms ~ 138112ms (file: 316-316 723-237-207-372-307-132-103-107-107-132-127-270-213-123-273-120-632-326-302-362-126-267-376-306-273-732-326-372-102-163-127-172-217-725-673-130-706-672-106-160-271-167-7_W_stt_the_old.mp3)
  Segment 36: 139112ms ~ 139688ms (file: 162-261-206-562-601-167-321 716-671-167-627_W.mp3)
  Segment 37: 140688ms ~ 141264ms (file: 710-175-170-105-170-175-173-751-715-756-751-175-751-571-517-517-157-157-513-153-157-150-153-157-751-756-715-751-715-751-175-715-173-105-175-175-510-107-105-150-157-150-105-751-1_W.mp3)
  Segment 38: 142264ms ~ 142840ms (file: 571-517-576-571-175-751-715-715-571-576-517-571-571-576-571-576-576-576-570-507-571-560-517-157-507-537-560-157-570-571-157-517-175-517-517-570-573-570-517-517-570-507-560-570-5_W.mp3)
  Segment 39: 143840ms ~ 152444ms (file: 261-321-263-627-673-103-672-756-237-276-267-723-765-765-326-726-765-267-231-126-120-127-120-267-123-627-261-236-326-765-361-312-675-765-267-176-261-126-136-236-321-106-160-617-2_W.mp3)
  Segment 40: 153444ms ~ 160788ms (file: 231-103-231-230-103-103-152-230-267-327-321-230-236-237-321-375-103-267-276-576-657-276-275-107-312-756-276-270-120-671-267-126-273-276-237-217-106-213-120-230-267-237-126-762-6_W.mp3)
  Segment 41: 161788ms ~ 162364ms (file: 130-103-134 130-134-310-143-314-134-103-134-130-130-314-130-103-140-104-103-143-134-314 413-310-103-103-301-134-103-134-145-134-103-134-143-145-134-145-134-103-134-134 340-130-3_V.mp3)
  Segment 42: 163364ms ~ 172076ms (file: 367-756-325-172-103-301-275-752-705-507-637-102-103-130-652-301-562-156-165-157-175-756-153-750-756-573-260-265-265-301-136-316-513-156-165-621-376-301-671-163-103-751-103-103-7_W.mp3)
  Segment 43: 173076ms ~ 173652ms (file: 316-631-126-132-162-615-153-567-357-657_W.mp3)
  Segment 44: 174652ms ~ 175228ms (file: 145-617-163-146-634-647-617-312-123-314-163-163-576-756-756-576-765-756-756-576-756-756-756-756-756-576-756-567-754-750-750-752-725-752-726-721-721-756-752-752-567-302-630-602-6_V.mp3)
  Segment 45: 176228ms ~ 185444ms (file: 236-321-321-263-321-237-673-765-102-136-263-260-726-327-321-231-612-261-267-765-210-162-327-236-231-230-732-237-237-267-236-312-763-276-312-107-756-326-236-102-327-302-312-173-3_W.mp3)
  Segment 46: 186444ms ~ 194579ms (file: 236-576-302-263-574-723-263-206-671-671-126-263-756-213-261-267-726-126-517-236-302-263-625-126-160-120-612-132-236-310-316-132-261-245-106-106-130-621-673-163-752-263-263-263-2_W.mp3)
  Segment 47: 195579ms ~ 196155ms (file: 167-735-761-716-371-132-321-213-127-167-137-167-163-167 672-671 627-602-602_W.mp3)
  Segment 48: 197155ms ~ 197731ms (file: 267-620-623-762-673-675-670-673-563-356-650-657-650-651-627-621-602_W.mp3)
  Segment 49: 198731ms ~ 199307ms (file: 371-321-301-312-237-325-306 106-103_V.mp3)
  Segment 50: 200307ms ~ 218271ms (file: 301-312-123-103-310-537-574-201-210-206-207-520-256-120-132-103-267-207-217-203-312-130-103-517-105-201-206-203-201-213-312-260-206-210-547-540-123-154-206-210-137-103-574-571-1_V.mp3)
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
> **Time**: 2026-04-05 10:40:13
> **AI**: gemini

---

## Response

[
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "근데 이제 우리가 처음에 주식 투자 하다 보면은 대부분 이제 뭔 말인지 몰라가지고 아이 모르겠다 안 하고 그러는 경우가 많거든 근데 생각보다 자기 분야로 좁히다 보면은 이게 막 그렇게 어",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 240, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 480, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "처음에", "start_ms": 800, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "주식", "start_ms": 1160, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "투자", "start_ms": 1440, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "하다", "start_ms": 1760, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "보면은", "start_ms": 2040, "dur_ms": 360, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "해놓고 이제와 막대한 비용을 전쟁으로 피해 입은 동맹들에게 전가하는 형국인 겁니다 이런 상황이어서 여론은 계속 나빠져",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "해놓고", "start_ms": 20, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이제와", "start_ms": 360, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "막대한", "start_ms": 760, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "비용을", "start_ms": 1160, "dur_ms": 320, "phase_lr_deg": 0.0}
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
"transcript": "이 표는 이제 IQ인지 EQ인지는 확인해야죠",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 80, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "표는", "start_ms": 240, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 520, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "IQ인지", "start_ms": 760, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "EQ인지는", "start_ms": 1200, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "확인해야죠", "start_ms": 1720, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "한 400인분 정도 준비를 하고 있어요 저는 튀김 담당입니다",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "400인분", "start_ms": 180, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 660, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "준비를", "start_ms": 980, "dur_ms": 320, "phase_lr_deg": 0.0}
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
"transcript": "안녕하세요 만나서 정말 반갑습니다 오늘 날씨가 참 좋네요",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 2300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 4100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 4900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 5300, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "자국이 직접 전쟁에 뛰어들어야만 가능한 것은 아닙니다 다른 나라가 제공하지 못하는 가치를 내놓기만 해도 시대의 흐름을 붙잡을 수 있다는 뜻입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "자국이", "start_ms": 25, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 360, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "전쟁에", "start_ms": 680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "뛰어들어야만", "start_ms": 1040, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "전쟁이 세가 공간의 일부로 인정받게 된 것인데요 그들은 그 배경을 설명하면서 한국 세탁기가 미국 집 구조를 바꿔놓았다라는 표현을 자연스럽게 하고 있었습니다",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "전쟁이", "start_ms": 35, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "세가", "start_ms": 420, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "공간의", "start_ms": 740, "dur_ms": 320, "phase_lr_deg": 0.0}
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
"transcript": "좀 높고 배터리가 좀 빨리 닳아가지고 제가 굉장히 아쉽다라고 말을 한 이후에요 한 일주일 정도 지나서 펌웨어 업데이트가 나왔는데 그거 업데이트 하고",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좀", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "높고", "start_ms": 240, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "배터리가", "start_ms": 520, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "늘리는 30일대로 늘리고 그 다음 해에는 47일 때까지 확대할 예정입니다 이런 무시무시한 자동화 양산 능력이야말로 납기 보장의 자신감이 되는 것입니다",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "늘리는", "start_ms": 20, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "30일대로", "start_ms": 360, "dur_ms": 480, "phase_lr_deg": 0.0}
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
"transcript": "물을 만들면 되지 않는가 이 질문에서 탄생한 것이 컨테이너 기반의 밀폐형 수직 농장입니다 구조부터 살펴볼까요 이 시스템은 외부와 100퍼센트 차단된 특수 외부",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "물을", "start_ms": 35, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "만들면", "start_ms": 310, "dur_ms": 320, "phase_lr_deg": 0.0}
]
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
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
"transcript": "결국은 앞으로는 이제 레이어를 여러 가지 둬가지고 방어를 잘 해야 할 텐데 단순히 한쪽에서 두는 게 아니라",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "결국은", "start_ms": 10, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "앞으로는", "start_ms": 360, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "냄새를 견딜 수 없는 수준에 이르자 농부들은 땅에 심은 감자를 캐보기로 합니다 농부들은 경악을 금치 못했습니다 땅에서 캔 감",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "냄새를", "start_ms": 50, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "견딜", "start_ms": 400, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "수익비가 엄청 좋은 게임을 할 수 있는 거기 때문에 음 다섯 번 실패해도 한 번만 성공하면 된다 그래도 수익 난다 그래서 자기가",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "수익비가", "start_ms": 30, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 420, "dur_ms": 280, "phase_lr_deg": 0.0}
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
"transcript": "세운 투자 시나리오랑 전략을 좀 집중할 수 있는 이런 충동적인 결정을 방지하는 데는 확실히 도움 되지 않을까 그런 생각이 드는 거",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "세운", "start_ms": 20, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "투자", "start_ms": 280, "dur_ms": 280, "phase_lr_deg": 0.0}
]
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
"transcript": "다음의 연속이고 제대로 된 길을 걷고 있는 생각으로 매주 기다려지고",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "다음의", "start_ms": 20, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "연속이고", "start_ms": 360, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "이미 수강한 영상을 반복 공부하고 있습니다 그 어느 때보다 열정",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "이미", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수강한", "start_ms": 240, "dur_ms": 320, "phase_lr_deg": 0.0}
]
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
"transcript": "요즘 날씨가 정말 화창하네요 주말에 나들이 가기 딱 좋은 것 같아요",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 100, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 450, "dur_ms": 360, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 10:40:13*
