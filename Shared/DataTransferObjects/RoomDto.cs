
namespace Shared.DataTransferObjects
{

    public record RoomDto
    {
        public int RoomId { get; set; }
        public string? Name { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }

    }


    public record RoomCreationForBuildingDto(string? Type,
                                             string? Content,
                                             string? Status,
                                             DateTime CreatedAt,
                                             DateTime UpdatedAt,
                                             int StudentId);

}