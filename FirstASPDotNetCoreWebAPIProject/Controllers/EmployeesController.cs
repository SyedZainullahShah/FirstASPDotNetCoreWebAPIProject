using FirstASPDotNetCoreWebAPIProject.DB;
using FirstASPDotNetCoreWebAPIProject.Models;
using Microsoft.AspNetCore.Mvc;
using FirstASPDotNetCoreWebAPIProject.Helpers;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstASPDotNetCoreWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly TestDbContext _context;
        public EmployeesController(TestDbContext context)
        {
            _context = context;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Employee>? employees = _context.Employees.ToList();
            if (!employees.Any())
                return NotFound(ApiResponse.Fail("No any employee record found."));

            return Ok(_context.Employees.ToList());
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {

            if (!int.TryParse(id, out int employeeId))
                return BadRequest(ApiResponse.Fail($"Id {id} is invalid employee Id." ));

            if (employeeId <= 0)
                return BadRequest(ApiResponse.Fail("Invalid Employee Id is entered." ));

            Employee? employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
                return NotFound(ApiResponse.Fail($"Employee with Id {employeeId} is not found." ));

            return Ok(employee);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Name))
                return BadRequest(ApiResponse.Fail("Employee name must be entered" ));

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        // POST api/<EmployeesController>
        [HttpPost("bulk")]
        public IActionResult Post([FromBody] List<Employee> employees)
        {
            foreach (Employee employee in employees)
                if (string.IsNullOrWhiteSpace(employee.Name))
                    return BadRequest(ApiResponse.Fail("Employee name must be entered" ));

            _context.Employees.AddRange(employees);
            _context.SaveChanges();

            return Ok(ApiResponse.Ok($"{employees.Count} employees have been added successfully", employees));
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Employee employee)
        {
            if (!int.TryParse(id, out int employeeId))
                return BadRequest(ApiResponse.Fail($"Id {id} is invalid employee Id." ));

            if (employeeId == 0)
                return BadRequest(ApiResponse.Fail("Employee Id must be a positive number." ));


            if (string.IsNullOrWhiteSpace(employee.Name))
                return BadRequest(ApiResponse.Fail("Employee name must be entered."));

            Employee? employeeToUpdate = _context.Employees.FirstOrDefault(x => x.Id == employeeId);

            if (employee == null)
                return NotFound(ApiResponse.Fail($"Employee with Id {employeeId} is not found."));

            employeeToUpdate.Name = employee.Name;
            _context.Employees.Update(employeeToUpdate);

            return CreatedAtAction(nameof(Get), new { id = employeeToUpdate.Id }, employeeToUpdate);
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!int.TryParse(id, out int employeeId))
                return BadRequest(ApiResponse.Fail($"Id {id} is invalid employee Id." ));

            if (employeeId == 0)
                return BadRequest(ApiResponse.Fail($"Employee Id must be a positive number."));

            Employee? employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
                return NotFound(ApiResponse.Fail($"Employee with Id {employeeId} is not found."));

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return Ok(ApiResponse.Ok($"Employee with Id {employeeId} has been deleted successfully.",null));
        }
    }
}
