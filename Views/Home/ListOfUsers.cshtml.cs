using System.Collections;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProductsCatalog.Client.Views.Home;

public class ListOfUsers : PageModel, IEnumerable
{
    public void OnGet()
    {

    }

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}