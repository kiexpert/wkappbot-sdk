# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=30288  time=03:56:23  uptime=9427s  mem=624MB  system_processes=363"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_035614.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 312-572-573-521-132-576-571-103-103-132-761 236-675-675-675-675 657-657-627-672-672-637-765-675-627-261-762-130-136-132-317-153-312-132-307-236-763-160-167-106-106-130-162-613-1_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 123-132-123-653-230-312-321-203-213-576-162-572-576-756-576-163 203-237-237-237-230-203-237-203-207-230-320-203 153_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 126-263-215-651-153-153-135-265-216-236-216-123 102-103-316-103-126-162-165-352-163-312-132-132-125-136-135-163-176-765-675-576-671-657-361-517-517-316-127-653-512-562-165-162-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 261-231 167-103-106-156-132-103-152-613-167-321-617-617-167-675-163-162-103-316-175-176-567-231-136-672-162-130-301-156-136-106-561-136-123-103-510-165-106-160-126-103-162-631-1_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 605-605-650-653-635-605-605-652 650-650-650-605-653-605-650-653-650 650-605-605-605-605-605-605 620-605-605-605-605-605-602-605 650-605-650-605-605-650-652 605-605-605-605-650-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 301-162-160-617-102-761-675-163-163-132-103-617-176-126-103-130-167-136-103-162-126-132-162-103-106-103-106-165-136-613-176-132-175-126-106-310-103-215-126-106-106-160-136-165-1_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 673-612-671-627-671-675-637-316-637-637-726-627-567-561-675-762-762-627-657-672-761-675-165-136-216-267-627-376-672-762-627-261-632-621-621-672-632-621-627-672-617-167-657-675-6_W.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 756-576-754-627-547-675-625-510-123-321-135-715-756-765-571-137-715-756-362-126-102-157-103_W.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 167-163-167-163-167-671-672-267-623-576-175-765-276-675-675-367-516-567-657-627-627-627-627-267-261-621-621-261-265-652-256-562-361-356-567-657-675-156-657-675-325-523-265-627-2_W.mp3)
  Segment 10: 14184ms ~ 19908ms (file: 107-103-237-276-573-103-230-276-276-570-507-756-357-574-574-157-301-213-103-132-573-573-675-367-361-653-631-136-136-163-316-136-103-361-631-136-163-163-613-136-316-130-126-162-3_W.mp3)
  Segment 11: 20908ms ~ 21484ms (file: 501-514-135-132-540-534-613-206-260-316-261-621-132-621-362-230-236-263-236-132-312-102-136-756-675-607-103-302-236-203-236-302-321-207-206-627-162-127-127-716-175-706-756-750-6_W.mp3)
  Segment 12: 22484ms ~ 23060ms (file: 237-125-123-132-312-201-126-312-213-132-731-312-132-267-132-376-231-203-267-302-312-756-267-321-312-306-607-276-103-231-123-103-103-132-130-316-130-651-267-127-126-107-126-132-2_W.mp3)
  Segment 13: 24060ms ~ 24636ms (file: 675 607-657-657-567-576-567-576-576-576-576-675-576-576-576-576-576-675-675-675-675 576-567-567-576-576-567-576-576-756-576-756-675-576-576-576-567-765 657-567-567-576-567-576-5_V.mp3)
  Segment 14: 25636ms ~ 30892ms (file: 123-217-210-572-253-261-130-120-203-210-162-321-350-327-260-123-130-762-572-715-162-751-175-753-106-736-173-137-271-267-312-137-310-762-237-321-123-132-317-267-267-261-267-267-2_W.mp3)
  Segment 15: 31892ms ~ 32468ms (file: 176-176-103-106 130-132 573-657 176-167-615-375-561-136-106-316-132-103-506-567-165-574-570-507-756-162-560-657-620-263-162-160-162-261-263-670-103-103-163-157-576-136-326-617-5_V.mp3)
  Segment 16: 33468ms ~ 34044ms (file: 216-231-712-371-312-213-375-351-376-763-673-657-574-612-672-671-673-637-627-627-261-326-675-670-637-671-672-673-670-670-673-671-627-125-217-542-153-321-321-312-237-132-327-327-2_W.mp3)
  Segment 17: 35044ms ~ 35620ms (file: 103-107-160-103-103-612-162-627-675-127-165-106-617-103-523-106-160-130-103-163-105-156-123-127-106-163-657-651-571-312-162-527-625-657-175-213-103-165-516-153-106-625-106-537-6_V.mp3)
  Segment 18: 36620ms ~ 37196ms (file: 675-675-675-657-657-675-765 567-576-576-576-576-576-657-576-576-675-756-576 567-576-574-576-576-567-567-576-576-576-576-576-576-576-576-675-675-675-675-675-675 576-576-576-576-5_W.mp3)
  Segment 19: 38196ms ~ 51696ms (file: 756-765-176-107-107-123-765-675-756-765-106-267-126-173-103-765-765-762-210-276-267-213-176-174-173-312-236-236-327-231-172-107-261-236-267-123-310-762-765-671-362-756-710-216-2_W.mp3)
  Segment 20: 52696ms ~ 53272ms (file: 765-675-675-675-675-657 567-576-576-576-576-576-576-576-576-657-657-756-576-576-576 576-576-576-576-576-756-576-756-567-576-576-756-576-567-576-576-756-576-576-567 567-576-567-5_W.mp3)
  Segment 21: 54272ms ~ 54848ms (file: 167-106-612-136-162-106-160-163-260-176-120-217-103-160-163-162-103-102-126-160-263-657-651-625-321-504-150-216-126-123 506-506-763 167-126-627-621-532-321-213-263-537-567-563-6_V.mp3)
  Segment 22: 55848ms ~ 56424ms (file: 371-162-716-123-732-173-263-236-326-132-735-726-765-162-617 165-602-123 126 263-321-103-136-103-136-125-136-136-132-531-103-137-617-103-651-751-703-263-312-103-261-160-136-312-1_V.mp3)
  Segment 23: 57424ms ~ 58000ms (file: 625-261-625-625-652-562-625-625-625 265-623-623-621-261-621-625-261-621-625-625-621-623-621-265-623-625-625-625-256-652-562-652-652-562-625-625-625 625-621-261-265-621-621-261-6_W.mp3)
  Segment 24: 59000ms ~ 59576ms (file: 623-627-620-632-362-632-263-623-362-623-362-632-623-632-623-632-627-623-623-620-623-362-632-623-263-632-623-625-362-623_W.mp3)
  Segment 25: 60576ms ~ 61152ms (file: 761-765-675-162-126-716-136-317 126_V.mp3)
  Segment 26: 62152ms ~ 62728ms (file: 132-164 126-756-576-567-162 213-316-612-267-163 623-206-236-627-627-126-216-230-231-123-213-372-372-371-126-163-261-162-312-134-165-163-251-135-623 615-132-123-317-317-310-317-6_V.mp3)
  Segment 27: 63728ms ~ 64304ms (file: 765-756-567-765-567-765-675-765-675-605-761-167-617-237-621-765-675-657-765-751-651-765-756-765-675-657-756-576-567-562-527-576-652-567-657-157-167-765-657-756-657-567-765-657-6_W.mp3)
  Segment 28: 65304ms ~ 65880ms (file: 375-213-127-132-103 103-103-621-627-632-367-362-310-136-162-163 765-567-567-576-765-675-675-657-567 652-256-567-765-675 316-361-610-631-631-637-576-675-567-576-675-671-675-675-7_W.mp3)
  Segment 29: 66880ms ~ 67456ms (file: 361-312-361-367-763-765-675-765-765-765-765-362-123-132-617-312-362-726-276-267-361-371-572-720-731-371-261-236-765-765-756-765-765-716-765-765-765-765-765-765-765-756-765-765-7_W.mp3)
  Segment 30: 68456ms ~ 69032ms (file: 206-512-571 137-265-236-263-236-236 267-263-162-637-672-675-576-657-576-261-126-621-263-260 236-162-175-103-130-106-163-632-623-657-612-126-601-102-132-132-302-176-160-123-130-5_W.mp3)
  Segment 31: 70032ms ~ 70608ms (file: 316-320-632-623-320-302-231-230-576-273-126-106-167-167-163-216-621-621-163-260-310-132-312-130-106-106-130-136-130-360-623-263-623-103-236-263-236-136-106-631-510-230-236-206-2_V.mp3)
  Segment 32: 71608ms ~ 72184ms (file: 136-136-167-132-103-103-132-314-103-123-103 164-132-103-103-103-137-103-132-103-103-760-765-736-763-756-127-126-756-756-761-762-671-675-657-651-756-675 617-160-561-561-156-156-7_W.mp3)
  Segment 33: 73184ms ~ 73760ms (file: 530-527-540-503-537-175-157-675-356-526-516-156-516-576-135-315-351-132-367-361-351-527-125-153-567-576-574-576-162-576-375-657-625-625-652-625-625-620-265-264-625-632-620-627-6_W.mp3)
  Segment 34: 74760ms ~ 75336ms (file: 675-657-576-216-130-167-672-231-201 623-621-361-547-143-132-157-621-627-627-261-263-263-263-261-617-162-261-160-106-167-267-216-261-756-756-576-527-261-671-726-657-261-136-612-6_W.mp3)
  Segment 35: 76336ms ~ 76912ms (file: 657-650-567-567-657-567-657-657-657-672-607-670-607-675-765-672-765-765 605-650-657-560-567-576-506-560-657-576-567-576-576-567-567-567-657-576-567-657-670-672-675-675-765-675 6_W.mp3)
  Segment 36: 77912ms ~ 78488ms (file: 567-265-657-657-657-657-651-261-617-361-367-621-261-627-620-620-635-670-367-673-675-536-576-567-156-516-657-625-657-621-670-625-260-567-653-675-675-657-657-657-765-675-635-576-5_W.mp3)
  Segment 37: 79488ms ~ 80064ms (file: 130-312-107-164-106-672-617-162-162-162-312-316-103-132-136-564-263-576-130-136-132-130-132-123-130-136-136-136-316-203-675-561-321-271-162-162-612-136-103 632-620-130-163-130-2_V.mp3)
  Segment 38: 81064ms ~ 81640ms (file: 576-657-675-617-527-123-321-302-623-326-623-621-263-136 103-132-310-765-361-631-621-162-627-671-621-236-326-312-130-321-260-260-260-216-216-127-657-657-652-612 102-137-163-127-1_V.mp3)
  Segment 39: 82640ms ~ 88256ms (file: 231-231-320-132-130-753-325-312-160-106-106-236-306-106-356-136-321-236-132-126-751-756-627-357-573-263-231-273-751-130-103-576-726-162-265-627-572-627-725-502-237-312-731-327-3_W.mp3)
  Segment 40: 89256ms ~ 89832ms (file: 621-136-267-267-627-672-631-136 573-502-256_W.mp3)
  Segment 41: 90832ms ~ 91408ms (file: 362-362-312-321-326 321-376-376-317-312_W.mp3)
  Segment 42: 92408ms ~ 92984ms (file: 103-137-316-617-132 106-136-136-623-631-316-136-163-103-136-615-136-362-651-316-103-675-361-316-316-613-103-163-103-103-136-156-751-103-132-103-310-132-316-103-316-103-136-613-6_V.mp3)
  Segment 43: 93984ms ~ 94560ms (file: 671-673-670-671-123-326-657-576-576-312 327-321-326-326-576-576-567-130-103-623-301-302-763-672-763-673-672-627-627-672-637-316-321-326-326-236-236-321-231-213-326-367-327-726-3_W.mp3)
  Segment 44: 95560ms ~ 96136ms (file: 132-123-123-163-261-206-326-236-123-137-132-132 317 123-267-261-260-263-136-136_V.mp3)
  Segment 45: 97136ms ~ 97712ms (file: 567-657-765 576-570-571-537-673-563 613-130-102-541-534-216-103-657-623-623-157-512-567-103-175-726 163-213-276 123_W.mp3)
  Segment 46: 98712ms ~ 99288ms (file: 371-317-136-617-612-105-136-130-136-156-103-130-137-103-136-315-136-103-130-315-361-136-103-103-136-136-316-163-136-103-312-126-163-165-527-361-671-671-627-162-617 106-103-137-1_V.mp3)
  Segment 47: 100288ms ~ 100864ms (file: 137-130-316-163-136-106-167-167-675-613-312-156-176-670-276-172-652-576-657-576-263-163-261-652-267-627-621-265-261-627-130-236-132-106-162-627-106-176-637-672-632-376-673-267-2_W.mp3)
  Segment 48: 101864ms ~ 102440ms (file: 213-126-123-123-132-321-132-123-213-125-257-215-256-231-235-126-216-123-127-126-123-231-126-126-362-562-527-527-526-567-257-572-267-265-372-173-715-375-371-267-567-573-573-352-3_W.mp3)
  Segment 49: 103440ms ~ 104016ms (file: 162-132-106-167-617-106-106-716-176-603-516-160-170-172 103-154-160-312-617-162-106-136-103-106-132-125-670-176-126-103-132-132-136 167-163-165-167-326-176-167-132-162-756-213-7_V.mp3)
  Segment 50: 105016ms ~ 105592ms (file: 203-231-123-260-602-620-260-621-206-321-167-763-632-632-136-132-136-217-120-602-625-652-652-716-632-136-316-315-137-103-132-136-167-162-167-136-176-167-317-672-672-107-136-106-1_V.mp3)
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
> **Time**: 2026-04-04 03:57:28
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "매물 소화 흐름으로 볼 수 있습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "매물", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "소화", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "흐름으로", "start_ms": 270, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 420, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 470, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 510, "dur_ms": 66, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "오늘 공부한 종목들을 한 줄로 정리하면 이겁니다. 매물 소화 이후 시세 분출",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "공부한", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "종목들을", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 430, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "줄로", "start_ms": 470, "dur_ms": 106, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "어제 그냥 흐름으로만 봤을 때는 헤지 테마를 하신 분들이 많았을 것 같아요.",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 45, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 165, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "흐름으로만", "start_ms": 265, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봤을", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "때는", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "하라고 나오는 거예요. 그래서 너무 웃겨가지고 어떻게 내 삶을 이렇게 똑같이 넣었지?",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "하라고", "start_ms": 70, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나오는", "start_ms": 220, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거예요.", "start_ms": 370, "dur_ms": 206, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 6,
"transcript": "중국은 국가가 법을 개조 고쳐가지고 실용 엔지니어들을 양성하고 있습니다.",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "중국은", "start_ms": 50, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "국가가", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "법을", "start_ms": 270, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "개조", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "고쳐가지고", "start_ms": 490, "dur_ms": 86, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "저점은 전일 저점이자 3일 이동 평균선과 5일 이동 평균선의 중간이었습니다.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "저점은", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전일", "start_ms": 190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "저점이자", "start_ms": 310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "3일", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "잘 벌면 이제 천만 원 이상 최근 일주일 사이의 수익이 되게 괜찮은데",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "잘", "start_ms": 35, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "벌면", "start_ms": 115, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 235, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "천만", "start_ms": 335, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 455, "dur_ms": 40, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "말보다 주먹과 발이 먼저 나가는 현장용 요원입니다.",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "말보다", "start_ms": 55, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주먹과", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "발이", "start_ms": 295, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 375, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "나가는", "start_ms": 475, "dur_ms": 101, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "로봇이 어딘가에서 잘 움직이는 그런 상상을 머릿속으로 계속을 해요.",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "로봇이", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어딘가에서", "start_ms": 190, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 370, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "움직이는", "start_ms": 450, "dur_ms": 5274, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "관심 종목 글에서 신재생 에너지 현대 에너지 솔루션",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "관심", "start_ms": 30, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "종목", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "글에서", "start_ms": 240, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "신재생", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "에너지", "start_ms": 460, "dur_ms": 116, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "이곳은 현실적으로 한국에선 여기밖에 없습니다. 환경을 바꾸지 않으면 기준도 바뀌지 않습니다.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "이곳은", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "현실적으로", "start_ms": 145, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국에선", "start_ms": 295, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여기밖에", "start_ms": 415, "dur_ms": 161, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "리먼 사태 때도 내가 살아남았는데 그때랑 비슷하게 삼전 하이닉스가",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "리먼", "start_ms": 40, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "사태", "start_ms": 140, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "때도", "start_ms": 240, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "살아남았는데", "start_ms": 400, "dur_ms": 4856, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "단기간에 20% 이상 급락한 거는 난 기회라고 생각을 한 거지. 다만 트럼프가 언제 말할지 모르니까",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "단기간에", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "20%", "start_ms": 200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 300, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "급락한", "start_ms": 400, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 520, "dur_ms": 56, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "크레인을 이용해 45kg짜리 로봇을 직접 제작한 보행 실험용 대형 러닝 머신",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "크레인을", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이용해", "start_ms": 185, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "45kg짜리", "start_ms": 305, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "로봇을", "start_ms": 505, "dur_ms": 71, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "매물 소화 후 돌파하면서 급등을 하고 눌림 지지 이후에 강하게 상승하고 조정을 거치고 에너지 축적을 하고 즉 매물 소화를",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "매물", "start_ms": 45, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "소화", "start_ms": 145, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "후", "start_ms": 245, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "돌파하면서", "start_ms": 295, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "급등을", "start_ms": 495, "dur_ms": 81, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "강대국들 속에서 살아남아야 했다. 이런 상황 속에서 한국은 동맹과 의존을 혼동하지 않는 법을 배웠고 산업 파트너를 다양화하며 위기를 혁신의 동력",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "강대국들", "start_ms": 60, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "살아남아야", "start_ms": 410, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "했다.", "start_ms": 610, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 730, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "상황", "start_ms": 830, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 930, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한국은", "start_ms": 1030, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "동맹과", "start_ms": 1150, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "의존을", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "혼동하지", "start_ms": 1450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "않는", "start_ms": 1650, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "법을", "start_ms": 1750, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "배웠고", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "산업", "start_ms": 2000, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "파트너를", "start_ms": 2100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "다양화하며", "start_ms": 2300, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "위기를", "start_ms": 2550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "혁신의", "start_ms": 2700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "동력", "start_ms": 2850, "dur_ms": 10650, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "볼 수 있습니다. 1월 말에 장대양봉을 세우고 중기분봉 박스권으로 움직이다가 돌파하면서 급등을 하고 또 2월 중순에도 박스권으로",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "볼", "start_ms": 40, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있습니다.", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "1월", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "말에", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "장대양봉을", "start_ms": 550, "dur_ms": 26, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 03:57:28*
