// gen-license.csx -- WKAppBot offline license generator
//
// Run with dotnet-script (https://github.com/dotnet-script/dotnet-script):
//   dotnet tool install -g dotnet-script
//   dotnet script tools/gen-license.csx -- \
//       --user-id  6f8b3c-... \
//       --email    alice@example.com \
//       --tier     Pro \
//       --expires  2027-01-01 \
//       --features AndroidAdb,VisionApi \
//       --priv     <PKCS8-base64-private-key> \
//       --out      ./alice.license.json
//
// Or pipe the private key in:
//   echo $PRIV | dotnet script tools/gen-license.csx -- --user-id ... --priv -
//
// Notes:
//   * Curve must be P-256. The key is SAVED nowhere by this script -- you
//     supply it on every invocation. Keep it in a vault.
//   * The signature covers a canonical JSON over the other fields:
//     keys sorted ASCII, no whitespace, ISO-8601 UTC for expiresAt.
//   * The CLI's embedded public key MUST match the private key you pass here.
//
#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

string? Arg(string name)
{
    var argv = Args.ToArray();
    var i = Array.IndexOf(argv, "--" + name);
    if (i < 0 || i + 1 >= argv.Length) return null;
    return argv[i + 1];
}

string Required(string name) =>
    Arg(name) ?? throw new ArgumentException($"--{name} is required");

string userId   = Required("user-id");
string email    = Required("email");
string tier     = Required("tier");                 // Free | Standard | Pro
string expires  = Required("expires");              // YYYY-MM-DD or ISO-8601
string features = Arg("features") ?? "";            // CSV: AskAI,Schedule,...
string priv     = Required("priv");                 // PKCS#8 base64, or "-" for stdin
string outPath  = Arg("out") ?? "./license.json";

string[] validTiers     = { "Free", "Standard", "Pro" };
string[] validFeatures  = { "AskAI", "Schedule", "AndroidAdb", "VisionApi", "UnlimitedScenarios" };

if (!validTiers.Contains(tier, StringComparer.OrdinalIgnoreCase))
    throw new ArgumentException($"--tier must be one of: {string.Join(", ", validTiers)}");
tier = validTiers.First(t => t.Equals(tier, StringComparison.OrdinalIgnoreCase));

DateTime expiry;
if (DateTime.TryParse(expires, System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal,
        out var parsed))
    expiry = DateTime.SpecifyKind(parsed, DateTimeKind.Utc);
else
    throw new ArgumentException($"--expires '{expires}' is not a valid date");

var featList = features
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    .Select(f =>
    {
        if (!validFeatures.Contains(f, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException($"unknown feature '{f}' (valid: {string.Join(',', validFeatures)})");
        return validFeatures.First(v => v.Equals(f, StringComparison.OrdinalIgnoreCase));
    })
    .Distinct(StringComparer.Ordinal)
    .OrderBy(s => s, StringComparer.Ordinal)
    .ToArray();

if (priv == "-") priv = Console.In.ReadToEnd().Trim();

string JsonString(string s)
{
    var sb = new StringBuilder(s.Length + 2);
    sb.Append('"');
    foreach (var c in s)
    {
        switch (c)
        {
            case '"':  sb.Append("\\\""); break;
            case '\\': sb.Append("\\\\"); break;
            case '\b': sb.Append("\\b"); break;
            case '\f': sb.Append("\\f"); break;
            case '\n': sb.Append("\\n"); break;
            case '\r': sb.Append("\\r"); break;
            case '\t': sb.Append("\\t"); break;
            default:
                if (c < 0x20) sb.Append($"\\u{(int)c:x4}");
                else sb.Append(c);
                break;
        }
    }
    sb.Append('"');
    return sb.ToString();
}

// Canonical payload -- MUST match LicenseManager.CanonicalPayload exactly.
var canon = new StringBuilder();
canon.Append('{');
canon.Append(JsonString("expiresAt")); canon.Append(':'); canon.Append(JsonString(expiry.ToString("yyyy-MM-ddTHH:mm:ssZ"))); canon.Append(',');
canon.Append(JsonString("features"));  canon.Append(":[");
for (int i = 0; i < featList.Length; i++) { if (i > 0) canon.Append(','); canon.Append(JsonString(featList[i])); }
canon.Append("],");
canon.Append(JsonString("tier"));      canon.Append(':'); canon.Append(JsonString(tier)); canon.Append(',');
canon.Append(JsonString("userEmail")); canon.Append(':'); canon.Append(JsonString(email)); canon.Append(',');
canon.Append(JsonString("userId"));    canon.Append(':'); canon.Append(JsonString(userId));
canon.Append('}');

var payloadBytes = Encoding.UTF8.GetBytes(canon.ToString());

using var ec = ECDsa.Create();
ec.ImportPkcs8PrivateKey(Convert.FromBase64String(priv), out _);
var sigBytes = ec.SignData(payloadBytes, HashAlgorithmName.SHA256);

string Base64Url(byte[] b)
    => Convert.ToBase64String(b).TrimEnd('=').Replace('+', '-').Replace('/', '_');
var sig = Base64Url(sigBytes);

// Output license.json (pretty-printed for humans). Verification re-canonicalizes
// from the parsed fields, so whitespace here doesn't affect signature validity.
var pretty = new StringBuilder();
pretty.AppendLine("{");
pretty.AppendLine($"  \"userId\":    {JsonString(userId)},");
pretty.AppendLine($"  \"userEmail\": {JsonString(email)},");
pretty.AppendLine($"  \"tier\":      {JsonString(tier)},");
pretty.AppendLine($"  \"expiresAt\": {JsonString(expiry.ToString("yyyy-MM-ddTHH:mm:ssZ"))},");
pretty.Append    ($"  \"features\":  [");
for (int i = 0; i < featList.Length; i++) { if (i > 0) pretty.Append(", "); pretty.Append(JsonString(featList[i])); }
pretty.AppendLine("],");
pretty.AppendLine($"  \"signature\": {JsonString(sig)}");
pretty.AppendLine("}");

File.WriteAllText(outPath, pretty.ToString(), new UTF8Encoding(false));
Console.WriteLine($"[gen-license] wrote {outPath}");
Console.WriteLine($"[gen-license] tier={tier} email={email} expires={expiry:yyyy-MM-dd}Z features=[{string.Join(',', featList)}]");
