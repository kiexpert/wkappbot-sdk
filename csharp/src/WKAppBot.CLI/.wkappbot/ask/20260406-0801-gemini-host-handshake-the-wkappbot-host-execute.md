# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=7628  time=07:59:11  uptime=14275s  mem=131MB  system_processes=412"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_075818.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 156-145 561-256-507-572-267-372-263-362-327-672-316-751-316-756-351-572-276-267-263-126-276-236-206-236-260-263-207-726-760-275-263-562-623-253-236-321-253-567-705-236-325-123-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 527-325-573 276-123-326-627-251-257-540-261-751-312-236-317-731-627-752-267-725-635-265-754-275-263-237-256-237-275-527-257-627-263-261-263-316-306-132-306-567-576-574-576-576-5_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 576-312-507-257-762-250-752-671-726-361-317-607-602-216-526-572-705-576-715-523-572-276-705-123-501-317-657-750-317-672-576-175-621-574-732-627-231-276-267-273-531-765_V.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 710-175-710-107-170-710-710-715-173-715-175-715-710-736-170-713-176-173-713-731-731-713-175 107-617-701-715-710-105-571-170-173-710-173-710-715-713-731-713-317-371-751-713-715-7_V.mp3)
  Segment 5: 6304ms ~ 12640ms (file: 167-206-260-213-675-672-326-162-103-761-765-756-267-632-126-362-216-106-162-263-236-256-253-617-276-320-256-301-276-326-532-135-103-327-132-153-362-357-652-276-602-623-602-236-3_W.mp3)
  Segment 6: 13640ms ~ 14216ms (file: 672-265-576-130-132-756-372-627-623-657-632-261-620-362-653-576-365-265-562-675-615-372-761-765-651-576-652-672-657-562-523-265-562-675-532-523-673-567-376-567-657-562-536-576-3_W.mp3)
  Segment 7: 15216ms ~ 25296ms (file: 263-257-203-765-267-623-613-263-236-103-267-267-572-231-321-531-320-756-574-216-130-160-610-102-613-126-136-310-256-627-236-756-576-632-163-623-263-265-572-263-321-123-163-326-2_W.mp3)
  Segment 8: 26296ms ~ 26872ms (file: 542-652-602-621-261-123 654-574-567-654 574-256-206-130-754-725-654-652-652_W.mp3)
  Segment 9: 27872ms ~ 28448ms (file: 672-631-361-732-576-756-106-710-760-157-156-105-175-154-157-716-157-175-236-123-625-265-625-652-625-627-627-625-627-612-162-312-265-236-237-672-167-162-162-163-613-213-123-371-1_W.mp3)
  Segment 10: 29448ms ~ 38304ms (file: 132-103-164-312-203-613-130-162-301-102-312-207-230-176-310-310-276-203-103-310-130-230-237-132-107-130-763-321-675-206-371-203-267-132-103-321-301-103-201-602-261-123-130-751-3_W.mp3)
  Segment 11: 39304ms ~ 48016ms (file: 203-263-312-601-132-316-760-231-213-352-130-602-260-260-261-301-132-136-157-132-136-132-620-670-607-670-326-361-236-261-236-321-203-230-320-723-126-130-103-263-320-301-302-231-2_W.mp3)
  Segment 12: 49016ms ~ 49592ms (file: 312-107-750-571-756-750-756-751-751-157-750-750-750-175-751-751-152-175-152-132-517-715-751-751-715-175-107-157-170-571-751-570-751-715-750-750-750-750-751-756-750-756-750-576-1_V.mp3)
  Segment 13: 50592ms ~ 51168ms (file: 163-316-631-617-637-637-673-637-673-671_W.mp3)
  Segment 14: 52168ms ~ 52744ms (file: 672-605-765-760-673-706-765-357-175-672-163-316-137_W.mp3)
  Segment 15: 53744ms ~ 54320ms (file: 127-103-502-231-125-213-750-267-756-574-507-236-507-236-523-567-275-513-153-137-310-163-130-312-136-126-137-137-163-126-132-136-316_W.mp3)
  Segment 16: 55320ms ~ 55896ms (file: 753-753-573-571-175-570-571-756-571-517-571-570-750-570-571-571-517-571-571-756-175 571-570-570-571-570-571-751-715-715-750 103-715-175-715-715-715-715-750-715-715-710 570-570-1_W.mp3)
  Segment 17: 56896ms ~ 65788ms (file: 267-267-276-271-754-276-376-267-267-231-326-261-267-263-312-236-675-267-267-572-574-756-574-765-527-263-216-136-310-132-273-273-267-210-176-126-217-103-132-263-167-732-236-310-1_W.mp3)
  Segment 18: 66788ms ~ 67364ms (file: 534-750-756-567-653-650-572-713-753-317-132-213-153-321-312-573-571-357-321-675-763-521-537-351-625-527-316-570-162-527-731-517-752-173-137-136-312-125-172-167-325-261-651-570-1_V.mp3)
  Segment 19: 68364ms ~ 68940ms (file: 574-574-571-517-571-517-157 574-574-574 571-574-572-574-157-150 576-574-574-574-573-567-571-572-573-573-517-517-571-175-507-517 574-574-574-574-571-571-571-571-175-135-715-705-1_W.mp3)
  Segment 20: 69940ms ~ 70516ms (file: 576-513-570-517-576-751-761-517-375-751-715-756-571-751-517-571-715-157-715-571-571-175-517-517-513-573-571-517-571-571 562-576-753-576-157-753-512-357-517-751-375-573-571-517-1_V.mp3)
  Segment 21: 71516ms ~ 78500ms (file: 273-754-517-751-132-573-571-157-123-731-310-231-102-103-107-310-107-317-635-357-627-276-231-312-531-107-130-137-107-317-107-730-107-106-231-672-321-572-107-130-576-270-315-752-2_W.mp3)
  Segment 22: 79500ms ~ 80076ms (file: 315-621-756-752-623-276-371-362-713-506-265-231-712-735-572-576-627-723-527-257-573-756_V.mp3)
  Segment 23: 81076ms ~ 81652ms (file: 675-157-531-516-265-261-326-613-574-517-572-574-613-136-316-136-136-763-763-317-763-367-762-763-637-765-130-153-315-301-136-316-132-316-613-316-132-675-627-617-761-376-176-736-2_W.mp3)
  Segment 24: 82652ms ~ 91328ms (file: 137-130-136-326-136-263-261-216-576-576-576-302-103-157-672-620-762-627-321-310-237-106-123-312-302-320-263-321-130-123-132-752-672-263-725-312-321-231-312-621-216-102-216-126-2_W.mp3)
  Segment 25: 92328ms ~ 92904ms (file: 574-754-751-751-175-713-157-157-175-175-175-175 157-751-175-175-175-175 175-157-175-157-175-157-157-157-173 715-175-175-157-175-175 315-375-753-753-751-715-175-175-715-175-157-1_W.mp3)
  Segment 26: 93904ms ~ 94480ms (file: 754-574-576-165-527-571-675-657-572-576-526-752-356-513-157-157-175-573-570-571-254-574-456-572-574-573-576-576-657-625-256-526-567-756-756-576-576-756-765-654-457-547-574-576-5_W.mp3)
  Segment 27: 95480ms ~ 96056ms (file: 541-547-607-607-765-765-576-574-574-547-321-302-753-576-576-752-327_W.mp3)
  Segment 28: 97056ms ~ 104148ms (file: 126-620-326-576-135-260-312-574-153-301-130-574-574-571-320-574-573-756-130-267-261-213-375-317-236-731-301-134-130-236-123-320-320-715-136-261-106-126-265-765-267-265-362-756-7_W.mp3)
  Segment 29: 105148ms ~ 105724ms (file: 716-376-317-317-135-576-567-576-576-657-517-576-756-173-716-106_W.mp3)
  Segment 30: 106724ms ~ 115148ms (file: 312-176-126-120-210-321-123-312-130-137-301-312-310-136-362-316-361-361-361-301-310-103-261-735-312-627-267-203-302-735-752-275-752-752-761-316-367-317-731-765-375-573-351-752-2_W.mp3)
  Segment 31: 116148ms ~ 116724ms (file: 671-675-136-136-367-356-537-513-571-567-657-765-765-672-763-672-613-173-736-732-762-106-167_W.mp3)
  Segment 32: 117724ms ~ 118300ms (file: 710-761-175-710-750-507-701-157-715-751-751-751-751-175-751-715-751-715 675-167-761-701-715-671-715-705-751-675-750-751-705-750-751-750-170-750-751-751-710-750-170-710-751-715-7_V.mp3)
  Segment 33: 119300ms ~ 124340ms (file: 267-321-237-526-206-253-127-273-725-236-153-162-136-106-176-201-132-203-627-321-320-236-231-120-102-235-275-163-132-107-761-170-126-125-571-213-260-206-572-725-125-130-162-175-1_W.mp3)
  Segment 34: 125340ms ~ 131028ms (file: 316-317-310-312-123-132-213-206-230-123-236-201-206-230-263-231-230 261-213-236-260-102-103-127-230-230-210-203-123-102-132-106-260-260-620-601-132-172-123-602-607-162-103-320-7_W.mp3)
  Segment 35: 132028ms ~ 132604ms (file: 756-576-756-756-715-753-753-735-756-375-735-736-753-317-761-526-130-627-627-156-103-321-627-176-327-267-570-236-213-135-540-215-103-173-136-350-135-310-137-135-157-712-275-715-1_V.mp3)
  Segment 36: 133604ms ~ 134180ms (file: 713-613-376-321-126-162-163-162-163-126-163-126-163-132-762-765-762-736-672-723-671-267-273-723-276-672-127-371-672-637-163-163-132-162-163-162-163-762-672-675-657-657-657-657-6_W.mp3)
  Segment 37: 135180ms ~ 135756ms (file: 367-367-361-361-361-316-573-537-573-503-357-765-673-376-673-623-362-316-367-632-361-316-710-175-715-675-726-571-517-576-517-561-625-637-627-627-627_W.mp3)
  Segment 38: 136756ms ~ 147160ms (file: 120-627-627-527-123-321-725-312-106-754-765-572-126-231-102-102-123-132-231-213-762-627-627-321-231-137-230-512-236-527-120-267-267-231-126-726-765-627-627-126-627-527-120-102-2_W.mp3)
  Segment 39: 148160ms ~ 148736ms (file: 625-567-621-762-276-627-672-672-637-607-376-567-135-136-623-265-562-523-567-576-563-567-356-356-351-351-351-635-326-657-537-567-576-716-167-756-537-567-652-567-567-236-375-376-3_W.mp3)
  Segment 40: 149736ms ~ 150312ms (file: 107-173-715-710-730-750-715-750-715-571-175-715-751-715-753-375-751 754-756-756-751-705-750-751-175-175-135-173-157-175-571-715-175-753-750-751-571-571-752-517-571-571-750-571 5_W.mp3)
  Segment 41: 151312ms ~ 151888ms (file: 236-267-265-523-750-721-573-261-237-627-275-625-265-675-675-657-263-263-267-236-263-213-260-703-213-762-267-361-327-235-257-572-276-602-750-573-745-207-167-612-271-316-576-761-5_V.mp3)
  Segment 42: 152888ms ~ 162140ms (file: 627-276-267-123-632-276-320-203-216-321-263-130-572-576-213-310-267-576-310-213-260-213-126-201-103-370-176-375-132-301-123-231-320-326-130-153-237-260-310-326-310-106-263-260-2_V.mp3)
  Segment 43: 163140ms ~ 170952ms (file: 136-236-372-735-312-103-107-132-136-137-217-267-236-231-237-236-321-103-136-631-371-317-362-263-236-267-273-271-267-276-276-263-160-327-657-351-106-276-172-137-362-326-312-301-1_W.mp3)
  Segment 44: 171952ms ~ 177388ms (file: 316-106-135-103-105-175-175-163-130-126-163-106-106-102-175-216-270-276-613-132-123-716-751-510-531-310-125-316-617-257-702-370-132-130-571-517-102-206-132-215-132-123-137-173-1_V.mp3)
  Segment 45: 178388ms ~ 178964ms (file: 540-236-276 573 574-574-756-572-527-527-576-301_W.mp3)
  Segment 46: 179964ms ~ 180540ms (file: 517-570-135-150-107-120-157-571-103-175-705-705-105-157-715-750-710 517-351-135-305-175-507-105-751-351-352-573-731-701-713-371-175-175-751-170-107 517-571-157-175-157-102-102-5_V.mp3)
  Segment 47: 181540ms ~ 187660ms (file: 763-716-317-213-123-132-103-103-156-120-321-675-652-617-320-102-163-103-715-107-260-276-312-130-321-213-102-132-123-120-125-217-267-621-275-263-267-302-267-276-127-132-130-132-2_W.mp3)
  Segment 48: 188660ms ~ 193124ms (file: 573-574-574-576-752-315-123-213-267-267-576-762-672-627-726-276-672-276-127-172-751-213-172-321-120-130-103-123-132-132-102-130-712-102-321-327-217-216-132-260-613-213-127-172-2_W_stt_if_you_buy_a_loaf.mp3)
  Segment 49: 194124ms ~ 194700ms (file: 570 261-562-562-526-623-351-562-251-570-752-267-572-321-235-627-316-326-217-237-236-576-267-263-256-527-506-756-260-576-325-261-276-263-306-217-107-312-326-537-531-537-517-576-3_W.mp3)
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
> **Time**: 2026-04-06 08:01:55
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답TITLE: Audio Transcription and Analysis for Language Learning

