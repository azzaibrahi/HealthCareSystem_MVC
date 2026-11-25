using AutoMapper;
using HealthCareSystem_MVC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareSystem_MVC.Controllers;

/// <summary>CRUD screens for patients.</summary>
public class PatientsController : Controller
{
    private const int PageSize = 10;
    private readonly IPatientService _patientService;
    private readonly IMapper _mapper;

    public PatientsController(IPatientService patientService, IMapper mapper)
    {
        _patientService = patientService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? searchValue, int pageIndex = 1)
    {
        var result = await _patientService.GetAllAsync(searchValue, pageIndex, PageSize);
        if (!result.Successeded || result.Data is null)
            return Problem(result.Error);

        var patients = _mapper.Map<IEnumerable<PatientViewModel>>(result.Data);
        var model = PaginatedList<PatientViewModel>.Create(
            patients,
            result.Data.TotalCount,
            result.Data.PageIndex,
            PageSize);

        return View(model);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(PatientViewModel patientVM)
    {
        if (!ModelState.IsValid)
            return View(patientVM);

        var result = await _patientService.CreateAsync(_mapper.Map<Patient>(patientVM));
        if (!result.Successeded)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(patientVM);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
        => await PatientHandlerAsync(id, nameof(Details));

    public async Task<IActionResult> Edit(int? id)
        => await PatientHandlerAsync(id, nameof(Edit));

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute] int id, PatientViewModel patientVM)
    {
        if (id != patientVM.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(patientVM);

        var result = await _patientService.UpdateAsync(_mapper.Map<Patient>(patientVM));
        if (!result.Successeded)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(patientVM);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
        => await PatientHandlerAsync(id, nameof(Delete));

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> ConfirmDelete(int? id)
    {
        if (!id.HasValue)
            return BadRequest();

        var result = await _patientService.DeleteAsync(id.Value);
        if (!result.Successeded || result.Data is null)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    private async Task<IActionResult> PatientHandlerAsync(int? id, string viewName)
    {
        if (!id.HasValue)
            return BadRequest();

        var result = await _patientService.GetByIdAsync(id.Value);
        if (!result.Successeded || result.Data is null)
            return NotFound();

        return View(viewName, _mapper.Map<PatientViewModel>(result.Data));
    }
}
