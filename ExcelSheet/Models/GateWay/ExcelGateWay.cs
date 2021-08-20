using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelSheet.Models.GateWay
{
	public class ExcelGateWay: ConnectionGateWay
	{

        public async Task<IEnumerable<int>> InsertEmployee(IEnumerable<FirstSheet> firstSheets)
        {

            try
            {
                EXCELConnection.Open();
                //RiderGateway riderGateway = new RiderGateway();
                var dtable = ConvertToDataTable(firstSheets);

                return await EXCELConnection.QueryAsync<int>(
                          sql: @"[dbo].[InsertEmployee]",
                           new { EmplyeeType = dtable.AsTableValuedParameter("InsertEmpType") },
                          commandType: CommandType.StoredProcedure
                          );

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                EXCELConnection.Close();

            }
        }
        public async Task<IEnumerable<int>> InsertSalary(IEnumerable<SecondSheet> secondSheets)
        {

            try
            {
                EXCELConnection.Open();
                //RiderGateway riderGateway = new RiderGateway();
                var dtable = ConvertToDataTable(secondSheets);

                return await EXCELConnection.QueryAsync<int>(
                          sql: @"[dbo].[InsertSalary]",
                           new { SalaryType = dtable.AsTableValuedParameter("InsertEmpSalaryType") },
                          commandType: CommandType.StoredProcedure
                          );

            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                EXCELConnection.Close();

            }
        }


        DataTable ConvertToDataTable<Excel>(IEnumerable<Excel> source)
            {
                var props = typeof(Excel).GetProperties();

                var dt = new DataTable();
                dt.Columns.AddRange(
                  props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
                );

                source.ToList().ForEach(
                  i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
                );

                return dt;
            }

        }

    }

