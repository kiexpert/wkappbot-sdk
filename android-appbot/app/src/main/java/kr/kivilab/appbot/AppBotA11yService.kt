package kr.kivilab.appbot

import android.accessibilityservice.AccessibilityService
import android.accessibilityservice.AccessibilityService.GLOBAL_ACTION_BACK
import android.accessibilityservice.AccessibilityService.GLOBAL_ACTION_HOME
import android.accessibilityservice.AccessibilityService.GLOBAL_ACTION_LOCK_SCREEN
import android.accessibilityservice.AccessibilityService.GLOBAL_ACTION_NOTIFICATIONS
import android.accessibilityservice.AccessibilityService.GLOBAL_ACTION_POWER_DIALOG
import android.accessibilityservice.AccessibilityService.GLOBAL_ACTION_QUICK_SETTINGS
import android.accessibilityservice.AccessibilityService.GLOBAL_ACTION_RECENTS
import android.accessibilityservice.GestureDescription
import android.os.Bundle
import android.view.accessibility.AccessibilityEvent
import android.view.accessibility.AccessibilityNodeInfo
import org.json.JSONArray
import org.json.JSONObject

/**
 * Exposes the foreground window as a JSON tree and performs actions on it.
 * Every action is focusless: it works on AccessibilityNodeInfo, never on coordinates first.
 */
class AppBotA11yService : AccessibilityService() {

    companion object {
        @Volatile
        var instance: AppBotA11yService? = null
            private set
    }

    override fun onServiceConnected() {
        super.onServiceConnected()
        instance = this
    }

    override fun onAccessibilityEvent(event: AccessibilityEvent?) {
        // Tree is read on demand by the MCP server; no event caching here.
    }

    override fun onInterrupt() {}

    override fun onUnbind(intent: android.content.Intent?): Boolean {
        instance = null
        return super.onUnbind(intent)
    }

    /** Dump the active window as nested JSON, depth-limited. */
    fun dumpTree(maxDepth: Int = 12): JSONObject {
        val root = rootInActiveWindow ?: return JSONObject().put("error", "no active window")
        return nodeToJson(root, 0, maxDepth)
    }

    private fun nodeToJson(node: AccessibilityNodeInfo, depth: Int, maxDepth: Int): JSONObject {
        val o = JSONObject()
        o.put("class", node.className?.toString() ?: "")
        o.put("text", node.text?.toString() ?: "")
        o.put("desc", node.contentDescription?.toString() ?: "")
        o.put("id", node.viewIdResourceName ?: "")
        o.put("clickable", node.isClickable)
        o.put("editable", node.isEditable)
        o.put("scrollable", node.isScrollable)
        val r = android.graphics.Rect()
        node.getBoundsInScreen(r)
        o.put("bounds", JSONArray(listOf(r.left, r.top, r.right, r.bottom)))
        if (depth < maxDepth) {
            val kids = JSONArray()
            for (i in 0 until node.childCount) {
                node.getChild(i)?.let { kids.put(nodeToJson(it, depth + 1, maxDepth)) }
            }
            o.put("children", kids)
        }
        return o
    }

    /** Find nodes whose text, description or view id contains the query. */
    fun findByText(query: String, limit: Int = 20): JSONArray {
        val out = JSONArray()
        val root = rootInActiveWindow ?: return out
        val hits = root.findAccessibilityNodeInfosByText(query) ?: emptyList()
        for (n in hits.take(limit)) {
            out.put(JSONObject().put("text", n.text?.toString() ?: "").put("id", n.viewIdResourceName ?: "").put("clickable", n.isClickable))
        }
        return out
    }

    /** Click the first clickable node (or its clickable ancestor) matching the text. */
    fun clickByText(query: String): Boolean {
        val root = rootInActiveWindow ?: return false
        val hit = root.findAccessibilityNodeInfosByText(query)?.firstOrNull() ?: return false
        var n: AccessibilityNodeInfo? = hit
        while (n != null && !n.isClickable) n = n.parent
        return n?.performAction(AccessibilityNodeInfo.ACTION_CLICK) ?: false
    }

    /** Set text on the first editable node, optionally scoped by a text match. */
    fun setText(text: String, targetQuery: String?): Boolean {
        val root = rootInActiveWindow ?: return false
        val node = if (targetQuery.isNullOrBlank()) findFirstEditable(root)
        else root.findAccessibilityNodeInfosByText(targetQuery)?.firstOrNull { it.isEditable }
        node ?: return false
        val args = Bundle().apply {
            putCharSequence(AccessibilityNodeInfo.ACTION_ARGUMENT_SET_TEXT_CHARSEQUENCE, text)
        }
        return node.performAction(AccessibilityNodeInfo.ACTION_SET_TEXT, args)
    }

    private fun findFirstEditable(n: AccessibilityNodeInfo): AccessibilityNodeInfo? {
        if (n.isEditable) return n
        for (i in 0 until n.childCount) {
            n.getChild(i)?.let { c -> findFirstEditable(c)?.let { return it } }
        }
        return null
    }

    /** Scroll the first scrollable node forward or backward. */
    fun scroll(forward: Boolean): Boolean {
        val root = rootInActiveWindow ?: return false
        val action = if (forward) AccessibilityNodeInfo.ACTION_SCROLL_FORWARD else AccessibilityNodeInfo.ACTION_SCROLL_BACKWARD
        val node = findFirstScrollable(root) ?: return false
        return node.performAction(action)
    }

    private fun findFirstScrollable(n: AccessibilityNodeInfo): AccessibilityNodeInfo? {
        if (n.isScrollable) return n
        for (i in 0 until n.childCount) {
            n.getChild(i)?.let { c -> findFirstScrollable(c)?.let { return it } }
        }
        return null
    }

    /** System-level actions the phone UI offers (see McpServer.GLOBAL_ACTIONS for the exposed names). */
    fun globalAction(name: String): Boolean = when (name) {
        "back" -> performGlobalAction(GLOBAL_ACTION_BACK)
        "home" -> performGlobalAction(GLOBAL_ACTION_HOME)
        "recents" -> performGlobalAction(GLOBAL_ACTION_RECENTS)
        "notifications" -> performGlobalAction(GLOBAL_ACTION_NOTIFICATIONS)
        "quick_settings" -> performGlobalAction(GLOBAL_ACTION_QUICK_SETTINGS)
        "power_dialog" -> performGlobalAction(GLOBAL_ACTION_POWER_DIALOG)
        "lock_screen" -> performGlobalAction(GLOBAL_ACTION_LOCK_SCREEN)
        else -> false
    }
}
