#!/usr/bin/env bash

set -eu -o pipefail

if [ -z "$1" ]; then
  echo "Please provide the sonar token 👀"
  exit 0
fi

if [ -z "$2" ]; then
  echo "Please provide the project version 👀"
  exit 0
fi

echo "### Reading variables..."
SONAR_TOKEN=$1
PROJECT_VERSION=$2

echo "### Beginning SonarScanner..."

dotnet sonarscanner begin \
  /k:"carlosclavijo_Nurtricenter-Contracting" \
  /o:"carlosclavijo" \
  /d:sonar.token="$SONAR_TOKEN" \
    /v:"$PROJECT_VERSION" \
    /d:sonar.host.url="https://sonarcloud.io" \
    /d:sonar.cs.opencover.reportsPaths="**/*/coverage.cobertura.xml" \
    /d:sonar.cs.vstest.reportsPaths="**/*/*.trx" \
    /d:sonar.exclusions="**/Migrations/**/*.cs"

echo "### Building solution..."
dotnet build Contracting.sln

echo "### Running tests..."
dotnet test Contracting.sln \
  --collect:"XPlat Code Coverage" \
  --results-directory TestResults \
  --logger "trx;LogFileName=test-results.trx"


echo "### Copying coverage report..."
COVERAGE_FILE=$(find . -path '*/coverage.opencover.xml' | head -n 1)

if [ -f "$COVERAGE_FILE" ]; then
  cp "$COVERAGE_FILE" ./coverage.opencover.xml
else
  echo "❌ coverage.opencover.xml not found"
  exit 1
fi

echo "### Finishing SonarScanner..."
dotnet sonarscanner end /d:sonar.token="$SONAR_TOKEN"
