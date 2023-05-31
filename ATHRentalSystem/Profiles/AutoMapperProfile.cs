using ATHRentalSystem.Models;
using AutoMapper;

namespace ATHRentalSystem.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TypPojazduViewModelDT, TypPojazduViewModel>();
            CreateMap<TypPojazduViewModel, TypPojazduViewModelDT>();

            CreateMap<PojazdViewModelDT, PojazdViewModel>();
            CreateMap<PojazdViewModel, PojazdViewModelDT>();

            CreateMap<RezerwacjeViewModelDT, RezerwacjeViewModel>();
            CreateMap<RezerwacjeViewModel, RezerwacjeViewModelDT>();

            CreateMap<PunktWypozyczenViewModelDT, PunktWypozyczenViewModel>();
            CreateMap<PunktWypozyczenViewModel, PunktWypozyczenViewModelDT>();

        }
    }
}
