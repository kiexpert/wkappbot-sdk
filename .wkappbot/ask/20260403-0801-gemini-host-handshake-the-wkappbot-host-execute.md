# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=08:00:57  uptime=24102s  mem=1523MB  system_processes=445"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_080047.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 21564ms (file: 321-627-236-507-165-135-312-317-263-623-263 376 261-672-136-106-321-132-310-136-103-173-763-762-673-631-103-136-572-627-175-710-173-136-135-316-167-106-163-576-512-130-137-103-1_V.mp3)
  Segment 2: 22564ms ~ 23140ms (file: 160-106-761-752-671-160-657-316-361-126-163-617-261-627-762-612-123-120-321-167-163 102-102-132-316-136-163-162-163-167-321-132-132-132-103 756-576-765-765-657-561-265-637-326-6_V.mp3)
  Segment 3: 24140ms ~ 24716ms (file: 701-103-103-103-103-103-106-163-106-167-103-106-236-137-167-167-162-106-107-106-106-167-103-231-230-102-136-137-130-132-136-127-132-106-167-367-631-571-601-175-103-103-103-136-1_V.mp3)
  Segment 4: 25716ms ~ 26292ms (file: 627-603-612-162-627-675-672-673-651-106-136-672-162-167-106-612-605-167-632-627-723-607-657-163-136-631-623-612-615-615-651-613-517-574-576-576-576-576-576-175-137-137-301-316-1_V.mp3)
  Segment 5: 27292ms ~ 27868ms (file: 102-132-310-310-130-120-130-130-310-306-102-216-620-136-160-160-106-106-102-132-312-320-312-302-132-102-103-130-632-261-236-602-261-206-260-230-312-301-312-132-302-301-730-735-3_V.mp3)
  Segment 6: 28868ms ~ 29444ms (file: 716-712-102-705-127-130-160-157-156-261-123-236-126-310-130-123-312-312-102-531-572-576-716-213-132-621 213_W.mp3)
  Segment 7: 30444ms ~ 31020ms (file: 103-130-103-610-657-167-521-132-103-106-106-103-103-532-153-126-136-106-716-167-672-130-751-167-127-167 761-103-103-130-103-123-130-132-657-571-167-163-671-136-136-176-107-156-1_V.mp3)
  Segment 8: 32020ms ~ 32596ms (file: 623-672-306-302-136-301-162-156-312-132-631-163-706-316-621-136-276-132-163-612-126-231-163-162-260-230-120-263-621-216-612-236-265-267-265-623-267-213-263-231-216-613-312 170-1_W.mp3)
  Segment 9: 33596ms ~ 34172ms (file: 312-312-675-675 362-361-301-316-132 657-517-163-310-263-307-602-671-123-132-236-206-312-305-307-267-203-326-301-132-213-132-123-602-210-504-706-317-320-602-367-620-123-106-102-1_V.mp3)
  Segment 10: 35172ms ~ 35748ms (file: 130-103-310-130-310-310-306-103-312-312-316-630-263-625-267-263-265-632-675-571-574-326-327-326-703-573-756-130-206-236-213-236-102-106-170-160-106-301-301-531-135-350-231-321-3_W.mp3)
  Segment 11: 36748ms ~ 37324ms (file: 126-361-653-625-675-361-137-123-163-216-623-267-167-137-160-561-321-631-162-132-312-632-316-126-612-627-213-162-623-736-621-321-361-123-236-326-326-612-132-132-126-601-163-623-3_W.mp3)
  Segment 12: 38324ms ~ 38900ms (file: 132-312-231-132-321-132-754-756-756-675-607-672-675-675-627-752-672-276-362-361-136-136-316-135-375-672-263-263-316-176-620-167-610-167-301-160-163-316-716-721-126-321-163-162-3_V.mp3)
  Segment 13: 39900ms ~ 40476ms (file: 160-571-675-625-623-523-230-524-301-310-302-620-653-352-302-326-754-357-327-325-320-352-547-567-574-756-675-513-253-756-317-137-135-103-203-203-203-236-523-320-236-316-162-163-2_W.mp3)
  Segment 14: 41476ms ~ 42052ms (file: 236-671-362 206-326-236-627-206 103-103-103-130-130-103-231-302-306-302-321-302-231-213-236-213-206 673-236-263 132_V.mp3)
  Segment 15: 43052ms ~ 43628ms (file: 315-135-135-315-315-357-315-317-563-567-576-527-673-315-157-153-537-756-571-571-537-153-312-375-135-132-136-517-517-157-563-365-657-356-534-541-534-325-325-351-352-325-352-312-3_W.mp3)
  Segment 16: 44628ms ~ 45204ms (file: 537-572-573-675-263-527-572-263-236-326-231-203-312-316-103-102-571-725-275-326-320-326-320-236-326-263-103-310-301 326-302-574-520-520-541-576-526-652-327-504-571-567-506-506-6_W.mp3)
  Segment 17: 46204ms ~ 46780ms (file: 135-260-260-265-627-625-672-165-612-162-675-602-765-670-610-106-602-625-574-754-305-103-103-153-156-657-576-567-152-123-216-610-621-627-132-326-361-163-310-263-261-361-316-751-7_W.mp3)
  Segment 18: 47780ms ~ 48356ms (file: 123-671-176 176-163-106-103-321-236-162-167-103-136-167-103-106-106-136-103-165-567-167-126-165-216-165-102-156-263_V.mp3)
  Segment 19: 49356ms ~ 49932ms (file: 576-756-570-576-573-507-506-574-572-167-176-675-576-576-627-620-650-562-506-650-720-725-163-631-716 673-617-675-765-162-123-136-316-236-263-612-621-156-326-132-103-106-167-132 1_W.mp3)
  Segment 20: 50932ms ~ 51508ms (file: 103-132-130-130-132-103 136-103-130-103-132-103-301-312-132-620-260-210-102-206-527-675-136-310-321-310-302-310-301-103-136-102-203-230-231-630-362 103-136-163-163-136-312-103-6_V.mp3)
  Segment 21: 52508ms ~ 53084ms (file: 362-203-231-130-103-126-162-162-756-765-102-103-163-621-236-612-126-163-613-162-612-136-306-360-630-306-321-365-632-576-576-651-163 231-310-301-106-613-136-136-326-362-130-106-6_W.mp3)
  Segment 22: 54084ms ~ 54660ms (file: 310-132-301-132-136-621-162-631-632-602-612-621-612-162-106-132-130-103-132-136-103-163-102-103-123-312-213-235-532-130-103-130-316-130-102-173-163-167-507-547-574-570-576-317-2_V.mp3)
  Segment 23: 55660ms ~ 56236ms (file: 162-106-261-617-267-672-625-267-627-621-675-657-627-672-675-627-763-675-267-631-675-267-627-627-627-627-672-762-627-625-627-627-625-765-675-126-672-762-672-675-621-765-627-652-6_W.mp3)
  Segment 24: 57236ms ~ 57812ms (file: 671-617-176-136-123-203-301-310-312-302-632-326-263-203-320-236-203-230-206-206-203-230-326-326-310-301-103-103-106-103-510-127-501-150-546-504-132-375-752-263-260-263-574-765-2_V.mp3)
  Segment 25: 58812ms ~ 59388ms (file: 201-230-231-206-106-301-310-613-601-106-301-312-216-132-132-653-176-162-126-236-137-136-617-312-173-126-125-162-362-625-325-567-567-567-156-130-130-106-321-615-517-576-127-657-1_W.mp3)
  Segment 26: 60388ms ~ 60964ms (file: 675-576-576-657-657-126-136-106-231-567-567-612-261-157-671-675-761-756-675-726-175-613-632-256-267-752-526-652-657-576-652-276-562-672-756-657-675-157-567-657-576-675-637-130-1_W.mp3)
  Segment 27: 61964ms ~ 62540ms (file: 413-263-260-321-620-306-632-302-326-630-301-162-316-621-263-326-231-123-231-326-203-236-320-132-320-203-237-213-321-301-103-132-371-302-602-612-106-162-163-106-576-312-123-601-6_V.mp3)
  Segment 28: 63540ms ~ 64116ms (file: 756-657-675-326-312-230-312-136-123-160-360-316-360-362-103-160-160-106-106-103-106-106-610-261-602-602-102-302-302-301-376-362-102-610-160-162-612-301-260-120-260-206-206-230-2_V.mp3)
  Segment 29: 65116ms ~ 65692ms (file: 312-136-516-507-136-613-316-132-123-263-136-132-360-316-123-312-203-130-130-130-316-573-351-310-632-160-136-321-637-103-126-123-103-356-617-162-163-132-136-312-103-136-132-132-1_V.mp3)
  Segment 30: 66692ms ~ 67268ms (file: 621-627-623-621-612-613-362-263-261-621-236-362-621-623-326-216-236-261-623-261-612-263_W.mp3)
  Segment 31: 68268ms ~ 68844ms (file: 257-256-263-215-216-321-327-325-736-136-137-312-213-312-273-726-172-372-752-256-257-267-273-372-327-273-237-325-236-237-271-273-257-235-236-236-236-213-275-237-217-273-327-312-3_W.mp3)
  Segment 32: 69844ms ~ 70420ms (file: 625-612-136-267-123 107-762-120-167-163-163-126-106-106-162-167-106-103-130-136-125-126-620-127-162-103-126-163-106-765-163-760 132-103-103-163-106-651-351-102-103-106-136-106-1_V.mp3)
  Segment 33: 71420ms ~ 71996ms (file: 263-765-762-675-672-367-236-163-205-657-257-261-765-756-765-507-570-673-574-765-625-725-506-570-750-527-267-260-213-216-162-136-601-132-137-136-132-571-617-160-612-216-216-602-2_W.mp3)
  Segment 34: 72996ms ~ 73572ms (file: 263-267-237-276-276-216-613-621-163-136-132-123-627-675-672-627-673-267-132-312-132-213-263-627-762-267-637-320-652-527-573-627-312-123-765-670-163-167-163-106-761-726-613-136-1_V.mp3)
  Segment 35: 74572ms ~ 75148ms (file: 167-107-106-136-263-632-657-576-576-576-567-267-627-267_W.mp3)
  Segment 36: 76148ms ~ 89000ms (file: 106-162-132-172-176 126-136-167-276-526-265-623-632-102-763-137-106-316-671-167-163-132-170-160-103-623-162-165-167-160-576-263 613-107-126-167-160-176-106-106-106-654-106-567-1_V.mp3)
  Segment 37: 90000ms ~ 90576ms (file: 613-162-362-261-612-321-631-213-163-521-762-627-163-312-261-627-627-267-263 627-627-627-617-627-621-627-623-632-627-612-167-176-327-126-210-201-126-120-210-102 125-261-312-321-1_W.mp3)
  Segment 38: 91576ms ~ 92152ms (file: 527-253-527-562-265-265-261-267-256-236-271-213-210-201-537-571-573-537-574-576-537-532-321-623-567-576-567-257-527-321-312-327-321-312-321-312-271-213-321-317-273-571-573-573-5_W.mp3)
  Segment 39: 93152ms ~ 93728ms (file: 672-621-216-521-157-756-526-567_W.mp3)
  Segment 40: 94728ms ~ 95304ms (file: 261-673-637-673-371-756-175-217-172-176-132-675-762-675-657-627-756-765-132-103-137-371-312-317-310-316-361-213-361-756-576-263-265-627-625-672-567-756-627-673-623-267-236 267-6_W.mp3)
  Segment 41: 96304ms ~ 108040ms (file: 620-602-627-657-612-607-625-672 675-657-216-126-176-571 620-621-602-602-231-321-321-231-321-213-321-213-162-326-261-231-132-672-631-637-367-637-361-632-367-635-365-653-567-657-6_W.mp3)
  Segment 42: 109040ms ~ 109616ms (file: 126-730-763-706-167-576-176-361-365-320-237-320-306-320-326-316-612-301-756-572-725-217-615-371-375-526-325-657-657-376-675-175-657-163-567-361-327-321-356-576-657-536-357-536-5_W.mp3)
  Segment 43: 110616ms ~ 111192ms (file: 236-120-123-132-102-106-312-102-301-123-312-613-136-302-230-103-306-103-136-317-102-130-136-130-160-316-160-107-106-103-316-301-362-301-312-302-312-302-637-203-263-275-320-236-2_V.mp3)
  Segment 44: 112192ms ~ 112768ms (file: 756-657-216-126-321-657-576-657-673-765-672-576-756-675-267 312-126-631-301-312-162-163-567-576-276-256-625-752-675-675-756-576-756-675-567-765-675-527-621-756-527-657-762-672-7_W.mp3)
  Segment 45: 113768ms ~ 114344ms (file: 675-675-160-176-617-160-610-627-627-167-613 163-167-160-167-612_W.mp3)
  Segment 46: 115344ms ~ 115920ms (file: 362-365-567-567-160-316-163-236-263-236-260-263-265-267-260-625-276-672-652-567-351-613-627-123-163-265-527-315-312-321-326-236-237-265-265-237-320-326-237-230-125-261-261-326-3_W.mp3)
  Segment 47: 116920ms ~ 117496ms (file: 516-167-162-102-136-130-167-103-167-671-671-675-136-361-367-106-671-156-621-610-627-620-136-163-671-316-306-152-213-132-306-163-167-103-137-157-176-165-126-163-102-504-514-514-6_W.mp3)
  Segment 48: 118496ms ~ 119072ms (file: 230-203-206-203-527-752-170-102-123-360-321-231-261-520-572-752-172-130-321-320-756-263-263-123-126-123-203-231-320-320-230-320-230-203-236-263-326-310-320-235-532-135-103-163-1_W.mp3)
  Segment 49: 120072ms ~ 120648ms (file: 610-103-107-652-156-106-103-103-136-103-132-165-701-157-160-165-602-103-163-123-103-163-617-165-103-617-137-165-162-613-103-136-106-106-103-136-103-103-132-130-132-106-106-671-3_V.mp3)
  Segment 50: 121648ms ~ 122224ms (file: 621-574-675-675-675-607-650-607-657-675-613-167-160-651-572-524-567-627-675-576-256-657-652-567-567-657-657-156-365-132-623-632-637 564-625-652-612 652-546-542-205-672-625-657-6_W.mp3)
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
> **Time**: 2026-04-03 08:01:59
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "오늘 배운 내용을 간단히 정리해 봅시다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 160, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 290, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "간단히", "start_ms": 440, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "정리해", "start_ms": 600, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "봅시다", "start_ms": 770, "dur_ms": 130, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "주격 조사와 목적격 조사를 배웠어요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "주격", "start_ms": 35, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "조사와", "start_ms": 170, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "목적격", "start_ms": 320, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "조사를", "start_ms": 480, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "배웠어요", "start_ms": 610, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 3,
"transcript": "이 가 그리고 을 를의 차이를 알겠죠",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "이", "start_ms": 50, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "가", "start_ms": 110, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "그리고", "start_ms": 180, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "을", "start_ms": 330, "dur_ms": 50, "phase_lr_deg": 0.0},
{"word": "를의", "start_ms": 390, "dur_ms": 90, "phase_lr_deg": 0.0},
{"word": "차이를", "start_ms": 490, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "알겠죠", "start_ms": 610, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "네 받침 유무에 따라 구분하는 게 핵심이에요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 30, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "받침", "start_ms": 100, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "유무에", "start_ms": 220, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "따라", "start_ms": 360, "dur_ms": 100, "phase_lr_deg": 5.0},
{"word": "구분하는", "start_ms": 470, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "게", "start_ms": 620, "dur_ms": 40, "phase_lr_deg": 5.0},
{"word": "핵심이에요", "start_ms": 670, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 5,
"transcript": "정말 똑똑하시네요 복습도 완벽합니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 40, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "똑똑하시네요", "start_ms": 160, "dur_ms": 240, "phase_lr_deg": 0.0},
{"word": "복습도", "start_ms": 410, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "완벽합니다", "start_ms": 550, "dur_ms": 230, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "선생님이 잘 가르쳐 주신 덕분이에요",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "선생님이", "start_ms": 25, "dur_ms": 160, "phase_lr_deg": 5.0},
{"word": "잘", "start_ms": 195, "dur_ms": 70, "phase_lr_deg": 5.0},
{"word": "가르쳐", "start_ms": 275, "dur_ms": 130, "phase_lr_deg": 5.0},
{"word": "주신", "start_ms": 415, "dur_ms": 90, "phase_lr_deg": 5.0},
{"word": "덕분이에요", "start_ms": 515, "dur_ms": 210, "phase_lr_deg": 5.0}
]
},
{
"segment": 7,
"transcript": "오늘 수업은 여기서 마칠게요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업은", "start_ms": 180, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "여기서", "start_ms": 320, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "마칠게요", "start_ms": 450, "dur_ms": 190, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "질문이 생기면 언제든지 메시지 주세요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "질문이", "start_ms": 35, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "생기면", "start_ms": 165, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "언제든지", "start_ms": 305, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "메시지", "start_ms": 455, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 595, "dur_ms": 110, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "네 감사합니다 안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 40, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "감사합니다", "start_ms": 110, "dur_ms": 240, "phase_lr_deg": 5.0},
{"word": "안녕히", "start_ms": 360, "dur_ms": 140, "phase_lr_deg": 5.0},
{"word": "계세요", "start_ms": 510, "dur_ms": 180, "phase_lr_deg": 5.0}
]
},
{
"segment": 10,
"transcript": "조심히 들어가세요 또 봐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "조심히", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "들어가세요", "start_ms": 200, "dur_ms": 210, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 420, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 490, "dur_ms": 120, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 12,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 13,
"transcript": "오늘 수업도 정말 즐거웠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 60, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "수업도", "start_ms": 180, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 310, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "즐거웠습니다", "start_ms": 430, "dur_ms": 240, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "다음 수업도 기대가 되네요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "다음", "start_ms": 25, "dur_ms": 110, "phase_lr_deg": 5.0},
{"word": "수업도", "start_ms": 145, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "기대가", "start_ms": 275, "dur_ms": 120, "phase_lr_deg": 5.0},
{"word": "되네요", "start_ms": 405, "dur_ms": 140, "phase_lr_deg": 5.0}
]
},
{
"segment": 15,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 16,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 17,
"transcript": "푹 쉬고 다음에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "푹", "start_ms": 45, "dur_ms": 70, "phase_lr_deg": 0.0},
{"word": "쉬고", "start_ms": 125, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "다음에", "start_ms": 245, "dur_ms": 130, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 385, "dur_ms": 60, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 455, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "안녕히 가세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "가세요", "start_ms": 190, "dur_ms": 380, "phase_lr_deg": 5.0}
]
},
{
"segment": 19,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 20,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 210, "dur_ms": 370, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 22,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 23,
"transcript": "행복한 하루 보내세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "행복한", "start_ms": 55, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "하루", "start_ms": 205, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "보내세요", "start_ms": 335, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "안녕히 계세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 35, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "계세요", "start_ms": 195, "dur_ms": 370, "phase_lr_deg": 5.0}
]
},
{
"segment": 25,
"transcript": "조금 이따 봐요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 45, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "이따", "start_ms": 165, "dur_ms": 120, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 295, "dur_ms": 140, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 27,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 28,
"transcript": "언제든지 연락해 주세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "언제든지", "start_ms": 60, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "연락해", "start_ms": 230, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "주세요", "start_ms": 380, "dur_ms": 180, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "기다리고 있겠습니다",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "기다리고", "start_ms": 30, "dur_ms": 180, "phase_lr_deg": 0.0},
{"word": "있겠습니다", "start_ms": 220, "dur_ms": 350, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 31,
"transcript": "네 선생님도 푹 쉬세요",
"speaker": "speaker_2",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "네", "start_ms": 50, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "선생님도", "start_ms": 120, "dur_ms": 150, "phase_lr_deg": 5.0},
{"word": "푹", "start_ms": 280, "dur_ms": 60, "phase_lr_deg": 5.0},
{"word": "쉬세요", "start_ms": 350, "dur_ms": 170, "phase_lr_deg": 5.0}
]
},
{
"segment": 32,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 33,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 40, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 200, "dur_ms": 330, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 35,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 36,
"transcript": "안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 25,
"audio_type": "speech",
"words": [
{"word": "안녕", "start_ms": 25, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 55, "dur_ms": 340, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 39,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 40,
"transcript": "모두 건강하세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "모두", "start_ms": 35, "dur_ms": 110, "phase_lr_deg": 0.0},
{"word": "건강하세요", "start_ms": 155, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 42,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 43,
"transcript": "다음에 또 만나요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "다음에", "start_ms": 45, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 215, "dur_ms": 80, "phase_lr_deg": 0.0},
{"word": "만나요", "start_ms": 305, "dur_ms": 210, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 45,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 46,
"transcript": "안녕히 가세요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 30, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "가세요", "start_ms": 190, "dur_ms": 380, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "(noise)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
},
{
"segment": 48,
"transcript": "(music)",
"speaker": "unknown",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 49,
"transcript": "진짜 안녕",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 50, "dur_ms": 140, "phase_lr_deg": 0.0},
{"word": "안녕", "start_ms": 200, "dur_ms": 360, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "바이 바이",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "en",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "바이", "start_ms": 40, "dur_ms": 160, "phase_lr_deg": 0.0},
{"word": "바이", "start_ms": 210, "dur_ms": 370, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 08:01:59*
