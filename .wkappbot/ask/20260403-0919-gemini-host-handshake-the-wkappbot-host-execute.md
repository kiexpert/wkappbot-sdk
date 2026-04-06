# Ask GEMINI

> **Question**: [HOST-HANDSHAKE]
The WKAppBot host executed a connectivity probe on startup:
[APPBOT_TOOL_CALL_BEGIN]{"id":"tc_init","argv":["readiness"]}[APPBOT_TOOL_CALL_END]
TOOL_RESULTS [MCP+APSP executed_by=host flushed=1 still_running=0]
[{"jsonrpc":"2.0","id":"tc_init","result":{"content":[{"type":"text","text":"Ready: True  host_pid=43752  time=09:18:39  uptime=28764s  mem=1936MB  system_processes=447"}],"isError":false}}]
Host is live. Your TOOL_CALL blocks will be executed in real time.
[/HOST-HANDSHAKE]

[file:study_batch_20260403_091829.mp3]
You are a professional audio transcription and analysis tool.
I'm building a karaoke system for language learning.
This audio file contains multiple short speech segments separated by ~1 second silence gaps.
Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.
Some segments may be NON-SPEECH. Classify each segment's audio_type:
  "speech" = human voice, "music" = song/BGM, "instrument" = piano/guitar/etc,
  "noise" = keyboard/click/fan/ambient, "mixed" = speech+music overlap.
