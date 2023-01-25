using DipApi.Models;
using DipApi.Enums;

namespace DipApi.Services
{
	public interface IHiringApplicationService
	{
		HiringApplication CreateHiringApplication(Guid guidNaturalPerson, Guid candidate, Guid subdivision, DateTime dateOfApply, bool physical);
		public byte[] GetPrintingFormHiringApplication(Guid guidPrintingForm);
		public bool SendHiringApplicationSigningDirectumId(Guid guidApplicationDismissal);
		public bool SendHiringApplicationSigningDataDirectum(Guid guidHiringApplication, DateTime dateTimeSigning, int signingId, HiringApplicationSigningStatus signingStatus = HiringApplicationSigningStatus.Signing);
		public bool SendHiringApplicationStatus(Guid guidHiringApplication, HiringApplicationStatus status = HiringApplicationStatus.Approved);


	}

	public class HiringApplicationService : IHiringApplicationService
	{
		//Создать заявление о найме
		public HiringApplication CreateHiringApplication(Guid guidNaturalPerson, Guid candidate, Guid subdivision, DateTime dateOfApply, bool physical)
		{
			return new HiringApplication();
		}

		//Получить печатную форму заявления
		public byte[] GetPrintingFormHiringApplication(Guid guidPrintingForm)
		{
			return new byte[0];
		}

		//Послать id задачи согласования заявления в директуме
		public bool SendHiringApplicationSigningDirectumId(Guid guidApplicationDismissal)
		{
			return true;
		}

		//Послать данные о согласовании завляения в директуме
		public bool SendHiringApplicationSigningDataDirectum(Guid guidHiringApplication, DateTime dateTimeSigning, int signingId, HiringApplicationSigningStatus signingStatus = HiringApplicationSigningStatus.Signing)
		{
			return true;
		}

		//Послать статус согласования заявления в директуме
		public bool SendHiringApplicationStatus(Guid guidHiringApplication, HiringApplicationStatus status = HiringApplicationStatus.Approved)
		{
			return true;
		}
	}
}
