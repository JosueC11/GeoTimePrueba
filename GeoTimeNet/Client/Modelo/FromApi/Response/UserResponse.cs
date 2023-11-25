namespace GeotimeNet.Client.Modelo.FromApi.Response
{
    public class UserResponse
    {

        public string? User { get; set; }
        public string? Token { get; set; }

        public UserResponse()
        {
            User = "";
            Token = "";
        }
    }
}
