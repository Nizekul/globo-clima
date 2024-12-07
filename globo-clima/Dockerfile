# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["globo-clima.csproj", "globo-clima/"]
RUN dotnet restore "./globo-clima/globo-clima.csproj"

WORKDIR "/src/globo-clima"
COPY . .
RUN dotnet build "globo-clima.csproj" --configuration Release --output /app/build

FROM build AS publish
RUN dotnet publish "globo-clima.csproj" \
            --configuration Release \ 
            --runtime linux-x64 \
            --self-contained false \ 
            --output /app/publish \
            -p:PublishReadyToRun=true  

FROM base AS final
WORKDIR /var/task

CMD ["AspNetOnLambda::AspNetOnLambda.LambdaEntryPoint::FunctionHandlerAsync"]
COPY --from=publish /app/publish .