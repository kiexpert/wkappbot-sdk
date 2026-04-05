# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=6944  time=07:10:22  uptime=19631s  mem=376MB  system_processes=354"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260405_070832.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 762-736-763-763-762-765-762-762-765-762-762-726-762-276-726-762-725-726-726-762-762-275-726-725-763-725-726_W.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 715-175-713-175-751-175-715-571-571-571-751 751-756-170-531-710-751-175-713-713-751-710-715-137-715-735-753-715-715-175-173-751-715-715-175-153-175-571-751-751 570-750-715-710-7_W.mp3)
  Segment 3: 3152ms ~ 12836ms (file: 320-231-275-216-507-326-523-321-201-103-312-215-123-123-670-216-216-310-573-752-203-213-507-754-574-132-250-620-762-125-123-321-537-260-213-126-120-102-103-130-276-213-213-120-2_V.mp3)
  Segment 4: 13836ms ~ 14412ms (file: 130-105-153-135-103-140-103-143-135-153-135-143-134 341-314-134-314-130-134-135-134-103-104-105-105-134-154-103-134-103-104-143-103-103-103-135-103-137-103-135 361-136-134-130-1_V.mp3)
  Segment 5: 15412ms ~ 23476ms (file: 726-276-275-675-102-102-102-320-307-312-321-126-756-765-120-132-576-526-615-126-102-123-132-302-321-312-312-267-216-167-106-106-671-765-167-176-267-267-263-325-756-210-672-206-2_W.mp3)
  Segment 6: 24476ms ~ 31496ms (file: 216-231-263-576-265-136-321-136-163-132-321-615-236-576-136-367-357-216-170-312-312-260-765-126-753-317-312-132-316-123-361-310-132-136-726-756-130-261-576-265-103-751-326-236-2_W.mp3)
  Segment 7: 32496ms ~ 33072ms (file: 301-137-134-103-103-130-130-314-317-314-314-134-314-134-314 315-374-310-104-103-317-301-310-130-137-341-104-130-134-143-134-314-134-134-134-104-134-103-134-431-103-134-143-134-1_V.mp3)
  Segment 8: 34072ms ~ 34648ms (file: 573-751-715-751-705-107-175 157-175-107-157-517-751-517-571-573-753-517-573-170-175-107-105-103 157-107-103-317-107-153-753-105-517-501-153-517-507-150-507_V.mp3)
  Segment 9: 35648ms ~ 36224ms (file: 570-751-571-517-715-574-574-571-751-571-574-751-754-751-157-175-751-715-751-751-713-173-705 751-750-571-751-750-751-571-751-571-173-715-715-173-751-571-751-761-175-173-715-751 7_W.mp3)
  Segment 10: 37224ms ~ 37800ms (file: 574-574 570-157-517-157-517-571-573-571-517-517-157-571-571-157-571-576-574 157-715-517-517-157-157-150-715-157-175-715-173-715-150-135-175-715-715-157-157-175-157-715-513-571-5_V.mp3)
  Segment 11: 38800ms ~ 51148ms (file: 736-674-627-627-137-137-263-127-123-765-716-107-276-310-236-216-761-672-312-362-326-163-107-321-263-231-102-765-157-103-163-167-137-317-735-762-267-267-627-672-276-276-725-752-2_W.mp3)
  Segment 12: 52148ms ~ 63020ms (file: 216-103-627-276-241-632-301-530-521-137-143-132-153-321-657-276-123-134-263-216-132-310-103-712-236-123-213-213-726-267-230-213-123-103-263-103-127-312-132-176-103-132-123-210-7_W.mp3)
  Segment 13: 64020ms ~ 71904ms (file: 260-250-263-526-206-207-321-261-570-132-715-765-671-627-625-607-137-752-312-132-231-201-276-231-312-705-672-105-137-705-213-207-263-267-175-267-263-236-710-213-137-507-572-265-1_V.mp3)
  Segment 14: 72904ms ~ 73480ms (file: 157-157-157-175-176-157-176-715 510-154-150-105-571-105-701-150-150-157-517-157-517-517-157-175-751-157-157-157-157-175 104-105-105-103-157-107-107-175-107-175-176-175-156-176-1_V.mp3)
  Segment 15: 74480ms ~ 75056ms (file: 136-136-103-136-106-671-617-136-130-132-620-165-162-613-160-107-130-136-103-163-103-136-153-137-617-716-163-361-167-261 567-136-163-103-157-631 136-136-103-627-517-657-130-316-1_V.mp3)
  Segment 16: 76056ms ~ 76632ms (file: 365-376-735-736-736-576-576-576-756-132-103-132-612-261 132-267-237-321-132-312-104-132-164-132-152-514-504-547-567-165-136-705-750-715-731-173-627-267-167-132-761-136-137-103-3_V.mp3)
  Segment 17: 77632ms ~ 87244ms (file: 312-123-610-265-256-260-621-261-216-572-216-123-572-625-765-725-321-132-132-312-231-321-103-267-267-621-627-107-261-126-735-231-261-162-265 576-105-561-263-261-216-261-263-756-1_W.mp3)
  Segment 18: 88244ms ~ 96632ms (file: 102-627-672-672-260-230-263-523-321-762-316-310-531-572-321-130-312-310-130-267-206-106-103-263-163-165-312-321-627-625-250-362-150-517-367-307-350-126-576-106-136-325-206-261-2_W.mp3)
  Segment 19: 97632ms ~ 98208ms (file: 136-130-162-261 136-132-316-102-103-103-103-167-201-206-260-263-321 107-627-627-567-572-562-367-306-321-361-675-275-672-267-361-576-617-765-752 675-167-105-310-130-765-576-574_W.mp3)
  Segment 20: 99208ms ~ 99784ms (file: 130-103-137-102-312-301-317-317-107-317-721-130 135-137-103-103-103-137-103-312-127-137-173-137-312-137-130-301-310-173-130-137-103-301-317-173-103-310-107-371-731 135-310-137-1_V.mp3)
  Segment 21: 100784ms ~ 101360ms (file: 150-501-571-570 510-510-507-510-510-510-150-501-150-574-510-510-510-507-510-574-571-150-570-547-574-571-573 507-501-510-150-105-715-150-517-105-105-150-570-150-150-507-507-501-1_W.mp3)
  Segment 22: 102360ms ~ 102936ms (file: 570-710-570-173-750-170-175-573-705-750-750-756-105-675-765-765-765-756-756-376-736-735-763-753-756-675-507-507-750-571-750-750-751-705-571-710-307-137-715-731-701-570 571-751-5_W.mp3)
  Segment 23: 103936ms ~ 110668ms (file: 765-547-210-253-570-546-123-216-502-521-521-504-502-315-231-752-523-265-162-627-103-301-231-321-302-230-103-574-527-106-120-514-150-175-123-206-276-526-267-102-126-312-201-325-3_W.mp3)
  Segment 24: 111668ms ~ 124988ms (file: 102-213 103-130-126-213-176-130-173-170-130-216-126-302-217-726-107-103-103-103-132-126-203-260-237-201-721-173-701-213-103-137-732-127-137-103-103-103-137-123-103-130-327-236-2_V.mp3)
  Segment 25: 125988ms ~ 126564ms (file: 267-276-236-263-163-162-153-302-203-320-320-301-132-261-206-263-236-123-136-103-106-312-371-310-103-130-312 672-170-107-167-713-617-176-671-753-176-361-761-721-127-672-627-623-2_W.mp3)
  Segment 26: 127564ms ~ 128140ms (file: 163-135-265-625-156-657-231-156-576-361-576-136-576-652-657-657-362-762-726-652-751-351-516-752-563-571-352-652-621-576-652-625-651-106-632-317-675-130-136-675-361-135-653-561-5_W.mp3)
  Segment 27: 129140ms ~ 129716ms (file: 574-570-570-570-150-157-501-576-501 510-150-150-105-105-150-150-517-517-751-501-510_V.mp3)
  Segment 28: 130716ms ~ 131292ms (file: 150-130-150-157-105-107-107-105-170-105-150-107-105-150-153-157-517 105-103-103-130-105-105-510-514-150-150-105-153-150-150-105-105-157-105-157-154-517-105 105-105-105-105-105-1_V.mp3)
  Segment 29: 132292ms ~ 142588ms (file: 126-327-273-230-275-356-136-312-267-136-172-762-126-276-267-267-123-376 276-765-756-725-756-750-276-216-751-756-765-756-326-321-136-162-120-321-123-125-256-237-120-310-310-261-2_W.mp3)
  Segment 30: 143588ms ~ 144164ms (file: 175-716-175-167-157-176-751-751-175-175-175 105-154-510-510-510-501-507-510-501-517-517-175-751-751-750-517-751-715-571-571-576-175-547_W.mp3)
  Segment 31: 145164ms ~ 145740ms (file: 376-162-361-651-371-672-625-125-162-617-103-672-632-612 574-574_W.mp3)
  Segment 32: 146740ms ~ 147316ms (file: 712-715-751-165-157-167-165-135-125-756-765-756-752-756 510-152-162-102-106-106-103-106-106-106_W.mp3)
  Segment 33: 148316ms ~ 159980ms (file: 213-260-201-103-102-320-213-320-231-236-321-213-320-203-231-132-321-310-163-210-203-102-612-326-275-230-201-172-236-236-167-213-163-267-102-162-102-160-267-267-143-267-267-672-2_W.mp3)
  Segment 34: 160980ms ~ 161556ms (file: 571-576-571-571-517-517-571-571-574-514-574-574-571-517 517-571-573-571-571-571-571-571-574-571-571-571-517-571-574-574-571-574-571-574-571-574-574-574-571_W.mp3)
  Segment 35: 162556ms ~ 163132ms (file: 601-621-672-267-625-602-576-567-627-162-162-261-546-574_W.mp3)
  Segment 36: 164132ms ~ 164708ms (file: 175-715-715-173-170-750-570-751-715-751-157-175-751-715-751-751-751-753 157-105-501-750-571-751-715-751-175-157-750-750-751-715-175-751-175-175-751-571 570-571-517-750-751-715-7_V.mp3)
  Segment 37: 165708ms ~ 166284ms (file: 106-136-760-730-673-327-327-763-307-735-705 173-607-761-176-167-143-672-641-762-761-132-756-165-312-341-132-635-321 102-103-103-123-657_V.mp3)
  Segment 38: 167284ms ~ 178408ms (file: 210-261-574-321-210-546-210-203-627-527-257-571-217-231-132-103-132-102-132-123-102-210-137-213-103-261-210-261-123-261-123-162-213-132-261-612-726-310-216-126-260-210-201-102-2_W.mp3)
  Segment 39: 179408ms ~ 179984ms (file: 150-751-517-513-137-135-132-135-367-176-175-162-756-763-673-106-276-267-321-213-102-126-162-321-162-762_W.mp3)
  Segment 40: 180984ms ~ 181560ms (file: 176-271-652-162-613-103-106-162-156-751-136-651-132-623-623-123-216-231-321_W.mp3)
  Segment 41: 182560ms ~ 183136ms (file: 715-157-107-710-710-715-750-751-570-751-750-751-576-567-571-517-567-756 510-175-107-750-107-170-170-170-175-716-720-701-715-756-571-576-756-576-756-765-756-576-765-576 715-170-1_V.mp3)
  Segment 42: 184136ms ~ 193568ms (file: 132-201-206-210-132-103-102-203-163-312-206-206-207-230-126-210-205-230-103-312-173-176-741-541-143-103-123-231-572-217-301-236-210-132-103-201-572-631-132-203-206-124-103-573-1_W.mp3)
  Segment 43: 194568ms ~ 195144ms (file: 576-571-576-157-560-507-520-157-751-157-173-517-753-751-571-570-571-751-571-517-571-571-576-571-576-651-576-576-570-561-571-570-507-570-576-516-501-510-570-507-571-516-510-501-5_W.mp3)
  Segment 44: 196144ms ~ 196720ms (file: 320-136-137-126-176-167-316-365-652-562-657-562_V.mp3)
  Segment 45: 197720ms ~ 198296ms (file: 316-623-316-316-361-163-106-167-675-756-765-623-765 674-764-317-765-765-765-761-725-376-762-163_W.mp3)
  Segment 46: 199296ms ~ 207108ms (file: 320-620-627-213-216-527-267-207-267-321-263-103-231-210-102-512-301-310-130-163-126-261-356-316-263-236-312-132-206-231-230-260-260-216-136-267-102-267-261-167-361-310-307-103-6_W.mp3)
  Segment 47: 208108ms ~ 214912ms (file: 265-312-620-260-265-312-265-261-257-263-752-267-321-506-765-201-260-263-206-620-263-150-156-506-765-260-276-203-261-756-725-745-254-106-165-507-620-206-216-123-120-127-570-602-1_W.mp3)
  Segment 48: 215912ms ~ 223256ms (file: 302-103-130-103-123-135-103-137-310-137-127-102-172-210-310-136-206-102-130-103-260-260-261-627-236-361-162-123-167-165-206-267-765-210-623-136-163-321-352-312-103-371-132-231-1_V.mp3)
  Segment 49: 224256ms ~ 235560ms (file: 270-270-143-167-102-170-267-271-276-201-126-735-237-721-273-207-206-751-127-705-176-132-136-103-102-276-237-103-267-752-132-573-351-316-301-753-267-236-163-326-130-102-203-107-1_W.mp3)
  Segment 50: 236560ms ~ 237136ms (file: 157-105-157-150-501-150-510-517-501-501-517-507-150-150-517-501-507-510-510-517-510-150-510-157-150-576 510-150-510-510-501-571-510-507-501-501-510-150-150-507-501-501-150-501-5_V.mp3)
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
> **Time**: 2026-04-05 07:11:21
> **AI**: gemini

