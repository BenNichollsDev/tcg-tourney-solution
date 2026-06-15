//
// Program: Local Games Store Management System
// Filename: NavigationService.cs
// Author: Benjamin Nicholls
// Course: BSc Software Engineering (Hons)
// Module: CSY4022 - Computing Project Dissertation
// Module Leader: Amir Minai
// Supervisor: Mark Johnson
//
// Date: 14/06/2026
//
// Disclaimer: The following source code is the sole work of the author unless otherwise stated.
// Copyright (C) Benjamin Nicholls. All Rights Reserved.
//
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