For non-speech: set confidence=0, transcript="(noise)"/"(music)"/"(instrument)".
Segment timing map (approximate):
  Segment 1: 0ms ~ 576ms (file: 137-173-107-317-705-705-754-351-352-567-657-652-672-632-106-167-126-123-132-143-312-132-312-324-540-534-541-132-542-540-502-103-163-167-132-321-230-362-627-163-167-657-132-136-1_V.mp3)
  Segment 2: 1576ms ~ 2152ms (file: 156-237-236-712-127-576-751-137-173-173-172-175-207-207-207-752-725-276-602-265-163 126-613-163 567-765-762_V.mp3)
  Segment 3: 3152ms ~ 3728ms (file: 261-326-231-602-620-602-620-602-623-621 260-206-206-360-621-230-326-103-105-153-130-310-132-136-130-310-132-136-316-301-130-310-321-132-263-621-627-726-672-312-735-753-754-521-5_W.mp3)
  Segment 4: 4728ms ~ 5304ms (file: 563-356-356-157-150-651-613-627-612-126-547-567-576-157-531-351-153-315-357-351-561-563-135-516-531-156-153-543-562-523-532-563-532-350-562-526-576-526-536-567-561-567-537-756-6_W.mp3)
  Segment 5: 6304ms ~ 6880ms (file: 301-306-106-103-106-163-106-103-130-136-136-236-263-123-312-314-132-103-102-102-316-312 574-547-574-547-573-547-137-617-167-671-534-543-567-326-236-321-521-216-301-356-310-103-6_W.mp3)
  Segment 6: 7880ms ~ 8456ms (file: 261-213-274-265-231-234-327-325-231-324-312-375-513-326-134-165-165-762-364-316-167-624-173-137-123-731-651-571-123-317-123-173_V.mp3)
  Segment 7: 9456ms ~ 10032ms (file: 172-162 103-316-103-130-513-627-162 761-103-671-107-610-136-136-317-103-321-561-103-167-163-136-321-136-136-163-106-123-216-136-162-136-130-163-265-617-106-136-103-160-163-132-6_V.mp3)
  Segment 8: 11032ms ~ 11608ms (file: 136-617-263-126-256-153-352-136-136-613-136-103-263-612-312-176-127-106-236-203-106-216-261-106-762-160-623-103-165-621-632-523-572-621-132-126-312-567 361-132-312-136-173-176 2_V.mp3)
  Segment 9: 12608ms ~ 13184ms (file: 762-756-756-726-150-265-321-261-173 136-263-312-623-263 132-136-107-671-617-107-137-132-263-627-261_W.mp3)
  Segment 10: 14184ms ~ 14760ms (file: 561-650-657-651-615-165-712-172-615-601-607-106-167-167-612-201-102-103-103-130-632-632-602-621-123-132-103 620-160-102-107-702-706-760-576-567-365-635-136-631-106-710-271-217-3_V.mp3)
  Segment 11: 15760ms ~ 16336ms (file: 132-316-524-143-157-137-137-137-314-132-765-725-756-307-372-321-120-231-672-750-351-327-326-703-750-752-130-103-103-213-320-316-103-132-710-206-216-216-213-706-706-726-762-763-6_V.mp3)
  Segment 12: 17336ms ~ 17912ms (file: 612-103-132-271-137-163-136-103-126-163-136-132-152-765-167-136-103-103-130-103 130-130-301-102-136-102-126-613-106-612-627-621-621-632-623-165-103-156-160-102-167-210-210-210-2_V.mp3)
  Segment 13: 18912ms ~ 19488ms (file: 267-673-623-567-635-623-127 106-173-160-163-517-631-612-163-130-631-574-754-736-160-162-326-123-106-136-102-103-165-165-672-136-316-512-125-136-176-162-132-623-267_W.mp3)
  Segment 14: 20488ms ~ 21064ms (file: 160-601-103-167-127-132-163-106-651-165-102-721-156-106-106-103-571-617 317-136-106-132-163-167-103-564-132-123-167-376-103-130-612-176-126-157-173-156-136-103-163-160-316-165-6_V.mp3)
  Segment 15: 22064ms ~ 22640ms (file: 713-137-103-106-132-136-132-126-132-102-132-136-132-162-132-126-162-102-123-126-126-123_V.mp3)
  Segment 16: 23640ms ~ 24216ms (file: 516-167-167-216-675-165-167-203-203-136-132-540-315-130-132-231-123-350-504-206-321-326-203-236-132-123-312-301-501-540-135-621-604-302-310-153-302-103-312-267-127-217-750-270-7_V.mp3)
  Segment 17: 25216ms ~ 25792ms (file: 567-167-657-261-627-652-567-567-652-671-576-657-567-657-675-657-756-265-256-561-567-765-765-765-675-652-356-625-625-106-103-132-130-310-130-576-176-760-715-651-132-132-173-527-2_W.mp3)
  Segment 18: 26792ms ~ 27368ms (file: 627-672-673 756-756-765-765-672-675-675 567-652-625-276_W.mp3)
  Segment 19: 28368ms ~ 28944ms (file: 260-120-230-352-362-302-302-302-352-534-103-106-162-126-123-315-320-302-302-320-326-302-672-726-302-150-103-213-302-310-726-216-103-216-103-163-167-167-176-173-137-132-260-326-1_V.mp3)
  Segment 20: 29944ms ~ 30520ms (file: 537-756-576-756-576-567-567-652-567-567-567-563-365-516-516-576-567-567-576-567-532-536-356-576-567-573-576-567-576-573-567-563-367-573-567-576-356-536-572-675-765-761-357-563-5_W.mp3)
  Segment 21: 31520ms ~ 32096ms (file: 321-321-213-321-312-217-237-237-327-273-723-725-372-236-236-273-236-627-735-327-726-276-236-237-236-236-257-325-257-327-273-327-273-327-321-632-623-273-546-546-524-514-637-671-6_W.mp3)
  Segment 22: 33096ms ~ 33672ms (file: 143-103-132-134-613-613-321-301-361-231-103-310-132-103-302-136-132-310-103-316-132-136-163-160-106-163-103-167-316-302-206-201-621-203-236-317-361-136-203-236-236-237-261-276-6_V.mp3)
  Segment 23: 34672ms ~ 35248ms (file: 320-630-126-132-132-623-613-126-263-216-276-672-310-361-361-132-162-134-103-130-321-623-261-162-132-632-623 213-236-326-362-123-316-312-235-263-236-352-365-362-312-632 236-236-2_W.mp3)
  Segment 24: 36248ms ~ 36824ms (file: 231-263-627-675-625-621-263-632-276-675-672-765-675-256-576-572-576-756-765-756-527-675-237-326-756-576-576-576-527-576-675-167-752-657-765-136-621-612-712-756-765-632-672-263-3_W.mp3)
  Segment 25: 37824ms ~ 38400ms (file: 103-745-736-176-165-163-136-132-675-123-167-102-761-670-163-132-137-165-106-162-106-127-160-176-162-136-163-106-162-136-761-125-167-175-167-134-106-103-143-125-124-157-165-531-1_V.mp3)
  Segment 26: 39400ms ~ 39976ms (file: 136-106-167-136-136-123-162-172-256-761-103-137-253-671-132-132-301-310-376-671-517-123-162-167-376-617-137-136-167-263-127-657 372-103_V.mp3)
  Segment 27: 40976ms ~ 41552ms (file: 301-163-123-106-701-761-761-671 726-326-213-216-632-612-362-103-236-621-623-316-610-103-136-261-671-367-172-176-167-613-603-103-132-135-132-316-316-106-103-206-762-762-620-106-1_V.mp3)
  Segment 28: 42552ms ~ 43128ms (file: 173-103-317-316-163-167-173-136-135-132-130-316-730-107-675-163-751-751-106-162-260-621-602-167-106-103-635-136-103-162-715-621-130-672-261-162-106-103-136-157-576-167-136-263-2_V.mp3)
  Segment 29: 44128ms ~ 44704ms (file: 175-761-716-765-765-765-617-657 261-612-621-627 136-162-163-167 716-761-173-136 762-752-762-752-765-756-752-765-762-763_W.mp3)
  Segment 30: 45704ms ~ 46280ms (file: 321-621-260-620-620-652-602-631-572-126-326-632-362-132-265-263-236-630-162-162-106-612-601-160-162-167-605-537-537-537-753-627-203-213-231-236-632-136-103-136-126-123-132-136-2_V.mp3)
  Segment 31: 47280ms ~ 47856ms (file: 176 765-765-756-756-756-765-763-765-760-756-763-760-756-765-765-726-723-762-756-756-765-765-765-675-765 627-127-617-627 750-172-715-756-765-576-176-176-756-167-765-765-761-762-1_W.mp3)
  Segment 32: 48856ms ~ 49432ms (file: 365-564-106-136-623-631-231-637-132-316-623-671-103-163-102-123-630-136-136-103-136-103-231-150-567-675-617-607-721-157-612-326-312-126-162-263-137-136-671-136-132-103-765-103-1_V.mp3)
  Segment 33: 50432ms ~ 51008ms (file: 576-574-576-574-517-574-157-576-315-103-126-206-206-123-263-263-271-276-206-126-126-521-123-172-216-623-130-103-103-103-132-513-107-351-156-321 326-103-165-106-106-105 102-103-1_V.mp3)
  Segment 34: 52008ms ~ 52584ms (file: 765-675-657-672-765-756-675-756 162 362-103-132-123-102-103-132_W.mp3)
  Segment 35: 53584ms ~ 54160ms (file: 612-623-621-637-754-165-176-107-316-326-106-103-130-310-320-176-641-312-103-675-675-627-507-602-267-627-627-760-756-107-167-173-137-756-167-167-617-167-136-206-310-261-613-617-1_V.mp3)
  Segment 36: 55160ms ~ 55736ms (file: 273-537-570-537-302-720-310-103-310-132-310-153-103-167-106-162-716-137-106-136-103-670-670-607 301-362-106-136-326-312-135-137-175-176-317-137-301-621-613-615-615-621-617-612-3_V.mp3)
  Segment 37: 56736ms ~ 57312ms (file: 751-730-671-576-756-756-657-526-675-657-652-652-567-562-526 672_W.mp3)
  Segment 38: 58312ms ~ 58888ms (file: 137-103-132-263-267-263-176-137-263-316-103-103-132-136-163-167-103-312-103-132-103-316-312-753-327-312-531-705-207-326-236-203-203-230-203-321-236-320-316-102-213-750-756-765-7_V.mp3)
  Segment 39: 59888ms ~ 60464ms (file: 163-316-103-132-671-512 713-312-167-167-106-165-106-136-160-162-127-162-130-715-756-170-167-163-627-176-132-617-163-165-263-175-102-621-623-106-631-106-176-106-136-671-617-613-6_V.mp3)
  Segment 40: 61464ms ~ 62040ms (file: 132-132-103-126-120-102-123-162-102-102-130-106-130-123-103-310-130-310-103-210-203-103-162-621-163-106_V.mp3)
  Segment 41: 63040ms ~ 63616ms (file: 127-321-256-235-325-235-256-265-263-326-623-267-526-256-562-526-257-574-526-652-516-517-567-615-653-163-532-527-527-532-526-521-256-324-261-261-312-231-216-231-237-263-263-215-3_W.mp3)
  Segment 42: 64616ms ~ 65192ms (file: 137-267-136-163-312-132-301-217-531-756-751-302-132-123-312-163-672-675-267-260-723-103-203-237-231-257-205-123-172-705-756-271-261-132-103-130-105-531-731-671-361-361-306-320-2_W.mp3)
  Segment 43: 66192ms ~ 66768ms (file: 567-675-657-653-653-563-567-653-657-576-567-567-576-567-657-675-652-657-567-631-657-672-367-615-672-612-672-675-627-657-673-765-657-625-756-657-162-267-325-756-576-567-627-236-2_W.mp3)
  Segment 44: 67768ms ~ 68344ms (file: 627-627-627-567-567-576-672-762-675-567-675-762-162-326-523-235-263-236-562-672-376-236-362-263-263-623-236-523-235-265-625-231-302-203-231-321-302-320-621-576-756-367-602-361-5_W.mp3)
  Segment 45: 69344ms ~ 69920ms (file: 102-126-312-162-621-617-601-621-260-621-621-260-260-260-260-260-230-103-103-162 130-312-103_W.mp3)
  Segment 46: 70920ms ~ 71496ms (file: 162-312-236-263-617-163-163-162-623-136-617-671-621-162-162-627-612-627-612-627-672-162-627-612-621-627-126-627-621-126-627-613-672-163-627 671-672-672-671-672-617-163-612-162-6_W.mp3)
  Segment 47: 72496ms ~ 73072ms (file: 102-301-120-102-163-153-516-167-306-376-675-672-167-571-175-167-165-615-657-612-162-765-617-675-637-657-653-563-567-563-315-165-175-135-135-135-135-163-165-126-261-165-152-153-1_W.mp3)
  Segment 48: 74072ms ~ 74648ms (file: 750-526-265-627-627-267-261 172 672-672-623-631_W.mp3)
  Segment 49: 75648ms ~ 76224ms (file: 567-576-756-756-765-637-653-167-532-537-357-325-135-327-653-576-576-571-516-574-257-572-216-263-321-251-153-527-365-256-253-273-576-152-362-132-136-652-572-357-275-571-573-513-3_W.mp3)
  Segment 50: 77224ms ~ 77800ms (file: 105-632-316-756-175-571-571-516-163-106-516-130-130-130-130-103-132-301-130-301-231-256-356-132-103 103-132-672-675-762-675-627-176-216-263-627-267-276 327-231-236-213-216-175-2_W.mp3)
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
> **Time**: 2026-04-03 09:19:32
> **AI**: gemini

