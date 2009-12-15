namespace ActiveRecordForAzure.Core {

    public class ActiveRecordTestSetupMemberRegistration<TEntity, TMember> where TEntity : new() {

        private readonly ActiveRecordTestSetup<TEntity> _testSetup;
        private readonly string _memberName;

        public ActiveRecordTestSetupMemberRegistration(ActiveRecordTestSetup<TEntity> testSetup, string memberName) {
            _testSetup = testSetup;
            _memberName = memberName;
        }

        public ActiveRecordTestSetup<TEntity> Returns(TMember value) {
            var memberSetup = new ActiveRecordTestSetupMember {
                                                                  Name = _memberName,
                                                                  Value = value
                                                              };
            _testSetup.Members.Add(memberSetup);

            return _testSetup;
        }
    }
}