{
"segment": 1,
"transcript": "ME 모델로 26빌리언 그러니까 260억 개죠 다음에 댄스 모델로 31빌리언까지 그러니까 스마트폰부터 랩탑이나 워크스테이션",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "ME", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "모델로", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "26빌리언", "start_ms": 1050, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그러니까", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "260억", "start_ms": 2300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "개죠", "start_ms": 2900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "댄스", "start_ms": 3750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "모델로", "start_ms": 4150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "31빌리언까지", "start_ms": 4600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그러니까", "start_ms": 5350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "스마트폰부터", "start_ms": 5850, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "랩탑이나", "start_ms": 6650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "워크스테이션", "start_ms": 7250, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "증시는 종전 기대감에 반응했습니다 나스닥 지수가 전 거래일보다 3.83% S&P 500 지수는 2.91% 급등했습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "증시는", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "종전", "start_ms": 650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "기대감에", "start_ms": 1050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "반응했습니다", "start_ms": 1650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "나스닥", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "지수가", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 3450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거래일보다", "start_ms": 3700, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "3.83%", "start_ms": 4400, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "S&P", "start_ms": 5300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "500", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지수는", "start_ms": 6150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "2.91%", "start_ms": 6650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "급등했습니다", "start_ms": 7450, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "트럼프 대통령이 호르무즈 해협 폐쇄가 꿈이 아니라 더 큰 흐름의 일부라고 분석했다 담강대학교의 천이판 교수는 많은 국가들이 양안 관계를 고려해 모호하거나 절충적인 표현을 사용하고 있다고 설명했다",
"speaker": "speaker_3",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "트럼프", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대통령이", "start_ms": 550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 1100, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "해협", "start_ms": 1700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "폐쇄가", "start_ms": 2100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "꿈이", "start_ms": 2600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 2950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 3400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 3650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "흐름의", "start_ms": 3950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일부라고", "start_ms": 4400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "분석했다", "start_ms": 5000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "담강대학교의", "start_ms": 5800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "천이판", "start_ms": 6650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "교수는", "start_ms": 7150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 7600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "국가들이", "start_ms": 8000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "양안", "start_ms": 8550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "관계를", "start_ms": 8950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "고려해", "start_ms": 9400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "모호하거나", "start_ms": 9900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "절충적인", "start_ms": 10650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "표현을", "start_ms": 11300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사용하고", "start_ms": 11800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 12400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "설명했다", "start_ms": 12850, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "People go to the Jeon-jib, Jeon restaurant, to get this and then also have Makgeolli, if you remember. Yes, and they'll have it together. This was the first time I made this.",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "en",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "People", "start_ms": 200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "go", "start_ms": 600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Jeon-jib,", "start_ms": 1250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "Jeon", "start_ms": 1800, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "restaurant,", "start_ms": 2150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "get", "start_ms": 3000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 3300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 3650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "then", "start_ms": 3900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "also", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 4550, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "Makgeolli,", "start_ms": 4850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "if", "start_ms": 5550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 5750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "remember.", "start_ms": 6000, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "구체적인 내용에 대해선 답을 피했습니다. MBC 뉴스 황소현입니다. 미국과 이란의 전쟁을 계기로 세계가 핵 도미노에 노출될 수 있다는 경고가 나오고 있습니다. 영국 왕립 국제 문제",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "구체적인", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "내용에", "start_ms": 800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대해선", "start_ms": 1300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "답을", "start_ms": 1850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "피했습니다.", "start_ms": 2200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "MBC", "start_ms": 3000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "황소현입니다.", "start_ms": 3850, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "미국과", "start_ms": 4800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이란의", "start_ms": 5350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전쟁을", "start_ms": 5850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "계기로", "start_ms": 6350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "세계가", "start_ms": 6850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "핵", "start_ms": 7350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "도미노에", "start_ms": 7600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "노출될", "start_ms": 8150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 8650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있다는", "start_ms": 8850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "경고가", "start_ms": 9300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나오고", "start_ms": 9800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 10300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "영국", "start_ms": 11000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "왕립", "start_ms": 11450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 11900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "문제", "start_ms": 12300, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "제가 처음에 생각했던 거는 에이전트가 워낙에 똑똑하다 보니까 그 똑똑한 점을 이용해서 아 우리가 지금 힐링을 하기 위해서",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "처음에", "start_ms": 500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "생각했던", "start_ms": 1000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 1650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "에이전트가", "start_ms": 2000, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "워낙에", "start_ms": 2750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "똑똑하다", "start_ms": 3250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 3900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 4400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "똑똑한", "start_ms": 4600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "점을", "start_ms": 5150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이용해서", "start_ms": 5550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 6200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 6400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 6850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "힐링을", "start_ms": 7250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 7800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "위해서", "start_ms": 8200, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "연관된다고 어떻게 보면 생산력까지 연관되는데 핵심 자산이죠 이 핵심 자산을 자국에서 한국 같은 경우는 한국 내에서도 생산할 수 있어야 되고요 미국은 미국 내에서",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "연관된다고", "start_ms": 180, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 1450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "생산력까지", "start_ms": 1850, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "연관되는데", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 3350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자산이죠", "start_ms": 3750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 4550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자산을", "start_ms": 4950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "자국에서", "start_ms": 5450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 6100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 6550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "경우는", "start_ms": 6950, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 7450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내에서도", "start_ms": 7900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "생산할", "start_ms": 8500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 9050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있어야", "start_ms": 9250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되고요", "start_ms": 9750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미국은", "start_ms": 10250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 10750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내에서", "start_ms": 11200, "dur_ms": 440, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "대한민국이 최초로 개발하는 이 역사적인 스텔스 전투기 개발 사업의 파트너로 함께하며 그래서 무려 그동안 우리 KF-21의 동체는 언제나 대",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "대한민국이", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "최초로", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "개발하는", "start_ms": 1450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "역사적인", "start_ms": 2300, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "스텔스", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "전투기", "start_ms": 3550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "개발", "start_ms": 4150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사업의", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "파트너로", "start_ms": 5100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "함께하며", "start_ms": 5750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 6400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "무려", "start_ms": 6900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그동안", "start_ms": 7300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 7800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "KF-21의", "start_ms": 8200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "동체는", "start_ms": 8950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "언제나", "start_ms": 9500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대", "start_ms": 10050, "dur_ms": 262, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "중동 전쟁은 누가 먼저 바닥나느냐를 겨루는 소모전으로 바뀌고 있습니다 실제로 이란은 개전 이후",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "중동", "start_ms": 120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전쟁은", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "누가", "start_ms": 1050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 1400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "바닥나느냐를", "start_ms": 1800, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "겨루는", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "소모전으로", "start_ms": 3150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "바뀌고", "start_ms": 3900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "실제로", "start_ms": 4950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이란은", "start_ms": 5500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "개전", "start_ms": 6000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이후", "start_ms": 6400, "dur_ms": 328, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "이스라엘은 방어 능력 자체보다 지속 가능성에서 압박을 받고 있는 상황입니다 그런데 같은 시기 전혀 다른 선택을 한 국가가 있습니다 아랍에미리트입니다 UAE 역시 이란의 미사일과 드론 공격을 받고 있지만 완전히 다",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "이스라엘은", "start_ms": 150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "방어", "start_ms": 850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "능력", "start_ms": 1250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "자체보다", "start_ms": 1650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "지속", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "가능성에서", "start_ms": 2650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "압박을", "start_ms": 3400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "받고", "start_ms": 3900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 4300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "상황입니다", "start_ms": 4650, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 5400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 5900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시기", "start_ms": 6300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전혀", "start_ms": 6700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "다른", "start_ms": 7100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선택을", "start_ms": 7500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 8050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "국가가", "start_ms": 8300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 8800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아랍에미리트입니다", "start_ms": 9350, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "UAE", "start_ms": 10400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "역시", "start_ms": 10950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이란의", "start_ms": 11400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미사일과", "start_ms": 11900, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "드론", "start_ms": 12550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "공격을", "start_ms": 12950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "받고", "start_ms": 13500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있지만", "start_ms": 13950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "완전히", "start_ms": 14500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 15050, "dur_ms": 246, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "에서 이 문제를 제기했던 것이고요 제 취재가 맞느냐 이거는 팩트의 영역은 아니고 사람들이 그렇게 해석을 하는 것이기 때문에 저도 100% 자신감이 없었는데 오히려 이 특사경 수사 지휘 권한이 없어진 게 결정되고 군",
"speaker": "speaker_9",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "에서", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "문제를", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제기했던", "start_ms": 1100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "것이고요", "start_ms": 1750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "취재가", "start_ms": 2550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "맞느냐", "start_ms": 3100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이거는", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "팩트의", "start_ms": 4100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "영역은", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아니고", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사람들이", "start_ms": 5550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 6200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "해석을", "start_ms": 6700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 7250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것이기", "start_ms": 7650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 8150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "저도", "start_ms": 8650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "100%", "start_ms": 9050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "자신감이", "start_ms": 9700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "없었는데", "start_ms": 10450, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "오히려", "start_ms": 11150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 11700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "특사경", "start_ms": 11900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "수사", "start_ms": 12450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지휘", "start_ms": 12850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "권한이", "start_ms": 13250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "없어진", "start_ms": 13800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 14400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "결정되고", "start_ms": 14600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "군", "start_ms": 15350, "dur_ms": 226, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "F-15 전투기 실종 조종사를 찾기 위해 미국과 이란이 경쟁적으로 수색 작업을 이어가고 있습니다 미군 특수부대 투입설까지 보도되는 가운데 이란은",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "F-15", "start_ms": 150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전투기", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "실종", "start_ms": 1350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조종사를", "start_ms": 1800, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "찾기", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "미국과", "start_ms": 3350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이란이", "start_ms": 3900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "경쟁적으로", "start_ms": 4400, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "수색", "start_ms": 5200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "작업을", "start_ms": 5650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이어가고", "start_ms": 6200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 6800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "미군", "start_ms": 7400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "특수부대", "start_ms": 7850, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "투입설까지", "start_ms": 8550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "보도되는", "start_ms": 9350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "가운데", "start_ms": 10000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이란은", "start_ms": 10550, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": "speaker_0",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 08:01:55*
