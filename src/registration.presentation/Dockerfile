FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "registration.presentation.csproj" --disable-parallel
RUN dotnet publish "registration.presentation.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5120
ENV ASPNETCORE_URLS=http://+:5120
ENTRYPOINT ["dotnet", "registration.presentation.dll"]