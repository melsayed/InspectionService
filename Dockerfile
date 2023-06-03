#docker build -t melsayed1987/inspectionservice . 
#docker run -p 8090:80  melsayed1987/inspectionservice
#docker file should be "Dockerfile"
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime

WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "InspectionService.dll"]