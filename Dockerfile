# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Stage 2: Copy source code
COPY ./Contracting.sln ./
COPY ./Contracting.Domain/ ./Contracting.Domain/
COPY ./Contracting.Application/ ./Contracting.Application/
COPY ./Contracting.Infrastructure/ ./Contracting.Infrastructure/
COPY ./Contracting.WebApi/ ./Contracting.WebApi/

# Stage 3: Restore dependencies
RUN dotnet restore ./Contracting.WebApi/Contracting.WebApi.csproj

# Stage 4: Build and publish the application
RUN dotnet publish ./Contracting.WebApi/Contracting.WebApi.csproj -c Release -o /out

# Stage 5: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Stage 6: Copy compiled files
COPY --from=build /out ./

# Stage 7: Expose the API port
EXPOSE 8080

# Stage 8: Startup command
ENTRYPOINT ["dotnet", "Contracting.WebApi.dll"]
