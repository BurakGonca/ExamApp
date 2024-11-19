using BAExamApp.Dtos.TrainerClassrooms;
using System;
using System.Collections.Generic;

namespace BAExamApp.Dtos.Trainers
{
    public class TrainerDetailsDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public byte[]? NewImage { get; set; }
        //public string CityName { get; set; }
        public string IdentityId { get; set; }
        public List<TrainerClassroomListForTrainerDetailsDto> TrainerClassrooms { get; set; }
    }
}
