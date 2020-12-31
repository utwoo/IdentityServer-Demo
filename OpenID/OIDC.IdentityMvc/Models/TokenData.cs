namespace OIDC.IdentityMvc.Models
{
    public class TokenData
    {
        public string code { get; set; }
        public string id_token { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
        public string session_state { get; set; }
    }
}