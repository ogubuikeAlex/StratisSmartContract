using Stratis.SmartContracts;

namespace SmartContractWithDotNet6
{
    /// <summary>
    /// A basic "Hello World" smart contract
    /// </summary>
    public class Class1 : SmartContract
    {
        public Class1(ISmartContractState smartContractState) : base(smartContractState) 
        {
            SetOwner(Message.Sender);
        }
       
        private void SetOwner(Address owner)
        {
            State.SetAddress("owner", owner);
        }

        public Address GetOwner()
        {
            return State.GetAddress("owner");
        }
    }
}