using System.ServiceModel;
using TalkBackWCF.Contract.Entities;

namespace TalkBackWCF.Contract
{
    [ServiceContract]
    public interface IUserCallback
    {
        [OperationContract]
        void OnLogin(OnLoginRequest userLogedin);

        [OperationContract]
        void OnLogout(OnlogoutRequest userLogedout);
        [OperationContract]
        void OnSignedUp(OnSignedUpRequest userSignedUp);
    }
}