using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Shared.DataTransferObjects
{
    public record ReservationDto
    {
        public int ReservationId { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int ApprovedBy { get; set; }

        public string? Description { get; set; }

        public int RoomId { get; set; }

    }


    public record ClubReservationGetDto
    {

        public int ReservationId { get; set; }
        public string ClubName { get; set; }
        public string ClubLogo { get; set; }
        public string ClubRoomName { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string KatilimLinki { get; set; }
        public string Banner { get; set; }
        public string FullName { get; set; }
        public int StudentNo { get; set; }
        public string Status { get; set; }

    }

    public record LectureReservationCreateDto
    {
        public int InstructorId { get; set; }
        public string LectureCode { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime EventDate { get; set; }
        public string RoomName { get; set; }
    }
    public record LectureReservationUpdateDto
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime EventDate { get; set; }
        public string RoomName { get; set; }
    }


    public record ClubReservationPostDto
    {
        public int StudentId { get; set; }
        public string ClubName { get; set; }
        public string RoomName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime EventTime { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Link { get; set; }
        public string Status { get; set; }


        // Banner artık doğrudan dosya (resim) gelecek
        public IFormFile BannerFile { get; set; }
    }

    public record ClubReservationDto
    {
        public int StudentId { get; set; }
        public string ClubName { get; set; }
        public string RoomName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime EventTime { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Link { get; set; }
        public string Status { get; set; }
        public string BannerPath { get; set; }
    }








}