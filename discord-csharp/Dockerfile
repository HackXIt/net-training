FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["discord-training-bot/discord-training-bot.csproj", "discord-training-bot/"]
RUN dotnet restore "discord-training-bot/discord-training-bot.csproj"
COPY . .
WORKDIR "/src/discord-training-bot"
RUN dotnet build "discord-training-bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "discord-training-bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "discord-training-bot.dll"]
