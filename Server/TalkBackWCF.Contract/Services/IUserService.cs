using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TalkBackWCF.Contract.Entities;

namespace TalkBackWCF.Contract
{
    [ServiceContract(CallbackContract =typeof(IUserCallback))]
    public interface IUserService
    {

        [OperationContract]
        SignUpResponse SignUp(SignUpRequest signUpRequest);
        [OperationContract]
        LoginResponse Login(LoginRequest loginRequest);
        [OperationContract]
        LogoutResponse Logout(LogoutRequest logoutRequest);

    }
}
