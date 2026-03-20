FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY VeterinaryClinicManagement.slnx .
COPY VeterinaryClinic.Shared/ VeterinaryClinic.Shared/
COPY VeterinaryClinic.Data/ VeterinaryClinic.Data/
COPY VeterinaryClinic.Services/ VeterinaryClinic.Services/
COPY VeterinaryClinicMVC/ VeterinaryClinicMVC/

RUN dotnet restore VeterinaryClinicMVC/VeterinaryClinicMVC.csproj
RUN dotnet publish VeterinaryClinicMVC/VeterinaryClinicMVC.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "VeterinaryClinicMVC.dll"]
