using System;
using System.Collections.Generic;
using System.Linq;


namespace ActiveRecordForAzure.Core {
    public class PageToken {

        private const char TokenSeparator = '@';

        public PageToken(IDictionary<string, string> tokenHeaders) {
            Token = ParsePageToken(tokenHeaders);
        }

        public PageToken(string tokenString) {
            Token = ParsePageToken(tokenString);
        }

        public PageToken(string rowKey, string partitionKey) {
            RowKey = rowKey;
            PartitionKey = partitionKey;
            Token = FormatToken(rowKey, partitionKey);
        }

        public string Token {
            get; private set;
        }

        public string RowKey { get; private set; }

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