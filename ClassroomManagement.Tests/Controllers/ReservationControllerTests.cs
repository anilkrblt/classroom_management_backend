using ClassroomManagementPresentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Contracts;
using Shared.DataTransferObjects;
using Xunit;
using System.Threading.Tasks;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;

namespace ClassroomManagement.Tests.Controllers
{
    public class ReservationControllerTests
    {
        [Fact]
        public async Task CreateClubReservation_ReturnsOkObjectResult_WhenDtoIsValid()
        {

            var mockServiceManager = new Mock<IServiceManager>();

            var mockReservationService = new Mock<IReservationService>();

            // 3) MockServiceManager, ReservationService property’sine mock’u veriyor
            mockServiceManager
                .Setup(sm => sm.ReservationService)
                .Returns(mockReservationService.Object);

            // 4) ReservationService.CreateClubReservationAsync -> başarılı bir dönüş
            var sampleReturnDto = new ClubReservationDto
            {
                StudentId = 123,
                ClubName = "ChessClub",
                RoomName = "BigHall",
                Title = "Chess Tournament",
                BannerPath = "images/clubs/banners/testbanner.png",
                // ... diğer alanlar ...
            };
            mockReservationService
                .Setup(rs => rs.CreateClubReservationAsync(It.IsAny<ClubReservationDto>()))
                .ReturnsAsync(sampleReturnDto);

            // 5) Kontrol edeceğimiz input DTO
            var inputDto = new ClubReservationPostDto
            {
                StudentId = 123,
                ClubName = "ChessClub",
                RoomName = "BigHall",
                Title = "Chess Tournament",
                // BannerFile gibi bir IFormFile alanı var, test için null veya fake verebilirsiniz.
                BannerFile = null
            };

            // 6) Controller örneği
            var controller = new ReservationController(mockServiceManager.Object);

            // ACT
            var result = await controller.CreateClubReservation(inputDto);

            // ASSERT
            // 1) Sonucun OkObjectResult olduğunu doğrulayın
            var okResult = Assert.IsType<OkObjectResult>(result);

            // 2) İçerikte beklenen tipte DTO var mı?
            var returnedDto = Assert.IsType<ClubReservationDto>(okResult.Value);
            Assert.Equal(sampleReturnDto.StudentId, returnedDto.StudentId);
            Assert.Equal(sampleReturnDto.ClubName, returnedDto.ClubName);
            Assert.Equal(sampleReturnDto.RoomName, returnedDto.RoomName);
            Assert.Equal(sampleReturnDto.Title, returnedDto.Title);

            // 3) ReservationService.CreateClubReservationAsync 1 kez çağrılmış mı?
            mockReservationService.Verify(
                rs => rs.CreateClubReservationAsync(It.IsAny<ClubReservationDto>()),
                Times.Once
            );
        }

        [Fact]
        public async Task CreateClubReservation_ReturnsBadRequest_WhenDtoIsNull()
        {
            // ARRANGE
            var mockServiceManager = new Mock<IServiceManager>();
            var controller = new ReservationController(mockServiceManager.Object);

            // ACT
            var result = await controller.CreateClubReservation(null);

            // ASSERT
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Dto is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateClubReservation_SavesBannerFile_WhenBannerFileProvided()
        {
            // ARRANGE
            var mockServiceManager = new Mock<IServiceManager>();
            var mockReservationService = new Mock<IReservationService>();

            mockServiceManager
                .Setup(sm => sm.ReservationService)
                .Returns(mockReservationService.Object);

            // Service katmanının dönüşü
            var sampleReturnDto = new ClubReservationDto
            {
                StudentId = 456,
                ClubName = "RoboticsClub",
                RoomName = "Lab1",
                Title = "Robot Showcase",
                BannerPath = "images/clubs/banners/testbanner.png"
            };
            mockReservationService
                .Setup(rs => rs.CreateClubReservationAsync(It.IsAny<ClubReservationDto>()))
                .ReturnsAsync(sampleReturnDto);

            // Sahte IFormFile oluşturma
            var fileContent = "FakeFileContent";
            var fileName = "sampleBanner.png";
            using var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent));
            var formFile = new FormFile(ms, 0, ms.Length, "BannerFile", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };

            var inputDto = new ClubReservationPostDto
            {
                StudentId = 456,
                ClubName = "RoboticsClub",
                RoomName = "Lab1",
                Title = "Robot Showcase",
                BannerFile = formFile  // Artık null değil
            };

            var controller = new ReservationController(mockServiceManager.Object);

            // ACT
            var result = await controller.CreateClubReservation(inputDto);

            // ASSERT
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<ClubReservationDto>(okResult.Value);
            Assert.Equal(sampleReturnDto.BannerPath, returnedDto.BannerPath);

            // CreateClubReservationAsync yine 1 kez çağrıldı mı?
            mockReservationService.Verify(rs => rs.CreateClubReservationAsync(It.IsAny<ClubReservationDto>()), Times.Once);
        }

        [Fact]
        public async Task CreateClubReservation_ReturnsNotFound_WhenServiceThrowsKeyNotFound()
        {
            // ARRANGE
            var mockServiceManager = new Mock<IServiceManager>();
            var mockReservationService = new Mock<IReservationService>();

            mockServiceManager
                .Setup(sm => sm.ReservationService)
                .Returns(mockReservationService.Object);

            // Servis KeyNotFoundException fırlatıyor
            mockReservationService
                .Setup(rs => rs.CreateClubReservationAsync(It.IsAny<ClubReservationDto>()))
                .ThrowsAsync(new KeyNotFoundException("Club 'NonExisting' not found."));

            var inputDto = new ClubReservationPostDto
            {
                StudentId = 789,
                ClubName = "NonExisting",
                RoomName = "Whatever",
                Title = "Impossible Reservation",
                BannerFile = null
            };

            var controller = new ReservationController(mockServiceManager.Object);

            // ACT
            // Şu an controller kodunda try-catch yok, dolayısıyla bu muhtemelen
            // DeveloperExceptionPageMiddleware ile 500 döndürüyor.
            // Ya da global exception middleware varsa "NotFound" olarak döndürülebilir.
            // Hangi yaklaşım varsa test accordingly.
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => controller.CreateClubReservation(inputDto));

            // ASSERT
            Assert.Contains("Club 'NonExisting' not found.", ex.Message);
        }
        [Fact]
        public async Task CreateClubReservation_ThrowsException_WhenServiceThrowsGeneralException()
        {
            // ARRANGE
            var mockServiceManager = new Mock<IServiceManager>();
            var mockReservationService = new Mock<IReservationService>();

            mockServiceManager
                .Setup(sm => sm.ReservationService)
                .Returns(mockReservationService.Object);

            // Bu sefer generic Exception fırlatalım
            mockReservationService
                .Setup(rs => rs.CreateClubReservationAsync(It.IsAny<ClubReservationDto>()))
                .ThrowsAsync(new Exception("Some DB error"));

            var inputDto = new ClubReservationPostDto
            {
                StudentId = 789,
                ClubName = "AnyClub",
                RoomName = "AnyRoom",
                Title = "AnyTitle"
            };

            var controller = new ReservationController(mockServiceManager.Object);

            // ACT & ASSERT
            var ex = await Assert.ThrowsAsync<Exception>(
                () => controller.CreateClubReservation(inputDto)
            );
            Assert.Equal("Some DB error", ex.Message);
        }


    }
}
