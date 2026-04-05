# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=24268  time=08:53:33  uptime=27830s  mem=458MB  system_processes=351"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_085126.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 7128ms (file: 175-712-137-175-716-167-571-175-173-216-132-172-123-206-102-167-701-571-715-670-756-215-275-761-675-750-752-162-576-613-716-175-176-651-675-163-673-672-617-567-576-261-365-137-5_W.mp3)
  Segment 2: 8128ms ~ 8704ms (file: 576-765-725-572-502-571-126-573-265-263-126-627-162-310-607-201-312-572-376-721-206-251-125-675-123-761-376-761-763-176-671-760_W.mp3)
  Segment 3: 9704ms ~ 27308ms (file: 502-543-213-102-576-576-132-571-570-130-210-301-103-103-103 132-103-103-103-103-143-103-574-310-312-547-312-103-102-210-310-102 267-654-546-201-206-210-126-136-173-756-137-132-5_V.mp3)
  Segment 4: 28308ms ~ 28884ms (file: 571-501-570-501-570-517 507-150-510-517-501-510-507-501-510-510-516-501-501-510-501-517-507-105-570-501-510-175-501-507-570-571-510-750-570-571-571 510-510-501-510-571-571-573-5_V.mp3)
  Segment 5: 29884ms ~ 30460ms (file: 105-107-105-150-157-157-105-105-150 157-517-153-157-105-513-105-153-517-157-153-150-105-105-150-157-150-157-105-157-105-150-105-157-501 105-153-135-501-105-103-513-105-150-510-1_V.mp3)
  Segment 6: 31460ms ~ 38084ms (file: 765-106-236-236-263-157-312-213-213-263-752-572-316-537-107-106-312-213-263-132-236-627-265-231-312-326-576-275-154-156-267-261-162-130-310-612-671-527-123-130-572-756-132-261-2_W.mp3)
  Segment 7: 39084ms ~ 41892ms (file: 217-176-170-175-132-175-301-672-263-260-130 153-150-510-150-157-103-175-751-175-175-751-173-175-715-175-175-175-175-157-175-175-715 510-153-150-517-153-175-175-175-750-175-751-7_W.mp3)
  Segment 8: 42892ms ~ 43468ms (file: 215-123-103-103-625-263-231-673-306-302-236-360-326-136-136-163-673-627-617-267-267-610-267-623-103-132-320-132-106-310-162-126-321-301-310-103-130-103-106-162-106-206-126-617-1_V.mp3)
  Segment 9: 44468ms ~ 54872ms (file: 132-267-267-571-570-267-257-572-312-231-160-136-137-321-312-276-132-173-576-236-276-576-726-236-326-236-321-576-267-123-103-260-310-326-617-312-236-263-106-321-237-362-137-531-2_W.mp3)
  Segment 10: 55872ms ~ 64836ms (file: 301-312-102-125-210-275-120-201-267-127-752-754-125-267-206-132-316-531-260-120-762-120-230-206-213-152-216-201-672-715-103-321-132-102-127-612-621-120-201-210-231-120-207-127-1_V.mp3)
  Segment 11: 65836ms ~ 66412ms (file: 172-716-637-672-276-106-623-213-162-261-236-623-103-103-160-106-260-320-273-126-102-162-612-632-231-126-310-321-312-312-321-236-213-231-321-132-213-126-120-126-126-217-216-206-2_W.mp3)
  Segment 12: 67412ms ~ 81164ms (file: 216-231-230-765-256-231-236-236-132-103-265-231-321-263-260-230-267-273-321-321-276-765-267-236-237-765-213-321 102-231-725-106-162-263-260-210-260-230-120-237-236-276-267-715-1_W.mp3)
  Segment 13: 82164ms ~ 90120ms (file: 236-270-237-270-362-316-132-130-276-231-271-237-276-321-230-120-230-120-516-125-105-612-631-163-631-163-230-203-756-576-756-237-726-102-320-372-273-236-203-102-102-217-173-203-1_W.mp3)
  Segment 14: 91120ms ~ 93100ms (file: 310-260 526-205-250-265-275-250-506-267-572-570-256-267-765-675-765-765-765-765-756-756-765-675-765-756 502-520-527-657-527-562-250-526-567-657-765-765-765-765-765-756-765-765-7_W.mp3)
  Segment 15: 94100ms ~ 94676ms (file: 657-632-126-310-163-136-136-316-136-102-103-163-130-102-106-176-136-360-562-163-316-675-103-163-165-163-257-502-763-617-163-136-617-163-103-613-176-162-675-167-103-126-162-160-2_V.mp3)
  Segment 16: 95676ms ~ 102336ms (file: 321-574-265-261-102-572-321-213-126-574-231-231-263-120-612-751-574-620-236-103-231-216-210-276-210-620-576-567-263-261-576-321-457-326-120-302-236-236-236-312-231-321-310-132-6_W.mp3)
  Segment 17: 103336ms ~ 103912ms (file: 105-107-175-107-105-157-107-107-175-107-107-107 517-150-150-105-157-107-107-715-175-175-715-170-750 501-150-517-157-105-170-105-571-517-715-107-175-170-107 105 150-175-157-105-1_V.mp3)
  Segment 18: 104912ms ~ 105488ms (file: 601-610-160-163-572-106-756-362-326-765-675-657-657-623-260-236-216-123-173-176-176-176-137-130-156-516-156-651-517-516-510-157-715-175-715-175-175-157-574-756-316-326-376-574-5_W.mp3)
  Segment 19: 106488ms ~ 107064ms (file: 501-507-501-570-510-517-501-517-570-570-571-574-510-571-517-507-507-501 105-510-150-150-510-571-517-150-105-105-510-501-150-501-157-150-501-150-510-157-150-157-501-510-501-150-5_V.mp3)
  Segment 20: 108064ms ~ 108640ms (file: 167-103-126-102-162-167-106-106-617-617-106-670-602-620-602-670-672-672-607-607-607-103-160-610-132-176-103-123-162 167-361-362-321-671-361-326-312-130-106-163-136-126-102-130-6_W.mp3)
  Segment 21: 109640ms ~ 110216ms (file: 201-263-231-231-326-631-361-362-631-632-631-653-365-302-263-362-625-203-235-253-576-175-135-317-136-576-576-157-123-123-125-301-312-312-534-573-501-571-567-547-213-251-152-236-2_W.mp3)
  Segment 22: 111216ms ~ 111792ms (file: 752-173-751-173-135-170-753-735-731-173-751-753-713-137-175-715-175-173-715-173-173-173-157 751-517-135-175-715-715-715-713-751-715-713-157-175-713-715-715-173-175-173-173-137-1_V.mp3)
  Segment 23: 112792ms ~ 123988ms (file: 231-270-270-237-326-327-231-361-236-126-261-623-216-231-326-213-216-263-216-127-263-326-326-132-752-270-607-210-312-261-236-376-723-370-736-137-310-312-317-206-267-106-617-236-1_W.mp3)
  Segment 24: 124988ms ~ 125564ms (file: 537-637-657-756-576-576-576-126-135-657-362-657-672-632-573-517-371-152-765-537-532-537-375-576-751-753-756-675-365-357-576-573-375-573-573-573-571-576-576-756-573-517-751-756-5_W.mp3)
  Segment 25: 126564ms ~ 127140ms (file: 765-765-715-756-716-753-763-756-751-315-163-136-136-163-136-136-136-631-163-136-753-756-716-712-761-376-375-726-375-275-751-372-372-375-761-613-613-163-316-135-165-615-165-126-1_W.mp3)
  Segment 26: 128140ms ~ 138904ms (file: 263-230-231-237-237-231-231-231-125-260-267-275-237-752-725-321-213-172-132-201-127-137-130-236-327-312-527-275-230-231-231-547-231-273-206-267-207-302-267-206-250-216-627-625-6_W.mp3)
  Segment 27: 139904ms ~ 140480ms (file: 671-162-123-765-163-160-103-132-136-123-107-725-372-273-672-756-756-756-765-753-756 103-362-726-156-163-310-137-317-357-135-567-162-567-102-620-236-102-132-672-526-106 176-325-3_V.mp3)
  Segment 28: 141480ms ~ 142056ms (file: 175-176-716-176-713-751 105-150-150-105-150-105-150-157-157-175-157-175-751-715-176-176-170-716-716-176-176-175-170-176-157 105-105-105-105-175-501-175-157-105-157-150-751-105-1_V.mp3)
  Segment 29: 143056ms ~ 149788ms (file: 753-657-120-572-321-231-203-205-765-572-123-760-312-231-670-237-203-230-725-271-230-120-310-315-632-736-362-310-673-632-231-201-672-362-237-302-263-136-120-327-317-321-632-763-7_W_stt_the_activity_has_entered_the_weekend_is.mp3)
  Segment 30: 150788ms ~ 151364ms (file: 316-321-263-316-103-312-102-103-136-706-670-123-576-607-165-756-675-716-132-137-327-173-132-230-321-513-610-632_W.mp3)
  Segment 31: 152364ms ~ 152940ms (file: 736-136-617-136-136-136-137-136-316-316-317-136-136-765-671-673-765-675-763-736-375-367-765-367-736-761-673-673-736-735-763_W.mp3)
  Segment 32: 153940ms ~ 174820ms (file: 103-102-103-237-267-263-236-123-103-130-627-203-321-231-312-321-231-312-163-574-765-130-104-132-103-627-672-672-105-517-213-527-524-106-167-621-672-615-263-260 230-263-236-236-2_W.mp3)
  Segment 33: 175820ms ~ 176396ms (file: 175-167-671-716-167-167-176-716-761-715-716-175-176-175-176-176 105-105-150-105-107-105-175-175-175-175-716-175-156-176-175-167-176-170-167-175-167-176-170-517 105-105-105-153-1_W.mp3)
  Segment 34: 177396ms ~ 188880ms (file: 103-163-176-236-213-231-213-125-215-256-261-260-312-130-123-213-213-514-136-103-671 576-612-210-213-126-310-103-130-103-673-367-261-352-312-537-531-537-532-532-263-756-651-603-6_W.mp3)
  Segment 35: 189880ms ~ 190456ms (file: 502-560-257-572-752-527-507-570-567-526-570-527-572-507-572 652-250-257-527-705-750-576-507-572-506-570-560-507-567-562-567-520-520-256-576-507 507-506-576-750-506-570-520-576-5_V.mp3)
  Segment 36: 191456ms ~ 200708ms (file: 276-712-137-173-137-173-173-136-631-136-136-123-273-312-763-326-236-326-267-276-572-276-237-237-127-132-312-752-275-327-627-120-136-120-351-321-367-312-267-162-106-216-761-716-7_W.mp3)
  Segment 37: 201708ms ~ 202284ms (file: 761-726-671-672-617-623-136 312-130-162-657-765-132-576-567-762-623-217-631-213-263-756-765-756-103-103-165-132-103-216-627-375-625-562-563-567-765-123-167-617-123-136-163-132-3_W.mp3)
  Segment 38: 203284ms ~ 203860ms (file: 105-150-150-157-510-157-157 150-150-153-105-150-150-150-150-156 105-150-150-153-150-156-156-153-135-153-150-150-150-150-150-150-150-153-150-150 154-105-157-150-157-153-517-156-5_V.mp3)
  Segment 39: 204860ms ~ 205436ms (file: 153-156-516-135-153-156-157-165-156-157 527-205-527-570-507-527-521-725-276-267-571-153-517-516-156-153-156-516-165-157-156-516-517 526-750-570-207-276-627-502-260-672-276-267-5_V.mp3)
  Segment 40: 206436ms ~ 207012ms (file: 530 105-105-571-571-517-571-501-570-570-571-570-570-570-576-576-567 504-507-157-571-507-501-510-751-750-571-517-571-571-570 547-507-570-570-517-571-574 501-504-150-570-507-517-5_V.mp3)
  Segment 41: 208012ms ~ 221188ms (file: 102-132-173-167-613-203-210-130-130-123-132-213-130-103-130-132-260-261-231-231-302-263-260-310-267-201-127-260-213-173-301-130-130-312-170-107-102-102-231-103-103-103-103-130-1_V.mp3)
  Segment 42: 222188ms ~ 222764ms (file: 105-570-751-570-517-754-754-571-751-571-750-750 574-574-574-574-570-574-574-750 570-574-751-157-517-571-150-571-107-754-574-574-574-574-751-517-157-153-157-157-153-153-137-137-1_W.mp3)
  Segment 43: 223764ms ~ 224340ms (file: 103-130-163-136-165-156-135-103-150-152-160-153-150-105-150-105 160-526-156-625-256-125-615-652-615-152-156-561-517-567-156-657 163-516-567-651-612-167-613-361-136-576-576-756-5_W.mp3)
  Segment 44: 225340ms ~ 242044ms (file: 206-162-106-103-103-103-132-103-103-106-130-103-230-103-103-103-103-130-153-106-103-143-301-132-641-103-310-230-203-236-103-103-103-106-206-264-103-132-103-103-143-173-713-103-3_V.mp3)
  Segment 45: 243044ms ~ 243620ms (file: 762 132-102-136-163-106-135-137-103-136-120-136-316-625-312-217-163-612 103-156-167-103-176-136-132-103-317-163-137-136-613-163-750-361-316-103-132-136-103-163-102-120-213-613-1_V.mp3)
  Segment 46: 244620ms ~ 249192ms (file: 103-106-120-215-152-127-321-631-571-751-271-231-130-130-103-273-237-127-213-321-175-107-723-175-321-231-326-316-132-310-102-136-107-753-312-301-103-271-127-175-103-362-175-103-1_W.mp3)
  Segment 47: 250192ms ~ 250768ms (file: 103-176-107-301-312-310-137-731-137-137-310-130_V.mp3)
  Segment 48: 251768ms ~ 252344ms (file: 157-517-105-517-153-157-501-150-517-501-510-561-510-150-150-510-501-150-105-150-150-150-510 150-150-105-517-501-105-150-510-510-517-510-150-510-105-510-150-567-517-510-517-516-5_V.mp3)
  Segment 49: 253344ms ~ 253920ms (file: 673-673-672-637-617-617-163-316-163-612_W.mp3)
  Segment 50: 254920ms ~ 255496ms (file: 572-576-571-571-571-571-571-715 250-570-507-502-502-572-756-765-751-571-576-756-751-576-571-751-576-571-157-157-751-517-571-751 502-275-206-520-265-275-275-756-756-756-753-571-7_W.mp3)
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
> **Time**: 2026-04-05 08:55:51
> **AI**: gemini

