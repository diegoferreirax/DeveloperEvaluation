CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Users" (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Username" character varying(50) NOT NULL,
    "Password" character varying(100) NOT NULL,
    "Phone" character varying(20) NOT NULL,
    "Email" character varying(100) NOT NULL,
    "Status" character varying(20) NOT NULL,
    "Role" character varying(20) NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241014011203_InitialMigrations', '8.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" ADD "CreatedAt" timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';

ALTER TABLE "Users" ADD "UpdatedAt" timestamp with time zone;

CREATE TABLE "Customer" (
    "Id" UUID NOT NULL DEFAULT (GEN_RANDOM_UUID()),
    "Name" character varying(150) NOT NULL,
    CONSTRAINT "PK_Customer" PRIMARY KEY ("Id")
);

CREATE TABLE "Item" (
    "Id" UUID NOT NULL DEFAULT (GEN_RANDOM_UUID()),
    "Product" character varying(200) NOT NULL,
    "UnitPrice" numeric(10,2) NOT NULL,
    CONSTRAINT "PK_Item" PRIMARY KEY ("Id")
);

CREATE TABLE "Sale" (
    "Id" UUID NOT NULL DEFAULT (GEN_RANDOM_UUID()),
    "CustomerId" UUID NOT NULL,
    "SaleNumber" integer NOT NULL,
    "SaleDate" TIMESTAMP NOT NULL,
    "TotalAmount" numeric(10,2) NOT NULL,
    "IsCanceled" boolean NOT NULL,
    "Branch" character varying(200) NOT NULL,
    CONSTRAINT "PK_Sale" PRIMARY KEY ("Id"),
    CONSTRAINT "AK_Sale_SaleNumber" UNIQUE ("SaleNumber"),
    CONSTRAINT "FK_Sale_Customer_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customer" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "SaleItem" (
    "Id" UUID NOT NULL DEFAULT (GEN_RANDOM_UUID()),
    "SaleId" UUID NOT NULL,
    "ItemId" UUID NOT NULL,
    "Quantity" integer NOT NULL,
    "Discount" numeric(5,2) NOT NULL,
    "TotalItemAmount" numeric(10,2) NOT NULL,
    CONSTRAINT "PK_SaleItem" PRIMARY KEY ("Id"),
    CONSTRAINT "AK_SaleItem_SaleId_ItemId" UNIQUE ("SaleId", "ItemId"),
    CONSTRAINT "FK_SaleItem_Item_ItemId" FOREIGN KEY ("ItemId") REFERENCES "Item" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_SaleItem_Sale_SaleId" FOREIGN KEY ("SaleId") REFERENCES "Sale" ("Id") ON DELETE CASCADE
);

INSERT INTO "Customer" ("Id", "Name")
VALUES ('3b765f33-6d77-4da6-906f-511b1e2d009d', 'Maria');
INSERT INTO "Customer" ("Id", "Name")
VALUES ('7bda9e04-f297-42f5-bd3b-c0fed49eacd4', 'Alberto');

INSERT INTO "Item" ("Id", "Product", "UnitPrice")
VALUES ('2ccb7715-03fc-447f-9632-73a8a8bcc816', 'Patagônia', 2.0);
INSERT INTO "Item" ("Id", "Product", "UnitPrice")
VALUES ('5818a8f0-cd7c-4a5e-a7f2-a99507e9260d', 'Brahma', 2.0);
INSERT INTO "Item" ("Id", "Product", "UnitPrice")
VALUES ('5ad99f20-db03-4d06-b539-28ece3792303', 'Skol', 2.0);
INSERT INTO "Item" ("Id", "Product", "UnitPrice")
VALUES ('85c1e99d-4311-4fba-95c3-f327e34d3020', 'Brahma DuploMaute', 2.0);
INSERT INTO "Item" ("Id", "Product", "UnitPrice")
VALUES ('c06a6875-7737-4558-a013-6acfb4e705c7', 'Patagônia IPA', 2.0);

CREATE INDEX "IX_Sale_CustomerId" ON "Sale" ("CustomerId");

CREATE INDEX "IX_SaleItem_ItemId" ON "SaleItem" ("ItemId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250309181449_AddSalesStructureTables', '8.0.10');

COMMIT;

