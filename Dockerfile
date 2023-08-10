FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
EXPOSE 5220
WORKDIR /.
COPY ["Domain/Domain.csproj", "/Domain"]
COPY ["Library2.2/Library2.2.csproj", "/Library2.2"]
#COPY Library2.2/bin/Release/net7.0/publish .
RUN dotnet restore "Library2.2/Library2.2.csproj"
RUN dotnet build "Library2.2/Library2.2.csproj" -c Release -o /build
RUN dotnet publish "Library2.2/Library2.2.csproj" -c Release -o /publish /p:UseAppHost=false
ENTRYPOINT ["dotnet", "Library2.2.dll"]