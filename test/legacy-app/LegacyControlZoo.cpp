#ifndef UNICODE
#define UNICODE
#endif
#ifndef _UNICODE
#define _UNICODE
#endif

#include <windows.h>
#include <commctrl.h>
#include <tchar.h>

#include <vector>

#pragma comment(lib, "comctl32.lib")
#pragma comment(lib, "user32.lib")
#pragma comment(lib, "gdi32.lib")

#define IDC_MDI_CLIENT      100
#define IDC_MENU_BAR        101
#define IDC_TOOLBAR         102
#define IDC_SINGLE_EDIT     103
#define IDC_MULTI_EDIT      104
#define IDC_OWNER_LIST      105
#define IDC_COMBO           106
#define IDC_OWNER_BUTTON    107
#define IDC_STATUS          108
#define IDC_TREE            109
#define IDC_LISTVIEW        110
#define IDC_CUSTOM_PANEL    111
// HTS (Heroes4) pattern reproduction controls
#define IDC_MASKEDIT        120
#define IDC_BTN_ICON_BUY    121
#define IDC_BTN_ICON_SELL   122
#define IDC_PRICE_GRID      123
#define IDC_MODAL_BTN       124
#define IDC_MODAL_OVERLAY   125

#define IDM_MDI_CHILD       40001
#define WTV_OWNERDRAWFIXED  0x0400

static const wchar_t* kFrameClass = L"LegacyControlZooFrame";
static const wchar_t* kChildClass = L"LegacyControlZooMdiChild";
static const wchar_t* kMenuBarClass = L"LegacyControlZooOwnerMenuBar";
static const wchar_t* kToolbarClass = L"LegacyControlZooOwnerToolbar";
static const wchar_t* kPanelClass = L"LegacyControlZooPaintOnlyPanel";

static HINSTANCE g_hinst = nullptr;
static HWND g_mdiClient = nullptr;
static HFONT g_uiFont = nullptr;

static void FillRectColor(HDC hdc, const RECT& rc, COLORREF color)
{
    HBRUSH brush = CreateSolidBrush(color);
    FillRect(hdc, &rc, brush);
    DeleteObject(brush);
}

static void DrawTextLabel(HDC hdc, const wchar_t* text, RECT rc, COLORREF fg, UINT format = DT_LEFT | DT_VCENTER | DT_SINGLELINE)
{
    SetBkMode(hdc, TRANSPARENT);
    SetTextColor(hdc, fg);
    SelectObject(hdc, g_uiFont);
    DrawTextW(hdc, text, -1, &rc, format);
}

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

static LRESULT NoAccessibilityWindowProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    if (msg == WM_GETOBJECT) {
        return 0;
    }

    switch (msg) {
    case WM_PAINT: {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hwnd, &ps);
        RECT rc;
        GetClientRect(hwnd, &rc);
        FillRectColor(hdc, rc, RGB(248, 248, 248));
        FrameRect(hdc, &rc, reinterpret_cast<HBRUSH>(GetStockObject(GRAY_BRUSH)));

        if (GetDlgCtrlID(hwnd) == IDC_MENU_BAR) {
            RECT item = { 8, 4, 58, 26 };
            DrawTextLabel(hdc, L"File", item, RGB(20, 20, 20), DT_CENTER | DT_VCENTER | DT_SINGLELINE);
            item.left += 58; item.right += 58;
            DrawTextLabel(hdc, L"Edit", item, RGB(20, 20, 20), DT_CENTER | DT_VCENTER | DT_SINGLELINE);
            item.left += 58; item.right += 58;
            DrawTextLabel(hdc, L"View", item, RGB(20, 20, 20), DT_CENTER | DT_VCENTER | DT_SINGLELINE);
            item.left += 58; item.right += 58;
            DrawTextLabel(hdc, L"Window", item, RGB(20, 20, 20), DT_CENTER | DT_VCENTER | DT_SINGLELINE);
        } else if (GetDlgCtrlID(hwnd) == IDC_TOOLBAR) {
            const wchar_t* glyphs[] = { L"N", L"O", L"S", L"R" };
            for (int i = 0; i < 4; ++i) {
                RECT button = { 8 + i * 42, 5, 42 + i * 42, 31 };
                FillRectColor(hdc, button, RGB(225, 230, 238));
                Rectangle(hdc, button.left, button.top, button.right, button.bottom);
                DrawTextLabel(hdc, glyphs[i], button, RGB(0, 70, 130), DT_CENTER | DT_VCENTER | DT_SINGLELINE);
            }
            RECT label = { 190, 4, 390, 32 };
            DrawTextLabel(hdc, L"Owner-drawn icon toolbar", label, RGB(40, 40, 40));
        } else {
            RECT title = { 12, 10, rc.right - 12, 34 };
            DrawTextLabel(hdc, L"Custom Paint-Only Panel", title, RGB(15, 15, 15));
            RECT block1 = { 18, 48, rc.right - 18, 82 };
            RECT block2 = { 18, 98, rc.right - 18, 132 };
            FillRectColor(hdc, block1, RGB(214, 235, 245));
            FillRectColor(hdc, block2, RGB(238, 224, 210));
            DrawTextLabel(hdc, L"OCR Target: Painted Account Balance", block1, RGB(0, 0, 0), DT_CENTER | DT_VCENTER | DT_SINGLELINE);
            DrawTextLabel(hdc, L"OCR Target: Painted Action Required", block2, RGB(0, 0, 0), DT_CENTER | DT_VCENTER | DT_SINGLELINE);
        }

        EndPaint(hwnd, &ps);
        return 0;
    }
    }

    return DefWindowProcW(hwnd, msg, wParam, lParam);
}

