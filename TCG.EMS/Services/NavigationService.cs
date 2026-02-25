using Microsoft.AspNetCore.Components;
using TCG.EMS.Interfaces;

namespace TCG.EMS.Services;

public class NavigationService : INavigationService
{
    private readonly NavigationManager _nav;

    public NavigationService(NavigationManager nav)
    {
        _nav = nav;
    }

    public void GoHome(string routeParams="")
    {
        _nav.NavigateTo($"/home{routeParams}");
    }

    public void GoTourney(string routeParams="")
    {
        _nav.NavigateTo($"/tournaments{routeParams}");
    }

    public void GoTourneyCreate(string routeParams="")
    {
        _nav.NavigateTo($"/tournaments/new{routeParams}");
    }
}