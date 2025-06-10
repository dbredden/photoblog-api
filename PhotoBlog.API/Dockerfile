# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PhotoBlog.API/PhotoBlog.API.csproj", "PhotoBlog.API/"]
COPY ["PhotoBlog.Application/PhotoBlog.Application.csproj", "PhotoBlog.Application/"]
COPY ["PhotoBlog.Domain/PhotoBlog.Domain.csproj", "PhotoBlog.Domain/"]
COPY ["PhotoBlog.Infrastructure/PhotoBlog.Infrastructure.csproj", "PhotoBlog.Infrastructure/"]
COPY ["PhotoBlog.Database/PhotoBlog.Database.csproj", "PhotoBlog.Database/"]

RUN dotnet restore "PhotoBlog.API/PhotoBlog.API.csproj"

COPY . .
WORKDIR "/src/PhotoBlog.API"
RUN dotnet publish "PhotoBlog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "PhotoBlog.API.dll"]
