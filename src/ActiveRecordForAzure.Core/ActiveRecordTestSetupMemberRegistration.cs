namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// The value to be returned by the specified member
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TMember">The type of the member.</typeparam>
    public class ActiveRecordTestSetupMemberRegistration<TEntity, TMember> where TEntity : new() {

        private readonly ActiveRecordTestSetup<TEntity> _testSetup;
        private readonly string _memberName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveRecordTestSetupMemberRegistration&lt;TEntity, TMember&gt;"/> class.
        /// </summary>
        /// <param name="testSetup">The test setup.</param>
        /// <param name="memberName">Name of the member.</param>
        public ActiveRecordTestSetupMemberRegistration(ActiveRecordTestSetup<TEntity> testSetup, string memberName) {
            _testSetup = testSetup;
            _memberName = memberName;
        }

        /// <summary>
        /// Returns the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
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