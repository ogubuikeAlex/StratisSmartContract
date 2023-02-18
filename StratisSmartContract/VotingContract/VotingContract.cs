using Stratis.SmartContracts;
public struct Voter
{
    public uint Weight;
    public bool Voted;
    public uint VoteProposalIndex;
}

public struct Proposal
{
    public string Name;
    public uint VoteCount;
}

internal class VotingContract : SmartContract
{
    public VotingContract(ISmartContractState state, byte[] proposals) : base(state)
    {
        ChairPerson = Message.Sender;
        var proposalsArray = Serializer.ToArray<Proposal>(proposals);
        ValidateAndAssignProposal(proposalsArray);
    }

    private Voter GetVoter(Address address) => State.GetStruct<Voter>($"voter:{address}");
    private void SetVoter(Address address, Voter voter) => State.SetStruct($"voter:{address}", voter);
    public Proposal[] Proposals
    {
        get => State.GetArray<Proposal>(nameof(Proposals));
        private set => State.SetArray(nameof(Proposals), value);
    }
    public Address ChairPerson
    {
        get => State.GetAddress(nameof(ChairPerson));
        private set => State.SetAddress(nameof(ChairPerson), value);
    }

    public bool GiveRightToVote(Address address)
    {
        Assert(Message.Sender == ChairPerson, "Only ChairPerson Can Call this");
        var voter = this.GetVoter(address);

        Assert(voter.Weight == 0, "The voter already has voting rights.");
        Assert(!voter.Voted, "Voter already voted.");

        voter.Weight = 1;
        this.SetVoter(address, voter);
        return true;
    }

    public bool Vote(uint proposalId)
    {
        var voter = this.GetVoter(Message.Sender);
        Assert(voter.Weight == 1, "Has no right to vote.");
        Assert(!voter.Voted, "Already voted.");

        voter.Voted = true;
        voter.VoteProposalIndex = proposalId;

        Proposals[proposalId].VoteCount += voter.Weight;
        SetVoter(Message.Sender, voter);

        //Every transaction has some fee associated and reading data
        //for each record would not be the best way.
        //Instead, the Log method of the SmartContract class enables you
        //to log the data that can be queried using API. 
        //So log works like event🤔
        Log(new Voter
        {
            Voted = true,
            Weight = 1,
            VoteProposalIndex = proposalId
        });
        return true;
    }

    public uint WinningProposal()
    {
        uint winningVoteCount = 0;
        uint winningProposalId = 0;

        for (uint i = 0; i < Proposals.Length; i++)
        {
            if (Proposals[i].VoteCount > winningVoteCount)
            {
                winningVoteCount = Proposals[i].VoteCount;
                winningProposalId = i;
            }
        }

        return winningProposalId;
    }

    public string WinnerName()
    {
        var winningProposalId = WinningProposal();
        var proposals = Proposals[winningProposalId];
        return proposals.Name;
    }

    private void ValidateAndAssignProposal(Proposal[] proposals)
    {
        //Assert is kinda like require in solidity
        Assert(proposals.Length > 1, "Please provide at least 2 proposals");
        Proposals = proposals;
    }
}
