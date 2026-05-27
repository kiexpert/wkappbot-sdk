GOAL: Add Heroes4 HTS (MFC/Win32) failure-pattern reproductions to LegacyControlZoo. Working dir D:/GitHub/wkappbot-sdk.

Two files only:
  FILE1: D:/GitHub/wkappbot-sdk/test/legacy-app/LegacyControlZoo.cpp  (~416 lines)
  FILE2: D:/GitHub/wkappbot-sdk/test/legacy-app/test-legacy-app.ps1   (~224 lines)

READ both files first. Apply edits described below. Do NOT compile, do NOT git commit, do NOT push. Just edit and report.

==============================================================================
FILE 1 EDITS (LegacyControlZoo.cpp)
==============================================================================

(A) Add new IDs right after the line:
    #define IDC_CUSTOM_PANEL    111
Insert:
    // HTS (Heroes4) pattern reproduction controls
    #define IDC_MASKEDIT        120
    #define IDC_BTN_ICON_BUY    121
    #define IDC_BTN_ICON_SELL   122
    #define IDC_PRICE_GRID      123
    #define IDC_MODAL_BTN       124
    #define IDC_MODAL_OVERLAY   125

(B) Insert THREE new static functions BEFORE NoAccessibilityWindowProc (after DrawTextLabel). Paste verbatim:

static LRESULT CALLBACK MaskEditSubclassProc(HWND hwnd, UINT msg, WPARAM wParam,
                                             LPARAM lParam, UINT_PTR, DWORD_PTR)
{
    switch (msg) {
    case WM_SETTEXT:
        return FALSE;
    case WM_CHAR: {
        if (wParam == VK_BACK) return DefSubclassProc(hwnd, msg, wParam, lParam);
        if (wParam < 0x30 || wParam > 0x39) { MessageBeep(MB_ICONEXCLAMATION); return 0; }
        if (GetWindowTextLengthW(hwnd) >= 6) { MessageBeep(MB_ICONEXCLAMATION); return 0; }
        return DefSubclassProc(hwnd, msg, wParam, lParam);
    }
    case WM_GETOBJECT:
        return 0;
    }
    return DefSubclassProc(hwnd, msg, wParam, lParam);
}

static LRESULT CALLBACK ModalOverlayProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg) {
    case WM_CREATE:
        SetTimer(hwnd, 1, 3000, nullptr);
        return 0;
    case WM_TIMER:
        if (wParam == 1) {
            KillTimer(hwnd, 1);
            HWND owner = GetWindow(hwnd, GW_OWNER);
            if (owner) EnableWindow(owner, TRUE);
            DestroyWindow(hwnd);
            return 0;
        }
        break;
    case WM_PAINT: {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hwnd, &ps);
        RECT rc;
        GetClientRect(hwnd, &rc);
        FillRectColor(hdc, rc, RGB(255, 255, 200));
        DrawTextLabel(hdc, L"Modal Dialog (auto-close 3s)", rc, RGB(0, 0, 0),
                      DT_CENTER | DT_VCENTER | DT_SINGLELINE);
        EndPaint(hwnd, &ps);
        return 0;
    }
    case WM_DESTROY: {
        HWND owner = GetWindow(hwnd, GW_OWNER);
        if (owner) EnableWindow(owner, TRUE);
        return 0;
    }
    }
    return DefWindowProcW(hwnd, msg, wParam, lParam);
}

static void OpenModalOverlay(HWND parent)
{
    HWND topLevel = GetAncestor(parent, GA_ROOT);
    RECT pr;
    GetWindowRect(topLevel, &pr);
    int w = 360, h = 120;
    int x = pr.left + ((pr.right - pr.left) - w) / 2;
    int y = pr.top + ((pr.bottom - pr.top) - h) / 2;
    EnableWindow(topLevel, FALSE);
    CreateWindowExW(WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
        L"LegacyControlZooModalOverlay", L"Modal Dialog",
        WS_POPUP | WS_VISIBLE | WS_BORDER | WS_CAPTION,
        x, y, w, h, topLevel, nullptr, g_hinst, nullptr);
}

