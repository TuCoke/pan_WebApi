#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["pan.web/pan.web.csproj", "pan.web/"]
COPY ["Pan.Common/Pan.Common.csproj", "Pan.Common/"]
COPY ["Pan.Domain/Pan.Domain.csproj", "Pan.Domain/"]
COPY ["Pan.Infrastructure/Pan.Infrastructure.csproj", "Pan.Infrastructure/"]
RUN dotnet restore "pan.web/pan.web.csproj"
COPY . .
WORKDIR "/src/pan.web"
RUN dotnet build "pan.web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "pan.web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pan.web.dll"]