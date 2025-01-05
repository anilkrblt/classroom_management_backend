using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;


namespace Service
{
    public class ClubService : IClubService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;


        public ClubService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all clubs
        public async Task<IEnumerable<ClubDto>> GetAllClubsAsync(bool trackChanges)
        {
            var clubs = await _repositoryManager.Club.GetAllClubsAsync(trackChanges);

            var clubDtos = clubs.Select(club =>
            {
                var managerMembership = club.ClubMemberships.FirstOrDefault(m => m.IsClubManager);
                
                return new ClubDto
                {
                    ClubId = club.ClubId,
                    ClubLogo = club.ClubLogoPath,
                    ClubShorcut = club.NameShortcut,
                    ClubName = club.Name,
                    ClubManager = managerMembership?.Student.FullName ?? "N/A", 
                    ClubManagerId = managerMembership?.Student.StudentId 
                };
            });

            return clubDtos;
        }



        // Get a specific club by ID
        public async Task<ClubDto> GetClubByIdAsync(int clubId, bool trackChanges)
        {
            var club = await _repositoryManager.Club.GetClubAsync(clubId, trackChanges);

            if (club == null)
                throw new KeyNotFoundException($"Club with ID {clubId} not found.");

            return _mapper.Map<ClubDto>(club);
        }

        // Get members of a specific club
        public async Task<IEnumerable<StudentDto>> GetClubMembersAsync(int clubId, bool trackChanges)
        {
            var club = await _repositoryManager.Club.GetClubAsync(clubId, trackChanges);

            if (club == null)
                throw new KeyNotFoundException($"Club with ID {clubId} not found.");

            var members = await _repositoryManager.ClubMembership.GetAllClubMembershipsAsync(trackChanges);
            var studentIds = members.Where(m => m.ClubId == clubId).Select(m => m.StudentId);

            var students = await _repositoryManager.Student.GetAllStudentsAsync(trackChanges);
            var clubMembers = students.Where(s => studentIds.Contains(s.StudentId));

            return _mapper.Map<IEnumerable<StudentDto>>(clubMembers);
        }

        // Update club manager (president)
        public async Task UpdateClubManagerAsync(int clubId, int studentId)
        {
            var club = await _repositoryManager.Club.GetClubAsync(clubId, true);

            if (club == null)
                throw new KeyNotFoundException($"Club with ID {clubId} not found.");

            var student = await _repositoryManager.Student.GetStudentAsync(studentId, false);
            if (student == null)
                throw new KeyNotFoundException($"Student with ID {studentId} not found.");

            await _repositoryManager.SaveAsync();
        }

        public async Task CreateClubAsync(ClubDto clubDto)
        {
            var club = _mapper.Map<Club>(clubDto);
            _repositoryManager.Club.CreateClub(club);
            await _repositoryManager.SaveAsync();
        }

        // Update an existing club
        public async Task UpdateClubAsync(int clubId, ClubDto clubDto)
        {
            var club = await _repositoryManager.Club.GetClubAsync(clubId, true);

            if (club == null)
                throw new KeyNotFoundException($"Club with ID {clubId} not found.");

            _mapper.Map(clubDto, club);
            await _repositoryManager.SaveAsync();
        }

        // Delete a club
        public async Task DeleteClubAsync(int clubId)
        {
            var club = await _repositoryManager.Club.GetClubAsync(clubId, true);

            if (club == null)
                throw new KeyNotFoundException($"Club with ID {clubId} not found.");

            await _repositoryManager.Club.DeleteClubAsync(club);
            await _repositoryManager.SaveAsync();
        }

        public Task<IEnumerable<ClubReservationGetDto>> GetAllClubReservations(bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
