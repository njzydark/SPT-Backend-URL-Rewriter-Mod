using SPTarkov.Server.Core.Models.Spt.Mod;

namespace SptBackendUrlRewriter;

public record SptBackendUrlRewriterMetadata : AbstractModMetadata
{
    public override string Name { get; init; } = "SPT Backend URL Rewriter";
    public override string Author { get; init; } = "SPT Community";
    public override List<string>? Contributors { get; init; } = [];
    public override List<string>? Incompatibilities { get; init; } = [];
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; } = [];
    public override string? Url { get; init; } = "https://github.com/njzydark/SPT-Backend-URL-Rewriter-Mod";
    public override bool? IsBundleMod { get; init; } = false;
    public override string License { get; init; } = "MIT";
    public override string ModGuid { get; init; } = "com.spt.backend-url-rewriter";
    public override SemanticVersioning.Version Version { get; init; } = new(1, 0, 0);
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");
}

