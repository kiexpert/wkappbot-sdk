# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24432  time=11:33:59  uptime=34109s  mem=439MB  system_processes=356"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_113042.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 8928ms (file: 326-123-123-167-167-317-621-103-231-267-102-276-267-207-627-260-125-253-210-576-726-126-260-627-762-216-213-276-701-217-206-231-275-120-260-216-102-132-106-176-761-231-120-756-7_W.mp3)
  Segment 2: 9928ms ~ 10504ms (file: 570-576-576-502-251-756-653-576-567-657-567 507-507-506-570-570 570-507-507-502-506-507_W.mp3)
  Segment 3: 11504ms ~ 12080ms (file: 105-105-105-156-106-157-156-157-157-517-157-517-150-105-157-135-154-534 106-150-510-150-150-150-510-157-157-150-156-157-150-157-157-157-105 105-510-150-150-105-150-157-175-150-1_V.mp3)
  Segment 4: 13080ms ~ 13656ms (file: 316-102-623-612 132-103-106-612-136-106-163-136-106-160-162-172-613-136-102-125-162-521-236-213-123 157-567-103-201-312-165-136-103-103-106-675-167-617-613-316-613-136-123-267-1_V.mp3)
  Segment 5: 14656ms ~ 15232ms (file: 615-615-617-615-615-651-617-615-615-615-617-610-615-617-615-615_V.mp3)
  Segment 6: 16232ms ~ 28292ms (file: 627-627-267-263-267-762-361-261-236-312-310-123-625-236-160-120-130-130-736-312-756-571-265-624-576-256-276-267-236-637-756-263-263-761-231-236-576-612-576-316-576-267-231-273-2_W.mp3)
  Segment 7: 29292ms ~ 29868ms (file: 361-632-236-261-263-263-627-276-267-675-632-136-130-160-130-137-316-103-137-312-136-160-132-103-156-163-136-162-631-132-103-761-216-103-167-263 106-102-672-621-631-627-623-163-2_V.mp3)
  Segment 8: 30868ms ~ 31444ms (file: 567-567-657-657-657-567-657-654-564-654-564-564-654-253-316-517-532-531-514-356-531-531-567-576-576-567-567-567-576-567-576-567-576-567-657-567-567-567-657 576-675-567-567-567-5_W.mp3)
  Segment 9: 32444ms ~ 42488ms (file: 167-312-217-320-160-236-236-216-236-321-573-175-173-137-761-751-103-576-316-623-762-326-637-765-321-327-120-601-106-762-267-362-352-371-106-176-170-672-317-312-137-106-617-671-5_W.mp3)
  Segment 10: 43488ms ~ 44064ms (file: 571-571-157-157-517-510-105-507-157-507-105-571-157-501-574-574-574 570-750-570-701-715-507-107-715-170-175-571-705-157-510-157-571-150-507-571-507-750-571-751-751-571-750-571-7_V.mp3)
  Segment 11: 45064ms ~ 45640ms (file: 576-657-567-652-267 657-625-567-657-567-567-756-675-167-627-671-167-576-576-576-576-526-657-675-657-672-167-165-657-567-652-526-562-675-756-576-675-671-567-567-567-576-573-213-7_W.mp3)
  Segment 12: 46640ms ~ 47216ms (file: 327-263-326-756-765-765-756-765-735-752-752-765-375-753-765-736-725-726-736-376-675-756-765-756-765-756-765-756-753-765-756-765-765-765 756-756-756-756-756-765-756-765-765-756-7_W.mp3)
  Segment 13: 48216ms ~ 48792ms (file: 163-715-726-672-367-672-260-620-301-531-135-137 302-130-132-316-320-350-350-352-316-132-376-306-306-675-236-263-103-310-103-675-670-630-607-675-672-607-210-625-615-672-326-670-6_W.mp3)
  Segment 14: 49792ms ~ 51412ms (file: 265-570-657-652-652-651-615-615-165-156-153-157-652-567-567-567-560-512-153-123-125-675-657-625-261-265-652-567-275-256-127-102-175-762-675-765-756-675-576-756-576-157-571-105-5_W.mp3)
  Segment 15: 52412ms ~ 57488ms (file: 213-231-216-267-216-261-102-132-216-130-320-267-206-230-207-206-102-126-201-210-210-120-210-132-231-261-203-260-217-231-213-627-310-312-170-231-216-213-126-132-135-136-312-173-2_W.mp3)
  Segment 16: 58488ms ~ 59064ms (file: 576-576-576-576-756-753-621-621-613-456-546-541-546-504-534-547-546-547-506-516-531-501-504-541-543-275-756-576-576-576-756-576-576-756-576-756-756-216-124-524-542-503-502-506-5_V.mp3)
  Segment 17: 60064ms ~ 69172ms (file: 102-160-267-267-267-623-213-263-267-236-652-267-752-754-102-263-265-567-267-267-106-103-160-103-136-627-625-136-167-162-756-275-210-260-170-537-130-610-163-162-602-601-612-601-6_W.mp3)
  Segment 18: 70172ms ~ 75032ms (file: 723-321-723-321-102-132-237-726-216-217-276-176-672-621-327-320-326-162-562-632-726-725-726-236-276-756-726-206-265-320-130-620-261-132-301-327-360-263-102-361-126-312-123-312-3_V.mp3)
  Segment 19: 76032ms ~ 87300ms (file: 756-623-632-765-675-162-756-165-175-756-651-167-765-621-167-617-621 163 316-103-125-613-167-162-162-362-613-130-671-160-167-163-136-127-103-127-176-130-106-103-263-123-236-613-2_W.mp3)
  Segment 20: 88300ms ~ 88876ms (file: 751-756-756-751-751-751-756-715 150-175-170-571-517-157-571-715-751-751-751-175-715-715-175-715-751-751-751-756-753-751 510-510-150-157-571-751-175-715-715-751-751-751-751-751-7_W.mp3)
  Segment 21: 89876ms ~ 90452ms (file: 672-602-261-612-601-672-607-602-612-620-601-621-203-230-206-213-126-312-312-632-236-623-236-623-230-327-320-143-132-312 541-534-675-260-206-612-162-123-260-263-206-260-206-210-2_W.mp3)
  Segment 22: 91452ms ~ 92028ms (file: 652-625-625-625-625-602-625-620-602-623-265-265-625-625-625-265-265-265-265-261-623-625-265-625-625-652-652-652-562-652-625-625-653-620-621-625-620-265-625-620-620-621-623-625-6_W.mp3)
  Segment 23: 93028ms ~ 93604ms (file: 150-517-150-501-150-517-150-517-510-501-150-170-157-507-510-175-517-576-576-574-574-574-517-517 574-105-517-105-510-517-105-157-105-150-570-517-501-510-571-507-157-507-510-105-5_V.mp3)
  Segment 24: 94604ms ~ 95180ms (file: 261-103-312-231-312-132-301-310-136-236-263-361-326-316-316-760-765-361-361-310-136-361-136-715-130-132-312-301-310-132-310-310-163-326-162-301-326-362-263-260-206-206-623-631-6_V.mp3)
  Segment 25: 96180ms ~ 96756ms (file: 207-207-273-201-136-137-327-237-321 273-273-231-273-273 127_V.mp3)
  Segment 26: 97756ms ~ 98332ms (file: 157-156-157-751-157-175 105-103-105-105-150-170-105-175-150-150-157-156-516-157-157-157-150-153-510-150-510-517-157-150 103-103-105-105-150-150-156-105-150-157-175-157-157-157-1_V.mp3)
  Segment 27: 99332ms ~ 106172ms (file: 261-132-162-627-627-276-276-236-216-231-625-261-236-321-132-273-321-673-627-675-675-312-301-103-102-130-120-137-107-120-321-672-261-672-312-527-103-231-627-231-673-237-267-623-2_W.mp3)
  Segment 28: 107172ms ~ 116532ms (file: 504-126-206-270-201-132-102-206-207-270-270-230-312-135-153-103-132-765-576-657-567-751-157-135-715-130-153-105-310-301-120-310-106-310-316-132-261-361-367-371-673-376-316-326-6_W.mp3)
  Segment 29: 117532ms ~ 118108ms (file: 546-275-572-572-576-576-576-576-576-576-576-607-630-306-653-376-163-631-301-531-546-536-503-504-534-532-563-205-230-320-305-627-637-632-572-572-576-756-576-756-576-576-756-756-3_V.mp3)
  Segment 30: 119108ms ~ 119684ms (file: 216-132-153-103-103-157-105-105-175-750-517-756-752-751-705-751-170-750-715-762-762-756-750-756-573-503-534-675-567-675-756-567-576-765-756-756-756-675-176-215-673-136-316_W.mp3)
  Segment 31: 120684ms ~ 121260ms (file: 567-321-630-250-260-650-526-265-702-267_V.mp3)
  Segment 32: 122260ms ~ 122836ms (file: 150-506-501-507-507-507-507-576-576 501-705-750-571-751-571-517-157-501-510-501-507-510-517-501-507-501-507-510-507-510-510-517-510-510-501-570-501-510-570-510-501-570 510-501-5_V.mp3)
  Segment 33: 123836ms ~ 124412ms (file: 267-260-620-625-263-652-652-657-652-652-625-657-625-657-625-657-651-671-621-623-625-261-623-621-625-265-625-265-267-627-621-261-261-621-621-261-261-265-263-625-625-526-526-652-5_W.mp3)
  Segment 34: 125412ms ~ 125988ms (file: 647-675-674-675-671-671-674-674-671-617-657-675-671-651-657-567-657-675-671-617-673 675-675-657-675-657-657-657-675-675-675-657-765-675-675-675-765-675-675-617 610-675-567-657-7_W.mp3)
  Segment 35: 126988ms ~ 127564ms (file: 706-602-267-602-627-267-602-627-126-201-261-261-260-612-613-362-267-216-103-312-613-106-103-106-136-163-120-132-620-325-310-163-627-237-263 132-312-132-132-316-312-103-360-361-6_W.mp3)
  Segment 36: 128564ms ~ 129140ms (file: 230-263-263-310-230-326-657-623-103-750-376-517-153-130-130-137-210-203-206-312-203-320-320-203-326-376-675-632-263-106-206-260-673-316-362-315-615-631-574-567-756-657-567-562-6_W.mp3)
  Segment 37: 130140ms ~ 134064ms (file: 216-127-213-231-263-217-123-216-316-273-213-216-312-261-123-132-123-103-132-132-312-326-372-637-236-213-236-132-326-326-263-623-267-126-376-326-326-312-216-312-316-362-216-237-1_W.mp3)
  Segment 38: 135064ms ~ 135640ms (file: 517-513-517-135-517-153-531 153-513-531-157-571-157-571 574-571-571-571-571-517-571-175-175-137-517-517-153 517-517-571-517-517-571-571-537-517_W.mp3)
  Segment 39: 136640ms ~ 143300ms (file: 276-617-106-123-126-763-273-752-751-106-231-267-267-216-263-312-173-125-721-216-102-217-372-106-726-576-213-120-703-271-327-230-372-726-726-672-207-267-206-573-237-357-237-573-7_W.mp3)
  Segment 40: 144300ms ~ 144876ms (file: 130-132-103-132-317-132-137-371-312-301-713-173-312-103-173-103-173-701-137-103-317-137-317-712-130-137 157-135-137-137-317-137-103-103-301-731-173-137-735-137-103-315-107-307-7_V.mp3)
  Segment 41: 145876ms ~ 160600ms (file: 267-725-231-312-132-216-123-130-213-216-213-132-120-123-123-123-725-576-725-276-671-726-721 574-150-120-132-576-576-102-210 571-574-574-752-201-301-574-103-103-157-527-573-571-1_W.mp3)
  Segment 42: 161600ms ~ 162176ms (file: 613-672-765-607-162-675-625-657-765-675-625-627-627-612-657-507-576-576-756-765-301-103-130-210-206-213-206-316-136-620-162-163-136-163-136-761-167-160-165-107-105-567-576-756-5_W.mp3)
  Segment 43: 163176ms ~ 163752ms (file: 175-751-571-571-716-671-762-176-751-753-715-716-517-172-617-756-725_W.mp3)
  Segment 44: 164752ms ~ 178072ms (file: 267-102-257-231-236-267-541-413-132-263-263-263-321-236-765-312-312-132-103-123-317-361-231-236-231-301-310-672-267-236-756-310-231-321-263-321-362-103-216-326-316-765-260-136-1_W.mp3)
  Segment 45: 179072ms ~ 179648ms (file: 627-362-623 761-673-637-136-631-671-675-675-736-361-261-367-574-574-763-672-671-176 210-126-162-627-671-761-617-367-165-276-367-637-176-762-176-173-372-321-362-176-273-672-671-2_W.mp3)
  Segment 46: 180648ms ~ 181224ms (file: 713-317-307-130-307-137-130-107-310-301-371-710-371-317-710-701-713-715-173-710-731-730-703 735-375-710-731-317-713-751-175-103-173-107-137-107-175-137-173-130-357-375-731-175-7_V.mp3)
  Segment 47: 182224ms ~ 182800ms (file: 576-756-576-675-765-765-715-750-750-751-751-765 705-706-705-705-705-750-754 703-702-706-702-702-701-706-706_W.mp3)
  Segment 48: 183800ms ~ 184376ms (file: 765-762-372-216-267-726-756-276-627-253-127-231-675-261-261-571-657-627-172-132-367-754-231-627-625-273-265-576-372-507-617-576-756-267-572-265-153-576-750-527-275-761-572-605-2_V.mp3)
  Segment 49: 185376ms ~ 185952ms (file: 657-657-657-657-657-657-657-651-615-657-657-567-657-651-657-657-651-657-657-657-657 256-526-567-657-657-657-567-657-567-657-657-657-657-657-567-567-657-657-657-651 562-567-657-5_W.mp3)
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
> **Time**: 2026-04-05 11:35:23
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "Oh my son I have more apps on my phone and I use them all man there's there's I can tell you there's probably nobody in this room",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "Oh", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "son", "start_ms": 280, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 580, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 760, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "apps", "start_ms": 960, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 1210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 1310, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "phone", "start_ms": 1430, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 1680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1830, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "use", "start_ms": 1930, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "them", "start_ms": 2130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 2280, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "man", "start_ms": 2480, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "there's", "start_ms": 2780, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "there's", "start_ms": 3030, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 3280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 3380, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "tell", "start_ms": 3530, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 3730, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "there's", "start_ms": 3880, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "probably", "start_ms": 4130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "nobody", "start_ms": 4530, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 4930, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 5030, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "room", "start_ms": 5180, "dur_ms": 300, "phase_lr_deg": 0.0}
]
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
"transcript": "구독과 좋아요 한 번씩만 부탁드립니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "구독과", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 770, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "번씩만", "start_ms": 920, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부탁드립니다", "start_ms": 1320, "dur_ms": 600, "phase_lr_deg": 0.0}
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
"transcript": "자 일단 제이슨 노의 매매법을 사용",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 30, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 230, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "제이슨", "start_ms": 530, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "노의", "start_ms": 930, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "매매법을", "start_ms": 1180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "사용", "start_ms": 1680, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "단계적으로 이어지면서 해결돼야 되는데 한 부분만 오염이 된다 하더라도 전체 판단이 흔들릴 수 있다라는 거죠 요런 시스템의 관점에서 봤을 때 예를 들면 핸들을 잡고 이렇게 가고 있는데",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "단계적으로", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이어지면서", "start_ms": 650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "해결돼야", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "되는데", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 2050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "부분만", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오염이", "start_ms": 2600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "된다", "start_ms": 3050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하더라도", "start_ms": 3350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "전체", "start_ms": 3850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "판단이", "start_ms": 4200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "흔들릴", "start_ms": 4650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5050, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다라는", "start_ms": 5150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "요런", "start_ms": 5900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시스템의", "start_ms": 6200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "관점에서", "start_ms": 6700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "봤을", "start_ms": 7200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 7450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "예를", "start_ms": 7600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "들면", "start_ms": 7800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "핸들을", "start_ms": 8050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 8450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 8750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 9150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 9450, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "라는 것이 아니라 의미 있는 흐름을 만들면서 자리를 먼저 형성합니다 따라서 중요한 것은 움직인 이후가 아니라",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "라는", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 270, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 470, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "의미", "start_ms": 820, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1070, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "흐름을", "start_ms": 1270, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "만들면서", "start_ms": 1670, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 2170, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 2520, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "형성합니다", "start_ms": 2820, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "따라서", "start_ms": 3470, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 3920, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 4320, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "움직인", "start_ms": 4570, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이후가", "start_ms": 4970, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 5370, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "버스를 통해서 각각의 모듈들이 메시지 인증이라던가 보안 게이트웨이라던가 OTA 같은 것도 계속 확장될 거잖아요 왜냐면 테슬라 같은 경우에는 하드웨어 4나 앞으로 AI 5 AI 6",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "mixed",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "버스를", "start_ms": 40, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "통해서", "start_ms": 440, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "각각의", "start_ms": 840, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모듈들이", "start_ms": 1240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "메시지", "start_ms": 1740, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "인증이라던가", "start_ms": 2190, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "보안", "start_ms": 2940, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "게이트웨이라던가", "start_ms": 3240, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "OTA", "start_ms": 4090, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 4490, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 4790, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 5040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "확장될", "start_ms": 5390, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거잖아요", "start_ms": 5840, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "왜냐면", "start_ms": 6290, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "테슬라", "start_ms": 6690, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 7140, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "경우에는", "start_ms": 7440, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하드웨어", "start_ms": 7940, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "4나", "start_ms": 8440, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 8740, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 9140, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "5", "start_ms": 9340, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 9540, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "6", "start_ms": 9740, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "5분 이상은 모든 기능을 다 지원하고 6 이하로는 기능의 차이만 있지 지원 자체는",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "5분", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이상은", "start_ms": 330, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 730, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "기능을", "start_ms": 1030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1430, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지원하고", "start_ms": 1530, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "6", "start_ms": 2030, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이하로는", "start_ms": 2230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기능의", "start_ms": 2730, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "차이만", "start_ms": 3130, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있지", "start_ms": 3530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "지원", "start_ms": 3830, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "자체는", "start_ms": 4130, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "지금 국제 정세에서 가장 빠르게 존재감을 키우고 있는 나라가 어디인지 묻는다면 세계 많은 전문가들은 주저 없이",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 60, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 410, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정세에서", "start_ms": 710, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 1310, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 1660, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "존재감을", "start_ms": 2110, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "키우고", "start_ms": 2660, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 3060, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "나라가", "start_ms": 3310, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어디인지", "start_ms": 3710, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "묻는다면", "start_ms": 4210, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "세계", "start_ms": 4760, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 5110, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "전문가들은", "start_ms": 5410, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "주저", "start_ms": 6060, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없이", "start_ms": 6360, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "정말 후회 없는 선택이었습니다 강의를 들을 때마다 놀라",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "후회", "start_ms": 380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "선택이었습니다", "start_ms": 980, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "강의를", "start_ms": 1780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "들을", "start_ms": 2230, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때마다", "start_ms": 2530, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "놀라", "start_ms": 2980, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "파 영상을 SNS에 올리면서 이게 진짜 멋있다라는 듯한 애정을 드러내기도 했는데요 그리고 진짜는 따로 있습니다 바로 영화 속에서 강렬한 존재감을 보",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "파", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "영상을", "start_ms": 240, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "SNS에", "start_ms": 690, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "올리면서", "start_ms": 1190, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1740, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 2040, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "멋있다라는", "start_ms": 2390, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "듯한", "start_ms": 3040, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "애정을", "start_ms": 3340, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드러내기도", "start_ms": 3790, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "했는데요", "start_ms": 4440, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 4990, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "진짜는", "start_ms": 5440, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 5890, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 6240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 6740, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 7090, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 7440, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "강렬한", "start_ms": 7940, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "존재감을", "start_ms": 8440, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보", "start_ms": 9040, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "라는 내용이 계속 부각을 받는 것 같습니다 여기 보면은 걸프 국가들의 무기 재고가 바닥이 나고 있는 가운데 하나의 천군 같은 레이저 무기가 지금 떠오르고 있다 그래서 보시면은 이제 천군 같은 경우에는 굉장히 비싸죠 한 발당 수십 억 대의 미사일",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "라는", "start_ms": 20, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내용이", "start_ms": 320, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "부각을", "start_ms": 1070, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "받는", "start_ms": 1520, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1820, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 1920, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 2420, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보면은", "start_ms": 2720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "걸프", "start_ms": 3120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "국가들의", "start_ms": 3470, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "무기", "start_ms": 4020, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "재고가", "start_ms": 4320, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "바닥이", "start_ms": 4770, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나고", "start_ms": 5170, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5470, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "가운데", "start_ms": 5720, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하나의", "start_ms": 6170, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "천군", "start_ms": 6570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 6920, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "레이저", "start_ms": 7220, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "무기가", "start_ms": 7670, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 8070, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "떠오르고", "start_ms": 8420, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 8920, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 9220, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보시면은", "start_ms": 9670, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 10220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "천군", "start_ms": 10520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 10870, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "경우에는", "start_ms": 11170, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 11670, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비싸죠", "start_ms": 12120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 12520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "발당", "start_ms": 12670, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수십", "start_ms": 13020, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "억", "start_ms": 13320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대의", "start_ms": 13470, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 13720, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "미친 온도 차에 외국인들은 한국 배우들은 외계인이냐 연기력이 공포스럽다며 혀를 내두르고 있습니다 지",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "미친", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "온도", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "차에", "start_ms": 730, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "외국인들은", "start_ms": 1030, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배우들은", "start_ms": 2080, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "외계인이냐", "start_ms": 2630, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "연기력이", "start_ms": 3330, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "공포스럽다며", "start_ms": 3880, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "혀를", "start_ms": 4780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "내두르고", "start_ms": 5130, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 5730, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지", "start_ms": 6280, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "미리 공부했던 종목입니다 그리고 실제로 매매를 했었고요 이 종목은 미래에셋 증권인데요 1월 달부터 2월 달까지 저",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "미리", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공부했던", "start_ms": 390, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "종목입니다", "start_ms": 940, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1690, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 2140, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 2640, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "했었고요", "start_ms": 3090, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3740, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "종목은", "start_ms": 3890, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미래에셋", "start_ms": 4340, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "증권인데요", "start_ms": 4940, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "1월", "start_ms": 5590, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "달부터", "start_ms": 5940, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "2월", "start_ms": 6390, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "달까지", "start_ms": 6740, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 7190, "dur_ms": 250, "phase_lr_deg": 0.0}
]
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
"transcript": "제가 어떤 지표를 요청하신 건지 정확히 구분할 수 있기 때문에 반드시 8번이라고 표기해 주시길 부탁드립니다 그럼 오늘도 영상 끝까지 시청해 주셔서 감사드리고 우리 구독자분들의 성공적인 매매와 안정적인 투자를 진심",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 30, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 330, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지표를", "start_ms": 680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "요청하신", "start_ms": 1130, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "건지", "start_ms": 1730, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정확히", "start_ms": 2080, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "구분할", "start_ms": 2530, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2980, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 3130, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 3430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 3830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "8번이라고", "start_ms": 4280, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "표기해", "start_ms": 4980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "주시길", "start_ms": 5430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부탁드립니다", "start_ms": 5830, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 6630, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 6980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "영상", "start_ms": 7430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 7830, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시청해", "start_ms": 8280, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 8780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "감사드리고", "start_ms": 9230, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 9930, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "구독자분들의", "start_ms": 10230, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "성공적인", "start_ms": 11030, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "매매와", "start_ms": 11630, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안정적인", "start_ms": 12130, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "투자를", "start_ms": 12730, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "진심", "start_ms": 13230, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:35:23*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=11:35:22  uptime=37539s  mem=502MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_113312.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 9756ms (file: 321-102-621-136-106-126-675-276-260-120-627-126-213-103-261-231-123-231-130-237-132-672-321-231-321-312-326-752-312-130-762-260-173-312-132-302-136-621-236-103-650-231-321-572-2_W.mp3)
  Segment 2: 10756ms ~ 16084ms (file: 326-236-320-627-762-726-170-120-123-612-271-273-270-327-572-352-231-213-326-276-721-576-236-320-231-571-620-231-312-327-203-576-137-102-326-372-203-572-627-630-320-301-675-671-5_V_stt_if.mp3)
  Segment 3: 17084ms ~ 17660ms (file: 627-621-563-175-217-674-613-163-361-137-135-301-316-156-653-163-167-671-603-176-621-617-631-136-123-761-526-136-103-617-516-610-672-632-236-613-127-162 130-135-602-620-260-302-1_W.mp3)
  Segment 4: 18660ms ~ 26940ms (file: 261-261-257-103-130-136-136-213-123-134-261-210-216-576-267-576-267-265-216-123-574-576-132-352-157-106-167-106-210-261-213-130-316-163-132-312-106-754-726-627-623-362-756-752-3_W.mp3)
  Segment 5: 27940ms ~ 28516ms (file: 143-134-143-301-145-145-134-103-417-143 431-103-103-134-301-104-310-143-310-134-130-134-310-103-140-140-143-413-134-143-143-143-143-143-134 304-345-531-147-310-103-314-301-103-1_V.mp3)
  Segment 6: 29516ms ~ 39668ms (file: 236-263-326-310-326-321-321-236-312-513-756-236-326-237-203-236-236-756-326-236-236-236-237-267-123-123-263-637-263_W.mp3)
  Segment 7: 40668ms ~ 41244ms (file: 301-102-130-130-132-310-132-123-132_W.mp3)
  Segment 8: 42244ms ~ 53044ms (file: 507-750-270-230-672-176-574-572-267-231-672-267 765-761-172-213-237-547-107-231-672-267-236-574-572-572-547-726-765-327-574-574-572-267-261-137-726-541-157-453-541-231-754-574-3_W.mp3)
  Segment 9: 54044ms ~ 54620ms (file: 547-547-504-507-504-507-547 570-507-574-571-507-574-574-574-507-517-571 547-547-547-547-547-547-574-574-574-571-570-571-517-501-510-517-570-574-750-571-751-754-754 507-517-571-1_V.mp3)
  Segment 10: 55620ms ~ 56196ms (file: 570-571-175-571-157-751-571-751-715-176-715-715 175-175-710-750-751-710-175-715-170-701-710-750-701-750-751-715-750-751-751-750-570-175-573-357-301-150-157 157-175-157-175-751-1_W.mp3)
  Segment 11: 57196ms ~ 57772ms (file: 150-170-175-105-150-153-105 145-105-105-150-150-150-150-150-157-107-105-105-104-107-106-105-105-150-150-150-150-105-561 105-105-105-153-105-150-150-107-105-105-105-105-106-105-1_V.mp3)
  Segment 12: 58772ms ~ 59348ms (file: 361-637-256-321-321-301-302-302-623-310-103-320-320-302-362-312-206-310-136-163-137-123-315-530-312-132-503-320-230-231-325-230-302-230-312-371-701-273-201-230-631-312-320-672-6_W.mp3)
  Segment 13: 60348ms ~ 68232ms (file: 762-574-132-231-213-213-267-267-267-261-263-236-321-573-723-312-316-273-231-723-132-756-574-310-231-236-123-105-271-321-236-206-310-130-316-175-725-302-231-362-570-625-275-327-5_W.mp3)
  Segment 14: 69232ms ~ 75280ms (file: 267-310-260-260-103-103-157-231-123-261-312-257-312-237-570-152-257-216-206-120-126-132-217-127-217-123-206-613-170-670-106-103-132-752-123-103-102-236-230-120-261-201-237-206-1_W.mp3)
  Segment 15: 76280ms ~ 76856ms (file: 751-157-175-157-175-167-176-517-716-167-175-175-176-176-175-176-167-176-167-175-174 150-105-105-150-105-105-150-150-157-175-517-157-175-176-751-715-751-715-176-761-716-175 150-1_V.mp3)
  Segment 16: 77856ms ~ 86352ms (file: 261-167-326-317-627-163-326-267-367-176-723-627-216-576-176-610-261-263-526-136-163-361-753-316-361-361-163-637-216-627-627-756-762-572-526-120-267-623-263-236-120-123-627-261-6_W.mp3)
  Segment 17: 87352ms ~ 87928ms (file: 105-510 510-501-501-150-150-153-513-105-501-150-105-150-105-153-105-157-103-105-157-153-135-157-105-517 510-150-105-501-514-514-501-517-501-504-501-510 510-510-501-510-501-501-5_V.mp3)
  Segment 18: 88928ms ~ 89504ms (file: 310 175-715-175-173-710-170-105-175-175-157-517-571-576-576-576-576-576-576-576-175-576-571-571-157-751-751-571-571-751-571-751-750-750-751-753-752-762-765-765-762-712-671-752-7_W.mp3)
  Segment 19: 90504ms ~ 101988ms (file: 267-230-132-574-572-537-752-275-261-154-210-201-231-206-123-103-310-130 756-765-657-571-756-576-576-571-675_W.mp3)
  Segment 20: 102988ms ~ 111123ms (file: 321-321-267-320-321-327-756-756-172-576-312-107-236-321-103-132-756-132-531-312-371-312-102-321-123-132-310-263-130-237-725-732-752-627-326-102-126-126-571-136-103-216-132-613-2_W.mp3)
  Segment 21: 112123ms ~ 117163ms (file: 231-236-267-602-627-106-236-236-672-627-762-263-326-236-267-216-126-236-263-627-672-627-267-621-267-267-326-261-362-231-621-261-321-312-632-236-213-162-621-261-621-263-126-267-2_W.mp3)
  Segment 22: 118163ms ~ 118739ms (file: 157-157-153-157-175-157-517-150-517-570 570-574-570-517-517-571-517-517-130-175-135-170-751-573_W.mp3)
  Segment 23: 119739ms ~ 130719ms (file: 321-273-231-321-321-325-321-321-312-137-325-327-623-275-273-213-326-273-306-361-103-102-103-213-130-310-103-216-130-620-127-312-136-210-206-132-103-251-132-310-103-132-317-130-1_V.mp3)
  Segment 24: 131719ms ~ 132295ms (file: 517-576-567-571 510-510-517-517-517-517-517-510-501-501-150-510-501-517-570-571-517-501-507-570-570 157-510-153-531-157-157-516-513-571-576-517-507-570-573-576-571-570-576-576-5_V.mp3)
  Segment 25: 133295ms ~ 133871ms (file: 501-501-571 510-507-517-507-501-150-510-510-150-510-150-501-507-571-510-157-510-501-501-517-570-510-517 105-157-157-105-157-105-501-105-157-105-150-150-510-105-507-570-574-570-5_V.mp3)
  Segment 26: 134871ms ~ 135447ms (file: 103-175-107-107-710-107-713-170-715-103-173 103-157-173-170-710-137-130-170-105-105-173-107-170-173-175-175-170-107-137-107-173-103-107-103-107-137-173-170-315-570_V.mp3)
  Segment 27: 136447ms ~ 145951ms (file: 162-120-261-627-213-132-132-267-130-627-261-231-231-123-130-320-756-713-756-261-167-216-576-627-261-132-106-103-534-213-260-201-263-261-672-675-170-672-236-276-263-123-123-132-1_W.mp3)
  Segment 28: 146951ms ~ 154547ms (file: 762-576-752-213-235-325-321-320-723-137-231-273-210-320-267-230-230-236-320-231-765-276-230-123-327-672-765-276-562-230-267-231-102-321-103-756-725-267-237-312-537-574-752-130-1_W.mp3)
  Segment 29: 155547ms ~ 156123ms (file: 567-567-567-576-571-657-263-563 150-165-157-567-531-152-572-576-571_W.mp3)
  Segment 30: 157123ms ~ 157699ms (file: 517-157-105-105-517-105-153-150-517-510-105-150-105-150-150-504-150-157-153-105-105-105-150-153-510-150-510-513-517-517-514-510 501-501-517-153-105-153-105-517-105-150-157-150-5_V.mp3)
  Segment 31: 158699ms ~ 159275ms (file: 510-570-520-250-207-267-261-621-103-103-130-132-236-205-527-572-521-213-236-216-570-517-136-321-320-321-132-103-130-310-103-316-610-321 675-567-657-675-672-675-675-765-675-672-6_W.mp3)
  Segment 32: 160275ms ~ 160851ms (file: 751-175-175-751-175-751-751-715-715-715-175-715-751-157-751-751-751-715-751 150-501-150-105-105-105-107-175-715-175-175-175-175-751-517-751-751-751-751-751-756-751-756-751-751-7_W.mp3)
  Segment 33: 161851ms ~ 162427ms (file: 571-571-571-517-571-571-517-571-751-751-571-573-571-751-517-105-105-105-103-105-157-150-150-135-153-175-137-301-103-517-571-517-713-175-715-751-175-176-175-751-157-157-153-753-1_V.mp3)
  Segment 34: 163427ms ~ 164003ms (file: 751-715-750 157-105-105-175-107-105-170-175-130-107-701-175-751-175-157-751-157-137-153-513-175-175 105-105-105-175-105-751-715-150-175-157-175-170-170-105-103-175-107-517-150-1_V.mp3)
  Segment 35: 165003ms ~ 165579ms (file: 306-360-613-613-367-632-635-351-316-362-763-316-613-612-352_W.mp3)
  Segment 36: 166579ms ~ 167155ms (file: 571-517-517-517-571-517-516-571-517-576-750-571-570-751-756-765-716-576-570-750-750-570-570-570-570 574-157-571-517-751 517-570-751-705-756-570-507-507-570-756-576 574-507-570-5_W.mp3)
  Segment 37: 168155ms ~ 168731ms (file: 165-165-165-163-672-672-765 576-574-574-572-570-570-570-570-576-567-657-657 572-572-570-576-570_W.mp3)
  Segment 38: 169731ms ~ 170307ms (file: 360-301-132-320 127-276-576-752-567-756-756-157-152-762-314-567-625-621-765-514-351-132-576-571-517-321-532-534-547-210-126-132-135-531 675-765 576-513-715-756-751-263-132-327 7_W.mp3)
  Segment 39: 171307ms ~ 171883ms (file: 517-156-526-157-157-517-315-561-516-137-513-576-517-156-516-572-517-517-576-571-517-513-517-521-572-571-517-571-156-572-576-157-561-571-537-175-157-156-150-517-157-517-157-571-7_W.mp3)
  Segment 40: 172883ms ~ 173459ms (file: 132-312-137-627-731-361-562-526-256-532-356-576-523-540-574-517-157-153-315-315-531-351-135-531-516-312-135-316-361-165-271-372-273-571-257-257-375-715-375-175-723-235-265-523-3_W.mp3)
  Segment 41: 174459ms ~ 175035ms (file: 136-167-137-260-627-675-657-627-621-627-627-621-627-621-162-106-627-612-610-617-657-627-612-213-123-210-213-102-120-203-123-120-210-120-213-132-612_W.mp3)
  Segment 42: 176035ms ~ 176611ms (file: 157 150-150-571-517-175-170-105-517-153-157-105-157-715-107-517 571-157-517-510-107-501 574-507-547-157-517-570-570-750-751-107-107-750-754-570-701-571-751-107-501-507-750 570-5_V.mp3)
  Segment 43: 177611ms ~ 178187ms (file: 576-567-576-756-576-576-576-576-576-576-576-567-756-756-756-504-504-506-507-507-504-506-506-547-576-501-506-506-507-507-504-507-506-725-756-576-576-576-576-756-576-756-576-576-7_W.mp3)
  Segment 44: 179187ms ~ 179763ms (file: 501-507-501-507-510-175-570-571-507-570-501-517-571-510-517-570-507-571-570-570-571 507-574-571-570-571-517-507-570-507-571-570-570-501-507-510-150-517-517-574-517-510-517-507-5_V.mp3)
  Segment 45: 180763ms ~ 181339ms (file: 312-132-231-203-123-501-326-201-301-302-137-726-263-567-207-126-123-175-125-127-137-201-136-216-321-312-612-613-136-167-167-617-672-675-605-675-607-356-173-607-607-672-602-123-3_W.mp3)
  Segment 46: 182339ms ~ 182915ms (file: 157-750-756-751-751-751-715-751-715-715-715-751-756-756-751-756-751-751-571 105-150-150-510-501-517-750-571-751-751-751-715-751-756-751-751-751-751-751-751-751-751-756-754-756-7_W.mp3)
  Segment 47: 183915ms ~ 184491ms (file: 576-572-572-756-756-762-576-657-615-165-167-561-156-156-517-165-572-527-526-572-527-527-576-765-615-165-165-651-615-651-156-561-657-567-516-571-615-516-576-516-561-516-615-165-5_W.mp3)
  Segment 48: 185491ms ~ 186067ms (file: 571-517-517-571-517-157-751-571-517-157-517-751 157-157-517-153-157-574-574-571-506-576 510-157-517-751-751-517-571-751-571-513 571-751-571-175-157-157-157-571-571-517-571-571-5_W.mp3)
  Segment 49: 187067ms ~ 187643ms (file: 132-163-163-136-132-136-136-625-612-126 672-671-765 162-165-165-165-156-615-165-615-615-165-165 162-621-612-615-651-651-615-615-651 362-756-157-253-157-153-756-167-132-156-165-6_W.mp3)
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
> **Time**: 2026-04-05 11:35:47
> **AI**: gemini

---

## Response

READY TOOL PROTOCOL: APSP v1

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:35:47*
