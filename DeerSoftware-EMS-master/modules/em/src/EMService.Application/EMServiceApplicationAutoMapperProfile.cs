using AutoMapper;

namespace EMService
{
    public class EMServiceApplicationAutoMapperProfile : Profile
    {
        public EMServiceApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<CreateOrganizationDto, Organization>();
            CreateMap<Organization, OrganizationDto>();
        }
    }
}
