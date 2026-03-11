# Whisper Key — Automata Command Recognition
> 삼두협의체(GPT + Gemini) 자문 결과 2026-03-10

## 현재 아키텍처

| 항목 | 내용 |
|------|------|
| FFT | 512 samples, 32ms window, 100fps sliding |
| 밴드 | 8-band × 4bit = 32bit raw token |
| **음코드** | **15-bit SoundCode**: top-5 bands by noise-subtracted clean energy, 각 밴드 인덱스(0-7) = 3-bit → rank-order permutation |
| 인코딩 | MSB: rank1(bit14-12) \| rank2(bit11-9) \| rank3(bit8-6) \| rank4(bit5-3) \| rank5(bit2-0) |
| 확인 | 음절 윈도우: N 프레임 연속 동일 코드 → confirmed |
| 노이즈 | DUET 스테레오 ILD+IPD 공간 분리 |
| 특징 | **순위 순열 코드 → 노이즈 불변** (절대 에너지가 아닌 상대 순위) |

## GPT 자문 (2026-03-10, 교정 버전)

### 핵심 파이프라인
1. **음절 토큰화**: confirmed SoundCode 스트림 → 반복 제거 → `{code, frames}` 음절 토큰
2. **명령 템플릿**: 음절별 코드 시퀀스 + 지속시간 범위 저장 (변형 여러 개)
3. **Prefix Automaton Trie**: 각 노드에 예상 다음 코드 저장, Hamming ≤ 1 허용 (8×8 비용 테이블 사전계산)
4. **순위 거리 스코어링**:
   ```
   cost = Σ position_weight[i] × band_dist[Ai][Bi]
   band_dist = 밴드 인덱스 순환거리 or 학습된 혼동 행렬
   ```
5. **DTW + 지속시간 가중**:
   ```
   local_cost = rank_distance × α + |durA - durB| × β
   Sakoe-Chiba 밴드 ±2 토큰 제한
   ```
6. **슬라이딩 윈도우 트리거**: 최근 12토큰 유지 → 각 템플릿에 증분 DTW → score < threshold 시 발화

### 추가 개선안 (GPT)
- **(a) rank-gap 인코딩**: 에너지 비율을 2-3 extra bits로 추가
- **(b) 혼동 행렬 학습**: 실제 발화 데이터로 band_dist 학습
- **(c) k-medoids 클러스터링**: ~64 메타코드로 클러스터링 → 노이즈 안정화

## Gemini 자문 (2026-03-10, 새 세션)

### 핵심 제안
1. **Weighted Edit Distance**: DTW 대신 에너지 밴드 인접성 기반 가중 편집 거리 (DTW보다 저비용)
2. **Prefix Trie + Kendall Tau 거리**: 순위 지터 허용, 트리 과다 분기 방지
3. **15-bit 인코딩 개선 3가지**:
   - **Bit-masking**: 5×3-bit 청크별 Hamming weight 체크로 후보 먼저 필터링
   - **Syllable Anchoring**: SoundCode 순열 → 한국어 자모(초성/중성/종성) 매핑
   - **Gray Coding**: 3-bit 밴드 인덱스를 Gray 코드로 재인코딩 → 인접 주파수 밴드 = 1비트 차이 → 양자화 노이즈 내성 향상

```python
def rank_distance(code1, code2):
    # Spearman's footrule distance on 15-bit SoundCode
    r1 = [(code1 >> (3 * i)) & 0x7 for i in range(5)]
    r2 = [(code2 >> (3 * i)) & 0x7 for i in range(5)]
    return sum(abs(a - b) for a, b in zip(r1, r2))
```

## GPT + Gemini 공통 결론

| 항목 | 결론 |
|------|------|
| 매칭 방식 | **Prefix Trie** + rank-distance (저지연 트리거에 최적) |
| 거리 함수 | **Kendall Tau** or **Spearman footrule** (순열 거리) |
| DTW | 유효하나 비용 높음 → 증분 DTW + Sakoe-Chiba 제한으로 최적화 |
| **Gray Coding** | **즉시 적용 가능** — 인코딩 방식만 바꾸면 됨, 노이즈 내성 향상 |
| 문장 경계 | 침묵 대기보다 F0 에너지 하락 추세 (200ms) 감지 |

## 우선 구현 후보

1. **Gray Coding** (쉬움): 3-bit 밴드 인덱스 Gray 인코딩 → SoundCode 계산 시 `GrayEncode(bandIdx)` 적용
2. **음절 토큰화**: `{code, frames}` 압축 → 슬라이딩 DTW 입력
3. **Prefix Trie**: 명령 템플릿 저장 + Hamming/Kendall 거리 매칭
4. **rank-gap bits**: 상위 5밴드 에너지 비율을 보조 비트로 추가

## Gray Coding 변환표 (3-bit)

| 밴드 인덱스 (Binary) | Gray Code | 의미 |
|---------------------|-----------|------|
| 000 (0) | 000 | VxLip |
| 001 (1) | 001 | Pharx |
| 010 (2) | 011 | Velar |
| 011 (3) | 010 | OralR |
| 100 (4) | 110 | HiRes |
| 101 (5) | 111 | Burst |
| 110 (6) | 101 | Sibil |
| 111 (7) | 100 | Breth |

> Gray 코드: `gray = n ^ (n >> 1)` — 인접 값이 항상 1비트만 다름
