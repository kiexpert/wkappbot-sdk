# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=14936  time=07:14:13  uptime=19754s  mem=444MB  system_processes=357"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_071323.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 312-572-357-756-153-163-130-630-361-637-163-163-106-316-163-163-163-615-165-167-167-165-165-163-136-167-167-163-160-126-106-160-165-165-167-167-165-163-167-165-617-613-167-160-3_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 163-136-136-136-312-613-316-136-763-376-367-136-326-576-321-123-617-631_W.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 230-320-302-320-301-576-302-637-762-127-136-123-267-206-206-370-327-237-367-672-673-315-127-130-163-162-617-106-260-301-176-167-170-326-137-103-203-576-267-750-260-620-675-267-1_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 276-216-267-316-132-213-320-132-123-132-571-201-132-321-172-137-572-270-725-762-627-267-276-726-203-620-235-136-123-103-307-235-501-132-321-302-231-236-206-206-206-173-126-301-2_V.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 765-756-675-657-756-762-675-756-672-675-756-756-765-765 576-576-756-765-657-756-672-267 675-765-762-672-762-762-672-765-762-765-675 627-267-672-672-762-672-267-672-627 267-263-6_W.mp3)
  Segment 6: 7880ms ~ 14792ms (file: 130-317-137-137-137-173-216-120-123-210-213-216-267-123-162-175-267-524-265-526-267-276-267-527-574-752-752-216-172-125-210-216-216-120-125-126-120-215-201-752-157-103-572-765-7_W.mp3)
  Segment 7: 15792ms ~ 16368ms (file: 607-631-675-613-361-637-637-316-367-735-357-751-751-710-576-753-307-305-137-175-173 753-751-107-301-153-137-713-670-361-306-317-713-310-137-107-103-307-130-736-173-301-103-310-3_V.mp3)
  Segment 8: 17368ms ~ 17944ms (file: 675-531-513-135-160-132-627-107-756-750-706-607-610-765-765-765-675-671-617-517-725-263-751-130-301-137-137-317-130-236-507-123-275-132-517-163-571-751-162-750_W.mp3)
  Segment 9: 18944ms ~ 19520ms (file: 316-613-163-612-106-160-160-625-213-123-130-130-130-106-103-310-103-136-316-132-201-261-203-132-136-103_V.mp3)
  Segment 10: 20520ms ~ 21096ms (file: 103 314-316-314-136-314-136-136-134-137-136-135-135-134-136-103-136-103-136-103-130-136-103-136-143-103-103-136 537-751-573-573-571-574-571-571-571 537-175-731-753-571-754-137-1_V.mp3)
  Segment 11: 22096ms ~ 29332ms (file: 572-756-576-756-571-236-256-576-756-754-751-267-276-175-153-150-157-574-527-627-615-276-120-175-261-172-136-621-675-527-756-672-321-175-573-132-571-213-103-206-602-123-103-302-2_W.mp3)
  Segment 12: 30332ms ~ 36128ms (file: 231-231-123-163-103-260-267-326-103-136-163-126-267-367-137-106-103-102-213-132-267-627-103-263-236-326-237-276-236-130-102-106-130-123-356-631-213-312-361-136-165-136-136-103-1_W.mp3)
  Segment 13: 37128ms ~ 45768ms (file: 236-732-160-236-326-160-526-167-137-751-163-576-720-276-327-173-175-167-236-321-756-267-276-237-562-236-236-260-267-751-321-253-130-321-623-275-261-230-573-572-316-265-267-270-7_W.mp3)
  Segment 14: 46768ms ~ 47344ms (file: 317-367-756-756-765-376-736-765-763-736-765-756-765-736-763-765-761-736-176-756-721-765-753-726-736-762-576-756-753-751-573-375-573-753-573-576-576-756-573-753-573-573-527-576_W.mp3)
  Segment 15: 48344ms ~ 55616ms (file: 123-301-317-175-715-607-617-102-267-170-765-326-362-276-230-617-372-736-627-102-263-327-236-213-160-312-136-612-736-137-130-103-176-136-137-135-576-167-130-316-163-137-312-216-2_W.mp3)
  Segment 16: 56616ms ~ 57192ms (file: 750-750-154-715-175-176-167-176-157-567-165-561-567-561-156-517-657-165-157-157-157-175-756-751-175-751-751-750-756-715-751-716-751-751-756-756-756-751-105-157-165-176-175-756-7_W.mp3)
  Segment 17: 58192ms ~ 58768ms (file: 251-572-302-213-102-123-126-623-657-756-265-216-507-267-563-756 572-267-756-275-137-175-231-706-123-561-720-217-570-126-157-612-157-725-567-672-152-251-275-507-517-371-723-127-1_V.mp3)
  Segment 18: 59768ms ~ 60344ms (file: 756-756-756-756 571-571-571-571-571-571-571-571-571-571-517-157-517 574-574-576-571-756-571-572-573-574-574-571-574-574-574 573_W.mp3)
  Segment 19: 61344ms ~ 61920ms (file: 135-157-130-136-135-136-163-136-103-651-107-106-103-215-761-671-617-576-763-260-261-632-136-136-132-136-574-132-162-561-163-631-103-567-162-673-156-136-106-103-173-132-105-136-1_V.mp3)
  Segment 20: 62920ms ~ 63496ms (file: 315-137-153-135-315-135-157-135-137-315-513-135-317-135-531-157-103-517-135-153-103-753-153-135 315-135-351-315-517-301-315-157-137-175-137-301-305-751-315-135-517-175-137-135-5_V.mp3)
  Segment 21: 64496ms ~ 65072ms (file: 157-571-576 507-517-157-150-517-570-571-574-574-574-571-573 150-570-507-570-570-517-570-517-571-571-574 517-570-517-571-571-175-715 107-715-175-751-715-715-751-715-175-715-175-1_W.mp3)
  Segment 22: 66072ms ~ 72156ms (file: 756-627-716-167-176-127-271-237-276-261-672-756-657-206-302-137-103-316-130-265-751-260-216-267-267-621-260-160-623-102-120-132-753-627-201-106-103-201-217-260-205-136-210-276-2_W.mp3)
  Segment 23: 73156ms ~ 73732ms (file: 107-710-137-137-103-170-137-713-731-103-173-170-315-130-517-173-107-375-703-130 301-130-130-310-301-371-173-731-173-371-307-103-103-103-107-137-317-103-351-130-107-370-137-310 1_V.mp3)
  Segment 24: 74732ms ~ 79196ms (file: 173-175-213-523-623-521-672-257-327_V.mp3)
  Segment 25: 80196ms ~ 85020ms (file: 213-574-217-320-507-275-213-173-132-712-712-276-273-106-107-173-312-762-267-263-726-705-765-267-267-103-170-712-756-725-267-752-721-273-217-752-725-762-312-705-237-702-276-271-7_W.mp3)
  Segment 26: 86020ms ~ 86596ms (file: 517-571-571-576-576-576-571-576-576-571-576-571-567-571-570-531-570-570-573-517-572-570-517 570-751-571-517-517-157-513-576-571-570-517-571-571-570-571-571-571-571-517-507-570-5_V.mp3)
  Segment 27: 87596ms ~ 88172ms (file: 105-170-136-312-213-261-216-316-167-317-376-317-136-137-167-637-765-657-765-751-576-675-627-167-137-312-312-750-751-750-750-756-765-715-701-716-315-751-715-715-713-103-103-136-1_W.mp3)
  Segment 28: 89172ms ~ 89748ms (file: 750-716-237-750-762-237-236-623-263-765-261-361-326-726-263-752-321-376-231-236-216-532-312-326-530-132-326-657-627-716-263-726-271-326-261-752-263-126-756-231-132-516-135-123-1_V.mp3)
  Segment 29: 90748ms ~ 91324ms (file: 574-507-507-574-570-507-157-517-157-157-570-751-507-574-574-574-574-517-571-751-507-574-574-574-571-753-507-705-573-570-570-571-751-570 570-574-507-570-570-570-517-751-750-107-7_W.mp3)
  Segment 30: 92324ms ~ 92900ms (file: 163-162-326-765-765-576-756-765-751-715-547-574-574-576-576-574-576-765-716-576-576-576 126-632-316-671-137-103-136-137-316-671-301-136-176-160-103-103-165-607-610-263-103-567-1_W.mp3)
  Segment 31: 93900ms ~ 94476ms (file: 574-574-574-570-571-751-751-750-753_W.mp3)
  Segment 32: 95476ms ~ 96052ms (file: 715-175-716-716-716-157-176-750-750-750-570-750-750-750-105-514-105-105-154-510-150-517-571-156-157-567-157-175-165-165-156-175-150 154-150-105-105-154-150-150-150-150-106-105-1_V.mp3)
  Segment 33: 97052ms ~ 97628ms (file: 621-576-365-675-167-567-657-735-567-675-357-563-513-561-150-516-576-572-513-526-257-265-627-276-652-675-765-612-610-756-571-756-765-716-761-175-715-651-576-657-675-516-765-612-6_W.mp3)
  Segment 34: 98628ms ~ 99204ms (file: 156-573-135 517-750-751-157-157-571-175-573-570-571-751-157-750-175-175-175-157-175-517-157-175 570-576-570-157-517-150-315-175-750-510-175-170-175-157-715-751-751-571-157-175-7_V.mp3)
  Segment 35: 100204ms ~ 106756ms (file: 205-215-321-315-310-310-317-302-213-123-107-123-572-507-123-123-157-126-102-216-157-175-301-312-132-612-126-132-103-132-276-103-167-672-620-627-621-137-726-765-132-103-201-276-2_W.mp3)
  Segment 36: 107756ms ~ 108332ms (file: 715-517-571-751-751-751-576-571-571-571-571-576-570-516-150-315-510-570-157-571-157-573-357-275-175-572-756-275-735-567-763-756-576-156-157-567-175-165-157-571-571-517-571-571-5_V.mp3)
  Segment 37: 109332ms ~ 109908ms (file: 507-501-510-157-517-157-517-507-571-501-507-517-517-501-501-517-517-501-150-150-510-501-510-501-510-507 570-571-570-501-507-105-501-150-501-517-501-501-510-150-510-517-571-571-5_W.mp3)
  Segment 38: 110908ms ~ 116704ms (file: 251-251-257-125-125-267-261-752-217-215-127-726-263-527-231-120-130-213-123-216-236-201-123-132-143-216-276-231-167-732-735-375-753-725-123-271-273-271-253-520-765-215-265-250-1_W.mp3)
  Segment 39: 117704ms ~ 118280ms (file: 167-716-671-716-175-567-215-176-162-176-761-671-761-716-765-761-765-765-763-765-761-765-765-765-716-716-176-671-617-617-671-671-761-167-176-173_W.mp3)
  Segment 40: 119280ms ~ 131160ms (file: 237-527-276-276-237-236-237-320-327-754-756-276-756-765-267-236-726-127-672-367-327-327-176-362-162-160-231-321-312-176-160-574-263-167-617-627-263-231-765-627-267-316-317-267-2_W.mp3)
  Segment 41: 132160ms ~ 132736ms (file: 267-213-756-750 231-123-126-123-123-312-152-105-152-175-175-752-273-207 263-236-623-261-756-375-765-752-576-570-376-365-675-653-576-601-236-236-657-756-507-723 130-310 625-627-6_W.mp3)
  Segment 42: 133736ms ~ 134312ms (file: 265-256-356-352-256-251-325-325-537-527-532-527-352-253-356-362-316-265-625-215-312-132-132-132-136-367-516-563-135-153-516-351-562-562-312-326-132-107-130-310-150-265-236-321-1_W.mp3)
  Segment 43: 135312ms ~ 135888ms (file: 157-105-571-105-157-157-571 517-157-517-105-105-107-750-715-517-571-157-517-157-157-107-107-175-157-715-571 547-571-570-517-501-157-715-751-517-507-570-517-570-510-751-157-105-1_V.mp3)
  Segment 44: 136888ms ~ 137464ms (file: 107-160-130-310-107-371-107-173-732-736-371-706-172-170-173-130-103-312-312-312-162-361-672-261-367-173-751-571-157-713-751-157-157-517-137-137-175-357-375-351-150-153-715-173-5_W.mp3)
  Segment 45: 138464ms ~ 139040ms (file: 613-561-617-105-163-103-156-516-132-136-136-765-567-150-132-301 126-103-103-761-126 253-213 641_V.mp3)
  Segment 46: 140040ms ~ 146916ms (file: 706-672-136-310-756-760-706-513-301-312-260-620-260-267-123-206-206-726-206-235-315-130-517-103-137-103-326-273-120-216-260-620-760-572-213-620-670-216-123-123-670-672-126-130-3_W.mp3)
  Segment 47: 147916ms ~ 148492ms (file: 135-153-125-671-751-175-753-756-710-713-715-175-176-715-715-715_W.mp3)
  Segment 48: 149492ms ~ 156008ms (file: 321-713-526-271-253-132-312-102-123-672-562-236-713-627-256-123-267-526-205-607-705-312-362-637-607-203-263-527-675-216-671-107-765-312-321-123-276-265-123-657-126-201-273-132-1_V.mp3)
  Segment 49: 157008ms ~ 157584ms (file: 725-375-750-176-275-752-357-762-732-725-521-573-215-132-572-756-256-263-750-126-725-265-631-326-756-736-135-576-570 267-672-267-275-507-231-273-376-715-206-372-275-576-571-574-7_V.mp3)
  Segment 50: 158584ms ~ 165748ms (file: 127-102-302-312-371-751-731-310-267-752-107-132-136-132-276-261-106-213-260-206-237-236-673-231-352-621-621-263-261-725-301-321-751-136-107-135-562-635-735-123-165-132-107-560-1_V.mp3)
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
> **Time**: 2026-04-05 07:15:55
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "Okay, you can have a look around.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "Okay", "start_ms": 10, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "you", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "can", "start_ms": 330, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "have", "start_ms": 440, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "a", "start_ms": 550, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "look", "start_ms": 610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "around", "start_ms": 770, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "This is the biggest room actually, I think.",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "en",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "This", "start_ms": 20, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "is", "start_ms": 180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "biggest", "start_ms": 400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "room", "start_ms": 660, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "actually", "start_ms": 870, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 1250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "think", "start_ms": 1360, "dur_ms": 200, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "And this one also has shelves.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "And", "start_ms": 15, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "this", "start_ms": 145, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "one", "start_ms": 305, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "also", "start_ms": 450, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "has", "start_ms": 710, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "shelves", "start_ms": 920, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "So I had the better room.",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "So", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 190, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "had", "start_ms": 300, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "the", "start_ms": 460, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "better", "start_ms": 570, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "room", "start_ms": 830, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "disponible sur Netflix dispo en français et en VO.",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "fr",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "disponible", "start_ms": 40, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "sur", "start_ms": 500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "Netflix", "start_ms": 660, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "dispo", "start_ms": 1100, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "en", "start_ms": 1410, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "français", "start_ms": 1520, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "et", "start_ms": 1930, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "en", "start_ms": 2040, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "VO", "start_ms": 2150, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "Oui je vous le dis quand c'est disponible en français parce que vous êtes nombreux à me le demander.",
"speaker": "speaker_2",
"confidence": 0.94,
"language": "fr",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "Oui", "start_ms": 50, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "je", "start_ms": 210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "vous", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "le", "start_ms": 480, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "dis", "start_ms": 590, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "quand", "start_ms": 750, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "c'est", "start_ms": 910, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "disponible", "start_ms": 1070, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "en", "start_ms": 1530, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "français", "start_ms": 1640, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "parce", "start_ms": 2050, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "que", "start_ms": 2260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "vous", "start_ms": 2370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "êtes", "start_ms": 2530, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "nombreux", "start_ms": 2690, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "à", "start_ms": 3050, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "me", "start_ms": 3110, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "le", "start_ms": 3220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "demander", "start_ms": 3330, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "Là ne passez pas à côté vous avez le choix mais s'il vous plaît essayez",
"speaker": "speaker_2",
"confidence": 0.93,
"language": "fr",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "Là", "start_ms": 60, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "ne", "start_ms": 220, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "passez", "start_ms": 330, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "pas", "start_ms": 640, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "à", "start_ms": 800, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "côté", "start_ms": 860, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "vous", "start_ms": 1170, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "avez", "start_ms": 1330, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "le", "start_ms": 1540, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "choix", "start_ms": 1650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "mais", "start_ms": 1910, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "s'il", "start_ms": 2070, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "vous", "start_ms": 2180, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "plaît", "start_ms": 2290, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "essayez", "start_ms": 2500, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "알루미늄의 특성에 의해서 좀 더 우주에 적합하다라는 의견이 있긴 한데 이게 배터리 셀로 만들었을 때 특성을 보면 특별히",
"speaker": "speaker_3",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "알루미늄의", "start_ms": 20, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "특성에", "start_ms": 580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "의해서", "start_ms": 940, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 1250, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1410, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "우주에", "start_ms": 1570, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "적합하다라는", "start_ms": 1880, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "의견이", "start_ms": 2440, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "있긴", "start_ms": 2750, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "한데", "start_ms": 2960, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "이게", "start_ms": 3170, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "배터리", "start_ms": 3380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "셀로", "start_ms": 3690, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "만들었을", "start_ms": 3950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 4310, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "특성을", "start_ms": 4470, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "보면", "start_ms": 4780, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "특별히", "start_ms": 5040, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "하는데 여기서도 추가적으로 어떻게 더 업그레이드가 됐는지 주요 관전 포인트가 될 거 같고요",
"speaker": "speaker_4",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "하는데", "start_ms": 25, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "여기서도", "start_ms": 380, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "추가적으로", "start_ms": 790, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떻게", "start_ms": 1300, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "더", "start_ms": 1610, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "업그레이드가", "start_ms": 1770, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "됐는지", "start_ms": 2330, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "주요", "start_ms": 2690, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "관전", "start_ms": 2950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "포인트가", "start_ms": 3210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "될", "start_ms": 3570, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 3730, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같고요", "start_ms": 3840, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "박민우 이제 본부장과 로봇 자회사 보스턴 다이내믹스의 기술 수장이 직접 무대에 올라 기술",
"speaker": "speaker_4",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "박민우", "start_ms": 30, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이제", "start_ms": 390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "본부장과", "start_ms": 650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "로봇", "start_ms": 1110, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "자회사", "start_ms": 1370, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보스턴", "start_ms": 1780, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "다이내믹스의", "start_ms": 2090, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 2650, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수장이", "start_ms": 2910, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "직접", "start_ms": 3270, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "무대에", "start_ms": 3580, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "올라", "start_ms": 3940, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "기술", "start_ms": 4200, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "문서를 봤는데 이건 사실일지는 잘 모르겠고 흔히 우리가 옛날에도 안드로이드 폰 탈옥한다 뭐 이런 얘기들이 있잖아요",
"speaker": "speaker_5",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "문서를", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "봤는데", "start_ms": 380, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이건", "start_ms": 740, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "사실일지는", "start_ms": 1000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 1510, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "모르겠고", "start_ms": 1670, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "흔히", "start_ms": 2080, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "우리가", "start_ms": 2340, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "옛날에도", "start_ms": 2650, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "안드로이드", "start_ms": 3110, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "폰", "start_ms": 3620, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "탈옥한다", "start_ms": 3780, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "뭐", "start_ms": 4240, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 4350, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "얘기들이", "start_ms": 4610, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있잖아요", "start_ms": 5020, "dur_ms": 400, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "FS가 제일",
"speaker": "speaker_5",
"confidence": 0.92,
"language": "en",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "FS가", "start_ms": 10, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제일", "start_ms": 420, "dur_ms": 300, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "(mixed)",
"speaker": "speaker_6",
"confidence": 0.85,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "mixed",
"words": [
{"word": "어쨌든", "start_ms": 50, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 460, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 720, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "아이디어를", "start_ms": 980, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "가지고", "start_ms": 1490, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "왔을", "start_ms": 1850, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 2110, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "만약에", "start_ms": 2270, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "선배님이", "start_ms": 2630, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "거기서", "start_ms": 3090, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 3450, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "그건", "start_ms": 3560, "dur_ms": 250, "phase_lr_deg": 0.0}
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
"transcript": "제가 만든 김치 먹을 수 있어요? 음 I I only did touch",
"speaker": "speaker_7",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "제가", "start_ms": 20, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "만든", "start_ms": 280, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "김치", "start_ms": 590, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "먹을", "start_ms": 950, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 1210, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있어요?", "start_ms": 1320, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "음", "start_ms": 1800, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 2100, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "I", "start_ms": 2250, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "only", "start_ms": 2400, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "did", "start_ms": 2700, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "touch", "start_ms": 2900, "dur_ms": 350, "phase_lr_deg": 0.0}
]
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
"transcript": "야 히 자 이렇게 둘이",
"speaker": "speaker_8",
"confidence": 0.90,
"language": "ko",
"silence_before_ms": 10,
"audio_type": "speech",
"words": [
{"word": "야", "start_ms": 10, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "히", "start_ms": 200, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "자", "start_ms": 400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "이렇게", "start_ms": 600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "둘이", "start_ms": 1000, "dur_ms": 300, "phase_lr_deg": 0.0}
]
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
"transcript": "지난 20년 동안 출전한 마라톤 대회만 500여 회 입상해서 받은 메달이 200",
"speaker": "speaker_9",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "지난", "start_ms": 15, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "20년", "start_ms": 380, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "동안", "start_ms": 790, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "출전한", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "마라톤", "start_ms": 1560, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "대회만", "start_ms": 1970, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "500여", "start_ms": 2380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "회", "start_ms": 2840, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "입상해서", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "받은", "start_ms": 3510, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "메달이", "start_ms": 3820, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "200", "start_ms": 4230, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "리스를 켜게 되는데 그냥 그 플래그들 이 지금 지역이 어떻냐 차량 구성이 어떻냐 운전자 상태가 어떻냐 이런 것들을 그런 플래그들을 바꾸면서 했을 때 어떤 내부의 어떤 캔이라든가 플렉스레이라든가 이런 메시지",
"speaker": "speaker_10",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "리스를", "start_ms": 25, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "켜게", "start_ms": 380, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "되는데", "start_ms": 640, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그냥", "start_ms": 1000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "그", "start_ms": 1260, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "플래그들", "start_ms": 1370, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 1830, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 1940, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "지역이", "start_ms": 2200, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떻냐", "start_ms": 2560, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "차량", "start_ms": 2920, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "구성이", "start_ms": 3230, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떻냐", "start_ms": 3640, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "운전자", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "상태가", "start_ms": 4410, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어떻냐", "start_ms": 4820, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 5200, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "것들을", "start_ms": 5460, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "그런", "start_ms": 5820, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "플래그들을", "start_ms": 6080, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "바꾸면서", "start_ms": 6640, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "했을", "start_ms": 7100, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 7360, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 7520, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "내부의", "start_ms": 7780, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 8140, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "캔이라든가", "start_ms": 8400, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "플렉스레이라든가", "start_ms": 8960, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이런", "start_ms": 9670, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "메시지", "start_ms": 9930, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "전화 소식이 처음 나왔을 때 일본 팬들은 환호는커녕 걱정부터 폭발했습니다 일본이 또 한국 작품을 리메이크한다면 이미 결과를 너무 잘 알고 있었기 때문",
"speaker": "speaker_11",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 15,
"audio_type": "speech",
"words": [
{"word": "전화", "start_ms": 15, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "소식이", "start_ms": 320, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "처음", "start_ms": 680, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "나왔을", "start_ms": 990, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때", "start_ms": 1350, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "일본", "start_ms": 1510, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "팬들은", "start_ms": 1820, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "환호는커녕", "start_ms": 2230, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "걱정부터", "start_ms": 2840, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "폭발했습니다", "start_ms": 3300, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "일본이", "start_ms": 4010, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 4370, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "한국", "start_ms": 4530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "작품을", "start_ms": 4840, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "리메이크한다면", "start_ms": 5200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "이미", "start_ms": 5910, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "결과를", "start_ms": 6170, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 6530, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 6840, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "알고", "start_ms": 7000, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "있었기", "start_ms": 7260, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "때문", "start_ms": 7620, "dur_ms": 250, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "골라서 똑똑하게 제공을 해 주는 겁니다 말한테 이 밭 전체를 갈아줘라고 하는 게 아니라 지금 갈아야 할",
"speaker": "speaker_12",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 20,
"audio_type": "speech",
"words": [
{"word": "골라서", "start_ms": 20, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "똑똑하게", "start_ms": 380, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "제공을", "start_ms": 840, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "해", "start_ms": 1200, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "주는", "start_ms": 1310, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "겁니다", "start_ms": 1570, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "말한테", "start_ms": 1930, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이", "start_ms": 2290, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "밭", "start_ms": 2400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "전체를", "start_ms": 2560, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "갈아줘라고", "start_ms": 2970, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하는", "start_ms": 3580, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 3840, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "아니라", "start_ms": 3950, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "지금", "start_ms": 4310, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "갈아야", "start_ms": 4620, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "할", "start_ms": 4980, "dur_ms": 150, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "보면은 시장 대응이 어려워요 굉장히 또 왔다 갔다 또 기준이 없을 수 있기 때문에 밸런스 있게 좀 잘 보시면 좋을 거 같고 하지만 아 너무 어렵다 전쟁에 대해서 어렵다 하지만 이슈가",
"speaker": "speaker_13",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "보면은", "start_ms": 25, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "시장", "start_ms": 380, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "대응이", "start_ms": 690, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어려워요", "start_ms": 1050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "굉장히", "start_ms": 1460, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 1820, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "왔다", "start_ms": 1980, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "갔다", "start_ms": 2240, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 2500, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "기준이", "start_ms": 2660, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "없을", "start_ms": 3020, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "수", "start_ms": 3280, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "있기", "start_ms": 3390, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "때문에", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "밸런스", "start_ms": 4010, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "있게", "start_ms": 4420, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "좀", "start_ms": 4680, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "잘", "start_ms": 4840, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "보시면", "start_ms": 5000, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋을", "start_ms": 5360, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거", "start_ms": 5620, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "같고", "start_ms": 5730, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 5990, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "아", "start_ms": 6350, "dur_ms": 100, "phase_lr_deg": 0.0},
{"word": "너무", "start_ms": 6460, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "어렵다", "start_ms": 6770, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "전쟁에", "start_ms": 7130, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "대해서", "start_ms": 7490, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "어렵다", "start_ms": 7850, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 8210, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "이슈가", "start_ms": 8570, "dur_ms": 350, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-05 07:15:55*
