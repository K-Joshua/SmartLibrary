using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;

[Route("SmartLibrary/[controller]")]
[ApiController]
public class FacultyController : ControllerBase
{
	private readonly IFacultyRepository _facultyRepository;

	public FacultyController(IFacultyRepository facultyRepository)
	{
		_facultyRepository = facultyRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetFaculties()
	{
		var faculties = await _facultyRepository.GetAllAsync();
		return (!faculties.Any()) ? NotFound("No Faculties Registered") : Ok(faculties);
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetFaculty(int id)
	{
		var faculty = await _facultyRepository.GetByIdAsync(id);
		return (faculty == null) ? NotFound($"#404! Id {id} Not Found") : Ok(faculty);
	}

	[HttpPost]
	public async Task<IActionResult> AddFaculty(AddFacultyDTO addFaculty)
	{
		var faculty = new Faculty
		{
			UserId = addFaculty.UserId,
			Department = addFaculty.Department,
			Position = addFaculty.Position
		};

		await _facultyRepository.AddAsync(faculty);

		return Ok(new GetFacultyDTO
		{
			FacultytId = faculty.FacultytId,
			UserId = faculty.UserId,
			Department = faculty.Department,
			Position = faculty.Position
		});
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateFaculty(int id, UpdateFacultyDTO updateFaculty)
	{
		var faculty = await _facultyRepository.GetByIdAsync(id);
		if (faculty == null) return NotFound($"#404, Id {id} Not Found");

		faculty.Department = updateFaculty.Department;
		faculty.Position = updateFaculty.Position;

		await _facultyRepository.UpdateAsync(faculty);
		return Ok(faculty);
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteFaculty(int id)
	{
		var faculty = await _facultyRepository.GetByIdAsync(id);
		if (faculty == null) return NotFound($"#404!, Id {id} Not Found");

		await _facultyRepository.DeleteAsync(faculty);
		return Ok($"Faculty With Id {id} Deleted Successfully.");
	}
}
