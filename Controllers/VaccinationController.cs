
using System.Reflection.Metadata;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using System.Collections.Generic;
using les2_demo2.Models;


namespace les2_demo2.Controllers
{
    [ApiController]

    public class VaccinationController : ControllerBase
    {
        private static List<VaccinType> _vaccinTypes;

        private static List<VaccinationLocation> _vaccinLocations;

        private static List<VaccinationRegistration > _registraties;


        public VaccinationController()
        {
            if(_registraties == null){
                _registraties = new List<VaccinationRegistration>();
               
            }




            if(_vaccinTypes == null){
                _vaccinTypes = new List<VaccinType>();
                _vaccinTypes.Add(new VaccinType()
                {
                    VaccinTypeId = Guid.NewGuid(),
                    Name = "Modera"
                });
            }


            if(_vaccinLocations == null){
                _vaccinLocations = new List<VaccinationLocation>();
                _vaccinLocations.Add(new VaccinationLocation()
                {
                    VaccinationLocationId = Guid.NewGuid(),
                    Name = "Kortrijk Expo"
                });
            }

        }

        [HttpPost]
        [Route("/registration")]

        public ActionResult<VaccinationRegistration> AddRegistration(VaccinationRegistration newRegistration){
            newRegistration.VaccinationRegistration = Guid.NewGuid();
            _registraties.Add(newRegistration);
            return newRegistration;
        }
      




        [HttpGet]
        [Route("/vaccins")]
 

        public ActionResult<List<VaccinType >> GetVaccins(){
            return new OkObjectResult(_vaccinTypes);
        }


        [HttpGet]
        [Route ("/locations")]
        

        public ActionResult<List<VaccinationLocation >> GetLocations(){
            return new OkObjectResult(_vaccinLocations);
        }

    }
}
