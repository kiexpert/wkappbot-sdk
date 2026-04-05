# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=11:38:15  uptime=27231s  mem=447MB  system_processes=358"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_113333.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 137-136-701-326-213-236-230-136-306-163 672-670-623-610-301-103-316-701-763-310-103-310-312-754-753-713-317-137-317-301-231-321-320-213-206-230-231-261-623-163-627-326-162-627-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 657-657-657-657-657-657-651-657-651-651-651-657-651-615-657-615 542-162-615-652-615-651-657-657-657-657-657-657-657-657-657-657-657-657-651-265 657-657-657-657-657-675-657-657-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 756-175-576-756-135-175-751-312-263-126-302-213-356-165-672-257-725-237-753-365-751-375-271-236-213-327-756-237-752-275-765-273-316-236-502-257 675-207-230-231-627-263-540-517-2_V.mp3)
  Segment 4: 4728ms ~ 10452ms (file: 735-137-762-726-312-136-265-632-625-267-231-752-127-126-321-236-216-726-326-236-763-263-236-375-237-326-360-231-326-267-372-762-217-237-276-276-267-726-762-162-267-263-613-637-6_W.mp3)
  Segment 5: 11452ms ~ 17572ms (file: 567-312-231-261-136-756-160-321-135-571-130-365-326-316-326-163-162-130-175-651-716-367-735-175-312-613-165-126-163-132-132-135-136-163-136-123-217-126-136-612-127-167-163-316-7_W.mp3)
  Segment 6: 18572ms ~ 19148ms (file: 106-617-517-136-103-152-317-103-136-103-165-361-617-136-103-576-165-136-671-561 103-103-130-103-615-613-132-615-162-136-167-132-103-576-136-103-130-106-106-136-132-132-137-136-1_V.mp3)
  Segment 7: 20148ms ~ 20724ms (file: 265-602-650-561-650-650-605-675-675-657-657-657-560-562-652-657-765-756-175-156-756-675-576-567-567-675-657-567-657-567-576-756-651-675-752-651-561-567-561-165-576-165-135-165-5_W.mp3)
  Segment 8: 21724ms ~ 22300ms (file: 673-367-627-506-657-627-263-207-120-102-106-302-132-130-305-267-672-657-762-675-765-672-675-627-672-672-257-260-302-132-621-206-321-312-657-576-765-267-102-167-127-236-207-167-1_W.mp3)
  Segment 9: 23300ms ~ 23876ms (file: 657-765-657-576-576-756-756-576-756-576-756-756-576-753-756-576-576-576-576-765-756-756-752-572-752-572-573-574-756-675-756-567-756-576-576-567-765-756-157-516-572-562-267-657-6_W.mp3)
  Segment 10: 24876ms ~ 25452ms (file: 134-135-315-451-405-547-154-104-103-104-103-105-134-145-154-105-154-103-105-103 154-534-517-571-415-310-143-103-130-105-140-134-104-451-415-104-314-134-103-135-103-104-143-140-1_V.mp3)
  Segment 11: 26452ms ~ 27028ms (file: 136-165-705-675-670-671-163-103-136-163-362-302-236-326-673-106-162-106-163-316-326-623-602-623-623-672-206-360-106-103-136-103-312 126-136-132_V.mp3)
  Segment 12: 28028ms ~ 39872ms (file: 273-321-320-301-310-267-312-372-167-231-237-231-231-726-130-160-162-627-136-136-136-367-160-574-267-137-761-620-726-756-657-376-621-102-132-753-637-573-573-613-136-367-361-367-5_W.mp3)
  Segment 13: 40872ms ~ 41448ms (file: 312-261-263-276-302-154-127-576-573-572-572-574-576-507-321-260-276-627-627-265-253-231-276-276-231-627-267-267-275-275-703-752-753-752-753-576-752-307-752-753-712-701-107-173-5_W.mp3)
  Segment 14: 42448ms ~ 51088ms (file: 312-132-765-671-627-326-320-267-276-103-267-231-327-103-137-132-765-167-236-326-231-107-120-750-307-130-715-317-165-136-136-312-635-176-527-756-236-312-620-576-175-261-215-756-7_W.mp3)
  Segment 15: 52088ms ~ 52664ms (file: 163-251-512-265-103-132-312-135-103-371-625-673-507-573-167-176-710-316-163-312-361-632-163-103-132-312-316-132-153-132-143 163 632-167-617-126 316-621-136-167-174-732-623-231-3_V.mp3)
  Segment 16: 53664ms ~ 54240ms (file: 671-671-601-607-601 207-620-621-627-625-657-621-672-617-617-672-672-627-672-617-671-607-601 725-275-267-675-675-672-627-627-267-627-672-627-672-671-672-675-671-672-675-617-671-6_W.mp3)
  Segment 17: 55240ms ~ 55816ms (file: 136-103-152-132-571-152-103-106-106-132-126-102-312-103-631-163-163-154-360-756-607-671-762-672-602-671-672-627-362-621-602-675-563-136-132-130-132-523-153-456-126-106-136-137-1_V.mp3)
  Segment 18: 56816ms ~ 57392ms (file: 715-175-715-175-175-715-175-175-715-175-175 175-571-751-517-150-153-150-157-715-715-751-751-715-751-715-175-715-715-175-751-715-715-751-751-576 150-135-510-150-105-105-150-175-1_W.mp3)
  Segment 19: 58392ms ~ 58968ms (file: 546-275-572-572-576-576-576-576-576-576-576-607-630-306-653-376-163-631-301-531-546-536-503-504-534-532-563-205-230-320-305-627-637-632-572-572-576-756-576-756-576-576-756-756-3_V.mp3)
  Segment 20: 59968ms ~ 70768ms (file: 321-167-517-574-102-372-732-732-123-327-236-627-574-547-276-276-267-672-326-716-167-267-267-267-362-103-230-325-237-276-367-573-130-376-367-172-327-230-123-103-671-760-160-167-1_W.mp3)
  Segment 21: 71768ms ~ 72344ms (file: 576-567-576-657-567-567-567-756-576-567-756-756-756-167-671-617-576-657-567-576-576-756-576-756-627-265-261-162-126-162-126-621-167-216-162-162-162-162-675-675-756-576-675-675-7_W.mp3)
  Segment 22: 73344ms ~ 73920ms (file: 237-137-321-172-123-705-762-271-750-762-261-652-261-267-652-567-173-215-527-671-672-215-513-753-762-574-251-215-725-507-152-257-236-765-672-231-705-627 321-765-317-316-361-316-1_V.mp3)
  Segment 23: 74920ms ~ 75496ms (file: 576-325-132-216-236-621-261-231-236-365-126-126-163-135-126-136-156-236-326-326-123-236-316-132-215-216-263-126-126-132-126-326-365-136-362-362-326-362-362-362-621-623-263-621-6_W.mp3)
  Segment 24: 76496ms ~ 77072ms (file: 751-753-571-507-570 157-105-107-170-170-170-170-170-170-175-715-751-756-517-756-576-765-756-715-571-751 105-750-157-710-107-175-170-175-170-710-705-170-170-107-170-751-715-751-5_W.mp3)
  Segment 25: 78072ms ~ 78648ms (file: 175-150-157-175-157-157-571-175 570-571-157-571-517-571-715-571-175-173-751-135-130-571-135-173-573-175-157-715-157-157-517-157-517-571 571-571-571-571-751-571-157-175-751-152-1_V.mp3)
  Segment 26: 79648ms ~ 80224ms (file: 371-316-573-317-136-376-631-136-317-137_W.mp3)
  Segment 27: 81224ms ~ 81800ms (file: 507-517-157-150-157-570-570-517-517-517-517-571-574-517-571-517-157-517-517 574-157-175-157-571-715-175-157 576-517-157-157-751-175-357-170-317-751-157-157-175-157-751-571-751-5_W.mp3)
  Segment 28: 82800ms ~ 88200ms (file: 657-625-651-657-217-315-153-531-102-237-237-271-120-312-130-132-657-167-106-160-106-167-163-574-125-217-271-762-236-103-237-260-276-276-217-137-756-203-201-520-152-210-103-132-2_W.mp3)
  Segment 29: 89200ms ~ 89776ms (file: 152-126-165-106-105-130-136-163-137-632-306-361-103-103-135-160-175-103-103-103-126-103-103-103-103-103-153-132-130-123-316-167-136_V.mp3)
  Segment 30: 90776ms ~ 97472ms (file: 130-123-137-653-572-675-213-206-613 132-132-132-123-547-267-216-126-203-260-103-201-260-620-126-213-210-210-102-206-267-206-762-236-260-762-206-203-123-201-761-216-201-276-621-7_V.mp3)
  Segment 31: 98472ms ~ 99048ms (file: 167-126-613 126 621-602-163 126-517_V.mp3)
  Segment 32: 100048ms ~ 100624ms (file: 102-103 610-610-160-610-610-610-612-610-162-612-612-621-612-610-610-612-610-601-631-510-512-513-523-531-567-256-256-512-521-521-521-165-165-156-156-513-125-135-152-315-135-315-1_W.mp3)
  Segment 33: 101624ms ~ 106520ms (file: 123-321-761-637-371-731-716-137-267-137-170-715-501-735-317-137-153-571-205-256-263-127-375-307-750-573-570-325-326-316-753-765-576-735-376-537-531-165-375-370-375-356-537-175-2_W.mp3)
  Segment 34: 107520ms ~ 108096ms (file: 576-657-567-652-267 657-625-567-657-567-567-756-675-167-627-671-167-576-576-576-576-526-657-675-657-672-167-165-657-567-652-526-562-675-756-576-675-671-567-567-567-576-573-213-7_W.mp3)
  Segment 35: 109096ms ~ 116188ms (file: 261-103-126-310-526-120-267-260-276-267-627-205-276-547-137-572-574-607-572-132-756-123-213-752-157-265-123-627-570-132-627-257-267-572-567-517-570-105-570-572-215-301-750-615-2_V.mp3)
  Segment 36: 117188ms ~ 117764ms (file: 613-675-657-675-652-765-671-675-617-625-267-652-576-576-765-765-671-321-631-316-317-136-736-675-671-163-316-136-326-320-623-625-352-235-265-216-236-216-216-213-261-263-263-502-5_W.mp3)
  Segment 37: 118764ms ~ 119340ms (file: 574-547-501-157-175-517-504-571-537-157-175 571-571-751-507-570-750 571-750-571-175-501-105-105-157-571-175-157-571-571-571-571-571-576-765-756-735-576-671-715-150-157-175-107-1_W.mp3)
  Segment 38: 120340ms ~ 130744ms (file: 316-263-170-326-326-267-762-236-162-326-623-316-263-623-267-765-623-623-267-267-260-236-260-302-376-267-237-103-207-276-263-263-326-362-362-362-236-260-765-316-102-207-326-236-7_W.mp3)
  Segment 39: 131744ms ~ 132320ms (file: 576-756-756 372-251-524-514-542-542-542-534 572-576-576-576-576-576-576-576-756-546-542-513-536-561-547-547-546-541-542-546-562-516-504-542 572-572-576-754-576-576-576-576-756-7_V.mp3)
  Segment 40: 133320ms ~ 133896ms (file: 106-136-315-675-675-136-136-631-103-163-263_V.mp3)
  Segment 41: 134896ms ~ 135472ms (file: 105-157-705-701-751-170-751-751-105-107-751-750-570-750-175-507-501-570-175-750 750-570-570-751-107-157-517-105-570-574-570-751-705-715-574-751-751-750-175-175-571-751 574-570-5_W.mp3)
  Segment 42: 136472ms ~ 143672ms (file: 312-267-627-175-201-726-102-132-756-130-213-213-312-231-261-231-312-321-123-276-231-261-267-261-621-163-312-132-627-263-236-312-672-627-261-761-176-130-672-260-103-321-230-201-7_W.mp3)
  Segment 43: 144672ms ~ 145248ms (file: 310-130-106-671-163-137-106-716-310-301-107-135-170-137-317-137-317-317-137 107-753-703-370-753-715-735-317-176-136-317-167-106-173-137-137-172-301-130-173-301-317-301-310-310-1_V.mp3)
  Segment 44: 146248ms ~ 146824ms (file: 276-206-270-276-267-265-275-572-572-153-561-156-156 507-507-572-752-527-570-570-752-572-256-267-257-275-256-351-152-153-516-156-165-156-516 627-572-570-750-572-705-502-527-750-2_V.mp3)
  Segment 45: 147824ms ~ 148400ms (file: 723-507-502-321-213-163-312-175-357-132-631-635-561-615-610-672-627-612-675-657-657-756-672-670-627 163-163_W.mp3)
  Segment 46: 149400ms ~ 149976ms (file: 627-673-263-326-261-263-621-265-263-265-261-265-265-263-265 675-632-213-123-670-672-627-132-621-261-613-316-523-125-136-513-312-136-321-675-561-672-673-627-672-672-632-163-310-2_W.mp3)
  Segment 47: 150976ms ~ 160696ms (file: 753-627-621-267-267-263-271-321-230-107-106-237-672-263-267-102-167-236-236-753-756-103-130-672-675-130-134-267-267-260-260-273-276-756-765-107-160-267-263-260-213-321-317-312-1_W.mp3)
  Segment 48: 161696ms ~ 162272ms (file: 576-567-657-567-516-156-516-156-152-156-157-517-157-512-512-153-153-156-156-156-153-156-156-516-571-576-567-157-756-675-657-157 106-165-615-610-165-615-615-651-576-576-517-516-5_W.mp3)
  Segment 49: 163272ms ~ 172236ms (file: 263-263-632-261-756-517-570-576-261-573-672-132-312-216-756-216-213-261-263-756-260-312-617-573-261-765-132-162-231-312-765-231-261-572-132-673-526-213-567-156-632-756-123-263-2_W.mp3)
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
> **Time**: 2026-04-05 11:39:53
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "I have more apps on my phone and I use them all man there's there's I can tell you there's probably nobody in this room",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 65, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 130, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "apps", "start_ms": 205, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 290, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 335, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "phone", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 485, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 540, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "use", "start_ms": 585, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "them", "start_ms": 650, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "all", "start_ms": 715, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "man", "start_ms": 800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "there's", "start_ms": 905, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "there's", "start_ms": 990, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1075, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 1130, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "tell", "start_ms": 1195, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 1280, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "there's", "start_ms": 1345, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "probably", "start_ms": 1430, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "nobody", "start_ms": 1555, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1660, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 1705, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "room", "start_ms": 1770, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "구독과 좋아요 한 번씩만 부탁드립니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "구독과", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 330, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "번씩만", "start_ms": 385, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "부탁드립니다", "start_ms": 510, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "자 일단 제이슨 노의 매매법을 사용하",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "제이슨", "start_ms": 210, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "노의", "start_ms": 335, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "매매법을", "start_ms": 420, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사용하", "start_ms": 575, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "인계적으로 이어지면서 해결되는데 한 부분만 오염이 된다 하더라도 전체 판단이 흔들릴 수 있다라는 거죠 요런 시스템의 관점에서 봤을 때",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "인계적으로", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이어지면서", "start_ms": 310, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "해결되는데", "start_ms": 540, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 750, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "부분만", "start_ms": 815, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "오염이", "start_ms": 960, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "된다", "start_ms": 1085, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "하더라도", "start_ms": 1190, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "전체", "start_ms": 1400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "판단이", "start_ms": 1525, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "흔들릴", "start_ms": 1670, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1795, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있다라는", "start_ms": 1850, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 2035, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "요런", "start_ms": 2200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시스템의", "start_ms": 2305, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "관점에서", "start_ms": 2460, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봤을", "start_ms": 2615, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2720, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "예를 들면 핸들을 잡고 이렇게 가고 있는데",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "예를", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "들면", "start_ms": 150, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "핸들을", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 645, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 750, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "라는 것이 아니라 의미 있는 흐름을 만들면서 자리를 먼저 형성합니다 따라서 중요한 것은 움직인 이후가 아니라",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "라는", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 115, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "의미", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "흐름을", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만들면서", "start_ms": 655, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 860, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 985, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "형성합니다", "start_ms": 1090, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "따라서", "start_ms": 1350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 1475, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 1620, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "움직인", "start_ms": 1725, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이후가", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 2005, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "버스를 통해서 각각의 모듈들이 메시지 인증이라던가 보안 게이트웨이라던가 OTA 같은 것도 계속 확장될 거잖아요 왜냐하면 테슬라 같은 경우에는 하드웨어 4나 앞으로 AI 5 AI 6",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "버스를", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "통해서", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "각각의", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "모듈들이", "start_ms": 395, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "메시지", "start_ms": 550, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "인증이라던가", "start_ms": 675, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보안", "start_ms": 930, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "게이트웨이라던가", "start_ms": 1015, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "OTA", "start_ms": 1320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 1445, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 1550, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 1635, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "확장될", "start_ms": 1740, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "거잖아요", "start_ms": 1885, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "왜냐하면", "start_ms": 2010, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "테슬라", "start_ms": 2165, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 2310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "경우에는", "start_ms": 2415, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하드웨어", "start_ms": 2570, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "4나", "start_ms": 2725, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 2830, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 2955, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "5", "start_ms": 3040, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 3105, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "6", "start_ms": 3190, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "7분 이상은 모든 기능을 다 지원하고 6 이하로는 기능의 차이만 있지 지원 자체는",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "7분", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이상은", "start_ms": 125, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기능을", "start_ms": 355, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 480, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "지원하고", "start_ms": 545, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "6", "start_ms": 730, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이하로는", "start_ms": 815, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기능의", "start_ms": 970, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "차이만", "start_ms": 1095, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있지", "start_ms": 1220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지원", "start_ms": 1325, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "자체는", "start_ms": 1430, "dur_ms": 120, "phase_lr_deg": 0.0}
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
"transcript": "지금 국제 정세에서 가장 빠르게 존재감을 키우고 있는 나라가 어디인지 묻는다면 세계의 많은 전문가들은 주저없이",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 135, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정세에서", "start_ms": 240, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 425, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 530, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "존재감을", "start_ms": 655, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "키우고", "start_ms": 840, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 965, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "나라가", "start_ms": 1050, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어디인지", "start_ms": 1175, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "묻는다면", "start_ms": 1330, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "세계의", "start_ms": 1515, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 1640, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전문가들은", "start_ms": 1745, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "주저없이", "start_ms": 1950, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "정말 후회 없는 선택이었습니다 강의를 들을 때마다 놀라",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "후회", "start_ms": 125, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 230, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "선택이었습니다", "start_ms": 335, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "강의를", "start_ms": 590, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들을", "start_ms": 735, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때마다", "start_ms": 840, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "놀라", "start_ms": 985, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "파 영상을 SNS에 올리면서 이게 진짜 멋있다라는 듯의 애정을 드러내기도 했는데요 그리고 진짜는 따로 있습니다 바로 영화 속에서 강렬한 존재감을 보이",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "파", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "영상을", "start_ms": 105, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "SNS에", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "올리면서", "start_ms": 455, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 640, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 745, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "멋있다라는", "start_ms": 850, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "듯의", "start_ms": 1075, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "애정을", "start_ms": 1180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "드러내기도", "start_ms": 1335, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "했는데요", "start_ms": 1520, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1675, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "진짜는", "start_ms": 1820, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 1965, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 2070, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 2225, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 2330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 2455, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "강렬한", "start_ms": 2600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "존재감을", "start_ms": 2755, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "보이", "start_ms": 2940, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "라는 내용이 계속 부각을 받는 것 같습니다 여기 보면은 걸프 국가들의 무기 재고가 바닥이 나고 있는 가운데 하나의 천광 같은 레이저 무기가 지금 떠오르고 있다 그래서 보시면은 이제 천궁 같은 경우에는 굉장히 비싸죠 한 발당 수십 억대의 미사일",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "라는", "start_ms": 30, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "내용이", "start_ms": 115, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "부각을", "start_ms": 345, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "받는", "start_ms": 470, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 575, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 630, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 785, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "보면은", "start_ms": 870, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "걸프", "start_ms": 995, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "국가들의", "start_ms": 1100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "무기", "start_ms": 1255, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "재고가", "start_ms": 1360, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "바닥이", "start_ms": 1505, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "나고", "start_ms": 1630, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1735, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "가운데", "start_ms": 1820, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하나의", "start_ms": 1965, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "천광", "start_ms": 2090, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 2215, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "레이저", "start_ms": 2320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "무기가", "start_ms": 2475, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 2600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "떠오르고", "start_ms": 2705, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 2860, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 2965, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보시면은", "start_ms": 3090, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 3245, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "천궁", "start_ms": 3350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 3475, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "경우에는", "start_ms": 3580, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 3735, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "비싸죠", "start_ms": 3880, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4005, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "발당", "start_ms": 4070, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수십", "start_ms": 4175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "억대의", "start_ms": 4280, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 4425, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "온도 차에 외국인들은 한국 배우들은 외계인이냐 연기력이 공포스럽다며 혀를 내두르고 있습니다 지",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "온도", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "차에", "start_ms": 145, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "외국인들은", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 455, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "배우들은", "start_ms": 560, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "외계인이냐", "start_ms": 715, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "연기력이", "start_ms": 900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "공포스럽다며", "start_ms": 1055, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "혀를", "start_ms": 1310, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "내두르고", "start_ms": 1415, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지", "start_ms": 1755, "dur_ms": 60, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "미리 공부했던 종목입니다 그리고 실제로 매매를 했었고요 이 종목은 미래에셋 증권인데요 1월 달부터 2월 달까지 저",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "미리", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공부했던", "start_ms": 135, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "종목입니다", "start_ms": 290, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 495, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 620, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 765, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "했었고요", "start_ms": 890, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1045, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "종목은", "start_ms": 1100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "미래에셋", "start_ms": 1225, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "증권인데요", "start_ms": 1410, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "1월", "start_ms": 1595, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "달부터", "start_ms": 1700, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "2월", "start_ms": 1825, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "달까지", "start_ms": 1930, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 2055, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "가 어떤 지표를 요청하신 건지 정확히 구분할 수 있기 때문에 반드시 8번이라고 표기해 주시길 부탁드립니다 그럼 오늘도 영상 끝까지 시청해 주셔서 감사드리고 우리 구독자 분들의 성공적인 매매와 안정적인 투자를 진심으로 응원합",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "가", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 85, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지표를", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "요청하신", "start_ms": 315, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "건지", "start_ms": 500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "정확히", "start_ms": 605, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "구분할", "start_ms": 760, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 905, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 960, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 1065, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 1190, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "8번이라고", "start_ms": 1345, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "표기해", "start_ms": 1550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주시길", "start_ms": 1705, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "부탁드립니다", "start_ms": 1830, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 2085, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 2190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영상", "start_ms": 2315, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 2420, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "시청해", "start_ms": 2565, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 2720, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "감사드리고", "start_ms": 2845, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 3030, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "구독자", "start_ms": 3135, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "분들의", "start_ms": 3290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "성공적인", "start_ms": 3415, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "매매와", "start_ms": 3600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "안정적인", "start_ms": 3755, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "투자를", "start_ms": 3940, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "진심으로", "start_ms": 4085, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "응원합", "start_ms": 4270, "dur_ms": 120, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:39:53*
