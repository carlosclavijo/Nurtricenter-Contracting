#!/bin/bash

# Ejecutar la WebAPI en segundo plano
dotnet webapi/Contracting.WebApi.dll &

# Guardar el PID
WEBAPI_PID=$!

# Ejecutar el WorkerService también en segundo plano
dotnet worker/Contracting.WorkerService.dll &

# Guardar el PID
WORKER_PID=$!

# Esperar que cualquiera de los dos termine
wait -n $WEBAPI_PID $WORKER_PID