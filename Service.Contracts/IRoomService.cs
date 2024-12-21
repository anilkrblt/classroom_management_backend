using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IRoomService
    {

        Task<IEnumerable<RoomDto>> GetAllRoomsAsync(bool trackChanges);
        Task<RoomDto> GetRoomByIdAsync(int roomId, bool trackChanges);
        Task<IEnumerable<RoomDto>> GetRoomsByDepartmentId(int departmentId, bool trackChanges);

        Task DeleteRoomForBuildingAsync(int roomId, int buildingId, bool trackChanges);

        Task UpdateRoomForBuilding(int buildingId, int id,
                                   RoomDto roomForUpdate,
                                   bool compTrackChanges, bool empTrackChanges);
        Task<RoomDto> CreateRoomForBuilding(int buildingId,
                                             RoomDto roomForCreation,
                                             bool trackChanges);

        Task<IEnumerable<LectureHallDto>> GetAllLectureHallsAsync(bool trackChanges);
        Task<LectureHallDto> CreateLectureHallForRoom(int roomId,
                                            LectureHallDto lectureHallForCreation,
                                            bool trackChanges);

        Task<IEnumerable<LabDto>> GetAllLabsAsync(bool trackChanges);

        Task UpdateLabForRoom(int roomId,
        LabDto labForUpdate, bool labTrackChanges);
        Task<IEnumerable<ClassDto>> GetAllClassesAsync(bool trackChanges);
        Task UpdateClassForRoom(int roomId,
        ClassDto classForUpdate, bool classTrackChanges);


    }
}