# SPT Backend URL Rewriter Mod

A lightweight SPT Server mod that rewrites backend URLs to fix incorrect backend URL issues in NAT environments.

## Problem Description

SPT Server may return incorrect backend URLs in the following scenarios:

- Using NAT port mapping
- `backendIp` configured as `0.0.0.0` or other listening addresses
- `backendPort` differs from the actual access port

This causes clients to fail connecting to the server properly.

## Solution

This mod intercepts HTTP responses and replaces the backend URL in responses with the `Host` from request headers, ensuring clients always use the correct address.

## How It Works

1. Intercepts the `HttpRouter.GetResponse()` method
2. Extracts the `Host` field from request headers
3. Replaces all instances of the original backend URL in the response with the request Host
4. Returns the modified response

## Installation

1. Build this project
2. Copy the generated `SptBackendUrlRewriter.dll` to SPT Server's `user/mods` directory
3. Restart the server

## Features

- ✅ Automatic backend URL fixing
- ✅ No manual configuration required
- ✅ NAT port mapping support
- ✅ Lightweight with no additional dependencies
- ✅ Does not interfere with other mods

## Log Output

After successful loading, you will see in the server logs:

```
[SPT Backend URL Rewriter] Loading patch...
[SPT Backend URL Rewriter] Patch loaded successfully!
[SPT Backend URL Rewriter] Server will now use the Host header from requests as the backend URL
```

## Technical Details

- **Target Framework**: .NET 9.0
- **Dependencies**: SPT Server 4.0.0-pre (SPT ~4.0.0)
- **Load Timing**: PreSptModLoader (before SPT mod loader)
- **Mod GUID**: `com.spt.backend-url-rewriter`
- **Version**: 1.0.0

## Use Cases

- Docker containerized deployments
- Reverse proxies (Nginx, Caddy, etc.)
- Port forwarding
- NAT network environments
- Cloud server deployments

## Source Code

This mod is extracted from the `HttpRouterOverride` functionality of the [Fika Server](https://github.com/project-fika/Fika-Server-CSharp) project.

## License

Please refer to the original project's license.

