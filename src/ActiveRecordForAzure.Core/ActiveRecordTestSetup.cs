using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ActiveRecordForAzure.Core {
    public class ActiveRecordTestSetup<TEntity> where TEntity : new() {

        private readonly int _numberOfEntities;
        private readonly List<ActiveRecordTestSetupMember> _memberSetups = new List<ActiveRecordTestSetupMember>();

        public ActiveRecordTestSetup(int numberOfEntities) {
            _numberOfEntities = numberOfEntities;
            ActiveRecordTestContext.EnsureTestContext();
            RegisterWithCurrentTestContext();
        }

        private void RegisterWithCurrentTestContext() {
            var context = ActiveRecordContext.Current as ActiveRecordTestContext;
            context.RegisterSetup(this);
        }

        public int NumberOfEntities {
            get { return _numberOfEntities; }
        }

        public IList<ActiveRecordTestSetupMember> Members {
            get { return _memberSetups; }
        }

        public ActiveRecordTestSetupMemberRegistration<TEntity, TMember> With<TMember>(Expression<Func<TEntity, TMember>> func) {

            var member = ((MemberExpression)func.Body).Member.Name;
            return new ActiveRecordTestSetupMemberRegistration<TEntity, TMember>(this,member);
        }

    }

    public class ActiveRecordTestSetupMember {
        public string Name { get; set; }
        public object Value { get; set; }
    }

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