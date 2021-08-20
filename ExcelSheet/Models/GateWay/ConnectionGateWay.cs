using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelSheet.Models.GateWay
{
	public class ConnectionGateWay
	{

        private static readonly SqlConnectionStringBuilder EXCELConnectionString =
         new SqlConnectionStringBuilder
		 {
			 DataSource = "DESKTOP-8OFNQTR\\SERVER3",
			 InitialCatalog = "Excel",
			 IntegratedSecurity = false
			 //Password = "72@Lhrq7",
			 //PersistSecurityInfo = false,
			 //Pooling = true,
			 //UserID = "AccDT",
			 //MultipleActiveResultSets = true

		 };

        public static IDbConnection EXCELConnection = new SqlConnection(EXCELConnectionString.ConnectionString);
    }
}
