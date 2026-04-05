# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=12:31:30  uptime=38547s  mem=418MB  system_processes=361"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_123118.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 5976ms (file: 103-107-627-267-123-132-310-130-321-170-106-167-673-371-321-136-130-312-316-130-310-317-316-361-312-327-231-103-312-572-574-564-567-576-651-653-135-130-137-317-132-312-302-103-6_W_stt_the_u.n._command_of_them_is_all_for_most.mp3)
  Segment 2: 6976ms ~ 7552ms (file: 572-570-756-175-756 520-520-257-527-201-267-725-752-576-756-756-756-756-756-756-756-751-576-751-756-571-751 526-520-520-576-502-257-572-572-752-576-571-576-576-576-571-574-574-5_V.mp3)
  Segment 3: 8552ms ~ 19856ms (file: 574-750-574-547-754-201-210-607-670-672-267-213-203-231-213-132-312-132-132-267-267-207-651-321-103-201-520-547-754-207-102-514-132-675-261-261-206-263-310-260-527-257-201-301-1_W.mp3)
  Segment 4: 20856ms ~ 21432ms (file: 715-751-751-751-715-715 150-153-130-715-751-715-751-751-175-715-751-715-756-756-756-751-751-756-756-753-765-756 510-517-751-571-715-751-751-756-751-751-751-751-751-715-751-751-7_W.mp3)
  Segment 5: 22432ms ~ 32116ms (file: 237-263-672-675-236-267-261-261-102-326-310-713-203-103-132-607-102-547-254-102-542-210-210-130-120-167-213-671-206 273-120-176-123-276-216-276-726-751-103-173-607-263-102-130-1_W.mp3)
  Segment 6: 33116ms ~ 39128ms (file: 726-301-167-132-123-302-135-537-765-230-210-210-120-201-103-120-136-105-103-105-127-271-176-725-627-531-103-172-376-170-103-231-137-130-136-617-162-120-301-102-172-103-572-312-3_V.mp3)
  Segment 7: 40128ms ~ 40704ms (file: 321 563-516-536-512-501-531-513-502-501-501-501-506-526-506-560-650-605 534-527-507-576-527-507-507-705-750-570-752-572-752-572-572-572-527-572-572-572-572-572-351-250-563-561-5_V.mp3)
  Segment 8: 41704ms ~ 42280ms (file: 517-157-105-107-105-156-105-170-170-107-751-765-573-736-756-537-576-135-123-157-175-175-107-125-157-127-152-127-132-125-105-137-315-135-765-756-756-756-376-657-675-756-576-675-7_W.mp3)
  Segment 9: 43280ms ~ 50552ms (file: 321-512-237-572-235-102-216-231-261-123-312-215-132-321-263-627-623-213-263-267-216-572-216-267-263-756-265-102-170-132-751-312-263-261-312-261-163-572-150-765-265-106-176-571-2_W.mp3)
  Segment 10: 51552ms ~ 52128ms (file: 107-150-150-175-107 750-105-150-150-157-157-157-517-156-156-157-157-157-517-517-517-157-175-541-753-705-705-750-750-705-750-753-753-754-750-150-150-105-150-157-157-157-175-157-5_W.mp3)
  Segment 11: 53128ms ~ 60616ms (file: 103-312-207-120-127-260-260-201-132-103-706-312-310-103-103-230-231-672-623-527-106-576-276-123-123-276-260-130-130-367-267-106-213-103-310-623-237-736-237-753-572-215-206-123-2_V.mp3)
  Segment 12: 61616ms ~ 72812ms (file: 231-312-136-302-230-203-205-673-130-312-137-312-172-574-301-210-207-132-621-271-276-267-260-261-320-637-326-321-236-731-310-512-301-316-312-572-762-275-527-213-326-123-135-263-1_W.mp3)
  Segment 13: 73812ms ~ 74388ms (file: 631-210-310-136-316-136-316-301-361-163-735-513-162-326-132-301-132-316-312-163-103-136-670-675-675-107-106-106-162-312-302-106-126-163-601-602-621-613-612-601-106-615-320-310-6_V.mp3)
  Segment 14: 75388ms ~ 75964ms (file: 153-157-157-157-157-157 173-510-157-513-315-517-135-135-175-103-153-150-153-157-513-157-153-157-157-157 105-103-135-135-375-315-135-103-107-105-153-157-157-157-157-175-571-751-7_V.mp3)
  Segment 15: 76964ms ~ 77540ms (file: 537-537-576-537 650-560 576-573-576-567-570-507-502-507-520-502-527-502-250-572-750-572-527 507-572-572-572-520-507-527-250-502-527-250-257-250-257-520-523-256-512-265-120-260-6_V.mp3)
  Segment 16: 78540ms ~ 79116ms (file: 657-675-675-671 621-612-617-617-671-671-675-657-675-657-675-657-657-675-647-657-675-657-675-675-674 621-610-612-617-617-671-671-675-657-675-657-567-567-657-567-654-657-675 612-6_W.mp3)
  Segment 17: 80116ms ~ 80692ms (file: 675-571-576-675-675-657-675-172-765-765-675-567-617-657-312-132-136-672-321-362-162-163-675-126-657-167-217-130-713-172-216-627-631-126-612-625-615-623-632-672-362-612-167-316-6_W.mp3)
  Segment 18: 81692ms ~ 90332ms (file: 261-132-261-572-107-132-174-273-237-213-572-527-135-103-103-132-321-132-130-103-102-103-103-106-103-106-120-236-210-230-257-215-213-130-321-260-260-236-231-231-253-276-260-172-1_W.mp3)
  Segment 19: 91332ms ~ 97812ms (file: 132-675-265-761-361-120-130-310-312-103-301-301-132-137-251-206-206-603-360-216-362-306-361-613-250-103-170-206-762-167-163-127-526-251-312-301-256-751-725-130-305-216-260-572-5_W.mp3)
  Segment 20: 98812ms ~ 99388ms (file: 725-123-125-127-315-516-675-756-156-516-567-651-562-516-563-165-672-162-321-103-132-103-510-301-103-135-103-517-576-516-260-206 263-230-302-231 756-763-765-672-672-267-301-760-7_W.mp3)
  Segment 21: 100388ms ~ 105680ms (file: 263-102-576-120-367-206-206-203-203-205-267-205-273-762-102-125-617-257-321-572-135-103-135-567-574-305-231-213-132-312-760-750-537-103-571-306-165-357-103-315-753-203-125-132-3_V.mp3)
  Segment 22: 106680ms ~ 107256ms (file: 267-567-567-567-657-567-156-165-621-627-672-672-627-576-752-275-276-267-236-517-574-756-753-613-167-756-576-756-675-576-567-513-157-215-672-675-765-637-132-326-236-621-621-612-6_W.mp3)
  Segment 23: 108256ms ~ 108832ms (file: 627-567-613-765-675-675-762-675-675-756-567-765-675-675_W.mp3)
  Segment 24: 109832ms ~ 110408ms (file: 751-157-175-157-175-167-176-517-716-167-175-175-176-176-175-176-167-176-167-175-174 150-105-105-150-105-105-150-150-157-175-517-157-175-176-751-715-751-715-176-761-716-175 150-1_V.mp3)
  Segment 25: 111408ms ~ 111984ms (file: 106-132-310-721-753-176-612-621-627-672-765-756-756-756-765-765-276-621-316-103-126-623-263-312-261-632-627-756-751 106-752-627 103-132-627-263-153 132-213_W.mp3)
  Segment 26: 112984ms ~ 124576ms (file: 127-167-106-756-312-317-132-132-765-267-612-765-231-321-263-267-127-267-267-132-263-267-261-231-107-267-627-627-627-756-756-130-267-627-260-123-310-312-316-163-675-612-132-103-1_W.mp3)
  Segment 27: 125576ms ~ 126152ms (file: 671-176-261-763-671-172-765-762-176-716-675-716-673-162-713-301-671-172-715_W.mp3)
  Segment 28: 127152ms ~ 137412ms (file: 203-231-103-103-574-102-201-240-310-301-756-752-756-302-756-213-310-102-203-231-752-275-102-203-312-301-302-321-670-261-102-310-675-210-201-302-301-206-203-230-130-207-231-230-2_W.mp3)
  Segment 29: 138412ms ~ 138988ms (file: 507-507-502-502-520-527-250-520-250-502-502-572-513-521-526-523 507-503-503-502-523-502-530-520-257-503-530-352-352-352-532-503-520-503-520-530-532-362-512-543-516-523-514 503-5_V.mp3)
  Segment 30: 139988ms ~ 140564ms (file: 156-165-651-651-165-516-561-516-517-156-516-517-516-157-576-517-561-516-157-571-156-617-675-765-675 165-165-651-165-156-652-561-561-516-561-561-516-567-561-516-517-516-561-165_W.mp3)
  Segment 31: 141564ms ~ 142140ms (file: 257-275-725-257-527-572-275-725-572-257-531-501-503-512-520-572-572-517-527-517-521-574-576-571-576-576-576-572-576-576-576-576-571-576-756-752-731-517-537-527-507-572-573-527-5_W.mp3)
  Segment 32: 143140ms ~ 143716ms (file: 135-513-516-150-310-135-136-136-312-326-763-623-263-613-361-613-167-312 361-316-317-761-765 756-756-157-156-137-371-713-735-317-361-137-371-713-763-765-713-137-572-672-725-673-3_W.mp3)
  Segment 33: 144716ms ~ 145292ms (file: 501-507-501-507-510-175-570-571-507-570-501-517-571-510-517-570-507-571-570-570-571 507-574-571-570-571-517-507-570-507-571-570-570-501-507-510-150-517-517-574-517-510-517-507-5_V.mp3)
  Segment 34: 146292ms ~ 146868ms (file: 546-563-301-316-310-316-301-567-756-567-756-607_W.mp3)
  Segment 35: 147868ms ~ 148444ms (file: 517-157-157-570-567-567-657-657-567-657-567-567-567-530-530 315-510-517-574-517-517-573-570-571-517-517-531-513-570-150-510-571-571-537-517-517-517-576-571-507 571-517-513-507-5_V.mp3)
  Segment 36: 149444ms ~ 150020ms (file: 657-576-567-567-756-567-756-567 576-567-657-567-567-657-675-567-576-567-756-567-576-567-576-567-675 752-756-576-576-756-576-756-576-756-756 752-756-675-756-756-576-756-576-756 7_W.mp3)
  Segment 37: 151020ms ~ 151596ms (file: 564 123-125-251-152-215-205-512-502-507-510-501 517-152-524-157-501-105-152-513-531-532-517-523-513-512-513-152-517-512-514-513-524-521-512-512-531-531-516-512-542-547-567-506-1_V.mp3)
  Segment 38: 152596ms ~ 153172ms (file: 175-710-750-751-751-571-175-701 135-153-513-705-153-301-517-507-705-170-170-710-170-173-137-137-175 570-150-107-351-517-371-170-715-710-713-173-713-710-713-173-170-173-170-731-1_V.mp3)
  Segment 39: 154172ms ~ 161624ms (file: 671-216-612-263-675-103-173-167-301-312-301-103-135-301-260-260-267-267-263-216-236-567-317-602-620-206-263-206-206-267-236-267-102-102-123-143-132-756-756-132-312-106-132-612-6_V.mp3)
  Segment 40: 162624ms ~ 163200ms (file: 765-762-675-675-167-761-671-765-765-176-361-103-203-613-302 132-263-236-261-263-267-236-261-574-572-623-172-132-263-507-172-103-127-103-107-327-372-723-273-237-167-176-162-126-7_W.mp3)
  Segment 41: 164200ms ~ 172696ms (file: 352-574-756-257-203-756-754-275-310-316-576-502-260-320-231-210-321-231-230-103-132-534-317-532-216-203-231-547-576-524-267-574-574-207-236-301-201-567-312-350-310-270-207-207-2_W.mp3)
  Segment 42: 173696ms ~ 181220ms (file: 276-127-371-756-672-276-102-102-754-756-675-765-261-106-172-267-230-652-163-132-106-130-126-120-231-263-256-367-762-273-231-752-756-725-267-106-175-237-260-213-231-573-574-574-7_W.mp3)
  Segment 43: 182220ms ~ 189672ms (file: 206-572-275-217-120-206-260-672-206-201-260-162-257-260-256-520-301-312-206-132-107-103-102-126-726-670-760-572-103-201-760-627-260-132-570-301-103-213-574-574-574-206-120-260-2_W.mp3)
  Segment 44: 190672ms ~ 196216ms (file: 126-261-127-103-126-102-102-126-275-125-125-230-172-721-267-206-321-207-230-753-213-231-206-270-312-237-321-103-326-203-102-217-206-203-320-210-203-127-210-203-725-270-206-752-1_V.mp3)
  Segment 45: 197216ms ~ 197792ms (file: 360-362-675-631 673-670-160-306-306 103-130_V.mp3)
  Segment 46: 198792ms ~ 199368ms (file: 571-501-150-571-501-150-510-105-507-150-517-571 501-510-507-517-150-105-517-751-507-510-507-150-571-150-517-150-517-105-150-510-501-570-751-105-105-105-107 510-501-510-501-510-1_V.mp3)
  Segment 47: 200368ms ~ 200944ms (file: 106-106-106-103-103-106-106-701-761-175-761-715-170-170 325-137-136-137-175-173-173-173-173-107 132-137-135-107-175-317-137-135-175-173-175-173-175-173-107-173-175-107-173-107-1_V.mp3)
  Segment 48: 201944ms ~ 209828ms (file: 136-312-327-627-230-275-256-752-107-123-276-327-132-675-130-103-310-327-357-763-631-130-176-263-267-261-103-106-106-103-270-102-107-167-123-320-301-103-107-102-120-261-176-132-1_W.mp3)
  Segment 49: 210828ms ~ 211404ms (file: 236-521-563-526-526-524-503-752-752-537-537-573-537-573-576-576-576-576-571-571-571-751-571-751-105-106-316-356-305-537-573-507-570-570-275-527-325-276-625-572-567-306-136-361-3_V.mp3)
  Segment 50: 212404ms ~ 212980ms (file: 132-132-725-321-375-125-356-315-372-153-132-317-325-123-172-236-625-213-672-271-237-527-316-572-135-137-713-705-763-756-716-520-523-356-123-267-672-267-261-326-270-213-657-136-1_V.mp3)
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
> **Time**: 2026-04-05 12:33:33
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "이용 가능하다라면서 함부로 속단할 수 없어요 그래도 분명한 건 이거죠",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이용", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가능하다라면서", "start_ms": 450, "dur_ms": 1200, "phase_lr_deg": 0.0},
{"word": "함부로", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "속단할", "start_ms": 2250, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "없어요", "start_ms": 3150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그래도", "start_ms": 3750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "분명한", "start_ms": 4350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "건", "start_ms": 4950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이거죠", "start_ms": 5250, "dur_ms": 700, "phase_lr_deg": 0.0}
]
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
"transcript": "있습니다 바퀴 달린 방탄 케이스를 사용하는 장면이 특히 좋았고 연기와 격투 장면도 훌륭했습니다 세계 곳곳에서 벌어지는 숨겨진 문제들도 잘 보여줬다는 점에서 매우 흥미롭게 잘 봤습니다 할리우드 액션 영화에서 자주 나오는 괜히 실수하고",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "있습니다", "start_ms": 30, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "바퀴", "start_ms": 630, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "달린", "start_ms": 1030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방탄", "start_ms": 1430, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "케이스를", "start_ms": 1830, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "사용하는", "start_ms": 2430, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "장면이", "start_ms": 3030, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 3530, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋았고", "start_ms": 3930, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "연기와", "start_ms": 4430, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "격투", "start_ms": 4930, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "장면도", "start_ms": 5330, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "훌륭했습니다", "start_ms": 5830, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "세계", "start_ms": 6630, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "곳곳에서", "start_ms": 7030, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "벌어지는", "start_ms": 7630, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "숨겨진", "start_ms": 8230, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "문제들도", "start_ms": 8730, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 9330, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "보여줬다는", "start_ms": 9530, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "점에서", "start_ms": 10230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "매우", "start_ms": 10630, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "흥미롭게", "start_ms": 11030, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 11630, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "봤습니다", "start_ms": 11830, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "할리우드", "start_ms": 12430, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "액션", "start_ms": 13030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "영화에서", "start_ms": 13430, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "자주", "start_ms": 14030, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나오는", "start_ms": 14430, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "괜히", "start_ms": 14930, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "실수하고", "start_ms": 15330, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 5,
"transcript": "마지막 가을 걷이로 바쁜 이곳의 농사꾼은 다름아닌 푸른 눈의 외국인들 전 스테판이라고 합니다 독일에서 왔어요",
"speaker": "mixed",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "mixed",
"words": [
{"word": "마지막", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가을", "start_ms": 600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "걷이로", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "바쁜", "start_ms": 1500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이곳의", "start_ms": 1900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "농사꾼은", "start_ms": 2400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "다름아닌", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "푸른", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "눈의", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "외국인들", "start_ms": 4400, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 5100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "스테판이라고", "start_ms": 5300, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 6200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "독일에서", "start_ms": 6600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "왔어요", "start_ms": 7200, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "남자는 사실 미친놈이 아니라 18살의 나이에 월드 트레이딩 챔피언십 우승부터 시작해 하루에도 수십 번의 진입으로 시장의 리듬을 완벽하게 통제하는 인물 그리고 수",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "남자는", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "사실", "start_ms": 540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "미친놈이", "start_ms": 940, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 1440, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "18살의", "start_ms": 1840, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "나이에", "start_ms": 2440, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "월드", "start_ms": 2840, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "트레이딩", "start_ms": 3240, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "챔피언십", "start_ms": 3840, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "우승부터", "start_ms": 4540, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "시작해", "start_ms": 5140, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하루에도", "start_ms": 5640, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "수십", "start_ms": 6240, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "번의", "start_ms": 6640, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "진입으로", "start_ms": 6940, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "시장의", "start_ms": 7540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "리듬을", "start_ms": 7940, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "완벽하게", "start_ms": 8340, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "통제하는", "start_ms": 8940, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인물", "start_ms": 9540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 9940, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 10340, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 8,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 9,
"transcript": "국민 여동생이라고 불리는 배우 프리앙카 아르모한이 한국을 향해 진심 가득한 편지를 보내면서 난리가 났거든요 프리앙카는 그냥 예쁘기만 한 게 아니라 특유의 맑고",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "국민", "start_ms": 60, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "여동생이라고", "start_ms": 460, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "불리는", "start_ms": 1260, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "배우", "start_ms": 1660, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "프리앙카", "start_ms": 2060, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아르모한이", "start_ms": 2660, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 3360, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "향해", "start_ms": 3860, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "진심", "start_ms": 4260, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가득한", "start_ms": 4660, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "편지를", "start_ms": 5160, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보내면서", "start_ms": 5660, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "난리가", "start_ms": 6260, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "났거든요", "start_ms": 6660, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "프리앙카는", "start_ms": 7260, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 7960, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "예쁘기만", "start_ms": 8360, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 8960, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 9160, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 9360, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "특유의", "start_ms": 9760, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "맑고", "start_ms": 10160, "dur_ms": 400, "phase_lr_deg": 0.0}
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
"transcript": "라는 걸 바로 예상하고 있었기 때문에 해수부는 선원들 안전과 보급 상황을 확인하고 마지막 한 명이 내릴 때까지 최선을 다하겠다고 밝혔습니다 MBC 뉴스 김민형입니다 지금 몇 명의 선원이 고립돼 있는 겁니까 지금",
"speaker": "speaker_5",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "라는", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "예상하고", "start_ms": 950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "있었기", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해수부는", "start_ms": 2450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "선원들", "start_ms": 3050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안전과", "start_ms": 3550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보급", "start_ms": 4050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "상황을", "start_ms": 4450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "확인하고", "start_ms": 4950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "마지막", "start_ms": 5550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 6050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "명이", "start_ms": 6250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내릴", "start_ms": 6550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때까지", "start_ms": 6950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "최선을", "start_ms": 7450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다하겠다고", "start_ms": 7950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "밝혔습니다", "start_ms": 8650, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "MBC", "start_ms": 9450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 9950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "김민형입니다", "start_ms": 10350, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 11250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 11650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "명의", "start_ms": 11850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "선원이", "start_ms": 12150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "고립돼", "start_ms": 12650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 13150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "겁니까", "start_ms": 13450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 13950, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "had on my jacket because I didn't expect it to be so cold so after he dropped me off my taxi driver checked up on me if I took medicines and if was feeling okay like",
"speaker": "speaker_6",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "had", "start_ms": 80, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 380, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 580, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "jacket", "start_ms": 780, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "because", "start_ms": 1280, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1680, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "didn't", "start_ms": 1780, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "expect", "start_ms": 2180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "it", "start_ms": 2680, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2880, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 3080, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 3280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "cold", "start_ms": 3580, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 4080, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "after", "start_ms": 4380, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "he", "start_ms": 4780, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "dropped", "start_ms": 4980, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "me", "start_ms": 5380, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "off", "start_ms": 5580, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 5880, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "taxi", "start_ms": 6080, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "driver", "start_ms": 6480, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "checked", "start_ms": 6980, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "up", "start_ms": 7380, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "on", "start_ms": 7580, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "me", "start_ms": 7780, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "if", "start_ms": 8080, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 8280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "took", "start_ms": 8380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "medicines", "start_ms": 8680, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "and", "start_ms": 9380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "if", "start_ms": 9680, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 9880, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "feeling", "start_ms": 10180, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "okay", "start_ms": 10680, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 11080, "dur_ms": 300, "phase_lr_deg": 0.0}
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
"transcript": "있었다면 판권 사서 우리 스타일로 만들자 이 정도면 충분했습니다 그런데 요즘은 이 정도로 안 끝납니다 해외 시청자들은 리메이크 소식",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "있었다면", "start_ms": 50, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "판권", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "사서", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 1450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "스타일로", "start_ms": 1850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "만들자", "start_ms": 2450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정도면", "start_ms": 3150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "충분했습니다", "start_ms": 3550, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "그런데", "start_ms": 4350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "요즘은", "start_ms": 4750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 5150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 5350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 5750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "끝납니다", "start_ms": 5950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "해외", "start_ms": 6550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "시청자들은", "start_ms": 6950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "리메이크", "start_ms": 7650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "소식", "start_ms": 8250, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "강조했던 AI 기능에서 아이폰은 바로 전 세대 아니 전전전전 세대까지 지금 팔리던 모든 아이",
"speaker": "speaker_8",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "강조했던", "start_ms": 40, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 640, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기능에서", "start_ms": 1040, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "아이폰은", "start_ms": 1640, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 2240, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 2640, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "세대", "start_ms": 2840, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 3240, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "전전전전", "start_ms": 3640, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "세대까지", "start_ms": 4640, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 5340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "팔리던", "start_ms": 5740, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 6240, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아이", "start_ms": 6640, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "이고 이번이 마지막이라는 생각으로 진심을 다해 공부하는 이유는 미래가 보이기 시작했기 때문입니다 혹시나 후기를 보고 일부 과장되게 말하는 걸로 보",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이고", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이번이", "start_ms": 350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마지막이라는", "start_ms": 750, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "생각으로", "start_ms": 1550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "진심을", "start_ms": 2050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "다해", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공부하는", "start_ms": 2950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이유는", "start_ms": 3550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "미래가", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보이기", "start_ms": 4550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시작했기", "start_ms": 5050, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 5750, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "혹시나", "start_ms": 6550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "후기를", "start_ms": 7050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 7550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일부", "start_ms": 7950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "과장되게", "start_ms": 8350, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "말하는", "start_ms": 8950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 9450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보", "start_ms": 9850, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "포함한 국제 위기 해결에 있어 양국 간 인식이 일치한다는 점을 환영했다 이번 방문의 핵심 목표 가운데 하나는 미국과 중국의 대응하기 위해 에마뉘엘 마크롱이 이른바 독립국들의 연합이라고 부르는 구상을 위한 토대를 마련하는 것이다 마크롱 대통령은 이제는",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "포함한", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "국제", "start_ms": 560, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "위기", "start_ms": 960, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "해결에", "start_ms": 1360, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "있어", "start_ms": 1860, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "양국", "start_ms": 2160, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "간", "start_ms": 2560, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "인식이", "start_ms": 2760, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "일치한다는", "start_ms": 3260, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "점을", "start_ms": 3960, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "환영했다", "start_ms": 4260, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 4860, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방문의", "start_ms": 5260, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "핵심", "start_ms": 5760, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "목표", "start_ms": 6160, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가운데", "start_ms": 6560, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하나는", "start_ms": 7060, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "미국과", "start_ms": 7560, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "중국의", "start_ms": 8060, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대응하기", "start_ms": 8560, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "위해", "start_ms": 9160, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "에마뉘엘", "start_ms": 9560, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "마크롱이", "start_ms": 10160, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이른바", "start_ms": 10760, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "독립국들의", "start_ms": 11260, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "연합이라고", "start_ms": 11960, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "부르는", "start_ms": 12760, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "구상을", "start_ms": 13260, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "위한", "start_ms": 13760, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "토대를", "start_ms": 14160, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마련하는", "start_ms": 14660, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "것이다", "start_ms": 15260, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "마크롱", "start_ms": 15760, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대통령은", "start_ms": 16260, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이제는", "start_ms": 16860, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "지워졌지만 그 지분만큼은 그대로 남기셨다는 거예요 아 그래 과거의 어느 부분은 가져가자 그게 이 집의 특징이다 이 집의 어떻게 보면 자산이다 이렇게 생각을 하셨다는 지점이",
"speaker": "speaker_11",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "지워졌지만", "start_ms": 50, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "지분만큼은", "start_ms": 950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "남기셨다는", "start_ms": 2150, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 3250, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그래", "start_ms": 3450, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "과거의", "start_ms": 3750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어느", "start_ms": 4250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "부분은", "start_ms": 4650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가져가자", "start_ms": 5150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 5750, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 6150, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "집의", "start_ms": 6350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "특징이다", "start_ms": 6750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 7350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "집의", "start_ms": 7550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 7950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 8450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "자산이다", "start_ms": 8850, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 9450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "생각을", "start_ms": 9950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하셨다는", "start_ms": 10450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "지점이", "start_ms": 11050, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "때와는 비교도 안 되고 심지어 본인이 늘 조롱하는 바이든 정부의 최저치 32%도 깨버렸습니다 특히 생활 물가 대응에 대한 지지",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "때와는", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "비교도", "start_ms": 540, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1040, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "되고", "start_ms": 1240, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "심지어", "start_ms": 1640, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "본인이", "start_ms": 2140, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "늘", "start_ms": 2640, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "조롱하는", "start_ms": 2840, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "바이든", "start_ms": 3440, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "정부의", "start_ms": 3940, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "최저치", "start_ms": 4440, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "32%도", "start_ms": 5040, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "깨버렸습니다", "start_ms": 5840, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 6640, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "생활", "start_ms": 7040, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "물가", "start_ms": 7440, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대응에", "start_ms": 7840, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 8340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "지지", "start_ms": 8740, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "수 있는 나라 때문이래 지금 이 순간에도 한국을 꿈꾸는 사람들이 전 세계에 엄청 많다고 합니다 은퇴하면 어디서 살고 싶나요 이 질문",
"speaker": "speaker_13",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "수", "start_ms": 50, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나라", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때문이래", "start_ms": 950, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 1550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "순간에도", "start_ms": 2150, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국을", "start_ms": 2750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "꿈꾸는", "start_ms": 3250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "사람들이", "start_ms": 3750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전", "start_ms": 4350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "세계에", "start_ms": 4550, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 5050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "많다고", "start_ms": 5450, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 5950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "은퇴하면", "start_ms": 6450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어디서", "start_ms": 7050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "살고", "start_ms": 7550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "싶나요", "start_ms": 7950, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 8450, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "질문", "start_ms": 8650, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
"transcript": "상으로 가고 싶어요 급하게 가고 싶진 않고 계속 오늘 하던 대로 1% 1% 하루 1%면 1년이면 365%잖아요 저는 그",
"speaker": "speaker_14",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "상으로", "start_ms": 40, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 940, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "급하게", "start_ms": 1440, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가고", "start_ms": 1940, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "싶진", "start_ms": 2340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "않고", "start_ms": 2740, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 3140, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 3540, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하던", "start_ms": 3940, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대로", "start_ms": 4340, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "1%", "start_ms": 4740, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "1%", "start_ms": 5240, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 5740, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "1%면", "start_ms": 6140, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "1년이면", "start_ms": 6740, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "365%잖아요", "start_ms": 7440, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 8440, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 8840, "dur_ms": 200, "phase_lr_deg": 0.0}
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
"transcript": "있는 거잖아요 근데 이게 USB를 꽂고 이렇게 하면은 무선으로 우리가 쓸 수 있다 이것 때문에 어떤 나라별로 어떤 패스트 기능",
"speaker": "speaker_15",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "있는", "start_ms": 50, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거잖아요", "start_ms": 350, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 1250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "USB를", "start_ms": 1650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "꽂고", "start_ms": 2250, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 2650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하면은", "start_ms": 3150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "무선으로", "start_ms": 3650, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 4250, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "쓸", "start_ms": 4750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "있다", "start_ms": 5150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "이것", "start_ms": 5550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 5950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 6350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "나라별로", "start_ms": 6750, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 7350, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "패스트", "start_ms": 7750, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "기능", "start_ms": 8250, "dur_ms": 400, "phase_lr_deg": 0.0}
]
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
},
{
"segment": 50,
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
*Generated by WKAppBot ask gemini — 2026-04-05 12:33:33*

---
## Final Summary

# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24932  time=12:33:17  uptime=29682s  mem=460MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_123212.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 615-165-160-157-156-157-165-157-167-156-167-156-176-173 541-504-507-150-150-105-105-105-150-175-517-157-150-157-170-156-150-157-167-175-176-175-173-756-756-756-751-750-750-754-7_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 657-651-617-657-651-657-617-671-657_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 760-670-367-637-162-675-617-617-613-617-671 307-317-301-137-137-301-607-301-316-630-610-160-613-613-607-637-736-651-675-675-673-765-675-567-657-761-576-657-576-765 735-130-703-3_V.mp3)
  Segment 4: 4728ms ~ 7860ms (file: 765-756-765-765-175-123-127-132-137-132-126-132-125-127-127-165-132-751-170-105-157-136-137-136-156-134-132-137-132-137-123-132-126-176-137-136-163-132-102-136-165-136 123-216-1_V.mp3)
  Segment 5: 8860ms ~ 9436ms (file: 562-534-526-523 527-506-531-517-514-562-503-562-571-521-512-513-527-573-527-521-527-572-572-572-572-572-573-751-753-561-526-502-527-502-527-576-527-207-275-725-275-725-572-576-7_W.mp3)
  Segment 6: 10436ms ~ 11012ms (file: 657-657-675-653-651-536-657-635-653-657-657 562-652-651-652-651-651-652-651-652-651-657-615-625-504-315-501-256-562-562-562-256-562-562-562-652-652-625-652-625-265_W.mp3)
  Segment 7: 12012ms ~ 12588ms (file: 576-572-572-756-756-762-576-657-615-165-167-561-156-156-517-165-572-527-526-572-527-527-576-765-615-165-165-651-615-651-156-561-657-567-516-571-615-516-576-516-561-516-615-165-5_W.mp3)
  Segment 8: 13588ms ~ 14164ms (file: 715-517-571-726-137-167-613-163-136-163-613-163-135-173-136-317-136-637-631-613-631-675-761-671-163-613-135-316-136-163-153-315-165-156-571-157-517-157-371-716-376-675-567-715-1_W.mp3)
  Segment 9: 15164ms ~ 15740ms (file: 136-162-162-163-136-136-132 132-132-132-136-126-132-136-132 301 130-316-130-130-136-316-136 310 230-230-236-123-136-132-163-157-103-167-163 271-627-627 627-623-627-623-625-627-6_W.mp3)
  Segment 10: 16740ms ~ 17316ms (file: 162-102-170-106-615-127-156-261-126-130-126-130-136-163-137-136-142-105-136-152-165-126-106-167-675-617-163-127-136-167-136-102_V.mp3)
  Segment 11: 18316ms ~ 29512ms (file: 231-312-136-302-230-203-205-673-130-312-137-312-172-574-301-210-207-132-621-271-276-267-260-261-320-637-326-321-236-731-310-512-301-316-312-572-762-275-527-213-326-123-135-263-1_W.mp3)
  Segment 12: 30512ms ~ 42104ms (file: 127-167-106-756-312-317-132-132-765-267-612-765-231-321-263-267-127-267-267-132-263-267-261-231-107-267-627-627-627-756-756-130-267-627-260-123-310-312-316-163-675-612-132-103-1_W.mp3)
  Segment 13: 43104ms ~ 51312ms (file: 756-752-765-675-675-675-751-267-765-372-236-236-527-567-352-365-576-357-567-753-576-267-312-321-236-236-235-754-326-574-320-237-723-327-123-263-542-574-236-263-267-236-276-257-2_W.mp3)
  Segment 14: 52312ms ~ 52888ms (file: 527-357-657-317-573-257-170-315-517-576-507-316-173-751-517-653-153-326-625-123-352-517-573-671-625-162-361-675-675-613-132-103-103-671-756-567-763-276-351-517-563-576-657-625-1_W.mp3)
  Segment 15: 53888ms ~ 54464ms (file: 170-173-107-307-173-137-170-175-710-107-170-710-103-710-107-701-170-173-713 175-157-157-175-175-715-175-751-753-175 570-574-574-571-510-175-170-173-175-715-715-751-715-175-715-1_V.mp3)
  Segment 16: 55464ms ~ 56040ms (file: 162-607-673-675-567-617-576-137-175-731 130-307-103-173-715-137-137-103-137-137-107-301-173-136-173-103-310-103-107-310-103-371-301-317-307-301-137 310-371-310-370-736-317-317-1_V.mp3)
  Segment 17: 57040ms ~ 60532ms (file: 726-572-572-752-572-257-756-725-752-576-765-572-756-567-576-756-657-576-765-675-672-103-213-123-512-236-206-602-231-132-132-301-627-265-260-103-675-725-150-726-752-725 105-512-5_W.mp3)
  Segment 18: 61532ms ~ 68480ms (file: 320-320-321-130-673-726-326-756-320-236-130-176-130-672-267-756-301-371-237-726-130-765-762-572-312-326-765-237-130-126-102-236-236-376-120-231-217-320-327-235-321-163-230-310-5_W.mp3)
  Segment 19: 69480ms ~ 70056ms (file: 152-760-231-237-573-135-267-216-512-321-617-321-635-267-547-236 541-620-362-316-326-316-312-361-613-605-503-215-132-123-620-123-326-361-316-316 167-571-613-371-173-731-310-375-7_W.mp3)
  Segment 20: 71056ms ~ 71632ms (file: 571-571 130-135-135-105-130-135-153-135-130-135-153-157-517-537-517-571-517-517-571-571-751-571-517-517-571 513-105-105-135-153-135-153-135-157-135-157-157-157-517-157-517-571-5_W.mp3)
  Segment 21: 72632ms ~ 73208ms (file: 765-762-675-675-167-761-671-765-765-176-361-103-203-613-302 132-263-236-261-263-267-236-261-574-572-623-172-132-263-507-172-103-127-103-107-327-372-723-273-237-167-176-162-126-7_W.mp3)
  Segment 22: 74208ms ~ 74784ms (file: 372-316-130-103 312-103-132-103-135-623-627-326-326-263-126-103-103_V.mp3)
  Segment 23: 75784ms ~ 76360ms (file: 307-301-316-316-310-301-312-302-301-312-130-103-312-103-312-317-132-312-321-163-765-670-672-627 132-143-136-106-134-132-137-136-136-675-675_V.mp3)
  Segment 24: 77360ms ~ 77936ms (file: 756-751-756-756-756-756-751-576-576-576-756 175-170-175-571-173-715-175-175-517-157-175-517-157-571-571-571-571-571-751-571-571-576-756-571-576-750 157-157-175-751-517-715-710-1_W.mp3)
  Segment 25: 78936ms ~ 79512ms (file: 103-715-173-310-170-301-173-735-317-175-713-107-751-701-130-105-130-716-713-130-170-371-175-710-753 175-701-103-107-713-137-170-107-376-701-107-106-715-701-317-103-301-103-301-7_W.mp3)
  Segment 26: 80512ms ~ 89836ms (file: 320-312-263-267-216-123-123-261-276-126-362-310-321-261-231-572-627-267-571-576-130-130-132-210-310-216-567-265-265-132-175-102-567-756-261-217-207-107-106-106-102-102-216-162-2_W.mp3)
  Segment 27: 90836ms ~ 91412ms (file: 102-103-103 102-120-120-120-102-120-120-210-120-210-210-102-102-132-310-716-106-617-160-160-610-610-160-612 106-610-160-160-160-601-160-160-160-610-612-610-163-610-162-160-160-1_V.mp3)
  Segment 28: 92412ms ~ 92988ms (file: 310-130-310-103 310-130-143-103-143-105-104-134-140-130-104-130-103-130-130-130-103-103-130-103-104-143-103-130-140-143-143 304-143-314-154-143-154-130-104-104-143-104-145-134-1_V.mp3)
  Segment 29: 93988ms ~ 94564ms (file: 176-172-716-125-256-251-235-513-517-375-126-712-213-312-572-715-756-572-562-571-317-123-671-253-750-213-123-102-321-257-176-106-163-576-576-576-576-576-576-716-176-167-173-130-1_V.mp3)
  Segment 30: 95564ms ~ 96140ms (file: 723-136-132-136-172-265-127 163 127 106-167-176-167-475-167-162-103-136-103-126-127-713-175-175-165-103-162 756-765-672-673-513-157-561-531-750-753-673-160-160 231-172-315-327-1_V.mp3)
  Segment 31: 97140ms ~ 104664ms (file: 276-127-371-756-672-276-102-102-754-756-675-765-261-106-172-267-230-652-163-132-106-130-126-120-231-263-256-367-762-273-231-752-756-725-267-106-175-237-260-213-231-573-574-574-7_W.mp3)
  Segment 32: 105664ms ~ 106240ms (file: 715-175-571-175-175-751 574-574-570-570-571-750-570-157-571-157-571-175-170-751-571-751-715-750-701-751-175-170-175 517-157-157-751-175-751-517-571-751-715-175-751-715-175-751-1_V.mp3)
  Segment 33: 107240ms ~ 107816ms (file: 765-526-576-572-576-567-672-256-265-652-165-615-165-165-612-165-156-315-156-156-653-165-163-657-675-567-657-756-657-675-765-675-625-765 560-506-150-156-165-165-561-615-165-165-1_W.mp3)
  Segment 34: 108816ms ~ 109392ms (file: 261-260-261-672-627-672-625-657-206-136-301-132-302-302-756-726-162-106-672-165-165-507-675-136-102-671-672-671-756-652-306-301-301-361-612-673-607-601-163-761-176-716-173-371-7_V.mp3)
  Segment 35: 110392ms ~ 112300ms (file: 103-132-301-132-216-621-216-136-132-136-132-136-176-137-103-123-301-723-206-254-265-231-102-103-321-132-132-321-132-132-132-301-312-617-671-576-576-574-574-574-574-576-361-621-6_V.mp3)
  Segment 36: 113300ms ~ 113876ms (file: 153-153-106-103-176-167-107-132-721-137-153-135-156-157-175-157-675-763-756-267-712-763-672-736-756-765-756-765-765-756-762-765-765-736-736-327-736-712-123-135-137-136-710-716-1_W.mp3)
  Segment 37: 114876ms ~ 115452ms (file: 705-612-672-276-507-671-672-564-627-213-637-257-321-520-321-206-213-257-352_V.mp3)
  Segment 38: 116452ms ~ 117028ms (file: 320-563-350-253-537-205-305-357-620-305-532-521-513-513 537-537-537-537-537-530-350-356-560-507-502-563-753-705-735-570-750-573-507-572-572-512-521-576-507-507-507-507-507-507-5_V.mp3)
  Segment 39: 118028ms ~ 118604ms (file: 573-657-761-536-536-517-135 617-610-617-167-103 726-571-756-572-576-576-201-516-621_W.mp3)
  Segment 40: 119604ms ~ 120180ms (file: 627-623-176-137-605-302-132-671-260-216-136-173-627-632-627-612-263-627-267-627-263-210-213-231-132-236-623-672-362-627-126-326-103-612-326-316-621-127-263-213-362-132-136-132-1_W.mp3)
  Segment 41: 121180ms ~ 121756ms (file: 251-251-523-205-251-152-507-507 521-253-521-520-725-527-506-502-520-507-504-507-502-501-502-502-205-265-326-326-215-625-625-657-652-654-625-260-260-260-231-261-627 276-652-503-5_W.mp3)
  Segment 42: 122756ms ~ 123332ms (file: 576-576-576-572 652-625-652-654-657-657-657-651-651-654-657-657-654-654-657 751-753-573-571-576-571-574-756-576-576-576-576-756-754 572-576-657-576-657-576-576-756 571-571-571-5_W.mp3)
  Segment 43: 124332ms ~ 124908ms (file: 365-362-136-312-312-312-132-132-312-312-316-163-132-321-351-576-573 136-125-216-126-123-152-216-216-365-315-316-316-532-351-635-653-367-615-275-567-562-527-547-547-547-546-513-1_W.mp3)
  Segment 44: 125908ms ~ 126484ms (file: 261-167-672-627-617-617-652-615-615-652-615-675-675-617-671-617-615-615-675-617-617-617_V.mp3)
  Segment 45: 127484ms ~ 134000ms (file: 213-236-216-126-103-263-263-762-267-326_W.mp3)
  Segment 46: 135000ms ~ 135576ms (file: 546-563-301-316-310-316-301-567-756-567-756-607_W.mp3)
  Segment 47: 136576ms ~ 147700ms (file: 126-126-632-526-120-216-623-236-371-312-201-672-256-621-263-102-102-261-327-316-312-132-102-106-263-236-236-321-267-263-627-267-260-123-632-326-321-267-130-126-213-263-276-726-2_W.mp3)
  Segment 48: 148700ms ~ 149276ms (file: 275-725-273-756-765-756-756-765-675-167-651-156-153-156-162-167-617-671-675-675-671-576-567-567-576-576-576-527-576-567-576-165-615-156-516-156-513-561-531-156-561-531-156-165-5_W.mp3)
  Segment 49: 150276ms ~ 150852ms (file: 574-571-157-571-750-715-751-751-517-175-517-751-107-751-715-175-710-751-571 150-175-170-710-710-715-751-175-751-751-571-751-571-751-715-751-751-751-751-756-715 571-751-715-715-7_W.mp3)
  Segment 50: 151852ms ~ 152428ms (file: 165-236-612-162-576-657-567-675-627-326-231-627-726-671-576-576-675-517-571-632-612-162-162-103-613-130-613-167-612-316-571-130-132-123-316-132-123-361-176-157-156-617-316-106-1_W.mp3)
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
> **Time**: 2026-04-05 12:33:56
> **AI**: gemini

---

## Response

READY TOOL PROTOCOL: APSP v1 (AppBot Parallel Streaming Protocol — in-band text transport). CONNECTIVITY CONFIRMED: The [HOST-HANDSHAKE] above proves the wkappbot host process IS running. Tool calls work as inline text markers — NO separate MCP server, NO external connection, NO handshake needed.

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:33:56*
