# Ejecutar desde la raíz donde está contracting.sln
$token = "e285392d8355a684240908092ca756c091ec4d3b"
$projectKey = "carlosclavijo_Nurtricenter-Contracting"
$org = "carlosclavijo"

dotnet sonarscanner begin `
  /k:"$projectKey" `
  /o:"$org" `
  /d:sonar.login="$token" `
  /d:sonar.cs.opencover.reportsPaths="Contracting.Test/TestResults/**/coverage.cobertura.xml"

dotnet test --collect:"XPlat Code Coverage"

dotnet sonarscanner end /d:sonar.login="$token"
