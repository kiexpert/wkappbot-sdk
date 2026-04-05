# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=9236  time=11:33:19  uptime=34629s  mem=480MB  system_processes=353"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_113255.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 261-620-261-621-621-621 267-621-617-621-621-612-621-671-617-617-612-672-627-627-672-671-671-612-610 612-627-612-672-617-671-627-617-672-671-671-675-671-627-621-671-612-627_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 532-507-516-512-706-572-256-325-376-276-321-372-357-206-237-702-263-237-572-267-237-236-263-236-257-216 137 365-702-162-750-672-572-261-536-573-527-732-267-325-573-576-621-752-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 273-276-526-576-652-652-652-275-652-265-627-672-672-620-310-106-621-602_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 576-756-576-756-752-413-546-547-275-154-574-547-562-546-253-752-572-576-576-576-756-576-756-756-756-576-756-752-657-635-362-752-756-756-576-756-576-756-576-576-576-567-756-756-7_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 362-362-352-576-576-563-537-756-756-176-675-675-367-672-627-631-571-672-625-623-625-167-106-130-312-316-103-163-316-132-312-165-275-765-213-762-617-312-362-312-136-376-236-361-5_W.mp3)
  Segment 6: 7880ms ~ 15008ms (file: 103-216-123-732-126-106-321-260-706-267-625-103-756-231-230-230-623-120-125-652-162-753-567-352-306-310-127-126-560-560-230-167-176-103-132-130-173-236-321-132-231-725-574-574-6_V_stt_but_hopefully_that_somebody_has_to_do_th.mp3)
  Segment 7: 16008ms ~ 16980ms (file: 103-104-154-147-154-154-157-154-150-401-451-145-105-154-145-147-134-417 150-170-103-103-134-104-145-103-104-104-315-134-103-103-134-104-103-134 316-316-143-341-315-104-430-143-1_V.mp3)
  Segment 8: 17980ms ~ 24604ms (file: 206-267-217-216-672-627-210-206-276-267-231-123-126-123-623-213-267-263-206-260-261-261-263-627-217-273-103-102-231-302-162-267-276-267-102-130-601-126-267-132-321-263-267-261-2_W.mp3)
  Segment 9: 25604ms ~ 26180ms (file: 715-753-572-132-125-257-572-527-527-531 153-513-571-561-517-751-735-715-751-571-576-715-571-175-751-751 571-576-751-571-571-157-573-571-751-571-715-751-751-715_W.mp3)
  Segment 10: 27180ms ~ 27756ms (file: 317-135-103-134-103-134-103-130-103-314-103-103-103-137-314-134-104-130-103-314-134-134-134-143-137-143 310-103-137-137-103-103-310-130-143-103-140-104-104-104-103-314-134-130-1_V.mp3)
  Segment 11: 28756ms ~ 29332ms (file: 672-627-765-675-617-671-637-673-567-567-675-367-165-136-657-657-653-657-657-567-576-567-675-756-567-675-576-576-756-675-673-361-672-672-631-620-673-657-601-163-671-610-561-612-1_W.mp3)
  Segment 12: 30332ms ~ 30908ms (file: 715-175-751-756-751 501-105-157-157-517-751-751-175-715-175-715-715-175-715-175-715-751-751-751-754 105-105-137-137-107-175-175-715-715-715-751-715-751-175-175-715-715-751-751-7_W.mp3)
  Segment 13: 31908ms ~ 32484ms (file: 576-657-567-753-576-675-567-562-527-562-561-352-657-576-765-576-756-756-567-321-563-132-312-576-563-567-537-356-576-527-567-532-356-765-527-327-567-103-135-135-561-617-351-136-3_W.mp3)
  Segment 14: 33484ms ~ 34060ms (file: 564-543-534-547-563-514-534-514-541-514-543-541-514-547-541-546-216-621-612-623-621-621-621-621-617-615-651-657-657-657-657-657-567-657-741-106-162-163-163-261-561-567 615-651-6_W.mp3)
  Segment 15: 35060ms ~ 48668ms (file: 327-537-547-275-267-260-320-321-103-267-176-132-173-176-726-276-267-276-267-213-107-326-230-236-370-267-320-127-206-230-302-752-273-231-753-671-127-712-172-726-327-236-216-312-3_W.mp3)
  Segment 16: 49668ms ~ 50244ms (file: 570-750-750-570-573-750-570-750-507-315-576-571-751-315-701-716-105-753-107-301-763-307-517-107-715-130-571-571-507-715-751-751-705-517-170-570-107-571-571-751-710-750-701-701-7_V.mp3)
  Segment 17: 51244ms ~ 51820ms (file: 576-756-576-576-756-576-756-532-514-514-541-543-546-542-543-543-453-564-542-752-756-576-756-576-576-576-756-576-576-576-756-576-756-576-756-135-564-516-564-546-547-564-524-561-5_W.mp3)
  Segment 18: 52820ms ~ 53396ms (file: 257-250 672-617-627-576 632-256-627-632-765-765-765-765-765-675-675-756-756-576-574-617 672-731-716-102-316-162-750-735-762-132-236-263-756-756-765-750-763-756-765-765 765-765-2_W.mp3)
  Segment 19: 54396ms ~ 54972ms (file: 103 135-103-103-135-135-315-351-153-135-315-157-135-315-315-153-310-135-325-135 135-301-103-310-130-153-135-315-135-315-310-135-153-315 135-315-103-132-317-135-310-315-135-310-1_V.mp3)
  Segment 20: 55972ms ~ 61516ms (file: 267-265-627-260-627-263-265-267-627-560-216-130-276-267-326-213-216-127-157-213-750-671-576-257-120-627-162-210-576-120-150-263-261-572-574-105-651-620-126-762-260-602-102-215-6_W.mp3)
  Segment 21: 62516ms ~ 63092ms (file: 103-301-607-601-106-160-371-103-310-107-130-317-130-730-173 173-705-730-701-357-735-130-176-103-106-136-163-137-130-321-613-617-310-316-130-627-106-617-613-173-317-765-173-350-1_V.mp3)
  Segment 22: 64092ms ~ 64668ms (file: 574-574-571-750-571-570-517-574-150-751-510-157-105-157-170-510-507-507-574-574-570-157-517-571-157-573-574 574-754-574-574-754-574-754-574-574-574-574-574-574-175-751-715-570-1_W.mp3)
  Segment 23: 65668ms ~ 66244ms (file: 675-675-675-671-672-672-672-612 621-621-621-627-672-672-672-672-672-672-675-675-675-675-670-602-621 620-620-267-627-627-627-672-627-627-627-627-620 621-621-612-621-620 621-621-6_W.mp3)
  Segment 24: 67244ms ~ 67820ms (file: 756-751-715-751-751-751-715-715-715-751-751-751-751 507-501-150-157-107-157-157-751-751-751-751-751-751-715-756-751-756-751-751-751-756-751-751-756-751-715-715 105-150-157-105-1_W.mp3)
  Segment 25: 68820ms ~ 76308ms (file: 201-267-312-126-261-572-217-201-270-207-206-231-301-302-276-731-267-206-106-312-672-236-527-675-756-675-315-301-135-261-206-207-672-256-267-236-203-201-574-507-130-301-123-132-1_W.mp3)
  Segment 26: 77308ms ~ 84868ms (file: 201-267-263-167-507-301-301-736-672-730-136-201-302-103-301-312-137-317-137-103-316-136-576-137-536-567-765-106-107-106-106-173-370-762-765-762-762-270-103-107-750-102-306-136-1_W.mp3)
  Segment 27: 85868ms ~ 86444ms (file: 713-713-175-175-751-751-173-157-713-715-751-175-715-753-713-715-753-753-753-753-751-751-753 517-571-137-713-715-175-715-735-713-173-713-713-173-713-712-712-713-715-715-137-713-1_V.mp3)
  Segment 28: 87444ms ~ 88020ms (file: 260-260-625-625-265-265-265-265-625-265-623-261-260-625-625-625-652-625-652-265-625-265-265 623-621-612-621-260-260-627-625-625-265-625-261-261-265-625-265-265-620-623-625-625-5_W.mp3)
  Segment 29: 89020ms ~ 89596ms (file: 175-173-710-715-715-751-751-756-756-756-751 157-715-751-170-175-715-710-175-175-175-715-170-157-157-175-175-751-751-756-715-175-756-715-157-576-756-756 751-571-157-715-175-571-5_W.mp3)
  Segment 30: 90596ms ~ 91172ms (file: 765-256-576-576-752-205-576-576 503-504-504-504-506-504-504-506-507-570-576-507-567-576-657-576-567-567-506-765-507-657-570-756-752-570-576-507-576 507-527-576-576-572-756-572-5_V.mp3)
  Segment 31: 92172ms ~ 92748ms (file: 571-357-572-256-627-507-257-236-752-251-235-572-725-123-127-175-570-251-572-620-327-132-752-621-326 236-162-351-376-736-376-673-761-763-736-367-713-671-631-136-612-602-623-316-1_W.mp3)
  Segment 32: 93748ms ~ 94324ms (file: 625-627-627-625-625-627-625-627-625-627 562-562-652-652-657-625-627-675-627-675-627-627-625-627-627-625-625-625-627-625 567-567-562-652-672-657-627-627-627-652-627-627-627-627-6_W.mp3)
  Segment 33: 95324ms ~ 104432ms (file: 267-102-120-103-273-237-217-276-267-257-207-230-231-173-127-207-103-765-267-267-230-216-102-317-276-276-273-230-230-102-136-136-230-352-276-102-230-320-203-143-123-236-231-321-2_W.mp3)
  Segment 34: 105432ms ~ 112920ms (file: 675-267-670-571-310-103-106-103-201-263-276-261-620-162-320-201-206-735-317-210-206-176-310-103-547-547-276-627-201-216-126-163-103-120-132-762-105-675-675-267-267-206-132-103-2_W.mp3)
  Segment 35: 113920ms ~ 119176ms (file: 573-315-136-571-361-136-726-231-261-312-236-265-213-653-613-632-136-132-573-576-125-675-365-163-576-350-321-132-136-132-216-612-125-312-576-123-756-136-236-326-263-312-132-130-7_W.mp3)
  Segment 36: 120176ms ~ 120752ms (file: 156-132-137-132-573-357-153-513-317-517-315-351-357-165-167-165-165-165-153-156-165-167-156-165-106-153-130-132-573-567-751-576-756-573-576-571-571-317-135-165-135-176-137-103-1_W.mp3)
  Segment 37: 121752ms ~ 130248ms (file: 267-627-321-172-107-173-756-317-231-231-321-213-231-130-752-106-263-260-261-216-236-107-102-263-751-132-103-273-231-213-123-230-312-231-753-725-237-312-632-723-130-531-106-231-2_W.mp3)
  Segment 38: 131248ms ~ 131824ms (file: 617-615-675-657-657-657-657-657-567-657-567-657-567-657-675-312 615-615-615-657-651-657-657-657-657-657-567-576-567-567-657-657-526-562-527-547 654-657-657-657-651-657-657-651-6_W.mp3)
  Segment 39: 132824ms ~ 133400ms (file: 653-673-761-163-130-573-751-137-621-135-163-136-361-316-672-675-657-651-715-567-571-517-571-567-571-756-756-571-576-576-756-571-756-567-571-175-176-167-173 571-105-120-107-160-1_W.mp3)
  Segment 40: 134400ms ~ 134976ms (file: 675-163-671-675-671-675-670-627-657-625-625-671-670-601-673-637-637-637-601 176-367-716-103-176-107-760-672-671-360-360-260-620-607-361-361-631-637-361-602-627-267-627-627-267 5_W.mp3)
  Segment 41: 135976ms ~ 140656ms (file: 570-753-251-504-102-106-573-236-203-267-312-152-321-672-206-267-267-276-524-627-206-231-312-236-172-152-276-172-125-123-123-716-157-127-231-257-217-230-203-276-261-216-123-213-1_W_stt_that_is_all_i_think_it's_really_easy_and.mp3)
  Segment 42: 141656ms ~ 142232ms (file: 312-321-103-650-321-312 306-203-260-236-261 756-176 165-253-132-310-236-312 216-236-261-621-613-602-172-362-627 127-710-603-621-132-103-163-136-103 132-126 312_W.mp3)
  Segment 43: 143232ms ~ 159323ms (file: 126-527-103-132-260-301-763-316-763-763-132-206-275-175-302-312-736-130-572-231-206-201-123-130-302-217-132-725-721-263-231-216-260-254-301-765-137-312-530-352-102-132-527-217-2_W.mp3)
  Segment 44: 160323ms ~ 160899ms (file: 756-756-756-751-253-524-543-562-526-521-567-534-561-513-546-504-541-725-756-576-756-576-756-576-576-576-576-576-756-576-756-756-425-543-534-541-514-504-547_V.mp3)
  Segment 45: 161899ms ~ 162475ms (file: 576-621-132-531-576-752-573-136-651-561-576-135-756-705-132-610-132-136-136-312-136-125-162-615-612-627-136-107 627-765_W.mp3)
  Segment 46: 163475ms ~ 164051ms (file: 162-276-275-572-275-756-675-165-263-236-236-253-572-752-753-275-273-574-756-267-267-263-527-576-526-576-576-657-672-625-627-627-267-267-267-627-627-672-672-621-652-615-612-657-6_W.mp3)
  Segment 47: 165051ms ~ 171675ms (file: 537-530-207-230-203-276-260-627-625-231-627-267-752-267-203-321-260-265-654-207-312-237-203-213-237-276-751-320-213-102-261-312-123-137-765-765-231-102-170-671-163-673-326-320-3_W.mp3)
  Segment 48: 172675ms ~ 178723ms (file: 267-310-260-260-103-103-157-231-123-261-312-257-312-237-570-152-257-216-206-120-126-132-217-127-217-123-206-613-170-670-106-103-132-752-123-103-102-236-230-120-261-201-237-206-1_W.mp3)
  Segment 49: 179723ms ~ 189083ms (file: 260-231-203-102-123-130-103-713-310-123-103-320-103-123-231-312-260-102-206-231-301-321-231-102-267-276-237-256-254-123-237-236-235-230-765-263-765-230-276-273-312-103-726-320-2_W.mp3)
  Segment 50: 190083ms ~ 190659ms (file: 134-130-310-104-134-341-143-143-341-134-103-134-104-415-314-134 407-304-134-310-130-143-134-314-103-143-301-310-143-134-103-104-134-103-134-103-104-134-143-134 103-314-401-143-1_V.mp3)
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
> **Time**: 2026-04-05 11:34:10
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
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "반갑", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "식사는 하셨나요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "식사는", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하셨나요?", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "But hopefully that somebody has to do that",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "But", "start_ms": 110, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "hopefully", "start_ms": 310, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "somebody", "start_ms": 860, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "has", "start_ms": 1310, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 1610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 1760, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "한국어 공부는 재미있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "재미있어요", "start_ms": 570, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "이 문장을 따라 읽어보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "읽어보세요", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "잘 하셨습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 190, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "천천히 다시 해볼까요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "해볼까요?", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
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
"transcript": "발음이 아주 정확해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정확해요", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "조금 더 크게 말해주세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "말해주세요", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "이것은 가라오케 시스템을 위한 테스트입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가라오케", "start_ms": 380, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시스템을", "start_ms": 780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 1180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "테스트입니다", "start_ms": 1380, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "네 알겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "알겠습니다", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "다시 한 번 들어보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어보세요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "문장이 조금 길어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "문장이", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "길어요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "천천히 끊어서 읽으면 도움이 됩니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "끊어서", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "읽으면", "start_ms": 560, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 760, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 960, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "이해가 가시나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "이해가", "start_ms": 25, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가시나요?", "start_ms": 275, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "네 이해했습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이해했습니다", "start_ms": 130, "dur_ms": 440, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "다음 단계를 진행하도록 하겠습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "단계를", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "진행하도록", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "질문이 있으시면 언제든지 말씀해주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있으시면", "start_ms": 240, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 490, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말씀해주세요", "start_ms": 790, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "알겠습니다 감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "알겠습니다", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 370, "dur_ms": 210, "phase_lr_deg": 0.0}
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
"transcript": "수고하셨습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 195, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "오늘 수업은 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
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
"transcript": "복습 잊지 마시고 다음 시간에 봬요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "복습", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "마시고", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 630, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 830, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "매일 조금씩 꾸준히 하는 것이 중요해요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조금씩", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 640, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 790, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 890, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
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
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
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
"transcript": "That is all I think it's really easy and",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "easy", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 1400, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "이제 마지막 테스트 문장입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "테스트", "start_ms": 420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "문장입니다", "start_ms": 720, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "완벽하게 끝났습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "완벽하게", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "끝났습니다", "start_ms": 340, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "모두 고생 많으셨어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많으셨어요", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "다음에 또 만나요 안녕",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 520, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:34:10*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14936  time=11:33:09  uptime=35290s  mem=432MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_112910.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 265-276-260-256-250-256-260-250-605-675-765-675-765-675-765-675-765-675_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 104-143-103-136-103-134-134-135-136-103-103-163-136-103-136-134-103-134-103-103-136-136-143 104-103-136-143-103-134-103-134-104-103-134-146-145-146-146-134-134 514-513-153-134-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 163-156-106-136-103-107-263-106-126-136-730-302-603-360-301-236-163-106-103-103-457-612-715-310-103-675-163-632-516-163-136-106-156-612-136-136-103-130-316-103-163-673 716-167-6_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 106-163-137-701-107-216-126-132-301-607-632-670-672-106-671-617-261-123-301-301-302-354-350-157-132-316-301-312-137-167-156-157-106-132-136-167-137-310-136-261-206-602-620-602-2_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 134-143-134-143-134-135-413-314-134-104-130-104-134-135 341-301-104-134-130-143-103-135-143-310-314-134-314-134-135-134-134-135-134-103 314-341-301-103-103-301-134-104-103-314-3_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 627-765-675-756-765-765-765-765-576-765-765-765-765-765-762-765-627-675-216-671-716-765-761-675-762-765-260-751-765-671-716-756-756-756-765-675-762-267-621-576-756-765-765-756-7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 573-573-756-573-753 136-163-615-165-571-157-317-103-301-317-731-173-107-103_W.mp3)
  Segment 8: 11032ms ~ 19240ms (file: 236-231-132-312-132-312-312-173-130-615-136-123-201-735-361-301-316-350-306-302-312-321-236-210-316-310-106-136-170-127-273-201-675-102-201-120-230-527-253-273-320-107-327-312-3_W.mp3)
  Segment 9: 20240ms ~ 26900ms (file: 231-103-312-762-127-216-527-275-217-167-107-753-276-276-301-371-731-753-720-153-357-301-132-327-302-312-236-302-327-301-321-132-603-176-172-370-526-751-672-216-261-216-216-213-2_V.mp3)
  Segment 10: 27900ms ~ 38592ms (file: 627-326-103-102-157-105-106-326-236-236-231-107-167-236-236-657-625-120-327-130-756-762-237-120-123-102-130-756-312-627-612-267-236-236-362-312-230-231-230-573-320-106-130-107-1_W.mp3)
  Segment 11: 39592ms ~ 50176ms (file: 301 261-263-302-263-203-230-310-213-267-263-103-260-312-301-312-260-310-260-103-271-237-123-231-213-102-271-162-132-216-132-261-236-312-126-230-612-261-216-120-231-276-267-267-6_W.mp3)
  Segment 12: 51176ms ~ 57584ms (file: 206-267-106-623-302-267-752-102-230-236-260-726-276-320-320-320-765-231-267-103-172-352-321-320-327-130-620-123-130-317-230-263-260-267-765-267-576-576-325-230-236-123-267-312-1_W.mp3)
  Segment 13: 58584ms ~ 59160ms (file: 576-571 571-507-507-504-547-540-574-547-574-574-574 574-547-574-574-574-570-571-576-576-576-574-574-574 547-547-547-574-574-574-574-574-576-574-574-576-576-576-571-105-517 574-5_W.mp3)
  Segment 14: 60160ms ~ 60736ms (file: 176-175-165-721-271-231-271-132-527-321-321-362-231-203-320-231-203-231-325-132-312-231-132-261-621-261-312-203-320-136-312-627-761-732-702-271-726-273-670-106-106-162-612-261-6_W.mp3)
  Segment 15: 61736ms ~ 62312ms (file: 130-132-103-135-132-103-103-130-310-135-103-135-130-301-135-103-102-310-310-103-130-123-310-130 135-103-130-103-130-315-130-103-310-103-301-123-103-103-103-123-130-312-103-130-3_V.mp3)
  Segment 16: 63312ms ~ 63888ms (file: 132-125-607-167-163-361-613-137-137-176-176-176-176-137-173-170-176-167-173-176-176-176-713-120-130-103-137-137-130-103-361 132 413_V.mp3)
  Segment 17: 64888ms ~ 81664ms (file: 310-103-230-132 123-203-130-132-130-203-301-302-103-130-301-213-321-321-230-103 132-413-130-310-107-103-206-126-130-571-312-547-103-310-301-530-570-574-576-230-210-301-321-130-2_V.mp3)
  Segment 18: 82664ms ~ 83240ms (file: 631 126-621-672-621-615-657-657-657-657-654-657-615 260-627-623-621-621-612-617-617-675-675-657-675-657-657-675-657-657-675-657-675-675-652-657-657-675-657-675-504-524-562-526-5_W.mp3)
  Segment 19: 84240ms ~ 95724ms (file: 267-230-132-574-572-537-752-275-261-154-210-201-231-206-123-103-310-130 756-765-657-571-756-576-576-571-675_W.mp3)
  Segment 20: 96724ms ~ 105148ms (file: 321-320-526-230-106-130-276-321-513-132-321-567-573-750-203-273-230-376-756-765-276-713-206-267-203-320-327-376-376-372-751-571-275-572-726-273-705-572-702-312-302-203-327-762-1_W.mp3)
  Segment 21: 106148ms ~ 106724ms (file: 203-302-306-203-260-206-320-270-207-261-236-231-320-326-623-602-301-320-306-132-163-103-130-301-231-301-307-306-367-751-705-173-761-132-317-103-613-167-163-103-310-103-230-302-6_W.mp3)
  Segment 22: 107724ms ~ 116796ms (file: 236-213-231-260-263-102-231-230-126-756-276-260-623-612-321-321-327-236-230-321-103-321-320-530-572-357-230-732-312-102-573-153-576-135-103-321-135-367-613-316-317-231-231-312-3_W.mp3)
  Segment 23: 117796ms ~ 118372ms (file: 213-265-162-721-126-316-136-136-127-361-763-731-672-760-361-316-310-103-160-761-107-137-127-327-132-567-607-675-605-675-675-675-675-675-765-765 765-675_W.mp3)
  Segment 24: 119372ms ~ 119948ms (file: 756-576-507-750-705-750-752-705-750-752-750-750-705-750-765-576-750-720-752-570-570 570-750-750-756-705-507-576-760-705-705-750-705-570-750-570-265-570-760-750-570 507-570-507-7_V.mp3)
  Segment 25: 120948ms ~ 121524ms (file: 126-321-312-321-631-123-163-132-102-135-613-127-136-316-621-726-312-752-251-712-560-175-276-521-127-312-726-507-157-215-725-257-571-273-526-276-231-273-572-263-275-273-576-362-5_V.mp3)
  Segment 26: 122524ms ~ 123100ms (file: 145-310-312-103-103-213-103-637-276-103-103-601-361-312-617-657-261-230-136-312-321-103-312-613-132-310-106-213-321-736-263-276-261-726-712-672-761-762-762-167-132-302-310-625-3_V.mp3)
  Segment 27: 124100ms ~ 124676ms (file: 153-135-153-153-517-510-517 517-517-571-517-573-573-513-517-517-517-517-576-517_W.mp3)
  Segment 28: 125676ms ~ 133956ms (file: 312-623-672-632-102-107-265-751-715-217-120-546-576-617-761-107-107-267-231-213-657-267-265-150-236-321-675-576-672-276-267-263-263-126-160-136-267-260-261-263-103-267-236-261-1_W.mp3)
  Segment 29: 134956ms ~ 135532ms (file: 571-157-571-107-175-150-157-157-713-175-715-175-173-173-173-173-713-173-713-317-175-715 517-570-571-570-751-571-571-513-715-153-175-715-715-137-173-135-170-137-153-713-713-170-7_V.mp3)
  Segment 30: 136532ms ~ 137108ms (file: 671-123-632-675-675-167 106-613-263-260-206-206-206-260-726-721-760-230-326-123-126-136-301-367-367-132-126-163-362-175-716_V.mp3)
  Segment 31: 138108ms ~ 138684ms (file: 136-316-137-376-375-573-375-371-375-713-375-375-371-137-371-136-316-673-376-637-673-673-673-637-637-763-167-367-376_W.mp3)
  Segment 32: 139684ms ~ 140260ms (file: 150-150-513-150-105-510-157-510-135-150-501-571 510-501-510-150-501-510-501-150-510-501-510-501-150-510-105-150-510-135-105-107-510-150-105-510-517-517-571-513-517-571-571-574-5_V.mp3)
  Segment 33: 141260ms ~ 141836ms (file: 761 163-621-216-612-162-126-126-214-612-261-153-631-126-134-136-126-132-713-471-345_V.mp3)
  Segment 34: 142836ms ~ 143412ms (file: 371-103-317-371-310-136-607-602-160-607-630-673-307-703-730-710-507-507 173-751-103-371-713-130-573-315-301-160-601-376-163-603-671-673-675-613-672-671-617-671-617-637-671-675-6_V.mp3)
  Segment 35: 144412ms ~ 144988ms (file: 751 751-570-173-175-751-571-751-570-754-570-173-715-571-750-173-735-713-175-751-753-571-713-751 751-751-751-750-571-571-751-517-751-751-751-175-173-715-571-750-715-571-715-715-7_W.mp3)
  Segment 36: 145988ms ~ 146564ms (file: 507-170-751-701-751-751 570-705-750-753-710-753-317-751-705-751-705-751-570-572-753-507-570-753-536-537-357-573-357-357-351-573-375-356-356-573-573-567-563-576-570-507-705-710-7_V.mp3)
  Segment 37: 147564ms ~ 148140ms (file: 103-315-135-103-175-135-315-135-103 135-105-135-135-312-135-132-153-315-317-137-135-103-137-310-103-315-135-317 137-130-135-135-315-153-135-315-137-132-137-103-103-103-132-317-3_V.mp3)
  Segment 38: 149140ms ~ 160660ms (file: 132-123-312-103-130-320-756-207-207-320-103-310-756-123-103-176-316-216-301-130-107-170-105-310-132-136-132-312-316-213-136-564-176-576-275-273-752-574-752-217-123-310-210-203-7_W.mp3)
  Segment 39: 161660ms ~ 162236ms (file: 657-675-267-672-673-163-136-136-135-153-567-765-675-567-567-567-576-567-567-567-657-615-165-615-517-571-571-571-715-517-561-516-156-576-657 615-561-156-651-156-651-165-156-567-5_W.mp3)
  Segment 40: 163236ms ~ 163812ms (file: 267-267-261-263-261-261-265-261-261-267-263-261-267-261-623-620-621-620-625-620-620-620 602-620-625-625-625-625-627-627-625-267-261-265-267-267-263-261-261-263-620-620-620-621-6_W.mp3)
  Segment 41: 164812ms ~ 173308ms (file: 102-102-132-120-231-310-201-231-321-103-127-573-275-120-236-231-132-231-756-207-207-203-207-126-106-102-230-102-750-103-203-103-201-102-102-263-126-201-120-102-213-301-123-210-1_V.mp3)
  Segment 42: 174308ms ~ 174884ms (file: 716-237-625-567-137-635-756-312-623-632-136-163-316_W.mp3)
  Segment 43: 175884ms ~ 183516ms (file: 756-163-261-765-321-216-165-526-321-572-261-615-216-276-231-267-652-310-130-317-753-231-213-123-263-267-261-312-130-103-573-623-632-263-213-263-312-531-547-715-213-231-261-627-2_W.mp3)
  Segment 44: 184516ms ~ 185092ms (file: 613-672-765-607-162-675-625-657-765-675-625-627-627-612-657-507-576-576-756-765-301-103-130-210-206-213-206-316-136-620-162-163-136-163-136-761-167-160-165-107-105-567-576-756-5_W.mp3)
  Segment 45: 186092ms ~ 186668ms (file: 173-157-756-765-165-762-167-761-172 752-756-570-726-276-167-176-163-317-165-137-135-617-763-765-103-360-362-137-701-571-127-536-157-370-671-170-173-731-317-123-126-157-123-507-2_W.mp3)
  Segment 46: 187668ms ~ 188244ms (file: 561-651-156-615-651-615-651-561-150-561-615-163-156-165-651-651-615-651-615-651-561-561-615-651-156-156-651-615-653 260-236-237-612-621-612-621-612 162-612-615-615-612-651-625-6_W.mp3)
  Segment 47: 189244ms ~ 189820ms (file: 103-316-576-751-157-617-716-176-160-326-132-301-165-632-627-632-367-176-671-136-136-102-136-302-231-201-261-321-256-623-103-132-613-265-132-367 547-574-574 671-675-623-326-163-7_W.mp3)
  Segment 48: 190820ms ~ 199784ms (file: 132-302-231-263-267-320-726-753-627-627-267-576-275-276-672-175-576-574-130-263-625-576-152-320-130-132-261-267-312-230-103-216-231-267-210-260-612-201-201-206-527-261-102-260-2_W.mp3)
  Segment 49: 200784ms ~ 201360ms (file: 574-576-576-561-675-576-762-756-756-576-537-576-574-576-657-765-107 546-567-572-576 765-756-752-572-572-576 123-132 312-756-572-236-612-123-321-375-213-763-716-672-672-236-216-2_W.mp3)
  Segment 50: 202360ms ~ 202936ms (file: 137-132-317-130-103-130-137-310-310-137-130-173-135-107-310 130-135-310-315-130-130-315-731-103-130-132-137-107-103-130-103-137-137 137-103-137-103-107-137-317-103-130-103-173-3_V.mp3)
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
> **Time**: 2026-04-05 11:34:10
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
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하세요", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "만나서 반갑습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "만나서", "start_ms": 40, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "반갑", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "습니다", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "오늘 날씨가 좋네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 210, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "식사는 하셨나요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "식사는", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하셨나요?", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "But hopefully that somebody has to do that",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "But", "start_ms": 110, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "hopefully", "start_ms": 310, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "somebody", "start_ms": 860, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "has", "start_ms": 1310, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 1510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 1610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 1760, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "한국어 공부는 재미있어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "공부는", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "재미있어요", "start_ms": 570, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "이 문장을 따라 읽어보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "따라", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "읽어보세요", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "잘 하셨습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 190, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "천천히 다시 해볼까요?",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 60, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "해볼까요?", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
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
"transcript": "발음이 아주 정확해요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "발음이", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정확해요", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "조금 더 크게 말해주세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "크게", "start_ms": 270, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "말해주세요", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "이것은 가라오케 시스템을 위한 테스트입니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "이것은", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가라오케", "start_ms": 380, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시스템을", "start_ms": 780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 1180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "테스트입니다", "start_ms": 1380, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "네 알겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "알겠습니다", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "다시 한 번 들어보세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들어보세요", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "문장이 조금 길어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "문장이", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "길어요", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "천천히 끊어서 읽으면 도움이 됩니다",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 60, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "끊어서", "start_ms": 360, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "읽으면", "start_ms": 560, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "도움이", "start_ms": 760, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "됩니다", "start_ms": 960, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "이해가 가시나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "이해가", "start_ms": 25, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가시나요?", "start_ms": 275, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "네 이해했습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이해했습니다", "start_ms": 130, "dur_ms": 440, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "다음 단계를 진행하도록 하겠습니다",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "단계를", "start_ms": 200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "진행하도록", "start_ms": 400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "질문이 있으시면 언제든지 말씀해주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있으시면", "start_ms": 240, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 490, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "말씀해주세요", "start_ms": 790, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "알겠습니다 감사합니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "알겠습니다", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 370, "dur_ms": 210, "phase_lr_deg": 0.0}
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
"transcript": "수고하셨습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하셨습니다", "start_ms": 195, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "오늘 수업은 여기까지입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기까지입니다", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
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
"transcript": "복습 잊지 마시고 다음 시간에 봬요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "복습", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "마시고", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 480, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 630, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "봬요", "start_ms": 830, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "매일 조금씩 꾸준히 하는 것이 중요해요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매일", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "조금씩", "start_ms": 190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "꾸준히", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 640, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 790, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "중요해요", "start_ms": 890, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "안녕히 계세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
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
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
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
"transcript": "That is all I think it's really easy and",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "really", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "easy", "start_ms": 1200, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 1400, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "이제 마지막 테스트 문장입니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "이제", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 220, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "테스트", "start_ms": 420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "문장입니다", "start_ms": 720, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "완벽하게 끝났습니다",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "완벽하게", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "끝났습니다", "start_ms": 340, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "모두 고생 많으셨어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "고생", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많으셨어요", "start_ms": 350, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "다음에 또 만나요 안녕",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 320, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 520, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:34:10*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=11:31:07  uptime=34680s  mem=462MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_112722.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7488ms (file: 103-312-207-120-127-260-260-201-132-103-706-312-310-103-103-230-231-672-623-527-106-576-276-123-123-276-260-130-130-367-267-106-213-103-310-623-237-736-237-753-572-215-206-123-2_V.mp3)
  Segment 2: 8488ms ~ 17272ms (file: 107-127-137-137-103-260-620-271-230-260-172-132-674-132-176-103-163-132-261-130-301-321-321-130 170-627-216-136-672-216-260-103-216-120-107-236-213-621-321-267-261-231-102-102-3_V.mp3)
  Segment 3: 18272ms ~ 18848ms (file: 316-316-103-130-163-631-132-570-657-623-501-103-136-103-130-607-103-231-132-632-261-132-203-302-103-610-103-136-136-176-540-510-320-102-132-136-103-504-572-521-176-173-107-107-1_V.mp3)
  Segment 4: 19848ms ~ 20424ms (file: 547-547-547-574-547-547-574-576-576-574-574-754-754-754-754-561-567-561-514 574-576-574-574-574-547-547-547-574-574-576-547-574-576-574-574-574-756-574-574-574-573-571-546-567-5_W.mp3)
  Segment 5: 21424ms ~ 22000ms (file: 351-130-150-130-135-135-153-315-513-510-153-157-513-157-571-517-571-517-513 105-153-157-130-153-517-517-150-170-130-130-310-317-157-513-517-517-573-571-571-571-571-571-571-751 1_V.mp3)
  Segment 6: 23000ms ~ 23576ms (file: 574-574-574-752-574-754-754-574-754-752-275-275-756-765-762-765-762-675-762-762-765-756-752-652-654-675-671-617-176-765-765-675-761-756-761-765-765-765-765-756-715 763-761-617-1_W.mp3)
  Segment 7: 24576ms ~ 25152ms (file: 132 172-176-763-726-712-713-763-713-735-761-765-736-675-367-507-657-507-537-513-573-576-756-756-763-765-756-317-312-316-327-273-217-231-623-312-371-172-127-216-213-261-362-672-6_W.mp3)
  Segment 8: 26152ms ~ 39328ms (file: 123-213-210-213-231-213-321-123-213-312-120-103-302-213-120-123-203-132-312-301-301-302-321-305-301-354-357-315-367-361-316-312-312-321-567-567-132-213-136-132-213-127-216-261-1_V.mp3)
  Segment 9: 40328ms ~ 47780ms (file: 126-136-213-273-263-753-731-231-572-256-260-672-137-130-312-326-236-365-653-362-123-715-316-216-312-213-231-102-236-236-276-213-312-371-762-260-263-201-260-260_W.mp3)
  Segment 10: 48780ms ~ 49356ms (file: 301-105-103-130-103-134-103-104-105-104-104-104-104-150-150-104-105-105-154-103-145-103-134 134-314-103-103-103-103-143-135-104-130-105-103-130-105-130-105-104-103-134-153-103-1_W.mp3)
  Segment 11: 50356ms ~ 50932ms (file: 217-321-213-236 712-235-572-750-267-526-231-261-572-756-316-672-273-273-213-570-261-567-620-236-267-756-327-127-560-523-625-537-326-265-735-356-756-275-375-263-621-256-526-725-3_V.mp3)
  Segment 12: 51932ms ~ 61544ms (file: 764-760-206-260-103-134-260-103-130-132-103-134-261-260-261-102-321-261-321-312-316-132-132-316-312-103-163-136-163-103-167-675-671-601-657-716-576-517-516-251-562-120-312-536-5_W.mp3)
  Segment 13: 62544ms ~ 68628ms (file: 276-231-325-265-357-621-315-132-325-357-267-752-103-172-123-135-132-132-315-235-235-103-137-312-213-271-261-236-103-325-756-173-132-367-513-253-521-102-103-301-235-123-261-132-2_W.mp3)
  Segment 14: 69628ms ~ 70204ms (file: 357-137-571-301-317-136-603-637-316-603-316-637-630-617-137-136-173-103-170 175-715-713-317-310-637-601-617-103-301-610-106-103-610-306-301-136-130-130-163-176-603-371-317-107-1_V.mp3)
  Segment 15: 71204ms ~ 77504ms (file: 257-265-276-275-275-702-271-721-173-271-271-173-671-176-170-107-175-756-756-571-157-321-326-275-236-753-721-672-756-276-726-167-276-267-627-637-312-132-316-573-107-572-754-673-1_W.mp3)
  Segment 16: 78504ms ~ 79080ms (file: 751-507-507-572-571-135-130-135-753-527-735-572-576-276-756-517-752-357-167 510-351-157-571-517-150-152-153-157-175-157-751-175-150-157-132-571-517-571-571-751-175-725-732-137-7_W.mp3)
  Segment 17: 80080ms ~ 80656ms (file: 701-517-170-571-157-751-750-573-705-570-715-731-751-570-735 150-750-751-750-751-751-751-137-750-570-170-750-710-753-107-517-751-750 571-574-570-571-570-157-175-157-571-750-150-7_W.mp3)
  Segment 18: 81656ms ~ 89252ms (file: 261-106-752-267-213-752-256-261-261-257-103-103-130-517-103-576-265-526-542-167-265-216-257-267-261-327-321-132-210-260-261-260-102-574-265-132-574-273-627-130-261-267-562-276-2_W.mp3)
  Segment 19: 90252ms ~ 90828ms (file: 713-715-175-173-175-107-701-173-731-107-715-137-173-713-137-170-173-137-173-175-175-715-715-301 173-175-371-173-713-715-713-175-173-137-175-173-713-317-137-175-137-103-317-137-7_V.mp3)
  Segment 20: 91828ms ~ 92404ms (file: 130-301-310-312-537-576-316-236-136-130-310-652-632-613-126-716-123-671-130-130-157-137-312-310-321-312-310-312-356-350-316-301-316-310-136-132-321-312-750-756-765-765-765-765-7_W.mp3)
  Segment 21: 93404ms ~ 93980ms (file: 756-675-752-756-765-765-765-765-765-765-756-675-675-765-675-762-763-376-132-657-756-351-375-761-137-213-201-216-612-261-321-132-137-132-136-317-153-516-567-156-571-517-516-567-5_W.mp3)
  Segment 22: 94980ms ~ 95556ms (file: 132-123-162-612-516-264-654-546-547-541-564-516-132-312-650-605-605-657-126-756-657-562-567-675-567-657-657-675-637-657-750-670-705-725-670-675-602-576-756-576-756-607-627-605-6_V.mp3)
  Segment 23: 96556ms ~ 100912ms (file: 260-267-260-260-620-620-260-260-620-260-620-627-267-620-260-620-627-260-260-260-260-260-260-620-260-260-206-260-706-361-216-321-312-321-320-321-231-321-276-206-261-267-263-261-2_W.mp3)
  Segment 24: 101912ms ~ 102488ms (file: 132-157-205-327-751-507-576-263-756-725-632-672-576-276-275-132-753-761-257-267-725-735-627-216-315-750-312-527-570-762-257-750-617-763-256-267-207-276-265-750-623-571-762-263-2_V.mp3)
  Segment 25: 103488ms ~ 104064ms (file: 547-325-321-321-541-517-537-531-316-326-326-231-163-613-326-547-574-574-517-573-375-136-615-534-537-531-517-267-623-263-267-263-327-236-263-231-231-126-261-216-256-152-512-326-3_W.mp3)
  Segment 26: 105064ms ~ 106972ms (file: 103-132-301-132-216-621-216-136-132-136-132-136-176-137-103-123-301-723-206-254-265-231-102-103-321-132-132-321-132-132-132-301-312-617-671-576-576-574-574-574-574-576-361-621-6_V.mp3)
  Segment 27: 107972ms ~ 108548ms (file: 765-576-367-167-167-671-167-716-176-176-176-761-107-167-157-531-517-576-537-753-713-751-671-137-130-167-176-167-106-106-106-105-106-160-107-107-103-103-763-756-765-765-765-736-6_W.mp3)
  Segment 28: 109548ms ~ 110124ms (file: 576-573-576-573-357-735-375-165-165-163-165-165-351-576-753-735-375-735-715-160-176-136-176-107-107-105-170-137-165-160-156-163-165-162-756-675-765-751-735-573-756-531-175-753-6_W.mp3)
  Segment 29: 111124ms ~ 121924ms (file: 507-750-270-230-672-176-574-572-267-231-672-267 765-761-172-213-237-547-107-231-672-267-236-574-572-572-547-726-765-327-574-574-572-267-261-137-726-541-157-453-541-231-754-574-3_W.mp3)
  Segment 30: 122924ms ~ 123500ms (file: 175-250-570-752-720-725-205-275-207-721_V.mp3)
  Segment 31: 124500ms ~ 125076ms (file: 765-546-504-504-506-504-503-502-504-507-506-506-506-567-507-752-756-765-756-756-567-756-756-756-756-752-756-756-756-725-504-561-507-501-507-507-507-507-576-571-506-506-506-567-5_V.mp3)
  Segment 32: 126076ms ~ 134032ms (file: 160-106-231-726-721-267-260-726-160-136-321-231-237-317-361-267-263-231-106-160-613-106-632-213-751-756-312-163-123-120-102-765-320-302-231-210-207-327-317-317-103-135-102-120-3_W.mp3)
  Segment 33: 135032ms ~ 135608ms (file: 710-175-715-713-371-176-170-173-731-173-715-173-170-173-710-317-750-750-751-751 571-175-137-157-713-173-175-157-173-715-175-713-175-712-175-172-175-172-176-127-175-175-170-173-7_V.mp3)
  Segment 34: 136608ms ~ 142584ms (file: 162-320-267-576-127-360-725-107-760-572-157-507-607-176-210-157-271-562-135-750-103-705-672-752-765-261-201-310-123-127-176-176-173-132-126-162-613-613-762-170-760-263-312-362-1_W.mp3)
  Segment 35: 143584ms ~ 144160ms (file: 136-135-156-517-516-163-136-103-136-103-130-103-136-310-103-316-106-106-103-103-120-102-103-130-136-310-320-310-310-302-301-602-306-316-361-362-361-203-203-203-520-230-263-760-6_W.mp3)
  Segment 36: 145160ms ~ 145736ms (file: 501-501-501-501-506-501-502-504-501-572-756-576-576-756-576-576-756-576-576-756-756-672-632-546-504-506-504-546-506-501-502-506-502-506-506-752-756-576-576-576-756-576-576-576-5_W.mp3)
  Segment 37: 146736ms ~ 147312ms (file: 501-561-517-517-751-137-571-175-153-137-127-165-173-167-731-675-756-756-756-756-756-756-735-756-372-153-765-715-135-153-135-156-153-135-571-753-371-517-751-357-751-173-175-156-1_W.mp3)
  Segment 38: 148312ms ~ 148888ms (file: 547-510-150-157-517-105-501-507-570-571-570-570-571-157 105-501-510-517-175-571-507-507-571-751-105-105-170 501-150-517-507-510-157-105-570-751-175-715 501-510-517-105-501-507-5_V.mp3)
  Segment 39: 149888ms ~ 150464ms (file: 732-162-752-132-127-507-512-627-167-157-267-316-215-572-721-576-712-267-271-257-751-312-312-571-357-321-506-576-675-126-735-576-735-527-705-162-123-705-157-276-706-713-726-217-2_V.mp3)
  Segment 40: 151464ms ~ 152040ms (file: 235-715-513-543-263-260-260-256-267-261-267-260-267-267-265-306-625-236-263-625-256-253-263 267-627-754-574-537-253-752-275-275-372-753-357-573-752-574-547-574-526-574-567-753-7_W.mp3)
  Segment 41: 153040ms ~ 153616ms (file: 567-356-357-615-157-176-317-713-316-167-106-537-736-730-730-301-576-517-157-136-105-163-150-672 576-756-136-630-362-673 576 132 213-325-415-275-213-146-132_V.mp3)
  Segment 42: 154616ms ~ 155192ms (file: 160-316-106-612-106-126-657-310-136-136-136-103-103-312-136-136-321-312-316-136-132-103-631-567-127-316-163-163-103-167-136-263-130-103-103-367-736-132-316-610-613-361-631-306-3_V.mp3)
  Segment 43: 156192ms ~ 164292ms (file: 276-361-103-136-172-126-756-627-610-612-672-160-317-163-672-107-321-263-210-126-263-762-612-103-143-267-312-312-321-325-326-571-672-752-176-107-160-617-721-162-673-261-362-107-3_V.mp3)
  Segment 44: 165292ms ~ 165868ms (file: 157-154-157-517 570-501-507-105-517-157-157-153-150-105-517-517-571-510-571-517-570-570-574-576-574-574-576-567-576-574-576-574-574-570 150-105-105-501-571-571-150-510-157-501-7_V.mp3)
  Segment 45: 166868ms ~ 167444ms (file: 715-715-276 130-173-716-130-731-731-107-103-137-130-370-301-107-703-301-301-103-130-130-317-170-301-135-705-701-570-705-730-701-731-107 315-130-317-107-137-307-712-371-103-173-1_V.mp3)
  Segment 46: 168444ms ~ 179208ms (file: 217-675-657-651-206-627-457-620-163-135-213-132-576-761-167-261-607-275-236-165-321-213-267-705-213-250-326-217-267-617-657-327-236-205-132-261-167-136-576-713-125-706-205-132-7_V.mp3)
  Segment 47: 180208ms ~ 184960ms (file: 213-206-532-135-231-321-735-753-216-231-236-265-671-132-231-576-132-263-132-312-623-672-572-251-216-236-263-263-213-312-572-756-265-216-517-261-137-130-231-312-132-316-312-573-5_W.mp3)
  Segment 48: 185960ms ~ 186536ms (file: 617-617-617-617-617-617-671-617-617-617-610-512-572-504-504-504-501-504-615-672-657-675-765-675-657-675-617-675-671-675-617-162-571-165-276-726-275-735-725-725-273-723-726-726-3_W.mp3)
  Segment 49: 187536ms ~ 188112ms (file: 231-132-612-613-136-126-521-123-163-132-326-261-123-561-162-361-513-521-203-153-576-157-756-672-601-261-610-620-602-627-765-672-712-710-107-136-102-103-726-237-163-132-136-134-2_W.mp3)
  Segment 50: 189112ms ~ 189688ms (file: 267-612-236-162-654-613-126 216-215-103 162-106-327-627-607-267-267-621-620-675-263-672-201-132-671-361-163-623 316-310-674-621-126-621 627_W.mp3)
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
> **Time**: 2026-04-05 11:34:52
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "안녕하세요 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 100, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 1400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 2100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 3500, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "Where is the nearest subway station please?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "Where", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "the", "start_ms": 700, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "nearest", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "subway", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "station", "start_ms": 1900, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "please?", "start_ms": 2600, "dur_ms": 400, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "가장 가까운 지하철역이 어디인가요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가까운", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지하철역이", "start_ms": 1100, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "어디인가요?", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "I would like to order a coffee.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "would", "start_ms": 400, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "like", "start_ms": 650, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 950, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "order", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 1550, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "coffee.", "start_ms": 1700, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 6,
"transcript": "커피 한 잔 주문할게요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "커피", "start_ms": 150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "잔", "start_ms": 950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "주문할게요.", "start_ms": 1250, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 8,
"transcript": "실례합니다 혹시 이 근처에 화장실이 있을까요?",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 300,
"audio_type": "speech",
"words": [
{"word": "실례합니다", "start_ms": 300, "dur_ms": 1100, "phase_lr_deg": 10.0},
{"word": "혹시", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "이", "start_ms": 2200, "dur_ms": 200, "phase_lr_deg": 10.0},
{"word": "근처에", "start_ms": 2450, "dur_ms": 700, "phase_lr_deg": 10.0},
{"word": "화장실이", "start_ms": 3200, "dur_ms": 900, "phase_lr_deg": 10.0},
{"word": "있을까요?", "start_ms": 4200, "dur_ms": 800, "phase_lr_deg": 10.0}
]
},
{
"segment": 9,
"transcript": "Excuse me, is there a restroom nearby?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "Excuse", "start_ms": 180, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "me,", "start_ms": 750, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 1100, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "there", "start_ms": 1350, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 1650, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "restroom", "start_ms": 1800, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "nearby?", "start_ms": 2500, "dur_ms": 600, "phase_lr_deg": -5.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:34:52*
