using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myProject.Data;
using myProject.Models;

namespace myProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PatientController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Получение данных о пациентах
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPatients()
        {
            return Ok(_db.Patients.ToList());
        }

        /// <summary>
        /// Создание пациента
        /// </summary>
        /// <param name="objPatient"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] Patient objPatient)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Error while creating new patient");
            }
            else
            {
                _db.Patients.Add(objPatient);
                await _db.SaveChangesAsync();

                return new JsonResult("Patient was created successfully");
            }
        }

        /// <summary>
        /// Обновление данных о пациенте
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objPatient"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] int id,
            [FromBody] Patient objPatient)
        {
            if (objPatient == null || id != objPatient.patientId)
            {
                return new JsonResult("Patient was not found");
            }
            else
            {
                _db.Patients.Update(objPatient);
                await _db.SaveChangesAsync();

                return new JsonResult("Patient was updated successfully");
            }
        }

        /// <summary>
        /// Удаление пациента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id)
        {
            var findPatient = await _db.Patients.FindAsync(id);

            if (findPatient == null)
            {
                return NotFound();
            }
            else
            {
                _db.Patients.Remove(findPatient);
                await _db.SaveChangesAsync();

                return new JsonResult("Patient was deleted successfully");
            }
        }
    }
}
