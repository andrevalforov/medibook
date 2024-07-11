using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Backend.Metadata;
using MediBook.Backend;

namespace MediBook
{
  public class Metadata : MetadataBase
  {
    public override IEnumerable<StyleSheet> GetStyleSheets(HttpContext httpContext)
    {
      return new StyleSheet[]
      {
        new StyleSheet("/wwwroot.areas.backend.css.supervision.min.css", 10000)
      };
    }

    public override IEnumerable<Script> GetScripts(HttpContext httpContext)
    {
      return new Script[]
      {
        new Script("//cdnjs.cloudflare.com/ajax/libs/tinymce/4.9.2/tinymce.min.js", 6000),
        new Script("/wwwroot.areas.backend.js.supervision.min.js", 10000)
      };
    }

    public override IEnumerable<MenuGroup> GetMenuGroups(HttpContext httpContext)
    {
      return new MenuGroup[]
      {
        new MenuGroup(
          "MediBook",
          0,
          new MenuItem[]
          {
            new MenuItem(null, "/backend/specializations", "Спеціальності", 2000, new string[] { Permissions.BrowseTopics }),
            new MenuItem(null, "/backend/doctors", "Лікарі", 3000, new string[] { Permissions.BrowseSupervisors }),
            new MenuItem(null, "/backend/patients", "Пацієнти", 4000, new string[] { Permissions.BrowseSupervisees }),
            new MenuItem(null, "/backend/consultations", "Консультації", 5000, new string[] { Permissions.BrowseSupervisions }),
            new MenuItem(null, "/backend/regions", "Області", 5500, new string[] { Permissions.BrowseRegions }),
            new MenuItem(null, "/backend/cities", "Міста", 6000, new string[] { Permissions.BrowseCities }),
            new MenuItem(null, "/backend/organizations", "Організації", 7000, new string[] { Permissions.BrowseOrganizations }),
          }
        )
      };
    }
  }
}