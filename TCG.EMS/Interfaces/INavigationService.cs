namespace TCG.EMS.Interfaces;

public interface INavigationService
{
    void GoHome(string routeParams="");
    void GoTourney(string routeParams="");
    void GoTourneyCreate(string routeParams="");
}