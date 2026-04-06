# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=06:55:28  uptime=20173s  mem=1609MB  system_processes=450"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_065518.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 276-276 123-761-621-672-765-765-756-726-275-103-615-762-275-257-276-203-206-237-237-257-320-213-250-253-576-765-675-756-615-213-736-362-236-263-326 103 325-352-532-352-537-567-5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 765-651-675-671-617-675-567-567-563-567-652-675-627-617-671-657-657-615-651-362-567-613-361-615-576-571-657-675-576-657-567-367-625-627-675-316-612-312-756-765 206-260-751-126-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 762-736-675-362-213-130 574-150 671 167-163-312-157-175 762-672-314-135-130-103-123-312-301-312-103-312 132-127-167-132-123-143 162-167-143-167-316-310-301-326-132-103-136-136-1_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 127-163-670-607-167-613-372-320-260-627-260-670-670-670-230-236 671-637-762-574-641-132-146-312-316-127-163-613-173-163-310-135-132-134-173-137-103-136-671-672-236-326-321-167-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 576 350-136 162-710-361-756 762-657_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 362-321-367-361-637-631-176-173-176-573-537-672-637-632-627-367-537-753-751-317-632-576-351-537-627-267-236-726-672-326-362-627-263-267-672-526-562-524-256-265-267-267-267-267-2_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 753-574-637-316-607-607-106-672-760-673-621-621-162-106-106-160-621-126-136-620-267-260-206-206 103-136-216-132 621-163-160-261-267-163-613-632-613-612-167-705-326-362-620-236-2_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 632-126-612-623-627-612-632-625-756-276-267-276-267-267 341-127-612 756-765-176-176-716-172-167-106-103-102-657-365-615-671-617_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 312 716-761 765-765-175-765-675-716-617_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 632-603-620-206-206-623-132-603-620-367-621-301-130-130-310-310-612-603-601-136-167-613-167-163-631-610-301-136-103-163-103-103-310-136-106-137-130-130-130-103-301 172-120-613-6_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 617-672-675-716-132-321-312-321-675-765-607-526-275-102-576-103-235-632-263 564-657-675-675_W.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 756-675-765-673-765 756-761-760-761-176-710-671-261-627-160-132_W.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 305-103-507-105-531-361-671-672-670-617-671-103-310-301-376-312-316-563-657-657-620-206-260-261-657-657-672-625-657-607-652-625-165-765-576-576-756-276-260-236-206-260-130-310-3_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 163-167-716-130-103-316-652-350-307-203-602-321-236-362-567-316-263-102-615-612-162-213-263-571-163-651-573-652-564-567-675-160-671-576-657-675-507-136-162-562-567-657-216-635-7_W.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 516-165-516-536-513-156-561-153-156-135-516-153-316-156-165-165-316-135-153-315-135-136-163-125-516-153-156-516-175-615-156-135-153-156-156-156-157-156-175-135-157-567-513-513-1_W.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 236-132-126-136-132-376-326-360-106-136-316-103-102-103-132-176-103-136-103-136-106-103-302-261-623-301-675-675-672-263-321-326-675-607-675-621-310-310-301-513-517-135-302-326-3_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 301-163-103-316-130-165-302-312-602-316-163-613-132-167-167-136-163-106-136-160-136-176-136-312-613-136-132-103-312-301-312 310-316-137-126-103-176-103-107-127-360-361-317-213-5_V.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 103-136-127-657-671-150-102-132-567-156-326-102-164-173-162-106-163-130-103-162-265-512-106-106-156-526-165-216-302-567-561-251-657-572-576-756-657-576-765-657-762-237-761-102-5_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 615-657-635-617-126-102-203-165-567-326-326-132-362-132-613-102-362-625-127-103-612-136-321-136-172-163-312-136-362-263-632-672-163-136-621-162-136-631-672-163-106-602-620-302-1_W.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 103-136-103-136-126-201-216-326-130-312-310-103-103-610-160-102-123-102-103-316-130-537-153-130-631-537-315-352-320-260-206-230-320-260-260-206-203-263-260-162-103-132-173-130-3_V.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 256-256-256-256-275-257-275-237-260-203-260-270-206-205-256-256-257-236-265-237-327-273-253-256-235-275-267-236-267-213-213-217-216-215-312-312-312-132-312-312-325-325-235-325-3_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 206-230-215-162-136-162-617-213-206-732-720-201-236-326-301-316-103-103-136-236-260-206-102-547-546-547-547-517-167-602-260-206-207-326-756-620-672-762-672-162-105-163-320-213-3_W.mp3)
  Segment 23: 34672ms ~ 47092ms (file: 260-637-726-725-275-276-271-752-752-725-275-752-672-672-671-657-652-625-125-751-756-756-135-106-756-310-715-126-213-671-130-103-165-103-106-514-153-517-127-163-173-671-102-167-1_W.mp3)
  Segment 24: 48092ms ~ 48668ms (file: 762-762-756-765 167-132-571-107-176-613-612-673-126-136-263-216-123-132-617-526-632-176-130-163-756-105-135-106-137-126-162-136-162-163-136-130-160-103-103-361-165-170-623-261-6_W.mp3)
  Segment 25: 49668ms ~ 55320ms (file: 134-213-107-731-167-127-173-327-136-203-135-315-321-165-761-763-671-136-136-163-612-520-162-160-132-162-312-103-130-105-326-356-670-261-675-160-321-160-627-620-162-103-572-513-5_V.mp3)
  Segment 26: 56320ms ~ 56896ms (file: 607-726-752-736-163-132-103-132-132-123-127-135-132-123-627-312 765-751-675 312-176-167-671-164-671-261-376-361 672-631-736-721-726-276-327-761-761-167-163-136-106-613-351-576-6_V.mp3)
  Segment 27: 57896ms ~ 58472ms (file: 675-576-675-621-162 567-657-657-567-576-567-576-213-621-562-517-576-571-361-102-273-172-103-132-306-103-132-126-610-103-163-106-136-107-107-715-136-172 103-137-701-162-106-567-1_V.mp3)
  Segment 28: 59472ms ~ 60048ms (file: 537-564-351-574-573-576-576-567-652-674-647-652-625-657-657-675-674-576-567-276-201-271-271-216-260-276-267-261-216-261-216-216-231-216-125-527-527-517-256-265-263-267-610-620-6_W.mp3)
  Segment 29: 61048ms ~ 61624ms (file: 106-136-157-156-167-164-165-316-301-106-321-163-132-136-176-132-671-610-675-762-706-673-136-137-751-701-126-123-206-267-263-120-726-106-607-206-162-126-612-607-601-627-732-275-3_V.mp3)
  Segment 30: 62624ms ~ 63200ms (file: 103-136-103-315-153-135-765-370-370-730-307-103-134-102-623-132-123-301-321-123-132-504-501-132-106-713-106-106-106-617-260-102-205-613-123-136-716-160-617-106-106-620-261-261-1_V.mp3)
  Segment 31: 64200ms ~ 64776ms (file: 613-123-632-612-261-632-623-613-361 756 362-126-123-136-672-567-672-675-654-651-612-136-103-132-312-123-361-136-612-617-167-150-571-657-617-130-567-650-547-546-625 132 613-672-6_W.mp3)
  Segment 32: 65776ms ~ 66352ms (file: 756-576-576-157-517-657-561-176-167-517-567-637-617-675-675-517-167-356-567-567-657-657-563-653-675-735-751-576-657-617-576-675-157-756-765-571-657-653-567-657-516-567-517-516-5_W.mp3)
  Segment 33: 67352ms ~ 67928ms (file: 216-231-263-627 172-706-672-763-673-671-602-672-760-103 176_W.mp3)
  Segment 34: 68928ms ~ 69504ms (file: 230-203-326-302-326-302-567-275-163-106-162-102-126-705-256-270-752-132_V.mp3)
  Segment 35: 70504ms ~ 71080ms (file: 765-763-526-256-721-172-516-576-657-217-213-271-762-762-762-726-765-765-762-762-761-762-761-761-716-726-726-725-276-276-273-276-275-274-275-572-257-576-627-642-657_W.mp3)
  Segment 36: 72080ms ~ 72656ms (file: 326-267-203-301-320-320-301-310-103-136-163-136-163-126-162-312-102-201-321-103-162-130-230-301-326-130-103-132-136-132-162-231-302-203-321-132-237-132-261-163-316-162-571-213-2_V.mp3)
  Segment 37: 73656ms ~ 74232ms (file: 132-231-263-756-627-312-231-123-321-132-413-130-301-301-271-130-310-132-103-136-316-547-574-765-312-137-156-162-176-103-175-751-107-167-132-705-137-137-317-376-371-750-705-307-3_V.mp3)
  Segment 38: 75232ms ~ 75808ms (file: 756 561-657-562-752-701-176-657-627-527-571-756-672-265-215-265-276-627-713-176-617-615-312-132-615-675-657-657-562-256-257-652-756-257-265-312-132-123-326-231-213-236-613_W.mp3)
  Segment 39: 76808ms ~ 77384ms (file: 362 317-312-132-132-132-103-103-312-127-153-132-675-765-627-623 765-362-675-675-317-316-361-136-103-103-132-132-750_V.mp3)
  Segment 40: 78384ms ~ 78960ms (file: 126-136-163-103-271-326-103-136-130-103-103-103-625-167-613-132-736-160-165-163-316-103-103-163-326-362-136-316-136-653-126-310-756-526-136-735-103-103-130-317-136-310-547-173-7_V.mp3)
  Segment 41: 79960ms ~ 80536ms (file: 106-206-260-267-256-103-510-135-103-136-103-103-126-216-201-261-312-213-231-321-213-231-127-126-761-126-503-507-105-132-547-760-675-650-603-163-136-167-162-163-321-163-516-501-5_W.mp3)
  Segment 42: 81536ms ~ 82112ms (file: 126-310-103-123-236-210-201-236-623-162-623-263-102-156-102-123-756-260-672-627-270-102-102-162-173-362-321-120-130-137-675-136-316-317-103-134-106-163-231-321-301-176-106-130-6_V.mp3)
  Segment 43: 83112ms ~ 83688ms (file: 163-163-137-103-316-617-130-132-130-103-103-356-136-167-130-103-136-162-167-106-136-601-106-610-623-631-617-107-163-671-163-132-617-216-132-136-163-652-361-172-617-316-130-163-1_V.mp3)
  Segment 44: 84688ms ~ 85264ms (file: 301-316-302-321-312-136-301-136-731-362-310-306-160-150-162-621-612-106-501-605-107-105-160-167-106-612-103-607-320-302-231-103-310-231-206-756-725-273-123-231-132-130-310-130-3_V.mp3)
  Segment 45: 86264ms ~ 86840ms (file: 276-562-217-675-615 671-657-657-576-627-675-576-567-567 675-576-657-671-651-126 136-360-126-162 726-756-765-761-617-103-671-617-160-130-162-612-130-103-623-132-130-546-230-136-1_W.mp3)
  Segment 46: 87840ms ~ 88416ms (file: 135-164-106-126-154-105-103-107-137-761-576-107-127-167-175-167-172-103-136-136-324-167-761-765-367-136-132-123-176-106-176-376-132-137-317-371-103-312-632-620-620-672-657 627-6_V.mp3)
  Segment 47: 89416ms ~ 89992ms (file: 756-163-106-103-276-716-136-716-103-761-106-176-167-176-321-167-752-107-176-103-176-137-106-136-123-132-617-761-632-617-716-765-760-761-310-130-130-106-160-760-137-107-756-756-1_V.mp3)
  Segment 48: 90992ms ~ 91568ms (file: 137-657-706-263-267-132-163-176-763-756 163-613-362-257-312-137-132-136-621-261-261-623-261-126-612-601-607-612-607-576-574-576-576-607-265-326-203-231-216-236-132-213-216-623-1_V.mp3)
  Segment 49: 92568ms ~ 93144ms (file: 267-261-267-213-267-265-715-175-150-176-126-156-125-756-716-605-106-510-157-170-176-516-510-105-501-651-613-610-561-657-675-657-675-675-675-370-760-136-651-650-167-651-607-670-6_V.mp3)
  Segment 50: 94144ms ~ 94720ms (file: 156-156-156-516-651-156-156-156-516-156-517-156-517-157-517-516-612-153-165-156-175-156-165-561-157-567-516-156-156-157-571-157-315-571-315-157-175-157-617-167-751-756-761-167-6_V.mp3)
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
> **Time**: 2026-04-03 06:56:44
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 기분은 어때요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "기분은", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "어때요?", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "날씨가 좋아서 아주 좋아요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "날씨가", "start_ms": 50, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 350, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "무슨 좋은 계획이라도 있나요?",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "무슨", "start_ms": 35, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 145, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "계획이라도", "start_ms": 245, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있나요?", "start_ms": 435, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "친구들과 한강에 가기로 했어요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "친구들과", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "한강에", "start_ms": 235, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가기로", "start_ms": 375, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 495, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "재미있겠네요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "재미", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있겠네요!", "start_ms": 190, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "한강에서 자전거 탈 거예요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한강에서", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "자전거", "start_ms": 230, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "탈", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 480, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "바람이 불어서 시원하겠어요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "바람이", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "불어서", "start_ms": 200, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "시원", "start_ms": 340, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하겠어요", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "네, 벌써 기대돼요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 125, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기대돼요", "start_ms": 285, "dur_ms": 260, "phase_lr_deg": 0.0}
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
"transcript": "한강에 사람이 많을까요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한강에", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사람이", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "많을까요?", "start_ms": 350, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "아마 주말이라 많을 것 같아요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "아마", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주말이라", "start_ms": 160, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "많을", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 440, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "주차하기 힘들겠네요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "주차", "start_ms": 45, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 185, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "힘들", "start_ms": 305, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "겠네요", "start_ms": 415, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "그래서 대중교통을 이용하려고요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "대중교통을", "start_ms": 190, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "이용", "start_ms": 420, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하려고요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "현명한 생각이네요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "현명한", "start_ms": 35, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "생각이네요!", "start_ms": 245, "dur_ms": 300, "phase_lr_deg": 0.0}
]
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
},
{
"segment": 16,
"transcript": "가서 맛있는 것도 먹나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "가서", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 160, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "먹나요?", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "치킨이랑 라면 먹기로 했어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "치킨이랑", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "라면", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먹기로", "start_ms": 400, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "했어요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "한강 라면이 정말 꿀맛이죠",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "한강", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "라면이", "start_ms": 175, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 335, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "꿀맛", "start_ms": 445, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이죠", "start_ms": 555, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "맞아요, 밖에서 먹으면 더 맛있어요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "맞아요,", "start_ms": 40, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "밖에서", "start_ms": 190, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "먹으면", "start_ms": 330, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 450, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "맛있어요", "start_ms": 510, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "즐거운 시간 보내고 오세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 35, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 205, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보내고", "start_ms": 325, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "오세요", "start_ms": 455, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "네, 다녀와서 얘기해 줄게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다녀와서", "start_ms": 130, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "얘기해", "start_ms": 320, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "줄게요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "조심히 잘 다녀오세요!",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 45, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 265, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "다녀오세요!", "start_ms": 345, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "요즘 한국 사람들은 한강에서 피크닉을 즐기는 걸 무척 좋아합니다. 텐트나 돗자리를 펴놓고 친구, 연인과 함께 맛있는 배달 음식을 시켜 먹는 문화가 아주 발달해 있어요. 특히 해 질 녘 노을을 보며 먹는 치킨과 맥주는 정말 일품입니다. 기회가 되시면 꼭 한번 경험해 보세요.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "요즘", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사람들은", "start_ms": 640, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한강에서", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "피크닉을", "start_ms": 1510, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "즐기는", "start_ms": 1970, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 2330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "무척", "start_ms": 2440, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좋아합니다.", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "텐트나", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "돗자리를", "start_ms": 3660, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "펴놓고", "start_ms": 4120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "친구,", "start_ms": 4480, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "연인과", "start_ms": 4740, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 5100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 5360, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배달", "start_ms": 5720, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "음식을", "start_ms": 6030, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시켜", "start_ms": 6390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "먹는", "start_ms": 6650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "문화가", "start_ms": 6910, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 7270, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "발달해", "start_ms": 7530, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있어요.", "start_ms": 7890, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 8400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 8710, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "질", "start_ms": 8820, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "녘", "start_ms": 8930, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "노을을", "start_ms": 9040, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보며", "start_ms": 9450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "먹는", "start_ms": 9760, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "치킨과", "start_ms": 10020, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맥주는", "start_ms": 10430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 10840, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "일품입니다.", "start_ms": 11100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기회가", "start_ms": 11800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되시면", "start_ms": 12160, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "꼭", "start_ms": 12520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한번", "start_ms": 12680, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "경험해", "start_ms": 12940, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보세요.", "start_ms": 13300, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "한강 가고 싶네요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "한강", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 230, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "싶네요!", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "같이 가요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "같이", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가요!", "start_ms": 190, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "좋은 생각이에요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "좋은", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "생각", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이에요!", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "주말에 시간 괜찮나요?",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "주말에", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시간", "start_ms": 240, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "괜찮나요?", "start_ms": 360, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "토요일 오후가 좋아요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "토요일", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "오후가", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 380, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "그럼 그때 만나요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그때", "start_ms": 165, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "만나요!", "start_ms": 285, "dur_ms": 290, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "어디서 만날까요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어디서", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만날까요?", "start_ms": 210, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "여의나루역 앞에서 봐요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "여의나루역", "start_ms": 40, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "앞에서", "start_ms": 300, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0}
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
"transcript": "알겠어요, 그럼 토요일에 봐요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "알겠어요,", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 215, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "토요일에", "start_ms": 335, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 495, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "네, 안녕히 가세요",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "안녕히", "start_ms": 130, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 36,
"transcript": "안녕히 계세요!",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "계세요!", "start_ms": 265, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "다음에 또 봐요",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 230, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 320, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "네, 연락드릴게요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 45, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "연락", "start_ms": 135, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 255, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "즐거운 주말 되세요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "즐거운", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "주말", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "되세요", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "민수 씨도요!",
"speaker": "speaker_2",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "민수", "start_ms": 35, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "씨도요!", "start_ms": 185, "dur_ms": 390, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "고마워요!",
"speaker": "speaker_1",
"confidence": 1.0,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "고마", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "워요!", "start_ms": 220, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "혹시 캠핑 좋아하세요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "혹시", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "캠핑", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "좋아하세요?", "start_ms": 305, "dur_ms": 270, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "네, 가끔 가요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가끔", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 260, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "어디로 주로 가시나요?",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "어디로", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "주로", "start_ms": 200, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가시나요?", "start_ms": 320, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "강원도 쪽으로 자주 가요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "강원도", "start_ms": 45, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "쪽으로", "start_ms": 235, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 355, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가요", "start_ms": 445, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "강원도는 바다도 있고 산도 있어서 정말 좋죠. 캠핑하기 딱 좋은 곳 같아요. 장비는 다 챙겨 다니시나요?",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "강원도는", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바다도", "start_ms": 420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "산도", "start_ms": 840, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋죠.", "start_ms": 1370, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "캠핑", "start_ms": 1700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 1910, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 2070, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 2180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "곳", "start_ms": 2340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "같아요.", "start_ms": 2430, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "장비는", "start_ms": 2800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 3060, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "챙겨", "start_ms": 3150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "다니", "start_ms": 3310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시나요?", "start_ms": 3470, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "네, 차에 항상 실려 있어요",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네,", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "차에", "start_ms": 130, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "항상", "start_ms": 250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "실려", "start_ms": 400, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 520, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "와, 부러워요! 저도 언젠가...",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "와,", "start_ms": 55, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부러워요!", "start_ms": 165, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 375, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "언젠가...", "start_ms": 465, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "같이 가요, 제가 다 알려드릴게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "같이", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가요,", "start_ms": 175, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 385, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "알려", "start_ms": 435, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "드릴게요", "start_ms": 535, "dur_ms": 40, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "정말요? 기대할게요!",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말요?", "start_ms": 40, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "기대", "start_ms": 230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "할게요!", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 06:56:44*
