using ExcelSheet.Models;
using ExcelSheet.Models.GateWay;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ExcelSheet.Controllers
{
	public class ExcelController : Controller
	{
		ExcelGateWay excelGateWay = new ExcelGateWay();
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ImportExcel()
		{
			return View();
		}
		

		public int UploadExcel(IFormFile file)
		{
			var Sheet1 = new List<FirstSheet>();
			var Sheet2 = new List<SecondSheet>();
			int result = 0;
			using (var stream =new MemoryStream())
			{
				file.CopyTo(stream);
				using (var package=new ExcelPackage(stream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
					ExcelWorksheet worksheet2 = package.Workbook.Worksheets[1];
					var rowCount = worksheet.Dimension.Rows;
					for (var row = 2; row <= rowCount; row++)
					{
						Sheet1.Add(new FirstSheet
						{ 
						Id= Convert.ToInt32( worksheet.Cells[row,1].Value.ToString().Trim()),
						Name=worksheet.Cells[row,2].Value.ToString().Trim(),
						Score= Convert.ToInt32(worksheet.Cells[row, 1].Value.ToString().Trim())
						});
					}
					var rowCount2 = worksheet2.Dimension.Rows;
					for (var row = 2; row <= rowCount2; row++)
					{
						Sheet2.Add(new SecondSheet
						{
							Id= Convert.ToInt32(worksheet2.Cells[row, 1].Value.ToString().Trim()),
							Salary= Convert.ToInt32(worksheet2.Cells[row, 2].Value.ToString().Trim()),
							Bonous= Convert.ToInt32(worksheet2.Cells[row, 3].Value.ToString().Trim()),

						});
					
					}

				}
			}
			var x = Sheet1;
			var y = Sheet1;
			var first =  excelGateWay.InsertEmployee(Sheet1);
			var second = excelGateWay.InsertSalary(Sheet2);
			return result;
		}
	}
}
