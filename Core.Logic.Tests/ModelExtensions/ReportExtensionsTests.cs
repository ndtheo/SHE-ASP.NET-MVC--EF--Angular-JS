using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core.Entities;

namespace Core.Logic.ModelExtensions.Tests
{
	[TestClass]
	public class ReportExtensionsTests
	{
		[TestMethod]
		public void TestModelHasNotChanged()
		{
			var report = new Report();
			var knownProperties = "AccidentIdCarIdRepairShopIdReportTypeIdExpertIdAutopsyDate1AutopsyDate2AutopsyDate3ReportDateCarPriceRepairShopDemandRepairShopAgreementExtraEquipmentOdometerCarConditionIdTireConditionIdCommentsLocationTotalDamageCostTaxCostTotalCostOldTotalDamageCostOldTaxCostOldTotalCostIsPreviousReportIsFinalReportPreviousReportIdAccidentCarRepairShopCarConditionExpertReportTypePreviousReportTireConditionDamagesVehicleIconReportClicksNameIdCreationDateCreatorIdLastUpdateDateLastUpdateUserIdCreatorNameLastUpdateUserNameLastUpdateUserCreator";
			var properties = report.GetType().GetProperties().Select(p => p.Name).Aggregate(string.Empty, (s, s1) => s + s1);

			Debug.WriteLine(properties);
			Assert.AreEqual(knownProperties, properties);

			// In case of a change in the Report object, the copy method may need to be updated.
			// This test and all copy tests should also be updated, to include new or updated properties.
		}

		[TestMethod]
		public void TestCopyWorksAsExpected()
		{
			var report = new Report
			{
				Id = 34,
				CarId = 988967575,
				Car = new Car(),
				PreviousReportId = 45,
				Damages = new List<Damage>()
				{
					new Damage(),
					new Damage(),
				},
				VehicleIconReportClicks = new List<VehicleIconReportClick>()
				{
					new VehicleIconReportClick()
				}
			};

			var reportCopy = report.CreateCopy();

			Assert.AreEqual(0, reportCopy.Id);

			foreach (var damage in reportCopy.Damages)
			{
				Assert.AreEqual(0, damage.ReportId);
				Assert.AreEqual(reportCopy, damage.Report);
			}

			foreach (var click in reportCopy.VehicleIconReportClicks)
			{
				Assert.AreEqual(0, click.ReportId);
				Assert.AreEqual(reportCopy, click.Report);
			}

			Assert.IsNull(reportCopy.Car);
			Assert.AreEqual(report.CarId, reportCopy.CarId);
			
			Assert.AreEqual(true, report.IsPreviousReport);
			Assert.AreEqual(false, reportCopy.IsPreviousReport);
			Assert.AreEqual(report.Id, reportCopy.PreviousReportId);
		}

		[TestMethod]
		public void TestNullHandled()
		{
			Assert.IsNull(ReportExtensions.CreateCopy(null));
		}
	}
}