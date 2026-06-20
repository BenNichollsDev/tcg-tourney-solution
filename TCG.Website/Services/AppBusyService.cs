
//
// Program: Local Games Store Management System
// Filename: AppBusyService.cs
// Author: Benjamin Nicholls
// Course: BSc Software Engineering (Hons)
// Module: CSY4022 - Computing Project Dissertation
// Module Leader: Amir Minai
// Supervisor: Mark Johnson
//
// Date: 20/06/2026
//
// Disclaimer: The following source code is the sole work of the author unless otherwise stated.
// Copyright (C) Benjamin Nicholls. All Rights Reserved.
//

using System;

namespace TCG.Website.Services
{
    public class AppBusyService
    {
        public bool IsReading { get; private set; }
        public bool IsWriting { get; private set; }

        public bool IsBusy => IsReading || IsWriting;

        public event Action? OnChange;

        public void StartRead()
        {
            IsReading = true;
            NotifyStateChanged();
        }

        public void StopRead()
        {
            IsReading = false;
            NotifyStateChanged();
        }

        public void StartWrite()
        {
            IsWriting = true;
            NotifyStateChanged();
        }

        public void StopWrite()
        {
            IsWriting = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
