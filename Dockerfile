FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:7.0 as base

FROM base as build
WORKDIR /app
COPY ./api.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet build -c Release -o build

FROM base as migrate
WORKDIR /app
COPY --from=build /app ./

FROM base as publisher
WORKDIR /app
COPY --from=build /app ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=publisher /app/out ./
ENTRYPOINT ["dotnet", "api.dll"]