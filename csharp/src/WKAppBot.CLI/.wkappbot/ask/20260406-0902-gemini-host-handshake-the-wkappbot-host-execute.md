# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=36536  time=09:00:08  uptime=11088s  mem=490MB  system_processes=413"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_085554.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 571-751-750-751 574-574-576-574-574-574-576-574-571-157-751-576-517-375-573-135-371-157 574-574-574-576-574-572-576-572-573-576-574-571-571-574-573-576-576 571-574-574-517-351-1_W.mp3)
  Segment 2: 1576ms ~ 5932ms (file: 132-312-236-312-312-321-312-236-213-321-271-123-312-210-123-312-123-123-213-321-230-213-231-321-216-120-231-126-123-213-326-312-132-123-132-132-312-132-312-326-321-316-312-213-1_V_stt_if.mp3)
  Segment 3: 6932ms ~ 7508ms (file: 301-623-261-326-321-312-103-732-327-137-576-701-571-167-560-136-326-317-136-136-312-312-321-123-132-137 172-527-126-705-725-276-125-672-132-621-503-235-570-571-216-750-527-257-7_W.mp3)
  Segment 4: 8508ms ~ 9084ms (file: 750-501-510-135-152-251-526-715-157-175-157-517-176-751-751-576-765-756-517-571-731-517-573_W.mp3)
  Segment 5: 10084ms ~ 10660ms (file: 571-576-571-573-571-576-756-573 574-574-571-571-517-175-751-571-571-751-571-357-571-571-571-571-570-571-573-571 574-574-574-574-574-576-576-571-571-571-571-573-751_W.mp3)
  Segment 6: 11660ms ~ 12236ms (file: 715-573 517-517-157-157-517-150-570-513-517-150-501-517-517 570-573-517-571-715-750-570-510-507-105-102-105-753-571-715-753-751-517 571-157-157-175-571-752-351-513-137-175-572-5_V.mp3)
  Segment 7: 13236ms ~ 13812ms (file: 736-175-136-623-132-123-132-132-312-372-172-317-726-765-762-752-267-672-735-765-367-725-723-357-521-356-675-351-357-315-517-572-163-136-126-123-372-723-657-752-216-351-257-253-1_W.mp3)
  Segment 8: 14812ms ~ 15388ms (file: 275-267-573-256-762-675-263-675-751-167-123-132-371-576-572-572-275-502-536-562-702-762-237-756-657-765-267-275-637-765-371-376-203-371-756-321-326-175-756-756-756-756-756-756-5_V.mp3)
  Segment 9: 16388ms ~ 22832ms (file: 130-751-372-273-275-237-236-267-267-267-276-751-106-310-230-372-716-163-312-753-267-672-627-602-602-263-672-261-162-325-137-167-625-267-267-326-236-231-617-106-621-326-267-267-3_W.mp3)
  Segment 10: 23832ms ~ 24408ms (file: 517-571-157-751-751-571-753-571-571-751-751-751-571-751-751-750-571-751-571 571-574-571-571-571-751-571-753-715-753-751-715-715-571-753-571-751-756-571-756-571-750-756-753-753 5_V.mp3)
  Segment 11: 25408ms ~ 25984ms (file: 751-576-571-756-751-571-576-573-571-756-571-576-573 570-576-571-576-751-756-570-570-571 507-510-510-751-571-571-571-517-751-517-751-753-571-751-753-751 574-573-576-570-573-571-7_W.mp3)
  Segment 12: 26984ms ~ 35804ms (file: 547-657-652-516-761-267-627-754-157-321-207-312-231-165-317-310-315-160-106-170-761-276-260-103-762-231-231-167-576-267-527-230-576-756-760-271-265-207-170-706-703-701-167-106-5_W.mp3)
  Segment 13: 36804ms ~ 37380ms (file: 765-357-675-361-673-567-675-763-765-367-736-367-765_W.mp3)
  Segment 14: 38380ms ~ 38956ms (file: 126-126-167-176-163-756-765-357-365-765-765-675-765-763-725-326-516-651-657-152-536-675-576-136-130-173-126-136-176-761-136-163-310-260-316-567-576-675-756-567-567-567-561-135_W.mp3)
  Segment 15: 39956ms ~ 52484ms (file: 750-752-756 132-413-276-206-207-310-136-721-126-126-210-123-321-123-213-231-675-267-213-574-524-103-320-321-261-261-123-253-302-213-310-207-267-123-130-302-263-657-326-123-576-2_W.mp3)
  Segment 16: 53484ms ~ 59640ms (file: 276-725-276-310-263-130-760-176-106-361-267-276-756-752-176-106-237-273-237-130-107-316-267-267-126-102-102-712-103-231-276-260-263-107-612-750-617-167-276-752-276-326-106-176-7_W.mp3)
  Segment 17: 60640ms ~ 68775ms (file: 321-201-701-207-310-301-130-231-320-302-201-201-763-756-752-370-756-275-572-102-321-103-310-201-752-270-572-257-207-320-750-267-201-321-261-123-763-573-201-213-103-310-321-132-3_V.mp3)
  Segment 18: 69775ms ~ 78019ms (file: 132-230-230-130-231-103-302-312-132-123-130-230-310-103-132-123-102-310-310-310-301-230-321-230-157-107-230-203-132-120-231-102-201-261-265-265-320-231-320-574-507-230-302-103-3_V.mp3)
  Segment 19: 79019ms ~ 79595ms (file: 571-371-157-571-571-571-750-175-105-107-107-170 105-507-576-105-715-517-753-107-157-173-175-326-213-613-753-673-316-137-735-130-165-136-135-132-136-132-175 657-576-163-572-612-3_W.mp3)
  Segment 20: 80595ms ~ 81171ms (file: 715-753-735-753-157-153-135-157-210-132-325-157-351-361-350-357_W.mp3)
  Segment 21: 82171ms ~ 82747ms (file: 752-635-567-602-276-261-716-325-275-357-267-270-216-261-103-375-751-275-275-725-623-625-375-213-736-173-326-275-257-236-267-623-267-126-216-720-312-237-576-762-527-275-267-126-5_V.mp3)
  Segment 22: 83747ms ~ 84323ms (file: 571-517-157-153-351 135-513-517-537-571-571-753-571-157-571-150-513-730-573-576-576-573 153-137-317-317-315-317-132-135-371-317-132 315-531-517-571-571-517-570-571-157-753-571-5_W.mp3)
  Segment 23: 85323ms ~ 85899ms (file: 253-705-512-627-270-572-326-576-126-623-312-315-132-312-270-273-725-215-273-537-752-572-547-762-706-215-562-267-317-324-723-357-326-523-321-135-163-576-316-163-237-237-237-236-7_V.mp3)
  Segment 24: 86899ms ~ 87475ms (file: 675-264-230 607 261-216-132-103-153-315-652-632-637-567-527-652-537-236-725-536-216-623-567 576-571-753-751-753-756-756-726-376-631-326-756-756-613-613-316-136-316-615-163-176-1_W.mp3)
  Segment 25: 88475ms ~ 89051ms (file: 157-150-153-135-175-157-175-175-175-715-132-130 130-102-102-102-103-152-157-152-157-157-175-175-517-157-157-175-715-135-175-137-175-157-157-157-152-132-103-132-162-163-162-136-1_V.mp3)
  Segment 26: 90051ms ~ 90627ms (file: 570-571-517-571-517-157-751-350-370-175-571-573-571-573-315-517-357 571-570-571-570-571-571-571-501-573 571-570-571-517-573-517-510-517-513-517 501-571-507-571-571-570-510-507-5_W.mp3)
  Segment 27: 91627ms ~ 92203ms (file: 567-173-570-156-713-526-263-756-657-756-267-316-672-267-261-576-213-576-235-574-756-576-652-756-261-675-625-763-576-572-756-567-635-521-731-756-625-237-257-276-750-173-130-137-5_V.mp3)
  Segment 28: 93203ms ~ 93779ms (file: 571-571-570-751-751 574-576-571-570-571-153-175-750-175-517-173-751-753-715-513-531-571-571-571-571 574-517-571-571-571-571-751-570-517-751-513-157-537-750-517-571-571-751 576-5_V.mp3)
  Segment 29: 94779ms ~ 103743ms (file: 276-176-267-267-756-267-627-236-672-716-170-267-736-753-732-107-716-106-317-371-267-126-705-167-706-231-170-120-231-263-276-230-310-261-260-210-267-703-217-267-627-310-762-716-7_W.mp3)
  Segment 30: 104743ms ~ 105319ms (file: 567-130-137-301-357-531-576-305-136-267-310-357-560-570-573-320-307-356-506-651-105-302-312-312-321-605-165-130-302-305-513-567-573-321-312-216-537-301 173-312-653-610-310-316-3_V.mp3)
  Segment 31: 106319ms ~ 111431ms (file: 216-170-627-216-276-276-267-321-312-321-106-761-627-537-357-267-276-237-623-267-206-276-705-756-276-276-276-162-102-276-106-267-260-260-270-320-276-250-165-672-235-207-120-213-5_W.mp3)
  Segment 32: 112431ms ~ 113007ms (file: 327-267-267-275-215-502-326-237-750-236-657-605-267-567-163-705-571-237-567-765-267-671-675-760-725-265-726-751-752-756-725-752-752-706-271-672-576-756-657 256-562-231-257-750-7_V.mp3)
  Segment 33: 114007ms ~ 114583ms (file: 510-507-570-570-571-571-571 571-571-517-573-537-570-537-517-517-517-531-517_W.mp3)
  Segment 34: 115583ms ~ 122675ms (file: 137-263-231-231-753-276-276-312-327-317-276-572-237-273-267-267-173-371-351-756-657-321-276-237-123-753-371-176-310-361-213-312-276-231-312-237-762-316-317-721-213-130-107-106-5_W.mp3)
  Segment 35: 123675ms ~ 124251ms (file: 567-576-267-326-207-236-576-165-267-207-276-765-752-126-125-753-327-570-327-312-672-762-602-527-725-256-265-576-256-310_V.mp3)
  Segment 36: 125251ms ~ 125827ms (file: 751-175-573-751-517-571-751-571-750-751-157-571-751-751-751-571 175-751-571-570-153-571-571-571-715-753-751-571-751-751-571-571-751-573-157-571-517-573-517-571-576-573-571-571 5_W.mp3)
  Segment 37: 126827ms ~ 127403ms (file: 675-762-763-763-765-765-765-762-716-765-763-657-756-765-567-567-756-657-756-675-650-675-756-675-765-765_W.mp3)
  Segment 38: 128403ms ~ 128979ms (file: 574-574-574-574-574-571-574-574-574-574-574-574-574-574-574 574-574-574-576-574-574-574-574-574-574-570-571-571-571-570-571-573-572-571 547-574-574-571-574-571-574-574-574-571-5_W.mp3)
  Segment 39: 129979ms ~ 137683ms (file: 120-327-312-213-261-275-301-236-267-236-132-750-123-173-271-273-130-176-137-230-135-230-527-302-312-312-357-307-327-312-567-534-761-607-726-137-375-761-306-164-576-231-230-203-3_W.mp3)
  Segment 40: 138683ms ~ 146531ms (file: 231-231-230-126-102-162-210-230-320-210-102-263-206-576-201-230-203-203-230-230-107-230-230-732-230-236-261-620-210-362-162-103-512-315-236-103-167-167-106-102-103-103-120-231-3_W.mp3)
  Segment 41: 147531ms ~ 148107ms (file: 517-157-150 576-573-572-517-157-157-510-150-175-175-107-130-751-375-756-752-723-715-756-753-157-317-135-175-362-213-367-316-367-367-376-613-673-675-576-756-735-617-153-317-316-1_W.mp3)
  Segment 42: 149107ms ~ 156235ms (file: 326-620-130-327-103-321-230-132-126-320-260-213-572-126-120-361-132-163-167-316-310-130-167-316-125-102-106-501-326-317-123-201-206-123-712-321-321-265-507-207-216-261-236-107-2_W.mp3)
  Segment 43: 157235ms ~ 163823ms (file: 213-236-302-230-312-307-317-301-203-275-657-375-236-570-530-172-127-357-276-206-216-357-306-310-301-301-215-210-210-123-312-132-123-137-230-321-160-107-160-106-163-123-120-105-1_W.mp3)
  Segment 44: 164823ms ~ 165399ms (file: 570-571-753 574-574-571-576-571-753-571-750-571-571-157-710-571-756-571-571-573 574-574-574-573-574-574-574-574-574-574-573-574 574-573-576-574_W.mp3)
  Segment 45: 166399ms ~ 174499ms (file: 206-120-201-201-123-102-317-175-231-206-132-576-750-627-756-213-621-570-520-201-201-207-276-260-267-210-207-273-725-217-752-612-627-231-237-130-312-620-260-620-627-620-173-267-2_W.mp3)
  Segment 46: 175499ms ~ 180971ms (file: 362-275-762-563-753-375-132-723-750-375-265-527-210-672-567-162-573-276-657-217-520-123-216-350-205-572-256-156-267-230-213-621-316-276-263-276-267-763-371-735-725-765-763-316-2_W.mp3)
  Segment 47: 181971ms ~ 182547ms (file: 537-517-513-135-153-315-137-135-315-135-135-315-312_W.mp3)
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
> **Time**: 2026-04-06 09:02:21
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "고맙고 미안하고 막 이러죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "고맙고", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미안하고", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이러죠", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "우리가 집을",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "우리가", "start_ms": 10, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "집을", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "지으면 정말 단열에 신경을 쓰고",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "지으면", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "단열에", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "신경을", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쓰고", "start_ms": 525, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "창문도 그렇게 큰 거 필요 없다 통창 이런 거 나는 필요 없다 그러면서 이제 하나하나 이렇게 그냥 언제",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "창문도", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 820, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "필요", "start_ms": 1220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없다", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "통창", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 2900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 3100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "필요", "start_ms": 3450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없다", "start_ms": 3850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그러면서", "start_ms": 4300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하나하나", "start_ms": 5300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 6100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "언제", "start_ms": 7200, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "말하는 매우 상징적이라고 할 수 있습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "말하는", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "상징적이라고", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 440, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 500, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "한국은 이미 글로벌 공급망과 외교 네트워크에서 매우 중요한 위치를 차지하고 있을 뿐만 아니라",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "한국은", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "글로벌", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공급망과", "start_ms": 1350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "외교", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "네트워크에서", "start_ms": 2550, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "위치를", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "차지하고", "start_ms": 4950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뿐만", "start_ms": 5950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "다양한 국가들과 협력 관계를 유지하고 있으며 방공망이 잇따라 뚫리자",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "다양한", "start_ms": 25, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "국가들과", "start_ms": 550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "협력", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "관계를", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "유지하고", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있으며", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방공망이", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "잇따라", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뚫리자", "start_ms": 4600, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "미국이 폴란드에 패트리어트 시스템 한 대와",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "미국이", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "폴란드에", "start_ms": 175, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "패트리어트", "start_ms": 335, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시스템", "start_ms": 445, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 535, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "대와", "start_ms": 565, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "패트리어트 발사용 미사일을 요청했지만 폴란드 영공을 방어",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "패트리어트", "start_ms": 30, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "발사용", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "미사일을", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "요청했지만", "start_ms": 1850, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "폴란드", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "영공을", "start_ms": 3250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방어", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "그렇다면 러셀 교수가 바라는 미래는 어떤 모습일까",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그렇다면", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "러셀", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "교수가", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "바라는", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "미래는", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 550, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "모습일까", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "It is very difficult to specify explicitly",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 70, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "difficult", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 330, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "specify", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "explicitly", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "덮게 될 것입니다 여러분들은 어떻게 생각하십니까",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "덮게", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것입니다", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여러분들은", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 505, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "생각하십니까", "start_ms": 565, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "공감하셨다면 소중한 언알바 후원 부탁드립니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "공감하셨다면", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "소중한", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "언알바", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "후원", "start_ms": 460, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "부탁드립니다", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "댓글의 고정된 내용을 통해 많은 사랑 부탁드리며 언알바는 오늘도 최소한",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "댓글의", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "고정된", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사랑", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "부탁드리며", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "언알바는", "start_ms": 3050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 3700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "최소한", "start_ms": 4150, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "아미 산토 이수바 아 베리 코리안 웨이",
"speaker": "speaker_5",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "아미", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "산토", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이수바", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 320, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "베리", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "코리안", "start_ms": 470, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "웨이", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "예스 댓츠 클래식 클래식 콤비네이션 예 와 역시 또 마음에 드시는 거는 찍으셔 와 굿 지저스 소 디스 갈비탕 이즈 낫 저스트 노멀 캄보 하이브리드",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Yes", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "classic", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "classic", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "combination", "start_ms": 1450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "yeah", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "역시", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 3450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마음에", "start_ms": 3750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드시는", "start_ms": 4250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "찍으셔", "start_ms": 5050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 5950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Jesus", "start_ms": 6300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 6850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 7100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "galbitang", "start_ms": 7450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 8000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 8250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 8550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "normal", "start_ms": 8900, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "치킨도 먹고",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "치킨도", "start_ms": 15, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 280, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "우와 너무 괜찮은데 이게 되게 큰 축제예요 그때 축제 기간 동안은 맥주가 단돈 천 원에서 전원 전원",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "우와", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "괜찮은데", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "1유로보다 적은 거예요 진짜로 회장입니다 저는 이렇게 지수가 크게 상승한 1, 2월보다 다행히도 이렇게 변동성이 심했던 3, 4월 시장에",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "1유로보다", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "적은", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진짜로", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "회장입니다", "start_ms": 1700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "대응할 수 있다 우려까지 나옵니다 아주 오래된 사드 같은 방공 요격 미사일 일부가 차출되는 정황이 포착되는 등 이란 전쟁으로 주한미군의 전략적 유연",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "대응할", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 560, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "우려까지", "start_ms": 780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나옵니다", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "진짜 무거워요 엄청 이만한데 이거 왜 이렇게 봐 끝장 아이 생선을 진짜 많이 줬어 아니 너무 오 야 와 이거 너무 감사하네 뻑뻑이 받으세요",
"speaker": "speaker_10",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "무거워요", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "회장이고 면회할 수 있는 시간도 정해져 있거든요 보호자가 또 옆에 있을 수가 없고 항상 딱 시간이 정해져 있고",
"speaker": "speaker_11",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "회장이고", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "면회할", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 305, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 365, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "그래서 이제 저 안에서 지금 언제 수술할지도 모르고 지금",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 210, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "방어 체계가 약화될 수 있다는 우려도 제기됩니다 현재 폴란드는 이미 우크라이나의 대규모 군사 지원을 제공한 상태에서 자국의 방공망에 공백이 생길 것을 우려하고 있습니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "방어", "start_ms": 25, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "체계가", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "폴란드 국방부 장관은 최근 인터뷰에서 패트리어트는 폴란드의 안보에 필수적인 자산이라며",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "폴란드", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "우크라이나에 대한 추가 지원은 신중하게 결정해야 한다고 강조했습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "우크라이나에", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "이러한 상황은 동유럽 국가들 사이에서도 복잡한 외교적 딜레마를 낳고 있습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이러한", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "한편 러시아는 서방 국가들의 미사일 지원에 대해 강력히 반발하고 있습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "한편", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "전문가들은 이번 조치가 전쟁의 양상을 어떻게 바꿀지 예의주시하고 있습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "전문가들은", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "결국 국제 사회의 협력이 어느 때보다 절실한 시점입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "결국", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "여러분은 이 상황에 대해 어떻게 생각하십니까",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "여러분은", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "댓글로 여러분의 의견을 남겨주세요 고맙습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "댓글로", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "다시 한번 강조하지만 이번 결정은 매우 중요한 의미를 가집니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "앞으로의 상황 변화에 따라 대응 전략을 수정해야 할 수도 있습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "앞으로의", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "지속적인 모니터링과 분석이 필요한 이유입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "지속적인", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "오늘 준비한 소식은 여기까지입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "시청해주셔서 감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "시청해주셔서", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "마지막으로 한 말씀 더 드리자면",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "마지막으로", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "구독과 좋아요는 큰 힘이 됩니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "구독과", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "다음 영상에서 뵙겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "감사합니다 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 09:02:21*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=28596  time=09:00:46  uptime=18528s  mem=441MB  system_processes=413"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260406_085622.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 756-751-750-751-570-751-751-751-753-751-571-751-751-756-750-750 750-170-703-701-705-765-750-715-750-715-705-751-750-751-715-705-751-750-705-715-105-750-715-751-750 765-570-570-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 157-150-153-135-175-157-175-175-175-715-132-130 130-102-102-102-103-152-157-152-157-157-175-175-517-157-157-175-715-135-175-137-175-157-157-157-152-132-103-132-162-163-162-136-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 750-750-752-537 751-751-751-715-751-751-753-576-537-175-137-175-375 576-752-573-571-710-105-705-573-705-703-170-170-123-175-175-170-175-753-375-752 571-570-570-752-570-517-753-5_V.mp3)
  Segment 4: 4728ms ~ 14376ms (file: 136-165-137-362-301-103-301-312-130-301-126-162-106-160-610-617-631-176-167-176-361-602-230-267-103-231-230-215-567-130-213-132-321-103-132-301-260-103-230-260-132-321-206-201-1_W.mp3)
  Segment 5: 15376ms ~ 15952ms (file: 103-103-107-130-135-170-107-175-130-715-517-150-715-157-105 120-132-312-132-126-120-120-102-107-107-123-120-102-715-107-107-107-701-175-107-710-175-751-705-507-751-715-132-126-1_V.mp3)
  Segment 6: 16952ms ~ 24548ms (file: 102-201-203-213-260-127-120-123-201-207-576-765-276-763-230-123-130-627-120-102-210-230-103-260-620-103-627-326-756-517-203-201-572-267-267-271-120-270-276-201-107-260-217-201-2_W.mp3)
  Segment 7: 25548ms ~ 31992ms (file: 267-230-627-123-203-231-237-320-206-621-260-263-316-321-623-765-321-572-627-267-620-625-126-263-326-260-572-267-253-216-263-126-263-106-621-231-263-265-236-523-362-236-256-236-2_W.mp3)
  Segment 8: 32992ms ~ 33568ms (file: 751-136-132-135-125-157-135-152-527-517-156-156-152-152-152-125-217-127-517-157-517-157-563-170-175-156-156-157-156-156-152-517-517-157-756-157-157-615-157-571-726-561-615-256-5_W.mp3)
  Segment 9: 34568ms ~ 42200ms (file: 104-104-103-103-103-103-210-302-126-130-531-270-206-260-326-350-541-150-301-321-201-230-276-263-260-173-137-312-574-547-573-320-203-320-320-106-137-501-570-501-517-321-570-106-1_V.mp3)
  Segment 10: 43200ms ~ 43776ms (file: 105-105-103-105-153-105-135-103-105-157-610-160-317-176-175-123-216-132-312-162-163-617-617-621-625-615-627-256-217-762-756-715-576-756-176-152-317-316-136-312-136-163-163-165-1_W.mp3)
  Segment 11: 44776ms ~ 45352ms (file: 573-275-265-637-175-326-657-627-526-267-672-576-326-526-576-216-675-236-527-275-376-275-672-352 157-571 627_W.mp3)
  Segment 12: 46352ms ~ 46928ms (file: 150-157-105-150-157 513-152-157-516-157-150-510-150-507-571-517-157-105-105-105-107-105-175-107-105-150-130-170-671-276-726-726-571-126 571-157-107-701-751-175-167-167-371-512-3_W.mp3)
  Segment 13: 47928ms ~ 48504ms (file: 175-573-157-751-175-570-715-157-751-751-571-751-751-570-573-571-571-571 571-570-570-750-570-753-570-571-753-751-175-756-570-571-571-571-576-571-753-571-571-570-571-571-750-570-5_V.mp3)
  Segment 14: 49504ms ~ 50080ms (file: 157-157-150-157-135-157-175-157-157-153-123-156-157-157-156 105-105-157-105-150-176_W.mp3)
  Segment 15: 51080ms ~ 55436ms (file: 132-312-236-312-312-321-312-236-213-321-271-123-312-210-123-312-123-123-213-321-230-213-231-321-216-120-231-126-123-213-326-312-132-123-132-132-312-132-312-326-321-316-312-213-1_V_stt_if.mp3)
  Segment 16: 56436ms ~ 57012ms (file: 317-631-623-123-132-307-760-763-675-761-735_W.mp3)
  Segment 17: 58012ms ~ 66976ms (file: 276-176-267-267-756-267-627-236-672-716-170-267-736-753-732-107-716-106-317-371-267-126-705-167-706-231-170-120-231-263-276-230-310-261-260-210-267-703-217-267-627-310-762-716-7_W.mp3)
  Segment 18: 67976ms ~ 68552ms (file: 570-571-570-571-571-571-571-517-571-571-571-571-517-517-571-517 157-157-517-157-157-153-513-157-153-157-153-513-537 513-517-517-517-571-517-517-517-531-517-517-571-571-572-573-5_W.mp3)
  Segment 19: 69552ms ~ 70128ms (file: 571-517-750-157-157-751-135-175-713-173-175-752-701-107-571-715-705-170-751-705-750-137-105-137-153-157-527-157-126-126-157-135-715-751-571-576-571-570-576-751-517-576-756-571_W.mp3)
  Segment 20: 71128ms ~ 77212ms (file: 320-130-130-321-312-536-310-102-130-103-103-103-126-127-210-312-231-237-276-267-273-310-132-267-623-260-627-263-236-236-236-573-352-167-132-237-237-136-175-261-276-672-762-230-3_W.mp3)
  Segment 21: 78212ms ~ 86060ms (file: 103-102-136-567-263-312-103-765-756-107-763-750-735-716-107-317-753-157-162-756-150-127-102-721-572-572-752-752-120-132-105-362-375-312-371-132-713-725-751-571-123-537-517-132-1_W.mp3)
  Segment 22: 87060ms ~ 95124ms (file: 210-216-754-751-205-201-162-162-176-176-103-321-230-203-320-320-675-230-302-130-316-130-103-106-375-103-136-751-457-317-321-103-132-137-173-136-170-173-310-126-162-621-627-623-1_V.mp3)
  Segment 23: 96124ms ~ 96700ms (file: 571-571-570-751-751 574-576-571-570-571-153-175-750-175-517-173-751-753-715-513-531-571-571-571-571 574-517-571-571-571-571-751-570-517-751-513-157-537-750-517-571-571-751 576-5_V.mp3)
  Segment 24: 97700ms ~ 98276ms (file: 571-517-571-751-571-573-571-570-576 574-571-571-571-571-157-517-157-157-571-715-517-173-517-751-751-753-571-570-571-571-750-750-751 574-517-517-517-571-571-571-573-570-571-570-5_W.mp3)
  Segment 25: 99276ms ~ 105432ms (file: 276-725-276-310-263-130-760-176-106-361-267-276-756-752-176-106-237-273-237-130-107-316-267-267-126-102-102-712-103-231-276-260-263-107-612-750-617-167-276-752-276-326-106-176-7_W.mp3)
  Segment 26: 106432ms ~ 107008ms (file: 312-321-326-312-123-302-317-210-173-361-267-172-731-167-107-317-127-317-176-130-360-261-123-102-713-627-132_W.mp3)
  Segment 27: 108008ms ~ 108584ms (file: 752-172-263-570-316-137-762-261-723-127-756-576-521-762-213-276-263-216-570-263-327-576-657-261-506_V.mp3)
  Segment 28: 109584ms ~ 110160ms (file: 517-573-135-375-735-735-753-751-751-735-753-753-753-537 507-507 517-571-153-573-571-571-571-517-573-513-573-531-573-570-571-752-571-753-537 571-570-570-570-571-571-517-573-571-7_W.mp3)
  Segment 29: 111160ms ~ 111736ms (file: 306-750-753-756-715-753-370-730 573-756-765-763-765-675-671-756-761_W.mp3)
  Segment 30: 112736ms ~ 113312ms (file: 571 574-574-574-571-571-571-573-571 510-571-750-751-152-537-751-571-751-751-571-517-750-571-570-570-751-756-735 507-571-753-175-175-576-571-715-571-571-517-173-715-175-157-751-5_V.mp3)
  Segment 31: 114312ms ~ 114888ms (file: 175-123-751-271-715-715-751-236-652-137-126-735-763-725-726-276-267-165-627-326-127-276-236-261-365-365-257-256-627-753-321-763-672-126-136-163-163-136-167-317-173-162-132-137-1_W.mp3)
  Segment 32: 115888ms ~ 116464ms (file: 136-136-725 126-213-316-162-512-163-132-136-132-132_W.mp3)
  Segment 33: 117464ms ~ 118040ms (file: 547-675-156-102-253-312-315-352-257-132-310-320-103-103-630-362-160-132-173-163-106-265-167-176-361-163-631-613-613-367-673-576-576-672-672-657-756-263-657-672-623-672-627-132-3_W.mp3)
  Segment 34: 119040ms ~ 119616ms (file: 571-501-157-571-750-750-537-715-157-751-571-517-751-756-751-751-571-571-751 571-571-573-715-570-751-517-715-751-571-715-751-751-753-570-753 571-571-576-572-571-751-751-751-756-5_W.mp3)
  Segment 35: 120616ms ~ 127708ms (file: 137-263-231-231-753-276-276-312-327-317-276-572-237-273-267-267-173-371-351-756-657-321-276-237-123-753-371-176-310-361-213-312-276-231-312-237-762-316-317-721-213-130-107-106-5_W.mp3)
  Segment 36: 128708ms ~ 129284ms (file: 157-517-576-516-517-516-156-531-753-756-715-571-517-571 750-750-753_W.mp3)
  Segment 37: 130284ms ~ 130860ms (file: 253-705-512-627-270-572-326-576-126-623-312-315-132-312-270-273-725-215-273-537-752-572-547-762-706-215-562-267-317-324-723-357-326-523-321-135-163-576-316-163-237-237-237-236-7_V.mp3)
  Segment 38: 131860ms ~ 132436ms (file: 571-751-157-157-751-751-517 570-571-531-571-157-571-157-571-573-517-157-571-571-751 570-570-517-517-751-175-751-175-157-570-571-571-571 570-517-751-157-153-175-573-157-571-571-7_W.mp3)
  Segment 39: 133436ms ~ 134012ms (file: 613-253-132-213-103-130-132-123-312-123-132-231-173-136-256-576-326-132_W.mp3)
  Segment 40: 135012ms ~ 143940ms (file: 176-672-157-671-103-570-301-216-206-125-764-162-761-237-321-105-103-367-257-210-327-213-132-310-321-302-312-316-102-103-310-312-301-713-672-132-312-362-376-573-316-361-160-231-1_W.mp3)
  Segment 41: 144940ms ~ 152248ms (file: 321-132-326-173-164-326-320-320-326-320-735-237-127-201-237-230-670-652-321-310-130-213-725-752-753-752-610-162-570-130-132-275-273-270-236-236-576-327-237-273-123-102-102-123-1_W.mp3)
  Segment 42: 153248ms ~ 153824ms (file: 715-371 517-571-571-572-572-571-571-571-571-751-173-517-751-753-751-752 572-571-157-137-175-321-753-715-716-175-752-317 576-571-571-517-175-172-751-517-751-751-571-571-751 571-5_W.mp3)
  Segment 43: 154824ms ~ 155400ms (file: 316-312-312-325-732-372-371-317-317-301-306-307-136-132-231-321-362-361-312-301-163-136-157-316-630-306-605-652-302-165-301-365-301-163-307 725-752-725-712-271-137-752-752 752-7_V.mp3)
  Segment 44: 156400ms ~ 156976ms (file: 751-375-175-537 572-571-571-573-571-371-102-705-735-571-573-731-570-751-375-751-351-375-753-753-173-371-357 571-501-573-517-573-571-571-570-753-751-753-571-571-571-571-571-537-7_W.mp3)
  Segment 45: 157976ms ~ 158552ms (file: 627-207-231-261-237-263-162-725-637-625-571-376-375-705-627-576-612-750-257-321-236-675-612-203-257-570-261-257-623-672-216-570-361-625-132-325-236-713-765-517-267-507-261-265-2_V.mp3)
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
> **Time**: 2026-04-06 09:02:21
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "고맙고 미안하고 막 이러죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "고맙고", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "미안하고", "start_ms": 180, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 390, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "이러죠", "start_ms": 480, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "우리가 집을",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "우리가", "start_ms": 10, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "집을", "start_ms": 270, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "지으면 정말 단열에 신경을 쓰고",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "지으면", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "단열에", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "신경을", "start_ms": 415, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "쓰고", "start_ms": 525, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "창문도 그렇게 큰 거 필요 없다 통창 이런 거 나는 필요 없다 그러면서 이제 하나하나 이렇게 그냥 언제",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "창문도", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 820, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "필요", "start_ms": 1220, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "없다", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "통창", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 2550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 2900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 3100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "필요", "start_ms": 3450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없다", "start_ms": 3850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그러면서", "start_ms": 4300, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 4950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "하나하나", "start_ms": 5300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 6100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 6700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "언제", "start_ms": 7200, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "말하는 매우 상징적이라고 할 수 있습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "말하는", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 170, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "상징적이라고", "start_ms": 280, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 440, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 500, "dur_ms": 30, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "한국은 이미 글로벌 공급망과 외교 네트워크에서 매우 중요한 위치를 차지하고 있을 뿐만 아니라",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "한국은", "start_ms": 20, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "글로벌", "start_ms": 800, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "공급망과", "start_ms": 1350, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "외교", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "네트워크에서", "start_ms": 2550, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 3500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 3850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "위치를", "start_ms": 4400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "차지하고", "start_ms": 4950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "뿐만", "start_ms": 5950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 6400, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "다양한 국가들과 협력 관계를 유지하고 있으며 방공망이 잇따라 뚫리자",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "다양한", "start_ms": 25, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "국가들과", "start_ms": 550, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "협력", "start_ms": 1200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "관계를", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "유지하고", "start_ms": 2200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있으며", "start_ms": 2850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방공망이", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "잇따라", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뚫리자", "start_ms": 4600, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "미국이 폴란드에 패트리어트 시스템 한 대와",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "미국이", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "폴란드에", "start_ms": 175, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "패트리어트", "start_ms": 335, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "시스템", "start_ms": 445, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 535, "dur_ms": 20, "phase_lr_deg": 0.0},
{"word": "대와", "start_ms": 565, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "패트리어트 발사용 미사일을 요청했지만 폴란드 영공을 방어",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "패트리어트", "start_ms": 30, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "발사용", "start_ms": 650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "미사일을", "start_ms": 1200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "요청했지만", "start_ms": 1850, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "폴란드", "start_ms": 2700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "영공을", "start_ms": 3250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "방어", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "그렇다면 러셀 교수가 바라는 미래는 어떤 모습일까",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "그렇다면", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "러셀", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "교수가", "start_ms": 290, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "바라는", "start_ms": 380, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "미래는", "start_ms": 490, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 550, "dur_ms": 10, "phase_lr_deg": 0.0},
{"word": "모습일까", "start_ms": 570, "dur_ms": 5, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "It is very difficult to specify explicitly",
"speaker": "speaker_4",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "It", "start_ms": 10, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 70, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "very", "start_ms": 130, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "difficult", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 330, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "specify", "start_ms": 390, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "explicitly", "start_ms": 500, "dur_ms": 70, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "덮게 될 것입니다 여러분들은 어떻게 생각하십니까",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "덮게", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 175, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "것입니다", "start_ms": 285, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여러분들은", "start_ms": 415, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 505, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "생각하십니까", "start_ms": 565, "dur_ms": 10, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "공감하셨다면 소중한 언알바 후원 부탁드립니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "공감하셨다면", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "소중한", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "언알바", "start_ms": 350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "후원", "start_ms": 460, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "부탁드립니다", "start_ms": 540, "dur_ms": 30, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "댓글의 고정된 내용을 통해 많은 사랑 부탁드리며 언알바는 오늘도 최소한",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "댓글의", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "고정된", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "통해", "start_ms": 1350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 1700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "사랑", "start_ms": 2050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "부탁드리며", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "언알바는", "start_ms": 3050, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "오늘도", "start_ms": 3700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "최소한", "start_ms": 4150, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "아미 산토 이수바 아 베리 코리안 웨이",
"speaker": "speaker_5",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "아미", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "산토", "start_ms": 100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이수바", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 320, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "베리", "start_ms": 380, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "코리안", "start_ms": 470, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "웨이", "start_ms": 550, "dur_ms": 20, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "예스 댓츠 클래식 클래식 콤비네이션 예 와 역시 또 마음에 드시는 거는 찍으셔 와 굿 지저스 소 디스 갈비탕 이즈 낫 저스트 노멀 캄보 하이브리드",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "Yes", "start_ms": 40, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "classic", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "classic", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "combination", "start_ms": 1450, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "yeah", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "역시", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 3450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "마음에", "start_ms": 3750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "드시는", "start_ms": 4250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거는", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "찍으셔", "start_ms": 5050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 5600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "good", "start_ms": 5950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Jesus", "start_ms": 6300, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 6850, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 7100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "galbitang", "start_ms": 7450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 8000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "not", "start_ms": 8250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "just", "start_ms": 8550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "normal", "start_ms": 8900, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "치킨도 먹고",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "치킨도", "start_ms": 15, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "먹고", "start_ms": 280, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "우와 너무 괜찮은데 이게 되게 큰 축제예요 그때 축제 기간 동안은 맥주가 단돈 천 원에서 전원 전원",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "우와", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "괜찮은데", "start_ms": 290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "1유로보다 적은 거예요 진짜로 회장입니다 저는 이렇게 지수가 크게 상승한 1, 2월보다 다행히도 이렇게 변동성이 심했던 3, 4월 시장에",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "1유로보다", "start_ms": 30, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "적은", "start_ms": 550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진짜로", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "회장입니다", "start_ms": 1700, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "대응할 수 있다 우려까지 나옵니다 아주 오래된 사드 같은 방공 요격 미사일 일부가 차출되는 정황이 포착되는 등 이란 전쟁으로 주한미군의 전략적 유연",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "대응할", "start_ms": 30, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 560, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "우려까지", "start_ms": 780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나옵니다", "start_ms": 1200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "진짜 무거워요 엄청 이만한데 이거 왜 이렇게 봐 끝장 아이 생선을 진짜 많이 줬어 아니 너무 오 야 와 이거 너무 감사하네 뻑뻑이 받으세요",
"speaker": "speaker_10",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "무거워요", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 800, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "회장이고 면회할 수 있는 시간도 정해져 있거든요 보호자가 또 옆에 있을 수가 없고 항상 딱 시간이 정해져 있고",
"speaker": "speaker_11",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "회장이고", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "면회할", "start_ms": 175, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 305, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 365, "dur_ms": 80, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "그래서 이제 저 안에서 지금 언제 수술할지도 모르고 지금",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 120, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "저", "start_ms": 210, "dur_ms": 50, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "방어 체계가 약화될 수 있다는 우려도 제기됩니다 현재 폴란드는 이미 우크라이나의 대규모 군사 지원을 제공한 상태에서 자국의 방공망에 공백이 생길 것을 우려하고 있습니다",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "방어", "start_ms": 25, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "체계가", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "폴란드 국방부 장관은 최근 인터뷰에서 패트리어트는 폴란드의 안보에 필수적인 자산이라며",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "폴란드", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "우크라이나에 대한 추가 지원은 신중하게 결정해야 한다고 강조했습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "우크라이나에", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "이러한 상황은 동유럽 국가들 사이에서도 복잡한 외교적 딜레마를 낳고 있습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "이러한", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "한편 러시아는 서방 국가들의 미사일 지원에 대해 강력히 반발하고 있습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "한편", "start_ms": 25, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "전문가들은 이번 조치가 전쟁의 양상을 어떻게 바꿀지 예의주시하고 있습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "전문가들은", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "결국 국제 사회의 협력이 어느 때보다 절실한 시점입니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "결국", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "여러분은 이 상황에 대해 어떻게 생각하십니까",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "여러분은", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "댓글로 여러분의 의견을 남겨주세요 고맙습니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "댓글로", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
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
"transcript": "다시 한번 강조하지만 이번 결정은 매우 중요한 의미를 가집니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 15, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "앞으로의 상황 변화에 따라 대응 전략을 수정해야 할 수도 있습니다",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "앞으로의", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "지속적인 모니터링과 분석이 필요한 이유입니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "지속적인", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "오늘 준비한 소식은 여기까지입니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "시청해주셔서 감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "시청해주셔서", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "마지막으로 한 말씀 더 드리자면",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "마지막으로", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "구독과 좋아요는 큰 힘이 됩니다",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "구독과", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "다음 영상에서 뵙겠습니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 10, "dur_ms": 100, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "감사합니다 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "감사합니다", "start_ms": 15, "dur_ms": 150, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-06 09:02:21*
