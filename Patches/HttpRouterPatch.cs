using Microsoft.Extensions.Primitives;
using SPTarkov.Reflection.Patching;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Routers;
using System.Reflection;

namespace SptBackendUrlRewriter.Patches;

/// <summary>
/// Patch to fix Backend URL issues
/// 
/// In setups where the backendIp or NAT port mapping differs from the SPT server's backend port,
/// SPT may construct an incorrect backend URL (e.g., using 0.0.0.0 instead of the correct address).
/// 
/// This patch intercepts HTTP responses and replaces the original backend URL 
/// with the Host from the request headers, ensuring clients use the correct address.
/// </summary>
public class HttpRouterPatch : AbstractPatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(HttpRouter).GetMethod(nameof(HttpRouter.GetResponse))!;
    }

    [PatchPostfix]
    public static async ValueTask<string?> Postfix(ValueTask<string?> __result, HttpRequest req)
    {
        HttpServerHelper httpServerHelper = ServiceLocator.ServiceProvider.GetService<HttpServerHelper>() 
            ?? throw new NullReferenceException("HttpServerHelper is null!");

        string? response = await __result;

        // If the request has a Host header, use it to replace the original host in the response
        if (!StringValues.IsNullOrEmpty(req.Headers.Host))
        {
            string originalHost = httpServerHelper.BuildUrl();
            string requestHost = req.Headers.Host.ToString();

            // Replace all occurrences of the original host with the request host
            response = response?.Replace(originalHost, requestHost);
        }

        return response;
    }
}

