#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Hahn.ApplicatonProcess.December2020.Web/Hahn.ApplicatonProcess.December2020.Web.csproj", "Hahn.ApplicatonProcess.December2020.Web/"]
RUN dotnet restore "Hahn.ApplicatonProcess.December2020.Web/Hahn.ApplicatonProcess.December2020.Web.csproj"
COPY . .
WORKDIR "/src/Hahn.ApplicatonProcess.December2020.Web"
RUN dotnet build "Hahn.ApplicatonProcess.December2020.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hahn.ApplicatonProcess.December2020.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hahn.ApplicatonProcess.December2020.Web.dll"]