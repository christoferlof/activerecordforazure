using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ActiveRecordForAzure.Core {
    
    /// <summary>
    /// Holds information about stubs to be created
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class ActiveRecordTestSetup<TEntity> where TEntity : new() {

        private readonly int _numberOfEntities;
        private readonly List<ActiveRecordTestSetupMember> _memberSetups = new List<ActiveRecordTestSetupMember>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveRecordTestSetup&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="numberOfEntities">The number of entities.</param>
        public ActiveRecordTestSetup(int numberOfEntities) {
            _numberOfEntities = numberOfEntities;
            ActiveRecordTestContext.EnsureTestContext();
            RegisterWithCurrentTestContext();
        }

        private void RegisterWithCurrentTestContext() {
            var context = ActiveRecordContext.Current as ActiveRecordTestContext;
            context.RegisterSetup(this);
        }

        /// <summary>
        /// Gets the number of entities to create
        /// </summary>
        /// <value>The number of entities.</value>
        public int NumberOfEntities {
            get { return _numberOfEntities; }
        }

        /// <summary>
        /// Gets the explicit defined entity members
        /// </summary>
        /// <value>The members.</value>
        public IList<ActiveRecordTestSetupMember> Members {
            get { return _memberSetups; }
        }

        /// <summary>
        /// Registeres the specified entity member for explicit value initialization
        /// </summary>
        /// <typeparam name="TMember">The type of the member.</typeparam>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        public ActiveRecordTestSetupMemberRegistration<TEntity, TMember> With<TMember>(Expression<Func<TEntity, TMember>> func) {

            var member = ((MemberExpression)func.Body).Member.Name;
            return new ActiveRecordTestSetupMemberRegistration<TEntity, TMember>(this,member);
        }

    }
}