static void AddStatic(HWND parent, const wchar_t* text, int x, int y, int w, int h)
{
    HWND label = CreateWindowExW(0, WC_STATICW, text, WS_CHILD | WS_VISIBLE, x, y, w, h, parent, nullptr, g_hinst, nullptr);
    SendMessageW(label, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);
}

static void PopulateTree(HWND tree)
{
    TVINSERTSTRUCTW item = {};
    item.hParent = TVI_ROOT;
    item.hInsertAfter = TVI_LAST;
    item.item.mask = TVIF_TEXT;
    item.item.pszText = const_cast<LPWSTR>(L"Painted Tree Root");
    HTREEITEM root = TreeView_InsertItem(tree, &item);

    item.hParent = root;
    item.item.pszText = const_cast<LPWSTR>(L"Custom Draw Child A");
    TreeView_InsertItem(tree, &item);
    item.item.pszText = const_cast<LPWSTR>(L"Custom Draw Child B");
    TreeView_InsertItem(tree, &item);
    TreeView_Expand(tree, root, TVE_EXPAND);
}

static void PopulateListView(HWND list)
{
    LVCOLUMNW col = {};
    col.mask = LVCF_TEXT | LVCF_WIDTH;
    col.cx = 110;
    col.pszText = const_cast<LPWSTR>(L"Column A");
    ListView_InsertColumn(list, 0, &col);
    col.cx = 130;
    col.pszText = const_cast<LPWSTR>(L"Column B");
    ListView_InsertColumn(list, 1, &col);

    for (int i = 0; i < 4; ++i) {
        wchar_t text[64];
        wsprintfW(text, L"Row %d", i + 1);
        LVITEMW item = {};
        item.mask = LVIF_TEXT;
        item.iItem = i;
        item.pszText = text;
        ListView_InsertItem(list, &item);
        wsprintfW(text, L"Report value %d", i + 1);
        ListView_SetItemText(list, i, 1, text);
    }
}

