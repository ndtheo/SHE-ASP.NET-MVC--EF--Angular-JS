using Core.Entities;
using Database.Models.DbContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace WebApplication.Api_Controllers.Tests
{
    [TestClass()]
    public class AccidentsControllerTests
    {
        /// <summary>
        /// Creation of an icident we check if the record is created correctly.
        /// </summary>
        [TestMethod(), TestCategory("Incidents")]
        public void PostAccidentCreationAndDeletionTest()
        {
            ///Initialization
            var db = new IncidentsDataContext();
            var incidentType = db.IncidentTypes.FirstOrDefault();
            var date = new DateTime(2019, 12, 1);
            var incident = new Incident() {  Description="Test description", Person="Test Person", IncidentDate = date, IncidentTypeId=incidentType.Id  };
            var incidentsApiController = new IncidentsController();

            ///Execution
            var result = incidentsApiController.PostIncident(incident);

            ///Assertion
            Assert.IsNotNull(result);
            int accidentId = incident.Id;
            Assert.IsTrue(accidentId > 0);
            ///Accident Car
            //var incidentAffectedRecord = db.Incidents.FirstOrDefault(c => c.Id == incidentId);
            //Assert.IsNotNull(accidentCar);
            //Assert.AreEqual(accidentCar.CarId, car.Id);
            //Assert.IsNotNull(accidentCar.CreationDate);
            //Assert.AreEqual(accidentCar.CreationDate.Value.Date, DateTime.Now.Date);
            /////Report
            //var report = db.Reports.FirstOrDefault(c => c.AccidentId == accidentId);
            //Assert.IsNotNull(report);
        }
    }
}