using OnlineBanking.Domain.Services;

namespace OnlineBanking.Application.Services
{
    public class FundsTransferAppService
    {
        private readonly FundsTransferDomainService _fundsTransferDomainService;

        public FundsTransferAppService()
        { 
            _fundsTransferDomainService = new FundsTransferDomainService();
        }
        
        public void TransferFunds(int sourceBankAccountId, int destBankAccountId, decimal amount)
        {
            _fundsTransferDomainService.TransferFunds(sourceBankAccountId, destBankAccountId, amount);
        }
    }
}