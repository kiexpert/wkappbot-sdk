# Whisper Key SoundCode — 구현 플래닝
> 삼두협의 Round 2 + Claude 취합 2026-03-11

## Phase 1 — Quick Wins (1-2일)

### 1-A. Gray Coding (1시간, 즉시 효과)
**무엇**: 3-bit 밴드 인덱스를 `gray = n ^ (n >> 1)` 변환 후 SoundCode 패킹
**왜**: 인접 밴드 드리프트 → 1비트 차이만 발생, 오판 폭발 방지
**스킵**: 디코더 테이블도 같이 추가 (역변환 `while(n>>=1) g^=n`)

### 1-B. 음절 토큰화 (2시간)
**무엇**: confirmed 프레임 연속 → `{code, frames}` 토큰으로 압축
**왜**: 100fps 슬라이딩이 만드는 jitter 즉시 제거
**구현**: 동일 코드 연속 N프레임 → 하나의 토큰으로 병합

### 1-C. 발화 게이트 (Gemini 추가, 2시간)
**무엇**: FFT 전체 에너지 상승 + 최소 음성 프레임 수 + 침묵 꼬리 감지
**왜**: 배경 소음이 아닌 실제 발화 구간만 매처에 전달
**구현**: 에너지 임계값 상승 → onset, 임계값 하락 200ms → offset

### 1-D. 거리 함수 교체 (3시간)
**무엇**: Hamming → 순위 인식 비용함수
```
cost(A, B) = Σ position_weight[i] × band_dist[Ai][Bi]
band_dist = 밴드 인덱스 0..7 순환거리 (Gray 코딩 후 1비트 차이면 비용 낮음)
```
**왜**: 순열 코드에 Hamming은 부적절 — 순서 차이가 의미 있음

**Phase 1 스킵**: Trie, DTW, 복잡한 메트릭, 음소 매핑

---

## Phase 2 — Core Pipeline (1주)

### 2-A. Weighted Edit Distance (1일)
**무엇**: DTW 대신 토큰 시퀀스에 가중 편집 거리 적용
```
비용: insert/delete = len_penalty, substitute = rank_distance(A, B)
```
**왜**: 1-3음절 명령어엔 DTW 오버스펙. WED가 더 빠르고 충분
**Gemini 추가**: 모음 핵(anchor) 코드에 가중치 높임, 자음 전환부 가중치 낮춤

### 2-B. 명령 템플릿 (반나절)
**무엇**: 명령어당 3-10개 실발화 토큰 시퀀스 저장 (변형 포함)
**구현**: JSON/JSONL 파일 per-command, 수동 녹음으로 시작

### 2-C. 스트리밍 Prefix Beam Search (2일)
**무엇**: 슬라이딩 최근 12토큰 → 활성 명령 후보 WED 점수 → threshold 이하 시 발화
**GPT**: 50개 명령 미만이면 Trie 불필요 → 단순 후보 리스트로 충분

**Phase 2 스킵**: Trie (명령어 많아질 때까지 보류), 멀티유저 모델링, 클라우드

---

## Phase 3 — Adaptive Learning (1-2주)

### 3-A. 고신뢰 토큰 수집 (1일)
**무엇**: WED 점수 상위 10% 확인 발화만 저장 (실수 학습 방지)

### 3-B. Template Centroid 업데이트 (GPT: medoid, Gemini: average) (2-3일)
**무엇**: 기존 템플릿 + 새 발화 → 중심값 갱신
```
GPT 방식: k-medoids (실제 존재하는 대표 시퀀스)
Gemini 방식: 이동 평균 (구현 더 쉬움)
추천: medoid가 더 안전 (평균이면 drift 위험)
```

### 3-C. 동적 임계값 (1일)
**무엇**: 명령어별 성공 이력 분산으로 accept threshold 자동 조정
**왜**: 아침/저녁, 피로도에 따라 같은 사람도 발음 변함

**Phase 3 스킵**: 딥러닝/신경망, 음소 anchoring (연구 과제)

---

## GPT vs Gemini 주요 차이점

| 항목 | GPT | Gemini | 채택 |
|------|-----|--------|------|
| Phase 1 완료 시간 | ~6시간 | 1-2일 | GPT (더 구체적) |
| 매처 방식 | Prefix Beam Search | WED + Windowing | 동일 본질 |
| 템플릿 업데이트 | k-medoids | Moving Average | GPT medoid (drift 안전) |
| Trie | >50개 명령에서만 | 언급 없음 | GPT |
| 발화 게이트 | Energy gate | Syllable Anchoring | Gemini (더 구체적) |

## 구현 우선순위 최종 정리

```
[즉시] Gray Coding → 음절 토큰화 → 발화 게이트
[1주]  WED 매처 → 템플릿 파일 포맷 → Beam Search
[2주+] Template Centroid 업데이트 → 동적 임계값
[보류] Trie (50개↑), DTW, Syllable Anchoring, 신경망
```
