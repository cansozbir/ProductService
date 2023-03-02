CREATE TABLE IF NOT EXISTS "Stores" (
    "Id" integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    "StoreName" varchar(250) NOT NULL
);

CREATE TABLE IF NOT EXISTS "Products" (
    "Id" integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    "ProductName" varchar(250) NOT NULL,
    "Cost" INT NOT NULL,
    "SalesPrice" INT NOT NULL
);

CREATE TABLE IF NOT EXISTS "InventorySales" (
    "Id" integer GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    "ProductId" INT NOT NULL,
    "StoreId" INT NOT NULL,
    "Date" DATE NOT NULL,
    "SalesQuantity" INT NOT NULL,
    "Stock" INT NOT NULL
);