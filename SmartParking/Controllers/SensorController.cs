using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartParking.DBContext;
using SmartParking.Models;
using SmartParking.Models.ViewModels;

namespace SmartParking.Controllers
{
    public class SensorController : Controller
    {
        private readonly ParkingDBContext parkingDBContext;

        public SensorController(ParkingDBContext parkingDBContext)
        {
            this.parkingDBContext = parkingDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sensors = await parkingDBContext.Sensor.ToListAsync();

            return View(sensors);
        }
        

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSensorViewModel addSensorRequest)
        {
            var sensor = new Sensor()
            {
                Name = addSensorRequest.Name,
                Type = addSensorRequest.Type,
                Description = addSensorRequest.Description
            };

            await parkingDBContext.Sensor.AddAsync(sensor);
            await parkingDBContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sensor = await parkingDBContext.Sensor.FirstOrDefaultAsync(s => s.Id == id);

            if (sensor != null)
            {
                var viewModel = new EditSensorViewModel()
                {
                    Id = sensor.Id,
                    Name = sensor.Name,
                    Type = sensor.Type,
                    Description = sensor.Description
                };

                return await Task.Run(() => View("Edit", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSensorViewModel model)
        {
            var sensor = await parkingDBContext.Sensor.FindAsync(model.Id);

            if (sensor != null)
            {
                sensor.Name = model.Name;
                sensor.Description = model.Description;
                sensor.Type = model.Type;

                await parkingDBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditSensorViewModel model)
        {
            var sensor = await parkingDBContext.Sensor.FindAsync(model.Id);

            if (sensor != null)
            {
                parkingDBContext.Sensor.Remove(sensor);
                await parkingDBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
