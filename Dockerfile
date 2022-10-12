FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /webapp
RUN dotnet dev-certs https

EXPOSE 80
EXPOSE 443
EXPOSE 7211
EXPOSE 5007

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /webapp
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=build /webapp/out .
ENTRYPOINT [ "dotnet", "NET-Mongo.dll",  "--environment=Development"]