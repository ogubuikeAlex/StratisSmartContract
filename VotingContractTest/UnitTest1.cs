/*namespace VotingContractTest
{
    using Moq;
    using NBitcoin;
    using Stratis.SmartContracts.CLR.Serialization;
    using Stratis.SmartContracts.Core;
    using StratisSmartContract.VotingContract;
    using Xunit;
    using Xunit.Abstractions;

    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private Serializer serializer;
        private Mock<Network> network;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            network = new Mock<Network>();
            serializer = new Serializer(new ContractPrimitiveSerializerV2(network.Object));
        }

        [Fact]
        public void SerializePraposalAsHexString()
        {
            var proposals = new Proposal[]
            {
                new() { Name = "Joe Biden", VoteCount = 0 },
                new() { Name = "Donald Trump", VoteCount = 0 }
            };

            var byteArr = serializer.Serialize(proposals);

            _testOutputHelper.WriteLine(serializer.Serialize(proposals).ToHexString());
        }

        *//*parameters: [
 "10#E590CF894A6F6520426964656E840000000093D28C446F6E616C64205472756D708400000000"
]*//*
    }
}*/