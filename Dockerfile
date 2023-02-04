FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#install prerequisites (curl)
RUN apt-get update && apt-get install -y curl


#Add timezone and locale to ENV for Greece athens
ENV TZ=Europe/Athens
ENV LANG el_GR.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src


#add nuget source
RUN dotnet nuget add source https://nuget.pkg.github.com/PlaySystems-Integrator/index.json -n github -u unavoidable0100 -p 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 --store-password-in-clear-text

COPY ["src/Play.Services.Api/Play.Services.Api.csproj", "src/Play.Services.Api/"]
#restore nuget packages
RUN dotnet restore "src/Play.Services.Api/Play.Services.Api.csproj"
COPY . .
WORKDIR "/src/src/Play.Services.Api"
RUN dotnet build "Play.Services.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Play.Services.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Play.Services.Api.dll"]


LABEL org.opencontainers.image.source = https://github.com/PlaySystems-Integrator/Play.Api