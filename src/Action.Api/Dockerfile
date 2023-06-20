FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIROMENT docker
ENTRYPOINT ["dotnet", "Actio.Api.dll"]
