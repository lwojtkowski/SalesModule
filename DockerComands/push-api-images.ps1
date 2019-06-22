az acr login --name SalesModule

<# CatalogAPI #>
docker build -t salesmodule/catalog-api:1.0 . -f CatalogAPI.Dockerfile

docker tag salesmodule/catalog-api:1.0 salesmodule.azurecr.io/catalog-api:1.0

docker push salesmodule.azurecr.io/catalog-api:1.0

<# CustomersAPI #>

docker build -t salesmodule/customers-api:1.0 . -f CustomersAPI.Dockerfile

docker tag salesmodule/customers-api:1.0 salesmodule.azurecr.io/customers-api:1.0

docker push salesmodule.azurecr.io/customers-api:1.0

<# OrderingAPI #>

docker build -t salesmodule/ordering-api:1.0 . -f OrderingAPI.Dockerfile

docker tag salesmodule/ordering-api:1.0 salesmodule.azurecr.io/ordering-api:1.0

docker push salesmodule.azurecr.io/ordering-api:1.0