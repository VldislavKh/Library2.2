FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
EXPOSE 80
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "Library2.2.dll"]