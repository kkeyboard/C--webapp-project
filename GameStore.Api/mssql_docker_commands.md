```bash
$sa_password = "password"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password \
    -p 1433:1433 \
    -v sqlvolume /var/opt/mssql \
    -d --rm mssql mcr.microsoft.com/mssql/server:2022-latest
```