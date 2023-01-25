using DipApi.Models;

namespace DipApi.Services
{
	public interface ICandidateService
	{
		Guid CreateCandidate(Candidate candidate);
	}
	
	public class CandidateService : ICandidateService
	{
		public Guid CreateCandidate(Candidate candidate)
		{
			return new Guid();
		}
	}
}
