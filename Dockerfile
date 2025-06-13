# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY ./Contracting.sln ./
COPY ./Contracting.Domain/ ./Contracting.Domain/
COPY ./Contracting.Application/ ./Contracting.Application/
COPY ./Contracting.Infrastructure/ ./Contracting.Infrastructure/
COPY ./Contracting.WebApi/ ./Contracting.WebApi/
COPY ./Contracting.WorkerService/ ./Contracting.WorkerService/

# Restore
RUN dotnet restore ./Contracting.WebApi/Contracting.WebApi.csproj
RUN dotnet restore ./Contracting.WorkerService/Contracting.WorkerService.csproj

# Build & Publish both
RUN dotnet publish ./Contracting.WebApi/Contracting.WebApi.csproj -c Release -o /out/webapi
RUN dotnet publish ./Contracting.WorkerService/Contracting.WorkerService.csproj -c Release -o /out/worker

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published apps
COPY --from=build /out/webapi ./webapi
COPY --from=build /out/worker ./worker

# Create startup script
COPY start.sh .
RUN chmod +x start.sh

# Expose API port
EXPOSE 8080

# Startup
ENTRYPOINT ["./start.sh"]