#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QBT_Invoice_Generator/QBT_Invoice_Generator.csproj", "QBT_Invoice_Generator/"]
RUN dotnet restore "./QBT_Invoice_Generator/./QBT_Invoice_Generator.csproj"
COPY . .
WORKDIR "/src/QBT_Invoice_Generator"
RUN dotnet build "./QBT_Invoice_Generator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QBT_Invoice_Generator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/QBT_Invoice_Generator/Fonts  .

ENTRYPOINT ["dotnet", "QBT_Invoice_Generator.dll"]