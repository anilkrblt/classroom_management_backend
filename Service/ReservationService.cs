using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using NLog;
using Entities.Exceptions;

namespace Service
{
    public class ReservationService : IReservationService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public ReservationService(IRepositoryManager repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // Reservation Methods
        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync(bool trackChanges)
        {
            var reservations = await _repository.Reservation.GetAllReservationsAsync(trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> GetReservationAsync(int reservationId, bool trackChanges)
        {
            var reservation = await _repository.Reservation.GetReservationAsync(reservationId, trackChanges);
            if (reservation == null)
                throw new ReservationNotFoundException(reservationId);

            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<IEnumerable<ReservationDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            var reservations = await _repository.Reservation.GetByIdsAsync(ids, trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public IEnumerable<ReservationDto> GetReservationsByRoomId(int roomId, bool trackChanges)
        {
            var reservations = _repository.Reservation.GetReservationsByRoomIdAsync(roomId, trackChanges);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }


        public async Task DeleteReservation(int roomId, int reservationId, bool trackChanges)
        {
            var room = await _repository.Room.GetRoomAsync(roomId, trackChanges);
            if (room == null)
                throw new RoomNotFoundException(roomId);

            var reservation = await _repository.Reservation.GetReservationAsync(roomId, trackChanges);
            if (reservation == null)
                throw new ReservationNotFoundException(reservationId);

            _repository.Reservation.DeleteReservation(reservation);
            await _repository.SaveAsync();
        }

        public async Task<ReservationDto> CreateReservationForRoomAsync(ReservationDto reservationDto, int roomId)
        {
            var reservationEntity = _mapper.Map<Reservation>(reservationDto);
            reservationEntity.RoomId = roomId;

            _repository.Reservation.CreateReservationForRoom(roomId, reservationEntity);
            await _repository.SaveAsync();

            return _mapper.Map<ReservationDto>(reservationEntity);
        }

        public async Task UpdateReservationAsync(int reservationId, ReservationDto reservationForUpdate, bool trackChanges)
        {
            var reservation = await _repository.Reservation.GetReservationAsync(reservationId, trackChanges);
            if (reservation == null)
                throw new ReservationNotFoundException(reservationId);

            _mapper.Map(reservationForUpdate, reservation);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<ClubReservationDto>> GetAllClubReservationsAsync(bool trackChanges)
        {
            var clubReservations = await _repository.ClubReservation.GetAllClubReservationsAsync(trackChanges);
            return _mapper.Map<IEnumerable<ClubReservationDto>>(clubReservations);
        }

        public async Task<ClubReservationDto> GetClubReservationByReservationIdAsync(int reservationId, bool trackChanges)
        {
            var clubReservation = await _repository.ClubReservation.GetClubReservationAsync(reservationId, trackChanges);
            if (clubReservation == null)
                throw new ArgumentException("Club Reservation not found.");

            return _mapper.Map<ClubReservationDto>(clubReservation);
        }


        public async Task DeleteClubReservationForReservationAsync(int reservationId, bool trackChanges)
        {
            var clubReservation = await _repository.ClubReservation.GetClubReservationAsync(reservationId, trackChanges);
            if (clubReservation == null)
                throw new ArgumentException("Club Reservation not found.");

            _repository.ClubReservation.DeleteClubReservation(clubReservation);
            await _repository.SaveAsync();
        }

        public async Task<ClubReservationDto> CreateClubReservationForReservation(int reservationId, ClubReservationDto clubReservationForCreation, bool trackChanges)
        {
            var clubReservationEntity = _mapper.Map<ClubReservation>(clubReservationForCreation);
            clubReservationEntity.ReservationId = reservationId;

            _repository.ClubReservation.CreateClubReservation(clubReservationEntity);
            await _repository.SaveAsync();

            return _mapper.Map<ClubReservationDto>(clubReservationEntity);
        }

        public async Task<IEnumerable<LectureReservationDto>> GetAllLectureReservationsAsync(bool trackChanges)
        {
            var lectureReservations = await _repository.LectureReservation.GetAllLectureReservationsAsync(trackChanges);
            return _mapper.Map<IEnumerable<LectureReservationDto>>(lectureReservations);
        }

        public async Task<LectureReservationDto> GetLectureReservationByReservationIdAsync(int reservationId, bool trackChanges)
        {
            var lectureReservation = await _repository.LectureReservation.GetLectureReservationAsync(reservationId, trackChanges);
            if (lectureReservation == null)
                throw new ArgumentException("Lecture Reservation not found.");

            return _mapper.Map<LectureReservationDto>(lectureReservation);
        }

        public async Task DeleteLectureReservationForReservationAsync(int reservationId, bool trackChanges)
        {
            var lectureReservation = await _repository.LectureReservation.GetLectureReservationAsync(reservationId, trackChanges);
            if (lectureReservation == null)
                throw new ArgumentException("Lecture Reservation not found.");

            _repository.LectureReservation.DeleteLectureReservation(lectureReservation.ReservationId);
            await _repository.SaveAsync();
        }

                /*
                public async Task<LectureReservationDto> CreateLectureReservationForReservation(int reservationId, LectureReservationDto lectureReservationForCreation, bool trackChanges)
                {
                    var lectureReservationEntity = _mapper.Map<LectureReservation>(lectureReservationForCreation);
                    lectureReservationEntity.ReservationId = reservationId;


                    _repository.LectureReservation.CreateLectureReservation(lectureReservationEntity);
                    await _repository.SaveAsync();

                    return _mapper.Map<LectureReservationDto>(lectureReservationEntity);
                }
                */
    }
}
