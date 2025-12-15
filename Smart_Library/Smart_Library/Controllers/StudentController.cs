using Microsoft.AspNetCore.Mvc;
using Smart_Library.SmartLibraryManagement.DTOs;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Models;

[Route("SmartLibrary/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
	private readonly IStudentRepository _studentRepository;

	public StudentController(IStudentRepository studentRepository)
	{
		_studentRepository = studentRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetBorrower()
	{
		var students = await _studentRepository.GetAllAsync();
		return (!students.Any()) ? NotFound("No Students Registered") : Ok(students);
	}

	[HttpGet]
	[Route("GetAllUser/Student")]
	public async Task<IActionResult> GetAllUser()
	{
		var payload = await _studentRepository.GetAllWithUserAsync();

		if (!payload.Any())
			return NotFound("No Students Registered");

		return Ok(payload);
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetBorrower(int id)
	{
		var student = await _studentRepository.GetByIdAsync(id);
		return (student == null) ? NotFound($"#404! Id {id} Not Found") : Ok(student);
	}

	[HttpPost]
	public async Task<IActionResult> AddBorrower(AddStudentDTO addStudent)
	{
		var student = new Student
		{
			UserId = addStudent.UserId,
			GradeLevel = addStudent.GradeLevel,
			Course = addStudent.Course
		};

		await _studentRepository.AddAsync(student);

		return Ok(new GetStudentDTO
		{
			StudentId = student.StudentId,
			UserId = student.UserId,
			GradeLevel = student.GradeLevel,
			Course = student.Course
		});
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateBorrower(int id, AddStudentDTO updateStudent)
	{
		var student = await _studentRepository.GetByIdAsync(id);
		if (student == null) return NotFound($"#404, Id {id} Not Found");

		student.UserId = updateStudent.UserId;
		student.GradeLevel = updateStudent.GradeLevel;
		student.Course = updateStudent.Course;

		await _studentRepository.UpdateAsync(student);
		return Ok(student);
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteBorrower(int id)
	{
		var student = await _studentRepository.GetByIdAsync(id);
		if (student == null) return NotFound($"#404!, Id {id} Not Found");

		await _studentRepository.DeleteAsync(student);
		return Ok($"Student With Id {id} Deleted Successfully.");
	}
}
