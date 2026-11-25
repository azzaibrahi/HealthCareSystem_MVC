using AutoMapper;
using HealthCareSystem_MVC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthCareSystem_MVC.Controllers;

/// <summary>CRUD screens for appointments with their composite key.</summary>
public class AppointmentsController : Controller
{
    private const int PageSize = 10;
    private readonly IAppointmentService _appointmentService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly IMapper _mapper;

    public AppointmentsController(
        IAppointmentService appointmentService,
        IPatientService patientService,
        IDoctorService doctorService,
        IMapper mapper)
    {
        _appointmentService = appointmentService;
        _patientService = patientService;
        _doctorService = doctorService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageIndex = 1)
    {
        var result = await _appointmentService.GetAllAsync(pageIndex, PageSize);
        if (!result.Successeded || result.Data is null)
            return Problem(result.Error);

        var appointments = _mapper.Map<IEnumerable<AppointmentViewModel>>(result.Data);
        var model = PaginatedList<AppointmentViewModel>.Create(
            appointments,
            result.Data.TotalCount,
            result.Data.PageIndex,
            PageSize);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateSelectListsAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AppointmentViewModel appointmentVM)
    {
        if (!ModelState.IsValid)
        {
            await PopulateSelectListsAsync();
            return View(appointmentVM);
        }

        var result = await _appointmentService.CreateAsync(_mapper.Map<Appointment>(appointmentVM));
        if (!result.Successeded)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            await PopulateSelectListsAsync();
            return View(appointmentVM);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int patientId, int doctorId, DateTime appointmentDate)
        => await AppointmentHandlerAsync(patientId, doctorId, appointmentDate, nameof(Details));

    public async Task<IActionResult> Edit(int patientId, int doctorId, DateTime appointmentDate)
        => await AppointmentHandlerAsync(patientId, doctorId, appointmentDate, nameof(Edit));

    [HttpPost]
    public async Task<IActionResult> Edit(
        int patientId,
        int doctorId,
        DateTime appointmentDate,
        AppointmentViewModel appointmentVM)
    {
        if (patientId != appointmentVM.PatientId || doctorId != appointmentVM.DoctorId)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            await PopulateSelectListsAsync();
            return View(appointmentVM);
        }

        var result = await _appointmentService.UpdateAsync(_mapper.Map<Appointment>(appointmentVM));
        if (!result.Successeded)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            await PopulateSelectListsAsync();
            return View(appointmentVM);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int patientId, int doctorId, DateTime appointmentDate)
        => await AppointmentHandlerAsync(patientId, doctorId, appointmentDate, nameof(Delete));

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> ConfirmDelete(int patientId, int doctorId, DateTime appointmentDate)
    {
        var result = await _appointmentService.DeleteAsync(patientId, doctorId, appointmentDate);
        if (!result.Successeded || result.Data is null)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    private async Task<IActionResult> AppointmentHandlerAsync(
        int patientId,
        int doctorId,
        DateTime appointmentDate,
        string viewName)
    {
        var result = await _appointmentService.GetByIdAsync(patientId, doctorId, appointmentDate);
        if (!result.Successeded || result.Data is null)
            return NotFound();

        if (viewName == nameof(Edit))
            await PopulateSelectListsAsync();

        return View(viewName, _mapper.Map<AppointmentViewModel>(result.Data));
    }

    private async Task PopulateSelectListsAsync()
    {
        var patients = (await _patientService.GetAllAsync(null, 1, int.MaxValue)).Data
            ?? PaginatedList<Patient>.Create(Enumerable.Empty<Patient>(), 1, 1);
        var doctors = (await _doctorService.GetAllAsync(null, 1, int.MaxValue)).Data
            ?? PaginatedList<Doctor>.Create(Enumerable.Empty<Doctor>(), 1, 1);

        ViewBag.Patients = new SelectList(patients, nameof(Patient.Id), nameof(Patient.Name));
        ViewBag.Doctors = new SelectList(doctors, nameof(Doctor.Id), nameof(Doctor.Name));
    }
}
