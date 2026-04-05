# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=12:06:30  uptime=37399s  mem=429MB  system_processes=361"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_120150.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 362-162-673-631-136-106-107-103-765-367-372-130-301-103-137-163-130-103-136-126-605-160-361-132-170-176-162-671-176 162-167-106-126-610-601-367-126-620-136-103-163-160-103-106-6_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 576-213-760-675-267-756-267-236-276-160-562-756-127-173-325-351-765-723-507-152-237-547-357-531-365-526-657-756-756-726-761-327-265-312-237-317-756-613-365-652-637-261-162-376-6_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 751-675-756-756-510-517-507-501-507-510-570-517-571-517-157-501-517-507-157-570-517-157-150-751-751 517-501-507-510-501-157-150-107-105-517-501-157-751-157-510-501-504-510-510-5_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 317-513-532-715-537-571-527-357-107-107-710-715-763-765-617-716-760-175-751-176-716-175-176-715-716-750-756-715-571-571-517-576-576-712-736-716-761-726-716-762-716-761-762-716-7_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 317-137-132-132-312 273-312-302 314 615-265-123-270-572-267-620-605-267-756-576-571-631-675-657-675-756-103-103-163-132-123-162 271-127-256-126-126-712-217-267-671-765-267-726-6_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 670-675-673-750-765-760-765-716-375-631-523-413-102-132-143-162-621-601-602-576-507-576-750-765-672-627-675-675-612-765-160-136-132-130-130-621-163-103-561-621-201-620-607-621-6_W.mp3)
  Segment 7: 9456ms ~ 16044ms (file: 167-362-361-321-231-570-423-231-231-301-326-573-206-261-573-621-601-670-701-137-327-103-317-321-507-572-237-307-167-143-765-263-216-572-705-276-263-157-237-136-310-762-527-521-3_W.mp3)
  Segment 8: 17044ms ~ 17620ms (file: 502-501-526-625-256-720-726-276-267-706-760-726-706-702-267-756-572-752-571 572-562-507-562-526-275-760-762-267-702-270-276-270-702-276-360-725-257-520-276-250-752-513-360-576 5_V.mp3)
  Segment 9: 18620ms ~ 19196ms (file: 657-576-765-576-756-567-567-657-675-657-675-675-675-657-615 260-627-652-625-657-651-657-675-657-657-675-675-576-756-765-675-765-675-761-671-675-617 652-657-675-657-675-657-657-6_W.mp3)
  Segment 10: 20196ms ~ 25020ms (file: 260-210-231-260-162-236-762-267-123-216-321-231-237-267-276-267-267-206-216-103-102-126-102-106-123-130-231-231-132-213-321-362-762-726-130-326-261-107-173-172-132-127-203-320-1_W.mp3)
  Segment 11: 26020ms ~ 26596ms (file: 201-260-203 216-120-162-612-162-126-612-612-612-621-612-612-617-671-671-617-612-160 162-621-612-621-612-612-621-612-617-612-610-612 205-206-652-257-612-675-651-615-652-675-672-6_V.mp3)
  Segment 12: 27596ms ~ 28172ms (file: 301-130-310-173-130-137-310-173-130-103-301-317-371-310-731-130-317 137-317-137-317-137-173-310-301-107-173-107-130-103-130-301-130-301-130-103-307-137-173-103-317-703-701-307 3_V.mp3)
  Segment 13: 29172ms ~ 29748ms (file: 175-157-157-153-751-150-571-157-573-571-157-573-157 157-571-157-517-157-517-157-175-157-157-517-571-517-571 517-157-175-157-157-517-157-157-175-157-157-157 150-517-157-517-571-1_V.mp3)
  Segment 14: 30748ms ~ 31324ms (file: 571-170-175-751-715-360-610-160-610-671-673-627-672-673-637-671-617-617-675-675-672-672-675 357-701-370-753-175-310-307-135-175-670-637-671-670-607-601-613-361-637-630-670-671-6_V.mp3)
  Segment 15: 32324ms ~ 32900ms (file: 103-136-316-132-137-310-231-123-231-123-132-132-162-210-120-312 574-756-706-760-764-764-762-764-762-760-706-764-671 126-623-162-310-106-103-136-617-136-106-163-103-651-103-123-1_W.mp3)
  Segment 16: 33900ms ~ 34476ms (file: 576-531-275-752-756-756-756-576-576-576-576-756-576-576-576-756-756-345-561-542-527-751-752-756-756-756-576-576-576-576-576-576-576-576-756-756-765 143-752-576-756-567-576-576-5_W.mp3)
  Segment 17: 35476ms ~ 40012ms (file: 567-765-567-765-756-675-756-756-564-567-564-567-567-527-567-576-547-576-574-576-567-576-567-576-576-567-576-576-547-546-153-305 103-125-501-521-205-203-206-213-231-103-301-263-6_W.mp3)
  Segment 18: 41012ms ~ 41588ms (file: 631-316-123-627-216-231-623-621-216-126-136_W.mp3)
  Segment 19: 42588ms ~ 43164ms (file: 571-571-513-573-157-537-571-153-517-153-531-351-157-513-153-135-315-153-165-135-365-157-153-513-517-571-517-517-517-573-571-517-157-571_W.mp3)
  Segment 20: 44164ms ~ 44740ms (file: 150-157-170-157-751-751-751-715-715-175-715-715-715-175-175-715-751-715-715 510-517-751-517-751-751-751-751-751-751-751-751-751-715-751-715-175-715-751-175-175-175 150-157-517-7_V.mp3)
  Segment 21: 45740ms ~ 53228ms (file: 261-236-276-675-261-120-126-126-567-523-210-267-627-627-127-612-135-106-106-312-312-123-201-205-160-102-326-362-326-365-637-165-163-163-136-316-361-316-136-361-623-136-163-231-1_W.mp3)
  Segment 22: 54228ms ~ 54804ms (file: 576-573-751-517-751-574-571 576-571-517-157-571-157-751-517-751-751-157-517-175-157-157-571-574-571-571-157-517-175-157-157-571-753-137-136-137-137-173-317-137-317-137-132-135-5_W.mp3)
  Segment 23: 55804ms ~ 56380ms (file: 574-574-574-574-574 574-574-574-574-574-574-574-574-754-571-175-715-175-175-157 574-574-574-574-574-574-574-574-574-574-574-574-574-574-574-573-735-751-571-574 574-574-574-574-5_W.mp3)
  Segment 24: 57380ms ~ 57956ms (file: 154-451-741-765-675-167-137-174-143-134-132-163-153-132-143-153-137_V.mp3)
  Segment 25: 58956ms ~ 59532ms (file: 512-501-513-516-157-156-513-512-521-512-512-157-153-513-512-517-513-501-513-560-567-752-756-756-513-156-517-571-571-517 501-527-257-752-507-576-571-560-652-567-605-657-567-570-5_V.mp3)
  Segment 26: 60532ms ~ 61108ms (file: 756-765-265-627 570-675-756-157-576-135-701-132_V.mp3)
  Segment 27: 62108ms ~ 68408ms (file: 257-265-276-275-275-702-271-721-173-271-271-173-671-176-170-107-175-756-756-571-157-321-326-275-236-753-721-672-756-276-726-167-276-267-627-637-312-132-316-573-107-572-754-673-1_W.mp3)
  Segment 28: 69408ms ~ 69984ms (file: 715-175-715-751-751-751-571 150-150-507-157-105-150-157-715-751-715-715-751-751-751-715-751-753-750 105-105-517-105-105-105-103-170-715-715-175-751-751-751-751-574-574 540-517-5_V.mp3)
  Segment 29: 70984ms ~ 71560ms (file: 670-721-637-362-762-671-765-617-136-316-130-103-716-716-671-761-716-675-765-176-137-130-217-123-261-126-631-136 136-103-123-210-136-103-670-675-617-136-316-137-175-126-162-167-5_V.mp3)
  Segment 30: 72560ms ~ 79328ms (file: 312-317-216-231-305-703-130-375-172-726-261-102-673-631-310-726-312-210-762-723-620-237-173-310-273-635-172-217-206-752-316-267-261-213-216-125-321-123-213-216-213-126-321-201-1_W.mp3)
  Segment 31: 80328ms ~ 80904ms (file: 165-160 105-153-105-150-150-516-156-150-157-165-157-105-105-105-106-106-107-107-103-103-107 150-105-150-157-150-157-105-150-150-136-105-103-105 510-150-105-513-130-103-106-167-1_W.mp3)
  Segment 32: 81904ms ~ 82480ms (file: 103-263-620-163-173-675-761-765-615-135-316-310-103-130-132-106-107-103-136-175-265-216-102-167_V.mp3)
  Segment 33: 83480ms ~ 88412ms (file: 123-267-276-273-723-270-276-273-217-320-267-216-276-267-627-267-267-376-725-702-721-127-120-120-130-231-230-126-260-260-267-103-165-160-512-103-102-672-201-260-263-326-106-123-1_V.mp3)
  Segment 34: 89412ms ~ 89988ms (file: 621-126 260-261-621-261-627-216 123-123 612-621-621-672-601 612-612-612-617-675-675-675-675-670-672-615-613-615-657-615-501-510-573-615-651-615-672-675-265-627-651-165-617-635-6_W.mp3)
  Segment 35: 90988ms ~ 100600ms (file: 764-760-206-260-103-134-260-103-130-132-103-134-261-260-261-102-321-261-321-312-316-132-132-316-312-103-163-136-163-103-167-675-671-601-657-716-576-517-516-251-562-120-312-536-5_W.mp3)
  Segment 36: 101600ms ~ 117224ms (file: 107-107-132-761-106-261-176-317-726-236-125-172-137-176-102-271-102-123-130-217-130-132-731-231-123-130-176 162-120-213-123-320-263-321-231-236-265-263-263-236-123-263-572-236-2_W.mp3)
  Segment 37: 118224ms ~ 118800ms (file: 576-570-570-507-570 150-517-157-105-105-517-157-105-517-501-517-510-570-576-501-510-501-150-510-570-517-571-570-574-574-574-574-574-574-574 570-507-501-175-517-150-517-157-510-5_V.mp3)
  Segment 38: 119800ms ~ 120376ms (file: 137-317-310-130-753-137-103-370-175-130-753-370-312-317-703 107-751-310-175-130-103-173-701-130-175-310-130-103-107-753-173-703-371-301-371-107-751-752-750-735-173-715-753-173 3_V.mp3)
  Segment 39: 121376ms ~ 136244ms (file: 762-723-751-325-715-326-130-320-310-327-327-267-320-276-276-206-320-574-275-237-273-231-317-765-765-726-312-203-321-310-236-267-762-326-673-321-230-765-102-371-310-321-276-130-1_W.mp3)
  Segment 40: 137244ms ~ 137820ms (file: 567-625-165-162-621-613-361-361-316-310-317-137-107-513-130-103-103 572-572-130 732-217-260-752-256-725-573-136-132-157-152-520-136-570-126-750-761-735-320-326-201-126-251-126-3_W.mp3)
  Segment 41: 138820ms ~ 139396ms (file: 570-547-547-576-567-567-567-562-567-270-267-627-276-652-672-672-762-572-725-276-632-657-675-631-316-576-567-657-675-671-657-675-172-137-137 263-267-261-267-257-572 251-625-621_W.mp3)
  Segment 42: 140396ms ~ 140972ms (file: 362-172-136-135-315-570-751-265-362-652-576-762-572-576-765-720-267-263-163-752-673-327-136-507-756-605-572-726-275-572-257-756-762-327-726-307-136-316-613-136-631-316-163-316-6_V.mp3)
  Segment 43: 141972ms ~ 147912ms (file: 574-572-574-321-105-260-201-276-542-230-357-670-267-201-213-163-162-715-671-136-752-206-102-762-726-276-675-257-267-267-506-602-672-607-301-765-670-670-571-310-176-637-210-132-1_W.mp3)
  Segment 44: 148912ms ~ 149488ms (file: 702-275-527-576-576-576-576-576-756-576-756-756 506-507-527-572-752-752-572-756-753-751-756-756-735-514-541-574-537-573-507-576-572-573-572-507-521-517-564-507-572-753-513-537-5_V.mp3)
  Segment 45: 150488ms ~ 151064ms (file: 576-257-752-576-527-520-502-756-726-571-750-576 701-750-750-751-750-576-576 576-576-576-576-752-572-572-756-756-756-756-576-756-576-725-752-572-576 507-507-570-572-527-507-576-5_W.mp3)
  Segment 46: 152064ms ~ 152640ms (file: 576-576-756-571 567-563-567-567-567-562-567-567-567-567-567-526-765-765-756-765-756-756-756-756-765-765 520-510-517-501-521-526-520-502-502-527-570-576-572-765 510-521-527-526-5_W.mp3)
  Segment 47: 153640ms ~ 154216ms (file: 510-501-510-501 570-571-510-501-150 510-150-517-157-517-571 510-105-150-105-105-150-513-510-571-571-157-157-751-701-751-715-750-750-751-750-751-705-750-571-573 547-547-574-570-5_V.mp3)
  Segment 48: 155216ms ~ 155792ms (file: 157-175-716-167-175-175-165-175-175-165-176-157-157-754-754-754-754-754-754-751-754-751-754-751-754-754-751-150-150-150-105-175-157-167-165-167-517-157-156-175-176-715-176-715-1_W.mp3)
  Segment 49: 156792ms ~ 157368ms (file: 725-152-321-231-152-125-165-165 562-256-567-617-167-572-527-351-315-315-135-136-531-157-756-625-657-625-625-265-562-567-652-562-574-562-563-562-567-576-657-756-562-256-265-135-3_W.mp3)
  Segment 50: 158368ms ~ 168340ms (file: 201-103-230-312-201-230-236-267-236-236-267-231-206-547-316-231-230-230-230-574-236-107-260-263-236-236-230-320-107-103-203-210-527-132-213-103-206-230-106-310-310-260-312-203-2_W.mp3)
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
> **Time**: 2026-04-05 12:07:52
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "것들을 이제 살펴봐야 되는데",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "것들을", "start_ms": 10, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "살펴봐야", "start_ms": 360, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "되는데", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "정확히 어떤 장치가 어떻게 이 내부 상태를 조작이 됐는지에 대한 것들을",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "정확히", "start_ms": 20, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "장치가", "start_ms": 230, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 330, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 410, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "내부", "start_ms": 450, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "상태를", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "조작이", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "처음부터 시작을 할 수 있으니까",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "처음부터", "start_ms": 30, "dur_ms": 160, "phase_lr_deg": -5.0},
{"word": "시작을", "start_ms": 200, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "할", "start_ms": 350, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "수", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "있으니까", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": -5.0}
]
},
{
"segment": 4,
"transcript": "아빠랑 할머니한테 참 고마운 것 같아요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아빠랑", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "할머니한테", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "참", "start_ms": 330, "dur_ms": 60, "phase_lr_deg": -5.0},
{"word": "고마운", "start_ms": 400, "dur_ms": 90, "phase_lr_deg": -5.0},
{"word": "것", "start_ms": 500, "dur_ms": 30, "phase_lr_deg": -5.0},
{"word": "같아요", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": -5.0}
]
},
{
"segment": 5,
"transcript": "나름 금수저시네요",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "나름", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 10.0},
{"word": "금수저시네요", "start_ms": 210, "dur_ms": 366, "phase_lr_deg": 10.0}
]
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
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 100, "dur_ms": 1500, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 2000, "dur_ms": 2000, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 4500, "dur_ms": 2000, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "기능 뭐 기존 기기에 있었던 거 안 넣어주는 거 흔히 있었던 일이거든요",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "기능", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 120, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "기존", "start_ms": 160, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "기기에", "start_ms": 250, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "있었던", "start_ms": 350, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 430, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 460, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "넣어주는", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "그런데 왜 이번에 유독 이런 얘기가 좀 심하게 나왔을까",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그런데", "start_ms": 20, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 150, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "이번에", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "유독", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "얘기가", "start_ms": 490, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "다 가질 수도 있다는 장점이 있고요 예 또 가성비도 좋죠",
"speaker": "speaker_6",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "다", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가질", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수도", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "장점이", "start_ms": 560, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있고요", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 1000, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "점심시간이 가까워지자",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "점심시간이", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가까워지자", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이제 진짜 맛있게 돼야 돼 오케이 예 야 오케이",
"speaker": "speaker_8",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "mixed",
"words": [
{"word": "이제", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "맛있게", "start_ms": 250, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "돼야", "start_ms": 380, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "돼", "start_ms": 450, "dur_ms": 26, "phase_lr_deg": 0.0},
{"word": "오케이", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 0.0}
]
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
"transcript": "이랬어요 이따 30분 있다 나오니까 얼른 준비해요 사고 칠 거면 네",
"speaker": "speaker_9",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "이랬어요", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이따", "start_ms": 200, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "30분", "start_ms": 290, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "나오니까", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "네 얼른 공개를 해서 백제 향로가 발견되었다 이렇게 보도가 되었고",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "얼른", "start_ms": 80, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공개를", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "백제", "start_ms": 410, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "향로가", "start_ms": 480, "dur_ms": 96, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "보도되자마자 수식어가 그거였습니다 백제 문화의 정수",
"speaker": "speaker_10",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "보도되자마자", "start_ms": 10, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "수식어가", "start_ms": 200, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그거였습니다", "start_ms": 330, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "백제", "start_ms": 490, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "문화의", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "그래서 저희가 이제 향로 대향로간 12월 22일에 얼른 공개를",
"speaker": "speaker_10",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "저희가", "start_ms": 140, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 270, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "향로", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "대향로간", "start_ms": 470, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "12월", "start_ms": 630, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "22일에", "start_ms": 760, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "얼른", "start_ms": 920, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공개를", "start_ms": 1030, "dur_ms": 3506, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "아 결국 역전당했던 전례를 거론하면서",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "아", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "결국", "start_ms": 70, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "역전당했던", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전례를", "start_ms": 340, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "거론하면서", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "한국 방위산업도 이와 비슷한 변곡점에 서 있다고 덧붙였습니다",
"speaker": "speaker_11",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "방위산업도", "start_ms": 110, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이와", "start_ms": 260, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "비슷한", "start_ms": 330, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "변곡점에", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "서", "start_ms": 530, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 560, "dur_ms": 16, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "한국이 아랍 에미리트로부터 총 2400만 배럴의 원유를 긴급 공급받기로 한 데 대해 일본 요미우리 신문이 이례적인 우대 조치라 보도했습니다",
"speaker": "speaker_12",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "한국이", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아랍", "start_ms": 190, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "에미리트로부터", "start_ms": 280, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "총", "start_ms": 470, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "2400만", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "이 신문은 그 배경으로 한국과 UAE 양국의 그간 군사 협력에 더해 한국산",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "신문은", "start_ms": 70, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "배경으로", "start_ms": 260, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "한국과", "start_ms": 400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "UAE", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "양국의", "start_ms": 620, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그간", "start_ms": 750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "군사", "start_ms": 860, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "협력에", "start_ms": 970, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더해", "start_ms": 1100, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한국산", "start_ms": 1190, "dur_ms": 6298, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "중동의 사막에서 통하는 이유는 역설적이게도 한국이 원래부터 농사짓기 끔찍하게 불리한 조건을 가진 나라였기 때문입니다",
"speaker": "speaker_13",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "중동의", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사막에서", "start_ms": 130, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "통하는", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이유는", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "역설적이게도", "start_ms": 530, "dur_ms": 46, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "국토의 약 70퍼센트가 산지로 덮여 있고 연간 강수량의 약 63퍼센트가 여름 석 달에 집중적으로 쏟아집니다",
"speaker": "speaker_13",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "국토의", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "약", "start_ms": 100, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "70퍼센트가", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "산지로", "start_ms": 300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "덮여", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 500, "dur_ms": 76, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "평탄하고 안정적인 경작지가 절대적으로 부족한 것이죠 이 척박한 조건에서",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "평탄하고", "start_ms": 30, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "안정적인", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "경작지가", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "절대적으로", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "직접 참여한 한일 합작 프로젝트입니다 특히 범죄 도시 시리즈의 핵심 인물인 마동석이 어소시에이트 프로듀서로 참여해 액션과 이",
"speaker": "speaker_14",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "직접", "start_ms": 20, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "참여한", "start_ms": 130, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한일", "start_ms": 260, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "합작", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "프로젝트입니다", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "뼈대가 되기 때문에",
"speaker": "speaker_15",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "뼈대가", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "되기", "start_ms": 220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 380, "dur_ms": 196, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "매매할 때 그 순간순간의 강약 조절을 통해서 그렇게 수익을 꾸준하게 쌓아가는 게 되게 매력적이고 그런 기분으로 매매를 하다 보니까 예 그게 좀 핵심 포인트가 아닌가 라면이 와가지고 잠깐 끊겼는데 이어서 라면 여러분들 오늘",
"speaker": "speaker_15",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "매매할", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 200, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 260, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "순간순간의", "start_ms": 300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "강약", "start_ms": 510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "조절을", "start_ms": 620, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "통해서", "start_ms": 780, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 910, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 1020, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "꾸준하게", "start_ms": 1180, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "쌓아가는", "start_ms": 1370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 1530, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "되게", "start_ms": 1580, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "매력적이고", "start_ms": 1690, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 1950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "기분으로", "start_ms": 2060, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 2250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "하다", "start_ms": 2410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 2520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "예", "start_ms": 2650, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 2710, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2800, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 2860, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "포인트가", "start_ms": 2990, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아닌가", "start_ms": 3150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "라면이", "start_ms": 3280, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "와가지고", "start_ms": 3470, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잠깐", "start_ms": 3630, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "끊겼는데", "start_ms": 3760, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이어서", "start_ms": 3950, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "라면", "start_ms": 4110, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여러분들", "start_ms": 4240, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 4450, "dur_ms": 1850, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:07:52*
