# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10512  time=04:14:26  uptime=10199s  mem=600MB  system_processes=362"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260404_041340.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 236-123-326-672-617-132-312-312-326-236-253-325-256-602-326-265-265-261-267-265-254-265-627-256-153-215-261-126-325-352-567-273-726-725-135-135-132-137-735-574-351-356-257-261-2_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 627-276-561-673-572 162 765-672-361-132-673-362-617-137-126-126-756-765-176-316-176-167-167-167-132-136-126-175-130-627-103-136-136-103-312-163-103-163-130-136-132-310-251-352-1_W.mp3)
  Segment 3: 3152ms ~ 5564ms (file: 321-132-326-120-162-120-651-103-546-540-547-362-312-130-301-136-103-130-156-157-613-651-576-136-103-137-103-671-610-567-567-125-675-713-763-326-132 541-176-762-723-725-723-372 6_W.mp3)
  Segment 4: 6564ms ~ 7140ms (file: 672-620-627-625-260-265-265-152-267-162-301-317-537-310-607-326-356-213-263-261-236-321-326-316-312-507-517-143-627-627-267-260-625-547-570-567-576-157-102-103-713 631-136-132-3_V.mp3)
  Segment 5: 8140ms ~ 8716ms (file: 256-562-561-136-156-652-125-231-625-627-627-620-265-135-312-752-752-756-371-137-516-576-576-275-215-257-573-135-367-275-275-236-576-756-657-756-571-256-265-265-236-231-126-512-5_W.mp3)
  Segment 6: 9716ms ~ 10292ms (file: 165-132-126-134-132-517-143-175_V.mp3)
  Segment 7: 11292ms ~ 11868ms (file: 176-123-657-617-106 132-103-103-104-103-136-167-174-167-165-163-132-123-132-125-167-164-104-102-103-143-132-104-123-106-167-162 132-163-132-136-132-163-132-143-132-126-132-123-1_V.mp3)
  Segment 8: 12868ms ~ 29824ms (file: 627-213-241-213-341 123-213-321-213-241-213-132-132-213 621-617-136-613-617-637-574-657-576-576-572-753-537-573-574-576-657-765-673-167-236-506-756-367-675-651-326-267-632-267 6_W.mp3)
  Segment 9: 30824ms ~ 31400ms (file: 321 126-132-231-362-326-312 762-267-672-167-627-763-162-621-163-613-321-136-615-165-231-103-130-613-132-163-316-103-136-167-130-613-130-103-130_W.mp3)
  Segment 10: 32400ms ~ 32976ms (file: 546-657-567-136-162-106-106-103-216-721-236-521-520-653-632-672-613-362-632-263-613-627-360-326-312-615-107-517-301-132-301-310-130-136-231-123-123-106-160-106-105-321-320-302-1_V.mp3)
  Segment 11: 33976ms ~ 34552ms (file: 130-760-765-765 760-756-765-765-761-762-756-756-576-756-765-765-765-657-576-576-675 560 267-263-623-632-627-627 213-617-570-670-765-761-763-367-673-675-673-637-136-637-672-675-6_W.mp3)
  Segment 12: 35552ms ~ 36128ms (file: 235-235-256-265-213-236-325-325-231-231-236-263-126-213-163-236-216-276-276-273-273-263-267-267-627-216-231-321-263-376-327-637-627-376-375-376-756-263-271-213-672-672-672-673-6_W.mp3)
  Segment 13: 37128ms ~ 37704ms (file: 627-657-675-657-657-675-652-657-657-672-627-621 625-657-657-675-625-657-675-765-652-627-265-123-267-627-627-675-627-672-672-613-672-672-762-762-672-762-617-762_W.mp3)
  Segment 14: 38704ms ~ 39280ms (file: 657-654-657-625-265-267-623-263-267-267-213-213-152-135-152-135-715-715-735-750-256-576-573-372-756-765-306-520-653-637-367-326-236-273-326-362-236-326-632-673-673-673-637-610-5_W.mp3)
  Segment 15: 40280ms ~ 40856ms (file: 564-560-326-561-134-361-103 162-167 106-136-163-106-163-156-106-617-163-163-103-156-301-501-621-672-653-567-316-361-316-267-617-672-327-327-316 263-263-236-625-625-165-102-123-5_V.mp3)
  Segment 16: 41856ms ~ 42432ms (file: 315-235-231-213-217-231-524-256-540-265-265-356-312-321-256-213-236-267-256-574-517-135-215-213-275-312-326-231-504-540-504-512-532-531-513-534-514-103-105-237-213-312-103-103-1_W.mp3)
  Segment 17: 43432ms ~ 44008ms (file: 675-675-675-675-657 576-576-567-576-567-567-576-576-576-576-576-576-576 567-576-576-576-576-576-567-567-756-576-576-576-756-567 576-567-576-576-576-576-576-576-576-576-576-576-5_W.mp3)
  Segment 18: 45008ms ~ 45584ms (file: 567-571-312-573-574-574-312-130-365-576-265-567-754-657-652-267-265-732-527-620-267-273-327-736-762-756-375-576-567-653-307-375-756-765-576-675-564-163-107-631-157-157-175-756-7_W.mp3)
  Segment 19: 46584ms ~ 47160ms (file: 103-126-132-360-316-361-756-574-576-576-756-267-267-267-267-623-310-203-231-620-602-261-310-136_W.mp3)
  Segment 20: 48160ms ~ 48736ms (file: 620-627-762-672-726-576-572-752-623 657-635-675-657-675-657-675-637-675-673-657-657-657-657-657-675-631-136-103-165-263-612-136-627-267-206-612 765-765-654-672-672-157-132-167-6_W.mp3)
  Segment 21: 49736ms ~ 50312ms (file: 607-607-607-607-675-763-576-567-136-167 627-670-657-675-136-136-321-321-321-326-231-712-763-276-203-231-263-675-657-307-123-132-301-302-325-312-320-276-327-203-203-206-206-302-7_W.mp3)
  Segment 22: 51312ms ~ 51888ms (file: 650-657-650-652-650-620-607-627-620-605-675-627-602-605-605-605-657-650-605-657-605-650-652-650-670-607-627-675-672-670-672-627 605-602-605-605-605-605-650-605-650-650-657-650-6_W.mp3)
  Segment 23: 52888ms ~ 53464ms (file: 765-176-756-751-216-562-762-106-173-107-176-152-136-106-176-610-105-103-102-130-163-132-163-103-136-135-173-752-107-163-157-125-103-135-167-315-271-216 136-103-316-132-361-130-1_V.mp3)
  Segment 24: 54464ms ~ 60296ms (file: 571-215-567-516-206-260-267-206-206-210-126-627-263-623-263-267-216-261-206-605-265-126-103-230-206-230-203-261-173-132-572-720-261-625-132-263-265-263-623-625-236-260-260-261-2_W.mp3)
  Segment 25: 61296ms ~ 61872ms (file: 625-625-623-652-657-657-567-576-567-567-657-362-160-657-675-657-675-652-657-657-657-672-670-607-607-670-670-602-760-607-627-675-672-126-162-163-762-672-627-672-627-672-672-627-6_W.mp3)
  Segment 26: 62872ms ~ 63448ms (file: 123-135-153-521-135-152-153-135 135-132-152-153-153-102-123-153-123-312-153-135-152-135-125-152-135-125-105-135-153 103-137-135-135-125-132-103-125-125-132-123-132-125-157 132-3_V.mp3)
  Segment 27: 64448ms ~ 65024ms (file: 103-103-157-175-715-625-627-632-632-130-132-136-570 761-672-762-260-602-620-620-603-602-620-613-160-217-632-627-627-106-162-106-162-216-102-216-132-103-130-301-253-351-534-267-6_V.mp3)
  Segment 28: 66024ms ~ 66600ms (file: 172-176-127-176-132-175-137-672-267-173-163-316-173-756-652-602-276-670-206-123-127-126-761-765-756-675-715-176-103-106-167-105-105-610-136-102-301-130-136-120-261-126-621-210-1_W.mp3)
  Segment 29: 67600ms ~ 68176ms (file: 571-567-175-157-175-517-576-561-516-561-675-165-165-651-765-176-761-357-365-563-561-517-516-571-576-516-516-175-165-165-651-657-567-576-567-567-513-513-567-165-165-516-567-165-1_W.mp3)
  Segment 30: 69176ms ~ 69752ms (file: 263-136-106-103-632-163-316-130-136-103-567-610-615-675-623-136-130-361-173-316-602-617-167-173-167-316-137-216-165-130-103-365-106-136-361-103-132-103-136-156-103-261-132-316-1_V.mp3)
  Segment 31: 70752ms ~ 79824ms (file: 106-175-103-126-167-162-120-231-207-237-231-203-270-130-162-163-230-732-132-102-103-102-310-103-527-267-210-672-267-312-321-537-752-126-541-201-261-572-267-231-762-572-752-207-2_W.mp3)
  Segment 32: 80824ms ~ 81400ms (file: 167 670-607-623-102-103-210-201-130-160-610-206-206-103-210-260-260-206-765-761-765-167-273-267-213-106-321-210-201-657-613-567-657-106-310-762-370-376-327-671-276-271-175-107-1_W.mp3)
  Segment 33: 82400ms ~ 82976ms (file: 321-612-316-326-312-675-752-271-657-726-671-612-621-162-236-263-163-162-160-316-312-231-261-106-123-136-326-162-216-621-612-632-216-627-236-570-572-562-627-631-136-610-316-236-6_W.mp3)
  Segment 34: 83976ms ~ 84552ms (file: 362-326-132-134-312-316-672-574-675-607 312-132-103-312-625-163-203-312-321-175-123-527-103-267-256-302-127-107-302-325-573-567-236-132-312-123-231-125-127-153-152-716-135-231-2_V.mp3)
  Segment 35: 85552ms ~ 86128ms (file: 106-163-316-106-236-132-132-612-632-162-625-265-521-672-261-136-621-632-613-675-106-136-160-167-137-136-613-631-172-176-106-765-102 765-761-167-213-561-163-621-167-163-106-165-1_V.mp3)
  Segment 36: 87128ms ~ 95300ms (file: 265-276-267-301-267-231-261-267-260-106-230-236-261-537-201-267-103-130-276-267-267-120-135-320-132-102-263-527-574-216-126-267-265-574-130-130-261-532-521-356-236-302-231-130-3_W.mp3)
  Segment 37: 96300ms ~ 96876ms (file: 312-132-261-621-362-136-135-135-632-632-657-632-675-675-130-261-162-261-627-632-132-103-136-715-765-675-675-627-627-267-267-762-627-726-726-672-670-620-567-576-675-167-637-623-6_W.mp3)
  Segment 38: 97876ms ~ 98452ms (file: 560-167-152-675-136-132-136-126-136-167-651-316 316-136-516-156-617-631-602-106-613-276-162-513-126-136-123-163 136-167-103-136-576-136-130-103-136-167-367-627-617-127-126-756-1_V.mp3)
  Segment 39: 99452ms ~ 100028ms (file: 576-576-576-576-576-576-576-576-627-627-267-275-106-132 576-567-652-567-572-567-672-765-675-657-657-521-715-176-576-162-217-761-167-756-672 576-657-675-657-672-675-672-652-567-5_W.mp3)
  Segment 40: 101028ms ~ 101604ms (file: 136-203-123-302-327-257-326-327-627-263-360-230-230-237-213-123-316-306-270-725-267-275-673-312-132-132-123-152-173-123-132-123-756-706-765-321-326-302-135-123-307-702-257-201-2_V.mp3)
  Segment 41: 102604ms ~ 103180ms (file: 320-312-302-723-315-173-162-167-136-103-136-617-163-103-610-301-216-132-612-261-165-135-102-123-106-603-206-261-213-120-231-236-160-125-216-260-321-126-365-510-675-617-605-657-5_W.mp3)
  Segment 42: 104180ms ~ 104756ms (file: 157-315-317-137-135-137-315-513-153-135-137-371-137-103 137-103-135-103-135-173-137-135-312-135-103-175-157-531-130-510-137-105-157-135-301-157-731-105-317-130-173-317-310 135-1_V.mp3)
  Segment 43: 105756ms ~ 106332ms (file: 632-163-623-632-130-130-130-130-130-675-563-561_V.mp3)
  Segment 44: 107332ms ~ 107908ms (file: 123-136-126-162-102-516-576-576-356-126-123-162-617-270-602-620-216-260-261-260-206-260-620-620-672-263-620-260-673-672-620-672-627-620-620-260-206-203-231-301-106-107-107-253-2_W.mp3)
  Segment 45: 108908ms ~ 109484ms (file: 512-316-136-673-103-163-510-514-103-236-173-107-576-126-156-130-103-103-653-671-206-130-360 361-312-756-163-167_V.mp3)
  Segment 46: 110484ms ~ 111060ms (file: 620-362-230-320-230-102-132-102-162-612-612-620-260-102-610-613-130-326-135-315-315-321-123-103-120-201-102-123-132-312-236-102-301-123-502-123-132-103-351-630-103-630-360-316-1_V.mp3)
  Segment 47: 112060ms ~ 112636ms (file: 531-541-547-541-540-542-540-504 251-321-756-576-567-576-672-762-672-765-576-573-573-573-756-576-567-576-756-576-576-231-126-215-267-657-521-275-576-527-321-673-672-763-675-675-7_W.mp3)
  Segment 48: 113636ms ~ 114212ms (file: 123-130-132-132-627-627-670-106-136-160-106-670-103-136-152-106-132-317-103-106-173-132-516-167-615-103-103-167-613-136-613-136-103-102-103-710-167-103-104-132-152-615-312-130-6_V.mp3)
  Segment 49: 115212ms ~ 115788ms (file: 350-310-312-123-716-103-106-162-163-163-103-203-320-123-126-213-103-236-261-527-576-576_V.mp3)
  Segment 50: 116788ms ~ 117364ms (file: 165-152-132-137-157-127-136-136-613-510-105-103-163-103-136-167-517-176-106-136-136-123-136-130-106-765-103-167-136-156-235-613-176-312-103-613-103-765-163-163-176-165-160-136-1_V.mp3)
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
> **Time**: 2026-04-04 04:15:33
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 2,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 3,
"transcript": "국무총리 땡땡 명 장관 땡땡 명 국회의원 땡땡 명 사법고시 몇 명 합격 행정고시 몇 명 합격",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "국무총리", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "땡땡", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명", "start_ms": 660, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "장관", "start_ms": 750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "땡땡", "start_ms": 1060, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명", "start_ms": 1220, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "국회의원", "start_ms": 1310, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "땡땡", "start_ms": 1720, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "명", "start_ms": 1880, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "사법고시", "start_ms": 1970, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 2400, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "명", "start_ms": 2510, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "합격", "start_ms": 2620, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "행정고시", "start_ms": 2930, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 3390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "명", "start_ms": 3500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "합격", "start_ms": 3610, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "이걸 오백 편 정도의 논문이 발표가 되는데 그중에서 이제 아홉 편만에 선정이 되는 겁니다",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "이걸", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "오백", "start_ms": 160, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "편", "start_ms": 320, "dur_ms": 80, "phase_lr_deg": -5.0},
{"word": "정도애", "start_ms": 410, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "논문이", "start_ms": 560, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "발표가", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "되는데", "start_ms": 960, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "그중에서", "start_ms": 1120, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "이제", "start_ms": 1380, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "아홉", "start_ms": 1510, "dur_ms": 140, "phase_lr_deg": -5.0},
{"word": "편만에", "start_ms": 1660, "dur_ms": 180, "phase_lr_deg": -5.0},
{"word": "선정이", "start_ms": 1850, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "되는", "start_ms": 2010, "dur_ms": 120, "phase_lr_deg": -5.0},
{"word": "겁니다", "start_ms": 2140, "dur_ms": 150, "phase_lr_deg": -5.0}
]
},
{
"segment": 5,
"transcript": "와",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "와", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 10.0}
]
},
{
"segment": 6,
"transcript": "사실 박혜원 교수의 로봇 사랑은 지도 교수의 한마디로 시작됐습니다",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "사실", "start_ms": 50, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "박혜원", "start_ms": 350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "교수의", "start_ms": 680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 990, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "사랑은", "start_ms": 1280, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지도", "start_ms": 1650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "교수의", "start_ms": 1910, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "한마디로", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시작됐습니다", "start_ms": 2610, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "사건이 맞물리면서 긴장감이 극대화되죠 영화의 스토리 구조 자체는 비교적 정통파였습니다 이중 스파이 배신 협력 희생 등 장르 팬이라면 익숙하게 느껴져",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "사건이", "start_ms": 45, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "맞물리면서", "start_ms": 410, "dur_ms": 550, "phase_lr_deg": 5.0},
{"word": "긴장감이", "start_ms": 970, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "극대화되죠", "start_ms": 1480, "dur_ms": 650, "phase_lr_deg": 5.0},
{"word": "영화의", "start_ms": 2150, "dur_ms": 400, "phase_lr_deg": 5.0},
{"word": "스토리", "start_ms": 2560, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "구조", "start_ms": 3020, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "자체는", "start_ms": 3380, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "비교적", "start_ms": 3840, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "정통파였습니다", "start_ms": 4300, "dur_ms": 800, "phase_lr_deg": 5.0},
{"word": "이중", "start_ms": 5150, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "스파이", "start_ms": 5510, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "배신", "start_ms": 5970, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "협력", "start_ms": 6330, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "희생", "start_ms": 6690, "dur_ms": 350, "phase_lr_deg": 5.0},
{"word": "등", "start_ms": 7050, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "장르", "start_ms": 7210, "dur_ms": 300, "phase_lr_deg": 5.0},
{"word": "팬이라면", "start_ms": 7520, "dur_ms": 500, "phase_lr_deg": 5.0},
{"word": "익숙하게", "start_ms": 8030, "dur_ms": 450, "phase_lr_deg": 5.0},
{"word": "느껴져", "start_ms": 8490, "dur_ms": 350, "phase_lr_deg": 5.0}
]
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 10,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 13,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 14,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 17,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 18,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 21,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 24,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 25,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 28,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 29,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 32,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 33,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 36,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 37,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 40,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 43,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 46,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 49,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 50,
"transcript": "(noise)",
"speaker": null,
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-04 04:15:33*
