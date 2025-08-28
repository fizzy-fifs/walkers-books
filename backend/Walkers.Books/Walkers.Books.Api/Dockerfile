
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj (project file is at the root of the build context)
COPY ./Walkers.Books.Api.csproj ./
RUN dotnet restore ./Walkers.Books.Api.csproj

# copy everything and build
COPY . ./
RUN dotnet publish ./Walkers.Books.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Walkers.Books.Api.dll"]
