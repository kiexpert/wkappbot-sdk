using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.CLI;

// -- SkillSource / SkillRecord --------------------------------------------─

internal enum SkillSource { Project, Hq }

internal sealed class SkillSourceRef
{
    [JsonPropertyName("file")]    public string  File    { get; set; } = "";
    [JsonPropertyName("line")]    public int?    Line    { get; set; }
    [JsonPropertyName("pattern")] public string? Pattern { get; set; }
    [JsonPropertyName("note")]    public string? Note    { get; set; }
}

internal sealed class SkillRequirement
{
    [JsonPropertyName("cmd")]    public string  Cmd    { get; set; } = "";
    [JsonPropertyName("expect")] public string  Expect { get; set; } = "";
    [JsonPropertyName("prompt")] public string? Prompt { get; set; }  // resolve note / user prompt context
}

internal sealed class SkillRecord
{
    [JsonPropertyName("id")]          public string Id      { get; set; } = "";
    [JsonPropertyName("app")]         public string App     { get; set; } = "";
    [JsonPropertyName("title")]       public string Title   { get; set; } = "";
    [JsonPropertyName("desc")]        public string Desc    { get; set; } = "";
    [JsonPropertyName("steps")]       public List<string> Steps { get; set; } = [];
    [JsonPropertyName("requirements")] public List<SkillRequirement>? Requirements { get; set; }
    [JsonPropertyName("primary_cmd")]  public string? PrimaryCmd { get; set; }
    [JsonPropertyName("tags")]        public List<string> Tags  { get; set; } = [];
    [JsonPropertyName("source_refs")]        public List<SkillSourceRef>? SourceRefs      { get; set; }
    [JsonPropertyName("regression_script")]  public string? RegressionScript              { get; set; }
    [JsonPropertyName("author")]             public string? Author                        { get; set; }
    [JsonPropertyName("created")]            public DateTime Created                      { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("updated")]            public DateTime? Updated                     { get; set; }
    [JsonPropertyName("version")]            public string? Version                       { get; set; }

    /// <summary>Most recent activity date -- updated if set, otherwise created.</summary>
    [JsonIgnore] public DateTime LastActivity => Updated ?? Created;

    [JsonIgnore] public SkillSource Source   { get; set; } = SkillSource.Project;
    /// <summary>Absolute path to the loaded .skill.json file (set by Load).</summary>
    [JsonIgnore] public string? FilePath     { get; set; }

    static readonly JsonSerializerOptions Opts = new() { WriteIndented = true };

    public void Save(string path) =>
        File.WriteAllText(path, JsonSerializer.Serialize(this, Opts));

    public static SkillRecord? Load(string path)
    {
        try
        {
            var s = JsonSerializer.Deserialize<SkillRecord>(File.ReadAllText(path));
            if (s != null) s.FilePath = path;
            return s;
        }
        catch { return null; }
    }
}
