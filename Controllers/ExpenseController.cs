using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RASCHET_HASHODOV.Data;
using RASCHET_HASHODOV.Models;
using RASCHET_HASHODOV.ViewModels;
using System.Linq;
using System.Security.Claims;

[Authorize]
public class ExpenseController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExpenseController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        var categories = _context.Categories.ToList();  // Получаем список категорий
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Получаем UserId

        // Можно получить пользователя, если нужно его имя или другие данные
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);

        var viewModel = new ExpenseFormViewModel
        {
            Categories = categories,  // Список категорий
                                      // Здесь можно передать пользователя, если нужно
                                      // UserId = userId, можно добавить в модель для отображения в форме
        };

        ViewBag.Categories = new SelectList(categories, "Id", "Name");  // Передаем список категорий в ViewBag

        return View(viewModel);
    }

    // Обработка POST-запроса на создание расхода
    [HttpPost]
    public IActionResult Create(ExpenseFormViewModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Получаем UserId
        if (userId == null)
        {
            return Unauthorized();  // Если UserId не найден, возвращаем ошибку
        }

        var expense = new Expense
        {
            Amount = model.Amount,
            Description = model.Description,
            Date = model.Date,
            CategoryId = model.CategoryId,
            UserId = userId
        };

        _context.Expenses.Add(expense);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    // Страница редактирования расхода
    public IActionResult Edit(int id)
    {
        var expense = _context.Expenses
            .Include(e => e.Category)  // Загружаем категорию
            .Include(e => e.User)      // Загружаем пользователя
            .FirstOrDefault(e => e.Id == id);

        if (expense == null || expense.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            return NotFound();

        var viewModel = new ExpenseFormViewModel
        {
            Id = expense.Id,
            Amount = expense.Amount,
            Description = expense.Description,
            Date = expense.Date,
            CategoryId = expense.CategoryId,  // Заполняем выбранную категорию
            Categories = _context.Categories.ToList(),  // Загружаем все категории
            UserId = expense.UserId,
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(ExpenseFormViewModel model)
    {
        var expense = _context.Expenses.Find(model.Id);
        if (expense == null || expense.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            return NotFound();

        expense.Amount = model.Amount;
        expense.Description = model.Description;
        expense.CategoryId = model.CategoryId;  // Обновляем категорию
        expense.Date = model.Date;

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Получаем все расходы текущего пользователя
        var expenses = _context.Expenses
            .Where(e => e.UserId == userId)
            .Include(e => e.Category)  // Загружаем категорию
            .Include(e => e.User)      // Загружаем пользователя
            .ToList();

        // Общая сумма расходов
        var totalAmount = expenses.Sum(e => e.Amount);

        // Расходы за текущий месяц
        var currentMonthExpenses = expenses
            .Where(e => e.Date.Month == DateTime.Today.Month && e.Date.Year == DateTime.Today.Year)
            .Sum(e => e.Amount);

        // Создаем ViewModel для передачи данных в представление
        var viewModel = new ExpensesViewModel
        {
            TotalAmount = totalAmount,
            CurrentMonthAmount = currentMonthExpenses,
            Expenses = expenses
        };

        return View(viewModel);
    }
    public IActionResult GetCategoryStats()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var expenses = _context.Expenses
            .Where(e => e.UserId == userId)
            .Include(e => e.Category)
            .ToList();

        var categoryStats = expenses
            .GroupBy(e => e.Category)
            .Select(group => new
            {
                CategoryName = group.Key.Name,
                TotalSpent = group.Sum(e => e.Amount),
                CurrentMonthSpent = group
                    .Where(e => e.Date.Month == DateTime.Today.Month && e.Date.Year == DateTime.Today.Year)
                    .Sum(e => e.Amount)
            })
            .ToList();

        return Json(categoryStats);
    }


    [HttpPost]
    public IActionResult Delete(int id)
    {
        var expense = _context.Expenses.Find(id);
        if (expense == null || expense.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            return NotFound();

        _context.Expenses.Remove(expense);
        _context.SaveChanges();

        return Json(new { success = true });
    }
}
