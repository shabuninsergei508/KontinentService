using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KontinentService.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cabins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    ShipName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    UrlRus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    QuestionText = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    IsAnswered = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    AppealPage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    RegDate = table.Column<DateTime>(nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    IsSpecial = table.Column<bool>(nullable: false),
                    IsTour = table.Column<bool>(nullable: false),
                    IsBustour = table.Column<bool>(nullable: false),
                    IsCruise = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filters_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    UrlRus = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterAllowables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    FilterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterAllowables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterAllowables_Filters_FilterId",
                        column: x => x.FilterId,
                        principalTable: "Filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Discount = table.Column<int>(nullable: false),
                    UrlRus = table.Column<string>(nullable: true),
                    SpecialDescription = table.Column<string>(nullable: true),
                    NumberOfOrders = table.Column<int>(nullable: false),
                    IndexOnPage = table.Column<int>(nullable: false),
                    DateIn = table.Column<DateTime>(nullable: false),
                    DateOut = table.Column<DateTime>(nullable: false),
                    Route = table.Column<string>(nullable: true),
                    IsHot = table.Column<bool>(nullable: false),
                    IsTour = table.Column<bool>(nullable: false),
                    IsBustour = table.Column<bool>(nullable: false),
                    IsCruise = table.Column<bool>(nullable: false),
                    SubcategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabinValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TourId = table.Column<int>(nullable: false),
                    CabinId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CabinValues_Cabins_CabinId",
                        column: x => x.CabinId,
                        principalTable: "Cabins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinValues_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TourId = table.Column<int>(nullable: true),
                    FilterAllowableId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterValues_FilterAllowables_FilterAllowableId",
                        column: x => x.FilterAllowableId,
                        principalTable: "FilterAllowables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterValues_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    TourId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    QuestionText = table.Column<string>(nullable: true),
                    DateAdd = table.Column<DateTime>(nullable: false),
                    IsAnswered = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    TourId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TourId = table.Column<int>(nullable: true),
                    PropertyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyValues_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyValues_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    TourId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    ScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleValues_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CabinValues_CabinId",
                table: "CabinValues",
                column: "CabinId");

            migrationBuilder.CreateIndex(
                name: "IX_CabinValues_TourId",
                table: "CabinValues",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterAllowables_FilterId",
                table: "FilterAllowables",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_CategoryId",
                table: "Filters",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterValues_FilterAllowableId",
                table: "FilterValues",
                column: "FilterAllowableId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterValues_TourId",
                table: "FilterValues",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_TourId",
                table: "Images",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TourId",
                table: "Orders",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValues_PropertyId",
                table: "PropertyValues",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValues_TourId",
                table: "PropertyValues",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TourId",
                table: "Schedules",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleValues_ScheduleId",
                table: "ScheduleValues",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_SubcategoryId",
                table: "Tours",
                column: "SubcategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabinValues");

            migrationBuilder.DropTable(
                name: "FeedbackMessages");

            migrationBuilder.DropTable(
                name: "FilterValues");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PropertyValues");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ScheduleValues");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cabins");

            migrationBuilder.DropTable(
                name: "FilterAllowables");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
