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

# Definir variáveis de ambiente no container final
ENV AWS_REGION=${AWS_REGION}
ENV AWS_ACCESS_KEY=${AWS__AccessKey}
ENV AWS_SECRET_KEY=${AWS__SecretKey}
ENV JWT_KEY=${Jwt__Key}
ENV JWT_ISSUER=${Jwt__Issuer}
ENV JWT_AUDIENCE=${Jwt__Audience}
ENV TOKEN_WEATHER=${TokenWeather}