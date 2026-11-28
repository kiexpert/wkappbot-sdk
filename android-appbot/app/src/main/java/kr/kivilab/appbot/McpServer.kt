package kr.kivilab.appbot

import android.app.Notification
import android.app.NotificationChannel
import android.app.NotificationManager
import android.app.Service
import android.content.Intent
import android.os.Build
import android.os.IBinder
import org.json.JSONArray
import org.json.JSONObject
import java.io.BufferedReader
import java.io.InputStreamReader
import java.io.OutputStreamWriter
import java.io.BufferedWriter
import java.net.InetAddress
import java.net.ServerSocket
import java.net.Socket
import kotlin.concurrent.thread

/**
 * Foreground service hosting a minimal MCP endpoint (JSON-RPC 2.0 over HTTP POST /mcp).
 * Bound to 127.0.0.1 only. Every request needs "Authorization: Bearer <token>".
 * No external MCP library: the protocol surface is initialize, tools/list, tools/call.
 */
class McpServer : Service() {

    companion object {
        const val PORT = 8765
        const val CHANNEL_ID = "appbot_mcp"
        const val NOTIF_ID = 8765
        const val ACTION_STOP = "kr.kivilab.appbot.STOP"
        const val PREFS = "appbot"
        const val KEY_TOKEN = "token"

        val GLOBAL_ACTIONS = listOf("back", "home", "recents", "notifications", "quick_settings", "power_dialog", "lock_screen")

        val TOOLS = listOf(
            "a11y_tree", "a11y_find", "a11y_click", "a11y_type", "a11y_scroll", "global_action"
        )
    }

    private var server: ServerSocket? = null
    @Volatile private var running = false
    private lateinit var token: String

    override fun onCreate() {
        super.onCreate()
        token = MainActivity.getOrCreateToken(this)
    }

    override fun onStartCommand(intent: Intent?, flags: Int, startId: Int): Int {
        if (intent?.action == ACTION_STOP) {
            stopSelf()
            return START_NOT_STICKY
        }
        startForegroundCompat()
        if (!running) startServer()
        return START_STICKY
    }

    override fun onDestroy() {
        running = false
        try { server?.close() } catch (_: Exception) {}
        super.onDestroy()
    }

    override fun onBind(intent: Intent?): IBinder? = null