---

## Response

[
{
"segment": 1,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "안녕하세요 만나서 반갑습니다 오늘 날씨가 참 좋네요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕하세요", "start_ms": 150, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "만나서", "start_ms": 1050, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "반갑습니다", "start_ms": 1600, "dur_ms": 900, "phase_lr_deg": 0.0},
{"word": "오늘", "start_ms": 3000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "날씨가", "start_ms": 3500, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "참", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "좋네요", "start_ms": 4600, "dur_ms": 500, "phase_lr_deg": 0.0}
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
"transcript": "한국어 공부를 시작한 지 얼마나 되셨나요 저는 이제 막 시작했어요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "한국어", "start_ms": 200, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "공부를", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "시작한", "start_ms": 1400, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "지", "start_ms": 1900, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "얼마나", "start_ms": 2150, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "되셨나요", "start_ms": 2750, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "저는", "start_ms": 4000, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "이제", "start_ms": 4500, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "막", "start_ms": 4900, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "시작했어요", "start_ms": 5200, "dur_ms": 850, "phase_lr_deg": -5.0}
]
},
{
"segment": 6,
"transcript": "열심히 하면 금방 늘 거예요 걱정하지 마세요 파이팅",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "열심히", "start_ms": 180, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "하면", "start_ms": 850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "금방", "start_ms": 1300, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "늘", "start_ms": 1800, "dur_ms": 250, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 2100, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "걱정하지", "start_ms": 3200, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 4000, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "파이팅", "start_ms": 5500, "dur_ms": 650, "phase_lr_deg": 0.0}
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
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "Thank you for your kind words. I will try my best to learn.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "Thank", "start_ms": 100, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "you", "start_ms": 450, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "for", "start_ms": 700, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "your", "start_ms": 950, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "kind", "start_ms": 1200, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "words.", "start_ms": 1600, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "I", "start_ms": 2500, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "will", "start_ms": 2750, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "try", "start_ms": 3050, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "my", "start_ms": 3400, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "best", "start_ms": 3650, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "to", "start_ms": 4100, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "learn.", "start_ms": 4350, "dur_ms": 500, "phase_lr_deg": -5.0}
]
},
{
"segment": 12,
"transcript": "일본어는 조금 어려워요 하지만 포기하지 않을 거예요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "일본어는", "start_ms": 250, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "조금", "start_ms": 1000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "어려워요", "start_ms": 1500, "dur_ms": 750, "phase_lr_deg": 0.0},
{"word": "하지만", "start_ms": 3000, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "포기하지", "start_ms": 3700, "dur_ms": 700, "phase_lr_deg": 0.0},
{"word": "않을", "start_ms": 4500, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "거예요", "start_ms": 4900, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "日本語も勉強していますか。とても面白いですよ。",
"speaker": "speaker_3",
"confidence": 0.93,
"language": "ja",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "日本語も", "start_ms": 150, "dur_ms": 700, "phase_lr_deg": 10.0},
{"word": "勉強して", "start_ms": 900, "dur_ms": 600, "phase_lr_deg": 10.0},
{"word": "いますか。", "start_ms": 1550, "dur_ms": 550, "phase_lr_deg": 10.0},
{"word": "とても", "start_ms": 3000, "dur_ms": 500, "phase_lr_deg": 10.0},
{"word": "面白いですよ。", "start_ms": 3600, "dur_ms": 900, "phase_lr_deg": 10.0}
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
"transcript": "여러 언어를 배우는 것은 정말 멋진 일이에요 계속 노력해봐요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "여러", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "언어를", "start_ms": 550, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "배우는", "start_ms": 1100, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "것은", "start_ms": 1600, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "정말", "start_ms": 2050, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "멋진", "start_ms": 2550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "일이에요", "start_ms": 3000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "계속", "start_ms": 4500, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "노력해봐요", "start_ms": 5100, "dur_ms": 800, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "음악을 들으면서 공부하는 것도 좋은 방법 중 하나예요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "음악을", "start_ms": 200, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "들으면서", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "공부하는", "start_ms": 1500, "dur_ms": 650, "phase_lr_deg": -5.0},
{"word": "것도", "start_ms": 2250, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "좋은", "start_ms": 2700, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "방법", "start_ms": 3200, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "중", "start_ms": 3750, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "하나예요", "start_ms": 4050, "dur_ms": 700, "phase_lr_deg": -5.0}
]
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
"transcript": "가장 좋아하는 노래 가사로 문장을 외워 보세요",
"speaker": "speaker_1",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "가장", "start_ms": 150, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "좋아하는", "start_ms": 600, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 1300, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "가사로", "start_ms": 1800, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "문장을", "start_ms": 2500, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "외워", "start_ms": 3100, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "보세요", "start_ms": 3600, "dur_ms": 600, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "That is a great idea! Music makes everything more fun.",
"speaker": "speaker_2",
"confidence": 0.99,
"language": "en",
"silence_before_ms": 100,
"audio_type": "speech",
"words": [
{"word": "That", "start_ms": 100, "dur_ms": 200, "phase_lr_deg": -5.0},
{"word": "is", "start_ms": 350, "dur_ms": 150, "phase_lr_deg": -5.0},
{"word": "a", "start_ms": 550, "dur_ms": 100, "phase_lr_deg": -5.0},
{"word": "great", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "idea!", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "Music", "start_ms": 2500, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "makes", "start_ms": 3050, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "everything", "start_ms": 3500, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "more", "start_ms": 4200, "dur_ms": 300, "phase_lr_deg": -5.0},
{"word": "fun.", "start_ms": 4600, "dur_ms": 400, "phase_lr_deg": -5.0}
]
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
"transcript": "오늘 배운 내용을 복습하는 것을 잊지 마세요 내일 또 봐요",
"speaker": "speaker_1",
"confidence": 0.98,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 120, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "배운", "start_ms": 550, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "내용을", "start_ms": 1050, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "복습하는", "start_ms": 1700, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "것을", "start_ms": 2450, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "잊지", "start_ms": 2900, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "마세요", "start_ms": 3400, "dur_ms": 600, "phase_lr_deg": 0.0},
{"word": "내일", "start_ms": 5000, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "또", "start_ms": 5500, "dur_ms": 200, "phase_lr_deg": 0.0},
{"word": "봐요", "start_ms": 5800, "dur_ms": 450, "phase_lr_deg": 0.0}
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
"transcript": "안녕히 가세요 다음에 또 뵙겠습니다 수고하셨습니다",
"speaker": "speaker_2",
"confidence": 0.97,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "안녕히", "start_ms": 150, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "가세요", "start_ms": 800, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "다음에", "start_ms": 2000, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "또", "start_ms": 2600, "dur_ms": 250, "phase_lr_deg": -5.0},
{"word": "뵙겠습니다", "start_ms": 2950, "dur_ms": 850, "phase_lr_deg": -5.0},
{"word": "수고하셨습니다", "start_ms": 4500, "dur_ms": 1100, "phase_lr_deg": -5.0}
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
"transcript": "언제든지 궁금한 게 있으면 물어보세요 제가 도와드릴게요",
"speaker": "speaker_1",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 120,
"audio_type": "speech",
"words": [
{"word": "언제든지", "start_ms": 120, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "궁금한", "start_ms": 850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "게", "start_ms": 1400, "dur_ms": 150, "phase_lr_deg": 0.0},
{"word": "있으면", "start_ms": 1650, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "물어보세요", "start_ms": 2250, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "제가", "start_ms": 3500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "도와드릴게요", "start_ms": 4000, "dur_ms": 900, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "(noise)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "noise",
"words": []
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
"transcript": "그럼 오늘 하루도 즐겁게 보내시고 다음에 만나요",
"speaker": "speaker_2",
"confidence": 0.95,
"language": "ko",
"silence_before_ms": 180,
"audio_type": "speech",
"words": [
{"word": "그럼", "start_ms": 180, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "오늘", "start_ms": 650, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "하루도", "start_ms": 1150, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "즐겁게", "start_ms": 1800, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "보내시고", "start_ms": 2450, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "다음에", "start_ms": 3500, "dur_ms": 500, "phase_lr_deg": -5.0},
{"word": "만나요", "start_ms": 4100, "dur_ms": 600, "phase_lr_deg": -5.0}
]
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
"transcript": "한국 노래 중에서 어떤 장르를 가장 좋아하세요 저는 발라드가 좋아요",
"speaker": "speaker_1",
"confidence": 0.94,
"language": "ko",
"silence_before_ms": 200,
"audio_type": "speech",
"words": [
{"word": "한국", "start_ms": 200, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "노래", "start_ms": 700, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "중에서", "start_ms": 1150, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "어떤", "start_ms": 1750, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "장르를", "start_ms": 2200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "가장", "start_ms": 2850, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "좋아하세요", "start_ms": 3350, "dur_ms": 800, "phase_lr_deg": 0.0},
{"word": "저는", "start_ms": 4500, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "발라드가", "start_ms": 5000, "dur_ms": 650, "phase_lr_deg": 0.0},
{"word": "좋아요", "start_ms": 5750, "dur_ms": 550, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "저는 케이팝 댄스 곡을 좋아해요 리듬이 정말 신나요",
"speaker": "speaker_2",
"confidence": 0.96,
"language": "ko",
"silence_before_ms": 150,
"audio_type": "speech",
"words": [
{"word": "저는", "start_ms": 150, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "케이팝", "start_ms": 650, "dur_ms": 600, "phase_lr_deg": -5.0},
{"word": "댄스", "start_ms": 1350, "dur_ms": 450, "phase_lr_deg": -5.0},
{"word": "곡을", "start_ms": 1900, "dur_ms": 350, "phase_lr_deg": -5.0},
{"word": "좋아해요", "start_ms": 2350, "dur_ms": 700, "phase_lr_deg": -5.0},
{"word": "리듬이", "start_ms": 3500, "dur_ms": 550, "phase_lr_deg": -5.0},
{"word": "정말", "start_ms": 4150, "dur_ms": 400, "phase_lr_deg": -5.0},
{"word": "신나요", "start_ms": 4650, "dur_ms": 600, "phase_lr_deg": -5.0}
]
},
{
"segment": 48,
"transcript": "(music)",
"speaker": "none",
"confidence": 0.0,
"language": "ko",
"silence_before_ms": 0,
"audio_type": "music",
"words": []
},
{
"segment": 49,
"transcript": "우리 같이 연습해 볼까요 하나 둘 셋 시작",
"speaker": "speaker_1",
"confidence": 0.93,
"language": "ko",
"silence_before_ms": 250,
"audio_type": "speech",
"words": [
{"word": "우리", "start_ms": 250, "dur_ms": 350, "phase_lr_deg": 0.0},
{"word": "같이", "start_ms": 700, "dur_ms": 400, "phase_lr_deg": 0.0},
{"word": "연습해", "start_ms": 1200, "dur_ms": 550, "phase_lr_deg": 0.0},
{"word": "볼까요", "start_ms": 1850, "dur_ms": 500, "phase_lr_deg": 0.0},
{"word": "하나", "start_ms": 3000, "dur_ms": 450, "phase_lr_deg": 0.0},
{"word": "둘", "start_ms": 3550, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "셋", "start_ms": 3950, "dur_ms": 300, "phase_lr_deg": 0.0},
{"word": "시작", "start_ms": 4500, "dur_ms": 600, "phase_lr_deg": 0.0}
]
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
*Generated by WKAppBot ask gemini — 2026-04-05 07:11:21*
