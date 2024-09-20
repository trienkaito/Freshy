using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FRESHY.Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class V11_INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "customer");

            migrationBuilder.EnsureSchema(
                name: "employee");

            migrationBuilder.EnsureSchema(
                name: "order");

            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.EnsureSchema(
                name: "review");

            migrationBuilder.EnsureSchema(
                name: "shipping");

            migrationBuilder.EnsureSchema(
                name: "supplier");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Phone = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Avatar = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(155)", maxLength: 155, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true),
                    Salary = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingCompanies",
                schema: "shipping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    FeatureImage = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    ShippingPrice = table.Column<decimal>(type: "MONEY", nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "supplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    FeatureImage = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true),
                    IsValid = table.Column<bool>(type: "BIT", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    StartedOn = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    EndedOn = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DiscountValue = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "customer",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderAddresses",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "CHAR(13)", maxLength: 13, nullable: false),
                    Country = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    District = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Ward = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Details = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    IsShippingAddress = table.Column<bool>(type: "BIT", nullable: false),
                    IsDefaultAddress = table.Column<bool>(type: "BIT", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "customer",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Avatar = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SSN = table.Column<string>(type: "NVARCHAR(16)", maxLength: 16, nullable: false),
                    DOB = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LivingAddress = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: false),
                    Hometown = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    CvLink = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobPositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_JobPositions_JobPositionId",
                        column: x => x.JobPositionId,
                        principalSchema: "employee",
                        principalTable: "JobPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    FeatureImage = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    DOM = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsShowToCustomer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "product",
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "supplier",
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderAddress = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    OrderStatus = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false, defaultValue: "SUCCESSED"),
                    PaymentType = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: false),
                    ShippingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductsAmount = table.Column<double>(type: "float", nullable: false),
                    PaidAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "customer",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_ShippingCompanies_ShippingId",
                        column: x => x.ShippingId,
                        principalSchema: "shipping",
                        principalTable: "ShippingCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalSchema: "order",
                        principalTable: "Vouchers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductLikes",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLikes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "customer",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLikes_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductUnits",
                schema: "product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitType = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    UnitValue = table.Column<double>(type: "FLOAT", nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    ImportPrice = table.Column<decimal>(type: "MONEY", nullable: false),
                    SellPrice = table.Column<decimal>(type: "MONEY", nullable: false),
                    UnitFeatureImage = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductUnits_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "review",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "NVARCHAR", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ParentReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RatingValue = table.Column<int>(type: "INT", nullable: true),
                    LikeCount = table.Column<int>(type: "INT", nullable: false),
                    IsBeingReply = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "customer",
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoughtQuantity = table.Column<int>(type: "INT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "MONEY", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalSchema: "customer",
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_ProductUnits_ProductUnitId",
                        column: x => x.ProductUnitId,
                        principalSchema: "product",
                        principalTable: "ProductUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ORDER_ITEMS",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoughtQuantity = table.Column<int>(type: "INT", nullable: false),
                    TotalProductPrice = table.Column<decimal>(type: "MONEY", nullable: false),
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_ITEMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ORDER_ITEMS_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalSchema: "order",
                        principalTable: "OrderDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ORDER_ITEMS_ProductUnits_ProductUnitId",
                        column: x => x.ProductUnitId,
                        principalSchema: "product",
                        principalTable: "ProductUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ORDER_ITEMS_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "product",
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "product",
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1261428f-9ef0-422c-8e07-8a050da9d348"), "CONDIMENTS" },
                    { new Guid("1b3f6be8-b0f3-4b24-8077-f72bafe24296"), "DAIRY PRODUCTS" },
                    { new Guid("270bd775-dba0-4a97-ae47-25469933793b"), "MEAT" },
                    { new Guid("3fa5edf7-6de6-4e09-8a2a-3439ad8c424d"), "VEGATABLES" },
                    { new Guid("42e3fc9f-43e4-4f93-a1d6-8fb40c1245ec"), "FRUITS" },
                    { new Guid("46dcce11-f1bf-4be2-98a4-e85689ee3f47"), "SEAFOOD" },
                    { new Guid("6c237811-55a3-4a9a-bef6-5b347a4e17b7"), "COMBO" },
                    { new Guid("de1c643c-d1c2-4745-96b0-900b8a8c4252"), "BEVERAGES" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                schema: "customer",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                schema: "customer",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductUnitId",
                schema: "customer",
                table: "CartItems",
                column: "ProductUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                schema: "customer",
                table: "Carts",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                schema: "customer",
                table: "Customers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Phone",
                schema: "customer",
                table: "Customers",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AccountId",
                schema: "employee",
                table: "Employees",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                schema: "employee",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobPositionId",
                schema: "employee",
                table: "Employees",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PhoneNumber",
                schema: "employee",
                table: "Employees",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobPositions_Name",
                schema: "employee",
                table: "JobPositions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_ITEMS_OrderDetailId",
                schema: "order",
                table: "ORDER_ITEMS",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_ITEMS_ProductId",
                schema: "order",
                table: "ORDER_ITEMS",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_ITEMS_ProductUnitId",
                schema: "order",
                table: "ORDER_ITEMS",
                column: "ProductUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddresses_CustomerId",
                schema: "customer",
                table: "OrderAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_CustomerId",
                schema: "order",
                table: "OrderDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ShippingId",
                schema: "order",
                table: "OrderDetails",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_VoucherId",
                schema: "order",
                table: "OrderDetails",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLikes_CustomerId",
                schema: "product",
                table: "ProductLikes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLikes_ProductId",
                schema: "product",
                table: "ProductLikes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                schema: "product",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                schema: "product",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeId",
                schema: "product",
                table: "Products",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Name",
                schema: "product",
                table: "ProductTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_ProductId",
                schema: "product",
                table: "ProductUnits",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                schema: "review",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EmployeeId",
                schema: "review",
                table: "Reviews",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                schema: "review",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Name",
                schema: "supplier",
                table: "Suppliers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_Code",
                schema: "order",
                table: "Vouchers",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "ORDER_ITEMS",
                schema: "order");

            migrationBuilder.DropTable(
                name: "OrderAddresses",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "ProductLikes",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "review");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "OrderDetails",
                schema: "order");

            migrationBuilder.DropTable(
                name: "ProductUnits",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "ShippingCompanies",
                schema: "shipping");

            migrationBuilder.DropTable(
                name: "Vouchers",
                schema: "order");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "product");

            migrationBuilder.DropTable(
                name: "JobPositions",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "ProductTypes",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "supplier");
        }
    }
}