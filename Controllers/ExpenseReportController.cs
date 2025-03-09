using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RASCHET_HASHODOV.Data;
using RASCHET_HASHODOV.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RASCHET_HASHODOV.Controllers
{
	public class ExpenseReportController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ExpenseReportController(ApplicationDbContext context)
		{
			_context = context;
		}

		// 📌 Просмотр списка отчётов
		public async Task<IActionResult> Index()
		{
			string userId = User.Identity.Name;

			if (!User.Identity.IsAuthenticated)
			{
				return Unauthorized();
			}

			var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
			if (user == null)
			{
				return NotFound("Пользователь не найден.");
			}

			var reports = await _context.ExpenseReports
				.Where(r => r.UserId == user.Id)
				.OrderByDescending(r => r.Period)
				.ToListAsync();

			return View(reports);
		}

		// 📌 Генерация отчёта о расходах
		public async Task<IActionResult> GenerateReport()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return Unauthorized();
			}

			string userId = User.Identity.Name;
			var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
			if (user == null)
			{
				return NotFound("Пользователь не найден.");
			}

			DateTime now = DateTime.UtcNow;
			int currentMonth = now.Month;
			int previousMonth = currentMonth == 1 ? 12 : currentMonth - 1;
			int currentYear = now.Year;
			int previousYear = currentMonth == 1 ? currentYear - 1 : currentYear;

			var currentMonthExpenses = await _context.Expenses
				.Where(e => e.UserId == user.Id && e.Date.Year == currentYear && e.Date.Month == currentMonth)
				.SumAsync(e => (decimal?)e.Amount) ?? 0;

			var previousMonthExpenses = await _context.Expenses
				.Where(e => e.UserId == user.Id && e.Date.Year == previousYear && e.Date.Month == previousMonth)
				.SumAsync(e => (decimal?)e.Amount) ?? 0;

			decimal percentageChange = previousMonthExpenses > 0
				? ((currentMonthExpenses - previousMonthExpenses) / previousMonthExpenses) * 100
				: 0;

			string message = percentageChange switch
			{
				>= -5 and <= 5 => "Ваши расходы остались примерно на том же уровне.",
				> 5 and <= 20 => "Вы потратили немного больше, чем в прошлом месяце.",
				> 20 and <= 50 => "Ваши расходы заметно увеличились, стоит задуматься о перераспределении бюджета.",
				> 50 and <= 100 => "Вы потратили значительно больше, чем в прошлом месяце!",
				> 100 => "Ваши расходы увеличились более чем на 100%!! Возможно, стоит пересмотреть бюджет.",
				< -5 and >= -20 => "Вы немного сэкономили в этом месяце, продолжайте в том же духе!",
				< -20 and >= -50 => "Ваши расходы заметно снизились, отличная работа по контролю бюджета!",
				< -50 and >= -100 => "Вы значительно сократили расходы! Возможно, можно немного расслабиться.",
				_ => "Ваши расходы сократились более чем на 100%! Проверьте, не пропустили ли вы какие-то обязательные платежи."
			};

			var report = new ExpenseReport
			{
				UserId = user.Id,
				Period = now.ToString("MMMM yyyy"),
				TotalAmount = currentMonthExpenses
			};

			_context.ExpenseReports.Add(report);
			await _context.SaveChangesAsync();

			ViewBag.PercentageChange = percentageChange;
			ViewBag.Message = message;

			return View(report);
		}

		// 📌 Детали отчёта
		public async Task<IActionResult> Details(int id)
		{
			var report = await _context.ExpenseReports.FirstOrDefaultAsync(r => r.Id == id);
			if (report == null)
			{
				return NotFound();
			}
			return View(report);
		}

		// 📌 Удаление отчёта
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var report = await _context.ExpenseReports.FindAsync(id);
			if (report == null)
			{
				return NotFound();
			}

			_context.ExpenseReports.Remove(report);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
