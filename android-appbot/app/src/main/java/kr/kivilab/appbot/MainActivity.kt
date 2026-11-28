package kr.kivilab.appbot

import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.provider.Settings
import android.widget.Button
import android.widget.LinearLayout
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import java.security.SecureRandom

class MainActivity : AppCompatActivity() {

    companion object {
        /** Per-install bearer token, generated once and shown to the owner. */
        fun getOrCreateToken(ctx: Context): String {
            val prefs = ctx.getSharedPreferences(McpServer.PREFS, Context.MODE_PRIVATE)
            prefs.getString(McpServer.KEY_TOKEN, null)?.let { return it }
            val bytes = ByteArray(24).also { SecureRandom().nextBytes(it) }
            val token = bytes.joinToString("") { "%02x".format(it) }
            prefs.edit().putString(McpServer.KEY_TOKEN, token).apply()
            return token
        }
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        val token = getOrCreateToken(this)

        val info = TextView(this).apply {
            text = "MCP endpoint: http://127.0.0.1:${McpServer.PORT}/mcp\n" +
                "Authorization: Bearer $token\n\n" +
                "Tools: ${McpServer.TOOLS.joinToString(", ")}"
            setPadding(48, 48, 48, 48)
        }
        val openA11y = Button(this).apply {
            text = "Open accessibility settings"
            setOnClickListener { startActivity(Intent(Settings.ACTION_ACCESSIBILITY_SETTINGS)) }
        }
        val start = Button(this).apply {
            text = "Start MCP server"
            setOnClickListener { startForegroundService(Intent(this@MainActivity, McpServer::class.java)) }
        }
        val stop = Button(this).apply {
            text = "Stop MCP server"
            setOnClickListener {
                startService(Intent(this@MainActivity, McpServer::class.java).setAction(McpServer.ACTION_STOP))
            }
        }
        setContentView(LinearLayout(this).apply {
            orientation = LinearLayout.VERTICAL
            addView(info)
            addView(openA11y)
            addView(start)
            addView(stop)
        })
    }
}
