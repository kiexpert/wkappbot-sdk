# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=34276  time=03:25:30  uptime=9255s  mem=443MB  system_processes=368"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_032314.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 607-672-607-672-673-672-673-670-567-605-672-675-627-756-675-306-301-136-507-650-672-607-672-627-657-627-106-167-613-613-162-162-621-132-631-301-163-132-570-361-750-136-375-267-3_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 372-326-362-231-367-217-265-672-361-236-261-236-362-316-326-321-327-326-236-263-362-263-236-263-362-326-365-356-376-612-673-213-126-623-167-671-671-671-267-267-261-267-271-275-5_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 762-756-752-716-671-617-167-162-617-167_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 103-103-564-125-127-176-316-163-610-635-617-106-163-631-107-136-761-123-167-165-610-163-163-103-173-610-136-160-167-561-136-316-167-160-613-620-106-602-132-162-163-103-613-162-1_V.mp3)
  Segment 5: 6304ms ~ 12568ms (file: 216-216-263-267-623-612-316-263-312-261-126-321-231-213-216-136-326-261-627-170-673-612-203-125-206-261-261-263-572-321-321-127-217-163-261-162-372-132-216-617-103-156-126-123-1_W.mp3)
  Segment 6: 13568ms ~ 14144ms (file: 132-126-136-123-136-574-516-135-132-136-132-312-132-231-126-162-136-132-162-132-132_W.mp3)
  Segment 7: 15144ms ~ 15720ms (file: 627-756-765-175-310-301-132-765-607-567-671-632-632-621-263-216-621-617-123-612-612-130-312-326-236-123-620-612-162-321-312-316-162-150-531-135-314-160-167-163-162-137-137-314-3_V.mp3)
  Segment 8: 16720ms ~ 17296ms (file: 123-512-512-321-301-310-756-675-651-136-135-173-574-571-571-517-574-756-750-756-715-176-261-657-627-765-657-612-275-627-657-715-632-632-327-756-137-312-613-651-163-356-576-657-1_W.mp3)
  Segment 9: 18296ms ~ 18872ms (file: 312-125-162-132-261-123-132-316-136-126-132-124-164-153-213 216-132-312-132-213-231-321-235-231-203-206-356-271-231-372-607-601-261-123-650-265-267-621-267-267-267-267-602-623-3_V.mp3)
  Segment 10: 19872ms ~ 27828ms (file: 325-317-301-762-723-723-376-627-602-726-275-267-276-725-705-726-317-375-756-752-275-321-123-627-160-765-756-765-513-137-365-617-756-576-762-673-753-574-152-735-756-571-571-735-7_W.mp3)
  Segment 11: 28828ms ~ 34516ms (file: 213-120-132-123-217-231-213-127-273-725-327-267-276-321-312-231-217-276-267-327-201-120-172-372-321-127-213-273-213-237-123-327-237-236-327-532-231-273-236-261-362-162-231-312-7_W.mp3)
  Segment 12: 35516ms ~ 36092ms (file: 765-765-675-675-675-607-675 612-627-675-765-765-675-176-371-103-361-537-163-312-103-132-106-301-617-632-612-263-613-136-106-107-135-136-610-136-160-103-103-136-103-546-561-167-1_V.mp3)
  Segment 13: 37092ms ~ 44976ms (file: 206-261-130-103-201-260-136-231-267-261-260-316-216-106-136-315-120-206-206-236-102-320-627-206-256-261-627-621-230-320-317-136-163-260-103-574-157-275-267-230-163-316-701-712-1_W.mp3)
  Segment 14: 45976ms ~ 46552ms (file: 362-103-132-670-627-107-172-173-132-176-132-103-176-137-163 765-376-306-307-307-321-673-173-312-213-216-261-612-612-176-137-670-167-126-261-263-267-261-612-162-103-130-136-316-1_V.mp3)
  Segment 15: 47552ms ~ 56696ms (file: 301-201-206-267-276-267-123-726-627-672-672-201-107-267-652-267-260-261-132-267-236-213-267-267-276-726-312-210-130-260-627-103-136-267-260-627-627-627-261-267-260-312-301-260-6_W.mp3)
  Segment 16: 57696ms ~ 70944ms (file: 132-237-320-132-103-765-267-301-103 231 217-726-273-231-261-620-236-310-263-201-265-213-230-306-216-213-321-320-210-326-206-236-135-735-175-163-263-205-312-316-237-263-320-130-3_W.mp3)
  Segment 17: 71944ms ~ 72520ms (file: 657-617-165-165-126-132-162-162-130-135-156-561-516-561-651-615-635-316-136-136-123-132-157-217-157-517-561-570-576-516-175-157-517-517-157-157-576-756-576-756-756-756-576-576-7_W.mp3)
  Segment 18: 73520ms ~ 74096ms (file: 613-657-501-615-367-651-567-561-613-165-165-361-361-756-675-763-163-610-136-136-136-176-163-651-361-136-130-136-103-130-106-107-103-613-167-263-261-602-103-317-137-167-615-317-1_W.mp3)
  Segment 19: 75096ms ~ 75672ms (file: 103 136-176-721-371-157-106-173-136-671-163-312-136-136-103-163-170-136-162-176-163-136-103-130 765-675-675-367-160-761-765-765-765-175-675-675-675-176-675-675-675-675_W.mp3)
  Segment 20: 76672ms ~ 77248ms (file: 126-231-162-612-126-123-312-176-762-756-567-613-612-162-167-716-621-163_W.mp3)
  Segment 21: 78248ms ~ 78824ms (file: 630-163-650-167-625-167-735-132-163-716-127_V.mp3)
  Segment 22: 79824ms ~ 80400ms (file: 520-261-621-612-163-216-261-236-263-126-132-213-362-267-120-312-312-236-213-563-520-162-216-612-102-107-203-103-126-102-153-126-260-236-306-301-103-132-103-103-103-103-320-103-2_V.mp3)
  Segment 23: 81400ms ~ 81976ms (file: 130-130-130-623-362-637-607-671-260-621-627-617-206-672-132-162-213-130-132-321-132-326-236 625-132-763-173-371-261-263-163-601-621-231-162-621-326-273-632-261-163-103-762-126-6_V.mp3)
  Segment 24: 82976ms ~ 89960ms (file: 132-507-576-175-206-725-547-657-602-637-605-613-621-601-602-627-671-750-265-371-106-260-627-106-761-267-621-260-267-267-657-620-160-132-610-621-620-620-621-617-602-602-620-263-2_W.mp3)
  Segment 25: 90960ms ~ 91536ms (file: 675-576-657-765-756-576-567-576-756-675-567-576-567-675-756-567-576-567-657_W.mp3)
  Segment 26: 92536ms ~ 93112ms (file: 657-362-602-265-625 675-765-723-327-562-652-261-267-263-231-263-256-263-263-276-213-657-561-612 635-327-367-537_W.mp3)
  Segment 27: 94112ms ~ 94688ms (file: 213-231-126-132-136-672-276-672-136-136-126-162-162-163-120-136-162-136-165-136-136-310-216-231-621-120-617-260-162-625-623-102-321-365-163-136-261-321-326-130-263-103-613-625-2_V.mp3)
  Segment 28: 95688ms ~ 96264ms (file: 103-103-321-312-612-160-130-301-637-567-657-310 670-670-136-132 621-612-167-756-362-103-105-251-612-672-627-672_W.mp3)
  Segment 29: 97264ms ~ 97840ms (file: 157-312-132-132-213-623-321-163-135-156-153-103-153-163-163-157-105-312-326-236-723-276-276-732-273-271-762-765 263 312 170-173_W.mp3)
  Segment 30: 98840ms ~ 99416ms (file: 107-175-156-137-216-156-162-103-123-621-103-312-301-312-103-132-136-156-263-157-123-135-136-136-316-567-106-130-275-623-132-672-231-163-162-132-756-657-136-617-672-673-607-163-6_V.mp3)
  Segment 31: 100416ms ~ 100992ms (file: 312-613-360-132-213-657-572-562-527-123-132-132-312-120-162-312-362-263-126-267-217-261-627-617-162-126-132-230-102-613-602-236-213-267-123-326-163-627-123-132-302-321-216-231-3_W.mp3)
  Segment 32: 101992ms ~ 102568ms (file: 167-160-103-136-136-167-657-156-653-567-576-106-176-160-106-615-130-132-135-761-157-612-617-572-617-156-102-617-601-160-103-136-106-136-137-132-517-352-106-176-610-167-623-163-1_W.mp3)
  Segment 33: 103568ms ~ 104144ms (file: 136-163-612-123-162-321 316-361-136-137-316-130-310-372-631-137-354-136-261-136-103-136-713-173-132-610-612-617-167-163-106-162-216-162-675-163-602-316-103 163-316-130-612-612-1_V.mp3)
  Segment 34: 105144ms ~ 105720ms (file: 627-602-261-260-237-206-263-203-130-103-107-107-132 627-261-261-206-163-631-123-201-621-132-657-105-165-267-163-756-107-106-267-316-316-216-523-532-126-132-123-163-527-576-576-5_W.mp3)
  Segment 35: 106720ms ~ 107296ms (file: 765-706-765-675-675-765-576-760-675-765-765-756-657-675-567-765-765-756-576-576-756-756-765-675-657-765-756-675-675-672-675-765-672-762-762-567-756-756-265-657-675-675-675-765-6_W.mp3)
  Segment 36: 108296ms ~ 108872ms (file: 312-623-316-361-632-361-623-162-612-613-132-657-167-617-617-265-621-621-612-615-617-176-761-627-167-576-756-167-671-627-672-176-312-163-617-613-362-216-236-721-327-263-612-327-3_W.mp3)
  Segment 37: 109872ms ~ 110448ms (file: 615-167-576 627-621-632-621-326-321-312-123-312-321-623-163-762-761-165_W.mp3)
  Segment 38: 111448ms ~ 112024ms (file: 263-620-276-752-576-570-135-736-576-675-576-756-675-675-756-765-657-675-623-263-263-672-672-267-672-637-675-716-237-371-765-362-237-316-756-637-621-263-672-673-765-675-760-561-5_W.mp3)
  Segment 39: 113024ms ~ 119000ms (file: 732-731-317-756-275-237-321-267-172-127-627-726-756-736-351-315-762-756-762-627-726-762-732-725-327-756-574-127-726-372-715-726-723-731-371-753-752-321-751-751-753-375-756-756-7_W.mp3)
  Segment 40: 120000ms ~ 120576ms (file: 126-230-263-162-612-236-162-672-163-157 132-517-154 106-617-103-654-126-167_W.mp3)
  Segment 41: 121576ms ~ 122152ms (file: 125-162-603-617-765-632-263-361-130-162-156-631-137 165-167-162-160-103-567-605-716-621-752-601-620 601-631-610-167-136-103-132-564-312-126-167_V.mp3)
  Segment 42: 123152ms ~ 123728ms (file: 260-260-260-261-203-302-572-652-265-275-213-130-132-312-106-621-120-236-327-751-125-103-107-261-120-210-102-627-672-627-607-607-602-602-602-620-620-620-261 260-263-260-627-620-2_W.mp3)
  Segment 43: 124728ms ~ 125304ms (file: 126-127-613-136-136-103-103-103-130-310 123-132-103-231-312-316-361-236_V.mp3)
  Segment 44: 126304ms ~ 134620ms (file: 321-123-102-213-163-152-125-162-230-527-230-120-132-164-312-132-163-231-167-103-267-752-217-527-213-132-231-517-215-123-573-572-257-256-267-230-231-231-213-237-120-320-213-320-2_V.mp3)
  Segment 45: 135620ms ~ 136196ms (file: 167-367-637-163-637-637-631-163-361-671-631-163-632-637-627-637-637-637-672-672-673_W.mp3)
  Segment 46: 137196ms ~ 137772ms (file: 362-173-321-106-167-132-167-176-106-165-130-130-163-164-132-162-312-103-162-160-163-103-615-106-167-610-160-671-156-160-103-167-103-176-726-576-136-103-106-103-176-106-136-162-1_V.mp3)
  Segment 47: 138772ms ~ 139348ms (file: 136-267-675-572-652-526-567-752-576-527-651-517-157-123-265-657-576-315-136-315-173-576-217-132-231 570-321-520-163-213-572-325-152-750-261-620-162-162-517-725-162-726-261-607-3_V.mp3)
  Segment 48: 140348ms ~ 140924ms (file: 516-103-132-167-167-136-156-627-760-765-716-765-751-751-761-317-127-136-562-103-103-103-103-136-615-167-315-136-135-136-612-137-316-137-103-106-132-130-103-106-163-356-316-130-1_V.mp3)
  Segment 49: 141924ms ~ 142500ms (file: 103-270-620-672-267-621-612-620-602-610-610-106-126-612-137-106-163-136-701-765-670-102-103-106-103-620-620-620-620-602-602-610-267-637-632-602-321-307-310-301-301-163-756-107-1_V.mp3)
  Segment 50: 143500ms ~ 144076ms (file: 102-103-123-132-125-102-103-136-136 173-163-167 261-216-216 236-263 765-576-710-165-627-627-756-267-672-627-621-627-617-762-726-326-753-356-675-657-632-267-673-762-672-672-617-1_W.mp3)
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
> **Time**: 2026-04-04 03:26:20
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "국제 무대에서 한국의 역할이 더욱 중요해진 것입니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "국제", "start_ms": 20, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "무대에서", "start_ms": 100, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한국의", "start_ms": 220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "역할이", "start_ms": 300, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "더욱", "start_ms": 360, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "중요해진", "start_ms": 420, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것입니다.", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "그리고 마지막으로 가장 중요한 이유가 있는데 그것은 바로 신뢰입니다.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 10, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "마지막으로", "start_ms": 70, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 180, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이유가", "start_ms": 320, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 390, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그것은", "start_ms": 450, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "신뢰입니다.", "start_ms": 540, "dur_ms": 36, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "한국은 국제 규칙을 지키고 위기 상황에서도 책임 있게 행동해온 국가입니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "한국은", "start_ms": 15, "dur_ms": 75, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 90, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "규칙을", "start_ms": 150, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "지키고", "start_ms": 230, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "위기", "start_ms": 300, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "상황에서도", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "책임", "start_ms": 450, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "행동해온", "start_ms": 540, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "국가입니다.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "또한 한국은 경제와 기술 분야에서 세계적인 수준의 경쟁력을 갖추고 있습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "또한", "start_ms": 20, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "한국은", "start_ms": 80, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "경제와", "start_ms": 150, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 220, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "분야에서", "start_ms": 280, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "세계적인", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수준의", "start_ms": 450, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "경쟁력을", "start_ms": 500, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "갖추고", "start_ms": 540, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
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
"transcript": "곧바로 로봇 진단에 나선 박혜원 교수",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "곧바로", "start_ms": 30, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "진단에", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나선", "start_ms": 300, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "박혜원", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "교수", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
*Generated by WKAppBot ask gemini — 2026-04-04 03:26:20*
