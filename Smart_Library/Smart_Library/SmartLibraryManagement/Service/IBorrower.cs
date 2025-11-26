using Microsoft.AspNetCore.Mvc;

namespace Smart_Library.SmartLibraryManagement.Service
{
    public interface IBorrower
    {
		public IActionResult GetBorrower();
		public IActionResult GetBorrower(int id);
		public IActionResult DeleteBorrower(int id);

		public IActionResult BookIssue();
	}
}
