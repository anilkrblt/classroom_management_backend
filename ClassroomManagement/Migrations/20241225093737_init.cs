using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClassroomManagement.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    BuildingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.BuildingId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.InstructorId);
                    table.ForeignKey(
                        name: "FK_Instructor_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lecture",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecture", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Lecture_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    ExamCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    RoomType = table.Column<int>(type: "INTEGER", nullable: false),
                    IsProjectorWorking = table.Column<bool>(type: "INTEGER", nullable: false),
                    Equipment = table.Column<string>(type: "TEXT", nullable: false),
                    BuildingId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Room_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    GradeLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Student_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LectureCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.ExamId);
                    table.ForeignKey(
                        name: "FK_Exam_Lecture_LectureCode",
                        column: x => x.LectureCode,
                        principalTable: "Lecture",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorPreference",
                columns: table => new
                {
                    PreferenceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstructorId = table.Column<int>(type: "INTEGER", nullable: false),
                    LectureCode = table.Column<string>(type: "TEXT", nullable: false),
                    PreferredTime = table.Column<string>(type: "TEXT", nullable: false),
                    UnavailableTimes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorPreference", x => x.PreferenceId);
                    table.ForeignKey(
                        name: "FK_InstructorPreference_Instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorPreference_Lecture_LectureCode",
                        column: x => x.LectureCode,
                        principalTable: "Lecture",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LectureCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_Lecture_LectureCode",
                        column: x => x.LectureCode,
                        principalTable: "Lecture",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "LectureSession",
                columns: table => new
                {
                    LectureSessionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayOfWeek = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    LectureCode = table.Column<string>(type: "TEXT", nullable: true),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    InstructorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureSession", x => x.LectureSessionId);
                    table.ForeignKey(
                        name: "FK_LectureSession_Instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureSession_Lecture_LectureCode",
                        column: x => x.LectureCode,
                        principalTable: "Lecture",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_LectureSession_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PhotoPath = table.Column<byte[]>(type: "BLOB", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubmittedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Request_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    ClubId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PresidentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageData = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.ClubId);
                    table.ForeignKey(
                        name: "FK_Club_Student_PresidentId",
                        column: x => x.PresidentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    LectureCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enrollment_Lecture_LectureCode",
                        column: x => x.LectureCode,
                        principalTable: "Lecture",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamSession",
                columns: table => new
                {
                    ExamSessionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExamDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DayOfWeek = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExamId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSession", x => x.ExamSessionId);
                    table.ForeignKey(
                        name: "FK_ExamSession_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamSession_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationRecipient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NotificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRecipient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationRecipient_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationRecipient_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClubMembership",
                columns: table => new
                {
                    ClubMembershipId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClubId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubMembership", x => x.ClubMembershipId);
                    table.ForeignKey(
                        name: "FK_ClubMembership_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "ClubId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubMembership_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    ReservationType = table.Column<int>(type: "INTEGER", nullable: false),
                    LectureCode = table.Column<string>(type: "TEXT", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventRegisterLink = table.Column<string>(type: "TEXT", nullable: false),
                    ClubName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    ClubId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservation_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "ClubId");
                    table.ForeignKey(
                        name: "FK_Reservation_Lecture_LectureCode",
                        column: x => x.LectureCode,
                        principalTable: "Lecture",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Building",
                columns: new[] { "BuildingId", "Name" },
                values: new object[,]
                {
                    { 1, "A" },
                    { 2, "B" }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentId", "Name" },
                values: new object[,]
                {
                    { 1, "Computer Engineering" },
                    { 2, "Machine Engineering" },
                    { 3, "Electric Electronic Engineering" },
                    { 4, "BioEngineering" },
                    { 5, "Food Engineering" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Email", "IsAdmin", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "alibaba@trakya.edu.tr", true, "Ali Baba", "123" },
                    { 2, "ayseyilmaz@trakya.edu.tr", false, "Ayşe Yılmaz", "abc" },
                    { 3, "mehmetcelik@trakya.edu.tr", false, "Mehmet Çelik", "456" },
                    { 4, "fatmademir@trakya.edu.tr", false, "Fatma Demir", "789" },
                    { 5, "ahmetkaya@trakya.edu.tr", true, "Ahmet Kaya", "xyz" },
                    { 6, "zeynepaksoy@trakya.edu.tr", false, "Zeynep Aksoy", "321" },
                    { 7, "hakanyildirim@trakya.edu.tr", false, "Hakan Yıldırım", "654" },
                    { 8, "seliner@trakya.edu.tr", false, "Selin Er", "987" },
                    { 9, "mustafakurt@trakya.edu.tr", true, "Mustafa Kurt", "qwe" },
                    { 10, "elifsari@trakya.edu.tr", false, "Elif Sarı", "zxc" }
                });

            migrationBuilder.InsertData(
                table: "Notification",
                columns: new[] { "NotificationId", "CreatedAt", "LectureCode", "Message" },
                values: new object[,]
                {
                    { 101, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Welcome to the new semester!" },
                    { 102, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Reminder: Submit your assignments by the end of this week." },
                    { 103, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Your midterm results are now available." },
                    { 104, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Join us for the upcoming workshop on AI and Machine Learning." },
                    { 105, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Library hours extended during finals week." },
                    { 106, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "New internship opportunities are available. Check your emails for details." },
                    { 107, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Update: Campus Wi-Fi maintenance scheduled for tomorrow." },
                    { 108, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Congratulations to all the participants of the Hackathon!" },
                    { 109, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Your course registration is now confirmed." },
                    { 110, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Don't miss the cultural fest this weekend!" }
                });

            migrationBuilder.InsertData(
                table: "Instructor",
                columns: new[] { "InstructorId", "DepartmentId", "Email", "IsAdmin", "Name", "Password", "Title" },
                values: new object[,]
                {
                    { 1, 1, "altanmesut@trakya.edu.tr", false, "Altan Mesut", "123", "Dr. Ögretim Üyesi" },
                    { 2, 1, "emirozturk@trakya.edu.tr", false, "Emir Öztürk", "123", "Dr. Ögretim Üyesi" },
                    { 3, 2, "aysekaya@trakya.edu.tr", false, "Ayşe Kaya", "456", "Doçent Doktor" },
                    { 4, 3, "mehmetyildiz@trakya.edu.tr", true, "Mehmet Yıldız", "789", "Profesör Doktor" },
                    { 5, 2, "fatmademir@trakya.edu.tr", false, "Fatma Demir", "abc", "Doçent Doktor" },
                    { 6, 1, "ahmetcelik@trakya.edu.tr", false, "Ahmet Çelik", "xyz", "Dr. Ögretim Üyesi" },
                    { 7, 3, "zeyneparslan@trakya.edu.tr", false, "Zeynep Arslan", "qwe", "Dr. Ögretim Üyesi" },
                    { 8, 4, "mustafaozkan@trakya.edu.tr", false, "Mustafa Özkan", "zxc", "Dr. Ögretim Üyesi" },
                    { 9, 3, "elifsari@trakya.edu.tr", true, "Elif Sarı", "poi", "Profesör Doktor" },
                    { 10, 2, "hakanyilmaz@trakya.edu.tr", false, "Hakan Yılmaz", "mnb", "Doçent Doktor" }
                });

            migrationBuilder.InsertData(
                table: "Lecture",
                columns: new[] { "Code", "DepartmentId", "Name" },
                values: new object[,]
                {
                    { "BIOL102", 1, "Biology" },
                    { "CHEM301", 3, "Chemistry" },
                    { "CS202", 1, "Computer Science" },
                    { "ECON301", 3, "Economics" },
                    { "ENG103", 2, "English Language" },
                    { "ENGM202", 2, "Engineering Mechanics" },
                    { "HIST205", 1, "History" },
                    { "MATH101", 1, "Mathematics I" },
                    { "PHIL101", 2, "Philosophy" },
                    { "PHYS201", 2, "Physics I" }
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "RoomId", "BuildingId", "Capacity", "DepartmentId", "Equipment", "ExamCapacity", "IsActive", "IsProjectorWorking", "Name", "RoomType" },
                values: new object[,]
                {
                    { 1, 1, 88, 1, "Bilgisayar,Sıra", 40, true, true, "L206", 2 },
                    { 2, 1, 40, 1, "Bilgisyar", 40, true, true, "L208", 2 },
                    { 3, 1, 75, 2, "Sıra", 25, true, true, "D201", 0 },
                    { 4, 1, 78, 2, "Sıra", 28, true, true, "D202", 0 },
                    { 5, 2, 172, 3, "sıra", 58, true, true, "A1", 5 },
                    { 6, 2, 178, 4, "sıra", 58, true, true, "A2", 5 },
                    { 7, 2, 178, 5, "sıra", 58, true, true, "A3", 5 },
                    { 8, 2, 81, 3, "sıra", 27, true, true, "S1", 0 },
                    { 9, 1, 100, 2, "sıra", 45, true, true, "L104", 0 },
                    { 10, 1, 40, 4, "sıra", 40, true, true, "L204", 0 }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentId", "DepartmentId", "Email", "GradeLevel", "Name", "Password" },
                values: new object[,]
                {
                    { 1, 1, "kemal123@hotmail.com", 2, "Kemal", "2131233A" },
                    { 2, 2, "ahmet123@hotmail.com", 2, "Ahmet", "2131233A" },
                    { 3, 3, "mustafa123@hotmail.com", 2, "Mustafa", "2131233A" },
                    { 4, 4, "bülent123@hotmail.com", 2, "Bülent", "2131233A" },
                    { 5, 5, "osman123@hotmail.com", 2, "Osman", "2131233A" },
                    { 6, 1, "hakan123@hotmail.com", 2, "Hakan", "2131233A" },
                    { 7, 2, "orhan123@hotmail.com", 2, "Orhan", "2131233A" },
                    { 8, 3, "kenan123@hotmail.com", 2, "Kenan", "2131233A" },
                    { 9, 3, "kamil123@hotmail.com", 2, "Kamil", "2131233A" },
                    { 10, 4, "salih123@hotmail.com", 2, "Salih", "2131233A" }
                });

            migrationBuilder.InsertData(
                table: "Club",
                columns: new[] { "ClubId", "ImageData", "Name", "PresidentId" },
                values: new object[,]
                {
                    { 1, new byte[0], "Mathematics Club", 1 },
                    { 2, new byte[0], "Physics Club", 2 },
                    { 3, new byte[0], "Chemistry Society", 3 },
                    { 4, new byte[0], "Biology Club", 4 },
                    { 5, new byte[0], "Computer Science Club", 5 },
                    { 6, new byte[0], "Engineering Club", 6 },
                    { 7, new byte[0], "Economics Club", 7 },
                    { 8, new byte[0], "History Society", 8 },
                    { 9, new byte[0], "Philosophy Club", 9 },
                    { 10, new byte[0], "Art and Design Club", 10 }
                });

            migrationBuilder.InsertData(
                table: "Enrollment",
                columns: new[] { "EnrollmentId", "LectureCode", "StudentId" },
                values: new object[,]
                {
                    { 1, "MATH101", 1 },
                    { 2, "PHYS201", 2 },
                    { 3, "CHEM301", 3 },
                    { 4, "BIOL102", 4 },
                    { 5, "CS202", 5 },
                    { 6, "ENG103", 6 },
                    { 7, "HIST205", 7 },
                    { 8, "ENGM202", 8 },
                    { 9, "ECON301", 9 },
                    { 10, "PHIL101", 10 }
                });

            migrationBuilder.InsertData(
                table: "Exam",
                columns: new[] { "ExamId", "LectureCode", "Name" },
                values: new object[,]
                {
                    { 1, "MATH101", "Mathematics Final Exam" },
                    { 2, "PHYS201", "Physics Midterm Exam" },
                    { 3, "CHEM301", "Chemistry Final Exam" },
                    { 4, "BIOL102", "Biology Quiz" },
                    { 5, "CS202", "Computer Science Lab Exam" },
                    { 6, "ENG103", "English Language Test" },
                    { 7, "HIST205", "History Midterm Exam" },
                    { 8, "ENGM202", "Engineering Mechanics Final" },
                    { 9, "ECON301", "Economics Final Exam" },
                    { 10, "PHIL101", "Philosophy Critical Thinking Test" }
                });

            migrationBuilder.InsertData(
                table: "InstructorPreference",
                columns: new[] { "PreferenceId", "InstructorId", "LectureCode", "PreferredTime", "UnavailableTimes" },
                values: new object[,]
                {
                    { 1, 1, "MATH101", "{ \"Monday\": [\"09:00-11:00\", \"13:00-15:00\"], \"Wednesday\": [\"10:00-12:00\"] }", "{ \"Friday\": [\"08:00-10:00\", \"14:00-16:00\"] }" },
                    { 2, 2, "PHYS201", "{ \"Tuesday\": [\"10:00-12:00\", \"14:00-16:00\"], \"Thursday\": [\"09:00-11:00\"] }", "{ \"Monday\": [\"08:00-10:00\"], \"Wednesday\": [\"15:00-17:00\"] }" },
                    { 3, 3, "BIOL102", "{ \"Monday\": [\"13:00-15:00\"], \"Friday\": [\"09:00-11:00\"] }", "{ \"Tuesday\": [\"10:00-12:00\"] }" },
                    { 4, 4, "CH401", "{ \"Wednesday\": [\"08:00-10:00\", \"12:00-14:00\"] }", "{ \"Thursday\": [\"09:00-11:00\"] }" },
                    { 5, 5, "CS202", "{ \"Tuesday\": [\"14:00-16:00\"], \"Friday\": [\"08:00-10:00\"] }", "{ \"Monday\": [\"09:00-11:00\"] }" }
                });

            migrationBuilder.InsertData(
                table: "LectureSession",
                columns: new[] { "LectureSessionId", "DayOfWeek", "EndTime", "InstructorId", "LectureCode", "RoomId", "StartTime" },
                values: new object[,]
                {
                    { 1, "Monday", new TimeSpan(0, 10, 30, 0, 0), 1, "MATH101", 1, new TimeSpan(0, 9, 0, 0, 0) },
                    { 2, "Tuesday", new TimeSpan(0, 12, 30, 0, 0), 2, "PHYS201", 2, new TimeSpan(0, 11, 0, 0, 0) },
                    { 3, "Wednesday", new TimeSpan(0, 14, 30, 0, 0), 3, "CHEM301", 3, new TimeSpan(0, 13, 0, 0, 0) },
                    { 4, "Thursday", new TimeSpan(0, 11, 30, 0, 0), 4, "BIOL102", 4, new TimeSpan(0, 10, 0, 0, 0) },
                    { 5, "Friday", new TimeSpan(0, 16, 30, 0, 0), 5, "CS202", 5, new TimeSpan(0, 15, 0, 0, 0) },
                    { 6, "Monday", new TimeSpan(0, 9, 30, 0, 0), 6, "ENG103", 6, new TimeSpan(0, 8, 0, 0, 0) },
                    { 7, "Tuesday", new TimeSpan(0, 15, 30, 0, 0), 7, "HIST205", 7, new TimeSpan(0, 14, 0, 0, 0) },
                    { 8, "Wednesday", new TimeSpan(0, 11, 0, 0, 0), 8, "ENGM202", 8, new TimeSpan(0, 9, 30, 0, 0) },
                    { 9, "Thursday", new TimeSpan(0, 17, 30, 0, 0), 9, "ECON301", 9, new TimeSpan(0, 16, 0, 0, 0) },
                    { 10, "Friday", new TimeSpan(0, 13, 30, 0, 0), 10, "PHIL101", 10, new TimeSpan(0, 12, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "NotificationRecipient",
                columns: new[] { "Id", "NotificationId", "StudentId" },
                values: new object[,]
                {
                    { 1, 101, 1 },
                    { 2, 102, 2 },
                    { 3, 103, 3 },
                    { 4, 104, 4 },
                    { 5, 105, 5 },
                    { 6, 106, 6 },
                    { 7, 107, 7 },
                    { 8, 108, 8 },
                    { 9, 109, 9 },
                    { 10, 110, 10 }
                });

            migrationBuilder.InsertData(
                table: "Request",
                columns: new[] { "RequestId", "Content", "CreatedAt", "PhotoPath", "RoomId", "Status", "SubmittedBy", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Fix the projector in Room 101", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], 1, "Pending", 1, "Maintenance", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Clean the whiteboard in Room 102", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], 2, "Approved", 2, "Cleaning", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Replace the chairs in Room 103", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], 3, "Completed", 3, "Equipment", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Internet not working in Room 104", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], 4, "Pending", 4, "IT Support", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Add a table to Room 105", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], 5, "Approved", 5, "Furniture", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Fix the air conditioning in Room 106", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], 6, "Pending", 6, "HVAC", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Microphone not working in Room 107", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], 7, "Completed", 7, "Audio", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Replace the bulbs in Room 108", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], 8, "Pending", 8, "Lighting", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Add extra chairs to Room 109", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], 9, "Approved", 9, "Seating", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Install a lock in Room 110", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new byte[0], 10, "Pending", 10, "Security", new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "ReservationId", "ClubId", "ClubName", "CreatedBy", "EndTime", "EventDate", "EventRegisterLink", "LectureCode", "ReservationType", "RoomId", "StartTime" },
                values: new object[,]
                {
                    { 1, null, "Science Club", 1, new TimeSpan(0, 16, 0, 0, 0), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/event1", "CS202", 0, 1, new TimeSpan(0, 14, 0, 0, 0) },
                    { 2, null, "Math Club", 2, new TimeSpan(0, 12, 0, 0, 0), new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/event2", "MATH101", 0, 2, new TimeSpan(0, 10, 0, 0, 0) },
                    { 3, null, "Physics Club", 3, new TimeSpan(0, 11, 0, 0, 0), new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/event3", "BIOL102", 0, 3, new TimeSpan(0, 9, 0, 0, 0) },
                    { 4, null, "Biology Club", 4, new TimeSpan(0, 15, 0, 0, 0), new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/event4", "BIOL102", 0, 4, new TimeSpan(0, 13, 0, 0, 0) },
                    { 5, null, "Chemistry Club", 5, new TimeSpan(0, 13, 0, 0, 0), new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/event5", "CHEM301", 0, 5, new TimeSpan(0, 11, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "ClubMembership",
                columns: new[] { "ClubMembershipId", "ClubId", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 4 },
                    { 5, 3, 5 },
                    { 6, 3, 6 },
                    { 7, 4, 7 },
                    { 8, 4, 8 },
                    { 9, 5, 9 },
                    { 10, 5, 10 }
                });

            migrationBuilder.InsertData(
                table: "ExamSession",
                columns: new[] { "ExamSessionId", "DayOfWeek", "EndTime", "ExamDate", "ExamId", "RoomId", "StartTime" },
                values: new object[,]
                {
                    { 1, "Monday", new TimeSpan(0, 11, 0, 0, 0), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new TimeSpan(0, 9, 0, 0, 0) },
                    { 2, "Tuesday", new TimeSpan(0, 15, 0, 0, 0), new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, new TimeSpan(0, 13, 0, 0, 0) },
                    { 3, "Wednesday", new TimeSpan(0, 12, 0, 0, 0), new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, new TimeSpan(0, 10, 0, 0, 0) },
                    { 4, "Thursday", new TimeSpan(0, 16, 0, 0, 0), new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, new TimeSpan(0, 14, 0, 0, 0) },
                    { 5, "Friday", new TimeSpan(0, 10, 30, 0, 0), new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, new TimeSpan(0, 8, 30, 0, 0) },
                    { 6, "Saturday", new TimeSpan(0, 13, 0, 0, 0), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 6, new TimeSpan(0, 11, 0, 0, 0) },
                    { 7, "Sunday", new TimeSpan(0, 17, 0, 0, 0), new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 7, new TimeSpan(0, 15, 0, 0, 0) },
                    { 8, "Monday", new TimeSpan(0, 11, 30, 0, 0), new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 8, new TimeSpan(0, 9, 30, 0, 0) },
                    { 9, "Tuesday", new TimeSpan(0, 16, 30, 0, 0), new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 9, new TimeSpan(0, 14, 30, 0, 0) },
                    { 10, "Wednesday", new TimeSpan(0, 14, 0, 0, 0), new DateTime(2024, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 10, new TimeSpan(0, 12, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Club_PresidentId",
                table: "Club",
                column: "PresidentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMembership_ClubId",
                table: "ClubMembership",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMembership_StudentId",
                table: "ClubMembership",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_LectureCode",
                table: "Enrollment",
                column: "LectureCode");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentId",
                table: "Enrollment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_LectureCode",
                table: "Exam",
                column: "LectureCode");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSession_ExamId",
                table: "ExamSession",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSession_RoomId",
                table: "ExamSession",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_DepartmentId",
                table: "Instructor",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorPreference_InstructorId",
                table: "InstructorPreference",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorPreference_LectureCode",
                table: "InstructorPreference",
                column: "LectureCode");

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_DepartmentId",
                table: "Lecture",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSession_InstructorId",
                table: "LectureSession",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSession_LectureCode",
                table: "LectureSession",
                column: "LectureCode");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSession_RoomId",
                table: "LectureSession",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_LectureCode",
                table: "Notification",
                column: "LectureCode");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRecipient_NotificationId",
                table: "NotificationRecipient",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRecipient_StudentId",
                table: "NotificationRecipient",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_RoomId",
                table: "Request",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ClubId",
                table: "Reservation",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_LectureCode",
                table: "Reservation",
                column: "LectureCode");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RoomId",
                table: "Reservation",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_BuildingId",
                table: "Room",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_DepartmentId",
                table: "Room",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_DepartmentId",
                table: "Student",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubMembership");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "ExamSession");

            migrationBuilder.DropTable(
                name: "InstructorPreference");

            migrationBuilder.DropTable(
                name: "LectureSession");

            migrationBuilder.DropTable(
                name: "NotificationRecipient");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Lecture");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
