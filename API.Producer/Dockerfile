#syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy *.sln and *.csproj
COPY ../*.sln .
COPY ../API.Producer/*.csproj ./API.Producer/
COPY ../Data.Entities/*.csproj ./Data.Entities/
COPY ../Worker.Consumer/*.csproj ./Worker.Consumer/

# Restore as distinct layers
RUN dotnet restore

# Copy everything else
COPY ../API.Producer/. ./API.Producer/
COPY ../Data.Entities/. ./Data.Entities/
COPY ../Worker.Consumer/. ./Worker.Consumer/

# Build and publish a release
WORKDIR /App/API.Producer
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/API.Producer/out .

# Copy entrypoint script and make it executable
COPY --from=build-env /App/API.Producer/entrypoint.sh .
RUN chmod +x entrypoint.sh

EXPOSE 80

ENTRYPOINT ["/bin/bash", "entrypoint.sh"]