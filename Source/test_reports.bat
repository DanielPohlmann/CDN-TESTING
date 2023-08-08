cd ../CandidateTesting.DanielHelerPohlmann.Tests
dotnet tool install -g dotnet-reportgenerator-globaltool
dotnet test -c Release /p:CollectCoverage=true /p:CoverletOutput=..\Source\results\coverage /p:CoverletOutputFormat=opencover
reportgenerator -reports:"..\Source\results\coverage.opencover.xml" -targetdir:"..\Source\results\coveragereport" -reporttypes:Html
cd ..\Source\results\coveragereport
index.html
pause