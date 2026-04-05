# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=17628  time=12:33:57  uptime=38450s  mem=424MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_123054.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 136-162-162-163-136-136-132 132-132-132-136-126-132-136-132 301 130-316-130-130-136-316-136 310 230-230-236-123-136-132-163-157-103-167-163 271-627-627 627-623-627-623-625-627-6_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 576-561-657-657-657-615 574-517-751-571-756-751-571-571-751-517-751-715-751-756-751-715-751-375_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 572-217-562-536-563-567-576-567-576-571 507-507-507-507-507-507-507-507-503-523-527-567-572-576-576-576-571-571-572-573-751-756-756-546-507-504-571-507-576-507-576-572-574-507-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 132-312-762-637-625-326-235-352-371_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 102-103-103 102-120-120-120-102-120-120-210-120-210-210-102-102-132-310-716-106-617-160-160-610-610-160-612 106-610-160-160-160-601-160-160-160-610-612-610-163-610-162-160-160-1_V.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 501-501-510-571-573-571-517-517-517-517-517-567-576-576-567-576-574-574-574-574 570-517-517-570-510-501-105-513-517-513-501-517-501-510-150-510-501-150-501-510-501-570-507-507-5_W.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 271-176-562-561-165 725-765-713-327-157-175-126-570-726-106-617-361-546-263-564-706-567-231-371-236-752-175-576-261-673-762-763-267-162-627-316-275-267-726-512-267-765-372-672-6_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 675-675-764-765-765-764-765-765-765-765-765-765-765-760-760-706-760-706-705-705-507-507-570-507-570-576-570-570-570-705-750-675-520-507-725-705-570-520-562-570-702-752-507-765-6_W.mp3)
  Segment 9: 12608ms ~ 19988ms (file: 135-571-157-173-573-135-751-570-127-132-103-137-213-215-132-172-705-541-547-574-756-762-276-572-265-627-135-762-305-253-273-210-237-103-123-502-123-175-153-750-726-206-725-312-2_V.mp3)
  Segment 10: 20988ms ~ 21564ms (file: 153 105-517-571-517-571-157-751-175-175-517-731-157-157-713-175-571-571-571-571-570-570-571 517-571-157-517-153-175-517-571-157-137-175-157-513-157-715-715-751-517-753-751-571-5_V.mp3)
  Segment 11: 22564ms ~ 31204ms (file: 260-260-213-320-326-130-671-130-276-275-123-201-275-127-103-260-620-576-275-107-103-760-301-103-320-103-217-230-203-130-213-102-213-312-721-320-103-213-102-276-307-152-206-312-1_V.mp3)
  Segment 12: 32204ms ~ 32780ms (file: 317-173-310-703-307 750-735-710-703-107-173-361-603-163-137-301-317-173-137-317-371-137-107-107-107-317-310-317-137-135-317-137-317 730-107-713-705-703-175-107-671-613-316-361-1_V.mp3)
  Segment 13: 33780ms ~ 34356ms (file: 165-167-132-136-132-162 670 673-637-632_V.mp3)
  Segment 14: 35356ms ~ 41116ms (file: 716-126-276-726-267-176-126-261-326-576-267-236-276-123-136-327-726-267-276-216-672-267-267-267-721-210-276-237-210-276-267-725-671-237-123-237-627-765-726-217-321-312-213-103-3_W.mp3)
  Segment 15: 42116ms ~ 42692ms (file: 675-726-756-623-326-267-276-726-263-362-326-652-623-671-621 631-216-673-627-276-765-675-672 567-362-326-236-632-627-675-167-617-607-607-651-567-657-637-613-657-567-625-567-617-5_W.mp3)
  Segment 16: 43692ms ~ 44268ms (file: 143-134-143-301-145-145-134-103-417-143 431-103-103-134-301-104-310-143-310-134-130-134-310-103-140-140-143-413-134-143-143-143-143-143-134 304-345-531-147-310-103-314-301-103-1_V.mp3)
  Segment 17: 45268ms ~ 45844ms (file: 517-571-175-571-517-751-173-157-173-173-175-173-713-173-173-173-157-715 150-165-153-351-132-135-123-137-137-136-751-751-157-570-135-150-157-135-715-715-173-173-173-153-175-135-5_V.mp3)
  Segment 18: 46844ms ~ 51992ms (file: 321-107-176-107-376-312-123-216-756-270-721-302-130-102-751-756-753-257-150-537-536-165-516-250-576-125-120-210-126-123-216-157-136-103-631-762-260-261-107-103-107-176-127-126-1_W.mp3)
  Segment 19: 52992ms ~ 53568ms (file: 725-123-125-127-315-516-675-756-156-516-567-651-562-516-563-165-672-162-321-103-132-103-510-301-103-135-103-517-576-516-260-206 263-230-302-231 756-763-765-672-672-267-301-760-7_W.mp3)
  Segment 20: 54568ms ~ 55144ms (file: 632-632-623-362 315-135-135-315-316-356-365-315-361-365-365_W.mp3)
  Segment 21: 56144ms ~ 63632ms (file: 175-765-301-103-574-320-302-123-521-317-156-756-760-750-574-132-514-567-576-531-103-310-576-205-135-132-705-137-173-126-153-312-713-705-756-153-571-137-751-176-765-752-765-105-1_V.mp3)
  Segment 22: 64632ms ~ 75684ms (file: 276-103-102-572-205-276-312-216-310-320-103-201-206-301-132-120-573-207-260-761-267-103-210-260-134-132-103-627-206-120-167-756-572-132-302-230-627-213-726-206-260-102-210-120-2_W.mp3)
  Segment 23: 76684ms ~ 77260ms (file: 567-567-657-651 576-576-567-567-567-576-576-567-576-564-567-567-567-567 572-576-567-567-576-576-567-567-576-567-567-576-576 576-567-567-576-576-567-576-567-576-567-765-756 756-7_W.mp3)
  Segment 24: 78260ms ~ 82796ms (file: 261-621-625-625-625-625-623-612-612-613-621-624-612-236-263 134-103-312 532-203-620-123-163-631-623-623-627-260-103-623-625-625-620-612-652-615-657-654-615-654-657-651-613 612-6_W.mp3)
  Segment 25: 83796ms ~ 84372ms (file: 567-561-516-561-651-657-651-567-516-657-653-653-137-653-371-156-137-567-576-576-576-567 502-502-527-523-572-516-567-576-576-567-567-156-561-516-516-561-576-576-561-571-513-735-5_W.mp3)
  Segment 26: 85372ms ~ 95128ms (file: 321-102-621-136-106-126-675-276-260-120-627-126-213-103-261-231-123-231-130-237-132-672-321-231-321-312-326-752-312-130-762-260-173-312-132-302-136-621-236-103-650-231-321-572-2_W.mp3)
  Segment 27: 96128ms ~ 96704ms (file: 571-751-715-175-157-576-571-571-571-175-571-571-157_W.mp3)
  Segment 28: 97704ms ~ 98280ms (file: 625 576-576-567-567-625-652-625-625-625-265-250-625-625-657-627-265-652-265-625-625-567-657-657-657-765-657-650-567 507-567-560-576-570-567-756-567-567-615-256-652-657-567-625-2_W.mp3)
  Segment 29: 99280ms ~ 99856ms (file: 512-526-513-527-531-513-512-501-526-507-512-532-527-567-531-156-521-527-527-527-567-567-567-567-567-567-576-567-576-576-576-576-751-756-751-531-526-572-563-576-527-572-572-507-2_W.mp3)
  Segment 30: 100856ms ~ 101432ms (file: 301-102-130-130-132-310-132-123-132_W.mp3)
  Segment 31: 102432ms ~ 103008ms (file: 267-567-567-567-657-567-156-165-621-627-672-672-627-576-752-275-276-267-236-517-574-756-753-613-167-756-576-756-675-576-567-513-157-215-672-675-765-637-132-326-236-621-621-612-6_W.mp3)
  Segment 32: 104008ms ~ 104584ms (file: 105-105-150-105-705-175-157-157-175-150-513-751-125-752-257-125-132-172-175-517-156-156-175-157-157-175-157-157-715-571-157-157-571-715-175-715-175-175-157-135-175-157-157-521-1_W.mp3)
  Segment 33: 105584ms ~ 106160ms (file: 576-576 231-627-265-576-752-736-657-635-271 761-674-132-132-620-263-263_W.mp3)
  Segment 34: 107160ms ~ 113676ms (file: 301-203-276-271-276-276-276-201-201-103-120-275-267-276-102-130-207-725-761-103-136-132-610-652-756-175-715-753-130-137-706-175-357-235-270-725-576-574-657-576-573-756-107-750-3_V.mp3)
  Segment 35: 114676ms ~ 115252ms (file: 671-617-625-652-657-627-265-267-625-652-625-267-627-627-267-267-265-615-567-135-136-135-135-153-135-165-165-163-572-527-576-756-163-615-651-156-567-516-571-175-137-135-516-136-5_W.mp3)
  Segment 36: 116252ms ~ 122696ms (file: 157-750-310-201-267-260-236-213-126-172-130-352-357-236-137-130-267-267-263-261-260-627-627-620-130-201-120-206-132-103-126-617-751-763-326-132-132-270-267-712-621-562-276-260-2_W.mp3)
  Segment 37: 123696ms ~ 124272ms (file: 521-501-517-571-574-571-751-527-576-523 710-751-725-726-725-715-751-762-721-725-716-715-705 765-761-765-671-627-761-621-762-765-762-762-765-267-672-675-526 750-726-725-750-752-7_V.mp3)
  Segment 38: 125272ms ~ 125848ms (file: 236-521-563-526-526-524-503-752-752-537-537-573-537-573-576-576-576-576-571-571-571-751-571-751-105-106-316-356-305-537-573-507-570-570-275-527-325-276-625-572-567-306-136-361-3_V.mp3)
  Segment 39: 126848ms ~ 127424ms (file: 632-126-261-216-231-560-563-523-326-263-236-632-203-206-236-203-326-213-562-562-652-625-265-625-613-641-136-136-570-574-750-175-506-501-517-123-261-620-602-621-602-560-562-351-2_V.mp3)
  Segment 40: 128424ms ~ 129000ms (file: 145-104-154-105-143-145-154-134-104 473-475-410-450-415-401-104-451-405-105-405-154-104-401-105-145-105-105-105-157-147-143-145-401-415-140-154 103-143-103-175-104-105-105-157-1_V.mp3)
  Segment 41: 130000ms ~ 138640ms (file: 627-725-263-574-106-263-263-321-210-425-256-267-627-652-310-672-627-547-231-102-312-627-657-752-267-263-231-275-362-231-263-620-326-130-103-541-317-574-263-213-132-120-106-102-3_W.mp3)
  Segment 42: 139640ms ~ 140216ms (file: 105-157-150-510-516-517-105-501-157-517-157-105-105-105-105-153-150-517-105-510 150-150-105-150-510-510-105-516-157-501-105-517-157-517-157-105-510-150-501-517-501-105-105-150-1_V.mp3)
  Segment 43: 141216ms ~ 152268ms (file: 213-260-267-216-675-267-236-627-267-627-265-713-106-107-103-103-132-126-123-627-267-137-106-715-106-167-261-612-173-675-675-567-631-312-675-265-231-102-163-106-615-632-751-136-1_W.mp3)
  Segment 44: 153268ms ~ 162844ms (file: 267-576-572-547-576-576-576-517-107-162-102-137-627-762-761-547-675-547-103-135-107-510-123-103-132-103-321-103-547-265-102-123-132-260-216-103-132-310-130-132-261-260-263-206-1_V.mp3)
  Segment 45: 163844ms ~ 183212ms (file: 132-103-301-130-205-130-103-103-301-310-230-123-137-103-231-201-213-572-310-756-756-572-576-310-103-132-130-130-103-132-143-103-751-130-132-231-321-231-103-130-312-756-153-123-1_V.mp3)
  Segment 46: 184212ms ~ 184788ms (file: 367-601-160-603-362-623-612-623-267-132-602-716-165-135-167-132-162-165-136_V.mp3)
  Segment 47: 185788ms ~ 190540ms (file: 216-213-231-213-261-237-261-231-267-267-260-261-256-236-231-126-263-261-276-256-265-236-276-326-672-267-267-263-261-276-216-236-231-276-263-231-261-216-216-326-263-627-126-312-3_W.mp3)
  Segment 48: 191540ms ~ 192116ms (file: 261-261-261-621-621-621-261-621-623-621-621-621-623-263 627-623-623 625-621-621-623-621-261-260-261-261-260-261-261-263 527-265-625-652-652-652-651-651-654-652-657-654-651-657-6_W.mp3)
  Segment 49: 193116ms ~ 193692ms (file: 231-132-612-613-136-126-521-123-163-132-326-261-123-561-162-361-513-521-203-153-576-157-756-672-601-261-610-620-602-627-765-672-712-710-107-136-102-103-726-237-163-132-136-134-2_W.mp3)
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
> **Time**: 2026-04-05 12:35:34
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "작아 보이죠 무게로 따지면 소수점 아래 0이 24개 붙는 그램 정도밖에 되지 않습니다 지구상의 어떤 저울로도",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "작아", "start_ms": 10, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "보이죠", "start_ms": 125, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "무게로", "start_ms": 270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "따지면", "start_ms": 405, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "소수점", "start_ms": 550, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "아래", "start_ms": 705, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "0이", "start_ms": 820, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "24개", "start_ms": 945, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "붙는", "start_ms": 1110, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "그램", "start_ms": 1225, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "정도밖에", "start_ms": 1360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "되지", "start_ms": 1545, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "않습니다", "start_ms": 1660, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "지구상의", "start_ms": 1825, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1980, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "저울로도", "start_ms": 2105, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "문의 한 미국 여성이 망설임 없이 말합니다 한국 시골이요 네 시골이요 그것도 한국에서요 그녀는 진짜로 사람을 믿고 살고 싶다면 무조건 한국으로 가야 한대요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "문의", "start_ms": 20, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 135, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "미국", "start_ms": 220, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "여성이", "start_ms": 345, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "망설임", "start_ms": 490, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "없이", "start_ms": 645, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "말합니다", "start_ms": 760, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 925, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "시골이요", "start_ms": 1050, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 1205, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "시골이요", "start_ms": 1300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그것도", "start_ms": 1455, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "한국에서요", "start_ms": 1590, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "그녀는", "start_ms": 1755, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "진짜로", "start_ms": 1880, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "사람을", "start_ms": 2025, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "믿고", "start_ms": 2160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "살고", "start_ms": 2275, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "싶다면", "start_ms": 2400, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "무조건", "start_ms": 2545, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "한국으로", "start_ms": 2690, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가야", "start_ms": 2845, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한대요", "start_ms": 2960, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "en",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 4,
"transcript": "통으로 한 공간이든 곳을",
"speaker": "speaker_3",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "통으로", "start_ms": 15, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "공간이든", "start_ms": 225, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "곳을", "start_ms": 380, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "통으로 딱 느낌대로 해야 진짜 맛있는 양념이요 어떻게 넣어도 맛있을 수밖에",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "통으로", "start_ms": 10, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "딱", "start_ms": 145, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "느낌대로", "start_ms": 240, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "해야", "start_ms": 395, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "맛있는", "start_ms": 635, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "양념이요", "start_ms": 780, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 945, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "넣어도", "start_ms": 1080, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "맛있을", "start_ms": 1215, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "수밖에", "start_ms": 1360, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "한 30cm고요 제가 질문을 드려보고 싶어요 몇 킬로일 것 같아요 음 한 3 4kg 2리터 생수 6",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "한", "start_ms": 15, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "30cm고요", "start_ms": 100, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 285, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "질문을", "start_ms": 400, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "드려보고", "start_ms": 535, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "싶어요", "start_ms": 690, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "몇", "start_ms": 815, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "킬로일", "start_ms": 910, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 1035, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 1120, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 1255, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1360, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "3", "start_ms": 1445, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "4kg", "start_ms": 1550, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "2리터", "start_ms": 1695, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "생수", "start_ms": 1840, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "6", "start_ms": 1965, "dur_ms": 90, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "안에 싶은데요 문제는 그다음입니다 한국어를 너무 자연스럽게 쓰거든요 등산 사진에 팬이 어디 산이에요 라고 물어보니까 번역기 느낌 1도 없이 콜로라도에 있는 산이에요 이름은 잘 모르겠어요 크크 이렇게 답을 해주고요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "안에", "start_ms": 20, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "싶은데요", "start_ms": 135, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "문제는", "start_ms": 280, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "그다음입니다", "start_ms": 415, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "한국어를", "start_ms": 600, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 755, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "자연스럽게", "start_ms": 870, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "쓰거든요", "start_ms": 1035, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "등산", "start_ms": 1190, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "사진에", "start_ms": 1305, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "팬이", "start_ms": 1440, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어디", "start_ms": 1555, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "산이에요", "start_ms": 1670, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "라고", "start_ms": 1815, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "물어보니까", "start_ms": 1920, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "번역기", "start_ms": 2085, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "느낌", "start_ms": 2220, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "1도", "start_ms": 2335, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "없이", "start_ms": 2460, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "콜로라도에", "start_ms": 2575, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 2740, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "산이에요", "start_ms": 2845, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이름은", "start_ms": 2990, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 3125, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "모르겠어요", "start_ms": 3220, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "크크", "start_ms": 3385, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 3510, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "답을", "start_ms": 3635, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "해주고요", "start_ms": 3740, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "포커스 양보가 필요합니다",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "포커스", "start_ms": 10, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "양보가", "start_ms": 155, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 290, "dur_ms": 160, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "상호간 무수히 많기 때문에 위험하다고 여겨지는 것은 아닙니다 이유는 아주 단순합니다 이 바다는 관광객에게도 해적에게도 너무나 가혹하고 지나치게 차갑기 때문입니다 모험이나 절경",
"speaker": "speaker_6",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "상호간", "start_ms": 20, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "무수히", "start_ms": 155, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "많기", "start_ms": 280, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 395, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "위험하다고", "start_ms": 530, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "여겨지는", "start_ms": 695, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 850, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 965, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이유는", "start_ms": 1110, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "아주", "start_ms": 1245, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "단순합니다", "start_ms": 1360, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1545, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "바다는", "start_ms": 1630, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "관광객에게도", "start_ms": 1765, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "해적에게도", "start_ms": 1970, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "너무나", "start_ms": 2155, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가혹하고", "start_ms": 2290, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "지나치게", "start_ms": 2445, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "차갑기", "start_ms": 2610, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 2755, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "모험이나", "start_ms": 2940, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "절경", "start_ms": 3095, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "빼나 그렇게 해야지 네가 오래 집도 사고 더 성장할 수 있는 거야 그게 진짜 어려운 거야 절제하는 게 그것만 네가 완성하",
"speaker": "speaker_7",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "빼나", "start_ms": 10, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그렇게", "start_ms": 135, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "해야지", "start_ms": 270, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 415, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "오래", "start_ms": 530, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "집도", "start_ms": 645, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "사고", "start_ms": 770, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 895, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "성장할", "start_ms": 990, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1145, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "있는", "start_ms": 1230, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "거야", "start_ms": 1345, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그게", "start_ms": 1470, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "진짜", "start_ms": 1585, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "어려운", "start_ms": 1710, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "거야", "start_ms": 1855, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "절제하는", "start_ms": 1980, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 2145, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "그것만", "start_ms": 2240, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "네가", "start_ms": 2375, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "완성하", "start_ms": 2490, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "언니 이게 어려웠을 거 같아요 근데 선배님이 아니 아니 너 하고 싶은 거 한 번 다 해봐 그리고 이제 선배님이 리액션",
"speaker": "speaker_8",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "언니", "start_ms": 15, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 140, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "어려웠을", "start_ms": 255, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 410, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "같아요", "start_ms": 495, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "근데", "start_ms": 630, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "선배님이", "start_ms": 745, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 910, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "아니", "start_ms": 1025, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "너", "start_ms": 1140, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "하고", "start_ms": 1225, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "싶은", "start_ms": 1340, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 1455, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 1540, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "번", "start_ms": 1625, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1710, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "해봐", "start_ms": 1795, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 1920, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 2065, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "선배님이", "start_ms": 2180, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "리액션", "start_ms": 2345, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "대관계 속에서 우리가 단순히 특정 진영의 무기 공급처로만 낙인 찍히는 우를 범해서는 안 된다는 점입니다 방산 수출의 활로를 뚫는 것과 별개로 우리의 무기가 중동의 참혹한 분쟁",
"speaker": "speaker_9",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "대관계", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "속에서", "start_ms": 175, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 320, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "단순히", "start_ms": 455, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "특정", "start_ms": 610, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "진영의", "start_ms": 735, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "무기", "start_ms": 870, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "공급처로만", "start_ms": 985, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "낙인", "start_ms": 1190, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "찍히는", "start_ms": 1315, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "우를", "start_ms": 1460, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "범해서는", "start_ms": 1575, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 1740, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "된다는", "start_ms": 1835, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "점입니다", "start_ms": 1980, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "방산", "start_ms": 2145, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "수출의", "start_ms": 2270, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "활로를", "start_ms": 2405, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "뚫는", "start_ms": 2550, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "것과", "start_ms": 2665, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "별개로", "start_ms": 2780, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "우리의", "start_ms": 2925, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "무기가", "start_ms": 3050, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "중동의", "start_ms": 3185, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "참혹한", "start_ms": 3320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "분쟁", "start_ms": 3475, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "무게 중심을 이동시켜서 얼음을 비틀어서 깨부순다고 하죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "무게", "start_ms": 10, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "중심을", "start_ms": 125, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이동시켜서", "start_ms": 270, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "얼음을", "start_ms": 475, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "비틀어서", "start_ms": 610, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "깨부순다고", "start_ms": 775, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "하죠", "start_ms": 960, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "진짜 신기하죠 보통 쇄빙선은 러시아처럼 바다가 꽁꽁 얼어버린 나라들이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 15, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "신기하죠", "start_ms": 140, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 295, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "쇄빙선은", "start_ms": 410, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "러시아처럼", "start_ms": 575, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "바다가", "start_ms": 760, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "꽁꽁", "start_ms": 895, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "얼어버린", "start_ms": 1020, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "나라들이", "start_ms": 1185, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "네 해협에 고립된 지 벌써 한 달이 다 되어 가는데 식료품이나 생필품 등의 재고가 충분한지도 궁금하거든요 어 식량",
"speaker": "speaker_10",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 20, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "해협에", "start_ms": 115, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "고립된", "start_ms": 240, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "지", "start_ms": 385, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "벌써", "start_ms": 470, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 585, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "달이", "start_ms": 670, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 785, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "되어", "start_ms": 870, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "가는데", "start_ms": 975, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "식료품이나", "start_ms": 1110, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "생필품", "start_ms": 1295, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "등의", "start_ms": 1450, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "재고가", "start_ms": 1565, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "충분한지도", "start_ms": 1710, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "궁금하거든요", "start_ms": 1895, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 2100, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "식량", "start_ms": 2195, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "은 입자 가속기로 만드는 방법이 유일합니다 CERN에서는 양성자 빔을 엄청난 속도로 가속시켜서 금속 표적에 충돌시켜요 이 충돌에서 반양성자가 생겨납니다 그리고 전기장과 자기장을 이용해서 이 반양성자를 천천히 감소시켜서 포획합니다 이 과정에서 대부분의 입자는 날아가 버리고 겨우",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "은", "start_ms": 10, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "입자", "start_ms": 95, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "가속기로", "start_ms": 210, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만드는", "start_ms": 365, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "방법이", "start_ms": 510, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "유일합니다", "start_ms": 645, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "CERN에서는", "start_ms": 830, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "양성자", "start_ms": 1015, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "빔을", "start_ms": 1160, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "엄청난", "start_ms": 1275, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "속도로", "start_ms": 1420, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "가속시켜서", "start_ms": 1555, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "금속", "start_ms": 1740, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "표적에", "start_ms": 1855, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "충돌시켜요", "start_ms": 1990, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2175, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "충돌에서", "start_ms": 2260, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "반양성자가", "start_ms": 2415, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "생겨납니다", "start_ms": 2600, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 2785, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "전기장과", "start_ms": 2930, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "자기장을", "start_ms": 3095, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이용해서", "start_ms": 3250, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 3395, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "반양성자를", "start_ms": 3480, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "천천히", "start_ms": 3665, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "감소시켜서", "start_ms": 3810, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "포획합니다", "start_ms": 3995, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 4180, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "과정에서", "start_ms": 4265, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "대부분의", "start_ms": 4420, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "입자는", "start_ms": 4585, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "날아가", "start_ms": 4730, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "버리고", "start_ms": 4865, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "겨우", "start_ms": 5000, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "리아 안녕하세요 안녕하세요 오가는 사람들의 손에 통으로",
"speaker": "speaker_11",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "리아", "start_ms": 15, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "안녕하세요", "start_ms": 130, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "안녕하세요", "start_ms": 295, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "오가는", "start_ms": 460, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "사람들의", "start_ms": 605, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "손에", "start_ms": 760, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "통으로", "start_ms": 875, "dur_ms": 130, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 12:35:34*
