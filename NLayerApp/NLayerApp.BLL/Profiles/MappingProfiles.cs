using AutoMapper;
using NLayerApp.BLL.DTO;
using NLayerApp.DAL.Entities;

namespace NLayerApp.BLL.Profiles
{
	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
			CreateMap<Departments, DepartmentsDTO>().ReverseMap();
            CreateMap<Doctors, DoctorsDTO>().ReverseMap();
			CreateMap<Patients, PatientsDTO>().ReverseMap();
		}
	}
}

