FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# build
COPY Pingbot.sln .
COPY ./src ./src
RUN dotnet build

# publish
RUN dotnet publish -c Release -o out

# runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
RUN useradd --create-home --shell /bin/bash runner
USER runner
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Pingbot.Api.dll"]