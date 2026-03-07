#!/bin/bash
# ──────────────────────────────────────────────────────
# daily_walk.sh — 자정 전 걷기 포인트 자동 수령
# 대상 앱 3종: 경기도 기후행동, 모니모, 만보시루
# 사용: bash scenarios/daily_walk.sh
# 예약: Windows 작업 스케줄러로 23:55에 실행
#
# 전략: a11y 트리 탐색 기반 (FLAG_SECURE 앱도 OK)
#   - 좌표 하드코딩 최소화
#   - wkappbot a11y #scope 매칭으로 정확한 요소 클릭
#   - 스크린샷 불가 화면은 a11y find로 텍스트 탐색
# ──────────────────────────────────────────────────────

set -e
WK="wkappbot a11y"
ADB="adb://"
OUTDIR="W:/SDK/bin/wkappbot.hq/output"

# ── Helpers ────────────────────────────────────────────

log() { echo "[$(date +%H:%M:%S)] $*"; }

# a11y 기반 클릭 (scope 매칭)
a11y_click() {
    local grap="$1"
    log "  a11y click: $grap"
    $WK click "$grap" 2>&1 | tail -1
}

# a11y 트리에서 텍스트 검색 (FLAG_SECURE 화면용)
a11y_find() {
    local grap="$1" pattern="$2"
    $WK find "$grap" 2>&1 | grep -i "$pattern" | head -3
}

# a11y inspect로 트리 텍스트 덤프
a11y_tree() {
    local grap="$1" depth="${2:-20}"
    $WK inspect "$grap" --depth "$depth" 2>&1
}

# 스크린샷 (FLAG_SECURE 아닌 앱용)
screenshot() {
    local name="$1"
    $WK screenshot "$ADB" -o "$OUTDIR/$name" >/dev/null 2>&1 || true
}

launch() {
    local pkg="$1"
    $WK eval "$ADB" --text "monkey -p $pkg -c android.intent.category.LAUNCHER 1" >/dev/null 2>&1
}

close_app() { $WK close "adb://*$1*" >/dev/null 2>&1 || true; }
back() { $WK back "$ADB" >/dev/null 2>&1; }
home() { $WK home "$ADB" >/dev/null 2>&1; }

# ══════════════════════════════════════════════════════
# 1. 경기도 기후행동 — 열기만 하면 포인트 적립
# ══════════════════════════════════════════════════════

daily_ggaction() {
    log "═══ [1/3] 경기도 기후행동 ═══"

    launch "kr.or.ggaction"
    log "앱 실행 대기..."
    sleep 8  # WebView 로딩 느림

    screenshot "daily_ggaction.png"
    log "포인트 적립 완료 (앱 열기만 하면 OK)"

    close_app "ggaction"
    sleep 1
    log "경기도 기후행동 완료!"
}

# ══════════════════════════════════════════════════════
# 2. 모니모 — 메뉴 → 걷기 → 젤리 받기
# ══════════════════════════════════════════════════════
# NOTE: Compose + WebView 앱, FLAG_SECURE 전체 적용
#       스크린샷 불가 → 100% a11y 트리 탐색으로 자동화
#       Fold5 외부화면(904x2316) 기준
#
# 화면 구조 (a11y depth 25):
#   메인메뉴: 좌=카테고리(최근/모니모/삼성생명...) 우=혜택 리스트(걷기/함께걷기/빙고...)
#   걷기화면: 걸음 수, 5000걸음 목표, 달성 시 "젤리 받기" 버튼 출현

daily_monimo() {
    log "═══ [2/3] 모니모 ═══"

    launch "net.ib.android.smcard"
    log "앱 실행 대기..."
    sleep 6

    # Step 1: 메인 메뉴에서 "걷기" 클릭 (a11y scope 매칭)
    # a11y tree: [Button]* 걷기  [275,749][879,869]
    log "메뉴에서 '걷기' 탭..."
    a11y_click "adb://*smcard*#걷기"
    sleep 4

    # Step 2: 걷기 화면 — a11y 트리로 상태 확인
    # FLAG_SECURE → 스크린샷 불가, a11y만 사용
    log "걷기 화면 a11y 분석..."
    local tree
    tree=$(a11y_tree "adb://*smcard*" 20)

    # 젤리 받기 / 포인트 받기 버튼 탐색
    if echo "$tree" | grep -qi "젤리.*받"; then
        log "젤리 받기 버튼 발견!"
        a11y_click "adb://*smcard*#*젤리*받*"
        sleep 2
        log "젤리 수령 완료!"
    elif echo "$tree" | grep -qi "받기"; then
        log "'받기' 버튼 발견!"
        a11y_click "adb://*smcard*#*받기*"
        sleep 2
        log "포인트 수령 완료!"
    elif echo "$tree" | grep -qi "미션 시작 전"; then
        log "미션 시작 전 — 걸음 수 부족 또는 아직 미시작"
    else
        log "걷기 화면 상태 불명 — 트리 일부:"
        echo "$tree" | grep -i "걸음\|젤리\|받기\|달성\|미션" | head -5
    fi

    # Step 3: 정리
    back
    sleep 1
    close_app "smcard"
    sleep 1
    log "모니모 완료!"
}

