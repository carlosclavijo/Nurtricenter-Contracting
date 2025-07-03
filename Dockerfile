FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de la solución y restaura las dependencias

COPY ["Contracting.sln", "."]
COPY ["Contracting.Domain/Contracting.Domain.csproj", "Contracting.Domain/"]
COPY ["Contracting.Application/Contracting.Application.csproj", "Contracting.Application/"]
COPY ["Contracting.Infrastructure/Contracting.Infrastructure.csproj", "Contracting.Infrastructure/"]
COPY ["Contracting.WebApi/Contracting.WebApi.csproj", "Contracting.WebApi/"]
COPY ["Contracting.WorkerService/Contracting.WorkerService.csproj", "Contracting.WorkerService/"]

# Restaura los paquetes NuGet
RUN dotnet restore "./Contracting.WebApi/Contracting.WebApi.csproj"
RUN dotnet restore "./Contracting.WorkerService/Contracting.WorkerService.csproj"

# Copia todo el código fuente
COPY . .

# Compila la solución
WORKDIR "/src/Contracting.WebApi"
RUN dotnet build "Contracting.WebApi.csproj" -c Release -o /app/build/webapi

WORKDIR "/src/Contracting.WorkerService"
RUN dotnet build "Contracting.WorkerService.csproj" -c Release -o /app/build/workerservice


# Publica la aplicación
FROM build AS publish
WORKDIR "/src/Contracting.WebApi"
RUN dotnet publish "./Contracting.WebApi.csproj" -c Release -o /app/publish/webapi /p:UseAppHost=false

FROM build AS publish-workerservice
WORKDIR "/src/Contracting.WorkerService"
RUN dotnet publish "./Contracting.WorkerService.csproj" -c Release -o /app/publish/workerservice /p:UseAppHost=false

# Crea la imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish/webapi .
COPY --from=publish-workerservice /app/publish/workerservice .

# Crea el script de inicio
RUN echo '#!/bin/bash \n\
dotnet /app/Contracting.WebApi.dll & \n\
dotnet /app/Contracting.WorkerService.dll \n\
wait' > /app/entrypoint.sh && \
chmod +x /app/entrypoint.sh

ENTRYPOINT ["/bin/bash", "/app/entrypoint.sh"]