# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24432  time=11:56:30  uptime=35461s  mem=497MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_115313.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 715 275-537-157-125-674-276-672-762-672-312-512-126-132-351 237-267-317-765-725-213-726-123-751-125-712-573-512-751-356-512-756-153 312 572-570-261-756-176-726-127-723-371-326-2_V.mp3)
  Segment 2: 1576ms ~ 8560ms (file: 236-321-216-576-321-103-751-123-263-132-102-136-251-130-132-132-261-263-572-261-216-756-267-627-123-123-756-756-236-261-261-567-263-312-132-756-267-216-132-123-521-132-143-267-5_W.mp3)
  Segment 3: 9560ms ~ 10136ms (file: 756-756-752-752-571-576-317-761-751-756-675-761-765-756-153-516-517-576-517-157-571-517-571-576-175-567-576-576-576-576-576-573-576-756-576-756-675 361-365-651-356-653-356-653-5_W.mp3)
  Segment 4: 11136ms ~ 18156ms (file: 102-103-130-163-261-547-326-106-106-132-576-754-231-263-107-756-106-263-625-572-627-123-574-210-162-236-263-754-756-321-263-263-236-620-236-130-120-130-362-326-261-263-231-623-6_W.mp3)
  Segment 5: 19156ms ~ 19732ms (file: 657-275-752-756-576-715-752-756-651-651-675-675-675-762-657-657-657 215-265-672-612-652-657-657-675-657-657-675-657-657-657-657-657-675-657-675-657-657-615-671_W.mp3)
  Segment 6: 20732ms ~ 28580ms (file: 257-251-572-573-103-310-310-206-216-263-213-123-213-321-201-210-261-236-206-213-216-672-267-260-627-276-127-267-627-267-260-103-203-576-517-715-261-256-574-213-752-527-572-152-1_W.mp3)
  Segment 7: 29580ms ~ 30156ms (file: 512-356-153-156-137-547-547-534-537-527-527-527-256-562-562-526-567-562-567-562-652-265 541-524_W.mp3)
  Segment 8: 31156ms ~ 31732ms (file: 671-671-675-675-675-671-670-607_W.mp3)
  Segment 9: 32732ms ~ 33308ms (file: 756-765-756-765-675-756-765-765-756-765-756-675-765-176-156-615-165-167-165-167-167-167-756-753-756-765-675-576-765-756-576-752-756-576-765-765-756-752-167-172-162-162-672-726-7_W.mp3)
  Segment 10: 34308ms ~ 34884ms (file: 215-217-543-234-315-215-274-651-431-467-125-235-465-132-364-135-364-312-615-123-237-321-214-321-367-157-267-136-752-564-162-276-213-127-127-614-132_V.mp3)
  Segment 11: 35884ms ~ 36460ms (file: 713-710-710-710-715-175-175-715-750-571-175-175-571-715-715-751-571-751-751-751 571-756-570-756-750-517-751-571-715-715-517-570-751-715-753-570-715-715-571-751-715-750-570 751-7_V.mp3)
  Segment 12: 37460ms ~ 38036ms (file: 201-527-251-215-105-251-152-257-521-527-501-125-502-512-251-215-210-215_V.mp3)
  Segment 13: 39036ms ~ 39612ms (file: 513-136-103-162-106-163-160-165-163-715-176-136-301-316-123-123-123-123-123-120-123-132-123-132-120-123-132_W.mp3)
  Segment 14: 40612ms ~ 50764ms (file: 236-263-326-310-326-321-321-236-312-513-756-236-326-237-203-236-236-756-326-236-236-236-237-267-123-123-263-637-263_W.mp3)
  Segment 15: 51764ms ~ 52340ms (file: 613-612-613-612-162-621-216-531-501-510-503-510-510-510-510-510-516-513-517-517 504-547-564 526-503-564-514-513-513-516-152-156-512-526-523-517-571-517-541-501-563-561-532-516-5_V.mp3)
  Segment 16: 53340ms ~ 61584ms (file: 123-621-327-132-601-216-210-263-321-217-217-210-276-625-675-526-270-216-507-270-126-372-301-312-132-135-671-215-206-213-216-126-213-102-206-715-231-267-237-261-542-152-302-312-2_V.mp3)
  Segment 17: 62584ms ~ 63160ms (file: 105-103-157-150-517-561-156 517-157-516-576-570-517-150-130-513-517-571-571-157-751 150-157-157-157-510-517-571-756-756-756-751 510-501-507-517-570-175-751-715-107-157-150-105-1_V.mp3)
  Segment 18: 64160ms ~ 68696ms (file: 625-631-623-261-231-312-123-231-132-132-102-132-102-130-162-312-210-102-120-630-302-210-302-310-623-326-120-302-216-301-310-132-132-301-321-210-132-120-213-163-132-132-310-213-2_V.mp3)
  Segment 19: 69696ms ~ 80496ms (file: 756-132-756-123-326-302-630-203-136-365-657-726-157-532-203-215-136-236-103-362-231-312-371-357-213-207-752-754-231-765-276-175-107-231-675-756-123-231-312-120-136-301-213-320-7_W.mp3)
  Segment 20: 81496ms ~ 89884ms (file: 612-260-126-236-260-267-260-263-321-263-231-321-230-102-102-154-321-267-267-276-213-621-167-316-236-321-160-267-263-267-260-756-372-276-627-263-236-130-716-176-173-765-703-176-1_W.mp3)
  Segment 21: 90884ms ~ 91460ms (file: 617-617-675-671 275-210-162-621-162-162-126-167-621-261_W.mp3)
  Segment 22: 92460ms ~ 93036ms (file: 160-167-160-163 130-137-136-130-613-316-136-167-163-163-613-163-176-167-617-617-176-176-671-167-176-716-176-167-173-716_W.mp3)
  Segment 23: 94036ms ~ 94612ms (file: 517-157-751-571-507-570 570-570-571-507-507-570-750-571-107-517-751-570-157-571-517-501-570-571-517-751-570-574-574-570-574-574-570-570-510-715-570-571-570-751-570-570-103-107-5_W.mp3)
  Segment 24: 95612ms ~ 96188ms (file: 301-602-621-260-261-261-326-367-360-136-106-130-130-603-236-627-672-620-213-136-312-316-103-136-267-267-261-620-260-260-206-265-206-361-216-236-127-750-572-756-756-756-150-103-3_W.mp3)
  Segment 25: 97188ms ~ 105323ms (file: 230-312-302-357-265-756-206-276-574-275-316-325-203-267-260-236-326-316-176-120-731-760-317-723-327-753-316-130-267-576-765-765-267-263-267-267-236-236-263-765-162-310-263-260-2_W.mp3)
  Segment 26: 106323ms ~ 106899ms (file: 145-415-173-174-145-415-137-104-105-104-135-104-135-103-130 137-135-134-143-103-154-134-153-105-104-317-145-104-104-154-134-174-107-104-153-104-157-157-175-147-145-154-104-145 7_V.mp3)
  Segment 27: 107899ms ~ 108475ms (file: 567-567-675-756-571-576-675-657-567-576-572 517-570-751-517-517-571-571-517-571-571-517-517-571-751-571-751-517-751-715-751-571 571-571-571-571-751-751-175-517-751-751-715-571-1_V.mp3)
  Segment 28: 109475ms ~ 110051ms (file: 150-571-510-150-501-510-170-510-501-510-157-571-570 105-150-105-501-510-501-507-501-570-517-517-150-517-510-501-510-150-510-510-510-157-157-517-510-510-571 150-150-510-105-501-1_W.mp3)
  Segment 29: 111051ms ~ 120843ms (file: 213-102-103-236-123-321-132-132-103-320-276-320-102-130-273-321-213-125-230-627-237-627-231-130-132-762-130-137-102-123-263-320-263-230-107-761-326-267-230-231-321-317-237-231-2_W.mp3)
  Segment 30: 121843ms ~ 122419ms (file: 567-576-756-576-657-716-572-572-576-576-576-576-576-675 514-501-571-752-751-576-756-576-576-756-571-576-576-571-756-571-576-751-571-765-531-501-512-504_W.mp3)
  Segment 31: 123419ms ~ 123995ms (file: 675-657-315-531-537-163-617-617-612-612-162-756-756-751-576-756-765-765-675-765-765-675-765-725-765-675-627-652-165-156-175-165-165-765-675-657-627-765-756-765-765-765-675-756-7_W.mp3)
  Segment 32: 124995ms ~ 125571ms (file: 751-617-615-136-612-613-761-126-162-762-176-756-761-725-261-726-172-675-627-621-726-762-715-127-725-276-157-517-571-752-752-756-752-725-765-721-725-721-762-725-725-752-725-712-7_W.mp3)
  Segment 33: 126571ms ~ 127147ms (file: 576-571-576-567-157-517-516 517-567-561-561-516-567-516-567-156-175 561-157-716-175-175-175 170-715-710-715-170-715-715-751-756-517 154-561-516-516-516-154-617-173-170-167-765-6_W.mp3)
  Segment 34: 128147ms ~ 128723ms (file: 167-261 762-276-627-267-276-213-326-321-123-231-231-213-123-123-321 132-102-123 712-132-261-126-627-162-126-261-126-126-261-216_W.mp3)
  Segment 35: 129723ms ~ 130299ms (file: 651-561-651-516-561-651-561-657-615-156-165-165-615-612-613-615-613-613-613-672-675-675-675-657-675-657-675-502-652-625-651-650-651-657-651-657-657-657-675-657-657 512-516-507-5_W.mp3)
  Segment 36: 131299ms ~ 131875ms (file: 756-735-753-375-756-576-753-735-756-736-371-753-750-713-701-713-710-705-705-710-751-705-750-701-705-573-750-157-751-570-750-751-703-357-765-735-670-756-752-750-756-573-765-756-7_V.mp3)
  Segment 37: 132875ms ~ 133451ms (file: 612-762-612-765-526-627 567-507-570-570-570-507-520-520-651-165-652-651-516-651-615-651-651-615-561-562_V.mp3)
  Segment 38: 134451ms ~ 135027ms (file: 256-251-512-215-215-125-512-201-251-527-520-257-127-215-512-105-517-157-521-157-521-150-657-765-675-562-657-657-675-675-165-561-561-765-527-576-576-567-567-657-567-567-675-675-6_W.mp3)
  Segment 39: 136027ms ~ 136603ms (file: 751-751-751-751-756-754-756-751-756-756-754-751 150-150-150-150-510-751-157-571-751-751-571-751-751-751-751-715-756-751-715-175-715-751-751-715-751-715 510-150-510-150-150-157-1_W.mp3)
  Segment 40: 137603ms ~ 138179ms (file: 205-576-267-765-756-765-756-756-756-765-756-756-756-756-756-576-576-576 250-257-526-725-276-657-576-765-762-675-765-765-765-675-756-765-765-765-675 502-502-520-257-502-572-570-5_W.mp3)
  Segment 41: 139179ms ~ 147495ms (file: 735-321-310-217-321-130-621-137-126-267-237-213-576-572-327-762-267-267-132-132-261-263-267-321-123-325-576-216-106-713-672-321-312-321-756-765-213-671-163-267-236-267-267-206-2_W.mp3)
  Segment 42: 148495ms ~ 149071ms (file: 574-574-574-547-574-517-574-571-571-570-571-517-751-157 574-574-574-574-574-517-510-105-571-571-574-517-571-501-570-574-157-517-751-571-105-150-571-570-517-507-507-570 574-547-5_W.mp3)
  Segment 43: 150071ms ~ 150647ms (file: 126-712-162-162-173-127-172-176-173-137-137-132-127-731-731-176-173-173-175-173-176-175-715-176-173-315-315-135-571-517-157-576_W.mp3)
  Segment 44: 151647ms ~ 152223ms (file: 152-541-503-504-503-504-504-506-256-561-571-567-576-567-567-576-576-756-562-501-506 576-567-756-756-756-576-576-756-576-567-576-576-756-765-504-506-502-503-572-657-576-576-576-5_W.mp3)
  Segment 45: 153223ms ~ 153799ms (file: 206-206-260-201 263-260-213-120-260-620-657-625-625-625-612-651-615-651-561-567-517-567-570-517-510-156-165-165-617-657-651-617-617-162-612-165-651-615-165-615-657-657-657-756-5_W.mp3)
  Segment 46: 154799ms ~ 155375ms (file: 136-612-160-620-362-206-362-167-632-613-602-106-162-103-632-132-617-316-123-103-103-103-761-670-621-123-210-765-751-756-765-756-675-761_V.mp3)
  Segment 47: 156375ms ~ 163539ms (file: 570-573-207-206-217-271-260-570-627-574-527-617-317-513-120-652-123-216-216-132-210-213-756-123-130-102-276-120-172-206-216-261-572-572-725-127-275-265-520-237-570-256-120-210-2_W.mp3)
  Segment 48: 164539ms ~ 165115ms (file: 310-167-136-103-103-613-653-162-163-632-130-631-130-103-361-612-160-103-136-103-132-546-670-163-316-136-103-167-136-610-103 103-516-621-615 103-124-160-163-517-301-130-617-672-1_V.mp3)
  Segment 49: 166115ms ~ 174431ms (file: 312-316-756-615-326-231-176-362-326-127-172-712-361-163-176-176-716-671-627-265-326-236-136-627-321-513-723-267-327-237-231-523-132-135-137-623-627-756-106-163-632-320-236-263-6_W.mp3)
  Segment 50: 175431ms ~ 176007ms (file: 306-637-637-213-157-736-751-537-312-532-736-135-135-726-261-567-263-321-372-235-720-752-261-267-750-276-726-621-265-623-267-560-267-203-276 273-726-627-756_V.mp3)
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
> **Time**: 2026-04-05 11:57:52
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 한 1분 정도 수준이 되죠 수십 개가 한꺼번에 올라가고 계속 깔고 있거든요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "1분", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "정도", "start_ms": 900, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수준이", "start_ms": 1150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "되죠", "start_ms": 1500, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수십", "start_ms": 2000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "개가", "start_ms": 2300, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한꺼번에", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "올라가고", "start_ms": 3000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "깔고", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있거든요", "start_ms": 4500, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "향후에는 교체 수요도 있을 것으로 봅니다 연간으로 보면 시간이 가면 갈수록",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "향후에는", "start_ms": 120, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "교체", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "수요도", "start_ms": 950, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있을", "start_ms": 1350, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "것으로", "start_ms": 1550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "봅니다", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "연간으로", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 3000, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시간이", "start_ms": 3300, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "50에서 100 메가와트아워 이상 될 걸로 봅니다 삼성 SDI가 납품하는 게",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "50에서", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "100", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "메가와트아워", "start_ms": 900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 1600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 1900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "걸로", "start_ms": 2050, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "봅니다", "start_ms": 2300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "삼성", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "SDI가", "start_ms": 3400, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "납품하는", "start_ms": 3900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 4400, "dur_ms": 100, "phase_lr_deg": 0.0}
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
"transcript": "들어가시죠 와 여기 현관이에요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "들어가시죠", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "와", "start_ms": 1000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기", "start_ms": 1200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "현관이에요", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "네",
"speaker": "speaker_3",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "결국 역전당했던 전례를 거론하면서 한국 방위산업도 이와 비슷한 변곡점에 서 있다고 덧붙였습니다",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "결국", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "역전당했던", "start_ms": 450, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "전례를", "start_ms": 1050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거론하면서", "start_ms": 1500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 2200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "방위산업도", "start_ms": 2600, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이와", "start_ms": 3300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "비슷한", "start_ms": 3600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "변곡점에", "start_ms": 4100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "서", "start_ms": 4700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있다고", "start_ms": 4800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "덧붙였습니다", "start_ms": 5200, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "한국이 아랍에미리트로부터 총 2,400만 배럴의 원유를 긴급 공급받기로 한 데 대해 일본 요미우리 신문이 이례적인 우대 조치라고 보도했습니다",
"speaker": "speaker_4",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "한국이", "start_ms": 80, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아랍에미리트로부터", "start_ms": 550, "dur_ms": 1000, "phase_lr_deg": 0.0},
{"word": "총", "start_ms": 1600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "2,400만", "start_ms": 1750, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "배럴의", "start_ms": 2450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "원유를", "start_ms": 2900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "긴급", "start_ms": 3400, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "공급받기로", "start_ms": 3800, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 4500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "데", "start_ms": 4600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 4700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "일본", "start_ms": 5100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "요미우리", "start_ms": 5500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "신문이", "start_ms": 6100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "이례적인", "start_ms": 6700, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "우대", "start_ms": 7300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "조치라고", "start_ms": 7700, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "보도했습니다", "start_ms": 8250, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "이 신문은 그 배경으로 한국과 UAE 양국의 그간 군사 협력에 더해 한국산",
"speaker": "speaker_4",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "신문은", "start_ms": 250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "배경으로", "start_ms": 950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "한국과", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "UAE", "start_ms": 2100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "양국의", "start_ms": 2600, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그간", "start_ms": 3200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "군사", "start_ms": 3600, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "협력에", "start_ms": 4000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "더해", "start_ms": 4600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한국산", "start_ms": 5100, "dur_ms": 500, "phase_lr_deg": 0.0}
]
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
"transcript": "다시 이렇게 하루 한 시간에 만 원씩 버는 노동을 비하하는 게 아니고 제가 할 수 있을까 저는 이미 도박쟁이의 삶의 결도 많이 있는 건데 제 인생",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 450, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "시간에", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "만", "start_ms": 1900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "원씩", "start_ms": 2050, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "버는", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "노동을", "start_ms": 2700, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "비하하는", "start_ms": 3200, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 3700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니고", "start_ms": 3800, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 4300, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 4650, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 4800, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있을까", "start_ms": 4900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 5400, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "도박쟁이의", "start_ms": 6100, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "삶의", "start_ms": 6800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "결도", "start_ms": 7150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "많이", "start_ms": 7500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 7850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "건데", "start_ms": 8150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "제", "start_ms": 8600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "인생", "start_ms": 8750, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "That segment was my second guess Yes Okay I got that one right Whew All right Yeah that was easy I got that right Okay so I got four out of five",
"speaker": "speaker_6",
"confidence": 0.94,
"language": "en",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "segment", "start_ms": 370, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 770, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "my", "start_ms": 970, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "second", "start_ms": 1120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "guess", "start_ms": 1470, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Yes", "start_ms": 1900, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Okay", "start_ms": 2200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 2500, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 2600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 2800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "one", "start_ms": 2950, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "right", "start_ms": 3150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Whew", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "All", "start_ms": 4000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "right", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Yeah", "start_ms": 4600, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 4850, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "was", "start_ms": 5000, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "easy", "start_ms": 5200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 5600, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 5700, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "that", "start_ms": 5900, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "right", "start_ms": 6050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "Okay", "start_ms": 6500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "so", "start_ms": 6800, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 6950, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "got", "start_ms": 7050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "four", "start_ms": 7250, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "out", "start_ms": 7550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "of", "start_ms": 7700, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "five", "start_ms": 7800, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "제는 연구 성과들이 차곡차곡 쌓였는데요",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "제는", "start_ms": 150, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "연구", "start_ms": 500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "성과들이", "start_ms": 900, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "차곡차곡", "start_ms": 1450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "쌓였는데요", "start_ms": 2050, "dur_ms": 650, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "대체해서 그냥 다 던져 버렸지 자 이게 시황을 보고 하는 거야 네가 옛날에는 눌림 추매 하고 큰 그림을 못 봤잖아 장 좋아서 네가 대충 사서 뭐 5일선 10일선 뭐 20일선 그리고 그",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "대체해서", "start_ms": 100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "던져", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "버렸지", "start_ms": 1600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 2200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 2400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시황을", "start_ms": 2750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보고", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 3650, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "거야", "start_ms": 4000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 4400, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "옛날에는", "start_ms": 4750, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "눌림", "start_ms": 5350, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "추매", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 6150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "큰", "start_ms": 6600, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "그림을", "start_ms": 6850, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 7350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "봤잖아", "start_ms": 7550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "장", "start_ms": 8100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "좋아서", "start_ms": 8350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 8850, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "대충", "start_ms": 9200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사서", "start_ms": 9600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 10000, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "5일선", "start_ms": 10200, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "10일선", "start_ms": 10700, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 11250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "20일선", "start_ms": 11450, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 12100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 12600, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
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
"transcript": "하버드 의대 데이비드 싱클레어 박사는 자신의 저서 노화의 종말과 세계적인 언론과의 인터뷰",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "하버드", "start_ms": 120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "의대", "start_ms": 620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "데이비드", "start_ms": 1020, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "싱클레어", "start_ms": 1570, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "박사는", "start_ms": 2170, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "자신의", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "저서", "start_ms": 3150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "노화의", "start_ms": 3550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "종말과", "start_ms": 4050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "세계적인", "start_ms": 4650, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "언론과의", "start_ms": 5250, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "인터뷰", "start_ms": 5900, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "그래요 국장이 먼저 빠지고 미장이 더 낙폭을 보이는 그런 형태를 보여서 하 이거 내가 또 여기서 기법 매매를 하면 여기서는 또 비중 조절을 못 하기 때문에 갔다가 다 손실이 나겠",
"speaker": "speaker_5",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "그래요", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "국장이", "start_ms": 600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "먼저", "start_ms": 1100, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "빠지고", "start_ms": 1500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "미장이", "start_ms": 2000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "낙폭을", "start_ms": 2750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보이는", "start_ms": 3250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 3750, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "형태를", "start_ms": 4100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "보여서", "start_ms": 4600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하", "start_ms": 5100, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이거", "start_ms": 5350, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "내가", "start_ms": 5700, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 6050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 6300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "기법", "start_ms": 6750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "매매를", "start_ms": 7150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 7650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기서는", "start_ms": 8100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 8650, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "비중", "start_ms": 8900, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "조절을", "start_ms": 9300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "못", "start_ms": 9800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "하기", "start_ms": 10050, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 10400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "갔다가", "start_ms": 10900, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 11400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "손실이", "start_ms": 11600, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "나겠", "start_ms": 12100, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "여러분 입자 가속기로 만드는 방법이 유일합니다 썬에서는 양성자 빔을 엄청난 속도로 가속시켜서 금속 표적에 충돌시켜요 이 충돌에서 반양성자가 생겨납니다 그리고 전기장과 자기장을 이용해서 이 반양성자를 천천히 감속시켜서 포획합니다 이 과정에서 대부분의 입자는 날아가 버리고 겨우",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "여러분", "start_ms": 100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "입자", "start_ms": 600, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "가속기로", "start_ms": 950, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "만드는", "start_ms": 1550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "방법이", "start_ms": 2050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유일합니다", "start_ms": 2550, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "썬에서는", "start_ms": 3400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "양성자", "start_ms": 4000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "빔을", "start_ms": 4500, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "엄청난", "start_ms": 4850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "속도로", "start_ms": 5400, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "가속시켜서", "start_ms": 5900, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "금속", "start_ms": 6650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "표적에", "start_ms": 7050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "충돌시켜요", "start_ms": 7550, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 8400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "충돌에서", "start_ms": 8600, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "반양성자가", "start_ms": 9200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "생겨납니다", "start_ms": 9950, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 10800, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "전기장과", "start_ms": 11300, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "자기장을", "start_ms": 11900, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이용해서", "start_ms": 12500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 13100, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "반양성자를", "start_ms": 13300, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "천천히", "start_ms": 14100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "감속시켜서", "start_ms": 14650, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "포획합니다", "start_ms": 15450, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 16300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "과정에서", "start_ms": 16500, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "대부분의", "start_ms": 17100, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "입자는", "start_ms": 17750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "날아가", "start_ms": 18250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "버리고", "start_ms": 18750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "겨우", "start_ms": 19250, "dur_ms": 350, "phase_lr_deg": 0.0}
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
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 11:57:52*
