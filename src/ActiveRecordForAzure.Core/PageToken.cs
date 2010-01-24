using System;
using System.Collections.Generic;
using System.Linq;

namespace ActiveRecordForAzure.Core {

    /// <summary>
    /// Page Token
    /// </summary>
    public class PageToken {

        private const char TokenSeparator = '@';

        /// <summary>
        /// Initializes a new instance of the <see cref="PageToken"/> class.
        /// </summary>
        /// <param name="tokenHeaders">The token headers.</param>
        public PageToken(IDictionary<string, string> tokenHeaders) {
            Token = ParsePageToken(tokenHeaders);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageToken"/> class.
        /// </summary>
        /// <param name="tokenString">The token string.</param>
        public PageToken(string tokenString) {
            Token = ParsePageToken(tokenString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageToken"/> class.
        /// </summary>
        /// <param name="rowKey">The row key.</param>
        /// <param name="partitionKey">The partition key.</param>
        public PageToken(string rowKey, string partitionKey) {
            RowKey = rowKey;
            PartitionKey = partitionKey;
            Token = FormatToken(rowKey, partitionKey);
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public string Token {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the row key.
        /// </summary>
        /// <value>The row key.</value>
        public string RowKey { get; private set; }

        /// <summary>
        /// Gets or sets the partition key.
        /// </summary>
        /// <value>The partition key.</value>
        public string PartitionKey { get; private set; }

        private string ParsePageToken(string pageToken) {

            ValidatePageToken(pageToken);

            var tokenParts = pageToken.Split(TokenSeparator);
            RowKey =        tokenParts[0];
            PartitionKey =  tokenParts[1];

            return FormatToken(tokenParts[0], tokenParts[1]);
        }

        private string ParsePageToken(IDictionary<string,string> tokenHeaders) {
            
            string nextPartitionKey = null;
            string nextRowKey = null;
            tokenHeaders.TryGetValue("x-ms-continuation-NextPartitionKey", out nextPartitionKey);
            tokenHeaders.TryGetValue("x-ms-continuation-NextRowKey", out nextRowKey);

            return FormatToken(nextRowKey, nextPartitionKey);
        }

        private void ValidatePageToken(string pageToken) {
            if (string.IsNullOrEmpty(pageToken)) throw new ArgumentException("Page Token must not be empty or null");
            if (!pageToken.Contains(TokenSeparator)) throw new ArgumentException("Invalid Page Token");
        }

        private string FormatToken(string row, string partition) {
            return string.Format("{1}{0}{2}", TokenSeparator, row, partition);
        }

    }
}