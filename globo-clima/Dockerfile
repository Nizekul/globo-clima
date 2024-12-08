# Definindo os argumentos de build
ARG AWS_REGION
ARG AWS_ACCESS_KEY
ARG AWS_SECRET_KEY
ARG JWT_KEY
ARG JWT_ISSUER
ARG JWT_AUDIENCE
ARG TOKEN_WEATHER
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