﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.WindowsAzure.Storage.Table;

namespace AzureHelperUtils
{
    public static class AzureStorageResourceHelper
    {        
        public static async Task PersistDataAsync(this CloudTable table, TableEntity data)
        {
            TableOperation insertOrReplaceOp = TableOperation.InsertOrReplace(data);
            try
            {
                await table.ExecuteAsync(insertOrReplaceOp);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Insert failed " + ex);
            }
        }
    }
}
