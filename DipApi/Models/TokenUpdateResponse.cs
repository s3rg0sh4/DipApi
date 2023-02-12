namespace DipApi.Models
{
	public class TokenUpdateResponse
	{
		public string Token { get; set; }
		public string RefreshToken { get; set; }
		
		public TokenUpdateResponse(string token, string refreshToken) 
		{
			Token = token;
			RefreshToken = refreshToken;
		}
	}
}
