using OnlineBanking.Application.Services;

namespace OnlineBanking.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var fundsTransferAppService = new FundsTransferAppService();
            fundsTransferAppService.TransferFunds(1, 2, 50M);
        }
    }
}