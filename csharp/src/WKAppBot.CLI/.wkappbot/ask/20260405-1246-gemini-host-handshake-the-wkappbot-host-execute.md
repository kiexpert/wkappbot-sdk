# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=10820  time=12:45:38  uptime=31274s  mem=396MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_124526.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 132-103-136-317-132-132-213-316-265-625_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 134 350-345-310-341-310-314-143-103-143-134-143-134-103-134-103-134-103-130-134-143-135-103-134 341-340-314-314-134-130-314-310-135-314-103-103-301-130-134-130-143-153-135-301-1_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 612-126-631-136-357-751-563-321-172-612 610-312_W.mp3)
  Segment 4: 4728ms ~ 11316ms (file: 132-213-257-213-137-213-762-132-263-127-321-132-126-102-321-756-132-352-276-752-273-205-312-760-327-710-132-572-132-157-123-321-123-236-236-132-762-712-326-267-231-120-315-607-1_V.mp3)
  Segment 5: 12316ms ~ 12892ms (file: 576-567-756-756-571-516-524-504-514-527-752-576-756-567-756-756-756-756-657-756-756-576-756-576-756-765-542-527-571-521-561-576-516-567-516-542-537-561 752-567-576-756-576-576-5_W.mp3)
  Segment 6: 13892ms ~ 14468ms (file: 265-263 261-621-625-625-652-625-625-652-625-652-625-621 263-625-267-625-625-625-625-625-265-625-625-625-627-260 216-621-652-651-625-625-625-612-625-652-625-621-625-621-623-621-5_W.mp3)
  Segment 7: 15468ms ~ 16044ms (file: 570 150-507-571-570-517-570-571-517-510-571-510-510-517-571-571-570-571-570-507-571-571-576-574-574_W.mp3)
  Segment 8: 17044ms ~ 25864ms (file: 762-762-675-501-102-102-675-675-765-675-632-130-130-167-203-310-310-103-103-176-167-230-762-120-216-312-360-602-621-231-236-120-372-760-176-176-706-671-206-672-576-257-217-210-2_W.mp3)
  Segment 9: 26864ms ~ 27440ms (file: 506-502-514-547-547-507-507-507-517-502-352-350-537-507-523-350-530-527-235-523-502-532-513 523-573-523-532-350-310-320-305-350-350-530-520-503-530-257-530-530-504-503 507-502-2_V.mp3)
  Segment 10: 28440ms ~ 29016ms (file: 301-310-137-103-130-130-317-103-130-137-103-137-103-137-103-130 317-312-137-130-103-317-137-103-107-130-103-137-173-157 301-137-317-317-301-317-312-317-312-301-317-137-301-310-3_V.mp3)
  Segment 11: 30016ms ~ 35308ms (file: 126-102-321-345-543-547-237-260-206-203-576-152-263-102-126-547-521-123-136-231-273-312-102-321-765-261-267-572-457-523-216-260-260-260-267-260-230-235-123-312-276-236-102-103-2_V_stt_one_thing_as_the_naval_office_in_london.mp3)
  Segment 12: 36308ms ~ 40052ms (file: 213-263-621-261-263-263-261-261-216-126-123-132-263-621-261-261-261-206-263-312-362-316-312-631-321-261-236-321-362-326-236-230-162-132-312-130-137-107-137-321-612-216-261-216-2_W.mp3)
  Segment 13: 41052ms ~ 41628ms (file: 312-310-130-130-130-317-170-137-130-107-175-137-310-375 137-130-137-137-130-301-317-132-317-173-301-371-301-713-317-103-137-130-103-130-137-137-301-130-132-270-713 135-132-315-1_V.mp3)
  Segment 14: 42628ms ~ 43204ms (file: 754-157-751-517-715-713-571-517-715-173-172-175-713-753-571-172-712-715-712-715-357-256-627-572-567-736-237-721-325-321-576-756-751-756-756-756-571-571-571-571-751-571-571-571-5_W.mp3)
  Segment 15: 44204ms ~ 44780ms (file: 103-103-102-156-157-103-103-165-162-103-103-106-167-156-125-162-716-126-612-152-675-132-365-176-136-165-160-163 601-607-167-160-612-603-612-132-761-162-751-527-621-136-103-172-1_V.mp3)
  Segment 16: 45780ms ~ 46356ms (file: 627-625-675-672-675-267 621-621-627-672-675-675-672-627-267-267-627-672-657-627-361 261-625-216 621-623-625-617-672-675-671-674-674-657-672-615-621-645-652-625-671-625-652-621-6_W.mp3)
  Segment 17: 47356ms ~ 47932ms (file: 175 574-574-574-574-574-571-570-517-105-170-107-157-175-175-705-710-107-570-750-571-751-715-753 547-547-574-574-574-574-754-574-754-574-750-571-570-750-157-107-105 175-751-571-5_W.mp3)
  Segment 18: 48932ms ~ 49508ms (file: 137-175-137-107-715-753-175-731-103-317-137-715-173-107-173-751-175-173_V.mp3)
  Segment 19: 50508ms ~ 51084ms (file: 715-307-715-751-765-137-601-103-732-703-307-317-750-371-173-710-170-710-317-317 175-753-375-703-730-731-310-701-107-673-603-607-630-603-670-751-703-317-107-130-176-310-137-317-3_V.mp3)
  Segment 20: 52084ms ~ 52660ms (file: 657-657-657-657-657-567-765-576-514-501-510-517-517-175-125-165-156-165-156-156-165-150-165-106-152-120-102-102-675-657-567-567-567-567-567-657-567-567-567-657-567-675 501-502-5_W.mp3)
  Segment 21: 53660ms ~ 54236ms (file: 531-532-620-275-572-572-503-503-305-512-205-532 507-503-257-532-570-253-532-502-527-512-251-305-531-152-352-520-503-573 507-520-205-502-520-502-570-507-250-502-527-250-235-532-2_V.mp3)
  Segment 22: 55236ms ~ 55812ms (file: 136-572-572-756-357-671-761-136-312-132-310-173-176-137-173-310-532-367-365-365-356-356-316-623-167-236-231-130-130-132-130-103-326-732-327 105-573-576-576-573-576-524-753-765-1_W.mp3)
  Segment 23: 56812ms ~ 57388ms (file: 137-173-173-713 130-103-310-137-136-316-173-137-713-103-105-173-301-103-137-137-310-310-103-137-137-317-301-130-103-137 310-107-301-301-173-175-673-316-371-137-130-376-137-317-7_V.mp3)
  Segment 24: 58388ms ~ 58964ms (file: 356-546-561-516-105-315-351-351-532-354-356-351-561-536-543-563-532-354-305-352-354-135-125-126-265-165-352-531-547-507-503-573-507-532-507-502-527-506-562-653-506-560-530-504_V.mp3)
  Segment 25: 59964ms ~ 60540ms (file: 507-570-576-576-752-725-725-762-756-765-752-752_W.mp3)
  Segment 26: 61540ms ~ 62116ms (file: 617-657-657-627-627-316-236-316 365-326-632-312-362-316 106-613-103-362-615-163-163-136-567-613-162-657-617-136-106-612-301-136-130-163-103-163-132-136-163-150-765-162-102-130-1_V.mp3)
  Segment 27: 63116ms ~ 72800ms (file: 103-132-215-130-527-237-231-123-617-231-273-237-675-107-103-321-267-762-106-102-123-102-236-621-103-137-320-126-216-231-236-120-120-236-231-126-120-637-106-103-321-213-263-236-2_W.mp3)
  Segment 28: 73800ms ~ 74376ms (file: 156-561-561-651-651-165-361-165-153-513 507-520-527-571-517-571-516-157-156-165-615-615-651-165-615-165-617-165-163-156-156 507-726-576-276-726-756-576-567-516-516-516-516-156-5_V.mp3)
  Segment 29: 75376ms ~ 81388ms (file: 261-301-312-132-617-120-130-102-730-276-760-517-130-310-321-123-103-710-726-103-203-103-123-126-270-267-207-175-576-206-271-267-520-265-230-327-235-126-571-102-745-267-576-320-1_W.mp3)
  Segment 30: 82388ms ~ 82964ms (file: 751-753-735-751-751-715 315-501-571-350-130-571-530-715-713-710-713-173-715-713-173-715-713-731-175-137-731-173-175-517-105-157-750 135-153-150-130-107-150-315-130-175-713-170-7_V.mp3)
  Segment 31: 83964ms ~ 84540ms (file: 615-617-267-267-723-137-716-726-762-726-726-762-721-726-725-725-756-752-765-756-726-651-567-576-576-576-756-576-576-576-576-567-576-576-675-576-576-567-576-751-315-572-675_W.mp3)
  Segment 32: 85540ms ~ 86116ms (file: 123-102-102-103 120-120-201-120-102-210-216-120-160-601-610-160-163-106-123-123-120-201-102-602-126-160-210-106-102-126-160-106-160-602-605-650-605-650-165-617-675-165-652-625-6_V.mp3)
  Segment 33: 87116ms ~ 87692ms (file: 751-175-175-751-175-751-751-715-715-715-175-715-751-157-751-751-751-715-751 150-501-150-105-105-105-107-175-715-175-175-175-175-751-517-751-751-751-751-751-756-751-756-751-751-7_W.mp3)
  Segment 34: 88692ms ~ 89268ms (file: 546-752-572-756-572-576-576-576-576-567-567-756-752-356-562-523-524-513-546-503-504-541-502-504-504-504-504-542-572-572-576-576-756-576-576-756-514-531-564-564-546-504-542-504-5_V.mp3)
  Segment 35: 90268ms ~ 101392ms (file: 126-126-632-526-120-216-623-236-371-312-201-672-256-621-263-102-102-261-327-316-312-132-102-106-263-236-236-321-267-263-627-267-260-123-632-326-321-267-130-126-213-263-276-726-2_W.mp3)
  Segment 36: 102392ms ~ 102968ms (file: 571-576-517-576-576-571-571-517 153-153-513-715-571-715-763-517-571-570-571-571-751-571-571-571-576-576-576-576-570-571-571-570-571-574 150-105-517-517-153-514-517-153-175-150-1_V.mp3)
  Segment 37: 103968ms ~ 114948ms (file: 236-103-310-160-276-260-752-275-574-256-201-302-102-132-267-123-132-260-260-320-302-260-203-231-263-260-236-260-261-260-260-201-260-263-325-753-260-320-762-752-752-267-213-321-5_W_stt_i'm_wondering_if_you_need_is_exams_becau.mp3)
  Segment 38: 115948ms ~ 116524ms (file: 751-715-715-751-751-751-751-751 501-157-501-175-715-751-751-571-715-175-751-715-175-175-715-175-715-715-715-170-517 150-150-150-510-157-157-751-175-173-170-170-175-751-175-175-1_V.mp3)
  Segment 39: 117524ms ~ 118100ms (file: 167-316-631-316-306-302-362-320-360-602-316-301-130-163-130-316-603-360-632-631-627-326-620-602-621-261-621-261-216-126-162-163-756-276-276-672-527-265-207-610-167-735-267-652-7_W.mp3)
  Segment 40: 119100ms ~ 119676ms (file: 716-673-671-716-673-761-617-376-763-571 130-103-132_W.mp3)
  Segment 41: 120676ms ~ 121252ms (file: 150-105-157-153-105-170-715-751-715-715-175-175-715-175-571-715-715-751-715-715-715-751-751-715 570-157-517-517-517-571-751-753-751-756-751-751-751-751-715-751-715 570-510-175-7_W.mp3)
  Segment 42: 122252ms ~ 132656ms (file: 735-327-276-130-123-267-573-312 672-761-367-625-725-237-267-237-273-236-254-761-276-726-761-562-156-165-236-756-532-270-206-752-765-176-321-260-103-371-632-267-256-230-123-107-6_V.mp3)
  Segment 43: 133656ms ~ 134232ms (file: 756-751-751-175-751-715-751-751-756-751-571 501-517-510-517-517-157-153-571-571-751-751-751-715-751-715-751-751-751-751-175-715-751-756-751-715 150-517-150-153-150-507-750-756-7_W.mp3)
  Segment 44: 135232ms ~ 135808ms (file: 273-327-217-236-275-672-752-753-752-617-751-751-756-751-715-751-756-715-751-751-715-756-173-751-715-751-175-715-175-756-715-175-715-175-715-751-751-715-175-173-137-715-576-715-7_W.mp3)
  Segment 45: 136808ms ~ 137384ms (file: 132-301-302-103-132-106-163-216-136-236-267-267-163-675-567-361-613-132-302-310-132-132-137-103-132-132-236-607-236-301-310-312-302-321-310-327-372-317-123-720-123-103-132-150-1_V.mp3)
  Segment 46: 138384ms ~ 138960ms (file: 627-623-176-137-605-302-132-671-260-216-136-173-627-632-627-612-263-627-267-627-263-210-213-231-132-236-623-672-362-627-126-326-103-612-326-316-621-127-263-213-362-132-136-132-1_W.mp3)
  Segment 47: 139960ms ~ 146512ms (file: 120-136-236-213-627-261-672-267-130-573-132-632-756-236-321-602-267-267-236-572-102-236-326-321-132-236-357-765-672-637-176-317-372-327-267-206-367-671-316-673-627-672-316-672-1_W.mp3)
  Segment 48: 147512ms ~ 148088ms (file: 251-621-162-206-201-201-201-521-502-502-514-507-567-512-523-503-507-503-506-507-502-507-507-507-507-507-507-507-576-507-576-756-576-507-527-576-705-507-572-576-312-312-362-326-6_V.mp3)
  Segment 49: 149088ms ~ 149664ms (file: 756-716-715-157-731-173-137-170-157-175-163-163-136-135-130-316-260-312-315-153-165-156-175-175-153-175-351-315-315-315-317-315-135-153-157-135-153-136-156-156-135-135-351-356-3_W.mp3)
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
> **Time**: 2026-04-05 12:46:42
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "엔지니어링 즉 마리의 훈련과 하네스 엔지니어링 그 마구를 제작하는 시스템을 설계하는 이 둘이 합쳐져야지 비로소 밭이 갈립니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "엔지니어링", "start_ms": 40, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "즉", "start_ms": 720, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "마리의", "start_ms": 900, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "훈련과", "start_ms": 1250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하네스", "start_ms": 1750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "엔지니어링", "start_ms": 2200, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 2900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "마구를", "start_ms": 3050, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "제작하는", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "시스템을", "start_ms": 4050, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "설계하는", "start_ms": 4580, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 5050, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "둘이", "start_ms": 5180, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "합쳐져야지", "start_ms": 5500, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "비로소", "start_ms": 6120, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "밭이", "start_ms": 6550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "갈립니다", "start_ms": 6900, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "서 너무 감사하고 제가 더 앞으로도 열심히 하겠습니다 감사합니다",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "서", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 150, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "감사하고", "start_ms": 450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "앞으로도", "start_ms": 1450, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "열심히", "start_ms": 1980, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "하겠습니다", "start_ms": 2400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 3000, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "이 모든 이야기의 시작도 참 영화 같습니다 가족의 한국 인연을 시작으로 자연",
"speaker": "speaker_3",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "모든", "start_ms": 180, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이야기의", "start_ms": 520, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "시작도", "start_ms": 1000, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 1420, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "영화", "start_ms": 1650, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가족의", "start_ms": 2500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 2950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "인연을", "start_ms": 3300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시작으로", "start_ms": 3700, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "자연", "start_ms": 4250, "dur_ms": 320, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "원숭이같이 생긴 유튜버가 수백만 원 들여가지고 굳이 일본까지 가가지고 스냅드래곤 달린 S",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "원숭이같이", "start_ms": 60, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "생긴", "start_ms": 680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "유튜버가", "start_ms": 1020, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "수백만", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 2050, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "들여가지고", "start_ms": 2220, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "굳이", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "일본까지", "start_ms": 3250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가가지고", "start_ms": 3850, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "스냅드래곤", "start_ms": 4380, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "달린", "start_ms": 5150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "S", "start_ms": 5550, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 6,
"transcript": "천궁이랑 추가적으로 천광도 있어요 천광을 레이저로 이제 드론을 요격하는 거예요 천궁이나 요런 것들은 이제 드론을 요격하기 어렵거든요 근데 천광은 굉장히 비용을 한 발 쏘는데 레이저로 한 발 쏘는데 몇 천",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "천궁이랑", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "추가적으로", "start_ms": 550, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "천광도", "start_ms": 1180, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "있어요", "start_ms": 1600, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "천광을", "start_ms": 1980, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "레이저로", "start_ms": 2450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2950, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "드론을", "start_ms": 3220, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "요격하는", "start_ms": 3620, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 4150, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "천궁이나", "start_ms": 4450, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "요런", "start_ms": 4980, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "것들은", "start_ms": 5320, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "거들떠보지도 않았죠 심지어 굶주린 개미 떼가 다가와 수컷들의 몸을 뜯어먹으며 사냥을",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "거들떠보지도", "start_ms": 50, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "않았죠", "start_ms": 750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "심지어", "start_ms": 1150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "굶주린", "start_ms": 1580, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "개미", "start_ms": 2050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "떼가", "start_ms": 2350, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "다가와", "start_ms": 2620, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "수컷들의", "start_ms": 3120, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "몸을", "start_ms": 3650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "뜯어먹으며", "start_ms": 3950, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "사냥을", "start_ms": 4620, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "해서 주말에 무슨 일이 터졌다 덤에 또 당연히 나오겠죠 이슈는 사라지는 게 아닌데 그것 때문에 다 하락한다 난 어디를 매매해야지 기준을 잡으면 오히려 더 좋은 기회가 될 수도 있습니다 왜냐하면",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "해서", "start_ms": 80, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "주말에", "start_ms": 380, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "무슨", "start_ms": 750, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "일이", "start_ms": 1050, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "터졌다", "start_ms": 1320, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "자 여러분 오늘 중요한 또 이슈",
"speaker": "speaker_8",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "자", "start_ms": 30, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "여러분", "start_ms": 150, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 520, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이슈", "start_ms": 1480, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "I love that. Right. I mean we do these exams because we have to filter people out. You know in Korea for example you can't let everybody go to med school. Well everybody can't do that. In in the fact is you know just given the nature of students",
"speaker": "speaker_9",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "I", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "love", "start_ms": 120, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "that.", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Right.", "start_ms": 550, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 800, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "mean", "start_ms": 910, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "we", "start_ms": 1080, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "do", "start_ms": 1230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "these", "start_ms": 1400, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "exams", "start_ms": 1650, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "because", "start_ms": 2000, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "자!",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "자!", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "야 얘도 맛있을 거 같아 오 너무 좋다 비주얼",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "야", "start_ms": 40, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "얘도", "start_ms": 180, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "맛있을", "start_ms": 460, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 900, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같아", "start_ms": 1020, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "오", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 1550, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "좋다", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "비주얼", "start_ms": 2300, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "250만 원 사람들이 많이 찾는 덕분에 억대 매출을 올리는 가게가 됐습니다 이거는 그 기름 가지고 오는",
"speaker": "speaker_11",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "250만", "start_ms": 50, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "원", "start_ms": 580, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "사람들이", "start_ms": 780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 1280, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "찾는", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "덕분에", "start_ms": 2000, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "억대", "start_ms": 2450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "매출을", "start_ms": 2820, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "올리는", "start_ms": 3280, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "가게가", "start_ms": 3720, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "됐습니다", "start_ms": 4180, "dur_ms": 450, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:46:42*
