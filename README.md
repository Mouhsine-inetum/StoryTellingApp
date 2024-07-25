# ASP.NET Razor Application with Azure B2C Authentication and API Integration

## Overview

This ASP.NET Razor application uses Azure Active Directory B2C (Azure B2C) for authentication and authorization. The application also integrates with an external API, which is secured using the same Azure B2C instance.

## Features

- User authentication via Azure B2C.
- Authorization based on user roles.
- Integration with a secured external API using `HttpClientFactory`.
- Secure handling of tokens and session management.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An Azure B2C tenant with a registered application for both the client (Razor app) and the API.

