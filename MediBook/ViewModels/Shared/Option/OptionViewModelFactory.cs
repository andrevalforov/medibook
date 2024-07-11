//using Magicalizer.Data.Repositories.Abstractions;
//using Magicalizer.Filters.Abstractions;
//using Microsoft.AspNetCore.Http;
//using Platformus;
//using MediBook.Data.Abstractions;
//using MediBook.Data.Entities;
//using MediBook.Data.Entities.Filters;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Threading.Tasks;

//namespace MediBook.ViewModels.Shared
//{
//  public static class OptionsViewModelFactory
//  {
//    public static async Task<UserOptions> CreateUserOptions(Organization organization, HttpContext httpContext)
//    {
//      IRepository<int, Organization, OrganizationFilter> organizationRepository = httpContext.GetStorage().GetRepository<int, Organization, OrganizationFilter>();
//      IRepository<int, UserPosition, UserPositionFilter> userPositionRepository = httpContext.GetStorage().GetRepository<int, UserPosition, UserPositionFilter>();
//      string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

//      IEnumerable<City> cities = await httpContext.GetStorage().GetRepository<int, City, IFilter>().GetAllAsync(inclusions: new Inclusion<City>(c => c.Name.Localizations));

//      var cityOptions = cities.Select(c => Create(c, culture)).OrderBy(c => c.Name);
//      int cityId = cityOptions.FirstOrDefault().Id;

//      City city = organization?.City ?? cities.FirstOrDefault(c => c.Id == cityId);
//      IEnumerable<Organization> organizations = await organizationRepository.GetAllAsync(new OrganizationFilter { CityId = city.Id }, inclusions: new Inclusion<Organization>(o => o.Name.Localizations));

//      organization ??= organizations.FirstOrDefault();

//      IEnumerable<UserPosition> positions = organizations.Any()
//        ? await userPositionRepository.GetAllAsync(new UserPositionFilter { OrganizationType = organization.Type }, inclusions: new Inclusion<UserPosition>(p => p.Name.Localizations))
//        : await userPositionRepository.GetAllAsync(inclusions: new Inclusion<UserPosition>(p => p.Name.Localizations));

//      return new UserOptions
//      {
//        Cities = cityOptions,
//        Organizations = organizations.Select(o => Create(o, culture)).OrderBy(o => o.Name),
//        UserPositions = positions.Select(p => Create(p, culture)).OrderBy(p => p.Name)
//      };
//    }

//    public static OptionViewModel Create(UserPosition userPosition, string cultureId)
//    {
//      if (userPosition == null)
//        return new OptionViewModel { Name = string.Empty };

//      return new OptionViewModel
//      {
//        Id = userPosition.Id,
//        Name = userPosition.Name?.Localizations?.FirstOrDefault(l => l.CultureId == cultureId)?.Value ?? string.Empty
//      };
//    }

//    public static OptionViewModel Create(City city, string cultureId)
//    {
//      if (city == null)
//        return new OptionViewModel { Name = string.Empty };

//      return new OptionViewModel
//      {
//        Id = city.Id,
//        Name = city.Name?.Localizations?.FirstOrDefault(l => l.CultureId == cultureId)?.Value ?? string.Empty
//      };
//    }

//    public static OptionViewModel Create(Organization organization, string cultureId)
//    {
//      if (organization == null)
//        return new OptionViewModel { Name = string.Empty };

//      return new OptionViewModel
//      {
//        Id = organization.Id,
//        Name = organization.Name?.Localizations?.FirstOrDefault(l => l.CultureId == cultureId)?.Value ?? string.Empty
//      };
//    }
//  }

//  public class UserOptions
//  {
//    public IEnumerable<OptionViewModel> Cities { get; set; }
//    public IEnumerable<OptionViewModel> Organizations { get; set; }
//    public IEnumerable<OptionViewModel> UserPositions { get; set; }
//  }
//}
