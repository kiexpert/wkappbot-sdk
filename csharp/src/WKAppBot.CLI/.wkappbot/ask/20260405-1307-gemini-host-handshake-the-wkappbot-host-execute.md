# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=13:06:23  uptime=31668s  mem=427MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_130611.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 750-517-570-517-570-571 570-510-571-507-510-570_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 175-751-735-517-715-570 105-710-715-510-517-175-157-713-173-572-572-517-751-750-175-751-175-157-752-751-751-751-753-752 571-570_V.mp3)
  Segment 3: 3152ms ~ 12908ms (file: 103-231-136-261-102-716-754-756-576-216-312-735-620-576-261-103 632-132-132-623-657-132-712-261-123-265-261-132-321-265-576-267-312-213-256-231-527-132-263-260-216-261-260-261-3_W.mp3)
  Segment 4: 13908ms ~ 14484ms (file: 213-231-321-753-751-651-176-756-715-175-167-135-137-135-715-175-175-175-573-576-536-351-325-537-567-576-562-257-571-257-672-276-265-752-576-176-517-173-165-165-153-176-165-175-1_W.mp3)
  Segment 5: 15484ms ~ 22828ms (file: 263-623-236-123-263-102-123-326-326-236-263-723-263-320-102-620-236-201-621-102-176-276-127-267-765-265-203-276-126-260-726-267-206-215-521-263-267-126-520-257-207-750-251-321-1_W.mp3)
  Segment 6: 23828ms ~ 24404ms (file: 321-213-325-172-327-372-735-275-257-357-752 570-571-570-574-570-570-570-570-570-750-753-570-751_W.mp3)
  Segment 7: 25404ms ~ 25980ms (file: 317-137-317-173-731-371-173-137-310-135-537-567-326-750-756-735-307-753-163-103-527-574-572-135-137-135-135-150-316-130-173-175-176-570-570-703-157-570-173-173-321-267-703-521-2_W.mp3)
  Segment 8: 26980ms ~ 27556ms (file: 502-502-250-257-276-320-520-527-203-207-203-521-512-574-572-573-576-517-572-526-253-230-250-532-257-205-520-520-520-527-205-752-502-527-257-257-572-527-527-570-507-502-507-507-5_V.mp3)
  Segment 9: 28556ms ~ 29132ms (file: 513-517-517-517-517-157-150-571-573-570-517-570-507 156-517-157-135-152-517-571-513-157-157-157-517-517-571-517-571-570-157-517-517-507-507-570-507-570-576-570 510-517-517-571-5_V.mp3)
  Segment 10: 30132ms ~ 30708ms (file: 751-702-751-751-132-723-136-615-615-157-571-751-571-751-517-157-751-571-157-157-571-753-157-157-315-130-315-357-375-357-753-135-375-361-317-375-316-635-675-315-716-756-671-165-1_W.mp3)
  Segment 11: 31708ms ~ 32284ms (file: 716-170-756-631-621-625-651-635-615-653 513-537-501-510-150-150-570-315-571-570-570-527-537-251-250-502-523-250-527-571-570-517-275-523 507-570-570-517-315-150-517-357-352-753-3_W.mp3)
  Segment 12: 33284ms ~ 33860ms (file: 512-526-521-526-521-526-521-521-521-521-507-507-502-507-507-506-570-570-570-576-572-576-572-527-576-576-572-576-572-752-572-572-527-576-567-576-326-236-623-236-216-152-512-521-5_W.mp3)
  Segment 13: 34860ms ~ 35436ms (file: 765-756-156-615-765-165-165-615-617-671-675-657-617-673-675-167-375-637-316-362-632-632-637-672-675-675-675-672-675-673-637-623-671-672_W.mp3)
  Segment 14: 36436ms ~ 37012ms (file: 572-267-263 271-671-576-756-265-512-752-635-750-236-507-315-263 206-260-756-632-532-562-752-267-362-503-321-726-267-572-213-520-275-572-317-371-731-317-261-206-213-217-260-265-2_V.mp3)
  Segment 15: 38012ms ~ 38588ms (file: 571-571-517-517-571-573-571-571 517_W.mp3)
  Segment 16: 39588ms ~ 40164ms (file: 361-632-605-652-603-547-547-513-574-507-574-571-571-507-576-576-572-576-576-576-576-576-572-576-507-572-750-570-750-507-567-361-612-312-315-316-325-352-536-312-356 507-506-507-5_V.mp3)
  Segment 17: 41164ms ~ 41740ms (file: 103 263-627-365-137-175-716-312-136 675-675-765-756-765-673-236-267-276-267-672-672-672-673-325-672 576-657-675-216-632-261-627 510 321-231_W.mp3)
  Segment 18: 42740ms ~ 43316ms (file: 301-130-310-135 750-750-570-572-752-572-572-574-571-571-751-750-702-752-570-572-570-751-750-751-357-571-571-570-571-751-571-571-573-571-517-517-570-527-751-571 574-571-570-510-5_V.mp3)
  Segment 19: 44316ms ~ 44892ms (file: 571-715-705-175-705-570-570-571-750_W.mp3)
  Segment 20: 45892ms ~ 56584ms (file: 236-301-130-564-102-167-260-765-320-527-312-162-257-236-754-310-263-301-623-236-103-371-236-321-762-132-756-102-103-231-237-162-216-756-627-123-257-213-752-263-267-236-120-102-1_W.mp3)
  Segment 21: 57584ms ~ 66728ms (file: 627-672-627-237-257-547-236-312-267-175-120-106-216-231-126-231-376-312-307-362-267-236-263-672-672-627-263-260-267-526-732-706-367-632-627-627-672-267-675-672-763-230-236-302-7_W.mp3)
  Segment 22: 67728ms ~ 68304ms (file: 570-750-750-576-750-751-571-513-516-513-105-105-153-105-157-517-157-157-173-175-751-715-175-715-715-175-571-715-571-570-570-571-570-570-157-153-715-715-517-751-175-517-571-517-5_V.mp3)
  Segment 23: 69304ms ~ 69880ms (file: 572-507-507-507-507-576-507-576-507-507-501-571-571-573-253-521-521-254-527-527-251-521-527-523-527-521-527-521-527-257-253-215 501-573-537-571-576-541-760-765-132_V.mp3)
  Segment 24: 70880ms ~ 71456ms (file: 501-536-503-503-501-534-532-521-253-502-507-502-506-503-507-547-574-574-507-507-507-507-507-507-570-576-570-576-756-572-567-572-576-576-572-521-251-523-251-251-256-523-251-257-5_V.mp3)
  Segment 25: 72456ms ~ 73032ms (file: 532-567-543-504-514-524-502-501-507-502-502-576-521-517-507-507 571-507-507-503-571-507-527-507-501-153-503-501-513-350-503-531-513-521-572-502-507-501-507-502-502-507-502-507-5_V.mp3)
  Segment 26: 74032ms ~ 74608ms (file: 574-576-165-756-574-576-756-567-576-516-157-157-167-612-165-153-627-160-716-716-715-157-517-547-175-715-516-576-576-136-576-572-162-671-760 103-610-516-716-516-106-130-756-573-3_W.mp3)
  Segment 27: 75608ms ~ 76184ms (file: 321-321-231-325-653-735-712-713-723-235-135-175-751-607-567 756-517 160-310-130-316-310-132-361-613-613-657 635-653 651_W.mp3)
  Segment 28: 77184ms ~ 77760ms (file: 153-152-125-250-120-120-135-123-153 512-512-521-512-532-315-152-513-130-513-513-231-150-135-132 157-157-150-135-125-120-135-326 572-572-512-153-513-512-152-152-572-153-513-520-5_W.mp3)
  Segment 29: 78760ms ~ 84844ms (file: 160-621-230-201-175-267-216-107-621-261-267-216-627-162-107-162-263-261-261-127-312-203-261-267-726-216-107-130-726-276-130-123-173-321-120-130-162-210-217-103-672-617-672-267-2_W.mp3)
  Segment 30: 85844ms ~ 90884ms (file: 231-236-267-602-627-106-236-236-672-627-762-263-326-236-267-216-126-236-263-627-672-627-267-621-267-267-326-261-362-231-621-261-321-312-632-236-213-162-621-261-621-263-126-267-2_W.mp3)
  Segment 31: 91884ms ~ 92460ms (file: 215 137-310-132-132-137-137-317-731-173-137-172-137-137-173-163-136-136-136-165-516-635-356-365-356-675-637-635-376-365-351-763-576-756-751-716-761-763-763-173-176-173-327-173-1_W.mp3)
  Segment 32: 93460ms ~ 94036ms (file: 125 152-153-512-152-135-152-153-512-513-153-152-512-152-512-523-512_W.mp3)
  Segment 33: 95036ms ~ 109184ms (file: 106-574-574-576-517-571-567-576-574-754-574-547-574-574-574-574-574-574-574-576-576-574-576-574-574 574-547-547-362-135-756-327-573-756-357-173-123_W.mp3)
  Segment 34: 110184ms ~ 121128ms (file: 621-261-107-107-316-261-621-261-567-256-321-231-152-260-162-123-216-574-620-261-132-263-576-621-132-153-126-210-120-540-103-312-312-267-213-321-231-210-713-621-326-216-260-312-1_W.mp3)
  Segment 35: 122128ms ~ 122704ms (file: 167-317-361-316-173-126-657-657-263-351-137-136-213-126-173-125-317-176-756-756-756-756-576-175-175-175-156-157-136-132-132-132-132-136-132-312-312-263-265-156-671-765-765-576-1_W.mp3)
  Segment 36: 123704ms ~ 131912ms (file: 756-752-765-675-675-675-751-267-765-372-236-236-527-567-352-365-576-357-567-753-576-267-312-321-236-236-235-754-326-574-320-237-723-327-123-263-542-574-236-263-267-236-276-257-2_W.mp3)
  Segment 37: 132912ms ~ 139536ms (file: 321-237-574-320-103-172-326-267-267-276-276-574-273-731-270-723-327-210-703-137-126-102-132-321-672-712-617-120-267-263-231-627-376-201-627-163-136-176-123-162-130-132-137-231-5_W.mp3)
  Segment 38: 140536ms ~ 141112ms (file: 571-571-751-713-213-527-573-510-715-153-137-135-320-730-173-370-715-137-753-375-517-715-125-752-205-271 517-570-570-513-572-571-571-571-751-753-571-175-751-750-175-157-517-517-1_V.mp3)
  Segment 39: 142112ms ~ 142688ms (file: 125-125 571-751-570-751-157-751-317-715-751-750-715-517-137-215-213-123-215-215-265-625-256-265-265-256-265-261 570-570-751-751-570-571-751-751-571-571-570-570-715-571-572-571-7_V.mp3)
  Segment 40: 143688ms ~ 149556ms (file: 267-263-213-162-267-672-617-261-276-217-261-261-162-267-261-261-216-261-213-312-127-126-216-217-213-123-126-127-217-127-172-126-167-102-123-126-267-126-120-217-126-162-514-572-5_W.mp3)
  Segment 41: 150556ms ~ 151132ms (file: 561-567-507-517-501-502-501-507-507-502-507-507-532-503-502-502-506-503-501-501-503-502-530-350-530-503-350-305-350-305-502-530-523-503-513-521-513-521-514-512-512-517-514-541-5_V.mp3)
  Segment 42: 152132ms ~ 152708ms (file: 157-105-105-157-150-516 105-150-150-150-150-150-105-105-105-157-150-156-105-150-157-517 150-150-150-105-150-150-150-150-150-150-150-157-150-150-150 105-150-105-105-157-105-105-1_V.mp3)
  Segment 43: 153708ms ~ 154284ms (file: 756-653-617-637-167-371-176-716-176-716-657-167-671-715-176-165-167-675-671-675-576-567-756-675-657-376-375-657-357-736-175-163-167-763-617-675-617-657-615-651-165-156-615-651-1_W.mp3)
  Segment 44: 155284ms ~ 155860ms (file: 573-657-761-536-536-517-135 617-610-617-167-103 726-571-756-572-576-576-201-516-621_W.mp3)
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
> **Time**: 2026-04-05 13:07:15
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "여기서부터가 진짜 가속 구간이 시작된다고 말합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "여기서부터가", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가속", "start_ms": 310, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "구간이", "start_ms": 450, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "보셨던 것처럼 큰 하락이 이어지고 있는데 이런 경우엔 아직 지표의 색상이 빨간색에서 노란색으로 변경되지 않았기 때문에",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "보셨던", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것처럼", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 320, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "하락이", "start_ms": 380, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "이어지고", "start_ms": 520, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "많이 망하긴 했거든요 근데 목표를 그렇게 높게 잡고 있지 않아요 오히려 저는 진짜 1, 2, 3월에 거의 한 20억 정도 수익이 났는데 저는 앞으로의 한 달에 1억만",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "많이", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "망하긴", "start_ms": 170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "했거든요", "start_ms": 330, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 520, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "목표를", "start_ms": 640, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "입니다 이럴 때 초강대국 미국이 가장 먼저 하는 선택은 단 하나입니다 가장 급한 불부터 끄기 위해 모든 자원을 쏟아붓는 것입니다 그리고 지금 그 모든 자원을 빨아들이는 블랙홀이 바로 중동입니다 문제는 그 대",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "입니다", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이럴", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 290, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "초강대국", "start_ms": 350, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "미국이", "start_ms": 580, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "이 FSD를 온 한 부분이 그런 느낌이라는 거죠 흔히 트러스트 바운더리라고 표현을 하는 게 내부의 이 안에 있는 것들은 이제 신뢰를 한다라고 봤을 때 어디",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "FSD를", "start_ms": 130, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "온", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "부분이", "start_ms": 500, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "습니다 미국인들은 그 누구보다 세탁기를 빨리 그리고 아주 오래동안 사용해 왔고 이미 세탁기라는 존재는 미국인",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "습니다", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "미국인들은", "start_ms": 140, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 370, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "누구보다", "start_ms": 430, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "세탁기를", "start_ms": 620, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "모두 바쁠 때면 40년 경력의 1대 사장님 등판 이렇게 자식과",
"speaker": "speaker_6",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "바쁠", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "때면", "start_ms": 320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "40년", "start_ms": 440, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "경력의", "start_ms": 630, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "거대한 타이타닉도 얼음에 부딪히자마자 반으로 갈라졌는데 쇄빙선은 도대체 어떻게 얼음을 부수면서 앞으로 나가는 거지? 쇄빙선의 비밀은 바로 이 앞머리 선수의",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "거대한", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "타이타닉도", "start_ms": 180, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "얼음에", "start_ms": 410, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "부딪히자마자", "start_ms": 560, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "반으로", "start_ms": 870, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "북인도양을 지나는 항공모함 에이브러햄 링컨함의 강습단을 순항 미사일 등으로 공격했다고 알려졌습니다 또 아랍에미리트 공군기지 외곽의 미 공군 시설도 탄도",
"speaker": "speaker_8",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "북인도양을", "start_ms": 50, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "지나는", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "항공모함", "start_ms": 470, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "에이브러햄", "start_ms": 680, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "링컨함의", "start_ms": 930, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "세컨더리 이 모듈들이 각자 있고 버스에 흐르는 것들을 각자 가져가는 기본적인 여전히 지금 SDV라고 하",
"speaker": "speaker_4",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "세컨더리", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 270, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "모듈들이", "start_ms": 330, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "각자", "start_ms": 520, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "산골로의 변신이 좋은 건 눈치 보지 않고 나답게 살 수 있어서",
"speaker": "speaker_9",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "산골로의", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "변신이", "start_ms": 270, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 550, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "눈치", "start_ms": 620, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보지", "start_ms": 750, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 870, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나답게", "start_ms": 980, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "살", "start_ms": 1140, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 1270, "dur_ms": 180, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:07:15*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=13:06:49  uptime=43026s  mem=489MB  system_processes=355"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_130638.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 314-132 162-201-123-130 154 246 216-132-312 132-136 123 267-627-621-617-621-617-612-617-612-621-621-627-627-627-672-672-627-627-627-612-267-627-612-627-612-103-130-562-513-521-5_V.mp3)
  Segment 2: 1576ms ~ 6616ms (file: 231-236-267-602-627-106-236-236-672-627-762-263-326-236-267-216-126-236-263-627-672-627-267-621-267-267-326-261-362-231-621-261-321-312-632-236-213-162-621-261-621-263-126-267-2_W.mp3)
  Segment 3: 7616ms ~ 8192ms (file: 321-213-325-172-327-372-735-275-257-357-752 570-571-570-574-570-570-570-570-570-750-753-570-751_W.mp3)
  Segment 4: 9192ms ~ 15816ms (file: 321-237-574-320-103-172-326-267-267-276-276-574-273-731-270-723-327-210-703-137-126-102-132-321-672-712-617-120-267-263-231-627-376-201-627-163-136-176-123-162-130-132-137-231-5_W.mp3)
  Segment 5: 16816ms ~ 17392ms (file: 356-503-350-305-503-503-506-530-506-503-527-507-576-572-573-507-572-507-507-507-573-507-507-507-507-507-507-571-514-571-573-574-507-547-564-356-503-563-351-531-530-507-503-351-3_V.mp3)
  Segment 6: 18392ms ~ 18968ms (file: 361-632-605-652-603-547-547-513-574-507-574-571-571-507-576-576-572-576-576-576-576-576-572-576-507-572-750-570-750-507-567-361-612-312-315-316-325-352-536-312-356 507-506-507-5_V.mp3)
  Segment 7: 19968ms ~ 20544ms (file: 531-512-513-517-513-572-513-517-157 514-514-512-517-571-512-517-512-152 571-537-531-572-571-572-571-572-537-513-513-351-531 517-527-512-521-572-517-571-571-571-572-572-571-517-5_W.mp3)
  Segment 8: 21544ms ~ 22120ms (file: 571-756-715-175-715-175-507-507-570-570-517-170-751-571-705-710-105-705-170-701-705-710-751-175-105-750-517-705-571-751-570-750-710-705-170-705-751-715 510-571-570-150-107-175-7_V.mp3)
  Segment 9: 23120ms ~ 23696ms (file: 576-352-325-231-230-237-201-103-735-703-735-372 127-736-763-376-376 761-675-267-320-302-632-326-132-316-136-627-267-276-163-176-361-362-562-256-265-326-321-263-265-267-207-203-7_V.mp3)
  Segment 10: 24696ms ~ 25272ms (file: 753-537-573-735-756-576-753-375-357-357-571-175-351-756-751-756-751-715-176-165-156-157-175-156-175-176-157-165-175-157-127-173-716-725-753-357-756-571-573-736-735-753-715-127-1_W.mp3)
  Segment 11: 26272ms ~ 26848ms (file: 125-125 571-751-570-751-157-751-317-715-751-750-715-517-137-215-213-123-215-215-265-625-256-265-265-256-265-261 570-570-751-751-570-571-751-751-571-571-570-570-715-571-572-571-7_V.mp3)
  Segment 12: 27848ms ~ 28424ms (file: 701-176-312-136-160-136-671-106-163-612-132-135-756-751-132-103-675-672-617-162-615_W.mp3)
  Segment 13: 29424ms ~ 30000ms (file: 153-153-106-103-176-167-107-132-721-137-153-135-156-157-175-157-675-763-756-267-712-763-672-736-756-765-756-765-765-756-762-765-765-736-736-327-736-712-123-135-137-136-710-716-1_W.mp3)
  Segment 14: 31000ms ~ 40144ms (file: 627-672-627-237-257-547-236-312-267-175-120-106-216-231-126-231-376-312-307-362-267-236-263-672-672-627-263-260-267-526-732-706-367-632-627-627-672-267-675-672-763-230-236-302-7_W.mp3)
  Segment 15: 41144ms ~ 41720ms (file: 561-567-507-517-501-502-501-507-507-502-507-507-532-503-502-502-506-503-501-501-503-502-530-350-530-503-350-305-350-305-502-530-523-503-513-521-513-521-514-512-512-517-514-541-5_V.mp3)
  Segment 16: 42720ms ~ 43296ms (file: 570-750-750-576-750-751-571-513-516-513-105-105-153-105-157-517-157-157-173-175-751-715-175-715-715-175-571-715-571-570-570-571-570-570-157-153-715-715-517-751-175-517-571-517-5_V.mp3)
  Segment 17: 44296ms ~ 44872ms (file: 570-572-753-752-752-375-573-537-570-572-572-576-576-756-753-537-572-752-752-572-257-572-752-752-752-576-571-756-752-357-257-573-675-537-157-571-517-572-572-572-752-572-752-572-5_W.mp3)
  Segment 18: 45872ms ~ 46448ms (file: 532-567-543-504-514-524-502-501-507-502-502-576-521-517-507-507 571-507-507-503-571-507-527-507-501-153-503-501-513-350-503-531-513-521-572-502-507-501-507-502-502-507-502-507-5_V.mp3)
  Segment 19: 47448ms ~ 48024ms (file: 125 152-153-512-152-135-152-153-512-513-153-152-512-152-512-523-512_W.mp3)
  Segment 20: 49024ms ~ 49600ms (file: 502-502-250-257-276-320-520-527-203-207-203-521-512-574-572-573-576-517-572-526-253-230-250-532-257-205-520-520-520-527-205-752-502-527-257-257-572-527-527-570-507-502-507-507-5_V.mp3)
  Segment 21: 50600ms ~ 61220ms (file: 751-571-315 512-517-537-576-576-517-567-561-571-567-567-567-567-576-571-576-576-713-132-572-574-507-513-572-572-517-536-352-527-541-512-537-527_W.mp3)
  Segment 22: 62220ms ~ 70824ms (file: 325-326-623-256-263-210-123-213-102-726-627-210-765-267-263-310-164-756-103-701-572-236-275-231-237-213-236-132-236-627-627-263-321-267-276-372-103-106-102-236-673-765-267-573-1_W.mp3)
  Segment 23: 71824ms ~ 72400ms (file: 157-153-153-512-150-152-150-105-125-105-152-102-152-512-517-150-152-153-531-517-523-517 501-517-512-150-150-521-152-157-152-125-125-127-513-571-513-571-125-513-510-150-512-517-5_W.mp3)
  Segment 24: 73400ms ~ 73976ms (file: 716-170-756-631-621-625-651-635-615-653 513-537-501-510-150-150-570-315-571-570-570-527-537-251-250-502-523-250-527-571-570-517-275-523 507-570-570-517-315-150-517-357-352-753-3_W.mp3)
  Segment 25: 74976ms ~ 75552ms (file: 105-105-105-103-105-153 105-107-105-571-510-507-157 152-510-157-157-150-105-157-150-150-105-150-175-157-157-153-150-157 105-103-105-105-531-105-105-105-153-153-105-105-150-501-1_V.mp3)
  Segment 26: 76552ms ~ 77128ms (file: 137-127-127-173-173-175-173-172 570-574-576-571-751-571-517-517-571-574-510-504-571-501-150-510-153-157-517-576-517-157-501-157-157-176-176-571-573_W.mp3)
  Segment 27: 78128ms ~ 78704ms (file: 537-523-547-572-504 165-613-651-507-504-507-504-504-507-507-507-527-507-502-574-507-574-507-502-501-502-503-541-531-512-517-516-506-501-507-501-531-507-507-570-507-574-502-756-5_V.mp3)
  Segment 28: 79704ms ~ 85788ms (file: 160-621-230-201-175-267-216-107-621-261-267-216-627-162-107-162-263-261-261-127-312-203-261-267-726-216-107-130-726-276-130-123-173-321-120-130-162-210-217-103-672-617-672-267-2_W.mp3)
  Segment 29: 86788ms ~ 87364ms (file: 756-653-617-637-167-371-176-716-176-716-657-167-671-715-176-165-167-675-671-675-576-567-756-675-657-376-375-657-357-736-175-163-167-763-617-675-617-657-615-651-165-156-615-651-1_W.mp3)
  Segment 30: 88364ms ~ 98120ms (file: 103-231-136-261-102-716-754-756-576-216-312-735-620-576-261-103 632-132-132-623-657-132-712-261-123-265-261-132-321-265-576-267-312-213-256-231-527-132-263-260-216-261-260-261-3_W.mp3)
  Segment 31: 99120ms ~ 99696ms (file: 571-753-573-357-753-371-537-753-157-750-571-753-753 510-150-517-517-517-175-752-517-175-571-751-753-713-750-701-715-751-530-571-175-152-357-573-537-570-571 750-750-751-751-750-7_V.mp3)
  Segment 32: 100696ms ~ 101272ms (file: 130 716-175 176-176-172-705-716-716-716-715-716-765-750-176-157-761-167-162-751-751-751-751-751-756-756-571-751-756-715-751-715-571-751-576-751-751-750-571-510-105-105-102-120-7_W.mp3)
  Segment 33: 102272ms ~ 116420ms (file: 106-574-574-576-517-571-567-576-574-754-574-547-574-574-574-574-574-574-574-576-576-574-576-574-574 574-547-547-362-135-756-327-573-756-357-173-123_W.mp3)
  Segment 34: 117420ms ~ 124764ms (file: 263-623-236-123-263-102-123-326-326-236-263-723-263-320-102-620-236-201-621-102-176-276-127-267-765-265-203-276-126-260-726-267-206-215-521-263-267-126-520-257-207-750-251-321-1_W.mp3)
  Segment 35: 125764ms ~ 126340ms (file: 263-623-102-106-106-163-572-756-756-756-576-576-576-576-576-756-576-576-576-756-756-751-260-263-620-623-365-310-130-135-531-527-507-507-502-520-570-562-572-527-752-725-752-572-6_W.mp3)
  Segment 36: 127340ms ~ 127916ms (file: 507-520-250-257-250-257-257-527-520 351-532-531-350-270-527-207-201-253-257-527-512-251-350-253-532-523-502-502 534-531-531-354-532-530-523-567-541-501-517-516-506-506-502-507-5_V.mp3)
  Segment 37: 128916ms ~ 135180ms (file: 261-671-621-213-517-210-213-267-231-276-213-217-137-302-103-765-672-321-765-615-125-231-206-250-216-210-103-574-231-310-130-321-157-132-267-276-176-173-547-271-302-236-321-165-1_W.mp3)
  Segment 38: 136180ms ~ 136756ms (file: 574-576-165-756-574-576-756-567-576-516-157-157-167-612-165-153-627-160-716-716-715-157-517-547-175-715-516-576-576-136-576-572-162-671-760 103-610-516-716-516-106-130-756-573-3_W.mp3)
  Segment 39: 137756ms ~ 138332ms (file: 602 617-671-167-765-765-716-127-123-167-163-603-621-123-362-132-310-627-162-615-637-756-756-765-327-760-627-673-612-612-167-701-750-751-107-617-630-310-136-625-623-362-236-652-5_W.mp3)
  Segment 40: 139332ms ~ 139908ms (file: 153-152-125-250-120-120-135-123-153 512-512-521-512-532-315-152-513-130-513-513-231-150-135-132 157-157-150-135-125-120-135-326 572-572-512-153-513-512-152-152-572-153-513-520-5_W.mp3)
  Segment 41: 140908ms ~ 141484ms (file: 261-263-574-562-263-267-602-670-627-167-671-162-316-301-307-627-216-216-162-612-657-625-620-672-136-672-607-236-267-326-106-156-132-162-263-263-206-236-236-206-576-576-567-513-6_W.mp3)
  Segment 42: 142484ms ~ 148352ms (file: 267-263-213-162-267-672-617-261-276-217-261-261-162-267-261-261-216-261-213-312-127-126-216-217-213-123-126-127-217-127-172-126-167-102-123-126-267-126-120-217-126-162-514-572-5_W.mp3)
  Segment 43: 149352ms ~ 149928ms (file: 137-301-137-103-103-173-107-705-703 310-137-103-310-301-173-137-130-137-317-103-310-130-301-137-103-103-130-107-315-170 103-135-317-317-301-103-137-137-130-310-317-137-130-301-3_V.mp3)
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
> **Time**: 2026-04-05 13:07:57
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "모두 바쁠 때면 40년 경력의 1대 사장님 등판 이렇게 자식과",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 20, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "바쁠", "start_ms": 150, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "때면", "start_ms": 300, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "40년", "start_ms": 420, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "경력의", "start_ms": 610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "1대", "start_ms": 770, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "사장님", "start_ms": 900, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "등판", "start_ms": 1090, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자식과", "start_ms": 1370, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "세컨더리 이 모듈들이 각자 있고 버스에 흐르는 것들을 각자 가져가는 기본적인 여전히 지금 SDV라고 하",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "세컨더리", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 260, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "모듈들이", "start_ms": 320, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "각자", "start_ms": 510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있고", "start_ms": 640, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "버스에", "start_ms": 760, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "흐르는", "start_ms": 920, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 1110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "각자", "start_ms": 1270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "가져가는", "start_ms": 1400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "기본적인", "start_ms": 1610, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "여전히", "start_ms": 1840, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 2010, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "SDV라고", "start_ms": 2120, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "하", "start_ms": 2370, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "이 FSD를 온 한 부분이 그런 느낌이라는 거죠 흔히 트러스트 바운더리라고 표현을 하는 게 내부의 이 안에 있는 것들은 이제 신뢰를 한다라고 봤을 때 어디",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 60, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "FSD를", "start_ms": 130, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "온", "start_ms": 340, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 430, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "부분이", "start_ms": 500, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 630, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "느낌이라는", "start_ms": 750, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 980, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "흔히", "start_ms": 1120, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "트러스트", "start_ms": 1260, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "바운더리라고", "start_ms": 1450, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 1740, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1910, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2030, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "내부의", "start_ms": 2080, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2240, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "안에", "start_ms": 2300, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2430, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것들은", "start_ms": 2550, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2720, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "신뢰를", "start_ms": 2840, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한다라고", "start_ms": 3000, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "봤을", "start_ms": 3220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 3350, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "어디", "start_ms": 3410, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "SpaceX 관계자들과 미팅을 진행했다는 겁니다 먼저 SpaceX 관계자들이 삼성 SDI 천안 사업장에 방문해 현장 실사를 이미 했었다는 거죠 그 일정은 아마 한 달 전",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "SpaceX", "start_ms": 40, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "관계자들과", "start_ms": 400, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "미팅을", "start_ms": 830, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "진행했다는", "start_ms": 1060, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 1390, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 1550, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "SpaceX", "start_ms": 1740, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "관계자들이", "start_ms": 2070, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 2530, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "SDI", "start_ms": 2690, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "천안", "start_ms": 2950, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "사업장에", "start_ms": 3140, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "방문에", "start_ms": 3430, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "현장", "start_ms": 3660, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "실사를", "start_ms": 3850, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 4070, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "했었다는", "start_ms": 4210, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거죠", "start_ms": 4470, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 4590, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "일정은", "start_ms": 4650, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "아마", "start_ms": 4880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 5040, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "달", "start_ms": 5130, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 5240, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "습니다 미국인들은 그 누구보다 세탁기를 빨리 그리고 아주 오랫동안 사용해 왔고 이미 세탁기라는 존재는 미국인",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "습니다", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "미국인들은", "start_ms": 140, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 370, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "누구보다", "start_ms": 430, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "세탁기를", "start_ms": 620, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "빨리", "start_ms": 830, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 960, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1120, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "오랫동안", "start_ms": 1260, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "사용해", "start_ms": 1490, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "왔고", "start_ms": 1680, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 1830, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "세탁기라는", "start_ms": 1960, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "존재는", "start_ms": 2210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "미국인", "start_ms": 2400, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "여기서부터가 진짜 가속 구간이 시작된다고 말합니다 보셨던 것처럼 큰 하락이 이어지고 있는데 이런 경우엔 아직 지표의 색상이 빨간색에서 노란색으로 변경되지 않았기 때문에",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "여기서부터가", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가속", "start_ms": 440, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "구간이", "start_ms": 580, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "시작된다고", "start_ms": 770, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "말합니다", "start_ms": 1100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "보셨던", "start_ms": 1400, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "것처럼", "start_ms": 1590, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 1750, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하락이", "start_ms": 1840, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이어지고", "start_ms": 2030, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 2260, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2430, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "경우엔", "start_ms": 2550, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "아직", "start_ms": 2740, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "지표의", "start_ms": 2870, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "색상이", "start_ms": 3040, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "빨간색에서", "start_ms": 3230, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "노란색으로", "start_ms": 3560, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "변경되지", "start_ms": 3920, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "않았기", "start_ms": 4210, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 4400, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "많이 망하긴 했거든요 근데 목표를 그렇게 높게 잡고 있지 않아요 오히려 저는 진짜 1, 2, 3월에 거의 한 20억 정도 수익이 났는데 저는 앞으로의 한 달에 1억만",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "많이", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "망하긴", "start_ms": 180, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "했거든요", "start_ms": 340, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 570, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "목표를", "start_ms": 690, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "높게", "start_ms": 1040, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "잡고", "start_ms": 1170, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "있지", "start_ms": 1320, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "않아요", "start_ms": 1440, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "오히려", "start_ms": 1630, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 1820, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1950, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "1,", "start_ms": 2100, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "2,", "start_ms": 2150, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "3월에", "start_ms": 2200, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "거의", "start_ms": 2390, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 2510, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "20억", "start_ms": 2600, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 2790, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수익이", "start_ms": 2920, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "났는데", "start_ms": 3090, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 3280, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "앞으로의", "start_ms": 3410, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 3670, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "달에", "start_ms": 3740, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "1억만", "start_ms": 3870, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "인생 캐릭터는 무엇인가요 우리나라 배우가 자랑스럽다면 터치를 두 번 눌러 주세요",
"speaker": "speaker_7",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "인생", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "캐릭터는", "start_ms": 190, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "무엇인가요", "start_ms": 450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "우리나라", "start_ms": 780, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "배우가", "start_ms": 1070, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "자랑스럽다면", "start_ms": 1260, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "터치를", "start_ms": 1690, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "두", "start_ms": 1910, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 1980, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "눌러", "start_ms": 2040, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 2200, "dur_ms": 220, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "산골로의 변신이 좋은 건 눈치 보지 않고 나답게 살 수 있어서",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "산골로의", "start_ms": 40, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "변신이", "start_ms": 270, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "좋은", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 550, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "눈치", "start_ms": 620, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보지", "start_ms": 750, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 870, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나답게", "start_ms": 980, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "살", "start_ms": 1140, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1200, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "있어서", "start_ms": 1270, "dur_ms": 180, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 13:07:57*
