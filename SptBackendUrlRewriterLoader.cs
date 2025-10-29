using SptBackendUrlRewriter.Patches;
using SPTarkov.DI.Annotations;
using SPTarkov.Reflection.Patching;
using SPTarkov.Server.Core.DI;

namespace SptBackendUrlRewriter;

/// <summary>
/// SPT Backend URL Rewriter Mod Loader
/// 
/// This mod solves the issue where SPT Server returns incorrect backend URLs
/// in NAT environments or with custom backend configurations.
/// </summary>
[Injectable(InjectionType.Singleton, TypePriority = OnLoadOrder.PreSptModLoader)]
public class SptBackendUrlRewriterLoader : IOnLoad
{
    private bool _patchApplied;

    public Task OnLoad()
    {
        if (_patchApplied)
        {
            return Task.CompletedTask;
        }

        try
        {
            Console.WriteLine("[SPT Backend URL Rewriter] Loading patch...");
            
            HttpRouterPatch patch = new();
            patch.Enable();
            
            Console.WriteLine("[SPT Backend URL Rewriter] Patch loaded successfully!");
            Console.WriteLine("[SPT Backend URL Rewriter] Server will now use the Host header from requests as the backend URL");
            
            _patchApplied = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SPT Backend URL Rewriter] Failed to load patch: {ex.Message}");
            Console.WriteLine($"[SPT Backend URL Rewriter] Stack trace: {ex.StackTrace}");
            throw;
        }

        return Task.CompletedTask;
    }
}


