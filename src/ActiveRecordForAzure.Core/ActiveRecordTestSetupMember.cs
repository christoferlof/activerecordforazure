namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// Holds the name and the value of the entity member to initialize
    /// </summary>
    public class ActiveRecordTestSetupMember {
        
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; set; }
    }
}