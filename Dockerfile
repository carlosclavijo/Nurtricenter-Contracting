# Etapa 1: Construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar la solución y los proyectos en el orden correcto
COPY ./Contracting.sln ./
COPY ./Contracting.Domain/ ./Contracting.Domain/
COPY ./Contracting.Application/ ./Contracting.Application/
COPY ./Contracting.Infrastructure/ ./Contracting.Infrastructure/
COPY ./Contracting.WebApi/ ./Contracting.WebApi/

# Restaurar dependencias
RUN dotnet restore ./Contracting.WebApi/Contracting.WebApi.csproj

# Compilar y publicar en modo Release
RUN dotnet publish ./Contracting.WebApi/Contracting.WebApi.csproj -c Release -o /out

# Etapa 2: Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar los archivos compilados
COPY --from=build /out ./

# Exponer el puerto de la API
EXPOSE 8080

# Comando de inicio
ENTRYPOINT ["dotnet", "Contracting.WebApi.dll"]