static void CreateChildControls(HWND child)
{
    AddStatic(child, L"Single-line EDIT (UIA tier 1)", 14, 12, 190, 20);
    HWND singleEdit = CreateWindowExW(WS_EX_CLIENTEDGE, WC_EDITW, L"standard edit text",
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | ES_AUTOHSCROLL,
        14, 34, 220, 24, child, reinterpret_cast<HMENU>(IDC_SINGLE_EDIT), g_hinst, nullptr);
    SendMessageW(singleEdit, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);

    AddStatic(child, L"Multi-line EDIT (UIA tier 1)", 252, 12, 190, 20);
    HWND multiEdit = CreateWindowExW(WS_EX_CLIENTEDGE, WC_EDITW, L"line one\r\nline two\r\nline three",
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | WS_VSCROLL | ES_MULTILINE | ES_AUTOVSCROLL,
        252, 34, 220, 84, child, reinterpret_cast<HMENU>(IDC_MULTI_EDIT), g_hinst, nullptr);
    SendMessageW(multiEdit, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);

    AddStatic(child, L"Owner-drawn LISTBOX (Win32/OCR)", 14, 72, 210, 20);
    HWND listbox = CreateWindowExW(WS_EX_CLIENTEDGE, WC_LISTBOXW, nullptr,
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | WS_VSCROLL | LBS_OWNERDRAWFIXED | LBS_HASSTRINGS,
        14, 94, 220, 112, child, reinterpret_cast<HMENU>(IDC_OWNER_LIST), g_hinst, nullptr);
    SendMessageW(listbox, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);
    SendMessageW(listbox, LB_ADDSTRING, 0, reinterpret_cast<LPARAM>(L"Painted item Alpha"));
    SendMessageW(listbox, LB_ADDSTRING, 0, reinterpret_cast<LPARAM>(L"Painted item Beta"));
    SendMessageW(listbox, LB_ADDSTRING, 0, reinterpret_cast<LPARAM>(L"Painted item Gamma"));

    AddStatic(child, L"Standard COMBOBOX", 252, 126, 190, 20);
    HWND combo = CreateWindowExW(0, WC_COMBOBOXW, nullptr,
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | CBS_DROPDOWNLIST,
        252, 148, 220, 120, child, reinterpret_cast<HMENU>(IDC_COMBO), g_hinst, nullptr);
    SendMessageW(combo, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);
    SendMessageW(combo, CB_ADDSTRING, 0, reinterpret_cast<LPARAM>(L"Combo choice one"));
    SendMessageW(combo, CB_ADDSTRING, 0, reinterpret_cast<LPARAM>(L"Combo choice two"));
    SendMessageW(combo, CB_SETCURSEL, 0, 0);

    HWND ownerButton = CreateWindowExW(0, WC_BUTTONW, L"",
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | BS_OWNERDRAW,
        252, 184, 220, 44, child, reinterpret_cast<HMENU>(IDC_OWNER_BUTTON), g_hinst, nullptr);
    SendMessageW(ownerButton, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);

    AddStatic(child, L"TREEVIEW custom draw", 14, 218, 180, 20);
    HWND tree = CreateWindowExW(WS_EX_CLIENTEDGE, WC_TREEVIEWW, nullptr,
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | TVS_HASLINES | TVS_LINESATROOT | TVS_HASBUTTONS | WTV_OWNERDRAWFIXED,
        14, 240, 220, 150, child, reinterpret_cast<HMENU>(IDC_TREE), g_hinst, nullptr);
    SendMessageW(tree, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);
    PopulateTree(tree);

    AddStatic(child, L"LISTVIEW report mode", 252, 238, 180, 20);
    HWND listView = CreateWindowExW(WS_EX_CLIENTEDGE, WC_LISTVIEWW, nullptr,
        WS_CHILD | WS_VISIBLE | WS_TABSTOP | LVS_REPORT | LVS_SINGLESEL,
        252, 260, 270, 130, child, reinterpret_cast<HMENU>(IDC_LISTVIEW), g_hinst, nullptr);
    SendMessageW(listView, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);
    PopulateListView(listView);

    HWND panel = CreateWindowExW(0, kPanelClass, nullptr, WS_CHILD | WS_VISIBLE,
        540, 34, 280, 356, child, reinterpret_cast<HMENU>(IDC_CUSTOM_PANEL), g_hinst, nullptr);
    SendMessageW(panel, WM_SETFONT, reinterpret_cast<WPARAM>(g_uiFont), TRUE);

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
}

static LRESULT CALLBACK ChildProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg) {
    case WM_CREATE:
        CreateChildControls(hwnd);
        return 0;
    case WM_MEASUREITEM: {
        MEASUREITEMSTRUCT* measure = reinterpret_cast<MEASUREITEMSTRUCT*>(lParam);
        if (measure->CtlID == IDC_OWNER_LIST) {
            measure->itemHeight = 24;
            return TRUE;
        }
        if (measure->CtlID == IDC_PRICE_GRID) {
            measure->itemHeight = 22;
            return TRUE;
        }
        return FALSE;
    }
    case WM_DRAWITEM: {
        DRAWITEMSTRUCT* draw = reinterpret_cast<DRAWITEMSTRUCT*>(lParam);
        if (draw->CtlID == IDC_OWNER_LIST) {
            wchar_t text[128] = {};
            SendMessageW(draw->hwndItem, LB_GETTEXT, draw->itemID, reinterpret_cast<LPARAM>(text));
            COLORREF bg = (draw->itemState & ODS_SELECTED) ? RGB(185, 210, 235) : RGB(250, 250, 250);
            FillRectColor(draw->hDC, draw->rcItem, bg);
            RECT textRc = draw->rcItem;
            textRc.left += 8;
            DrawTextLabel(draw->hDC, text, textRc, RGB(0, 0, 0));
            return TRUE;
        }
        if (draw->CtlID == IDC_OWNER_BUTTON) {
            FillRectColor(draw->hDC, draw->rcItem, RGB(40, 105, 150));
            Rectangle(draw->hDC, draw->rcItem.left, draw->rcItem.top, draw->rcItem.right, draw->rcItem.bottom);
            DrawTextLabel(draw->hDC, L"Owner Drawn BUTTON", draw->rcItem, RGB(255, 255, 255), DT_CENTER | DT_VCENTER | DT_SINGLELINE);
            return TRUE;
        }
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
        return FALSE;
    }
    case WM_COMMAND:
        if (LOWORD(wParam) == IDC_MODAL_BTN && HIWORD(wParam) == BN_CLICKED) {
            OpenModalOverlay(hwnd);
            return 0;
        }
        break;
    case WM_NOTIFY: {
        NMHDR* hdr = reinterpret_cast<NMHDR*>(lParam);
        if (hdr->idFrom == IDC_TREE && hdr->code == NM_CUSTOMDRAW) {
            NMTVCUSTOMDRAW* cd = reinterpret_cast<NMTVCUSTOMDRAW*>(lParam);
            if (cd->nmcd.dwDrawStage == CDDS_PREPAINT) {
                return CDRF_NOTIFYITEMDRAW;
            }
            if (cd->nmcd.dwDrawStage == CDDS_ITEMPREPAINT) {
                cd->clrText = RGB(30, 30, 30);
                cd->clrTextBk = RGB(235, 244, 235);
                return CDRF_DODEFAULT;
            }
        }
        break;
    }
    }

    return DefMDIChildProcW(hwnd, msg, wParam, lParam);
}

