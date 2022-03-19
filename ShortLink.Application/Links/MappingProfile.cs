namespace ShortLink.Application.Links;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile() : base()
    {
        CreateMap<Commands.CreateLinkCommand, Domain.Models.Link>();
        CreateMap<Domain.Models.Link, Commands.CreateLinkCommand>();
    }
}