    private fun startForegroundCompat() {
        val nm = getSystemService(NotificationManager::class.java)
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            nm.createNotificationChannel(NotificationChannel(CHANNEL_ID, "AppBot MCP", NotificationManager.IMPORTANCE_LOW))
        }
        val stopIntent = Intent(this, McpServer::class.java).setAction(ACTION_STOP)
        val stopPi = android.app.PendingIntent.getService(
            this, 0, stopIntent, android.app.PendingIntent.FLAG_IMMUTABLE
        )
        val n: Notification = Notification.Builder(this, CHANNEL_ID)
            .setContentTitle("AppBot MCP on 127.0.0.1:$PORT")
            .setContentText("Tap Stop to disconnect all models")
            .setSmallIcon(android.R.drawable.ic_lock_idle_lock)
            .addAction(Notification.Action.Builder(null, "Stop", stopPi).build())
            .setOngoing(true)
            .build()
        startForeground(NOTIF_ID, n, android.content.pm.ServiceInfo.FOREGROUND_SERVICE_TYPE_SPECIAL_USE)
    }

    private fun startServer() {
        running = true
        thread(name = "mcp-accept") {
            try {
                // Loopback only: the bind address itself refuses any non-loopback peer.
                val s = ServerSocket(PORT, 16, InetAddress.getByName("127.0.0.1"))
                server = s
                while (running) {
                    val client = s.accept()
                    thread(name = "mcp-client") { handle(client) }
                }
            } catch (_: Exception) {
                running = false
            }
        }
    }

    private fun handle(sock: Socket) {
        sock.use { c ->
            // Defence in depth: refuse any peer that is not loopback.
            if (!c.inetAddress.isLoopbackAddress) return
            val reader = BufferedReader(InputStreamReader(c.getInputStream(), Charsets.UTF_8))
            val writer = BufferedWriter(OutputStreamWriter(c.getOutputStream(), Charsets.UTF_8))
            val requestLine = reader.readLine() ?: return
            val headers = HashMap<String, String>()
            while (true) {
                val line = reader.readLine() ?: break
                if (line.isEmpty()) break
                val idx = line.indexOf(':')
                if (idx > 0) headers[line.substring(0, idx).trim().lowercase()] = line.substring(idx + 1).trim()
            }
            val length = headers["content-length"]?.toIntOrNull() ?: 0
            val body = CharArray(length)
            var read = 0
            while (read < length) {
                val n = reader.read(body, read, length - read)
                if (n < 0) break
                read += n
            }
            if (!requestLine.startsWith("POST /mcp ")) {
                respond(writer, 404, "")
                return
            }
            if (headers["authorization"] != "Bearer $token") {
                respond(writer, 401, "")
                return
            }
            val reply = dispatch(String(body, 0, read))
            if (reply == null) respond(writer, 202, "") else respond(writer, 200, reply.toString(), "application/json")
        }
    }

    private fun respond(w: BufferedWriter, code: Int, body: String, type: String = "text/plain") {
        val reason = when (code) { 200 -> "OK"; 202 -> "Accepted"; 401 -> "Unauthorized"; else -> "Not Found" }
        val bytes = body.toByteArray(Charsets.UTF_8)
        w.write("HTTP/1.1 $code $reason\r\nContent-Type: $type\r\nContent-Length: ${bytes.size}\r\nConnection: close\r\n\r\n")
        w.write(body)
        w.flush()
    }

    /** JSON-RPC dispatch. Returns null for notifications (no id). */
    internal fun dispatch(raw: String): JSONObject? {
        val req = try { JSONObject(raw) } catch (_: Exception) {
            return error(JSONObject.NULL, -32700, "parse error")
        }
        val id = req.opt("id") ?: return null
        val method = req.optString("method")
        return when (method) {
            "initialize" -> result(id, JSONObject()
                .put("protocolVersion", "2024-11-05")
                .put("serverInfo", JSONObject().put("name", "appbot-android").put("version", "0.1.0"))
                .put("capabilities", JSONObject().put("tools", JSONObject())))
            "tools/list" -> result(id, JSONObject().put("tools", toolList()))
            "tools/call" -> {
                val params = req.optJSONObject("params") ?: JSONObject()
                val name = params.optString("name")
                val args = params.optJSONObject("arguments") ?: JSONObject()
                result(id, callTool(name, args))
            }
            else -> error(id, -32601, "method not found: $method")
        }
    }

    private fun toolList(): JSONArray {
        fun tool(name: String, desc: String, props: JSONObject = JSONObject()) = JSONObject()
            .put("name", name).put("description", desc)
            .put("inputSchema", JSONObject().put("type", "object").put("properties", props))
        return JSONArray()
            .put(tool("a11y_tree", "Dump the active window as a JSON tree"))
            .put(tool("a11y_find", "Find nodes by text, desc or id", JSONObject()
                .put("query", JSONObject().put("type", "string"))))
            .put(tool("a11y_click", "Click the first clickable node matching text", JSONObject()
                .put("query", JSONObject().put("type", "string"))))
            .put(tool("a11y_type", "Set text on an editable node", JSONObject()
                .put("text", JSONObject().put("type", "string"))
                .put("target", JSONObject().put("type", "string"))))
            .put(tool("a11y_scroll", "Scroll the first scrollable node", JSONObject()
                .put("forward", JSONObject().put("type", "boolean"))))
            .put(tool("global_action", "System action: back, home, recents, notifications, quick_settings, power_dialog, lock_screen", JSONObject()
                .put("name", JSONObject().put("type", "string").put("enum", JSONArray(GLOBAL_ACTIONS)))))
    }

    private fun callTool(name: String, a: JSONObject): JSONObject {
        val svc = AppBotA11yService.instance
            ?: return textResult("Accessibility service is not enabled. Enable AppBot in Settings > Accessibility.", true)
        val ok: Any = when (name) {
            "a11y_tree" -> svc.dumpTree()
            "a11y_find" -> svc.findByText(a.optString("query"))
            "a11y_click" -> svc.clickByText(a.optString("query"))
            "a11y_type" -> svc.setText(a.optString("text"), a.optString("target").ifEmpty { null })
            "a11y_scroll" -> svc.scroll(a.optBoolean("forward", true))
            "global_action" -> svc.globalAction(a.optString("name"))
            else -> return textResult("unknown tool: $name", true)
        }
        return textResult(ok.toString(), false)
    }

    private fun textResult(text: String, isError: Boolean) = JSONObject()
        .put("content", JSONArray().put(JSONObject().put("type", "text").put("text", text)))
        .put("isError", isError)

    private fun result(id: Any, r: JSONObject) = JSONObject().put("jsonrpc", "2.0").put("id", id).put("result", r)

    private fun error(id: Any, code: Int, msg: String) = JSONObject().put("jsonrpc", "2.0").put("id", id)
        .put("error", JSONObject().put("code", code).put("message", msg))
}
