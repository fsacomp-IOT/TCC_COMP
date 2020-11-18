FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app/TCC_COMP

COPY . .

RUN cd /app/TCC_COMP/TCC_COMP.API \
    ; dotnet publish -c Release -o /app/apigarden

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY --from=apigarden /app/apigarden/* /app/apigarden

CMD ["/usr/bin/dotnet /app/apigarden/TCC_COMP.API.dll"]


