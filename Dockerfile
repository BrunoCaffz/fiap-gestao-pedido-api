# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o csproj
COPY GestaoResiduosAPI/GestaoResiduosAPI.csproj GestaoResiduosAPI/

# Restaura dependências
RUN dotnet restore GestaoResiduosAPI/GestaoResiduosAPI.csproj

# Copia todo o código
COPY GestaoResiduosAPI/ GestaoResiduosAPI/

# Publica
RUN dotnet publish GestaoResiduosAPI/GestaoResiduosAPI.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GestaoResiduosAPI.dll"]
