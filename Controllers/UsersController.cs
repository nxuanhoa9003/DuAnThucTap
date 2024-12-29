using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_DonNghiPhep.Data;
using Web_DonNghiPhep.Models;
using Web_DonNghiPhep.Services;
using Web_DonNghiPhep.ViewModels;

namespace Web_DonNghiPhep.Controllers
{

    [Authorize(Roles = "admin")]
    [Route("tai-khoan")]
    public class UsersController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IMessageService _messageService;
        public UsersController(MyDBContext context, IMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }

        [HttpGet("/dang-nhap/")]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                if (roles.Contains("admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else return View();
        }

        [HttpPost("/dang-nhap/")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind("UserName,Password")] UserVM uservm)
        {

            var user = await _context.User
                                .Include(x => x.UserRoles)
                                    .ThenInclude(x => x.Role)
                                .Include(x => x.EmployeeUs)
                                .SingleOrDefaultAsync(x => x.UserName.Equals(uservm.UserName) && x.Password.Equals(uservm.Password));
            if (user == null)
            {
                _messageService.SetMessage("Đăng nhập thất bại", "error");
                return View();
            }
            else
            {
                if (user.Status)
                {


                    var claims = new List<Claim>
                     {
                        new Claim("Employeeid", user.Employee_ID),
                         new Claim("FullName", user.EmployeeUs.FullName)
                     };
                    claims.AddRange(user.UserRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.Role_Name.ToLower())));

                    var department = _context.Department.FirstOrDefault(x => x.ManagerId == user.Employee_ID);

                    // Tạo ClaimsIdentity (Danh tính người dùng)
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Đăng nhập và lưu thông tin người dùng trong cookie
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    // Cập nhật HttpContext.User để kiểm tra ngay trong request
                    HttpContext.User = new ClaimsPrincipal(claimsIdentity);

                    _messageService.SetMessage("Đăng nhập thành công");

                    if (User.IsInRole("admin"))
                    {

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
                        if (roles.Count == 1 && roles[0] == "quản lý") // Kiểm tra chỉ có một vai trò và vai trò đó là "quản lý"
                        {
                            return RedirectToAction("ApproveRequests", "LeaveRequests");
                        }
                        else
                        {
                            return RedirectToAction("Index", "LeaveRequests");
                        }

                    }
                }
                else
                {
                    _messageService.SetMessage("Tài khoản của bạn bị khoá", "warning");
                    return RedirectToAction("login");
                }

            }
        }

        [HttpPost("/dang-xuat")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            // Đăng xuất người dùng và xóa cookie xác thực
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login");
        }

        // GET: Users
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var listuser = _context.User
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.EmployeeUs)
                    .ThenInclude(e => e.DepartmentEmployees) // Bao gồm bảng trung gian
                        .ThenInclude(de => de.Department)   // Lấy thông tin phòng ban từ bảng trung gian
                .Include(u => u.EmployeeUs)
                    .ThenInclude(e => e.Title);
            var listus = await listuser.Select(x => new EmployeeVM
            {
                Employee_ID = x.Employee_ID,
                FullName = x.EmployeeUs.FullName,
                Dob = x.EmployeeUs.Dob.Value,
                Department_name = x.EmployeeUs.DepartmentEmployees.FirstOrDefault(z => z.EmployeeId == x.Employee_ID).Department.DepartmentName,
                UserName = x.UserName,
                Title_name = x.EmployeeUs.Title.Title_name,
                Status = x.Status,
                Role_IDs = x.UserRoles.Where(z => z.UserID == x.UserID).Select(x => x.RoleID).ToList(),
                Roles_Name = x.UserRoles.Where(z => z.UserID == x.UserID).Select(x => x.Role.Role_Name).ToList(),
                 created_at = x.created_at,
                updated_at = x.updated_at,
            }).ToListAsync();
            return View(listus);
        }

        // GET: Users/Details/5
        [HttpGet("chi-tiet-tai-khoan/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                 .Include(u => u.EmployeeUs)
                     .ThenInclude(e => e.DepartmentEmployees)
                 .Include(u => u.EmployeeUs)
                     .ThenInclude(e => e.Title)
                .Select(x => new EmployeeVM
                {
                    Employee_ID = x.Employee_ID,
                    FullName = x.EmployeeUs.FullName,
                    Dob = x.EmployeeUs.Dob.Value,
                    Department_name = x.EmployeeUs.DepartmentEmployees.FirstOrDefault(z => z.EmployeeId == x.Employee_ID).Department.DepartmentName,
                    UserName = x.UserName,
                    Password = x.Password,
                    Title_name = x.EmployeeUs.Title.Title_name,
                    Status = x.Status,
                    Role_IDs = x.UserRoles.Where(z => z.UserID == x.UserID).Select(x => x.RoleID).ToList(),
                    Roles_Name = x.UserRoles.Where(z => z.UserID == x.UserID).Select(x => x.Role.Role_Name).ToList(),
                    created_at = x.created_at,
                    updated_at = x.updated_at,
                    
                })
                .FirstOrDefaultAsync(m => m.Employee_ID == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [HttpGet("tao-tai-khoan-moi")]
        public IActionResult Create()
        {
            ViewData["Department_ID"] = new SelectList(_context.Department, "Department_id", "DepartmentName");
            ViewData["Title_ID"] = new SelectList(_context.Title, "Title_id", "Title_name");
            ViewData["Role_ID"] = new SelectList(_context.Role, "Role_ID", "Role_Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("tao-tai-khoan-moi")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Employee_ID,UserName,Password,FullName,Role_IDs,Title_id,Department_id,Dob,PhoneNumber,Email")] EmployeeVM evm)
        {

            bool check = false;
            if (ModelState.IsValid)
            {
                check = true;

                var existingEmployee = await _context.Employee
                    .FirstOrDefaultAsync(x => x.Employee_ID.ToLower() == evm.Employee_ID.Trim().ToLower());


                if (existingEmployee != null)
                {
                    ModelState.AddModelError("Employee_ID", "Mã nhân viên đã tồn tại");
                    check = false;
                }

                var existingUser = await _context.Employee
                   .Include(x => x.User)
                   .FirstOrDefaultAsync(x => x.User.UserName.ToLower() == evm.UserName.Trim().ToLower());

                if (existingUser != null)
                {
                    check = false;
                    ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại");

                }

                if (evm.UserName.Equals(evm.Password))
                {
                    check = false;
                    ModelState.AddModelError("Password", "Mật khẩu không được giống tên đăng nhập");
                }

            }


            if (check)
            {
                Employee emnew = new Employee()
                {
                    Employee_ID = evm.Employee_ID,
                    FullName = evm.FullName,
                    Dob = evm.Dob,
                    Email = evm.Email,
                    PhoneNumber = evm.PhoneNumber,
                    Title_id = evm.Title_id,
                };
                User usernew = new User()
                {
                    UserID = Guid.NewGuid(),
                    Employee_ID = evm.Employee_ID,
                    UserName = evm.UserName,
                    Password = evm.Password,
                    Status = true,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,
                };

                DepartmentEmployee dpem = new DepartmentEmployee()
                {
                    EmployeeId = evm.Employee_ID,
                    DepartmentId = evm.Department_id
                };

                _context.Add(emnew);
                _context.Add(usernew);
                _context.Add(dpem);
                var rsc = await _context.SaveChangesAsync();
                if (rsc > 0)
                {
                    foreach (var roleId in evm.Role_IDs)
                    {
                        var userRole = new UserRole
                        {
                            UserID = usernew.UserID,
                            RoleID = roleId
                        };
                        _context.UserRole.Add(userRole);
                    }


                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Department_ID"] = new SelectList(_context.Department, "Department_id", "DepartmentName");
            ViewData["Title_ID"] = new SelectList(_context.Title, "Title_id", "Title_name");
            ViewData["Role_ID"] = new SelectList(_context.Role, "Role_ID", "Role_ID", "Role_Name");
            return View(evm);
        }

        // GET: Users/Edit/5
        [HttpGet("chinh-sua-tai-khoan/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listuser = await _context.Employee
                            .Include(x => x.User)
                                .ThenInclude(x => x.UserRoles)
                            .Include(x => x.DepartmentEmployees)
                            .Include(x => x.Title)
                            .ToListAsync();
            var user = listuser.Where(x => x.Employee_ID == id)
                        .Select(x => new EmployeeVM
                        {
                            Employee_ID = x.Employee_ID,
                            UserName = x.User.UserName,
                            FullName = x.FullName,
                            Email = x.Email,
                            Dob = x.Dob,
                            PhoneNumber = x.PhoneNumber,
                            Password = x.User.Password,
                            Department_id = x.DepartmentEmployees.SingleOrDefault(z => z.EmployeeId == x.Employee_ID)?.DepartmentId,
                            Title_id = x.Title_id,
                            Status = x.User.Status,
                            Role_IDs = x.User.UserRoles.Select(z => z.RoleID).ToList(),
                        }).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            ViewData["Department_ID"] = new SelectList(_context.Department, "Department_id", "DepartmentName", user.Department_id);
            ViewData["Title_ID"] = new SelectList(_context.Title, "Title_id", "Title_name", user.Title_id);
            ViewData["Role_ID"] = new SelectList(_context.Role, "Role_ID", "Role_Name", user.Role_IDs);

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("chinh-sua-tai-khoan/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Employee_ID,UserName,Dob,Password,FullName,Status,Role_IDs,Title_id,Department_id,Dob,PhoneNumber,Email")] EmployeeVM evm)
        {
            if (id != evm.Employee_ID)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FirstOrDefaultAsync(x => x.Employee_ID.Equals(evm.Employee_ID.Trim()));
            var user = await _context.User.FirstOrDefaultAsync(x => x.Employee_ID.Equals(evm.Employee_ID.Trim()));

            if (employee == null || user == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                
                var isUserNameDuplicate = await _context.User
                    .AnyAsync(u => u.UserName.ToLower() == evm.UserName.Trim().ToLower() && u.Employee_ID != evm.Employee_ID);

                if (isUserNameDuplicate)
                {
                    ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại");
                }
                else if (evm.UserName.Equals(evm.Password))
                {
                    ModelState.AddModelError("Password", "Mật khẩu không được giống tên đăng nhập");
                }
                else
                {
                    try
                    {

                        employee.FullName = evm.FullName;
                        employee.Dob = evm.Dob;
                        employee.Email = evm.Email;
                        employee.PhoneNumber = evm.PhoneNumber;
                        employee.Title_id = evm.Title_id;
                        /// test
                        using (var transaction = _context.Database.BeginTransaction())
                        {
                            try
                            {
                                var departemployee = _context.DepartmentEmployee.FirstOrDefault(x => x.EmployeeId == employee.Employee_ID);

                                if (departemployee != null)
                                {
                                   
                                    _context.DepartmentEmployee.Remove(departemployee);
                                    _context.SaveChanges();

                                    
                                    var newDepartmentEmployee = new DepartmentEmployee
                                    {
                                        DepartmentId = evm.Department_id,
                                        EmployeeId = evm.Employee_ID
                                    };

                                    _context.DepartmentEmployee.Add(newDepartmentEmployee);
                                    
                                }

                                var depmanager = _context.Department.FirstOrDefault(x => x.ManagerId == employee.Employee_ID);
                                if(depmanager != null)
                                {
                                    depmanager.ManagerId = null;
                                    _context.Update(depmanager);
                                }

                                transaction.Commit();
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }



                        user.UserName = evm.UserName;
                        user.Password = evm.Password;
                        user.Status = evm.Status;
                        user.updated_at = DateTime.Now;

                        // Xóa các role cũ của người dùng
                        var existingUserRoles = _context.UserRole.Where(ur => ur.UserID == user.UserID);
                        _context.UserRole.RemoveRange(existingUserRoles);


                        foreach (var roleId in evm.Role_IDs)
                        {
                            var userRole = new UserRole
                            {
                                UserID = user.UserID,
                                RoleID = roleId
                            };
                            _context.UserRole.Add(userRole);
                        }

                        _context.Entry(employee).State = EntityState.Modified;
                        _context.Entry(user).State = EntityState.Modified;

                        var rs = await _context.SaveChangesAsync();
                        if (rs > 0)
                        {
                            _messageService.SetMessage("Cập nhật thành công");
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(evm.Employee_ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            _messageService.SetMessage("Cập nhật thất bại", "error");
                            throw;
                        }
                    }
                }
            }

            
            ViewData["Department_ID"] = new SelectList(_context.Department, "Department_id", "DepartmentName", evm.Department_id);
            ViewData["Title_ID"] = new SelectList(_context.Title, "Title_id", "Title_name", evm.Title_id);
            ViewData["Role_ID"] = new SelectList(_context.Role, "Role_ID", "Role_ID", evm.Role_IDs);
            return View(evm);
        }

        // GET: Users/Delete/5
        [HttpGet("xoa-tai-khoan/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(x => x.EmployeeUs)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(m => m.Employee_ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost("xoa-tai-khoan/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.User.Include(x => x.EmployeeUs).FirstOrDefaultAsync(x => x.Employee_ID == id);
            if (user != null)
            {
                var employee = user.EmployeeUs;
                if (employee != null)
                {

                    _context.User.Remove(user);
                    _context.Employee.Remove(employee);

                }
            }

            int affectedRows = await _context.SaveChangesAsync();
            if (affectedRows > 0)
            {
                _messageService.SetMessage("Xoá thành công");
            }
            else
            {
                _messageService.SetMessage("Xoá thất bại", "error");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Employee_ID == id);
        }
    }
}
