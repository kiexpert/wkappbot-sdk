# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=11716  time=10:55:50  uptime=32807s  mem=478MB  system_processes=352"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_105204.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 8712ms (file: 321-326-267-267-206-263-321-236-725-263-267-267-301-310-301-753-735-756-721-231-267-263-103-310-312-310-756-765-263-260-267-675-107-237-273-761-175-267-267-276-572-574-310-230-1_W_stt_the_yet_they_are_seeking_to_the_masses_s.mp3)
  Segment 2: 9712ms ~ 26416ms (file: 145-230-321-102-103-521-103-104-103-230-103-312-103-102-103-203-123-102-320-103-103-103-413-132-106-123-103-123-301-214-103-301-301-203-321-102-201-126-231-237-367-321-312-321-1_V.mp3)
  Segment 3: 27416ms ~ 35480ms (file: 547-320-273-230-203-157-270-270-756-574-105-765-265-716-103-137-137-107-754-257-205-572-301-302-276-276-267-276-312-273-103-103-754-574-301-176-571-547-103-276-127-301-310-176-5_W.mp3)
  Segment 4: 36480ms ~ 37056ms (file: 156 501-510-150-107-105-105-105-150-105-150-150-150-150-157-751-165-750-750-750-570-105-105-105-150-105-150-150-150-157-150-175-651-156-150-105-150-105-135-105-157-157-103 150-1_V.mp3)
  Segment 5: 38056ms ~ 43636ms (file: 210-237-210-132-130-103-172-210-302-210-321-321-102-301-210-321-320-312-103-312-132-130-320-210-321-312-120-123-312-103-123-201-201-103-103-120-127-230-201-130-102-310-301-132-1_V.mp3)
  Segment 6: 44636ms ~ 49532ms (file: 213-621-612-316-326-312-132-230-263-362-216-367-637-213-312-230-205-301-321-213-201-231-320-163-320-326-132-213-320-126-271-231-236-276-302-307-276-367-326-267-237-231-756-267-2_W.mp3)
  Segment 7: 50532ms ~ 58667ms (file: 120-312-201-206-260-102-627-126-750-317-571-302-260-576-260-215-130-103-501-302-260-271-201-275-312-206-132-301-201-260-132-132-760-672-267-527-574-217-261-102-210-672-607-261-2_W.mp3)
  Segment 8: 59667ms ~ 60243ms (file: 507-517-501-507-570-507-507-570 570-517-510-517-517-157-517-571-501-507-517-501-517-157-517-501-105-507-501-570-507-501-507-510-570-510-507-510-571 510-501-517-105-105-517-517-1_V.mp3)
  Segment 9: 61243ms ~ 63079ms (file: 574-576-267-107-176-103 765-765-765-675-765-543-621-657-135-623-132-132-623-326-321-362-576-173-132-675-670-652-316-362-316-316-671-712-127-217-213-213-206-127-576-530-517-130-1_W.mp3)
  Segment 10: 64079ms ~ 64655ms (file: 136-607-351-612-635-631-756-675-136-310-316-316-130-316-136-760-701-371-731-130-132-123-237-213-512-125-312-132-362-231-571-321-267-236-315-326-507-675-756-367-567-271-125-273-6_W.mp3)
  Segment 11: 65655ms ~ 66231ms (file: 562-567-106-271-726-273-273-271-231-321-231-231-263-621-213-326-130-103-132-312-213-132-236 123_W.mp3)
  Segment 12: 67231ms ~ 67807ms (file: 153-135-351-517-130-617-150-130-103-153-157-157-153-157-175-517-571-157-751 157-105-153-150-310-135-150-105-107-137-103-150-153-157-153-157-715-153-157-517-517 137-153-135-315-1_V.mp3)
  Segment 13: 68807ms ~ 76439ms (file: 756-756-267-267-756-126-167-756-756-267-320-132-612-137-176-765-576-231-754-756-576-267-213-201-671-130-751-574-627-627-672-132-751-371-367-312-361-562-576-756-275-765-231-231-1_W.mp3)
  Segment 14: 77439ms ~ 79635ms (file: 576-576-256-315-571-576-527 572-572-567-576-576-576-576-567-576-576-576-756-576-576-567-756-576-576-657-576-576-567-576-567-516-571-571-517 752-576-567-576-576-567-576-576-576-7_W.mp3)
  Segment 15: 80635ms ~ 81211ms (file: 361-603-325-312-612-163-162-126-136-570-675-631-632-546-356-103-135-326-231-302-527-502-152-312-321-312-132-320-534-354-750-706-276-136-106-760-713-106-312-302-230-325-237-213-3_V.mp3)
  Segment 16: 82211ms ~ 92039ms (file: 715-231-750-754-756-302-107-132-267-321-570-765-726-301-731-315-301-136-231-120-321-672-261-216-216-760-137-267-106-275-276-605-617-206-321-235-275-260-260-216-301-572-216-542-5_W_stt_that's_what_won_two_years_of_the_fact_is.mp3)
  Segment 17: 93039ms ~ 100131ms (file: 127-721-726-132-726-725-127-271-712-132-217-261-267-572-103-201-102-320-123-310-310-310-132-123-517-271-216-132-123-123-157-574-547-514-510-574-721-217-627-675-216-312-216-210-2_W.mp3)
  Segment 18: 101131ms ~ 101707ms (file: 235-362-526-326-236-271-216-257-702-760-756-623-750-570-706-507-572-157 751-271-627-702-273-572-152-320-125-172-321-172-734-357-321-763-356-250-256-752-572-257-751-326-621-627-5_V.mp3)
  Segment 19: 102707ms ~ 103283ms (file: 312-132-132-321-573-756-753-317-312-316-316-315-316-326-320-326-236-265-253-215-126-213-231-236-236-326-235-235-321-316-132-136-312-513-563-652-126-351-235-236-216-231-216-265-2_W.mp3)
  Segment 20: 104283ms ~ 104859ms (file: 675-657-657 576-576-576-576-756-576-576-576-576-567-576-576-576-576-576-576 657-576-576-576-576-576-576-576-576-576-576-567-576-576-567-657-576-657 576-576-576-576-576-765-756-5_V.mp3)
  Segment 21: 105859ms ~ 106435ms (file: 731-371-173-716-765-713-730-701-761-735-752-637-635-706-312-316-137-176-176-137-137-706-612-123-705-126-217-132-317-752-765-716-752-175-623-765-712-517-617-267-732-572-257-725-5_V.mp3)
  Segment 22: 107435ms ~ 108011ms (file: 507-123-163-756-237-512-617-126-756-572-157-316-502-750-726-271-217-702-127-267-573-205-175-725-572-572-753-127-237-125-237-572-527-276-573-517-572-237-706-570-627-375-173-572-2_V.mp3)
  Segment 23: 109011ms ~ 114087ms (file: 731-271-572-275-310-175-107-576-267-726-275-612-750-751-756-657-260-312-130-207-260-137-217-173-106-176-317-371-123-571-103-103-321-753-217-270-215-207-213-375-735-137-765-572-7_W.mp3)
  Segment 24: 115087ms ~ 115663ms (file: 160 576-651-567-715-765-706-176-621-671-156-165-657-652-651-156-630-362-603-326-620-623-621-635-310-756-570-756-175-705-710-327-123-136-627-163-176-163-136-106-106-213-671-103-1_V.mp3)
  Segment 25: 116663ms ~ 128435ms (file: 230-320-312-230-201-103-134-571-103-103-130-312-132-103-132-103-206-102-103-735-103-102-203-316-132-230-107-106-216-321-103-312-306-236-310-176-267-306-136-317-316-176-106-236-6_V.mp3)
  Segment 26: 129435ms ~ 130011ms (file: 261-632-216-132-206-567-570-574-310-162-367-753-162-126-120-106-102-162-106-137-163-610-610-153-612-326-103-530-357-521-126-162-102-576-312-163-162-103-106-106-261-321-326-301-1_V.mp3)
  Segment 27: 131011ms ~ 131587ms (file: 105-157-157-571-157-175-175-156-157-715-156-157-157-657-517-615-157-175-175-175-750-750-750-705-756-750-510-150-105-150-105-150-517-157-175-157-176-175-165-157-157-517-157-175-5_W.mp3)
  Segment 28: 132587ms ~ 133163ms (file: 526-257-750-527-725-726-572-527-527-526-257 570-502-527-521-270-520-507-752-257-276-276-276-267-267-276-275-725-752-157-512-513-512-516-156-156-517 572-507-576-572-502-520-257-2_V.mp3)
  Segment 29: 134163ms ~ 134739ms (file: 651-567-657-651-654-657-567-561-567-657-657-657 265_W.mp3)
  Segment 30: 135739ms ~ 136315ms (file: 576 501-501-507-510-510-510-517-507-510-501-507-501-507-507_V.mp3)
  Segment 31: 137315ms ~ 137891ms (file: 672-736-672-675-736-754-376-371-371-137-136-317-316-752-237-267-726-176-357-127-327-213-726-756-756-756-657-265-621-651-625-672-267-267-267-321-672-627-271-627-362-163-163-526-5_W.mp3)
  Segment 32: 138891ms ~ 139467ms (file: 507-507-501-570-570-571-576-576-576-507-507-576 573-571-501-570-576-576-517-156-150-107-501_V.mp3)
  Segment 33: 140467ms ~ 141043ms (file: 130-105-135-135-135-135-135-153-153-315-157-153-517-157-571-157-157-175-175-157-175 150-153-135-361-135-310-135-135-135-513-617-156-517-157-157-157-157-175-157-175-175-175-137-1_V.mp3)
  Segment 34: 142043ms ~ 142619ms (file: 105-154 105-157-157-105-107-105-517-510-157-510-157-157-157-105 103-105-150-150-153-150-154-150-150-105-153-513-150-105-156-574-157-507 103-105-150-105-150-105-105-105-150-156-5_W.mp3)
  Segment 35: 143619ms ~ 144195ms (file: 546-652-657-526-315-351-135-163-136-163-631-675-765-762-765-756-725-675-576-675-652-762-675-216-260-625-756-576-576-567-657-756-567-756-675-671-675-657-675-657-675-657-675-672-6_W.mp3)
  Segment 36: 145195ms ~ 145771ms (file: 572-576-572-753-675-657-675 523-257-572-572-572-572-752-574-572-572-572-752-571-752-572-752-572 576-567-752-527-527-527_W.mp3)
  Segment 37: 146771ms ~ 147347ms (file: 301-310-136-310-301-316-103-310-310-103-130-327-236-623-206-201-126-261-261-236-203-216-236-612-567-132-761-126-674-213-230-547-537-301-157-306-301-301-310-236-576-126-367-316-3_V.mp3)
  Segment 38: 148347ms ~ 148923ms (file: 153-513-517-157-513-153-130-671-167-130-106-106-137-317-176-162-136-167-165-136-102-103-136-130-130-301-132-130-103-103-637-162-157-173-103-163-103-163-106-167-160-103-106-136-1_V.mp3)
  Segment 39: 149923ms ~ 154387ms (file: 123-152-201-102-126-126-267-267-176-326-263-263-162-236-623-621-126-126-621-367-623-267-627-263-765-237-132-327-362-632-236-617-260-261-312-263-263-216-256-206-362-321-103-123-6_W.mp3)
  Segment 40: 155387ms ~ 155963ms (file: 263-623-321-236-326-263-263-621-261-623-132-231-213-321-123-123-625-267-263-627-657-576-567-265-267-627 672-627-627-267-652-625-625-267-312-632-612-621-162-312-621-162-162-163-1_W.mp3)
  Segment 41: 156963ms ~ 164631ms (file: 216-574-756-206-203-230-267-203-231-206-260-203-132-103-170-627-263-206-236-230-320-276-413-167-102-301-320-106-312-260-260-206-206-213-260-236-103-263-267-312-310-260-120 216-5_W.mp3)
  Segment 42: 165631ms ~ 166207ms (file: 367-657-356-635-657-563-365 315-135-153-517-715-756-576-756-675-751-571-576-573-576-517-103-725-715-375-637-657-361-653-365-763-631-613-637 153-571-571-571-753-751-751-571-751-7_W.mp3)
  Segment 43: 167207ms ~ 169727ms (file: 574-751-571-751-571-571 527-526-260-572-527-752-756-576-756-754-571-571-571-574-574-571-571-574-517-571-170-301-315 256-502-650-205-250-752-750-750-750-576-570-751-570-754-570-7_V.mp3)
  Segment 44: 170727ms ~ 171303ms (file: 736-756-357-357 312-321-302-301-371-173-371-176-716-731-103-613-613-610-610-610-617-673-617-613 413-312 130-310-732-763-675-371-763-715 132-126-620-762-673-756-765-265-765-130-1_W.mp3)
  Segment 45: 172303ms ~ 172879ms (file: 165-371-362-613-671-613-612-637-627-617-635-251 627_W.mp3)
  Segment 46: 173879ms ~ 174455ms (file: 175-172-725-715-175-716-715-175-176-176-152-125-213-216-261-261-621-265-263-265-265-265-265-625-265-265-265-623-625-625-625-265-625-625-265-623-625-621-127-127-172-172-172-126-1_W.mp3)
  Segment 47: 175455ms ~ 176031ms (file: 310-613-106-216-136-162-627-217-162-312-261-106-163-106-130-167-316-617-163-613-160-136-610-632-132-102-321-136-316-103-312-163-617-160-163-162-106-167-617-136-310-103-567-367-1_V.mp3)
  Segment 48: 177031ms ~ 177607ms (file: 731-765-763-763-765-762-762-762-763-762-762-376-763-675-762-763-762-736-763-675-367-376-371-672-367-713-167-176-750-750-706-710-103-765-765-765-765-762-726-276-726-762-762-627-7_W.mp3)
  Segment 49: 178607ms ~ 179183ms (file: 230-320-302-753-175-130-132-203-267-326-203-320-325-130-103-136-206-130-173-103-132-230-321-320-103-132-765-756-172-103-137-173-107-137-675-673-137-713-137-107-357-365-362-137-3_W.mp3)
  Segment 50: 180183ms ~ 180759ms (file: 537-576-576-573-573-576-573-761-176-376-173-153-135-135-157-163-165-135-153-712-723-725-523-751-752-756-751-175-752-573-372-753-573-753-125-275-715-175-123-175-327-275-731-173-3_W.mp3)
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
> **Time**: 2026-04-05 10:57:25
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "No, you're speaking for the masses, so you're good.",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 321,
"audio_type": "speech",
"words": [
{"word": "No,", "start_ms": 321, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "you're", "start_ms": 914, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "speaking", "start_ms": 1181, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "for", "start_ms": 1448, "dur_ms": 206, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 1654, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "masses,", "start_ms": 1917, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 2474, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "you're", "start_ms": 2710, "dur_ms": 725, "phase_lr_deg": 0.0},
{"word": "good.", "start_ms": 3435, "dur_ms": 263, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "말도 안 되고, 질도 안 좋아. 반대로 어떤 종목은 하루에 1, 2% 움직이면서 꾸역꾸역 우상향하는 종목들이 있어.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 145,
"audio_type": "speech",
"words": [
{"word": "말도", "start_ms": 145, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 375, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "되고,", "start_ms": 696, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "질도", "start_ms": 798, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 901, "dur_ms": 521, "phase_lr_deg": 0.0},
{"word": "좋아.", "start_ms": 1422, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "반대로", "start_ms": 1525, "dur_ms": 104, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1629, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "종목은", "start_ms": 1732, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "하루에", "start_ms": 1962, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "1,", "start_ms": 2065, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "2%", "start_ms": 2377, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "움직이면서", "start_ms": 2480, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "꾸역꾸역", "start_ms": 2582, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "우상향하는", "start_ms": 2685, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "종목들이", "start_ms": 2888, "dur_ms": 123, "phase_lr_deg": 0.0},
{"word": "있어.", "start_ms": 3011, "dur_ms": 102, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "Well, I actually think it's more beneficial to be older in today's society that's like conquered by AI because you experienced a life without AI.",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 547,
"audio_type": "speech",
"words": [
{"word": "Well,", "start_ms": 547, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 867, "dur_ms": 273, "phase_lr_deg": 0.0},
{"word": "actually", "start_ms": 1140, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 1370, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "it's", "start_ms": 1573, "dur_ms": 157, "phase_lr_deg": 0.0},
{"word": "more", "start_ms": 1730, "dur_ms": 270, "phase_lr_deg": 0.0},
{"word": "beneficial", "start_ms": 2000, "dur_ms": 270, "phase_lr_deg": 0.0},
{"word": "to", "start_ms": 2270, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "be", "start_ms": 3026, "dur_ms": 574, "phase_lr_deg": 0.0},
{"word": "older", "start_ms": 3600, "dur_ms": 105, "phase_lr_deg": 0.0},
{"word": "in", "start_ms": 3705, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "today's", "start_ms": 4470, "dur_ms": 265, "phase_lr_deg": 0.0},
{"word": "society", "start_ms": 4735, "dur_ms": 716, "phase_lr_deg": 0.0},
{"word": "that's", "start_ms": 5451, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "like", "start_ms": 5554, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "conquered", "start_ms": 5691, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "by", "start_ms": 5828, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "AI", "start_ms": 5935, "dur_ms": 754, "phase_lr_deg": 0.0},
{"word": "because", "start_ms": 6689, "dur_ms": 257, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 6946, "dur_ms": 205, "phase_lr_deg": 0.0},
{"word": "experienced", "start_ms": 7151, "dur_ms": 572, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 7723, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "life", "start_ms": 8024, "dur_ms": 302, "phase_lr_deg": 0.0},
{"word": "without", "start_ms": 8326, "dur_ms": 276, "phase_lr_deg": 0.0},
{"word": "AI.", "start_ms": 8602, "dur_ms": 276, "phase_lr_deg": 0.0}
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
"transcript": "근데 이런 걸 이제 성격 급한 사람들이 들고 있으면은 속 터져서 못 한다는 거야.",
"speaker": "speaker_1",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 210,
"audio_type": "speech",
"words": [
{"word": "근데", "start_ms": 210, "dur_ms": 237, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 447, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "걸", "start_ms": 657, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 789, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "성격", "start_ms": 919, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "급한", "start_ms": 1022, "dur_ms": 172, "phase_lr_deg": 0.0},
{"word": "사람들이", "start_ms": 1194, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "들고", "start_ms": 1404, "dur_ms": 302, "phase_lr_deg": 0.0},
{"word": "있으면은", "start_ms": 1706, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "속", "start_ms": 1916, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "터져서", "start_ms": 2237, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 2558, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "한다는", "start_ms": 2660, "dur_ms": 301, "phase_lr_deg": 0.0},
{"word": "거야.", "start_ms": 2961, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "어 왜 이렇게 안 가? 하고 팔아버리면 그다음 날 또 오르고. 그런 거겠지.",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 213,
"audio_type": "speech",
"words": [
{"word": "어", "start_ms": 213, "dur_ms": 621, "phase_lr_deg": 0.0},
{"word": "왜", "start_ms": 834, "dur_ms": 612, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 1446, "dur_ms": 316, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1762, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "가?", "start_ms": 2088, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 2400, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "팔아버리면", "start_ms": 2532, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "그다음", "start_ms": 2762, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "날", "start_ms": 3025, "dur_ms": 362, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 3387, "dur_ms": 216, "phase_lr_deg": 0.0},
{"word": "오르고.", "start_ms": 3603, "dur_ms": 367, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 3970, "dur_ms": 637, "phase_lr_deg": 0.0},
{"word": "거겠지.", "start_ms": 4607, "dur_ms": 213, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "나도 성격이 엄청 급한 성격이 아니다 보니까 내 유튜브 보는 사람들은 이제 기본 1.5배속 해야 되잖아.",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "나도", "start_ms": 120, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "성격이", "start_ms": 432, "dur_ms": 201, "phase_lr_deg": 0.0},
{"word": "엄청", "start_ms": 633, "dur_ms": 206, "phase_lr_deg": 0.0},
{"word": "급한", "start_ms": 839, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "성격이", "start_ms": 1099, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "아니다", "start_ms": 1201, "dur_ms": 627, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 1828, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "내", "start_ms": 1954, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "유튜브", "start_ms": 2704, "dur_ms": 317, "phase_lr_deg": 0.0},
{"word": "보는", "start_ms": 3021, "dur_ms": 571, "phase_lr_deg": 0.0},
{"word": "사람들은", "start_ms": 3592, "dur_ms": 302, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 3894, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "기본", "start_ms": 4154, "dur_ms": 576, "phase_lr_deg": 0.0},
{"word": "1.5배속", "start_ms": 4730, "dur_ms": 260, "phase_lr_deg": 0.0},
{"word": "해야", "start_ms": 4990, "dur_ms": 215, "phase_lr_deg": 0.0},
{"word": "되잖아.", "start_ms": 5205, "dur_ms": 130, "phase_lr_deg": 0.0}
]
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
"transcript": "그러니까 나는 막 코스닥을 하는 것보다는 그냥 꾸역꾸역 하루에...",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 574,
"audio_type": "speech",
"words": [
{"word": "그러니까", "start_ms": 574, "dur_ms": 576, "phase_lr_deg": 0.0},
{"word": "나는", "start_ms": 1150, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "막", "start_ms": 1417, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "코스닥을", "start_ms": 1524, "dur_ms": 176, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 1700, "dur_ms": 103, "phase_lr_deg": 0.0},
{"word": "것보다는", "start_ms": 1803, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 2568, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "꾸역꾸역", "start_ms": 3333, "dur_ms": 765, "phase_lr_deg": 0.0},
{"word": "하루에...", "start_ms": 4098, "dur_ms": 675, "phase_lr_deg": 0.0}
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
"transcript": "때는 계속 피드백해 주시면서 혼내주시고 하다 보니까 이제 등산의 위대함이라기보다...",
"speaker": "speaker_3",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 756,
"audio_type": "speech",
"words": [
{"word": "때는", "start_ms": 756, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 1512, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "피드백해", "start_ms": 1779, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "주시면서", "start_ms": 2046, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "혼내주시고", "start_ms": 2802, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "하다", "start_ms": 2928, "dur_ms": 167, "phase_lr_deg": 0.0},
{"word": "보니까", "start_ms": 3095, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 3851, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "등산의", "start_ms": 4607, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "위대함이라기보다...", "start_ms": 4874, "dur_ms": 320, "phase_lr_deg": 0.0}
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
"transcript": "수급 단타 스승님과 함께하는 등산은 어떻게 가치로 표현...",
"speaker": "speaker_3",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 715,
"audio_type": "speech",
"words": [
{"word": "수급", "start_ms": 715, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "단타", "start_ms": 946, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "스승님과", "start_ms": 1696, "dur_ms": 754, "phase_lr_deg": 0.0},
{"word": "함께하는", "start_ms": 2450, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "등산은", "start_ms": 3206, "dur_ms": 302, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 3508, "dur_ms": 107, "phase_lr_deg": 0.0},
{"word": "가치로", "start_ms": 3615, "dur_ms": 132, "phase_lr_deg": 0.0},
{"word": "표현...", "start_ms": 3747, "dur_ms": 267, "phase_lr_deg": 0.0}
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
"transcript": "빚날 정도로 꼼꼼하게 닦아냅니다.",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 731,
"audio_type": "speech",
"words": [
{"word": "빚날", "start_ms": 731, "dur_ms": 271, "phase_lr_deg": 0.0},
{"word": "정도로", "start_ms": 1002, "dur_ms": 572, "phase_lr_deg": 0.0},
{"word": "꼼꼼하게", "start_ms": 1574, "dur_ms": 275, "phase_lr_deg": 0.0},
{"word": "닦아냅니다.", "start_ms": 1849, "dur_ms": 310, "phase_lr_deg": 0.0}
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
"transcript": "튀기는 데 사용되는 기름 양...",
"speaker": "speaker_4",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 230,
"audio_type": "speech",
"words": [
{"word": "튀기는", "start_ms": 230, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 550, "dur_ms": 312, "phase_lr_deg": 0.0},
{"word": "사용되는", "start_ms": 862, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "기름", "start_ms": 1092, "dur_ms": 201, "phase_lr_deg": 0.0},
{"word": "양...", "start_ms": 1293, "dur_ms": 103, "phase_lr_deg": 0.0}
]
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
"transcript": "말 그대로 흥행 보증 수표 같은 공식이었던 거죠.",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 105,
"audio_type": "speech",
"words": [
{"word": "말", "start_ms": 105, "dur_ms": 157, "phase_lr_deg": 0.0},
{"word": "그대로", "start_ms": 262, "dur_ms": 157, "phase_lr_deg": 0.0},
{"word": "흥행", "start_ms": 419, "dur_ms": 571, "phase_lr_deg": 0.0},
{"word": "보증", "start_ms": 990, "dur_ms": 157, "phase_lr_deg": 0.0},
{"word": "수표", "start_ms": 1147, "dur_ms": 175, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 1322, "dur_ms": 175, "phase_lr_deg": 0.0},
{"word": "공식이었던", "start_ms": 1497, "dur_ms": 156, "phase_lr_deg": 0.0},
{"word": "거죠.", "start_ms": 1653, "dur_ms": 157, "phase_lr_deg": 0.0}
]
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
"transcript": "물론 원작 팬들 사이에서는 원작의 감정을 못 살렸다는 비판이 계속 있었지만...",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 672,
"audio_type": "speech",
"words": [
{"word": "물론", "start_ms": 672, "dur_ms": 736, "phase_lr_deg": 0.0},
{"word": "원작", "start_ms": 1408, "dur_ms": 672, "phase_lr_deg": 0.0},
{"word": "팬들", "start_ms": 2080, "dur_ms": 675, "phase_lr_deg": 0.0},
{"word": "사이에서는", "start_ms": 2755, "dur_ms": 736, "phase_lr_deg": 0.0},
{"word": "원작의", "start_ms": 3491, "dur_ms": 754, "phase_lr_deg": 0.0},
{"word": "감정을", "start_ms": 4245, "dur_ms": 376, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 4621, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "살렸다는", "start_ms": 4992, "dur_ms": 371, "phase_lr_deg": 0.0},
{"word": "비판이", "start_ms": 5363, "dur_ms": 137, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 5500, "dur_ms": 136, "phase_lr_deg": 0.0},
{"word": "있었지만...", "start_ms": 5636, "dur_ms": 317, "phase_lr_deg": 0.0}
]
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
"transcript": "한국 드라마 리메이크라는 타이틀 자체가 일본...",
"speaker": "speaker_5",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 546,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 546, "dur_ms": 652, "phase_lr_deg": 0.0},
{"word": "드라마", "start_ms": 1198, "dur_ms": 657, "phase_lr_deg": 0.0},
{"word": "리메이크라는", "start_ms": 1855, "dur_ms": 526, "phase_lr_deg": 0.0},
{"word": "타이틀", "start_ms": 2381, "dur_ms": 315, "phase_lr_deg": 0.0},
{"word": "자체가", "start_ms": 2696, "dur_ms": 351, "phase_lr_deg": 0.0},
{"word": "일본...", "start_ms": 3047, "dur_ms": 135, "phase_lr_deg": 0.0}
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
"transcript": "주민이 전폭 지지하는 후보를 뽑아달라고 호소했지만 정치적 타격만이 입었습니다.",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 123,
"audio_type": "speech",
"words": [
{"word": "주민이", "start_ms": 123, "dur_ms": 152, "phase_lr_deg": 0.0},
{"word": "전폭", "start_ms": 275, "dur_ms": 201, "phase_lr_deg": 0.0},
{"word": "지지하는", "start_ms": 476, "dur_ms": 102, "phase_lr_deg": 0.0},
{"word": "후보를", "start_ms": 578, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "뽑아달라고", "start_ms": 704, "dur_ms": 126, "phase_lr_deg": 0.0},
{"word": "호소했지만", "start_ms": 830, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "정치적", "start_ms": 1097, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "타격만이", "start_ms": 1364, "dur_ms": 176, "phase_lr_deg": 0.0},
{"word": "입었습니다.", "start_ms": 1540, "dur_ms": 326, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "민주당 당선자는 초현실적이라는 말로 승리가 믿기지 않는...",
"speaker": "speaker_6",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 263,
"audio_type": "speech",
"words": [
{"word": "민주당", "start_ms": 263, "dur_ms": 623, "phase_lr_deg": 0.0},
{"word": "당선자는", "start_ms": 886, "dur_ms": 321, "phase_lr_deg": 0.0},
{"word": "초현실적이라는", "start_ms": 1207, "dur_ms": 236, "phase_lr_deg": 0.0},
{"word": "말로", "start_ms": 1443, "dur_ms": 326, "phase_lr_deg": 0.0},
{"word": "승리가", "start_ms": 1769, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "믿기지", "start_ms": 2032, "dur_ms": 263, "phase_lr_deg": 0.0},
{"word": "않는...", "start_ms": 2295, "dur_ms": 621, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "유리합니다. 많이 바꼈어요. 우리 아이들에게 제공해줄 수 있는 것들이...",
"speaker": "speaker_7",
"confidence": 0.93,
"language": "mixed",
"silence_before_ms": 216,
"audio_type": "mixed",
"words": [
{"word": "유리합니다.", "start_ms": 216, "dur_ms": 574, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 790, "dur_ms": 756, "phase_lr_deg": 0.0},
{"word": "바꼈어요.", "start_ms": 1546, "dur_ms": 206, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 1752, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "아이들에게", "start_ms": 1955, "dur_ms": 230, "phase_lr_deg": 0.0},
{"word": "제공해줄", "start_ms": 2185, "dur_ms": 267, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 2452, "dur_ms": 203, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2655, "dur_ms": 231, "phase_lr_deg": 0.0},
{"word": "것들이...", "start_ms": 2886, "dur_ms": 206, "phase_lr_deg": 0.0}
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
*Generated by WKAppBot ask gemini — 2026-04-05 10:57:25*
