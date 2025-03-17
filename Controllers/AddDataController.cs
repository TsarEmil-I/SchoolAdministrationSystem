using AutoMapper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;
using System.Globalization;

[Authorize(Roles = "Admin")]
public class AddDataController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddDataController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        return View(); 
    }

    [HttpPost]
    public async Task<IActionResult> ImportCSV(AddDataDTO model)
    {
        if (model.CsvFile == null || model.CsvFile.Length == 0)
        {
            ModelState.AddModelError("", "Моля, изберете валиден CSV файл.");
            return View("AddData");
        }

        using (var stream = new StreamReader(model.CsvFile.OpenReadStream()))
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };

            using (var csv = new CsvHelper.CsvReader(stream, csvConfig))
            {
                var records = csv.GetRecords<AddDataDTO>().ToList();
                var entities = _mapper.Map<List<Student>>(records);

                await _context.Students.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
        }

        TempData["SuccessMessage"] = "Данните бяха успешно добавени!";
        return RedirectToAction("Index");
    }
}
