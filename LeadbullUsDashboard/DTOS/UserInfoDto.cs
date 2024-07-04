namespace Api.DTOS
{
    public class UserInfoDto
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> serviceProfiles { get; set; }
    }
}
