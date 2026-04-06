# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:46:18  uptime=23223s  mem=1781MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_074608.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 265-257-256-534-534-574-576-536-576-567-510-576-517-567-567-567-567-567-536-574-547-576-567-540 352-235-250-326-352-315-356-351-351-315-315-351-357-576-576-574-570-521-513-523-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 176-173-143-132-607-260-260-163-103-136-106-106-362-602-620-260-627-625-675-672-621-261-230-260-126-126-162-172-723-716-301-302-312-756-207-206-206-126-206-102-206-130-103-135 2_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 627-132-572-507-530-672-316-132-126-107-130-321-621-126-107-160-120-321-156-627-132-612-356-136-132-103-103-163-136-103-132-135-106-137-120-167-156-103-103-312-103-103-136-541-2_W.mp3)
  Segment 4: 4728ms ~ 12863ms (file: 172-136-571-105-125-136-163-106-167-617-167-613-561-316-360-103-163-136-136-103-573-132-312_V.mp3)
  Segment 5: 13863ms ~ 14439ms (file: 651-654-651-654-316-326-163-216-265-261-163-612-126-162-536-256-256-162-126-312-123-531-536-153-513-153-152-315-312-123-214-321-321-540-514-153-150-150-650-650-635-657-675-263-2_W.mp3)
  Segment 6: 15439ms ~ 16015ms (file: 136-163-136-316-260-260-260-267-627-136-130-130-672-617-617-367-617-671-167-163-167-136-167-130-136-102-167-765-657-617-136-167-617-167-617-167-167-167-617-120-627-671-671-167-7_W.mp3)
  Segment 7: 17015ms ~ 17591ms (file: 612-602-126-102-162-162-102-120-123-321-102-312-103-630-627-627-517-157-231-321-321-213-321-213-230-231-321-261-657-613-612-671-675-675-231-132-130-102-162-163-103-163-516-560-3_V.mp3)
  Segment 8: 18591ms ~ 19167ms (file: 623-137-156 103-167-136-761-106-106-572-176-263-132-256-106-103-316-103-136-103-136-163-163-163-160-132-130-574-165-106-651-163-106-263-167-106-163-162-175-170-751-751-576-715-1_V.mp3)
  Segment 9: 20167ms ~ 20743ms (file: 276-236-342-413-125-132-173-167-670-670-672-672-263-103-132-134-132-134-134-135-175-132-123-371-317-137-136-162-132-213 670-670-670-607-607-103-675-672-765-657-657-567-762-765-6_V.mp3)
  Segment 10: 21743ms ~ 22319ms (file: 103 132-132-136-162-123-125-136-132-163-103-132-621-630-160-126-632-162-765-105-106-132-163-162-106-106-316-103-361-361-136-103-167-103-106-136-103-123-132-173-621-362-132-602-3_V.mp3)
  Segment 11: 23319ms ~ 23895ms (file: 627-162 621-765-675-672-620-625-267-260-256-265-126-213-123-213-216-216-571-750-574-570-705-715-176-127-132-136-173-167-154-236-103-132-103-132_V.mp3)
  Segment 12: 24895ms ~ 25471ms (file: 367-130-310-105-512-630-320-310-103-163-613-306-321-326-302-236-126-612-612-627-617-162-126-602-602-263-231-103-130-160-163-163-716-675-573-627-672-260-620-126-230-162-216-216-2_V.mp3)
  Segment 13: 26471ms ~ 27047ms (file: 657-512 576-576-653-672-673-103-103-103-713-760-765-632-576-756-576-756-765-160-156-103-371-732-756-735-617-163-610-167-130-613-167-160-576-571-617-167-312-623-162-136-167-102-1_W.mp3)
  Segment 14: 28047ms ~ 28623ms (file: 765-756-657-657-620-620-203-127 765-753_W.mp3)
  Segment 15: 29623ms ~ 30199ms (file: 765-756-576-756-106-167-750-735-610-102-621-260-621-267-206-126-621-102-613-261-206-261-203-126-107-203-620-762-106-612-613-160-631-612-673-106-162-612-612-312-612-103-612-106-3_W.mp3)
  Segment 16: 31199ms ~ 31775ms (file: 617-213-621-127-217-627-261-102-627-621-317-132-136-163-627-627_W.mp3)
  Segment 17: 32775ms ~ 33351ms (file: 176-761-107-103-136-501-571-570-756-176-672-276-276-576-576-765-765-176-671-371-305-306-306-216-730-176-103-107-167-260-620-627-167-564-136-360-632-367-176-547-572-371-367-317-1_W.mp3)
  Segment 18: 34351ms ~ 34927ms (file: 567-675-175-260-260-602-126-216-210-312-236-526-206-260-267-260-620-206-236-106-136-103-310-132-136-315-216-613-103-167-162-761-621-160-362-761-176-613-613-176-360-627-715-123-1_V.mp3)
  Segment 19: 35927ms ~ 36503ms (file: 267-627-103-312-130-132-306-310-130-153-537-567-157-756-567-574-756-576-257-267-235-320-320-302-302-316-130-103-103-135-571-576-103-651-507-576-567-567-567-567-362-263-326-130-3_W.mp3)
  Segment 20: 37503ms ~ 38079ms (file: 735-106-175-162-106-130-163-162-106-136-126-102-102-312-753-735-106-106-103-132-201-201_V.mp3)
  Segment 21: 39079ms ~ 41671ms (file: 361 627 671-607-163-671-176-731-601-176-310-126-136-102-276-612-621-621-621-102-716-175-170-127-672-675-637-630-602-237 162 132-672-637-675-507-765-657 167-103-136-576-107-136 5_W.mp3)
  Segment 22: 42671ms ~ 43247ms (file: 316-217-321-271-167-136-136-136-725-725-275-217-753-327-326-163-312-532-356-516-514-167-136-163-132-126-126-573-576-752-576-526-351-352-251-536-367-375-371-365-316-135-315-317-3_W.mp3)
  Segment 23: 44247ms ~ 44823ms (file: 320-613-136-302-306-306-206-203-207-206-203-267-260-206-352-547-547-547-207-206-230-327-320-572-105-106-302-637-136-302-267-310-107-571-571-675-571 127-154-230-302-132-602-627-7_W.mp3)
  Segment 24: 45823ms ~ 46399ms (file: 132-632-163-615-612-162-312-321-321-326-312-301-361-362-136-361-163-136-301-610-306-632-612-621-613-162-612-162-631-103-163-106-601-602-613-413-132-143_V.mp3)
  Segment 25: 47399ms ~ 47975ms (file: 567-516-716-761-617-103-215-162 163-106-125-156-625-103-137-136-125-103 261-312-362-137-132-312-236-216-321 567-657 216-261-762-137-316_W.mp3)
  Segment 26: 48975ms ~ 49551ms (file: 265-265-652-625-265-265-263-623-263-523-265-265-362-263-235-536-352-361-316-312-315-316-316-316-312-365-312-316-316-312-316-132-316-231-231-321-316-326-623-362-365-615-625-236-2_W.mp3)
  Segment 27: 50551ms ~ 51127ms (file: 134-136-632-623-362-312-132-236-326-312-163-361-361-362-263-623-236-102-103-132-312-213-312-261-162-602-672-603-675-765-106-310-136-607-670-607-602-123-163-123-541-574-620-206-2_V.mp3)
  Segment 28: 52127ms ~ 52703ms (file: 631-610-162 701-136-163-123-103-103-156-562-167-106-103-103-132-103-135-621-132-165-607-157-162-103-106-106-103-106-103-263-251-615-613-263-620-162-123-601-106-137-136-103-160-5_V.mp3)
  Segment 29: 53703ms ~ 54279ms (file: 163-137-120-167-136-103-561-120-167-167-167-106-576-157-106-106-561-716-126-102-167-106-163-637-162-163-126-103-163-106-167-125-167-132-367-165-106-163-136-106-163-601-103-623-1_V.mp3)
  Segment 30: 55279ms ~ 67159ms (file: 176 612-671-136-623-765-651-357-136-713-761-671-657-613-316-176-103-306-135-576-163-160-632-132-613-130-135-617-130-136-316-132-165-136-103-167-163-126-136-612-615-617-376-670-1_W.mp3)
  Segment 31: 68159ms ~ 68735ms (file: 675-672 765-637 576-756-756-756-106-627-261 657-675-657-615-675 670-657-106-136-132-165-162-136-236 160-167-162-103-136-103-136-103-136-156-137-563-132-126-163-103-601-623 627-6_W.mp3)
  Segment 32: 69735ms ~ 70311ms (file: 765-765-317-310-136-206-320-103-106-162-103-765-675-607-627-602-620-267-302-705-176-137-173-132-103-103-132-163-310-301-316-312-130-316-230-312-310-321-302-305-531-103-125-132-3_V.mp3)
  Segment 33: 71311ms ~ 71887ms (file: 657-513-675-657-576-675-567-576-675-576-675-657-567-567-567-576-567-576-657-567-635-567-651-576-675-756-156-657-657-615-651-615-165-517-657-156-537-657-675-657-567-156-156-165-6_V.mp3)
  Segment 34: 72887ms ~ 73463ms (file: 167-765-103-132-236-572-756-705-326-320-103-630-123-132-670-106-632-362-130-301-612-132-725-106-576-106-361-615-106-103-160-103-263-162-312-627-362-132-160-601-163-160-160-163-6_V.mp3)
  Segment 35: 74463ms ~ 75039ms (file: 526-265-256-526-256-635-267-621-562-567-527-672-652-625-625-265-265-265-265-265-265-236-236-326-236-263-236-326-263-326-256-236-265-265-263-265-256-256-254-526-256-235-576-573-5_W.mp3)
  Segment 36: 76039ms ~ 76615ms (file: 356-572-573-354-253-236-362-576-567-352-357-327-276-527-543-532-257-152-135-135-135-315-315-315-315-326-316-326-236-352-527-326-375-326-325-326-316-637-362-367-362-326-637-316-3_W.mp3)
  Segment 37: 77615ms ~ 78191ms (file: 163-162-157-123-167-152-162-106-132-162-172-127-162-106-631-167-603-610-267-612-612-130-617-136-657-106-107-675-617-160-623-160-613-362-751-156-137-126-127-126-103-163-321-123-1_V.mp3)
  Segment 38: 79191ms ~ 79767ms (file: 537-537-573-153-316-132-103-130-163-103-510-517-573-531-310-127-326-203-302-671-176-132 135-152-756-752-273-623-627-267-621-261-261-231-321-260-621-260-267-627-572-574-574-257-2_W.mp3)
  Segment 39: 80767ms ~ 81343ms (file: 132-715-167-576-567-652-567-765-576-157-175-107-567-576-576-657-675-671-657-627-672-627-267-765-126-163-260-523 571-572-673-576-571-256-510-576-357-517-573-576-632-362-675-675-6_W.mp3)
  Segment 40: 82343ms ~ 82919ms (file: 103-564-615-756-765-760-106-132-106-137-103-574-574-365-136-102-123-312-710-173-620-162-261-602-620-176-261-163-136-174-105-362 564-657-652-162-261-312-163-367 126-162-627-261-6_V.mp3)
  Segment 41: 83919ms ~ 84495ms (file: 126-102-103-750-571-107-132-160-136-167-613-206-615-321-106-610-671-160-163-103-167-103-136-126-106-160-167-610-125-203-103-132-102-165-150-163-316-103-120-103-210-623-756-675-1_V.mp3)
  Segment 42: 85495ms ~ 86071ms (file: 361-362-362-362-361-326-362-361-361-136-130-132-123-132-103-132-162-267-167-756-675-675-672-163-136-175-163-310-316-302-361-103-103-230-210-203-213-102-103-301-136-132-706-756-7_V.mp3)
  Segment 43: 87071ms ~ 87647ms (file: 136-671-163-263 162-163-163 132-645-652-167_V.mp3)
  Segment 44: 88647ms ~ 89223ms (file: 132 103-162-617-301-726-175-617-163-167-316-136-103-176-137-103-107-103-657-176-361-127-126-213-637-567-567-652-562-765-657-567-625-657-567-562-576-572-675-120-731-102-675-561-5_W.mp3)
  Segment 45: 90223ms ~ 90799ms (file: 123-132-236-231-216-231 516 163-625-627-625-265-216-612-625-263-236-132-263-627-236 751-756-752-765 627-260-162-312-136_W.mp3)
  Segment 46: 91799ms ~ 92375ms (file: 657-632-365-316-670-627-605-657-651-756-672-620-627-672-672-162-103-326-263-267-267-672-261-627-267-720-723-672-762-760-760-132-261-163-637-652-560-675-524-627-267-623-136-621-2_W.mp3)
  Segment 47: 93375ms ~ 93951ms (file: 106-126-126-123-123-126-310-213-102-102_V.mp3)
  Segment 48: 94951ms ~ 95527ms (file: 627-623-213-267-623-362-632-625-267-263-627-625-236-623-726-672-765-267-623-623-367-236-627-526-256-265-625-627-256-627-275-367-236-527-675-623-672-726-123-326-261-253-627-267-2_W.mp3)
  Segment 49: 96527ms ~ 97103ms (file: 310-136-132-103-136-132-126-132-163-132-125-103-163-176-167-260-610-603-103-132-102-160-316-231-213-621-126-263-260-620-126-540-542-103-103-152-160-160-167-617-671-603-106-106-1_V.mp3)
  Segment 50: 98103ms ~ 98679ms (file: 312-267-263-267-267-627-267-547-574-517-156-276-574-574-574-540-574-576-567-576-567-650-534-543-547-354-540-547-517-517-540-547-574-547-531-312-316-623-351-315-326-325-576-357-5_W.mp3)
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
> **Time**: 2026-04-03 07:47:28
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 기분은 어떠세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기분은", "start_ms": 165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어떠세요?", "start_ms": 315, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "날씨가 좋아서 아주 상쾌해요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 180, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "상쾌해요", "start_ms": 390, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "주말에 특별한 계획이 있나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 40, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "특별한", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "계획이", "start_ms": 310, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 410, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "I am planning to go hiking with my friends.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 60, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "am", "start_ms": 110, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "planning", "start_ms": 170, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 340, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 410, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "hiking", "start_ms": 490, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "with", "start_ms": 640, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 710, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "friends.", "start_ms": 770, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "등산은 건강에 아주 좋은 취미죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "등산은", "start_ms": 55, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "건강에", "start_ms": 195, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 315, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 405, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "취미죠", "start_ms": 485, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "정상에 올랐을 때의 기분은 최고예요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "정상에", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "올랐을", "start_ms": 185, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "때의", "start_ms": 315, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "기분은", "start_ms": 395, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "최고예요", "start_ms": 505, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "어떤 산으로 가기로 했나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "어떤", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산으로", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "했나요?", "start_ms": 390, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "북한산에 가려고 해요 가깝고 좋거든요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "북한산에", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "가려고", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해요", "start_ms": 310, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "가깝고", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋거든요", "start_ms": 490, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "안전에 유의해서 잘 다녀오세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안전에", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "유의해서", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 295, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다녀오세요", "start_ms": 365, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "고마워요 다녀와서 이야기해 줄게요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "고마워요", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "다녀와서", "start_ms": 160, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이야기해", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "줄게요", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "좋아요 사진도 많이 찍어오세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋아요", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사진도", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 270, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "찍어오세요", "start_ms": 370, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Don't forget to wear comfortable shoes.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Don't", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "forget", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 235, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "wear", "start_ms": 285, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "comfortable", "start_ms": 385, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "shoes.", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "물론이죠 등산화는 이미 챙겨뒀어요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "물론이죠", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "등산화는", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 320, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "챙겨뒀어요", "start_ms": 420, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "준비성이 정말 철저하시네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "준비성이", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 185, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "철저하시네요", "start_ms": 285, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "간식으로는 무엇을 가져가시나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "간식으로는", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "무엇을", "start_ms": 190, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "가져가시나요?", "start_ms": 290, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "오이와 초콜릿을 좀 가져가려고요",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "오이와", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "초콜릿을", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 305, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "가져가려고요", "start_ms": 385, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "수분 보충과 에너지에 최고죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "수분", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보충과", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "에너지에", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "최고죠", "start_ms": 385, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "Enjoy your trip to the mountains!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Enjoy", "start_ms": 35, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "your", "start_ms": 135, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "trip", "start_ms": 185, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 265, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 315, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "mountains!", "start_ms": 355, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "정말 감사해요 잘 다녀올게요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "감사해요", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "다녀올게요", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "다음에는 같이 가면 좋겠네요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에는", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가면", "start_ms": 250, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋겠네요", "start_ms": 340, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "물론이죠 다음에 꼭 같이 가요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "물론이죠", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 285, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 355, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 445, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "기대하고 있을게요 연락주세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "기대하고", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있을게요", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "연락주세요", "start_ms": 285, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "그럼 오늘 하루도 잘 보내세요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 140, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하루도", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 420, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "당신도 즐거운 시간 보내시길 빌어요",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "당신도", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "즐거운", "start_ms": 160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 280, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보내시길", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "빌어요", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Take care of yourself!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Take", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "care", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 220, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "yourself!", "start_ms": 270, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "안녕히 계세요 다음에 또 봐요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "계세요", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 285, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 395, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 455, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "조심히 들어가세요 안녕",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 170, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "대화가 정말 즐거웠습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "대화가", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 185, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "즐거웠습니다", "start_ms": 285, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "저도요 유익한 시간이었어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "저도요", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "유익한", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "시간이었어요", "start_ms": 285, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "I look forward to our next talk.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 35, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "look", "start_ms": 85, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "forward", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 295, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "our", "start_ms": 345, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "next", "start_ms": 405, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "talk.", "start_ms": 505, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "그럼 진짜로 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "진짜로", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 380, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "네 수고하셨습니다 안녕",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수고하셨습니다", "start_ms": 100, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0}
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
"transcript": "마지막 인사를 다시 하네요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "마지막", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "인사를", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 305, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "하네요", "start_ms": 405, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "Have a nice weekend!",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Have", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 125, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "nice", "start_ms": 165, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "weekend!", "start_ms": 265, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "You too, bye bye!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "You", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "too,", "start_ms": 100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "bye", "start_ms": 190, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "bye!", "start_ms": 290, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "정말 갈게요 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "갈게요", "start_ms": 150, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 290, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "진짜 마지막 안녕!",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 45, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 145, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "안녕!", "start_ms": 265, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "수고 많으셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "수고", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "많으셨습니다", "start_ms": 160, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "항상 건강하세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "건강하세요", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "행복한 하루 보내세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 170, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "Keep smiling always!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Keep", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "smiling", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "always!", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "감사합니다 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "See you soon!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 105, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "soon!", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "진짜 마지막 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 50, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 150, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 270, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 390, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "Byebye!",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Byebye!", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "잘 가요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 40, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 120, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "안녕!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "안녕!", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "고생하셨습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "고생하셨습니다", "start_ms": 50, "dur_ms": 230, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-03 07:47:28*
