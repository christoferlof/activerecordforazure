using System;
using System.Linq.Expressions;

namespace ActiveRecordForAzure.Core {
    public class ActiveRecordTestSetup<TEntity> {
        
        private readonly int _numberOfEntities;

        public ActiveRecordTestSetup(int numberOfEntities) {
            _numberOfEntities = numberOfEntities;

            var context = ActiveRecordTestContext.Initialize();
            context.RegisterSetup(this);
            
        }

        public int NumberOfEntities {
            get { return _numberOfEntities; }
        }

        public ActiveRecordTestSetupMember<TEntity,TMember> With<TMember>(Expression<Func<TEntity, TMember>>func) {
            return new ActiveRecordTestSetupMember<TEntity, TMember>();
        }

    }

    public class ActiveRecordTestSetupMember<TEntity,TMember> {
        public ActiveRecordTestSetupMember<TEntity,TMember> Returns(TMember value) {
            return this;
        }
    }
}