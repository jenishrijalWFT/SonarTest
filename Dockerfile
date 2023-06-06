#Pull dotnet image from https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /Boss.Gateway
COPY . .

# copy csproj and restore as distinct layers also run ls just to see if files are present
RUN dotnet restore

# copy everything else and build app
COPY Boss.Gateway.Api/. /Boss.Gateway/
WORKDIR /Boss.Gateway/
RUN dotnet publish Boss.Gateway.sln  -c Release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /Boss.Gateway
COPY --from=build-env /Boss.Gateway/out .
COPY /Boss.Gateway.Persistence/Migrations/ /Boss.Gateway/Boss.Gateway.Persistence/Migrations
RUN cd //usr/share/dotnet/shared/Microsoft.NETCore.App/7.0.2/
RUN apt-get update; apt-get install libfontconfig1 wget -y
RUN wget -q https://github.com/mono/SkiaSharp/releases/download/v1.60.3/libSkiaSharp.so
RUN chmod -x libSkiaSharp.so
RUN cd -
ENV ASPNETCORE_URLS http://+:8013
ENTRYPOINT ["dotnet", "Boss.Gateway.Api.dll"]

# Expose port 8013
EXPOSE 80
EXPOSE 8013
