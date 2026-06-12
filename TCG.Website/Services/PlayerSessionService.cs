namespace TCG.Website.Services
{
    // Stores user login
    public class PlayerSessionService
    {
        public int? PlayerId { get; set; }

        public string? PlayerName { get; set; }

        public bool IsLoggedIn => PlayerId.HasValue;

        public void Clear()
        {
            PlayerId = null;
            PlayerName = null;
        }
    }
}
