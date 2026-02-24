using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int KroTrialSpecialCommand(string command, string[] args)
    {
        var m = Regex.Match(command ?? string.Empty, @"^kro-trial-(\d{8})$", RegexOptions.IgnoreCase);
        if (!m.Success)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[KRO-TRIAL] Invalid command format.");
            Console.ResetColor();
            Console.WriteLine("Usage: wkappbot kro-trial-YYYYMMDD");
            Console.WriteLine("Example: wkappbot kro-trial-20260225");
            return 1;
        }

        var yyyymmdd = m.Groups[1].Value;
        var baseDir = Directory.GetCurrentDirectory();
        var probeDir = FindInputProbeDir(baseDir) ?? Path.Combine(baseDir, "tools", "InputProbe");
        var bakPath = Path.Combine(probeDir, $"Program.success.{yyyymmdd}.cs.bak");
        var probeProj = Path.Combine(probeDir, "InputProbe.csproj");

        if (!File.Exists(bakPath))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[KRO-TRIAL] 역사 코드 없음: {bakPath}");
            Console.ResetColor();
            Console.WriteLine("Usage: wkappbot kro-trial-YYYYMMDD");
            Console.WriteLine("Example: wkappbot kro-trial-20260225");
            return 1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[KRO-TRIAL] 역사 코드 발견: {bakPath}");
        Console.ResetColor();

        if (!File.Exists(probeProj))
        {
            Console.WriteLine("[KRO-TRIAL] InputProbe 프로젝트를 찾지 못했습니다.");
            Console.WriteLine($"[KRO-TRIAL] 수동 실행: W:\\SDK\\dotnet\\dotnet.exe run --project \"{probeProj}\" -- \"메모장\" \"KRO-TRIAL-{yyyymmdd}\"");
            return 0;
        }

        var dotnetExe = @"W:\SDK\dotnet\dotnet.exe";
        if (!File.Exists(dotnetExe))
            dotnetExe = "dotnet";

        var runArgs = $"run --project \"{probeProj}\" -- \"메모장\" \"KRO-TRIAL-{yyyymmdd}\"";
        Console.WriteLine($"[KRO-TRIAL] InputProbe 실행: {dotnetExe} {runArgs}");

        try
        {
            using var proc = Process.Start(new ProcessStartInfo
            {
                FileName = dotnetExe,
                Arguments = runArgs,
                UseShellExecute = false,
                WorkingDirectory = baseDir
            });

            if (proc is null)
            {
                Console.WriteLine("[KRO-TRIAL] InputProbe 실행 시작 실패. 위 명령으로 수동 실행하세요.");
                return 0;
            }

            proc.WaitForExit();
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[KRO-TRIAL] InputProbe 즉시 실행 실패: {ex.Message}");
            Console.WriteLine($"[KRO-TRIAL] 수동 실행: {dotnetExe} {runArgs}");
            return 0;
        }
    }

    static string? FindInputProbeDir(string startDir)
    {
        try
        {
            var di = new DirectoryInfo(startDir);
            while (di is not null)
            {
                var candidate = Path.Combine(di.FullName, "tools", "InputProbe");
                if (Directory.Exists(candidate))
                    return candidate;
                di = di.Parent;
            }
        }
        catch { }

        return null;
    }
}
