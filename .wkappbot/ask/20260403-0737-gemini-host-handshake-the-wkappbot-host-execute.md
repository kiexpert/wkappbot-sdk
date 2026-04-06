# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=07:36:34  uptime=22640s  mem=1656MB  system_processes=446"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_073625.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 173-163-162-261-672-130-236-236-236-506-216-534-263-623-263-267-213-612-126-126-756-267-267-675-675-675-675-765_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 107-167-130 316-617 130-261-176-123_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 216-213-213-572-725-725-275-273-257-257-275-231-231-321-327-137-172-132-312-123-273-267-627-625-362-623-632-623-627-623-326-321-321-231-237-237-237-237-327-237-237-237-537-572-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 162-160-167-102-103-165-156-762-672-160-136-216-615-261-162-176-130-657-163-263-716-732-612-136-163-726-125 103-614-136-671-716-761-541-123-276-210-261-156-756-756-576-756-765-7_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 130-316-136-310-302-167-316-631-160-617-267-623-316-301-132-130-316-160-615-103-267-362-620-160-621-627-621-162-167-167-132-216-167-165-106-103-615-657-236-162-263-617-167-106-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 670-675-760-670-637-670-675-706-756-756-675-671-106-107-561-645-675-612-617-750-574-750-756-756-765-675-760-756-750-574-765-627-672-657-163-317-756-537-576-567-357-356-765-675-7_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 132-312-136-132-123-132-132-153-136-132-321-531-526-256-326-326-256-263-321-123-312-631-312-312-132-351-152-132-125-521-352-315-126-125-125-261-132-321-312-132-312-132-352-365-3_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 136-123-160-106-123-126-321-102-126-103-164-136-132-123-132-310-261_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 612-106-106-513 310-132-162-172-127-123-130-123-210-675-673-576-256-672-672-672-627 602-127-162-126-106-103-163-136_V.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 261-162-106-103-136-165-721-160-546-165-231-671-607-167-301-657-675-670-675-675-671-675-612-601-675-765-675-213-302-532-312-120-126-213-621-267-163 263-263-631-126-632-236-623-3_W.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 625-652-625-137-261-261-213-126-237-237-657-675-576-753-752-765-756-263-765-675-675-726-261-237-267-726-762-763-672-726-275-372-203-210-213-201-263-263-326 106-163-136-316-216-6_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 602-162-316-671-316-671-263-260-102-120-316-102-107-132-103-103-167-167-167-761-651-167-325-136-173-130-103-172-137-514-103-657-651-160-165-106-263-617-731-103-261-126-263-217-2_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 176-123 236-326-321 261-236-263-627-267-627_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 162-675-726-621-162-236-231-263-716-762-762-765-657-762-752-726-726_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 157-516-157-531-153-156-156-165-531-560-651-156-517-573-576-531-657-156-536-567-653-567-563-357-315-537-516-567-516-156-571-156-516-157-156-157-156-516-105-102-130-351-351-315-3_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 123-603-123-126-325-153-123-127-205-275-312-352-325-321-327-327-326-267-362-126-123-102-132-302-213-327-207-237-317-327-632-730-317-137-327-321-271-231-237-732-236-301-123-103-3_W.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 751-752-715-761-751-217-271-170-263_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 123-621-620-160-301-623-263 621 301-321-260-206-260-206-261-203 673-360 236-620-602-602-602-620-762-760-607-610-601-612-613-613-632-612-670-671-760-132-136-103-263-673-675-672-6_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 164-326-645-164-165-123-672-670-132-236-231-326-236-157-632-302-120-231-167-236-321-621-632-361-136-206-213-657-623-562-150-130-310-102-163-613-312-132-103-312-573-531-150-137-6_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 567-567-567-576-657-536-576-567-576-652-567-653-653-756-576-675-756-657-576-657-657-672-567-176-521-561-567-567-635-567-567-572-105-357-103-632-675-652-657-652-657-576-567-576-6_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 123-126-621-602-216-261-215-531-123-123-103-132 574-570-573-657-657-675-132-316-103-136-621-206-620-263-621-103-135-136-136-132 176-162-765-706-137-137-105-126-103-132-126-167-1_V.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 217-210-240-230-261-263-263-230-236-253-235-215-235-231-321-325 362-317-317-376-317-316-315-536-517-537-657-657-365-537-165-576-576-567-152-527-574-257-257-257-275-236-236-261-2_W.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 216-674-605-675-623-263-672-321-127-217-261-672-672-672-762-762-726-701-126-137-136-132-123-126-271-273-271-217-216-127-132-127-172-132-123-312-123-127-126-123-126-132-167-123-1_V.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 106-126-165-637-615-126-261-103-673-631-103-761-163-163-165-167-176-163-610-130-153-103-103-107-170-321-675-673-672-627-267-623-675-672-267-762-672-726-762-276-275-273-276-275-2_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 167-106-163-136-312-231-162-230-302-136-301-326-213-326-136-103-126-123-612-613-206-260-136-130-102-216-162-230-206-603-607-756-570-751-165-106-632-302-326-302-753-576-325-306-3_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 236-320-106-321-320-302-632-627-326-362-163-103-103-130-310-632-362-302-301-136-103-102-210-201-206-230-203-135-106-201-261-316-310-130-136-163-306-302-301-136-310-306-321-102-1_W.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 576-574-576-574-576-574-574-750-657-652-625-265-265-256-607-321-312-103-263-263 361-146-163 163 103-321 163-103 621-261-765-621 173-617-671-263-261-263-263-621-261-162-132-257-6_W.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 750-376-765-275-651-265-675-652-627-762-627-672-163-107-107-271-102-721-631-312-231-216-201-210-210-201-210-201-312-130_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 126-321 321-326-321 167 104-103-143-124-135-136-132-136-136-132-136-132-103-103-130-103-610-162-136-126-136-162-103-126-132 360 160_V.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 671-567-657-675-612-627-623-263-623-623-162-361-213-231-103-310-103 160-160-160-201-204-201 321-103-123-132-132-312-130 631-136-310-310-103-632-635-617-367-631-163-136-130-102-1_W.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 125-102-130-132-103-132-716-132-132-675-627-623-261-627-312 671-106-136-130-163-137-316-312-160-675-671-316-216-213-132-132-231-103-163-132-136-136-136-160-356-106-167-310-136-3_V.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 136-137-126-163-617-123-132-135-162-165_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 216-261-231-120-312-123-175-132-127-237-675-576-125-316-137-130-326-126-321-573-576-576-576-365-173-627-236-320-213-312-132-103-756-706-672-367-326-302-103-301-132-731-170-715-3_W.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 302-130-130-130-310-316-135-132-206-263-260-206 130-103-310-310-310-167-162-261-267-312_V.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 631-126-136-213-621-623-621-623-623-632-621-621-623-623-621-621-621-623-260-621-623-621-623-621-623-623-620-620-627-620-627-621-621-620-627-260-261-621-621-623-621-620-620-623-6_W.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 130-326-123-601-130-706-175-170-263-276 136-631-130-137-136-136-162-625-726-152 107-103-167-136-106-613-612-136-617-316-165-316-615-103-130-157-130-136-107-130-135-213-721-657-3_V.mp3)
  Segment 37: 56736ms ~ 67716ms (file: 137-103-765-106-217-165-527-126-126-103-126-103-176-167-106-167-106-106-165-106-103-137-136-716-162-167-715-321-713-316-127-165-675-507-361_V.mp3)
  Segment 38: 68716ms ~ 69292ms (file: 523-352-253-321-321-231-321-231-321-321-312-316-321-126-326-236-321-326-231-526-526-567-536-365-563-536-543-574-627-236-326-362-312-531-573-135-372-312-257-526-256-265-625-546-5_W.mp3)
  Segment 39: 70292ms ~ 70868ms (file: 106-301-103-231-310-132-263-623-623-621-605-601-613-601-160-106-610-106-163-675-675-762-263-260-236-126-162-106-103-601-610-360-301-106-132-103-106-163-103-765-301-312-316-362-1_V.mp3)
  Segment 40: 71868ms ~ 72444ms (file: 261-260-236-236-675-576-752-756-627-576-103-132-576-572-756-762-627_W.mp3)
  Segment 41: 73444ms ~ 75352ms (file: 651-316-162-764-754-361-132-136-672-613-361-631-612-627-612-672-162-617-672-617-617-617-617-617-607-675-617-261-167-672-120-167-106 316-637-123-617-261-123-126-132-632-163-765-6_W.mp3)
  Segment 42: 76352ms ~ 76928ms (file: 103-162-167-261-631-613-610-167-136-103-123-136-362-561-126-206-216-126-163-623-621-632-672_V.mp3)
  Segment 43: 77928ms ~ 78504ms (file: 752-725-753-674-543-540-573-576-573-357-540-547-371-317-715-572-152-712-547-574-570-237-237-231-231-235-231-231-375-326-326-326-273-275-273-327-756-375-317-317-534-351-536-356-5_W.mp3)
  Segment 44: 79504ms ~ 80080ms (file: 143-145-143-132-134-173-136-132-137-132-143-123-132-213-132-267-123-613-265-672-627-123-362-162-213-312-132_V.mp3)
  Segment 45: 81080ms ~ 81656ms (file: 103-301-132-321-130-517-125-163-615-165-621-672-723-126-126-123-312-321-361-130-126-123-130-163-631-361-712-617-675-165-675-765-754-756-754-627-765-507-675-675-615-103-130-312-3_W.mp3)
  Segment 46: 82656ms ~ 83232ms (file: 547-567-576-576-574-567-576-576-567-576-576-765_W.mp3)
  Segment 47: 84232ms ~ 84808ms (file: 103-125-176-106-103-756 574-574-574-627-316-675-762-132-316 103-165-625-136-602-103-671-175-312-167-103-106-326-362-126-160-162-165-136-103-167-136-103-765-167-167-167-136-137-2_V.mp3)
  Segment 48: 85808ms ~ 86384ms (file: 152-317-136-315-516-153-513-156-136-136-315-316-631-163-156-315-316-316-316-351-536-315-130-301-315-130-153-351-157-165-516-165-156-156-651-657-675-657-765-756-567-765-561-561-5_W.mp3)
  Segment 49: 87384ms ~ 87960ms (file: 637-653-627-267-675-316-265-671-631-617-623-632-576-567-567-576-567-756-576-652-576-651-562-576-752-625-621-562-562-635-675-361-612-652-156-561-567-576-567-657-765-526-562-567-5_W.mp3)
  Segment 50: 88960ms ~ 89536ms (file: 267-162-162-754-574-615-163-547-574-571-517-326-123-130-132-123-162-167-571-571-567-175 321-236 675-765-675-765-765-762-765-762-672-672-572-756-572-756-756-756-675_W.mp3)
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
> **Time**: 2026-04-03 07:37:51
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "안녕하세요. 오늘 날씨가 참 좋네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕하세요.", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "좋네요.", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "Yes, it's a beautiful day.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "it's", "start_ms": 210, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 280, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "beautiful", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "day.", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": -5.0}
]
},
{
"segment": 3,
"transcript": "이런 날에는 산책을 가고 싶어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "날에는", "start_ms": 170, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "산책을", "start_ms": 300, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 440, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "싶어요.", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "I agree. Where should we go?",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 45, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "agree.", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "Where", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "should", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "we", "start_ms": 430, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "go?", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": -5.0}
]
},
{
"segment": 5,
"transcript": "근처 공원이 좋겠어요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "근처", "start_ms": 60, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "공원이", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋겠어요.", "start_ms": 370, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "That sounds perfect.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "sounds", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "perfect.", "start_ms": 290, "dur_ms": 280, "phase_lr_deg": -5.0}
]
},
{
"segment": 7,
"transcript": "혹시 배고프지는 않으세요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "배고프지는", "start_ms": 160, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "않으세요?", "start_ms": 390, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "Actually, I am a bit hungry.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Actually,", "start_ms": 110, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "I", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "am", "start_ms": 320, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 390, "dur_ms": 30, "phase_lr_deg": -5.0},
{"word": "bit", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "hungry.", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": -5.0}
]
},
{
"segment": 9,
"transcript": "그럼 가기 전에 뭐 좀 먹을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 55, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가기", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전에", "start_ms": 285, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 385, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 435, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "먹을까요?", "start_ms": 485, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "What kind of food do you like?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "kind", "start_ms": 110, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "of", "start_ms": 190, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "food", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "do", "start_ms": 350, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "like?", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": -5.0}
]
},
{
"segment": 11,
"transcript": "저는 매운 음식을 좋아해요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 70, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "매운", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋아해요.", "start_ms": 460, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "Me too! Let's have Tteokbokki.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Me", "start_ms": 100, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "too!", "start_ms": 180, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "Let's", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "have", "start_ms": 360, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "Tteokbokki.", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": -5.0}
]
},
{
"segment": 13,
"transcript": "좋아요. 저기 맛집이 있어요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "좋아요.", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "저기", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛집이", "start_ms": 290, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "Perfect. I can't wait.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "Perfect.", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "I", "start_ms": 190, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "can't", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "wait.", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": -5.0}
]
},
{
"segment": 15,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 16,
"transcript": "여기 정말 맛있어 보여요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 155, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있어", "start_ms": 275, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "보여요.", "start_ms": 415, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "It smells so good.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 80, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "smells", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "so", "start_ms": 300, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "good.", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": -5.0}
]
},
{
"segment": 18,
"transcript": "주문은 키오스크에서 하면 돼요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "주문은", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "키오스크에서", "start_ms": 185, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "돼요.", "start_ms": 505, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "Okay, let me try it.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Okay,", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "let", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "me", "start_ms": 250, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "try", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "it.", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": -5.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "주문 완료했어요.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "주문", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "완료했어요.", "start_ms": 280, "dur_ms": 290, "phase_lr_deg": -5.0}
]
},
{
"segment": 22,
"transcript": "잘하셨어요. 이제 기다려 볼까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "잘하셨어요.", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기다려", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "볼까요?", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "How long does it take?",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "How", "start_ms": 35, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "long", "start_ms": 105, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "does", "start_ms": 215, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "it", "start_ms": 305, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "take?", "start_ms": 365, "dur_ms": 210, "phase_lr_deg": -5.0}
]
},
{
"segment": 24,
"transcript": "보통 10분 정도 걸려요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "보통", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "10분", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 320, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "걸려요.", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "Not too bad. Let's wait.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Not", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "too", "start_ms": 150, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "bad.", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "Let's", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "wait.", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": -5.0}
]
},
{
"segment": 26,
"transcript": "음식이 나왔네요. 맛있게 드세요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "음식이", "start_ms": 45, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나왔네요.", "start_ms": 205, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 395, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드세요.", "start_ms": 515, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "(music)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 28,
"transcript": "Thank you! It looks spicy.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 110, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "you!", "start_ms": 220, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "It", "start_ms": 320, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "looks", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "spicy.", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": -5.0}
]
},
{
"segment": 29,
"transcript": "네, 색깔이 아주 빨갛네요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "색깔이", "start_ms": 120, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 260, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "빨갛네요.", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "Wow, this is really hot!",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Wow,", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "this", "start_ms": 155, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 245, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "really", "start_ms": 305, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "hot!", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": -5.0}
]
},
{
"segment": 31,
"transcript": "물 좀 드릴까요?",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "물", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "드릴까요?", "start_ms": 240, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "Yes, please.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "Yes,", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "please.", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": -5.0}
]
},
{
"segment": 33,
"transcript": "여기 있습니다. 천천히 드세요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여기", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "천천히", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드세요.", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "Thank you. It's delicious though.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "you.", "start_ms": 145, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "It's", "start_ms": 235, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "delicious", "start_ms": 325, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "though.", "start_ms": 515, "dur_ms": 60, "phase_lr_deg": -5.0}
]
},
{
"segment": 35,
"transcript": "입맛에 맞으셔서 다행이에요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "입맛에", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "맞으셔서", "start_ms": 200, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "다행이에요.", "start_ms": 430, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "I'm full now. Let's go.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "I'm", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "full", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "now.", "start_ms": 245, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "Let's", "start_ms": 345, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "go.", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": -5.0}
]
},
{
"segment": 37,
"transcript": "네, 산책하러 가요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 60, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "산책하러", "start_ms": 150, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "가요.", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "The weather is still great.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "The", "start_ms": 110, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "weather", "start_ms": 170, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 310, "dur_ms": 50, "phase_lr_deg": -5.0},
{"word": "still", "start_ms": 370, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "great.", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": -5.0}
]
},
{
"segment": 39,
"transcript": "공원 공기가 아주 상쾌해요.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "공원", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "공기가", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "상쾌해요.", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "I feel refreshed.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "feel", "start_ms": 125, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "refreshed.", "start_ms": 285, "dur_ms": 290, "phase_lr_deg": -5.0}
]
},
{
"segment": 41,
"transcript": "오길 잘한 것 같아요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오길", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "잘한", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 400, "dur_ms": 170, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "Thank you for bringing me here.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 155, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "for", "start_ms": 245, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "bringing", "start_ms": 315, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "me", "start_ms": 465, "dur_ms": 40, "phase_lr_deg": -5.0},
{"word": "here.", "start_ms": 515, "dur_ms": 60, "phase_lr_deg": -5.0}
]
},
{
"segment": 43,
"transcript": "(instrument)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "instrument",
"words": []
},
{
"segment": 44,
"transcript": "아니에요. 저도 즐거웠어요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "아니에요.", "start_ms": 60, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "즐거웠어요.", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "Let's do this again sometime.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Let's", "start_ms": 50, "dur_ms": 110, "phase_lr_deg": -5.0},
{"word": "do", "start_ms": 170, "dur_ms": 70, "phase_lr_deg": -5.0},
{"word": "this", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "again", "start_ms": 350, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "sometime.", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": -5.0}
]
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "네, 언제든지 좋아요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 135, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "좋아요.", "start_ms": 365, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "See you later.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "See", "start_ms": 110, "dur_ms": 130, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "later.", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": -5.0}
]
},
{
"segment": 49,
"transcript": "네, 다음에 봐요.",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 50, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봐요.", "start_ms": 300, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "Goodbye!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "Goodbye!", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": -5.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 07:37:51*
