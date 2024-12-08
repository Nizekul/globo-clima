# Definindo os argumentos de build
ARG AWS__Region
ARG AWS__AccessKey
ARG AWS__SecretKey
ARG Jwt__Key
ARG Jwt__Issuer
ARG Jwt__Audience
ARG TokenWeather

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "globo-clima.dll"]