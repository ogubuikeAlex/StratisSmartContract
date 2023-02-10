using Stratis.SmartContracts;
using System;

public class MyContract : SmartContract
{
    public MyContract(ISmartContractState smartContractState)
    : base(smartContractState)
    {
        SetOwner(Message.Sender);
    }

    private void SetOwner(Address owner)
    {
        PersistentState.SetAddress("owner", owner);
    }

    public Address GetOwner()
    {
        return PersistentState.GetAddress("owner");
    }
}