# ══════════════════════════════════════════════════════
# 3. 만보시루 — 포인트 확인 → 걸음수 → 기부/지역화폐
# ══════════════════════════════════════════════════════
# 만보시루는 FLAG_SECURE 아님 → 스크린샷+OCR 사용 가능
# 하지만 a11y도 병행하여 안정성 확보
#
# 홈 → [포인트 확인] → 걸음수 S자 경로 → 달성분 클릭 → 기부/지역화폐
# 좌표는 Fold5 외부화면(904x2316) 기준, a11y 보완

daily_manbosiru() {
    log "═══ [3/3] 만보시루 ═══"

    launch "manbosiru.com"
    log "앱 실행 대기..."
    sleep 5

    # Step 1: a11y로 "포인트 확인" 버튼 탐색
    log "포인트 확인 탭..."
    local tree
    tree=$(a11y_tree "adb://*manbosiru*" 15)

    if echo "$tree" | grep -qi "포인트.*확인"; then
        a11y_click "adb://*manbosiru*#*포인트*확인*"
    else
        # 폴백: 하단 좌표 (포인트 확인 버튼 추정)
        log "  a11y 매칭 실패 → 좌표 폴백"
        $WK eval "$ADB" --text "input tap 451 1760" >/dev/null 2>&1
    fi
    sleep 4

    # Step 2: a11y로 걸음수/기부/지역화폐 탐색
    tree=$(a11y_tree "adb://*manbosiru*" 15)

    # 달성한 걸음수 버튼 찾기 (높은 것부터)
    local clicked=false
    for keyword in "10,000" "10000" "7,000" "7000" "5,000" "5000" "3,000" "3000"; do
        if echo "$tree" | grep -qi "$keyword"; then
            log "  ${keyword}보 달성 감지!"
            a11y_click "adb://*manbosiru*#*${keyword}*"
            sleep 3

            # 기부/지역화폐 화면 탐색
            tree=$(a11y_tree "adb://*manbosiru*" 15)

            if echo "$tree" | grep -qi "기부"; then
                log "  기부하기 선택..."
                a11y_click "adb://*manbosiru*#*기부*"
                sleep 2
                # 확인 버튼
                if echo "$(a11y_tree "adb://*manbosiru*" 10)" | grep -qi "확인"; then
                    a11y_click "adb://*manbosiru*#*확인*"
                    sleep 2
                fi
            elif echo "$tree" | grep -qi "지역화폐"; then
                log "  지역화폐 수령..."
                a11y_click "adb://*manbosiru*#*지역화폐*"
                sleep 2
            fi

            clicked=true
            break
        fi
    done

    if [ "$clicked" = false ]; then
        log "  달성한 걸음수 없음 (오늘 못 걸었나..?)"
    fi

    close_app "manbosiru"
    sleep 1
    log "만보시루 완료!"
}

# ══════════════════════════════════════════════════════
# Main — 순차 실행
# ══════════════════════════════════════════════════════

main() {
    log "╔══════════════════════════════════════╗"
    log "║  데일리워크 자동 포인트 수령 시작!    ║"
    log "╚══════════════════════════════════════╝"

    # 화면 켜기
    $WK eval "$ADB" --text "input keyevent KEYCODE_WAKEUP" >/dev/null 2>&1
    sleep 1

    daily_ggaction
    daily_monimo
    daily_manbosiru

    # 홈으로 돌아가기
    home

    log "╔══════════════════════════════════════╗"
    log "║  데일리워크 완료!                     ║"
    log "╚══════════════════════════════════════╝"

    # Slack으로 결과 보고
    wkappbot slack send "데일리워크 자동 포인트 수령 완료! (경기도기후행동 + 모니모 + 만보시루)" 2>/dev/null || true
}

main "$@"
