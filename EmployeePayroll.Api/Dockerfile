# ===================== STAGE 1: BUILD =====================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyasını kopyala ve restore et
COPY ["EmployeePayroll.Api.csproj", "./"]
RUN dotnet restore "EmployeePayroll.Api.csproj"

# Geri kalan dosyaları kopyala
COPY . .

# Publish işlemi
RUN dotnet publish -c Release -o /app/publish

# ===================== STAGE 2: RUNTIME =====================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 7608
ENTRYPOINT ["dotnet", "EmployeePayroll.Api.dll"]