static void CreateInitialMdiChild(HWND frame)
{
    MDICREATESTRUCTW mcs = {};
    mcs.szTitle = L"MDI Child - Legacy Controls";
    mcs.szClass = kChildClass;
    mcs.hOwner = g_hinst;
    mcs.x = 12;
    mcs.y = 12;
    mcs.cx = 860;
    mcs.cy = 470;
    mcs.style = WS_VISIBLE | WS_OVERLAPPEDWINDOW;
    SendMessageW(g_mdiClient, WM_MDICREATE, 0, reinterpret_cast<LPARAM>(&mcs));
    SendMessageW(frame, WM_COMMAND, IDM_MDI_CHILD, 0);
}

static void LayoutFrame(HWND hwnd)
{
    RECT rc;
    GetClientRect(hwnd, &rc);
    const int menuHeight = 32;
    const int toolbarHeight = 38;
    const int statusHeight = 24;

    HWND menuBar = GetDlgItem(hwnd, IDC_MENU_BAR);
    HWND toolbar = GetDlgItem(hwnd, IDC_TOOLBAR);
    HWND status = GetDlgItem(hwnd, IDC_STATUS);

    MoveWindow(menuBar, 0, 0, rc.right, menuHeight, TRUE);
    MoveWindow(toolbar, 0, menuHeight, rc.right, toolbarHeight, TRUE);
    MoveWindow(g_mdiClient, 0, menuHeight + toolbarHeight, rc.right, rc.bottom - menuHeight - toolbarHeight - statusHeight, TRUE);
    MoveWindow(status, 0, rc.bottom - statusHeight, rc.right, statusHeight, TRUE);
}

static LRESULT CALLBACK FrameProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg) {
    case WM_CREATE: {
        CreateWindowExW(0, kMenuBarClass, nullptr, WS_CHILD | WS_VISIBLE,
            0, 0, 0, 0, hwnd, reinterpret_cast<HMENU>(IDC_MENU_BAR), g_hinst, nullptr);
        CreateWindowExW(0, kToolbarClass, nullptr, WS_CHILD | WS_VISIBLE,
            0, 0, 0, 0, hwnd, reinterpret_cast<HMENU>(IDC_TOOLBAR), g_hinst, nullptr);

        CLIENTCREATESTRUCT ccs = {};
        ccs.hWindowMenu = nullptr;
        ccs.idFirstChild = 50000;
        g_mdiClient = CreateWindowExW(WS_EX_CLIENTEDGE, L"MDICLIENT", nullptr,
            WS_CHILD | WS_CLIPCHILDREN | WS_VISIBLE,
            0, 0, 0, 0, hwnd, reinterpret_cast<HMENU>(IDC_MDI_CLIENT), g_hinst, &ccs);

        HWND status = CreateWindowExW(0, STATUSCLASSNAMEW, nullptr,
            WS_CHILD | WS_VISIBLE | SBARS_SIZEGRIP,
            0, 0, 0, 0, hwnd, reinterpret_cast<HMENU>(IDC_STATUS), g_hinst, nullptr);
        int parts[] = { 240, 520, -1 };
        SendMessageW(status, SB_SETPARTS, 3, reinterpret_cast<LPARAM>(parts));
        SendMessageW(status, SB_SETTEXT, 0 | SBT_OWNERDRAW, reinterpret_cast<LPARAM>(L"Status Painted Ready"));
        SendMessageW(status, SB_SETTEXT, 1 | SBT_OWNERDRAW, reinterpret_cast<LPARAM>(L"Status Painted HWND"));
        SendMessageW(status, SB_SETTEXT, 2 | SBT_OWNERDRAW, reinterpret_cast<LPARAM>(L"Status Painted No Text"));

        LayoutFrame(hwnd);
        CreateInitialMdiChild(hwnd);
        return 0;
    }
    case WM_SIZE:
        LayoutFrame(hwnd);
        return 0;
    case WM_DRAWITEM: {
        DRAWITEMSTRUCT* draw = reinterpret_cast<DRAWITEMSTRUCT*>(lParam);
        if (draw->CtlID == IDC_STATUS) {
            const wchar_t* text = reinterpret_cast<const wchar_t*>(draw->itemData);
            RECT rc = draw->rcItem;
            FillRectColor(draw->hDC, rc, RGB(236, 236, 236));
            rc.left += 8;
            DrawTextLabel(draw->hDC, text ? text : L"Status Painted", rc, RGB(20, 20, 20));
            return TRUE;
        }
        break;
    }
    case WM_COMMAND:
        if (LOWORD(wParam) == IDM_MDI_CHILD) {
            return 0;
        }
        break;
    case WM_DESTROY:
        PostQuitMessage(0);
        return 0;
    }

    return DefFrameProcW(hwnd, g_mdiClient, msg, wParam, lParam);
}

