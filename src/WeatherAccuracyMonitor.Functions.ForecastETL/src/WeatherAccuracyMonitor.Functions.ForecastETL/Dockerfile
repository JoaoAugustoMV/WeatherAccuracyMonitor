# =========================
# Stage 1 – Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo (mais simples e confiável para templates do VS)
COPY . .

# Restaura e publica
RUN dotnet restore
RUN dotnet publish \
    -c Release \
    -o /app/publish

# =========================
# Stage 2 – Runtime
# =========================
FROM public.ecr.aws/lambda/dotnet:8
WORKDIR /var/task

COPY --from=build /app/publish .

CMD ["WeatherAccuracyMonitor.Functions.ForecastETL::WeatherAccuracyMonitor.Functions.ForecastETL.Function::FunctionHandler"]
