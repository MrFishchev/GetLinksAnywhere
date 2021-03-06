#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GetLinksAnywhere/GetLinksAnywhere.csproj", "GetLinksAnywhere/"]
COPY ["GetLinksAnywhere.Common/GetLinksAnywhere.Common.csproj", "GetLinksAnywhere.Common/"]
COPY ["GetLinksAnywhere.Model/GetLinksAnywhere.Model.csproj", "GetLinksAnywhere.Model/"]
RUN dotnet restore "GetLinksAnywhere/GetLinksAnywhere.csproj"
COPY . .
WORKDIR "/src/GetLinksAnywhere"
RUN dotnet build "GetLinksAnywhere.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GetLinksAnywhere.csproj" -c Release -o /app/publish

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://+:80
EXPOSE 80

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GetLinksAnywhere.dll"]