
using System.Security.Cryptography;
using System.Globalization;
using System.Xml.Xsl;
using System.Xml.Schema;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using System.Collections.Generic;

using les2_demo2.Models;
using System.Linq;
using les2_demo2.Configuration;
using Microsoft.Extensions.Options;
using CsvHelper.Configuration;
using CsvHelper;



namespace les2_demo2.Controllers
{
    [ApiController]

    public class VaccinationController : ControllerBase
    {

        private CSVSettings _settings;



        private static List<VaccinType> _vaccinTypes;

        private static List<VaccinationLocation> _vaccinLocations;

        private static List<VaccinationRegistration> _registraties;


        public VaccinationController(IOptions<CSVSettings> settings)

        {
            _settings = settings.Value;


            if (_vaccinTypes == null)
                _vaccinTypes = ReadCSVVaccins();

            if (_vaccinLocations == null)
                _vaccinLocations = ReadCSVLocations();


        }


        //registraties bijhouden
        private void SaveRegistrations()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";"
            };

            using (var writer = new StreamWriter(_settings.CSVRegistrations))
            {
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(_registraties);
                }
            }

        }


        private List<VaccinType> ReadCSVVaccins()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";"
            };

            using (var reader = new StreamReader(_settings.CSVVaccins))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<VaccinType>();
                    return records.ToList<VaccinType>();
                }
            }

        }




        private List<VaccinationLocation> ReadCSVLocations()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";"
            };

            using (var reader = new StreamReader(_settings.CSVLocations))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<VaccinationLocation>();
                    return records.ToList<VaccinationLocation>();
                }
            }

        }



        [HttpGet]
        [Route("/registrations")]

        public ActionResult<List<VaccinationRegistration>> GetRegistrations()
        {
            return new OkObjectResult(_registraties);
        }


        [HttpPost]
        [Route("/registration")]

        public ActionResult<VaccinationRegistration> AddRegistration(VaccinationRegistration newRegistration)
        {


            if (newRegistration == null)
                return new BadRequestResult();

            if (_vaccinTypes.Where(vt => vt.VaccinTypeId == newRegistration.VaccinTypeId).Count() == 0)
            {
                return new BadRequestResult();
            }

            if (_vaccinLocations.Where(vt => vt.VaccinationLocationId == newRegistration.VaccinationLocationId).Count() == 0)
            {
                return new BadRequestResult();
            }



            newRegistration.VaccinationRegistrationId = Guid.NewGuid();
            _registraties.Add(newRegistration);
            SaveRegistrations();
            return newRegistration;
        }





        [HttpGet]
        [Route("/vaccins")]


        public ActionResult<List<VaccinType>> GetVaccins()
        {
            return new OkObjectResult(_vaccinTypes);
        }


        [HttpGet]
        [Route("/locations")]


        public ActionResult<List<VaccinationLocation>> GetLocations()
        {
            return new OkObjectResult(_vaccinLocations);
        }

    }
}
