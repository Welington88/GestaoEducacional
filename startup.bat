echo Iniciando Projeto BackEnd Api......
start dotnet run --project .\src\GestaoEducacional.Api\GestaoEducacional.Api.csproj
echo Iniciando Projeto BackEnd Api Pronto!!!
echo Iniciando Projeto Front End Angular .....
start dotnet run --project .\src\GestaoEducacional.View\GestaoEducacional.View.csproj
echo Iniciando Projeto Front End Angular Pronto
TIMEOUT 10
start http://localhost:5057/
pause