---

## Response

AnalysisAnalysis쿼리 성공Gemini의 응답[
{
"segment": 1,
"transcript": "구",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 45,
"audio_type": "speech",
"words": [
{"word": "구", "start_ms": 45, "dur_ms": 310, "phase_lr_deg": 0.0}
]
},
{
"segment": 2,
"transcript": "십",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "십", "start_ms": 60, "dur_ms": 280, "phase_lr_deg": 0.0}
]
},
{
"segment": 3,
"transcript": "일월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "일월", "start_ms": 30, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 4,
"transcript": "이월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "이월", "start_ms": 55, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 5,
"transcript": "삼월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "삼월", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 6,
"transcript": "사월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "사월", "start_ms": 70, "dur_ms": 470, "phase_lr_deg": 0.0}
]
},
{
"segment": 7,
"transcript": "오월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "오월", "start_ms": 50, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 8,
"transcript": "유월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "유월", "start_ms": 80, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 9,
"transcript": "칠월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "칠월", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 10,
"transcript": "팔월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "팔월", "start_ms": 60, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 11,
"transcript": "구월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "구월", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 12,
"transcript": "시월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "시월", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 13,
"transcript": "십일월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "십일월", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 14,
"transcript": "십이월",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "십이월", "start_ms": 65, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 15,
"transcript": "월요일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "월요일", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 16,
"transcript": "화요일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "화요일", "start_ms": 70, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 17,
"transcript": "수요일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "수요일", "start_ms": 50, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 18,
"transcript": "목요일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "목요일", "start_ms": 80, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 19,
"transcript": "금요일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "금요일", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 20,
"transcript": "토요일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "토요일", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 21,
"transcript": "일요일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "일요일", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 22,
"transcript": "오늘",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "오늘", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 23,
"transcript": "내일",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "내일", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 24,
"transcript": "어제",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "어제", "start_ms": 65, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 25,
"transcript": "아침",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "아침", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 26,
"transcript": "점심",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "점심", "start_ms": 70, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 27,
"transcript": "저녁",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "저녁", "start_ms": 50, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 28,
"transcript": "밤",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "밤", "start_ms": 80, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 29,
"transcript": "새벽",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "새벽", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 30,
"transcript": "지금",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "지금", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 31,
"transcript": "나중에",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 32,
"transcript": "빨리",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "빨리", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 33,
"transcript": "천천히",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "천천히", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 34,
"transcript": "항상",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "항상", "start_ms": 65, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 35,
"transcript": "자주",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "자주", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 36,
"transcript": "가끔",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "가끔", "start_ms": 70, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 37,
"transcript": "전혀",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "전혀", "start_ms": 50, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 38,
"transcript": "많이",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "많이", "start_ms": 80, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 39,
"transcript": "조금",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "조금", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 40,
"transcript": "매우",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "매우", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
]
},
{
"segment": 41,
"transcript": "진짜",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "진짜", "start_ms": 40, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 42,
"transcript": "정말",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 55,
"audio_type": "speech",
"words": [
{"word": "정말", "start_ms": 55, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 43,
"transcript": "함께",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 35,
"audio_type": "speech",
"words": [
{"word": "함께", "start_ms": 35, "dur_ms": 520, "phase_lr_deg": 0.0}
]
},
{
"segment": 44,
"transcript": "혼자",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 65,
"audio_type": "speech",
"words": [
{"word": "혼자", "start_ms": 65, "dur_ms": 500, "phase_lr_deg": 0.0}
]
},
{
"segment": 45,
"transcript": "다시",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 40,
"audio_type": "speech",
"words": [
{"word": "다시", "start_ms": 40, "dur_ms": 510, "phase_lr_deg": 0.0}
]
},
{
"segment": 46,
"transcript": "먼저",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 70,
"audio_type": "speech",
"words": [
{"word": "먼저", "start_ms": 70, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 47,
"transcript": "나중에",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 50,
"audio_type": "speech",
"words": [
{"word": "나중에", "start_ms": 50, "dur_ms": 490, "phase_lr_deg": 0.0}
]
},
{
"segment": 48,
"transcript": "그리고",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 80,
"audio_type": "speech",
"words": [
{"word": "그리고", "start_ms": 80, "dur_ms": 480, "phase_lr_deg": 0.0}
]
},
{
"segment": 49,
"transcript": "하지만",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 30,
"audio_type": "speech",
"words": [
{"word": "하지만", "start_ms": 30, "dur_ms": 530, "phase_lr_deg": 0.0}
]
},
{
"segment": 50,
"transcript": "그래서",
"speaker": "speaker_1",
"confidence": 0.99,
"language": "ko",
"silence_before_ms": 60,
"audio_type": "speech",
"words": [
{"word": "그래서", "start_ms": 60, "dur_ms": 450, "phase_lr_deg": 0.0}
]
}
]

---
*Generated by WKAppBot ask gemini — 2026-04-03 09:19:32*
