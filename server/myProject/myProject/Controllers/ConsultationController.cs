using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myProject.Data;
using myProject.Models;

namespace myProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultationController : ControllerBase
    {
        private readonly ApplicationDbContext _dbCons;

        public ConsultationController(ApplicationDbContext db)
        {
            _dbCons = db;
        }

        /// <summary>
        /// Получение данных о консультациях пациента
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetConsultations()
        {
            return Ok(_dbCons.Patients.Include(p => p.Consultations).ToList());
        }

        /// <summary>
        /// Создание консультаций пациента
        /// </summary>
        /// <param name="objConsultation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddConsultation([FromBody] Consultation objConsultation)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Error while creating new consultation");
            }
            else
            {
                _dbCons.Consultations.Add(objConsultation);
                await _dbCons.SaveChangesAsync();

                return new JsonResult("Consultation was created successfully");
            }
        }
        /// <summary>
        /// Обновление данных консультаций пациента
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objConsultation"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsultation([FromRoute] int id,
            [FromBody] Consultation objConsultation)
        {
            if (objConsultation == null || id != objConsultation.consultationId)
            {
                return new JsonResult("Consultation was not found");
            }
            else
            {
                _dbCons.Consultations.Update(objConsultation);
                await _dbCons.SaveChangesAsync();

                return new JsonResult("Consultation was updated successfully");
            }
        }

        /// <summary>
        /// Удаление консультаций пациента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultation([FromRoute] int id)
        {
            var findConsultation = await _dbCons.Consultations.FindAsync(id);

            if (findConsultation == null)
            {
                return NotFound();
            }
            else
            {
                _dbCons.Consultations.Remove(findConsultation);
                await _dbCons.SaveChangesAsync();

                return new JsonResult("Consultation was deleted successfully");
            }
        }
    }
}
