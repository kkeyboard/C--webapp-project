## SQL server
- Use postgreSQL server
```yaml file
version: '3'

services:
  database:
    image: 'postgres:latest'
    ports:
      - 15432:5432
    env_file:
      - .env
    networks:
      - postgres-network
    volumes:
      - ./db-data/:/var/lib/postgresql/data/
  
  pgadmin:
    image: dpage/pgadmin4
    ports:
      - 15433:80
    env_file:
      - .env
    depends_on:
      - database
    networks:
      - postgres-network
    volumes:
      - ./pgadmin-data/:/var/lib/pgadmin/

networks: 
  postgres-network:
    driver: bridge
```
- .env file
```env
POSTGRES_PASSWORD=$DATABASE_PASSWORD
PGADMIN_DEFAULT_EMAIL=$DEFAULT_EMAIL
PGADMIN_DEFAULT_PASSWORD=$DEFAULT_PASSWORD
```

## Secret Manager
```bash 
dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Sever=localhost; Database=GameStore; User Id=sa; Password=SOME-PASSWORD; TrustServerCertification=True"

dotnet user-secrets list
```