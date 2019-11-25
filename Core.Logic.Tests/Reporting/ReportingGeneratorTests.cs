using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Logic.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models.DbContext;
using Database.Models.SearchCriteria;

namespace Core.Logic.Reporting.Tests
{
    [TestClass()]
    public class ReportingGeneratorTests
    {
        [TestMethod(), TestCategory("Reporting")]
        public void GetReportDataTest()
        {
            //var db = new ApplicationDbContext();
            //var reportSearchCriteria = new ReportSearchCriteria() { CreationDateFrom = new System.DateTime(2019, 1, 1), CreationDateTo = new System.DateTime(2019, 2, 1, 23, 59, 59).AddDays(-1) };

            //var reportingGenerator = new ReportingGenerator(db,reportSearchCriteria);
            //var data = reportingGenerator.GetReportData();

            //Assert.IsNotNull(data);
        }
    }
}