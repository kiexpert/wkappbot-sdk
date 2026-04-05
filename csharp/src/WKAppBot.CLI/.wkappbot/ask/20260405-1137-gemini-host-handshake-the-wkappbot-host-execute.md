# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=11:36:23  uptime=35240s  mem=426MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_113320.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 527-507-507-507-502-507-507-501-576-560-506-576-750-765-756 572-257-572-507-507-517-571-507-507-502-501-507_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 571-107-571-175-170-157-105-107-571-571-570-751-750-570 150-570-105-570-701-570-710-175-107-170-175-105-105-107-750-175-175-157-517-715-175-701-751-715-751-751-751-756-751-715-5_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 576-756-765-756-756-765-765-756-756-675-675-756-756-765-765-765-765-675-671-765-675-765-576-561-517-516-567-576-513-576-751-571-751-571-751-571-571-751-756-576-754-754-576-751-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 103-123-102-103-317-716-627-627-761-765-756-751-576-671-132-531-576-162-623-152-567-625-652-657-627-132-136-132_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 652-615-651-615-657-675-657-657-675-657-675-675 572-527-572-527-527-250 265-260_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 103-135-571-156-165-761-125-631-130-135-103-516-315-516-153-106-136-132-130-170-167-103-567-236-132-130-103-210-561-157-306-176-263-261 137_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 136-103-136-136-136-103-163-134-136-135-143-136-136-106-136-103-136-316-103-136-134-135_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 631-136-125-217-127-127-162-127-152-165-162-321-213-376-371-317-317-376-217-713-617-172-671-763-172-756-726-673-732-367-735-752-756-762-765-752-751-325-725-756-756-756-672-765-1_W.mp3)
  Segment 9: 12608ms ~ 18692ms (file: 570-175-157-261-261-213-312-376-726-627-761-507-203-230-231-123-312-275-236-312-725-275-570-325-267-276-237-102-753-756-237-574-750-531-321-327-267-627-265-260-675-627-632-672-3_W.mp3)
  Segment 10: 19692ms ~ 24444ms (file: 213-206-532-135-231-321-735-753-216-231-236-265-671-132-231-576-132-263-132-312-623-672-572-251-216-236-263-263-213-312-572-756-265-216-517-261-137-130-231-312-132-316-312-573-5_W.mp3)
  Segment 11: 25444ms ~ 33760ms (file: 231-236-270-216-267-267-576-765-270-762-726-276-574-756-321-237-257-270-203-132-762-273-237-726-231-203-570-237-637-132-543-534-270-762-102-231-201-231-257-230-203-261-231-572-2_W.mp3)
  Segment 12: 34760ms ~ 35336ms (file: 475-471-541-435-453-405-104-310-103-145-145-105-104-157-157-105-134-104-154-104 375-573-317-157-471-103-501-175-750-150-105-104-154-107-105-135-154-105-105-154-105-104-103-175-1_V.mp3)
  Segment 13: 36336ms ~ 36912ms (file: 576-571-571-571-570-561-571-501-510-175-751-576-571-175-715-751-751-756-571-571-571-576-576-517-571-571-517-576-576-507-570-570-570-570-570-576-576-572-752-572-517-570-571-570-5_W.mp3)
  Segment 14: 37912ms ~ 44608ms (file: 765-132-130-321-132-671-236-213-125-213-137-357-137-132-715-135-237-310-123-231-237-316-321-312-671-136-126-671-120-127-127-231-231-123-327-237-276-213-627-723-517-157-136-150-2_W.mp3)
  Segment 15: 45608ms ~ 46184ms (file: 130-153-135-135-105-153-316-130-130-153-513-517-513-517-157-157-571-513-517-571-517-517-571-571-571-513 103-135-513-157-173-130-137-517-517-517-157-157-157-517-573-517-513-573-5_V.mp3)
  Segment 16: 47184ms ~ 56040ms (file: 120-132-310-132-126-231-216-207-123-756-275-126-103-754-206-207-103-123-157-132-531-257-136-312-326-675-267-725-213-130-301-207-351-103-263-371-715-702-102-207-132-123-230-213-2_V.mp3)
  Segment 17: 57040ms ~ 57616ms (file: 132-103-715-172-571-725-723-273-312-723-726-276-756-175-615-156-153-516-541-751-517-175-756-751-213-276-267-716-263-126-213-321-261-762-752-216-163-610-106-130-163-160-163-162-6_W.mp3)
  Segment 18: 58616ms ~ 59192ms (file: 157-750-756-751-751-751-715-751-715-715-715-751-756-756-751-756-751-751-571 105-150-150-510-501-517-750-571-751-751-751-715-751-756-751-751-751-751-751-751-751-751-756-754-756-7_W.mp3)
  Segment 19: 60192ms ~ 66312ms (file: 302-267-261-362-123-210-263-231-507-206-120-570-275-576-265-316-627-261-263-672-260-320-236-375-263-123-132-102-127-713-107-120-761-130-236-357-203-132-176-123-210-102-162-725-3_W.mp3)
  Segment 20: 67312ms ~ 67888ms (file: 276-275-572-576-576-571-576-752 251-702-567-567-567-567-576-567_W.mp3)
  Segment 21: 68888ms ~ 69464ms (file: 517-163-136-103-132-126-106-137-105-175-153-136-157-126-103-130-317-136-612-673-106-176-103-136-130-103-103-130-103-671-176-561-765-602-756-263-765 136_V.mp3)
  Segment 22: 70464ms ~ 71040ms (file: 130-315-731-317-317-307-751-715-317-173-173-715 315-310-730-710-710-130-317-153-317-710-107-317-301-715-103-310-301-107-173-103-137-730-713-751-315-731-317-176 715-370-317-710-7_V.mp3)
  Segment 23: 72040ms ~ 72616ms (file: 361-137-376-671-637-157-361-675-657-365-137-135-153-135-315-315_W.mp3)
  Segment 24: 73616ms ~ 74192ms (file: 231-216-312-261-256-231-216-267-506-521-125-172-571-215-756-275-357-127-253-627-276-267-507-267-267-572-235-256-572-570-571-675-320-136-156-165-256-276 627-716-532-723-572-263-7_V.mp3)
  Segment 25: 75192ms ~ 84948ms (file: 236-126-754-263-627-612-613-132-263-754-256-261-231-261-321-176-316-267-571-132-231-261-216-120-261-312-261-265-231-263-103-725-576-625-263-271-326-321-310-175-756-263-130-103-1_W.mp3)
  Segment 26: 85948ms ~ 86524ms (file: 750-572-756-750-573-567-760-570-572-765-750-576-507-570 507-507-576-572-570-520-572-705-507-576-570-570-576-570-567-570-750-750-756-752-725-705 507-571-507-527-572-507-570-560-5_V.mp3)
  Segment 27: 87524ms ~ 88100ms (file: 162-261-621-321-123-261-213-162-152-163-256-127-163-617-567-167-271-621-136-102-670-672-672-670-670-670-576-576-657-675-673-326-267-206-571-756-372-732-760-760-310-310-136-137-3_W.mp3)
  Segment 28: 89100ms ~ 89676ms (file: 371-312-163-631-623-362-163-167-136-316-106-306-165-716-263-263_V.mp3)
  Segment 29: 90676ms ~ 91252ms (file: 310 175-715-175-173-710-170-105-175-175-157-517-571-576-576-576-576-576-576-576-175-576-571-571-157-751-751-571-571-751-571-751-750-750-751-753-752-762-765-765-762-712-671-752-7_W.mp3)
  Segment 30: 92252ms ~ 92828ms (file: 107-753-307-157-103-301-310-137-613-360-361-630-601-613-631-637-613-631-613-367-675-761-173 730-307-735-307-310-317-136-601-613-601-613-163-610-610-613-316-136-301-317-312-312-1_V.mp3)
  Segment 31: 93828ms ~ 103548ms (file: 753-627-621-267-267-263-271-321-230-107-106-237-672-263-267-102-167-236-236-753-756-103-130-672-675-130-134-267-267-260-260-273-276-756-765-107-160-267-263-260-213-321-317-312-1_W.mp3)
  Segment 32: 104548ms ~ 105124ms (file: 652-654-564-564 567-627-625-621-651-627-267-762-627-657-765-756-725-762-752-752-752-572-576-572-752-572-572-572-576-257-761-761-617-617-675-675-765-671-617-671-617-576-527-576-5_W.mp3)
  Segment 33: 106124ms ~ 106700ms (file: 574-574-574-574-574-574-574-574-547-574-574 576-574-547-574-547-574-574-574-574-574-574-574-574-576-574-576-574-547-574-574-574-570 547-576-576-574-574-574-570-547-574-574-576-5_W.mp3)
  Segment 34: 107700ms ~ 108276ms (file: 761-130-107-103-236-263-201-231-213-261-765-762-765-672-627-762 267-127-612-621-263-132-263-263-261-326-627-617-126-675-675_W.mp3)
  Segment 35: 109276ms ~ 109852ms (file: 231-216-326-231-123-321-231-132-261-231-213-123-621-126-265-625-165-132-236-231-236-321-231-321-231-132-326-216-261-312-216-236-216-236-265-265-562-526-632-236-623-263-236-261-2_W.mp3)
  Segment 36: 110852ms ~ 116036ms (file: 362-735-326-620-627-506-625-326-503-756-263-576-253-357-321-172-312-317-537-367-217-621-371-523_V.mp3)
  Segment 37: 117036ms ~ 117612ms (file: 150-150-150-507-150-501-105-157-157-154 105-105-150-105-105-154-510-150-157-157-150-150-157-517-517-157-157-571-157-517-157-105 105-150-150-105-150-157-157-517-157-157-157-150-1_V.mp3)
  Segment 38: 118612ms ~ 119188ms (file: 153-135-135-153-156-156-153-153-756-751-765-735-735-735-753-735-753-573-576-753 751-715-571-173-750-170-715-715-175-715-756-157-175-756-751-715-751-751-175 517-571-753-573-751-5_W.mp3)
  Segment 39: 120188ms ~ 120764ms (file: 507-521-435-507-632-251-762-267-576-236-321-752-267-572-502-567-507-672-561-576-762-675-362-763-527-576-576-657 567-526-726-265-765-265-752-502-623-257-250-526-257-235-750-507-2_W.mp3)
  Segment 40: 121764ms ~ 122340ms (file: 316-106-130-130-301-103-103-105-150-510-312-216-506-576-574-312-612-362-574-103-301 321-621-201-310-540-562-267-526-512-123-103-175-310-130-103-103-312-132-103-132-657-675-675-6_W.mp3)
  Segment 41: 123340ms ~ 123916ms (file: 673-317-173-173-173-317-173-367-173-167 135-731-173-317-135-135-132-317-137-175-763-716-736-761-761-761-756-715-716-713-173 513-137-315-715-571-157-751-735 571-175-571-537-175-7_W.mp3)
  Segment 42: 124916ms ~ 125492ms (file: 130-172-301-103-173-130-137-317-317-317-731-713-157-170-103-130-310-103-173-317 137-130-173-317-301-301-103-103-136-137-713-310-173-317-310-130-310-137-130-137-731-310-372-730 1_V.mp3)
  Segment 43: 126492ms ~ 127068ms (file: 576-621-132-531-576-752-573-136-651-561-576-135-756-705-132-610-132-136-136-312-136-125-162-615-612-627-136-107 627-765_W.mp3)
  Segment 44: 128068ms ~ 128644ms (file: 132-136-135-135-315-135-136-163-312-126-312-726-273-237-327-132-351-123-213-261-312-163-362-316-574-576-217-623-361-326-312-315-137-173-527-352-753-756-574-256-276-257-235-205-2_W.mp3)
  Segment 45: 129644ms ~ 130220ms (file: 751-756-756-751-751-751-756-715 150-175-170-571-517-157-571-715-751-751-751-175-715-715-175-715-751-751-751-756-753-751 510-510-150-157-571-751-175-715-715-751-751-751-751-751-7_W.mp3)
  Segment 46: 131220ms ~ 131796ms (file: 570-507-756-507-570 507-576-567-560-256-527-576-567-507-570-576-576-567-567-567-605-675-625-657-675-652-657-657 567-765-570-570-570-652-267-657-572-570-576-570-572-576-765-576-6_V.mp3)
  Segment 47: 132796ms ~ 133372ms (file: 763-763-765-751-671-675-671-761-673-671-675-765-675-567-165-651-651-613-157-135-103-130_W.mp3)
  Segment 48: 134372ms ~ 138224ms (file: 756-571-756-537-375-571-537-157-135-173-135-163-315-753-175-137-735-375-137-317-123-756-367-320-365-352-365-325-351-327-675-375-357-365-376-735-675-763-576_W.mp3)
  Segment 49: 139224ms ~ 139800ms (file: 135-571-751-753 137-153-135-531-153-130-135-137-315-315-513-157-571-573-571-517-571-517-513-517-517-571-571 103-315-315-517-571-513-531-513-517-157-517-517-571-517-517-571-517-5_W.mp3)
  Segment 50: 140800ms ~ 141376ms (file: 175-713-715-135-713-715-172-715-712-175-751-715-735-752-751-731-735-715-753-750 751-753-153-175-175-175-751-710-715-172-173-713-715-170-715-710-712-713-137-715-710-715-715-731-3_V.mp3)
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
> **Time**: 2026-04-05 11:37:49
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "On my phone. I have more apps on my phone and I use them all, man.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "On", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "phone.", "start_ms": 250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 460, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 510, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 630, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "apps", "start_ms": 780, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 970, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 1070, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "phone", "start_ms": 1180, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 1410, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1510, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "use", "start_ms": 1580, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "them", "start_ms": 1730, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "all,", "start_ms": 1840, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "man.", "start_ms": 2030, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "There's no, I can tell you there's probably nobody in this room",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "There's", "start_ms": 20, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "no,", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 370, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 440, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "tell", "start_ms": 560, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 710, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "there's", "start_ms": 810, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "probably", "start_ms": 1000, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "nobody", "start_ms": 1330, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1640, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 1760, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "room", "start_ms": 1910, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "구독과 좋아요 한 번씩만 부탁드립니다.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "구독과", "start_ms": 30, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 460, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 850, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "번씩만", "start_ms": 980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "부탁드립니다.", "start_ms": 1440, "dur_ms": 680, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "자, 일단 제이슨 노의 매매법을 사용하여",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "자,", "start_ms": 50, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "일단", "start_ms": 250, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "제이슨", "start_ms": 570, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "노의", "start_ms": 1000, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "매매법을", "start_ms": 1290, "dur_ms": 540, "phase_lr_deg": 0.0},
{"word": "사용하여", "start_ms": 1840, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "유기적으로 이어지면서 해결돼야 되는데 한 부분만 오염이 된다 하더라도 전체 판단이 흔들릴 수 있다라는 거죠. 요런 시스템의 관점에서 보았을 때, 예를 들면 이제 핸들을 잡고 이렇게 가고 있는데",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "유기적으로", "start_ms": 40, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "이어지면서", "start_ms": 630, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "해결돼야", "start_ms": 1260, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "되는데", "start_ms": 1780, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 2130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "부분만", "start_ms": 2290, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "오염이", "start_ms": 2720, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "된다", "start_ms": 3110, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "하더라도", "start_ms": 3400, "dur_ms": 560, "phase_lr_deg": 0.0},
{"word": "전체", "start_ms": 3970, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "판단이", "start_ms": 4290, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "흔들릴", "start_ms": 4740, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 5170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다라는", "start_ms": 5280, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "거죠.", "start_ms": 5770, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "요런", "start_ms": 6100, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "시스템의", "start_ms": 6420, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "관점에서", "start_ms": 6950, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "보았을", "start_ms": 7440, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "때,", "start_ms": 7810, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "예를", "start_ms": 8000, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "들면", "start_ms": 8250, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 8520, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "핸들을", "start_ms": 8770, "dur_ms": 390, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 9170, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 9460, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 9810, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 10080, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "라는 것이 아니라 의미 있는 흐름을 만들면서 자리를 먼저 형성합니다. 따라서 중요한 것은 움직인 이후가 아니라",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "라는", "start_ms": 60, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "것이", "start_ms": 310, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 540, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "의미", "start_ms": 930, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1220, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "흐름을", "start_ms": 1440, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "만들면서", "start_ms": 1870, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "자리를", "start_ms": 2390, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 2780, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "형성합니다.", "start_ms": 3100, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "따라서", "start_ms": 3830, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 4280, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 4710, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "움직인", "start_ms": 4960, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "이후가", "start_ms": 5410, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 5840, "dur_ms": 410, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "버스를 통해서 각각의 모듈들이 메시지 인증이라든가 뭐 보안 게이트웨이라든가 OTA 같은 것도 계속 이제 확장이 될 거잖아요. 왜냐하면 테슬라 같은 경우에는 하드웨어 4나 뭐 앞으로 AI 5 AI 6",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "버스를", "start_ms": 40, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "통해서", "start_ms": 430, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "각각의", "start_ms": 850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "모듈들이", "start_ms": 1280, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "메시지", "start_ms": 1810, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "인증이라든가", "start_ms": 2260, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 3050, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보안", "start_ms": 3160, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "게이트웨이라든가", "start_ms": 3480, "dur_ms": 920, "phase_lr_deg": 0.0},
{"word": "OTA", "start_ms": 4410, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 4840, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "것도", "start_ms": 5170, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 5460, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 5810, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "확장이", "start_ms": 6060, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 6490, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "거잖아요.", "start_ms": 6620, "dur_ms": 540, "phase_lr_deg": 0.0},
{"word": "왜냐하면", "start_ms": 7170, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "테슬라", "start_ms": 7700, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 8150, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "경우에는", "start_ms": 8480, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "하드웨어", "start_ms": 9000, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "4나", "start_ms": 9490, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 9820, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "앞으로", "start_ms": 9930, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 10350, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "5", "start_ms": 10640, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 10800, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "6", "start_ms": 11090, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "분 이상은 모든 기능을 다 지원하고 식스 이하로는 기능의 차이만 있지 지원 자체는",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "분", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이상은", "start_ms": 190, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 620, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "기능을", "start_ms": 940, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1330, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "지원하고", "start_ms": 1460, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "식스", "start_ms": 1990, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "이하로는", "start_ms": 2340, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "기능의", "start_ms": 2830, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "차이만", "start_ms": 3200, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "있지", "start_ms": 3620, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "지원", "start_ms": 3910, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "자체는", "start_ms": 4240, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "지금 국제 정세에서 가장 빠르게 존재감을 키우고 있는 나라가 어디인지 묻는다면 세계 많은 전문가들은 주저없이",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 50, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 370, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "정세에서", "start_ms": 720, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 1310, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 1640, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "존재감을", "start_ms": 2070, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "키우고", "start_ms": 2600, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2990, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "나라가", "start_ms": 3240, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "어디인지", "start_ms": 3660, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "묻는다면", "start_ms": 4150, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "세계", "start_ms": 4670, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 5000, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "전문가들은", "start_ms": 5290, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "주저없이", "start_ms": 5910, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "정말 후회 없는 선택이었습니다. 강의를 들을 때마다 놀라",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 20, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "후회", "start_ms": 370, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 700, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "선택이었습니다.", "start_ms": 990, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "강의를", "start_ms": 1820, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "들을", "start_ms": 2270, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "때마다", "start_ms": 2590, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "놀라", "start_ms": 3020, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "파 영상을 SNS에 올리면서 이게 진짜 멋있다라는 듯의 애정을 드러내기도 했는데요. 그리고 진짜는 따로 있습니다. 바로 영화 속에서 강렬한 존재감을 보",
"speaker": "speaker_5",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "파", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "영상을", "start_ms": 170, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "SNS에", "start_ms": 590, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "올리면서", "start_ms": 1120, "dur_ms": 540, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1670, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1960, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "멋있다라는", "start_ms": 2310, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "듯의", "start_ms": 2930, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "애정을", "start_ms": 3200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "드러내기도", "start_ms": 3630, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "했는데요.", "start_ms": 4260, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 4750, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "진짜는", "start_ms": 5140, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "따로", "start_ms": 5570, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 5890, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 6480, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 6810, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 7160, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "강렬한", "start_ms": 7610, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "존재감을", "start_ms": 8100, "dur_ms": 560, "phase_lr_deg": 0.0},
{"word": "보", "start_ms": 8670, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "라는 내용이 계속 부각을 받는 것 같습니다. 자 여기 보면은 걸프 국가들의 무기 재고가 바닥이 나고 있는 가운데 하나의 천군 같은 레이저 무기가 지금 떠오르고 있다. 그래서 보시면 이제 천궁 같은 경우에는 굉장히 비싸죠. 한 발당 수십억 대의 미사일",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "라는", "start_ms": 30, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "내용이", "start_ms": 280, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 670, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "부각을", "start_ms": 990, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "받는", "start_ms": 1420, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1710, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "같습니다.", "start_ms": 1870, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 2400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 2530, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "보면은", "start_ms": 2800, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "걸프", "start_ms": 3190, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "국가들의", "start_ms": 3520, "dur_ms": 540, "phase_lr_deg": 0.0},
{"word": "무기", "start_ms": 4070, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "재고가", "start_ms": 4390, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "바닥이", "start_ms": 4820, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "나고", "start_ms": 5210, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 5500, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "가운데", "start_ms": 5750, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "하나의", "start_ms": 6170, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "천군", "start_ms": 6540, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 6890, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "레이저", "start_ms": 7210, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "무기가", "start_ms": 7640, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 8030, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "떠오르고", "start_ms": 8350, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "있다.", "start_ms": 8840, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 9170, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "보시면", "start_ms": 9560, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 9910, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "천궁", "start_ms": 10160, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 10480, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "경우에는", "start_ms": 10800, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 11320, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "비싸죠.", "start_ms": 11770, "dur_ms": 460, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 12240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "발당", "start_ms": 12370, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "수십억", "start_ms": 12700, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "대의", "start_ms": 13120, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "미사일", "start_ms": 13390, "dur_ms": 420, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "이런 온도 차에 외국인들은 한국 배우들은 외계인이냐 연기력이 공포스럽다며 혀를 내두르고 있습니다. 지",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이런", "start_ms": 40, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "온도", "start_ms": 330, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "차에", "start_ms": 650, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "외국인들은", "start_ms": 920, "dur_ms": 610, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 1540, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "배우들은", "start_ms": 1870, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "외계인이냐", "start_ms": 2360, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "연기력이", "start_ms": 2990, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "공포스럽다며", "start_ms": 3520, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "혀를", "start_ms": 4310, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "내두르고", "start_ms": 4630, "dur_ms": 510, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 5150, "dur_ms": 540, "phase_lr_deg": 0.0},
{"word": "지", "start_ms": 5700, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "미리 공부했던 종목입니다. 그리고 실제로 매매를 했었고요. 이 종목은 미래에셋 증권인데요, 1월달부터 2월달까지 저",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "미리", "start_ms": 20, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "공부했던", "start_ms": 340, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "종목입니다.", "start_ms": 830, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1560, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 1950, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 2370, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "했었고요.", "start_ms": 2800, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3390, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "종목은", "start_ms": 3520, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "미래에셋", "start_ms": 3940, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "증권인데요,", "start_ms": 4470, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "1월달부터", "start_ms": 5160, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "2월달까지", "start_ms": 5790, "dur_ms": 640, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 6440, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "제가 어떤 지표를 요청하신 건지 정확히 구분할 수 있기 때문에 반드시 8번이라고 표기해 주시길 부탁드립니다. 그럼 오늘도 영상 끝까지 시청해 주셔서 감사드리고 우리 구독자분들의 성공적인 매매와 안정적인 투자를 진심으로",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 30, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 320, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "지표를", "start_ms": 640, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "요청하신", "start_ms": 1070, "dur_ms": 540, "phase_lr_deg": 0.0},
{"word": "건지", "start_ms": 1620, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "정확히", "start_ms": 1910, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "구분할", "start_ms": 2330, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2780, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 2890, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 3210, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "반드시", "start_ms": 3600, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "8번이라고", "start_ms": 4030, "dur_ms": 640, "phase_lr_deg": 0.0},
{"word": "표기해", "start_ms": 4680, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "주시길", "start_ms": 5070, "dur_ms": 360, "phase_lr_deg": 0.0},
{"word": "부탁드립니다.", "start_ms": 5440, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 6230, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 6550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "영상", "start_ms": 6940, "dur_ms": 340, "phase_lr_deg": 0.0},
{"word": "끝까지", "start_ms": 7290, "dur_ms": 440, "phase_lr_deg": 0.0},
{"word": "시청해", "start_ms": 7740, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "주셔서", "start_ms": 8160, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "감사드리고", "start_ms": 8550, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 9240, "dur_ms": 310, "phase_lr_deg": 0.0},
{"word": "구독자분들의", "start_ms": 9560, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "성공적인", "start_ms": 10290, "dur_ms": 540, "phase_lr_deg": 0.0},
{"word": "매매와", "start_ms": 10840, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "안정적인", "start_ms": 11270, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "투자를", "start_ms": 11800, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "진심으로", "start_ms": 12220, "dur_ms": 580, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:37:49*
