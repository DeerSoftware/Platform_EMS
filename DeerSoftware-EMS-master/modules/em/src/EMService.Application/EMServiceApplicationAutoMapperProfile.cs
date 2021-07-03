using AutoMapper;
using AutoMapper.Configuration;
using EMService.AssetTree;
using EMService.AssetTree.Dto;

namespace EMService
{
    public class EMServiceApplicationAutoMapperProfile : Profile
    {
        public EMServiceApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            #region AssetTree

            CreateMap<Foundation, FoundationDto>();
            CreateMap<FoundationDto, Foundation>();

            CreateMap<(Foundation, Device), DeviceDto>()
                 .ForMember(u => u.Id, options => options.MapFrom(input => input.Item1.Id))
                 .ForMember(u => u.ParentId, options => options.MapFrom(input => input.Item1.ParentId))
                 .ForMember(u => u.NodeId, options => options.MapFrom(input => input.Item1.NodeId))
                 .ForMember(u => u.DeviceType, options => options.MapFrom(input => input.Item1.DeviceType))
                 .ForMember(u => u.Code, options => options.MapFrom(input => input.Item1.Code))
                 .ForMember(u => u.Name, options => options.MapFrom(input => input.Item1.Name))
                 .ForMember(u => u.TreeArea, options => options.MapFrom(input => input.Item1.TreeArea))
                 .ForMember(u => u.JianPin, options => options.MapFrom(input => input.Item1.JianPin))
                 .ForMember(u => u.Sort, options => options.MapFrom(input => input.Item1.Sort))
                 .ForMember(u => u.LocationCode, options => options.MapFrom(input => input.Item2.LocationCode))
                 .ForMember(u => u.ErpCode, options => options.MapFrom(input => input.Item2.ErpCode))
                 .ForMember(u => u.ControlLevel, options => options.MapFrom(input => input.Item2.ControlLevel))
                 .ForMember(u => u.Specialty, options => options.MapFrom(input => input.Item2.Specialty))
                 .ForMember(u => u.DeviceCategory, options => options.MapFrom(input => input.Item2.DeviceCategory))
                 .ForMember(u => u.DeviceKind, options => options.MapFrom(input => input.Item2.DeviceKind))
                 .ForMember(u => u.Profession, options => options.MapFrom(input => input.Item2.Profession))
                 .ForMember(u => u.Spec, options => options.MapFrom(input => input.Item2.Spec))
                 .ForMember(u => u.Model, options => options.MapFrom(input => input.Item2.Model))
                 .ForMember(u => u.MeasureUnit, options => options.MapFrom(input => input.Item2.MeasureUnit))
                 .ForMember(u => u.ResponsibleUserId, options => options.MapFrom(input => input.Item2.ResponsibleUserId))
                 .ForMember(u => u.ResponsibleEngineer, options => options.MapFrom(input => input.Item2.ResponsibleEngineer))
                 .ForMember(u => u.UsedState, options => options.MapFrom(input => input.Item2.UsedState))
                 .ForMember(u => u.Description, options => options.MapFrom(input => input.Item2.Description));

            CreateMap<DeviceDto, Device>();

            CreateMap<(Foundation, Point), PointDto>()
               .ForMember(u => u.Id, options => options.MapFrom(input => input.Item1.Id))
               .ForMember(u => u.ParentId, options => options.MapFrom(input => input.Item1.ParentId))
               .ForMember(u => u.NodeId, options => options.MapFrom(input => input.Item1.NodeId))
               .ForMember(u => u.DeviceType, options => options.MapFrom(input => input.Item1.DeviceType))
               .ForMember(u => u.Code, options => options.MapFrom(input => input.Item1.Code))
               .ForMember(u => u.Name, options => options.MapFrom(input => input.Item1.Name))
               .ForMember(u => u.TreeArea, options => options.MapFrom(input => input.Item1.TreeArea))
               .ForMember(u => u.JianPin, options => options.MapFrom(input => input.Item1.JianPin))
               .ForMember(u => u.Sort, options => options.MapFrom(input => input.Item1.Sort))
               .ForMember(u => u.FixedCode, options => options.MapFrom(input => input.Item2.FixedCode))
               .ForMember(u => u.ControlLevel, options => options.MapFrom(input => input.Item2.ControlLevel))
               .ForMember(u => u.Specialty, options => options.MapFrom(input => input.Item2.Specialty))
               .ForMember(u => u.ProcessCode, options => options.MapFrom(input => input.Item2.ProcessCode))
               .ForMember(u => u.ElecTag, options => options.MapFrom(input => input.Item2.ElecTag))
               .ForMember(u => u.AccessMode, options => options.MapFrom(input => input.Item2.AccessMode))
               .ForMember(u => u.UnitType, options => options.MapFrom(input => input.Item2.UnitType))
               .ForMember(u => u.EngineeringUnit, options => options.MapFrom(input => input.Item2.EngineeringUnit))
               .ForMember(u => u.MeasWay, options => options.MapFrom(input => input.Item2.MeasWay))
               .ForMember(u => u.MeasureDirect, options => options.MapFrom(input => input.Item2.MeasureDirect));

            CreateMap<PointDto, Point>();

            CreateMap<PopMenu, PopMenuDto>();
            CreateMap<PopMenuDto, PopMenu>();

            CreateMap<CreateOrganizationDto, Organization>();
            CreateMap<Organization, OrganizationDto>();
            CreateMap<DeviceSystem, DevSystemDto>();

            #endregion
        }
    }
}
