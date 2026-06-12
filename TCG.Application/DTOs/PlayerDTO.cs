namespace TCG.Application.Dtos
{
    public class PlayerDto
    {
        public int PlayerId { get; set; }

        public string PlayerFirstName { get; set; } = string.Empty;

        public string PlayerLastName { get; set; } = string.Empty;

        public string PlayerEmail { get; set; } = string.Empty;

        public string PlayerPhone { get; set; } = string.Empty;

        public int PlayerAge { get; set; }

        public string PlayerGender { get; set; } = string.Empty;
    }
}
