echo Iniciando Projeto BackEnd Api......
start dotnet run --project .\src\GestaoEducacional.Api\GestaoEducacional.Api.csproj
echo Iniciando Projeto BackEnd Api Pronto!!!
echo Instalando pacotes npm
echo Iniciando Projeto Front End Angular .....
start dotnet run --project .\src\GestaoEducacional.View\GestaoEducacional.View.csproj
echo Iniciando Projeto Front End Angular Pronto
cd src\GestaoEducacional.View\ClientApp
start npm i
cd .\
TIMEOUT 5
start http://localhost:5068/swagger/index.html
TIMEOUT 5
start http://localhost:5057/
pause