---

## Response

AnalysisAnalysisGemini의 응답[
{
"segment": 1,
"transcript": "꺼내는데 이제 기와도 너무 중요한 인물일 것 같으니까 쇠붙이는 아예 쓰지도 못하고 기와도 손으로 다 거둬",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "꺼내는데", "start_ms": 150, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 880, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "기와도", "start_ms": 1250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 1780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중요한", "start_ms": 2180, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "인물일", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 3150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같으니까", "start_ms": 3320, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "쇠붙이는", "start_ms": 4050, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "아예", "start_ms": 4720, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "쓰지도", "start_ms": 5150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "못하고", "start_ms": 5650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "기와도", "start_ms": 6120, "dur_ms": 410, "phase_lr_deg": 0.0},
{"word": "손으로", "start_ms": 6580, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 7010, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "거둬", "start_ms": 7110, "dur_ms": 18, "phase_lr_deg": 0.0}
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
"transcript": "세상에서 사용하는 그릇이 다 물질입니다 아까 뭐라고 말씀드렸죠 반물질은 물질과 만나는 순간 바로 소멸합니다 따라서 반물질은 어떤 그릇이라도 담는 순간 소멸해버리죠 그래서 절대로 벽에 닿지 않도록 공중에 띄워야 합니다 이때 쓰는 게 자기장과 전기장의 조합이에요 자기장으로",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "세상에서", "start_ms": 120, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "사용하는", "start_ms": 820, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "그릇이", "start_ms": 1420, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "다", "start_ms": 1890, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "물질입니다", "start_ms": 2120, "dur_ms": 780, "phase_lr_deg": 0.0},
{"word": "아까", "start_ms": 3150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뭐라고", "start_ms": 3550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "말씀드렸죠", "start_ms": 4020, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "반물질은", "start_ms": 4950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "물질과", "start_ms": 5650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "만나는", "start_ms": 6180, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "순간", "start_ms": 6650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "바로", "start_ms": 7080, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "소멸합니다", "start_ms": 7450, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "따라서", "start_ms": 8450, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "반물질은", "start_ms": 9020, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 9700, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "그릇이라도", "start_ms": 10030, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "담는", "start_ms": 10800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "순간", "start_ms": 11200, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "소멸해버리죠", "start_ms": 11650, "dur_ms": 880, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 12750, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "절대로", "start_ms": 13250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "벽에", "start_ms": 13780, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "닿지", "start_ms": 14110, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "않도록", "start_ms": 14480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "공중에", "start_ms": 14980, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "띄워야", "start_ms": 15510, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "합니다", "start_ms": 15980, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이때", "start_ms": 16650, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "쓰는", "start_ms": 17020, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 17350, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "자기장과", "start_ms": 17520, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "전기장의", "start_ms": 18220, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "조합이에요", "start_ms": 18850, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "자기장으로", "start_ms": 19750, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "것처럼 노아의 매매는 정말 단순하면서도 강력합니다 리듬이 만들어지고 거기에 반응만 하면 되니까요 하지만 이 전략에도 치명적인 단점이 있",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "것처럼", "start_ms": 100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "노아의", "start_ms": 650, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "매매는", "start_ms": 1080, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 1650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "단순하면서도", "start_ms": 2050, "dur_ms": 820, "phase_lr_deg": 0.0},
{"word": "강력합니다", "start_ms": 2920, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "리듬이", "start_ms": 3850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "만들어지고", "start_ms": 4320, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "거기에", "start_ms": 5050, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "반응만", "start_ms": 5480, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 5980, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "되니까요", "start_ms": 6350, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 7050, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 7520, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "전략에도", "start_ms": 7680, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "치명적인", "start_ms": 8320, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "단점이", "start_ms": 8920, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "있", "start_ms": 9380, "dur_ms": 44, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "양보가 필요합니다",
"speaker": "speaker_4",
"confidence": 0.92,
"language": "ko",
"silence_before_ms": 1100,
"audio_type": "speech",
"words": [
{"word": "양보가", "start_ms": 1100, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "필요합니다", "start_ms": 1680, "dur_ms": 750, "phase_lr_deg": 0.0}
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
"transcript": "MBC 뉴스 김입니다 이란과 아라비아 반도 사이에 위치한 호르무즈 해협은 육지에 인공적으로 만든 운하와 달리 여러 나라에 둘러싸인 바다의 일부입니다",
"speaker": "speaker_5",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "MBC", "start_ms": 200, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "뉴스", "start_ms": 680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "김입니다", "start_ms": 1050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "이란과", "start_ms": 1850, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "아라비아", "start_ms": 2380, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "반도", "start_ms": 2950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "사이에", "start_ms": 3350, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "위치한", "start_ms": 3820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "호르무즈", "start_ms": 4320, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "해협은", "start_ms": 4920, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "육지에", "start_ms": 5550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "인공적으로", "start_ms": 6020, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "만든", "start_ms": 6800, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "운하와", "start_ms": 7200, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "달리", "start_ms": 7750, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "여러", "start_ms": 8250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "나라에", "start_ms": 8650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "둘러싸인", "start_ms": 9120, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "바다의", "start_ms": 9750, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "일부입니다", "start_ms": 10180, "dur_ms": 624, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "바다의 경계와 사유 마른 나쁜 뿐만 아니라 기술 이전까지 결합한 종합 경쟁력을 막강하게 갖추고 있기 때문입니다 대만 방중은 유럽 국가들이 공급 지연과 생산 한계에 발목을 잡힌 사이 한국은 빠르게 계약하고 빠르게 인",
"speaker": "speaker_6",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "바다의", "start_ms": 150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "경계와", "start_ms": 580, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "사유", "start_ms": 1150, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "마른", "start_ms": 1520, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "나쁜", "start_ms": 1950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "뿐만", "start_ms": 2350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 2720, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 3250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이전까지", "start_ms": 3650, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "결합한", "start_ms": 4280, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "종합", "start_ms": 4780, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "경쟁력을", "start_ms": 5210, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "막강하게", "start_ms": 5850, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "갖추고", "start_ms": 6450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 6950, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "때문입니다", "start_ms": 7320, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "대만", "start_ms": 8250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "방중은", "start_ms": 8650, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "유럽", "start_ms": 9250, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "국가들이", "start_ms": 9620, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "공급", "start_ms": 10250, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "지연과", "start_ms": 10680, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "생산", "start_ms": 11250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "한계에", "start_ms": 11650, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "발목을", "start_ms": 12120, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "잡힌", "start_ms": 12620, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "사이", "start_ms": 13050, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "한국은", "start_ms": 13450, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 13950, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "계약하고", "start_ms": 14420, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "빠르게", "start_ms": 15020, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "인", "start_ms": 15480, "dur_ms": 16, "phase_lr_deg": 0.0}
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
"transcript": "드 총리 멤버 무스타파 슐레이만 등등 수많은 실리콘밸리 건물들의 AI에 대한 의견을 담고 있습니다 특히 이 책의 특징은 우리 같은 보통 사람들보다는 기업가 경영인 마케터 또 스타트업의 입장에서 AI를 어떻게 사용해야 될지 주로 이야기한다는 거예요 그래서 이런 비즈니스",
"speaker": "speaker_7",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "드", "start_ms": 150, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "총리", "start_ms": 320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "멤버", "start_ms": 720, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "무스타파", "start_ms": 1250, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "슐레이만", "start_ms": 1850, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "등등", "start_ms": 2550, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "수많은", "start_ms": 3050, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "실리콘밸리", "start_ms": 3580, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "건물들의", "start_ms": 4380, "dur_ms": 620, "phase_lr_deg": 0.0},
{"word": "AI에", "start_ms": 5050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대한", "start_ms": 5550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "의견을", "start_ms": 5980, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "담고", "start_ms": 6450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "있습니다", "start_ms": 6850, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "특히", "start_ms": 7750, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 8120, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "책의", "start_ms": 8280, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "특징은", "start_ms": 8650, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "우리", "start_ms": 9250, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "같은", "start_ms": 9620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "보통", "start_ms": 10020, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "사람들보다는", "start_ms": 10450, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "기업가", "start_ms": 11250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "경영인", "start_ms": 11780, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "마케터", "start_ms": 12350, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 12880, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "스타트업의", "start_ms": 13120, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "입장에서", "start_ms": 13880, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "AI를", "start_ms": 14480, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 14920, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "사용해야", "start_ms": 15380, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "될지", "start_ms": 16020, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "주로", "start_ms": 16380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이야기한다는", "start_ms": 16780, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 17580, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "그래서", "start_ms": 18250, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 18720, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "비즈니스", "start_ms": 19080, "dur_ms": 672, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "음에 리포좀 봉입 기술을 개발해 냈습니다 NMN의 치명적 단점인 갈변과 파괴를 막고 피부 장벽을 뚫고 들어가",
"speaker": "speaker_8",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "음에", "start_ms": 120, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "리포좀", "start_ms": 520, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "봉입", "start_ms": 980, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "기술을", "start_ms": 1350, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "개발해", "start_ms": 1850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "냈습니다", "start_ms": 2320, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "NMN의", "start_ms": 3250, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "치명적", "start_ms": 3880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "단점인", "start_ms": 4380, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "갈변과", "start_ms": 4850, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "파괴를", "start_ms": 5380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "막고", "start_ms": 5880, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "피부", "start_ms": 6450, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "장벽을", "start_ms": 6820, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뚫고", "start_ms": 7320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "들어가", "start_ms": 7720, "dur_ms": 236, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "입니다",
"speaker": "speaker_4",
"confidence": 0.88,
"language": "ko",
"silence_before_ms": 800,
"audio_type": "speech",
"words": [
{"word": "입니다", "start_ms": 800, "dur_ms": 1180, "phase_lr_deg": 0.0}
]
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
"transcript": "AI에 대해 우리는 더 이상 공포심을 가질 필요가 없습니다 트럼프 대통령이 주한미군 철수를 언급한다면 우리는 오히려 이를 대한민국이 어",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "AI에", "start_ms": 150, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "대해", "start_ms": 650, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "우리는", "start_ms": 1020, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1450, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "이상", "start_ms": 1680, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "공포심을", "start_ms": 2050, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "가질", "start_ms": 2680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "필요가", "start_ms": 3080, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "없습니다", "start_ms": 3550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "트럼프", "start_ms": 4450, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "대통령이", "start_ms": 4980, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "주한미군", "start_ms": 5580, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "철수를", "start_ms": 6150, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "언급한다면", "start_ms": 6680, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "우리는", "start_ms": 7450, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "오히려", "start_ms": 7880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "이를", "start_ms": 8350, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "대한민국이", "start_ms": 8680, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "어", "start_ms": 9480, "dur_ms": 1180, "phase_lr_deg": 0.0}
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
"transcript": "공항 진입을 가로막은 할머니에게 대체 입국 심사관은 뭐라고 했던 걸까요 노 소주 예스 소주 깐깐한 심사관은 물론 아이들까지 깜짝 놀라게 한 할머니의 서프라이즈 아이템 여",
"speaker": "speaker_10",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "공항", "start_ms": 150, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "진입을", "start_ms": 580, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "가로막은", "start_ms": 1050, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "할머니에게", "start_ms": 1620, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "대체", "start_ms": 2350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "입국", "start_ms": 2720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "심사관은", "start_ms": 3120, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "뭐라고", "start_ms": 3750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "했던", "start_ms": 4220, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "걸까요", "start_ms": 4580, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "노", "start_ms": 5250, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "소주", "start_ms": 5550, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "예스", "start_ms": 6120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "소주", "start_ms": 6520, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "깐깐한", "start_ms": 7250, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "심사관은", "start_ms": 7780, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "물론", "start_ms": 8380, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "아이들까지", "start_ms": 8820, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "깜짝", "start_ms": 9520, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "놀라게", "start_ms": 9880, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "한", "start_ms": 10350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "할머니의", "start_ms": 10550, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "서프라이즈", "start_ms": 11150, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "아이템", "start_ms": 11880, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "여", "start_ms": 12410, "dur_ms": 786, "phase_lr_deg": 0.0}
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
"transcript": "협력자라는 이 현상을 명쾌하게 설명합니다 맑고 깨끗한 피부 균형 잡힌 외모는 과거 원시 시대",
"speaker": "speaker_11",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "협력자라는", "start_ms": 150, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 880, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "현상을", "start_ms": 1050, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "명쾌하게", "start_ms": 1520, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "설명합니다", "start_ms": 2120, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "맑고", "start_ms": 3150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "깨끗한", "start_ms": 3550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "피부", "start_ms": 4050, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "균형", "start_ms": 4420, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잡힌", "start_ms": 4820, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "외모는", "start_ms": 5250, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "과거", "start_ms": 5750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "원시", "start_ms": 6150, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "시대", "start_ms": 6520, "dur_ms": 444, "phase_lr_deg": 0.0}
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
"transcript": "받고 많은 분들의 기대가 좀 많은 만큼 이게 만약에 안 된다고 하면 실망이 더 커질 거니까 삼성이 조금만 더 갤럭시 유저분들의 마음이 딴 데로 가지 않게 조금 더 신경 써 주시면 좋을 것 같습니다 네 그럼 이번 영상 여기까지 할게요 감사합니다 테크몽이었습니다",
"speaker": "speaker_12",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "받고", "start_ms": 150, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 520, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "분들의", "start_ms": 920, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "기대가", "start_ms": 1380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1880, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "많은", "start_ms": 2080, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "만큼", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 2850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 3220, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "안", "start_ms": 3680, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "된다고", "start_ms": 3910, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 4410, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "실망이", "start_ms": 4780, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 5250, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "커질", "start_ms": 5480, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거니까", "start_ms": 5880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "삼성이", "start_ms": 6380, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "조금만", "start_ms": 6910, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 7380, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "갤럭시", "start_ms": 7610, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "유저분들의", "start_ms": 8110, "dur_ms": 680, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 8850, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "딴", "start_ms": 9320, "dur_ms": 220, "phase_lr_deg": 0.0},
{"word": "데로", "start_ms": 9580, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "가지", "start_ms": 9950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "않게", "start_ms": 10350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 10720, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 11120, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "신경", "start_ms": 11350, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "써", "start_ms": 11720, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "주시면", "start_ms": 11880, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "좋을", "start_ms": 12380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "것", "start_ms": 12780, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "같습니다", "start_ms": 12950, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "네", "start_ms": 13750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "그럼", "start_ms": 13950, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "이번", "start_ms": 14320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "영상", "start_ms": 14720, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "여기까지", "start_ms": 15150, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "할게요", "start_ms": 15750, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "감사합니다", "start_ms": 16220, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "테크몽이었습니다", "start_ms": 17050, "dur_ms": 1050, "phase_lr_deg": 0.0}
]
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
"transcript": "보이는 어떤 상태 플래그를 바꾸는 방식이 되지 않을까라고 예측이 되는 부분들이 있는데 아까도 말씀드렸잖아요 내부적인 다음 조건을 확인한 다음에 FS",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "보이는", "start_ms": 150, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 620, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "상태", "start_ms": 980, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "플래그를", "start_ms": 1410, "dur_ms": 580, "phase_lr_deg": 0.0},
{"word": "바꾸는", "start_ms": 2050, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "방식이", "start_ms": 2520, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "되지", "start_ms": 3020, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "않을까라고", "start_ms": 3380, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "예측이", "start_ms": 4150, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "되는", "start_ms": 4680, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "부분들이", "start_ms": 5080, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "있는데", "start_ms": 5650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "아까도", "start_ms": 6350, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "말씀드렸잖아요", "start_ms": 6820, "dur_ms": 880, "phase_lr_deg": 0.0},
{"word": "내부적인", "start_ms": 7950, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "다음", "start_ms": 8650, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "조건을", "start_ms": 9020, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "확인한", "start_ms": 9520, "dur_ms": 480, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 10050, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "FS", "start_ms": 10550, "dur_ms": 10330, "phase_lr_deg": 0.0}
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
"transcript": "재미있었다 이런 거를 보면 너무 마음이 아파졌어요 볼 때마다 아닙니다 이렇게 듣고 계실지 모르겠지만서도 어 제가 하지만 한계가 있잖아요 이번에 금관전시 있었잖아요 그때 그때 그 담당 학예사님이 저한테 뭐라 그랬었냐 어 흩어져 있던 빌라 왕족들이 다 모",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "재미있었다", "start_ms": 150, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 850, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "거를", "start_ms": 1220, "dur_ms": 280, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 1550, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 1950, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "마음이", "start_ms": 2320, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아파졌어요", "start_ms": 2780, "dur_ms": 720, "phase_lr_deg": 0.0},
{"word": "볼", "start_ms": 3550, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "때마다", "start_ms": 3780, "dur_ms": 420, "phase_lr_deg": 0.0},
{"word": "아닙니다", "start_ms": 4250, "dur_ms": 520, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 4820, "dur_ms": 380, "phase_lr_deg": 0.0},
{"word": "듣고", "start_ms": 5250, "dur_ms": 320, "phase_lr_deg": 0.0},
{"word": "계실지", "start_ms": 5620, "

---
*Generated by WKAppBot ask gemini — 2026-04-05 08:55:51*
