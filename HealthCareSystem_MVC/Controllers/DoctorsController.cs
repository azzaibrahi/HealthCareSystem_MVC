using AutoMapper;
using HealthCareSystem_MVC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareSystem_MVC.Controllers;

/// <summary>CRUD screens for doctors.</summary>
public class DoctorsController : Controller
{
    private const int PageSize = 10;
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public DoctorsController(IDoctorService doctorService, IMapper mapper)
    {
        _doctorService = doctorService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? searchValue, int pageIndex = 1)
    {
        var result = await _doctorService.GetAllAsync(searchValue, pageIndex, PageSize);
        if (!result.Successeded || result.Data is null)
            return Problem(result.Error);

        var doctors = _mapper.Map<IEnumerable<DoctorViewModel>>(result.Data);
        var model = PaginatedList<DoctorViewModel>.Create(
            doctors,
            result.Data.TotalCount,
            result.Data.PageIndex,
            PageSize);

        return View(model);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(DoctorViewModel doctorVM)
    {
        if (!ModelState.IsValid)
            return View(doctorVM);

        var result = await _doctorService.CreateAsync(_mapper.Map<Doctor>(doctorVM));
        if (!result.Successeded)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(doctorVM);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
        => await DoctorHandlerAsync(id, nameof(Details));

    public async Task<IActionResult> Edit(int? id)
        => await DoctorHandlerAsync(id, nameof(Edit));

    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute] int id, DoctorViewModel doctorVM)
    {
        if (id != doctorVM.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(doctorVM);

        var result = await _doctorService.UpdateAsync(_mapper.Map<Doctor>(doctorVM));
        if (!result.Successeded)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(doctorVM);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
        => await DoctorHandlerAsync(id, nameof(Delete));

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> ConfirmDelete(int? id)
    {
        if (!id.HasValue)
            return BadRequest();

        var result = await _doctorService.DeleteAsync(id.Value);
        if (!result.Successeded || result.Data is null)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    private async Task<IActionResult> DoctorHandlerAsync(int? id, string viewName)
    {
        if (!id.HasValue)
            return BadRequest();

        var result = await _doctorService.GetByIdAsync(id.Value);
        if (!result.Successeded || result.Data is null)
            return NotFound();

        return View(viewName, _mapper.Map<DoctorViewModel>(result.Data));
    }
}
