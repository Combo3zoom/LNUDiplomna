FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app

COPY ../LightsOn.sln ./ 
COPY ../LightsOn.WebApp/LightsOn.WebApp.csproj ./LightsOn.WebApp/

COPY ../LightsOn.BlazorApp.Tests.Unit/LightsOn.BlazorApp.Tests.Unit.csproj ./LightsOn.BlazorApp.Tests.Unit/

RUN dotnet restore 

COPY . ./  
RUN dotnet publish -c Release -o out

FROM nginx:1.23.0-alpine
WORKDIR /app

EXPOSE 8080
COPY LightsOn.WebApp/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/out/wwwroot /usr/share/nginx/html
