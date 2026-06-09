using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.DTOs.Reports;
using ZooManagmentSystem.Models.Animal;
using ZooManagmentSystem.Models.Client;
using ZooManagmentSystem.Models.Report;

namespace ZooManagmentSystem.Controllers
{
    [Route("report")]
    [ApiController]
    [Authorize(Roles = "Manager, Employee")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<ReportCreateDto> _validator;

        public ReportsController(AppDbContext context, IValidator<ReportCreateDto> validator)
        {
            _context = context;
            _validator = validator;
        }

        [Route("")]
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReports()
        {
            var reports = await _context.Reports
            .Select(r => new ReportDto
            {
                Id = r.id,
                Title = r.Title,
                Content = r.Content,
                Type = r.Type,
                CreatedAt = r.CreatedAt,
                AuthorId = r.AuthorId,
            })
                .ToListAsync();
            return reports;
        }

        [Route("forEmployee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReportsForEmployee()
        {
            int employeeId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value ?? "0");

            var reports = await _context.Reports
            .Where(r => r.AuthorId == employeeId)
            .Select(r => new ReportDto
            {
                Id = r.id,
                Title = r.Title,
                Content = r.Content,
                Type = r.Type,
                CreatedAt = r.CreatedAt,
                AuthorId = r.AuthorId,
            })
                .ToListAsync();
            return reports;
        }


        [Route("{id}")]
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<ReportDto>> GetReportModel(int id)
        {
            var reportModel = await _context.Reports.FindAsync(id);

            if (reportModel == null)
            {
                return NotFound();
            }

            return Ok(new ReportDto
            {
                Title = reportModel.Title,
                Content = reportModel.Content,
                Type = reportModel.Type,
                CreatedAt = DateTime.Now,
                AuthorId = reportModel.AuthorId,
            });
        }

        [Route("types")]
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetReportTypes()
        {
            var types = Enum.GetValues(typeof(ReportType))
                .Cast<ReportType>()
                .Select(t => new { Id = (int)t, Name = t.ToString() })
                .ToList();

            return Ok(types);
        }

        [Route("types/forEmployee")]
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetReportTypesForEmployee()
        {
            var allowedTypes = new[] { ReportType.FeedingPlan };

            var types = allowedTypes
                .Select(t => new { Id = (int)t, Name = t.ToString() })
                .ToList();

            return Ok(types);
        }


        [Route("forManager")]
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> PostReportForManager(ReportCreateDto reportDto)
        {
            var validation = await _validator.ValidateAsync(reportDto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var reportModel = new ReportModel
            {
                Title = reportDto.Title,
                Type = reportDto.Type,
                CreatedAt = DateTime.Now,
                AuthorId = reportDto.AuthorId
            };

            object contentObj = null;
            if (reportDto.Type == ReportType.VisitorStatistics)
            {
                contentObj = GenerateVisitorStatisticsReport();
            }
            else if (reportDto.Type == ReportType.FeedingPlan)
            {
                contentObj = GenerateFeedingPlanReport();
            }
            else
            {
                return BadRequest(new { message = "Report type not supported yet." });
            }

            reportModel.Content = JsonSerializer.Serialize(contentObj);

            _context.Reports.Add(reportModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Report generated" });
        }

        [Route("forEmployee")]
        [HttpPost]
        public async Task<ActionResult> PostReportForEmployee(ReportCreateDto reportDto)
        {
            var validation = await _validator.ValidateAsync(reportDto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            int employeeId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value ?? "0");
            if (reportDto.AuthorId != employeeId)
            {
                return BadRequest(new { message = "You are not authorized to create this report." });
            }

            var reportModel = new ReportModel
            {
                Title = reportDto.Title,
                Type = reportDto.Type,
                CreatedAt = DateTime.Now,
                AuthorId = reportDto.AuthorId
            };

            object contentObj = null;
            if (reportDto.Type == ReportType.FeedingPlan)
            {
                contentObj = GenerateFeedingPlanReport();
            }
            else
            {
                return BadRequest(new { message = "Report type not supported yet." });
            }

            reportModel.Content = JsonSerializer.Serialize(contentObj);

            _context.Reports.Add(reportModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Report generated" });
        }

        [Route("{id}")]
        [HttpDelete]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteReportModel(int id)
        {
            var reportModel = await _context.Reports.FindAsync(id);
            if (reportModel == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(reportModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted report." });
        }

        [Route("generatePDF/{id}")]
        [HttpGet]
        [EnableRateLimiting("pdf")]
        public async Task<IActionResult> GeneratePDF(int id)
        {

            var report = await _context.Reports.FindAsync(id);
            if (report == null) return NotFound();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Header().Text(report.Title)
                        .FontSize(28).Bold().AlignCenter().FontColor(Colors.LightGreen.Darken1);
                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Generated at: {report.CreatedAt:dd.MM.yyyy}");
                        col.Item().Text($"Type: {report.Type}");
                        col.Item().PaddingTop(20).Text("Data:")
                        .FontSize(18).Bold().FontColor(Colors.LightGreen.Darken3);

                        // Render structured report content depending on report type
                        if (report.Type == ReportType.VisitorStatistics)
                        {
                            VisitorStatisticsDto dto = null;
                            try
                            {
                                dto = JsonSerializer.Deserialize<VisitorStatisticsDto>(report.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            }
                            catch
                            {
                                dto = null;
                            }

                            if (dto != null)
                            {
                                col.Item().Text($"Month: {dto.Month}");
                                col.Item().Text($"Total Visitors: {dto.TotalVisitors}");
                                col.Item().Text($"Total Revenue: {dto.TotalRevenue}");
                                col.Item().PaddingTop(10).Text("Details by Entry Type:")
                                    .FontSize(18).Bold().FontColor(Colors.LightGreen.Darken3);

                                foreach (var kvp in dto.EntryTypeCounts)
                                {
                                    var percent = dto.Percentages != null && dto.Percentages.ContainsKey(kvp.Key) ? dto.Percentages[kvp.Key] : 0m;
                                    col.Item().Text($"{kvp.Key}: {kvp.Value} ({percent}%)");
                                }
                            }
                            else
                            {
                                // Fallback: print raw content
                                col.Item().Text(report.Content);
                            }
                        }
                        else if(report.Type == ReportType.FeedingPlan)
                        {
                            FeedingPlanDto dto = null;
                            try
                            {
                                dto = JsonSerializer.Deserialize<FeedingPlanDto>(report.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            }
                            catch
                            {
                                dto = null;
                            }
                            if (dto != null)
                            {
                                col.Item().Text($"Plan for: {dto.date}");
                                col.Item().Text($"Animals to feed today: {dto.AnimalCount}");
                                col.Item().PaddingTop(10).Text("Food needed:")
                                    .FontSize(18).Bold().FontColor(Colors.LightGreen.Darken3);

                                foreach (var foodType in dto.FoodNeeded)
                                {
                                    col.Item().Text($"{foodType.Key}: {foodType.Value}");
                                }

                                col.Item().PaddingTop(10).Text("Feeding plan:")
                                    .FontSize(18).Bold().FontColor(Colors.LightGreen.Darken3);


                                var feedingDetails = dto.FeedingDetails
                                    .OrderByDescending(f => f.quantity)
                                    .ToList();
                                foreach (var details in feedingDetails)
                                {
                                    col.Item().Text($"{details.animalName}")
                                        .FontSize(16).Bold();
                                    col.Item().Text($"Enclosure: {details.enclosureName}");
                                    col.Item().Text($"Food: {details.foodType}");
                                    col.Item().Text($"Quantity: {details.quantity} servings of {details.serving} each");
                                    col.Spacing(5);
                                }
                            }
                            else
                            {
                                // Fallback: print raw content
                                col.Item().Text(report.Content);
                            }
                        }
                        else
                        {
                            // Generic fallback for other report types
                            col.Item().Text(report.Content);
                        }
                    });
                    page.Footer().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
                });
            });

            var pdfBytes = pdf.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"{report.Title}.pdf");
        }



        private bool ReportModelExists(int id)
        {
            return _context.Reports.Any(e => e.id == id);
        }

        private VisitorStatisticsDto GenerateVisitorStatisticsReport()
        {
            DateTime now = DateTime.Now;
            var tickets = _context.Tickets
                .Where(t => t.PurchaseDate.Month == now.Month && t.PurchaseDate.Year == now.Year)
                .Include(t => t.EntryTypes)
                .ToList();
            var entryTypes = _context.EntryTypes.ToList();

            Dictionary<string, int> entryTypeCounts = new Dictionary<string, int>();
            foreach (var entryType in entryTypes)
            {
                entryTypeCounts[entryType.TypeName] = 0;
            }
            entryTypeCounts["Unknown"] = 0;

            foreach (TicketModel ticket in tickets)
            {
                foreach (var entryType in ticket.EntryTypes)
                {
                    var entryTypeName = entryTypes.FirstOrDefault(et => et.id == entryType.EntryTypeId)?.TypeName;
                    if (entryTypeName != null && entryTypeCounts.ContainsKey(entryTypeName))
                    {
                        entryTypeCounts[entryTypeName] += entryType.Quantity;
                    }
                    else
                    {
                        entryTypeCounts["Unknown"] += entryType.Quantity;
                    }
                }
            }

            int totalVisitors = entryTypeCounts.Values.Sum();

            Dictionary<string, decimal> percentage = new Dictionary<string, decimal>();
            if (totalVisitors > 0)
            {
                foreach (var kvp in entryTypeCounts)
                {
                    percentage[kvp.Key] = Math.Round((decimal)kvp.Value / totalVisitors * 100, 2);
                }
            }
            else
            {
                foreach (var kvp in entryTypeCounts)
                {
                    percentage[kvp.Key] = 0m;
                }
                percentage["Unknown"] = 0m;
            }

            decimal totalRevenue = tickets.Sum(t => t.Price);

            var dto = new VisitorStatisticsDto
            {
                Month = now.ToString("MMMM yyyy"),
                EntryTypeCounts = entryTypeCounts,
                Percentages = percentage,
                TotalVisitors = totalVisitors,
                TotalRevenue = totalRevenue
            };

            return dto;
        }

        private FeedingPlanDto GenerateFeedingPlanReport()
        {
            DateTime now = DateTime.Now;

            int employeeId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value ?? "0");
            var employee = _context.Employees.Find(employeeId);
            if (employee == null)
            {
                return null;
            }

            List<AnimalModel> animals = _context.Animals
                .Where(a => a.FeedingEmployeeId == employeeId)
                .Include(a => a.Food)
                .ToList();

            if(animals.Count == 0)
            {
                Console.WriteLine("No animals found for employee");
                return null;
            }

            Dictionary<string, decimal> foodNeeded = new Dictionary<string, decimal>();
            List<FeedingDetails> feedingDetails = new List<FeedingDetails>();

            FeedingPlanDto feedingPlan = new FeedingPlanDto
            {
                date = now.ToString("dd.MM.yyyy"),
                AnimalCount = animals.Count,
            };

            foreach (var animal in animals) 
            {
                FeedingDetails details = new FeedingDetails
                {
                    animalName = animal.Name,
                    enclosureName = animal.Enclosure != null ? animal.Enclosure.Name : "Unknown",
                    quantity = animal.FeedingsPerDay ?? 0,
                    serving = animal.AmountPerFeeding ?? 0m,
                    foodType = animal.Food != null ? animal.Food.FoodName : "Unknown"
                };
                feedingDetails.Add(details);

                if (animal.Food != null)
                {
                    if (foodNeeded.ContainsKey(animal.Food.FoodName))
                    {
                        foodNeeded[animal.Food.FoodName] += details.quantity * details.serving;
                    }
                    else
                    {
                        foodNeeded[animal.Food.FoodName] = details.quantity * details.serving;
                    }
                }
            }
            feedingPlan.FoodNeeded = foodNeeded;
            feedingPlan.FeedingDetails = feedingDetails;


            return feedingPlan;
        }

    }
}
