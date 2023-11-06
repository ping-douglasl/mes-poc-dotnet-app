FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:7.0 as base

WORKDIR /app

COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=base /app/out ./
ENTRYPOINT ["dotnet", "api.dll"]