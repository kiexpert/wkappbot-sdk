# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=21096  time=08:35:49  uptime=18223s  mem=123MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_083433.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 135-107-715-175-570-573-571 751-150-157-705-571-713-135-150-137-713-157-750-175 517-571-571-571-157-715-570-570-570 571-571-574-574-574-571 573-573-573-571-573-571-157-103-137 5_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 153-156-607-517 137-376-316-657-306-730-652-751-160-106-670_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 132-123-120-102-130-312-123-132-132-123-102-103-103-130-102-103-102-105-120-125-125-152-517-517-517-152-157-153-152-135-132-162-126-126-162-123-213-123-120-132-120-123-120-120-1_V.mp3)
  Segment 4: 4728ms ~ 14556ms (file: 201-312-132-231-206-253-103-230-534-520-201-132-103-103-135-123-574-215-132-213-136-103-301-312-261-103-321-263-263-236-103-132-132-103-261-206-145 712-321-315-352-257-320-351-3_V.mp3)
  Segment 5: 15556ms ~ 23080ms (file: 675-103-167-571-125-321-731-276-276-762-627-162-162-267-236-715-752-312-572-756-167-106-321-312-321-372-126-123-273-627-267-726-715-267-576-765-627-267-765-216-103-312-103-730-7_W.mp3)
  Segment 6: 24080ms ~ 24656ms (file: 574-571-573-571-573-574-574-574-574-574-574-574-574-574-574-574-571-570-571-571-573 573-570-574-571-571-157-751-157-571-571-570-517-517-571-751-175-751-157-751-715-573-750-573-5_W.mp3)
  Segment 7: 25656ms ~ 35304ms (file: 104-107-106-103-130-103-103-135-351-317-271-273-102-613-201-321-312-102-103-103-103-106-107-170-132-123-527-763-761-312-360-567-310-265-132-601-163-126-176-163-137-103-175-317-3_W.mp3)
  Segment 8: 36304ms ~ 43468ms (file: 361-163-630-120-716-632-637-673-673-627-316-310-136-613-213-763-106-576-275-106-527-752-106-710-312-632-627-162-103-176-163-312-106-173-213-237-273-762-267-276-527-270-706-726-7_W.mp3)
  Segment 9: 44468ms ~ 51812ms (file: 271-267-702-371-130-307-276-270-320-130-103-150-137-123-310-130-123-103-375-130-310-573-310-167-137-310-231-152-123-132-127-275-217-127-217-120-271-216-721-201-123-271-217-276-1_W.mp3)
  Segment 10: 52812ms ~ 53388ms (file: 132-137-731-137-572-573-576-753-607-675-637-763-367-756-763-736-376-316-173-273-157-637-617-163-302-312-132-123-162-312-132-172-173-372-172-716-712-137-736-672-761-736-765-756-7_W.mp3)
  Segment 11: 54388ms ~ 54964ms (file: 517-750-751-150-715-571-715-715-570-150-705-571-517-751-753-570-751-517-571-573-571-576-753-572-576-573 570-571-751-157-715-753-537-571-715-173-731-175-710-570-751-750-570-571-5_W.mp3)
  Segment 12: 55964ms ~ 64352ms (file: 576-761-670-760-267-216-261-213-267-213-210-176-120-263-574-750-701-230-206-576-765-261-217-130-672-127-307-320-207-620-731-765-675-236-123-621-230-120-263-206-132-103-213-201-1_W.mp3)
  Segment 13: 65352ms ~ 74280ms (file: 123-321-263-231-312-213-321-326-123-213-326-263-231-765-756-312-231-321-623-267-627-130-132-263-170-103-627-260-261-205-132-236-620-602-320-261-301-301-765-310-237-236-321-320-2_W.mp3)
  Segment 14: 75280ms ~ 75856ms (file: 713-715-715-751-715-176-173-175-175-175-175-175-715-175-176-175-175 173-175-176-175-173-175-173-175-715-175-157-175-175-176 137-137-173-173-715-715-715-173-175-175-173-137-175-1_W.mp3)
  Segment 15: 76856ms ~ 83768ms (file: 312-120-107-102-267-617-603-267-276-107-736-123-231-361-362-216-164-326-570-761-120-312-326-273-106-126-673-620-120-316-632-765-123-310-302-130-107-102-675-276-267-627-702-163-7_W.mp3)
  Segment 16: 84768ms ~ 85344ms (file: 637-521-576-576-705 631-762-627-752-267-312-203-231-523-725-267-726-570-261-251-753-576-217-702-271-267-720-736-675-506-573-762-267-257-672-372-257-567-576-765-572-520-321-576-1_V.mp3)
  Segment 17: 86344ms ~ 86920ms (file: 126-162-130-120-102-102-130-103-130-102-130-103-103-120-102-102-102-102-132-103-123-102-123-126-123-120-102-120-132-152-156-517-152-152-153-152-156-513-510-103-130-103-102-103-1_V.mp3)
  Segment 18: 87920ms ~ 88496ms (file: 173-157-735-715-517-537-170-173-753-571-157-571-510-513-175-570-750 517-576-576-157-135-310-710-513-573-571-750-517-571-513-571-357-571-715-573-357-357 507-570-517-517-170-301-5_V.mp3)
  Segment 19: 89496ms ~ 90072ms (file: 125-137-132-164-124-162-123-136-132-165-123-146-136_V.mp3)
  Segment 20: 91072ms ~ 91648ms (file: 571-570-751-571-751-751-573-576-570-571 570-571-571-576-571-570-157-751-517-750-571-570-170-751-175-175-571-157-751-576-175-173-157-751 574-574-574-574-574-574-574-574-571-574-5_W.mp3)
  Segment 21: 92648ms ~ 93224ms (file: 726-671-271-203-263-321-326-237-237-236-236-206-267-326-237-602-326-165-762-267-672-132 213-326-750-716-317-362-527-273-167-276-576-627-620-625-506-672-725-213-752-752-625-537-3_V.mp3)
  Segment 22: 94224ms ~ 94800ms (file: 136-317-132-132-173-317-136-316-317-716-317-316-317-317-317-361-132-137-317-173-137-130-137-132-135-137-173-371-135-235-312-172-126-175-167-175-173-176-176-137-127-153-175-751-7_W.mp3)
  Segment 23: 95800ms ~ 101812ms (file: 103-102-135-102-613-102-201-301-103-265-625-210-260-260-320-126-130-207-213-132-217-203-230-203-230-152-157-365-735-751-216-231-613-136-163-163-361-631-163-657-625-263-265-675-6_W.mp3)
  Segment 24: 102812ms ~ 103388ms (file: 751-571-571-751 571-571-571-571-573-571-571-571-571-571-517-571-571-571-574-574-571-574-574-517 574-574-574-574-574-574-574-574-574-574-571-571-574 570-573 571-547-547 574-574-5_W.mp3)
  Segment 25: 104388ms ~ 104964ms (file: 571-517-570-573-750-751 572-572-576-576-576-576-576-576-576-571-572 574-571-574-574-576-571-572-574-571-574-571-571-574-574-574-574_W.mp3)
  Segment 26: 105964ms ~ 106540ms (file: 571-576-571-756-754-756-517-351-675-732-567-657-256-162-612-617-165-651-165-516-765-576-576-572-573-571-526-572-562-756-752-756-756-753-572-756-752-756-765-756-576-754-756-567-5_W.mp3)
  Segment 27: 107540ms ~ 116216ms (file: 157-201-175-572-103-514-576-574-265-537-765-753-312-534-367-317-357-537-573-356-301-310-231-523-157-103-534-534-543-547-514-153-130-165-135-103-321-231-132-312-316-365-135-102-1_W.mp3)
  Segment 28: 117216ms ~ 117792ms (file: 517-571-751-750-351-713-750-753-730-713-701-103-102-762-172-756-701-753-507-706-270-107-721 576-571-753-571-751-507-170-735-735-573-751-701-572-570-570-750-753-571-756-571-576-5_V.mp3)
  Segment 29: 118792ms ~ 119368ms (file: 572-576-576-576-576-517-507-576-576-576-571-716_W.mp3)
  Segment 30: 120368ms ~ 120944ms (file: 765-756-756-754-675-561-561-352-126 576-756-576-576-132-612 714-751-751-153-571-357-576-576-362-751-675-675-651-351-574-573-756-735-756-753-576-576-576-756-567-573-537-137_W.mp3)
  Segment 31: 121944ms ~ 122520ms (file: 675-673-673-675-765-675-765-657 675 756-751-756_W.mp3)
  Segment 32: 123520ms ~ 124096ms (file: 572-372-275-670-367-526-351-173-750-132-207-726-235-726-236-267-653-267-576-271-520-136-167-375-756-253-572-752-326-572-237-237-327-236-267-216-235-572-325-312-235-312-726-657-6_V.mp3)
  Segment 33: 125096ms ~ 129884ms (file: 356-537-765-752-367-713-176-671-760-706-726-362-672-507-672-607-621-230-362-627-567-152-276-520-673-567-312-312-517-706-251-206-670-206-261-613-671-321-752-570-367-765-521-216-3_V.mp3)
  Segment 34: 130884ms ~ 131460ms (file: 576-657-652-156-162-705-657-167-156-756-356-143-176-173-170-107-516-570-715-170-715-751-715-175-715-751-675-756-567-561-157-165-156-571-657-625-256-267-256-652-561-657-756-356-1_V.mp3)
  Segment 35: 132460ms ~ 133036ms (file: 123 107-175-107-105-105 135-301-317-713-716-761-765-137-176-763-712-176-765-765-752-675-671-671-675-675-713-673-716-673-736-163-712-167-157-163-163-173-376-672-765-762-736-675-3_W.mp3)
  Segment 36: 134036ms ~ 134612ms (file: 512-157-752 572-572-571-751-571-751-571-715-756-752 150-510-517-157-571-150-157-571-157-175-503-315-157-571-715-170-571-753-175-175-135-317-130-712-371-375-371-710-713-357-756-7_V.mp3)
  Segment 37: 135612ms ~ 145260ms (file: 620-103-107-236-206-206-263-207-206-260-267-260-206-230-236-263-623-301-132-130-103-102-120-132-231-163-106-132-321-567-103-134-312-213-321-130-301-276-756-752-276-267-127-276-7_V.mp3)
  Segment 38: 146260ms ~ 151804ms (file: 123-312-107-103-270-237-705-106-620-107-273-250-675-627-672-573-573-257-231-261-352-517-237-276-263-327-507-237-273-750-301-130-372-537-107-167-357-765-375-137-107-362-570-375-2_W.mp3)
  Segment 39: 152804ms ~ 153380ms (file: 267-276-672-752-527-312-261-267-623-276-627-726-562-627-623-217-270-752-765-756-261-506-672-213-627-275-756-726-752-670-236-752-752-267 756-267-256-750-672-267-762-206-756-206-5_V.mp3)
  Segment 40: 154380ms ~ 154956ms (file: 317-730-165-317-267-716-637-132-572-765-570-751-572-562-751-675-265-752-756-132-752-623-762-702-672-652-367-163-321-132-135-517-537-570-756_W.mp3)
  Segment 41: 155956ms ~ 172300ms (file: 301-136-136-316-132-203-263-236-610-160-102-260-206-206-537-236-176-107-206-320-132-326-602-321-310-136-103-301-310-216-103-103-120 623-136-236-263-547-574-215-261 524-752-547-2_W.mp3)
  Segment 42: 173300ms ~ 173876ms (file: 312-135-316-321-216-765-132-623-637-135-136-135-316-361-135-617-163-376-631-361-316-617-671-576-615_W.mp3)
  Segment 43: 174876ms ~ 182760ms (file: 136-567-276-620-126-263-627-501-203-253-203-251-572-263-120-102-231-273-765-102-126-267-230-302-206-702-203-602-236-230-123-627-627-265-236-236-263-523-250-126-210-275-236-120-7_W.mp3)
  Segment 44: 183760ms ~ 184336ms (file: 750-571-750 571-571-571-175-573-751-571-517-751-751-571-571-751-576-715-751 574-571-571-571-571-576-571-571 576-574-571-573-537-573 574-574-574-574-574-574-571-756-750-570 573-5_W.mp3)
  Segment 45: 185336ms ~ 185912ms (file: 517-571-157-753-570 574-576-576-576-576-576-574-574-574-574-574 576-572-572-572-576-572-574-576-576-576-574-574_W.mp3)
  Segment 46: 186912ms ~ 193176ms (file: 657-670-567-267-263-103-320-201-310-230-203-542-620-130-275-217-203-213-312-312-705-751-751-507-647-625-570-517-612-207-257-250-260-230-103-543-213-203-203-103-263-267-260-207-2_W.mp3)
  Segment 47: 194176ms ~ 205300ms (file: 371-326-132-317-276-620-136-173-176-317-103-107-136-176-210-271-123-136-130-317-103-371-726-267-106-176-276-726-267-263-172-273-176-137-103-307-362-276-267-326-263-107-173-612-2_W.mp3)
  Segment 48: 206300ms ~ 206876ms (file: 167-675-617-632-136-312-132-136-136-136-123-136-137-136-106-130-167-673-132-653-136-301-165-752-635-761-267-372-231-627-260-672-576-576-512-516-315-310_V.mp3)
  Segment 49: 207876ms ~ 214680ms (file: 537-103-213-130-312-301-321-132-130-162-137-756-231-210-206-752-132-537-517-231-267-763-310-321-123-102-326-236-276-752-237-213-756-320-756-176-107-715-201-216-237-321-753-576-2_W.mp3)
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
> **Time**: 2026-04-06 08:38:16
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "그리고 이제 좀 그때는 이제 어렸지만 좀 크고 사춘기도 되고 그러면",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 110, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그때는", "start_ms": 1250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어렸지만", "start_ms": 2150, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2800, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "크고", "start_ms": 3100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사춘기도", "start_ms": 3600, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 4300, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 4750, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "자기만의 공간이 이제 필요한데 얼마나 좋아할까 얘네들이",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "자기만의", "start_ms": 80, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "공간이", "start_ms": 750, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1300, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "필요한데", "start_ms": 1700, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 2450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "좋아할까", "start_ms": 3000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "얘네들이", "start_ms": 3700, "dur_ms": 720, "phase_lr_deg": 0.0}
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
"transcript": "바닥 위에 상상으로 올렸던 우리 집 9년 전 시작부터 마지막까지를",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "mixed",
"words": [
{"word": "바닥", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "위에", "start_ms": 600, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "상상으로", "start_ms": 1050, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "올렸던", "start_ms": 1800, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 2400, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 2800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "9년", "start_ms": 3100, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 3650, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "시작부터", "start_ms": 3900, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "마지막까지를", "start_ms": 4750, "dur_ms": 950, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "모두 지켜볼 수 있을 것이라는 사실과 그 과정 속 그의 계속되는 진화를",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "모두", "start_ms": 50, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "지켜볼", "start_ms": 500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 1350, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "것이라는", "start_ms": 1750, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "사실과", "start_ms": 2450, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "과정", "start_ms": 3250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "속", "start_ms": 3750, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "그의", "start_ms": 3980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계속되는", "start_ms": 4350, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "진화를", "start_ms": 5200, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "전부 목격해왔다는 사실은 우리에게 더없는 행",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "전부", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "목격해왔다는", "start_ms": 520, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "사실은", "start_ms": 1650, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "우리에게", "start_ms": 2350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "더없는", "start_ms": 3050, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "행", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "벽 하나하나 했던 것 같아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "벽", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하나하나", "start_ms": 550, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "했던", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1800, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 2050, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "와 이거 뭐 눈물 없이는 들을 수 없는 얘기네 이거 와",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 750, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "눈물", "start_ms": 950, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "없이는", "start_ms": 1450, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "들을", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2480, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 2680, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "얘기네", "start_ms": 3150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 3850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 4200, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "봄 여름 가을 겨울 초까지 해서 4계절 동안 골조 작업만 했습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "봄", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여름", "start_ms": 580, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "가을", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "겨울", "start_ms": 1550, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "초까지", "start_ms": 2080, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 2780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "4계절", "start_ms": 3280, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "골조", "start_ms": 4500, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "작업만", "start_ms": 5050, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "했습니다", "start_ms": 5780, "dur_ms": 820, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "와",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "어그로는 저리가라고 할 정도로 지저분하고 조잡한 타블로이드성 헤드라인만 뽑아내는데 특히나 국내 축구 기사는 클릭도 하기 싫을 정도로 혐오스",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "어그로는", "start_ms": 100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "저리가라고", "start_ms": 780, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1650, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 1900, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "지저분하고", "start_ms": 2550, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "조잡한", "start_ms": 3450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "타블로이드성", "start_ms": 4000, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "헤드라인만", "start_ms": 5000, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "뽑아내는데", "start_ms": 5850, "dur_ms": 880, "phase_lr_deg": 0.0},
{"word": "특히나", "start_ms": 6800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "국내", "start_ms": 7400, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "축구", "start_ms": 7800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "기사는", "start_ms": 8250, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "클릭도", "start_ms": 8800, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 9400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싫을", "start_ms": 9800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 10300, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "혐오스", "start_ms": 10900, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "천장은 각관 틀 사이부분만 감사고 박공 지붕 모양을 그대로 드러내 개방감을 높였는데요 이런 천장",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "mixed",
"words": [
{"word": "천장은", "start_ms": 110, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "각관", "start_ms": 800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "틀", "start_ms": 1250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "사이부분만", "start_ms": 1500, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "감사고", "start_ms": 2400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "박공", "start_ms": 2950, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "지붕", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모양을", "start_ms": 3900, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "드러내", "start_ms": 5050, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "개방감을", "start_ms": 5650, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "높였는데요", "start_ms": 6400, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 7300, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "천장", "start_ms": 7700, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "계기가 됐습니다 이에 대해 대만 당국은 자국의 정체성과 존엄을 지키겠다는 명분으로 대응에 나섰습니다 문제는 그 대응 방식이었습니다 대만은 한국을 포함한 일부 국가를 상대로 도저히 이해할 수",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "계기가", "start_ms": 60, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "됐습니다", "start_ms": 600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이에", "start_ms": 1300, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 1650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 2050, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "당국은", "start_ms": 2500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자국의", "start_ms": 3100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "정체성과", "start_ms": 3600, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "존엄을", "start_ms": 4400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "지키겠다는", "start_ms": 4950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "명분으로", "start_ms": 5750, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "대응에", "start_ms": 6450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "나섰습니다", "start_ms": 7000, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "문제는", "start_ms": 7850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 8450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대응", "start_ms": 8650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "방식이었습니다", "start_ms": 9100, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "대만은", "start_ms": 10100, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 10700, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "포함한", "start_ms": 11250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 11850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "국가를", "start_ms": 12300, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "상대로", "start_ms": 12850, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "도저히", "start_ms": 13450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이해할", "start_ms": 14050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 14600, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Everything everything rice kkakdugi minari not just naughty minari so naughty minari",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 150,
"audio_type": "mixed",
"words": [
{"word": "Everything", "start_ms": 150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "everything", "start_ms": 850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "rice", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "kkakdugi", "start_ms": 2050, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 2800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 3500, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 3850, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "naughty", "start_ms": 4250, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 4800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 5500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "naughty", "start_ms": 5800, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "해서 말인데 손흥민 선수는 원래 호날두를 롤모델로 했었죠 그래서 정말 호날두처럼 빠른 발과 강력한 슛 그리고 완벽한 마무리 능력까",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 100, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "말인데", "start_ms": 500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "손흥민", "start_ms": 1100, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "선수는", "start_ms": 1700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "원래", "start_ms": 2300, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "호날두를", "start_ms": 2750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "롤모델로", "start_ms": 3450, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "했었죠", "start_ms": 4200, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 4850, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 5400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "호날두처럼", "start_ms": 5900, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "빠른", "start_ms": 6700, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "발과", "start_ms": 7150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "강력한", "start_ms": 7650, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "슛", "start_ms": 8250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 8650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "완벽한", "start_ms": 9150, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "마무리", "start_ms": 9800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "능력까", "start_ms": 10380, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "하 정말 목공을 이렇게 많이 다루시는 선생님께서 얘기를 하시니까 굉장히 설득력 있게 들리네요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "하", "start_ms": 120, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "목공을", "start_ms": 950, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1550, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2050, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "다루시는", "start_ms": 2450, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "선생님께서", "start_ms": 3150, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "얘기를", "start_ms": 4000, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하시니까", "start_ms": 4550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 5250, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "설득력", "start_ms": 5850, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 6500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들리네요", "start_ms": 6900, "dur_ms": 680, "phase_lr_deg": 0.0}
]
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
"transcript": "What makes us humans humans, right? That's that's my biggest worry is it's not so much a worry I think we will figure it out but it creates discomfort in the process as we",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 180, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "makes", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "us", "start_ms": 850, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "humans", "start_ms": 1050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "humans,", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "right?", "start_ms": 2200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "That's", "start_ms": 2650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 3450, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "biggest", "start_ms": 3700, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "worry", "start_ms": 4200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 4650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 4850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 5150, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 5400, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "much", "start_ms": 5650, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 5950, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "worry", "start_ms": 6100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 6500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 6680, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 7000, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 7200, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "figure", "start_ms": 7450, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 7850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "out", "start_ms": 8050, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "but", "start_ms": 8380, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 8620, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "creates", "start_ms": 8800, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "discomfort", "start_ms": 9300, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 10050, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 10250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "process", "start_ms": 10450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "as", "start_ms": 11000, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 11200, "dur_ms": 210, "phase_lr_deg": 0.0}
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
"transcript": "후에는 이제는 좀 빠진 것들이 많이 있어서 고것까지 대략 하면 그정도 나오지 않았을까 생각이 들더라고요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "후에는", "start_ms": 150, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이제는", "start_ms": 680, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "빠진", "start_ms": 1380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것들이", "start_ms": 1880, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2450, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 2880, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "고것까지", "start_ms": 3480, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "대략", "start_ms": 4200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 4680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그정도", "start_ms": 5180, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나오지", "start_ms": 5780, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "않았을까", "start_ms": 6350, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 7100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "들더라고요", "start_ms": 7700, "dur_ms": 820, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "그래도 2층에다가 120평인데 2억이면은 네 돈 벌었다고 생각",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그래도", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "2층에다가", "start_ms": 620, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "120평인데", "start_ms": 1400, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "2억이면은", "start_ms": 2400, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 3280, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 3580, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "벌었다고", "start_ms": 3900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "생각", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "까지 해서 다음날인 d+2일차 아침에 갭 상승했을 때 이렇게 수익 실현해서 추가로 1억 이상 수익을",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "까지", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 480, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "다음날인", "start_ms": 950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "d+2일차", "start_ms": 1650, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 2500, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "갭", "start_ms": 3080, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "상승했을", "start_ms": 3380, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 4100, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 4350, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "수익", "start_ms": 4880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "실현해서", "start_ms": 5350, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "추가로", "start_ms": 6150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "1억", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 7150, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 7650, "dur_ms": 580, "phase_lr_deg": 0.0}
]
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
"transcript": "I know it's getting in the sweet what is it that",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 110,
"audio_type": "mixed",
"words": [
{"word": "I", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 300, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 650, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "getting", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1400, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "sweet", "start_ms": 1880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 2350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 2720, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 2980, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 3200, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "이와 함께 천궁2 추가 포대 배치도 빠르게 진행되고 있는데요 이미 세 번째 포대 장비가 중동 지역에 도착했고 조만간 실전 운용에 들어갈 예정입니다 이스라",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "이와", "start_ms": 90, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 520, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "천궁2", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "추가", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "포대", "start_ms": 2100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "배치도", "start_ms": 2650, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 3350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "진행되고", "start_ms": 3950, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "있는데요", "start_ms": 4750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 5450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 5900, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 6150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "포대", "start_ms": 6650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "장비가", "start_ms": 7180, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 7850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지역에", "start_ms": 8350, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "도착했고", "start_ms": 8950, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "조만간", "start_ms": 9800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "실전", "start_ms": 10500, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "운용에", "start_ms": 11050, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "들어갈", "start_ms": 11750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "예정입니다", "start_ms": 12350, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "이스라", "start_ms": 13250, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "변화는 벌써 시작됐다 나에 대해 너무 잘 알고 있는 AI 비서인 우리 삶을",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "mixed",
"words": [
{"word": "변화는", "start_ms": 120, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "시작됐다", "start_ms": 1200, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "나에", "start_ms": 2000, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 2450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3350, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "알고", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 4500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "비서인", "start_ms": 4950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 5650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "삶을", "start_ms": 6100, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "이마 아미 이만오천엔 마다마다 이고 이마 이고 이만오천엔 이였는데 보토 이만큼 나와요 그돈으로 금으로",
"speaker": "speaker_9",
"confidence": 0.88,
"language": "ja",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이마", "start_ms": 150, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아미", "start_ms": 600, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이만오천엔", "start_ms": 1050, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "마다마다", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이고", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이마", "start_ms": 3200, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이고", "start_ms": 3650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이만오천엔", "start_ms": 4150, "dur_ms": 880, "phase_lr_deg": 0.0},
{"word": "이였는데", "start_ms": 5100, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "보토", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이만큼", "start_ms": 6400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "나와요", "start_ms": 7100, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "그돈으로", "start_ms": 7750, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "금으로", "start_ms": 8550, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "그의 야심을 알려면 다시 그 한국이 자체 개발한 kf-21 보라매 전투기로 돌아가야 합니다 이 전투기는 결코 만만한 배경을 가진 기체가 아닙니다 2001년에 구상이 처",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "그의", "start_ms": 80, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "야심을", "start_ms": 450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "알려면", "start_ms": 1050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 1750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국이", "start_ms": 2420, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자체", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "개발한", "start_ms": 3550, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "kf-21", "start_ms": 4250, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "보라매", "start_ms": 5150, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "전투기로", "start_ms": 5750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "돌아가야", "start_ms": 6550, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 7300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 7900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전투기는", "start_ms": 8100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "결코", "start_ms": 8800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "만만한", "start_ms": 9300, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "배경을", "start_ms": 9900, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "가진", "start_ms": 10550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기체가", "start_ms": 11050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 11650, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "2001년에", "start_ms": 12400, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "구상이", "start_ms": 13300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "처", "start_ms": 13900, "dur_ms": 320, "phase_lr_deg": 0.0}
]
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:38:16*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=7628  time=08:34:39  uptime=16403s  mem=207MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_083213.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 702-157-713 571-571-571-574-570-571-570-576-571-571-751-517-157-157-150-103-571-523-753-751-157-710-173-317-215-137 573-571-572-571-150-170-157-571-571-150-713-576-103-710-725-5_W.mp3)
  Segment 2: 1576ms ~ 10648ms (file: 123-751-103-207-270-207-236-275-120-210-210-216-720-721-276-276-267-217-201-127-213-721-726-260-260-527-527-574-172-127-275-275-276-213-312-231-217-213-275-213-230-321-102-201-1_W.mp3)
  Segment 3: 11648ms ~ 19532ms (file: 756-103-123-103-315-537-574-130-267-756-173-103-310-107-726-130-176-716-106-271-312-132-312-136-617-130-351-257-106-167-123-167-631-107-571-316-231-312-130-627-260-576-673-763-1_W.mp3)
  Segment 4: 20532ms ~ 21108ms (file: 753-761 751-175-571-157-517-715-173-170-170-107-130-137-730-710-710-301-170-175-716 175-571-571-753-751-571-571-571-715-173-715-731-750-731-175-175-170-715-753-751-756-751-725-1_W.mp3)
  Segment 5: 22108ms ~ 22684ms (file: 371-762-716-756-756-765-756-156-657-576-657-564-561-517-567_W.mp3)
  Segment 6: 23684ms ~ 24260ms (file: 571-517-570-750-756-715-573-576-751-571-750-576-571-751-751-571-576-571_W.mp3)
  Segment 7: 25260ms ~ 25836ms (file: 574-574-574-574-574-574-574-574-574-574 574-574-574-574-574-574-574-574 574-574-574-574-574-574-574-574-574-574-571-571-751-571-517-571 574-574-574-574-574-574-574-574-570-570-5_W.mp3)
  Segment 8: 26836ms ~ 34216ms (file: 276-726-276-762-276-276-267-276-276-276-276-627-267-267-106-751-752-102-130-751-106-260-206-230-206-213-231-123-261-103-107-236-263-273-210-756-130-106-136-164-143-132-106-107-5_W.mp3)
  Segment 9: 35216ms ~ 41768ms (file: 236-301-621-632-756-765-263-260-275-316-362-263-362-625-260-260-270-273-725-752-351-671-170-136-176-132-610-237-237-576-625-102-265-257-567-270-203-263-213-216-321-231-326-163-2_W.mp3)
  Segment 10: 42768ms ~ 43344ms (file: 537-573-351-573-537-130-105-310-130-103-157-105-517-105-153-157-103-105-135-150-135-175-157-537-513-573-571-537-531-173-571-576-573 135-150-157-105-301-107-153-130-157-105-103-1_V.mp3)
  Segment 11: 44344ms ~ 50572ms (file: 756-102-312-276-132-320-317-206-735-756-627-257-206-263-123-607-260-267-263-167-627-302-507-132-265-103-236-506-206-321-203-230-752-756-273-270-756-275-276-213-213-761-213-231-2_W.mp3)
  Segment 12: 51572ms ~ 52148ms (file: 765-172-506-251-275-521-257-725-275-625-275-267-236-507-325-576-527-325-572-563-352 763-267-756-136-172_V.mp3)
  Segment 13: 53148ms ~ 53724ms (file: 627-627-316-316-136-316-361-316-136-312-136-361-316-752-173-162-576-157-125-761-271-125-162-625-125-612-175-176-175-156-167-175-157-137-751-725-152-167-751_W.mp3)
  Segment 14: 54724ms ~ 62212ms (file: 132-267-206-123-270-312-726-127-321-267-627-621-230-317-275-231-216-310-126-570-607-236-320-106-217-725-675-123-123-316-367-123-236-623-136-736-572-623-231-627-231-271-126-216-7_W.mp3)
  Segment 15: 63212ms ~ 63788ms (file: 675-567-756-750-705-735-703-765-276-761-765-675-760-760-765-670-765-736-760-765 761-751-741_W.mp3)
  Segment 16: 64788ms ~ 74508ms (file: 715-237-103-103-201-321-672-231-103-132-103-203-320-302-754-102-230-574-203-230-203-320-230-203-236-756-720-213-750-301-756-756-137-206-657-657-237-217-201-230-203-106-753-754-1_W.mp3)
  Segment 17: 75508ms ~ 76084ms (file: 176-715-176-713-175-763-716-716-715 176-165-701-173-137-105-170-170-710-710-710-710-175-751-716-175-716-173-176-713-176-176-176-175-176-710 173-175-710-751-705-175-176-710-715-7_V.mp3)
  Segment 18: 77084ms ~ 77660ms (file: 265-765-567-537-351-715-561-526-567-756-576-562-567-576-156-156-125-751-567-157-167-156-165-651-521-571-756-761-750-567-157-715-715-715-751-750-715-701-715-710-173-715-157-751-1_W.mp3)
  Segment 19: 78660ms ~ 85536ms (file: 136-627-107-302-230-302-267-607-574-547-576-537-576-312-675-302-362-301-263-306-132-173-713-317-130-132-102-102-162-231-263-132-130-316-371-316-106-230-132-312-320-320-312-103-3_W.mp3)
  Segment 20: 86536ms ~ 91072ms (file: 267-276-765-765-762-267-265-210-271-235-231-210-524-312-574-310-237-627-263-213-213-627-276-263-762-763-716-761-763-276-267-765-762-763-765-761-762-167-761-765-761-762-267-756-6_W.mp3)
  Segment 21: 92072ms ~ 92648ms (file: 163-631-153-263-207-367-316-357-263-263-356-562-625-251-326-672-267-236-271-267-263-670-236-267-263-371-731-367-167-137-317-762-576-157-725-362-765-672-265-625-327-570-276-376-5_V.mp3)
  Segment 22: 93648ms ~ 94224ms (file: 167-675-617-632-136-312-132-136-136-136-123-136-137-136-106-130-167-673-132-653-136-301-165-752-635-761-267-372-231-627-260-672-576-576-512-516-315-310_V.mp3)
  Segment 23: 95224ms ~ 95800ms (file: 765-765-765-765-765-765-765-756-765-762-765-756-751-573-157-517-257-127-125-123-125-172-217-572-752-725-751-752-752-715-751-135-157-735-715-175-357-537-351-153-157-536-576-513-5_W.mp3)
  Segment 24: 96800ms ~ 105080ms (file: 173-130-267-130-126-160-103-301-312-263-627-261-130-301-103-276-326-236-326-326-230-731-107-230-103-316-306-130-203-367-321-320-203-103-106-570-657-276-726-673-167-167-136-176-2_V.mp3)
  Segment 25: 106080ms ~ 106656ms (file: 102 231 132 126-156-613-321-316-132-321-326-136_V.mp3)
  Segment 26: 107656ms ~ 108232ms (file: 517-571-157-753-570 574-576-576-576-576-576-574-574-574-574-574 576-572-572-572-576-572-574-576-576-576-574-574_W.mp3)
  Segment 27: 109232ms ~ 109808ms (file: 751-750-750-756-765-756-756-756-576-765-675-675-657-675-567-756-576-756-576-573-576-576-756-576-576-576-756-576-576-756-567-756-571-657-265-576-752-316-316-361-136-316-316-312-5_W.mp3)
  Segment 28: 110808ms ~ 111384ms (file: 217-257-261-602-263-236-526-672-623-216-260-136-267-625-256-762-267-263-756-567-263-236-537-315-351-531-573-573-315-327-367-256-325-352-351-573-316-567-365-673-513-135-576-756-1_W.mp3)
  Segment 29: 112384ms ~ 112960ms (file: 571-153-751-571-751-571-571-571 571-571-571-517-517-517-175-573-517-751-751-517-751-571-517 571-571-571-517-571-571-574-571 574-574-571-571-574-574-571-574-571-573-576-574-517-5_W.mp3)
  Segment 30: 113960ms ~ 121340ms (file: 672-726-267-273-276-120-213-127-721-723-752-276-236-103-206-762-160-630-672-672-160-621-267-216-752-756-762-762-216-276-257-126-106-765-276-617-176-752-275-126-752-276-276-671-7_W.mp3)
  Segment 31: 122340ms ~ 131700ms (file: 102-103-172-176-607-263-201-216-102-312-271-207-237-103-167-162-623-216-123-132-207-270-107-132-312-143-132-271-621-627-673-362-312-123-731-376-130-537-752-370-137-143-102-175-1_W.mp3)
  Segment 32: 132700ms ~ 139396ms (file: 106-167-106-175-576-130-267-102-263-236-756-725-236-753-137-132-103-263-263-126-321-261-123-167-756-765-756-517-106-753-106-267-137-103-176-237-123-103-103-527-267-327-130-263-1_W.mp3)
  Segment 33: 140396ms ~ 140972ms (file: 716-751-150-175-136-136-163-163-163-163-163-175-317-137 361-532-312-132-132-132-137-137-137-137-135-137-137-761-176-765-761-716-736-175-715-715-513-517-571-715-710-173-137-132-1_W.mp3)
  Segment 34: 141972ms ~ 147732ms (file: 361-361-672-127-762-276-627-675-276-236-216-201-236-267-263-267-721-756-672-267-372-213-160-627-627-703-361-563-236-623-672-527-762-276-671-276-267-612-206-267-263-263-231-756-2_W.mp3)
  Segment 35: 148732ms ~ 158344ms (file: 760-136-216-107-675-106-201-203-103-230-102-260-120-102-135-132-162-106-130-231-321-230-230-320-106-126-106-102-267-256-216-312-321-210-123-106-206-201-130-235-312-102-312-321-3_V.mp3)
  Segment 36: 159344ms ~ 166724ms (file: 263-160-326-763-567-372-237-675-623-160-762-276-276-237-632-601-267-610-120-203-103-327-216-106-617-312-163-756-765-267-276-371-132-267-236-236-237-237-132-327-176-765-267-213-2_W.mp3)
  Segment 37: 167724ms ~ 168300ms (file: 135-312-310-132-130-316-176-167-153-367-316-507-506-315-157-315-301-317-310-317-312-312-136-132-136-312-132-136-312-317-132-137-317-317-137-317-132-132-574-375-751-573-135-130-3_W.mp3)
  Segment 38: 169300ms ~ 169876ms (file: 537-627-163-136-163-361-316-613-136-136-136-176-716-172-716-173-176-137-765-765-765-765-763-675-675-673-671-761-763-162-675-675-761-675-765-763-765_W.mp3)
  Segment 39: 170876ms ~ 174152ms (file: 351-576-261-563-657-632-613-216-315-132-312-123-123-132-123-316-136-315-563-536-315-135-315-365-316_W.mp3)
  Segment 40: 175152ms ~ 175728ms (file: 750 574-574-571-574-571-574-571-573-571-571-574-574-571-574-574-574-572-574-574-571_W.mp3)
  Segment 41: 176728ms ~ 182128ms (file: 103-106-756-756-103-701-137-130-213-123-627-135-312-752-725-507-765-750-735-607-526-527-607-602-257-257-205-267-275-205-250-205-265-257-257-257-257-276-265-726-725-725-216-201-6_W.mp3)
  Segment 42: 183128ms ~ 183704ms (file: 751-571-571-751 571-571-571-571-573-571-571-571-571-571-517-571-571-571-574-574-571-574-574-517 574-574-574-574-574-574-574-574-574-574-571-571-574 570-573 571-547-547 574-574-5_W.mp3)
  Segment 43: 184704ms ~ 201048ms (file: 301-136-136-316-132-203-263-236-610-160-102-260-206-206-537-236-176-107-206-320-132-326-602-321-310-136-103-301-310-216-103-103-120 623-136-236-263-547-574-215-261 524-752-547-2_W.mp3)
  Segment 44: 202048ms ~ 202624ms (file: 713-735-756-571-375-753-571-750-750-517-570-570-756-756 574-574-574-574-574-574-547-547-574-573 571-574-574-573 572-571-574-574-574-574-571 547-574-574-574-574-574-574-576-574-5_W.mp3)
  Segment 45: 203624ms ~ 204200ms (file: 672-765-765-675-162-165-162-136-106-132-367-763-672-716-167-165-567-675-671-163-613-613-613-612-613-613-613-612-613-617-617-613-613-631-613-631-613-613-617-123-103-130-316-163-1_W.mp3)
  Segment 46: 205200ms ~ 210816ms (file: 261-256-217-127-216-320-216-126-321-576-527-213-206-217-162-136-127-513-106-203-216-621-231-562-302-302-102-132-126-123-312-216-216-213-231-213-362-216-216-517-316-712-130-203-7_W.mp3)
  Segment 47: 211816ms ~ 212392ms (file: 572-136-235-725-762-256-213-267-312-207-276-136-651-132-570-362-507-372-652-507-576-276-267-506-163-725-326-123-372-371-275-570-756-725-752-327-563-576 236-162-526-705-257-576-2_V.mp3)
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
> **Time**: 2026-04-06 08:38:19
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "그리고 이제 좀 그때는 이제 어렸지만 좀 크고 사춘기도 되고 그러면",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 110, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그때는", "start_ms": 1250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어렸지만", "start_ms": 2150, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2800, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "크고", "start_ms": 3100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사춘기도", "start_ms": 3600, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 4300, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 4750, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "자기만의 공간이 이제 필요한데 얼마나 좋아할까 얘네들이",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "자기만의", "start_ms": 80, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "공간이", "start_ms": 750, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1300, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "필요한데", "start_ms": 1700, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 2450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "좋아할까", "start_ms": 3000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "얘네들이", "start_ms": 3700, "dur_ms": 720, "phase_lr_deg": 0.0}
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
"transcript": "바닥 위에 상상으로 올렸던 우리 집 9년 전 시작부터 마지막까지를",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "mixed",
"words": [
{"word": "바닥", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "위에", "start_ms": 600, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "상상으로", "start_ms": 1050, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "올렸던", "start_ms": 1800, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 2400, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 2800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "9년", "start_ms": 3100, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 3650, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "시작부터", "start_ms": 3900, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "마지막까지를", "start_ms": 4750, "dur_ms": 950, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "모두 지켜볼 수 있을 것이라는 사실과 그 과정 속 그의 계속되는 진화를",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "모두", "start_ms": 50, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "지켜볼", "start_ms": 500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 1350, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "것이라는", "start_ms": 1750, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "사실과", "start_ms": 2450, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "과정", "start_ms": 3250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "속", "start_ms": 3750, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "그의", "start_ms": 3980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계속되는", "start_ms": 4350, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "진화를", "start_ms": 5200, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "전부 목격해왔다는 사실은 우리에게 더없는 행",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "전부", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "목격해왔다는", "start_ms": 520, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "사실은", "start_ms": 1650, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "우리에게", "start_ms": 2350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "더없는", "start_ms": 3050, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "행", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "벽 하나하나 했던 것 같아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "벽", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하나하나", "start_ms": 550, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "했던", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1800, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 2050, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "와 이거 뭐 눈물 없이는 들을 수 없는 얘기네 이거 와",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 750, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "눈물", "start_ms": 950, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "없이는", "start_ms": 1450, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "들을", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2480, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 2680, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "얘기네", "start_ms": 3150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 3850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 4200, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "봄 여름 가을 겨울 초까지 해서 4계절 동안 골조 작업만 했습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "봄", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여름", "start_ms": 580, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "가을", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "겨울", "start_ms": 1550, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "초까지", "start_ms": 2080, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 2780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "4계절", "start_ms": 3280, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "골조", "start_ms": 4500, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "작업만", "start_ms": 5050, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "했습니다", "start_ms": 5780, "dur_ms": 820, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "와",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "어그로는 저리가라고 할 정도로 지저분하고 조잡한 타블로이드성 헤드라인만 뽑아내는데 특히나 국내 축구 기사는 클릭도 하기 싫을 정도로 혐오스",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "어그로는", "start_ms": 100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "저리가라고", "start_ms": 780, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1650, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 1900, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "지저분하고", "start_ms": 2550, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "조잡한", "start_ms": 3450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "타블로이드성", "start_ms": 4000, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "헤드라인만", "start_ms": 5000, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "뽑아내는데", "start_ms": 5850, "dur_ms": 880, "phase_lr_deg": 0.0},
{"word": "특히나", "start_ms": 6800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "국내", "start_ms": 7400, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "축구", "start_ms": 7800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "기사는", "start_ms": 8250, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "클릭도", "start_ms": 8800, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 9400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싫을", "start_ms": 9800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 10300, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "혐오스", "start_ms": 10900, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "천장은 각관 틀 사이부분만 감사고 박공 지붕 모양을 그대로 드러내 개방감을 높였는데요 이런 천장",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "mixed",
"words": [
{"word": "천장은", "start_ms": 110, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "각관", "start_ms": 800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "틀", "start_ms": 1250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "사이부분만", "start_ms": 1500, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "감사고", "start_ms": 2400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "박공", "start_ms": 2950, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "지붕", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모양을", "start_ms": 3900, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "드러내", "start_ms": 5050, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "개방감을", "start_ms": 5650, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "높였는데요", "start_ms": 6400, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 7300, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "천장", "start_ms": 7700, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "계기가 됐습니다 이에 대해 대만 당국은 자국의 정체성과 존엄을 지키겠다는 명분으로 대응에 나섰습니다 문제는 그 대응 방식이었습니다 대만은 한국을 포함한 일부 국가를 상대로 도저히 이해할 수",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "계기가", "start_ms": 60, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "됐습니다", "start_ms": 600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이에", "start_ms": 1300, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 1650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 2050, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "당국은", "start_ms": 2500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자국의", "start_ms": 3100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "정체성과", "start_ms": 3600, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "존엄을", "start_ms": 4400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "지키겠다는", "start_ms": 4950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "명분으로", "start_ms": 5750, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "대응에", "start_ms": 6450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "나섰습니다", "start_ms": 7000, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "문제는", "start_ms": 7850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 8450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대응", "start_ms": 8650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "방식이었습니다", "start_ms": 9100, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "대만은", "start_ms": 10100, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 10700, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "포함한", "start_ms": 11250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 11850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "국가를", "start_ms": 12300, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "상대로", "start_ms": 12850, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "도저히", "start_ms": 13450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이해할", "start_ms": 14050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 14600, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Everything everything rice kkakdugi minari not just naughty minari so naughty minari",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 150,
"audio_type": "mixed",
"words": [
{"word": "Everything", "start_ms": 150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "everything", "start_ms": 850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "rice", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "kkakdugi", "start_ms": 2050, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 2800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 3500, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 3850, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "naughty", "start_ms": 4250, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 4800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 5500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "naughty", "start_ms": 5800, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "해서 말인데 손흥민 선수는 원래 호날두를 롤모델로 했었죠 그래서 정말 호날두처럼 빠른 발과 강력한 슛 그리고 완벽한 마무리 능력까",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 100, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "말인데", "start_ms": 500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "손흥민", "start_ms": 1100, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "선수는", "start_ms": 1700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "원래", "start_ms": 2300, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "호날두를", "start_ms": 2750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "롤모델로", "start_ms": 3450, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "했었죠", "start_ms": 4200, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 4850, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 5400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "호날두처럼", "start_ms": 5900, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "빠른", "start_ms": 6700, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "발과", "start_ms": 7150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "강력한", "start_ms": 7650, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "슛", "start_ms": 8250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 8650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "완벽한", "start_ms": 9150, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "마무리", "start_ms": 9800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "능력까", "start_ms": 10380, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "하 정말 목공을 이렇게 많이 다루시는 선생님께서 얘기를 하시니까 굉장히 설득력 있게 들리네요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "하", "start_ms": 120, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "목공을", "start_ms": 950, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1550, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2050, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "다루시는", "start_ms": 2450, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "선생님께서", "start_ms": 3150, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "얘기를", "start_ms": 4000, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하시니까", "start_ms": 4550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 5250, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "설득력", "start_ms": 5850, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 6500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들리네요", "start_ms": 6900, "dur_ms": 680, "phase_lr_deg": 0.0}
]
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
"transcript": "What makes us humans humans, right? That's that's my biggest worry is it's not so much a worry I think we will figure it out but it creates discomfort in the process as we",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 180, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "makes", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "us", "start_ms": 850, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "humans", "start_ms": 1050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "humans,", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "right?", "start_ms": 2200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "That's", "start_ms": 2650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 3450, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "biggest", "start_ms": 3700, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "worry", "start_ms": 4200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 4650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 4850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 5150, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 5400, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "much", "start_ms": 5650, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 5950, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "worry", "start_ms": 6100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 6500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 6680, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 7000, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 7200, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "figure", "start_ms": 7450, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 7850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "out", "start_ms": 8050, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "but", "start_ms": 8380, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 8620, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "creates", "start_ms": 8800, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "discomfort", "start_ms": 9300, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 10050, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 10250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "process", "start_ms": 10450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "as", "start_ms": 11000, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 11200, "dur_ms": 210, "phase_lr_deg": 0.0}
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
"transcript": "후에는 이제는 좀 빠진 것들이 많이 있어서 고것까지 대략 하면 그정도 나오지 않았을까 생각이 들더라고요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "후에는", "start_ms": 150, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이제는", "start_ms": 680, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "빠진", "start_ms": 1380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것들이", "start_ms": 1880, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2450, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 2880, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "고것까지", "start_ms": 3480, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "대략", "start_ms": 4200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 4680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그정도", "start_ms": 5180, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나오지", "start_ms": 5780, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "않았을까", "start_ms": 6350, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 7100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "들더라고요", "start_ms": 7700, "dur_ms": 820, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "그래도 2층에다가 120평인데 2억이면은 네 돈 벌었다고 생각",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그래도", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "2층에다가", "start_ms": 620, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "120평인데", "start_ms": 1400, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "2억이면은", "start_ms": 2400, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 3280, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 3580, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "벌었다고", "start_ms": 3900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "생각", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "까지 해서 다음날인 d+2일차 아침에 갭 상승했을 때 이렇게 수익 실현해서 추가로 1억 이상 수익을",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "까지", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 480, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "다음날인", "start_ms": 950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "d+2일차", "start_ms": 1650, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 2500, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "갭", "start_ms": 3080, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "상승했을", "start_ms": 3380, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 4100, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 4350, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "수익", "start_ms": 4880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "실현해서", "start_ms": 5350, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "추가로", "start_ms": 6150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "1억", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 7150, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 7650, "dur_ms": 580, "phase_lr_deg": 0.0}
]
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
"transcript": "I know it's getting in the sweet what is it that",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 110,
"audio_type": "mixed",
"words": [
{"word": "I", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 300, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 650, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "getting", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1400, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "sweet", "start_ms": 1880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 2350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 2720, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 2980, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 3200, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "이와 함께 천궁2 추가 포대 배치도 빠르게 진행되고 있는데요 이미 세 번째 포대 장비가 중동 지역에 도착했고 조만간 실전 운용에 들어갈 예정입니다 이스라",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "이와", "start_ms": 90, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 520, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "천궁2", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "추가", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "포대", "start_ms": 2100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "배치도", "start_ms": 2650, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 3350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "진행되고", "start_ms": 3950, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "있는데요", "start_ms": 4750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 5450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 5900, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 6150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "포대", "start_ms": 6650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "장비가", "start_ms": 7180, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 7850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지역에", "start_ms": 8350, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "도착했고", "start_ms": 8950, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "조만간", "start_ms": 9800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "실전", "start_ms": 10500, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "운용에", "start_ms": 11050, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "들어갈", "start_ms": 11750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "예정입니다", "start_ms": 12350, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "이스라", "start_ms": 13250, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "변화는 벌써 시작됐다 나에 대해 너무 잘 알고 있는 AI 비서인 우리 삶을",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "mixed",
"words": [
{"word": "변화는", "start_ms": 120, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "시작됐다", "start_ms": 1200, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "나에", "start_ms": 2000, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 2450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3350, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "알고", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 4500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "비서인", "start_ms": 4950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 5650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "삶을", "start_ms": 6100, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "이마 아미 이만오천엔 마다마다 이고 이마 이고 이만오천엔 이였는데 보토 이만큼 나와요 그돈으로 금으로",
"speaker": "speaker_9",
"confidence": 0.88,
"language": "ja",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이마", "start_ms": 150, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아미", "start_ms": 600, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이만오천엔", "start_ms": 1050, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "마다마다", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이고", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이마", "start_ms": 3200, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이고", "start_ms": 3650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이만오천엔", "start_ms": 4150, "dur_ms": 880, "phase_lr_deg": 0.0},
{"word": "이였는데", "start_ms": 5100, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "보토", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이만큼", "start_ms": 6400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "나와요", "start_ms": 7100, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "그돈으로", "start_ms": 7750, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "금으로", "start_ms": 8550, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "그의 야심을 알려면 다시 그 한국이 자체 개발한 kf-21 보라매 전투기로 돌아가야 합니다 이 전투기는 결코 만만한 배경을 가진 기체가 아닙니다 2001년에 구상이 처",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "그의", "start_ms": 80, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "야심을", "start_ms": 450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "알려면", "start_ms": 1050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 1750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국이", "start_ms": 2420, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자체", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "개발한", "start_ms": 3550, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "kf-21", "start_ms": 4250, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "보라매", "start_ms": 5150, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "전투기로", "start_ms": 5750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "돌아가야", "start_ms": 6550, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 7300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 7900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전투기는", "start_ms": 8100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "결코", "start_ms": 8800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "만만한", "start_ms": 9300, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "배경을", "start_ms": 9900, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "가진", "start_ms": 10550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기체가", "start_ms": 11050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 11650, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "2001년에", "start_ms": 12400, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "구상이", "start_ms": 13300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "처", "start_ms": 13900, "dur_ms": 320, "phase_lr_deg": 0.0}
]
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:38:19*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=31656  time=08:34:39  uptime=17288s  mem=437MB  system_processes=409"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_083140.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 210-157-751-517-517-175-517-157-150-175-157-175-157-157-175-517-715-715-751-751-157-517-751 571-571-571-157-517-517-571-157-157-513-135-571-312-571-751-571-175-715-517-756-571-1_V.mp3)
  Segment 2: 1576ms ~ 11620ms (file: 132-720-370-237-203-273-705-750-103-136-573-153-135-213-321-130-103-321-317-310-264-261-213-321-573-530-270-721-701-570-375-236-675-321-236-235-203-327-237-237-710-107-153-367-3_W.mp3)
  Segment 3: 12620ms ~ 20936ms (file: 106-123-105-203-267-213-172-156-345-263-276-527-132-251-157-375-316-317-103-572-123-276-302-302-635-320-321-372-310-763-107-623-231-235-276-132-205-203-560-236-237-123-670-102-2_W.mp3)
  Segment 4: 21936ms ~ 22512ms (file: 576-574-576-576-571-571-573-537-571-571-571-574-571 574-574-571-517-517-571-537-517-571-571-571-751-157-510-517-513 571-571-571-571-517-573-576-570-517-517-130-735-510-517-175-5_W.mp3)
  Segment 5: 23512ms ~ 24088ms (file: 570-267-256-267-572-213-705-752-507-301-321-765-625-567 162-527-706-675-275-326-576-206-267-502-251-267-572-627-312-312-762-756-213-267-125-235-256-750-627-316-317-371-367-317-3_V.mp3)
  Segment 6: 25088ms ~ 25664ms (file: 571-720-527-265-726-507-362-726-521-174-537 137-725-507-235-327-725-275-362-762-576-261-572-526-271-172-627-512-273-563-213-671-235-205-632-261-527-627-263-257-276-706-750-263-2_V.mp3)
  Segment 7: 26664ms ~ 27240ms (file: 537-573-351-573-537-130-105-310-130-103-157-105-517-105-153-157-103-105-135-150-135-175-157-537-513-573-571-537-531-173-571-576-573 135-150-157-105-301-107-153-130-157-105-103-1_V.mp3)
  Segment 8: 28240ms ~ 28816ms (file: 713-735-756-571-375-753-571-750-750-517-570-570-756-756 574-574-574-574-574-574-547-547-574-573 571-574-574-573 572-571-574-574-574-574-571 547-574-574-574-574-574-574-576-574-5_W.mp3)
  Segment 9: 29816ms ~ 30392ms (file: 571-517-137-123-136-130-136-153-172-321-123-251-157-130-137-125-103-153-130-130-130-150-130-123-213-153-312-310-130-132-153-752-765-675-130-153-130-315-302-130-237-135-150-137-1_V.mp3)
  Segment 10: 31392ms ~ 31968ms (file: 672-765-765-675-162-165-162-136-106-132-367-763-672-716-167-165-567-675-671-163-613-613-613-612-613-613-613-612-613-617-617-613-613-631-613-631-613-613-617-123-103-130-316-163-1_W.mp3)
  Segment 11: 32968ms ~ 39412ms (file: 615-163-106-123-201-210-102-132-310-321-321-103-310-201-172-210-371-310-213-321-163-160-671-367-236-267-267-762-672-762-761-671-261-217-126-216-230-321-201-123-123-312-123-213-2_W.mp3)
  Segment 12: 40412ms ~ 40988ms (file: 750-157-507-150-173-715-105-507-150-102-107-105-105-517-510-571 531-510-501-153-751-571-157-573-157-573-570-513-105-517-573-105-153-571-501-150-513-153-503-501-517-502-507-150 5_V.mp3)
  Segment 13: 41988ms ~ 42564ms (file: 153-153-135-715-150-517-571 517-517_W.mp3)
  Segment 14: 43564ms ~ 44140ms (file: 765-172-506-251-275-521-257-725-275-625-275-267-236-507-325-576-527-325-572-563-352 763-267-756-136-172_V.mp3)
  Segment 15: 45140ms ~ 52376ms (file: 103-176-136-106-103-176-710-267-327-106-130-136-752-267-276-263-612-326-372-137-573-752-275-236-267-762-267-236-123-132-326-361-362-326-263-160-756-756-721-312-270-627-106-103-7_W.mp3)
  Segment 16: 53376ms ~ 61332ms (file: 317-261-137-231-163-163-136-130-163-317-136-130-621-173-130-321-312-130-312-310-301-310-376-312-317-371-321-631-136-371-213-362-230-327-207-327-321-765-672-756-135-731-732-276-6_W.mp3)
  Segment 17: 62332ms ~ 62908ms (file: 507-157-136-136-163-167-160-106-163-657-651-675-651-613-615-165-165-163-136-163-361-631-672-627-163-162-167-163-163-216-673-673-671-761-761-765-765-765-675-765-672-765-675-673-7_W.mp3)
  Segment 18: 63908ms ~ 64484ms (file: 627-751-621-136-576-657-236-753-362-316-561-615-750 756-326-632-267-312-507-325-567-503-206-231-621-123-672-621-601-602-675-267-762-326-672-602-576-756-527-562-507-576-627-267-1_V.mp3)
  Segment 19: 65484ms ~ 66060ms (file: 675-675-652-675-637-516-675-672-765 765-157-570 607-613 512-567-627_W.mp3)
  Segment 20: 67060ms ~ 73360ms (file: 507-201-302-602-672-207-231-317-325-132-627-625-570-321-265-547-306-362-267-265-206-260-213-627-312-317-126-271-126-612-627-261-357-752-231-620-265-756-102-310-261-504-206-625-3_W.mp3)
  Segment 21: 74360ms ~ 74936ms (file: 570-751-751-570-753-105-157-137-175-710-715-175-571-175-107-751-175-730-170-735-731-132 571-572-571-571-571-571-571-571-571-571-571-517-571-571-571-571-571 571-571-571-571-571-5_W.mp3)
  Segment 22: 75936ms ~ 84684ms (file: 310-673-576-351-753-375-753-361-137-572-572-652-276-732-715-301-136-236-172-103-721-132-312-130-632-731-523-627-756-103-106-310-567-241-574-130-301-320-231-312-321-130-750-137-1_W.mp3)
  Segment 23: 85684ms ~ 95728ms (file: 157-102-756-213-627-260-263-210-206-756-270-276-132-260-103-320-310-310-312-102-201-756-754-675-321-236-103-143-261-123-210-752-107-130-210-571-571-206-573-130-132-163-210-321-3_W.mp3)
  Segment 24: 96728ms ~ 97304ms (file: 157-267-731-163-172-572-275-276-576-326-627-627-362-206-327-267-562-756-635-570-312-516-563-136-275-506-657-265-263 526-752-625-237-572-263-726-652-126-750-632-623-267-527-617-2_V.mp3)
  Segment 25: 98304ms ~ 98880ms (file: 756-567-576-675-561-165-261-576-156-152-165-175-175-156-165-615-216-162-562-135-165-165-126-162-567-125-125-162-165-162-172-675-562-572-162-201-517-105-106-170-710-107-710-715-7_W.mp3)
  Segment 26: 99880ms ~ 100456ms (file: 123-361-175-763-715-137-701-571-576-165-571-103-507-351-612-123-176-716-710-715-751-715_W.mp3)
  Segment 27: 101456ms ~ 102032ms (file: 157-571-751-571-751-751 517-571-571-573-571-574-574-574-574 574-571-574-573-571-571-571-571-571-571-517-517-571-571 517-571-571-573 517-571-571-571-517-517-517-571-571-571-517-5_W.mp3)
  Segment 28: 103032ms ~ 103608ms (file: 572-372-275-670-367-526-351-173-750-132-207-726-235-726-236-267-653-267-576-271-520-136-167-375-756-253-572-752-326-572-237-237-327-236-267-216-235-572-325-312-235-312-726-657-6_V.mp3)
  Segment 29: 104608ms ~ 112384ms (file: 132-167-126-137-312-321-310-130-265-260-312-301-172-325-132-230-207-327-270-730-725-723-675-706-756-750-756-750-756-602-620-236-320-732-573-273-263-236-753-312-213-623-761-627-2_W.mp3)
  Segment 30: 113384ms ~ 121052ms (file: 623-327-750-756-167-105-132-267-672-250-263-236-167-306-763-715-237-572-132-267-762-576-627-230-751-307-263-326-301-617-167-756-574-517-176-167-367-103-627-312-671-574-157-610-6_W.mp3)
  Segment 31: 122052ms ~ 129432ms (file: 672-726-267-273-276-120-213-127-721-723-752-276-236-103-206-762-160-630-672-672-160-621-267-216-752-756-762-762-216-276-257-126-106-765-276-617-176-752-275-126-752-276-276-671-7_W.mp3)
  Segment 32: 130432ms ~ 131008ms (file: 327-371-136-137-137-173-137-173-175-175-175-172-137-132-371-317-231-127_V.mp3)
  Segment 33: 132008ms ~ 132584ms (file: 637-312-132-167-316-165-156-156-123-132-762-237-371-273-217-376-367-361-317-731-317-356-317-371-367-376-763-367-376-367-756-637-253-217-127-125-157-152-153-156-152-150-152-153-1_W.mp3)
  Segment 34: 133584ms ~ 140892ms (file: 132-103-723-276-130-123-627-623-127-210-215-162-271-213-627-576-213-213-237-106-312-263-103-513-510-312-572-213-236-627-352-132-316-216-216-120-263-260-321-360-213-217-271-273-2_W.mp3)
  Segment 35: 141892ms ~ 142468ms (file: 716-715-716-715-716-751-715-716-716 761-607-751-105-715-107-170-715-751-710-715-751-756-765-715-761-715-716-701 715-617-106-751-705-715-107-710-170-715-175-175-176-715-176-716-7_V.mp3)
  Segment 36: 143468ms ~ 153512ms (file: 327-302-310-502-321-321-376-627-627-670-256-532-137-702-706-256-203-236-237-312-106-275-206-213-132-172-753-753-230-320-735-652-260-564-253-265-357-327-136-163-127-236-236-752-5_W.mp3)
  Segment 37: 154512ms ~ 155088ms (file: 574-571-574-571-574-571-574-571-574-571-570-571-571-571-570-573 571-571-574-571-571-571-517-517-517-517-517-571-571-517-571-570-570-570-571-571-571-570-574-573 570-571-571-507-5_W.mp3)
  Segment 38: 156088ms ~ 161020ms (file: 362-723-301-127-206-102-263-267-265-230-173-756-705-607-176-705-320-321-203-230-236-235-570-106-127-271-352-372-236-237-267-267-275-756-756-206-756-132-105-360-531-231-271-756-2_W.mp3)
  Segment 39: 162020ms ~ 162596ms (file: 160-620-216-216-126-261-107-123-153-123-321-132-312-312-273-261-751-725-123-321-513-572-126-621-261-267-567-623-265-625-216-720-712-620-251-362-507-623-726-217-126-260-275-726-5_V.mp3)
  Segment 40: 163596ms ~ 164172ms (file: 316-610-136-130-571-576-567-156-571-651-516-157-657-657-106-157-135-136-317-136-156-257-367-137-123-372-370-317-310-351-317-310 136-130-103-106 351-315 751-127-213-235-312-512-1_W.mp3)
  Segment 41: 165172ms ~ 165748ms (file: 571-571 571-574-571-574-574-574-570-570-576-571-571-571 157-157-157-517-150-105-150-105-103-710-750-570-175-173-513-150-573-715-710-571-571-157-570-175 517-150-157-510-150-510-1_V.mp3)
  Segment 42: 166748ms ~ 167324ms (file: 153-153-512-517-752-735-752-732-571-135-127-176-261-763-736-637-732-713-721-372-731-576-571-751-756-751-760-173-173-713-137-372-137-173-173-762-731-712-752-753-732-756-753-637-3_W.mp3)
  Segment 43: 168324ms ~ 168900ms (file: 176-715-176-713-175-763-716-716-715 176-165-701-173-137-105-170-170-710-710-710-710-175-751-716-175-716-173-176-713-176-176-176-175-176-710 173-175-710-751-705-175-176-710-715-7_V.mp3)
  Segment 44: 169900ms ~ 170476ms (file: 254-570-625-602-123-275-572-276-673-601-653-236-675-502-317-571-321-625-570-602-372-576-736-567-675-167-631-576-231-576-570-675-672-756-576-765-752-256-715-150-132-235-507-231-3_V.mp3)
  Segment 45: 171476ms ~ 180836ms (file: 102-103-172-176-607-263-201-216-102-312-271-207-237-103-167-162-623-216-123-132-207-270-107-132-312-143-132-271-621-627-673-362-312-123-731-376-130-537-752-370-137-143-102-175-1_W.mp3)
  Segment 46: 181836ms ~ 182412ms (file: 305-507-354-157-107-157-362-310-316-627-132-132-621-362-162-236-160-620-132-317-375-372-752-732-735-371 475-547-132-132 673-671-617-327-523-327 106-136 206 543 265-261 265-546 6_W.mp3)
  Segment 47: 183412ms ~ 183988ms (file: 175-173-573-571-571-751 576-517-570-135-750-175-157-715-751-157-573-517-715-750-751-571-517-753-715-576 574-574-576-571-570-753-573-571-751-753-157-570-751-571-571-751-573-576-5_W.mp3)
  Segment 48: 184988ms ~ 185564ms (file: 170-376-265-135-315-132-321-635-310-316-573-357-301-301-756-672-302-732-731-360-371-735-753-637-371-372-672-672-672-672-627-675-621-573-301-316-302-321-316-301-310-321-236-216-3_W.mp3)
  Segment 49: 186564ms ~ 187140ms (file: 175-751-175-710-157-175-571-517 571-571-517-157-517-571-517-571-751-576-571-517-751-175-517-571-751-715-157 576-570-751-571-517-571-571-571-571-570-157-571-751-571-715-157-571-7_V.mp3)
  Segment 50: 188140ms ~ 188716ms (file: 750-150-750-510-750-751-157-571-751-571-573-175-571-573-571-573 576-576-157-153-570-175-153-715-175-517-510-507-751-157-751-753-517-571-153-715-517-571-573-571 574-574-574-574-5_W.mp3)
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
> **Time**: 2026-04-06 08:38:19
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 4,
"transcript": "그리고 이제 좀 그때는 이제 어렸지만 좀 크고 사춘기도 되고 그러면",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 110, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그때는", "start_ms": 1250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어렸지만", "start_ms": 2150, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 2800, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "크고", "start_ms": 3100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사춘기도", "start_ms": 3600, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 4300, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그러면", "start_ms": 4750, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "자기만의 공간이 이제 필요한데 얼마나 좋아할까 얘네들이",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "자기만의", "start_ms": 80, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "공간이", "start_ms": 750, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 1300, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "필요한데", "start_ms": 1700, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "얼마나", "start_ms": 2450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "좋아할까", "start_ms": 3000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "얘네들이", "start_ms": 3700, "dur_ms": 720, "phase_lr_deg": 0.0}
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
"transcript": "바닥 위에 상상으로 올렸던 우리 집 9년 전 시작부터 마지막까지를",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "mixed",
"words": [
{"word": "바닥", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "위에", "start_ms": 600, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "상상으로", "start_ms": 1050, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "올렸던", "start_ms": 1800, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 2400, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "집", "start_ms": 2800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "9년", "start_ms": 3100, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 3650, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "시작부터", "start_ms": 3900, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "마지막까지를", "start_ms": 4750, "dur_ms": 950, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "모두 지켜볼 수 있을 것이라는 사실과 그 과정 속 그의 계속되는 진화를",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "mixed",
"words": [
{"word": "모두", "start_ms": 50, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "지켜볼", "start_ms": 500, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 1350, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "것이라는", "start_ms": 1750, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "사실과", "start_ms": 2450, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 3100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "과정", "start_ms": 3250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "속", "start_ms": 3750, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "그의", "start_ms": 3980, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "계속되는", "start_ms": 4350, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "진화를", "start_ms": 5200, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "전부 목격해왔다는 사실은 우리에게 더없는 행",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "mixed",
"words": [
{"word": "전부", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "목격해왔다는", "start_ms": 520, "dur_ms": 1100, "phase_lr_deg": 0.0},
{"word": "사실은", "start_ms": 1650, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "우리에게", "start_ms": 2350, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "더없는", "start_ms": 3050, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "행", "start_ms": 3700, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "벽 하나하나 했던 것 같아요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "벽", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하나하나", "start_ms": 550, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "했던", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1800, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 2050, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "와 이거 뭐 눈물 없이는 들을 수 없는 얘기네 이거 와",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 90, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 750, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "눈물", "start_ms": 950, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "없이는", "start_ms": 1450, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "들을", "start_ms": 2100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2480, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "없는", "start_ms": 2680, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "얘기네", "start_ms": 3150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 3850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 4200, "dur_ms": 350, "phase_lr_deg": 0.0}
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
"transcript": "봄 여름 가을 겨울 초까지 해서 4계절 동안 골조 작업만 했습니다",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "봄", "start_ms": 180, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여름", "start_ms": 580, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "가을", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "겨울", "start_ms": 1550, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "초까지", "start_ms": 2080, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 2780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "4계절", "start_ms": 3280, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "골조", "start_ms": 4500, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "작업만", "start_ms": 5050, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "했습니다", "start_ms": 5780, "dur_ms": 820, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "와",
"speaker": "speaker_3",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "어그로는 저리가라고 할 정도로 지저분하고 조잡한 타블로이드성 헤드라인만 뽑아내는데 특히나 국내 축구 기사는 클릭도 하기 싫을 정도로 혐오스",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "어그로는", "start_ms": 100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "저리가라고", "start_ms": 780, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 1650, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 1900, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "지저분하고", "start_ms": 2550, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "조잡한", "start_ms": 3450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "타블로이드성", "start_ms": 4000, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "헤드라인만", "start_ms": 5000, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "뽑아내는데", "start_ms": 5850, "dur_ms": 880, "phase_lr_deg": 0.0},
{"word": "특히나", "start_ms": 6800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "국내", "start_ms": 7400, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "축구", "start_ms": 7800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "기사는", "start_ms": 8250, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "클릭도", "start_ms": 8800, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 9400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "싫을", "start_ms": 9800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 10300, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "혐오스", "start_ms": 10900, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "천장은 각관 틀 사이부분만 감사고 박공 지붕 모양을 그대로 드러내 개방감을 높였는데요 이런 천장",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 110,
"audio_type": "mixed",
"words": [
{"word": "천장은", "start_ms": 110, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "각관", "start_ms": 800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "틀", "start_ms": 1250, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "사이부분만", "start_ms": 1500, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "감사고", "start_ms": 2400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "박공", "start_ms": 2950, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "지붕", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모양을", "start_ms": 3900, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 4450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "드러내", "start_ms": 5050, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "개방감을", "start_ms": 5650, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "높였는데요", "start_ms": 6400, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 7300, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "천장", "start_ms": 7700, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "계기가 됐습니다 이에 대해 대만 당국은 자국의 정체성과 존엄을 지키겠다는 명분으로 대응에 나섰습니다 문제는 그 대응 방식이었습니다 대만은 한국을 포함한 일부 국가를 상대로 도저히 이해할 수",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "계기가", "start_ms": 60, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "됐습니다", "start_ms": 600, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이에", "start_ms": 1300, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 1650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 2050, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "당국은", "start_ms": 2500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자국의", "start_ms": 3100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "정체성과", "start_ms": 3600, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "존엄을", "start_ms": 4400, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "지키겠다는", "start_ms": 4950, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "명분으로", "start_ms": 5750, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "대응에", "start_ms": 6450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "나섰습니다", "start_ms": 7000, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "문제는", "start_ms": 7850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 8450, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대응", "start_ms": 8650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "방식이었습니다", "start_ms": 9100, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "대만은", "start_ms": 10100, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 10700, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "포함한", "start_ms": 11250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 11850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "국가를", "start_ms": 12300, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "상대로", "start_ms": 12850, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "도저히", "start_ms": 13450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이해할", "start_ms": 14050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 14600, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "Everything everything rice kkakdugi minari not just naughty minari so naughty minari",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "en",
"silence_before_ms": 150,
"audio_type": "mixed",
"words": [
{"word": "Everything", "start_ms": 150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "everything", "start_ms": 850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "rice", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "kkakdugi", "start_ms": 2050, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 2800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 3500, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 3850, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "naughty", "start_ms": 4250, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 4800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 5500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "naughty", "start_ms": 5800, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "minari", "start_ms": 6350, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "해서 말인데 손흥민 선수는 원래 호날두를 롤모델로 했었죠 그래서 정말 호날두처럼 빠른 발과 강력한 슛 그리고 완벽한 마무리 능력까",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 100, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "말인데", "start_ms": 500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "손흥민", "start_ms": 1100, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "선수는", "start_ms": 1700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "원래", "start_ms": 2300, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "호날두를", "start_ms": 2750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "롤모델로", "start_ms": 3450, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "했었죠", "start_ms": 4200, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 4850, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 5400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "호날두처럼", "start_ms": 5900, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "빠른", "start_ms": 6700, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "발과", "start_ms": 7150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "강력한", "start_ms": 7650, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "슛", "start_ms": 8250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 8650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "완벽한", "start_ms": 9150, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "마무리", "start_ms": 9800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "능력까", "start_ms": 10380, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "하 정말 목공을 이렇게 많이 다루시는 선생님께서 얘기를 하시니까 굉장히 설득력 있게 들리네요",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "하", "start_ms": 120, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "목공을", "start_ms": 950, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1550, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2050, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "다루시는", "start_ms": 2450, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "선생님께서", "start_ms": 3150, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "얘기를", "start_ms": 4000, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하시니까", "start_ms": 4550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 5250, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "설득력", "start_ms": 5850, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 6500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들리네요", "start_ms": 6900, "dur_ms": 680, "phase_lr_deg": 0.0}
]
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
"transcript": "What makes us humans humans, right? That's that's my biggest worry is it's not so much a worry I think we will figure it out but it creates discomfort in the process as we",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "What", "start_ms": 180, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "makes", "start_ms": 480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "us", "start_ms": 850, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "humans", "start_ms": 1050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "humans,", "start_ms": 1600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "right?", "start_ms": 2200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "That's", "start_ms": 2650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 3450, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "biggest", "start_ms": 3700, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "worry", "start_ms": 4200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 4650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 4850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 5150, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 5400, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "much", "start_ms": 5650, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 5950, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "worry", "start_ms": 6100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 6500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 6680, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 7000, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "will", "start_ms": 7200, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "figure", "start_ms": 7450, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 7850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "out", "start_ms": 8050, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "but", "start_ms": 8380, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 8620, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "creates", "start_ms": 8800, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "discomfort", "start_ms": 9300, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 10050, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 10250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "process", "start_ms": 10450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "as", "start_ms": 11000, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 11200, "dur_ms": 210, "phase_lr_deg": 0.0}
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
"transcript": "후에는 이제는 좀 빠진 것들이 많이 있어서 고것까지 대략 하면 그정도 나오지 않았을까 생각이 들더라고요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "후에는", "start_ms": 150, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "이제는", "start_ms": 680, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1150, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "빠진", "start_ms": 1380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것들이", "start_ms": 1880, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 2450, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 2880, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "고것까지", "start_ms": 3480, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "대략", "start_ms": 4200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 4680, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그정도", "start_ms": 5180, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "나오지", "start_ms": 5780, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "않았을까", "start_ms": 6350, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "생각이", "start_ms": 7100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "들더라고요", "start_ms": 7700, "dur_ms": 820, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "그래도 2층에다가 120평인데 2억이면은 네 돈 벌었다고 생각",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "그래도", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "2층에다가", "start_ms": 620, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "120평인데", "start_ms": 1400, "dur_ms": 950, "phase_lr_deg": 0.0},
{"word": "2억이면은", "start_ms": 2400, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 3280, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "돈", "start_ms": 3580, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "벌었다고", "start_ms": 3900, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "생각", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "까지 해서 다음날인 d+2일차 아침에 갭 상승했을 때 이렇게 수익 실현해서 추가로 1억 이상 수익을",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "까지", "start_ms": 80, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해서", "start_ms": 480, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "다음날인", "start_ms": 950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "d+2일차", "start_ms": 1650, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "아침에", "start_ms": 2500, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "갭", "start_ms": 3080, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "상승했을", "start_ms": 3380, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 4100, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 4350, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "수익", "start_ms": 4880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "실현해서", "start_ms": 5350, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "추가로", "start_ms": 6150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "1억", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 7150, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "수익을", "start_ms": 7650, "dur_ms": 580, "phase_lr_deg": 0.0}
]
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
"transcript": "I know it's getting in the sweet what is it that",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 110,
"audio_type": "mixed",
"words": [
{"word": "I", "start_ms": 110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "know", "start_ms": 300, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 650, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "getting", "start_ms": 900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 1400, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1650, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "sweet", "start_ms": 1880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "what", "start_ms": 2350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 2720, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 2980, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 3200, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "이와 함께 천궁2 추가 포대 배치도 빠르게 진행되고 있는데요 이미 세 번째 포대 장비가 중동 지역에 도착했고 조만간 실전 운용에 들어갈 예정입니다 이스라",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 90,
"audio_type": "speech",
"words": [
{"word": "이와", "start_ms": 90, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "함께", "start_ms": 520, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "천궁2", "start_ms": 1000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "추가", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "포대", "start_ms": 2100, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "배치도", "start_ms": 2650, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 3350, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "진행되고", "start_ms": 3950, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "있는데요", "start_ms": 4750, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 5450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "세", "start_ms": 5900, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "번째", "start_ms": 6150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "포대", "start_ms": 6650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "장비가", "start_ms": 7180, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "중동", "start_ms": 7850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지역에", "start_ms": 8350, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "도착했고", "start_ms": 8950, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "조만간", "start_ms": 9800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "실전", "start_ms": 10500, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "운용에", "start_ms": 11050, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "들어갈", "start_ms": 11750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "예정입니다", "start_ms": 12350, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "이스라", "start_ms": 13250, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "변화는 벌써 시작됐다 나에 대해 너무 잘 알고 있는 AI 비서인 우리 삶을",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "mixed",
"words": [
{"word": "변화는", "start_ms": 120, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "시작됐다", "start_ms": 1200, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "나에", "start_ms": 2000, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 2450, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3350, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "알고", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 4500, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "비서인", "start_ms": 4950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 5650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "삶을", "start_ms": 6100, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "이마 아미 이만오천엔 마다마다 이고 이마 이고 이만오천엔 이였는데 보토 이만큼 나와요 그돈으로 금으로",
"speaker": "speaker_9",
"confidence": 0.88,
"language": "ja",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이마", "start_ms": 150, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아미", "start_ms": 600, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이만오천엔", "start_ms": 1050, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "마다마다", "start_ms": 2000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이고", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이마", "start_ms": 3200, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "이고", "start_ms": 3650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이만오천엔", "start_ms": 4150, "dur_ms": 880, "phase_lr_deg": 0.0},
{"word": "이였는데", "start_ms": 5100, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "보토", "start_ms": 5900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이만큼", "start_ms": 6400, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "나와요", "start_ms": 7100, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "그돈으로", "start_ms": 7750, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "금으로", "start_ms": 8550, "dur_ms": 550, "phase_lr_deg": 0.0}
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
"transcript": "그의 야심을 알려면 다시 그 한국이 자체 개발한 kf-21 보라매 전투기로 돌아가야 합니다 이 전투기는 결코 만만한 배경을 가진 기체가 아닙니다 2001년에 구상이 처",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "그의", "start_ms": 80, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "야심을", "start_ms": 450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "알려면", "start_ms": 1050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "다시", "start_ms": 1750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국이", "start_ms": 2420, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자체", "start_ms": 3050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "개발한", "start_ms": 3550, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "kf-21", "start_ms": 4250, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "보라매", "start_ms": 5150, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "전투기로", "start_ms": 5750, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "돌아가야", "start_ms": 6550, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 7300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 7900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전투기는", "start_ms": 8100, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "결코", "start_ms": 8800, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "만만한", "start_ms": 9300, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "배경을", "start_ms": 9900, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "가진", "start_ms": 10550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "기체가", "start_ms": 11050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 11650, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "2001년에", "start_ms": 12400, "dur_ms": 850, "phase_lr_deg": 0.0},
{"word": "구상이", "start_ms": 13300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "처", "start_ms": 13900, "dur_ms": 320, "phase_lr_deg": 0.0}
]
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:38:19*
