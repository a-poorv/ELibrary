using System.Linq;
namespace E_Library.Models
{
    public class AccountRepo:IAccount
    {

        private readonly LMsSystemContext _lmsContext;
        public AccountRepo(LMsSystemContext lmsContext)
        {
            _lmsContext = lmsContext;
        }

        Account IAccount.getuserByname(string username)
        {
            return _lmsContext.Accounts.FirstOrDefault(u => u.UserName == username);
        }
    }
}