(C) Append HTS controls at the END of CreateChildControls(HWND child) -- just before the closing brace. The .cpp file is UTF-8; use Korean Hangul directly:

    // === HTS (Heroes4) pattern controls -- start ===
    AddStatic(child, L"종목코드:", 14, 400, 70, 20);
    HWND maskEdit = CreateWindowExW(WS_EX_CLIENTEDGE, WC_EDITW, L"",
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | ES_AUTOHSCROLL,
        86, 398, 110, 24, child, reinterpret_cast<HMENU>(IDC_MASKEDIT), g_hinst, nullptr);
    SendMessageW(maskEdit, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);
    SetWindowSubclass(maskEdit, MaskEditSubclassProc, 0, 0);

    AddStatic(child, L"매수", 210, 400, 36, 20);
    CreateWindowExW(0, WC_BUTTONW, L"",
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | BS_OWNERDRAW,
        210, 420, 50, 30, child, reinterpret_cast<HMENU>(IDC_BTN_ICON_BUY), g_hinst, nullptr);
    AddStatic(child, L"매도", 268, 400, 36, 20);
    CreateWindowExW(0, WC_BUTTONW, L"",
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | BS_OWNERDRAW,
        268, 420, 50, 30, child, reinterpret_cast<HMENU>(IDC_BTN_ICON_SELL), g_hinst, nullptr);

    AddStatic(child, L"호가창 (OCR target)", 334, 400, 180, 20);
    HWND priceGrid = CreateWindowExW(WS_EX_CLIENTEDGE, WC_LISTBOXW, nullptr,
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | LBS_OWNERDRAWFIXED | LBS_NOSEL,
        334, 420, 190, 80, child, reinterpret_cast<HMENU>(IDC_PRICE_GRID), g_hinst, nullptr);
    SendMessageW(priceGrid, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);
    SendMessageW(priceGrid, LB_ADDSTRING, 0, reinterpret_cast<LPARAM>(L"071,500 ▲ +1.5%"));
    SendMessageW(priceGrid, LB_ADDSTRING, 0, reinterpret_cast<LPARAM>(L"071,400 ▲ +1.3%"));
    SendMessageW(priceGrid, LB_ADDSTRING, 0, reinterpret_cast<LPARAM>(L"071,300 ▼ -0.1%"));

    HWND btnModal = CreateWindowExW(0, WC_BUTTONW, L"Open Modal",
        WS_CHILD | WS_VISIBLE | WS_TABSTOP,
        540, 420, 110, 30, child, reinterpret_cast<HMENU>(IDC_MODAL_BTN), g_hinst, nullptr);
    SendMessageW(btnModal, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);
    // === HTS pattern controls -- end ===

(D) Extend ChildProc:

  D1) Inside the existing WM_DRAWITEM case, after the IDC_OWNER_BUTTON branch and BEFORE the closing "return FALSE;" of that case, insert:

        if (draw->CtlID == IDC_BTN_ICON_BUY) {
            FillRectColor(draw->hDC, draw->rcItem, RGB(40, 160, 80));
            Rectangle(draw->hDC, draw->rcItem.left, draw->rcItem.top,
                      draw->rcItem.right, draw->rcItem.bottom);
            return TRUE;
        }
        if (draw->CtlID == IDC_BTN_ICON_SELL) {
            FillRectColor(draw->hDC, draw->rcItem, RGB(200, 60, 60));
            Rectangle(draw->hDC, draw->rcItem.left, draw->rcItem.top,
                      draw->rcItem.right, draw->rcItem.bottom);
            return TRUE;
        }
        if (draw->CtlID == IDC_PRICE_GRID) {
            wchar_t text[128] = {};
            SendMessageW(draw->hwndItem, LB_GETTEXT, draw->itemID,
                         reinterpret_cast<LPARAM>(text));
            COLORREF bg = (draw->itemID % 2) ? RGB(248, 248, 248) : RGB(255, 255, 255);
            FillRectColor(draw->hDC, draw->rcItem, bg);
            RECT t = draw->rcItem; t.left += 6;
            COLORREF fg = (wcschr(text, L'▲') != nullptr) ? RGB(200, 30, 30)
                        : (wcschr(text, L'▼') != nullptr) ? RGB(0, 80, 200)
                        : RGB(20, 20, 20);
            DrawTextLabel(draw->hDC, text, t, fg);
            return TRUE;
        }

  D2) Inside the existing WM_MEASUREITEM case, before "return FALSE;", insert:

        if (measure->CtlID == IDC_PRICE_GRID) {
            measure->itemHeight = 22;
            return TRUE;
        }

  D3) Add a NEW case to ChildProc top-level switch:

        case WM_COMMAND:
            if (LOWORD(wParam) == IDC_MODAL_BTN && HIWORD(wParam) == BN_CLICKED) {
                OpenModalOverlay(hwnd);
                return 0;
            }
            break;

