using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace MRODBL.Entities
{
    public class DBConnectionInfo
    {
        public string ConnectionString { get; set; }
    }
}
