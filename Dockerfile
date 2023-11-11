FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:7.0 as base

FROM base as build
WORKDIR /app
COPY ./src/mes-poc-dotnet-app.csproj ./src/mes-poc-dotnet-app.csproj
RUN dotnet restore ./src/mes-poc-dotnet-app.csproj
COPY . ./
RUN dotnet build -c Release -o build ./src/mes-poc-dotnet-app.csproj

FROM base as migrate
WORKDIR /app
COPY --from=build /app ./

FROM base as publisher
WORKDIR /app
COPY --from=build /app ./
RUN dotnet publish ./src/mes-poc-dotnet-app.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=publisher /app/out ./
ENTRYPOINT ["dotnet", "api.dll"]