(E) In wWinMain, near the existing RegisterWindowClass(...) calls, register the modal overlay class:

    RegisterWindowClass(L"LegacyControlZooModalOverlay", ModalOverlayProc,
        reinterpret_cast<HBRUSH>(COLOR_INFOBK + 1));

==============================================================================
FILE 2 EDITS (test-legacy-app.ps1)
==============================================================================

INSERT AFTER the existing test 26 (clipboard-read) block and BEFORE the Write-Host "=== TEARDOWN ===" line. The "27. close" test stays last.

Block to insert (use Korean Hangul directly; ps1 saved as UTF-8 is fine):

Write-Host ""
Write-Host "=== HTS PATTERNS (영웅문 재현) ==="
Write-Host ""

# HTS-1: UIA should FAIL on masked edit (no accessible name)
$script:TestCount++
Write-Host -NoNewline "[$('{0:d2}' -f $script:TestCount)] HTS-1 mask-edit UIA blind (expect NO TARGET) ... "
$r = Invoke-WK @("a11y", "find", "*종목코드*")
if ($r.Output -match "NO TARGET|not found|no match" -or -not $r.Ok) {
    Write-Host "PASS (correct UIA failure)"
    $script:TotalPass++
} else {
    Write-Host "BUG: UIA found masked edit by Korean label (false positive)"
    $script:TotalSoftFail++
}

# HTS-2: WM_CHAR path on masked edit (Win32 tier 2)
Run-Test "HTS-2 mask-edit WM_CHAR type" @("a11y", "type", $grap, "005930", "--force") "\[OK\]" $false | Out-Null

# HTS-3: OCR fallback reads price grid text
$script:TestCount++
Write-Host -NoNewline "[$('{0:d2}' -f $script:TestCount)] HTS-3 OCR price-grid pattern ... "
$r = Invoke-WK @("a11y", "ocr", $grap) 30
if ($r.Output -match "(\d{3},\d{3}|%)") {
    Write-Host "PASS (price pattern found in OCR)"
    $script:TotalPass++
} else {
    Write-Host "SOFT-FAIL (OCR returned no price pattern)"
    $script:TotalSoftFail++
}

# HTS-4: Icon-only button find
$script:TestCount++
Write-Host -NoNewline "[$('{0:d2}' -f $script:TestCount)] HTS-4 icon-button no-name lookup ... "
$r = Invoke-WK @("a11y", "find", "*매수*")
if ($r.Ok -and $r.Output -match "# TARGET") {
    Write-Host "PASS (label-by-proximity match worked)"
    $script:TotalPass++
} else {
    Write-Host "HTS-PATTERN (icon button has no UIA name -- expected miss)"
    $script:TotalSoftFail++
}

# HTS-5: Modal dialog detection
$script:TestCount++
Write-Host -NoNewline "[$('{0:d2}' -f $script:TestCount)] HTS-5 modal detection ... "
Invoke-WK @("a11y", "click", $grap, "Open Modal") | Out-Null
Start-Sleep -Milliseconds 500
$r = Invoke-WK @("a11y", "find", "{title:'Modal Dialog'}") 5
if ($r.Output -match "# TARGET|Modal") {
    Write-Host "PASS (modal detected)"
    $script:TotalPass++
} else {
    Write-Host "SOFT-FAIL (modal not detected via a11y find)"
    $script:TotalSoftFail++
}

# Wait for modal auto-close
Start-Sleep -Seconds 4

==============================================================================
EXIT CRITERION (self-verify)
==============================================================================
1) Line count of LegacyControlZoo.cpp < 600 (target 480-540).
2) cpp contains: IDC_MASKEDIT, IDC_BTN_ICON_BUY, IDC_BTN_ICON_SELL, IDC_PRICE_GRID, IDC_MODAL_BTN, MaskEditSubclassProc, ModalOverlayProc, OpenModalOverlay.
3) ps1 contains: "HTS-1 mask-edit", "HTS-2 mask-edit", "HTS-3 OCR", "HTS-4 icon-button", "HTS-5 modal".
4) Do NOT compile. Do NOT git commit. Do NOT push.
5) Report cpp line count and ps1 HTS test names after editing.