static ATOM RegisterWindowClass(const wchar_t* name, WNDPROC proc, HBRUSH brush)
{
    WNDCLASSEXW wc = {};
    wc.cbSize = sizeof(wc);
    wc.style = CS_HREDRAW | CS_VREDRAW;
    wc.lpfnWndProc = proc;
    wc.hInstance = g_hinst;
    wc.hCursor = LoadCursor(nullptr, IDC_ARROW);
    wc.hbrBackground = brush;
    wc.lpszClassName = name;
    return RegisterClassExW(&wc);
}

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE, PWSTR, int nCmdShow)
{
    g_hinst = hInstance;

    INITCOMMONCONTROLSEX icc = {};
    icc.dwSize = sizeof(icc);
    icc.dwICC = ICC_WIN95_CLASSES | ICC_BAR_CLASSES | ICC_TREEVIEW_CLASSES | ICC_LISTVIEW_CLASSES;
    InitCommonControlsEx(&icc);

    NONCLIENTMETRICSW metrics = {};
    metrics.cbSize = sizeof(metrics);
    SystemParametersInfoW(SPI_GETNONCLIENTMETRICS, sizeof(metrics), &metrics, 0);
    g_uiFont = CreateFontIndirectW(&metrics.lfMessageFont);

    RegisterWindowClass(kFrameClass, FrameProc, reinterpret_cast<HBRUSH>(COLOR_APPWORKSPACE + 1));
    RegisterWindowClass(kChildClass, ChildProc, reinterpret_cast<HBRUSH>(COLOR_WINDOW + 1));
    RegisterWindowClass(kMenuBarClass, NoAccessibilityWindowProc, reinterpret_cast<HBRUSH>(COLOR_BTNFACE + 1));
    RegisterWindowClass(kToolbarClass, NoAccessibilityWindowProc, reinterpret_cast<HBRUSH>(COLOR_BTNFACE + 1));
    RegisterWindowClass(kPanelClass, NoAccessibilityWindowProc, reinterpret_cast<HBRUSH>(COLOR_WINDOW + 1));
    RegisterWindowClass(L"LegacyControlZooModalOverlay", ModalOverlayProc,
        reinterpret_cast<HBRUSH>(COLOR_INFOBK + 1));

    HWND hwnd = CreateWindowExW(0, kFrameClass, L"LegacyControlZoo - WKAppBot Test",
        WS_OVERLAPPEDWINDOW | WS_CLIPCHILDREN,
        CW_USEDEFAULT, CW_USEDEFAULT, 980, 650,
        nullptr, nullptr, hInstance, nullptr);

    if (!hwnd) {
        DeleteObject(g_uiFont);
        return 1;
    }

    ShowWindow(hwnd, nCmdShow);
    UpdateWindow(hwnd);

    MSG msg;
    while (GetMessageW(&msg, nullptr, 0, 0)) {
        if (!TranslateMDISysAccel(g_mdiClient, &msg)) {
            TranslateMessage(&msg);
            DispatchMessageW(&msg);
        }
    }

    DeleteObject(g_uiFont);
    return static_cast<int>(msg.wParam);
}
