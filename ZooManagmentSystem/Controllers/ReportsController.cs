using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Report;
using ZooManagmentSystem.Models.Client;
using System.Text.Json;
using ZooManagmentSystem.DTOs.Reports;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace ZooManagmentSystem.Controllers
{
    [Route("report")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReports()
        {
            var reports = await _context.Reports
            .Select(r => new ReportDto
            {
                Title = r.Title,
                Content = r.Content,
                Type = r.Type,
                CreatedAt = DateTime.Now,
                AuthorId = r.AuthorId,
            })
                .ToListAsync();
            return reports;
        }

        [Route("getOne/{id}")]
        [HttpGet]
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


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("new/forManager")]
        [HttpPost]
        public async Task<ActionResult> PostReportForManager(ReportCreateDto reportDto)
        {
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
            else
            {
                return BadRequest(new { message = "Report type not supported yet." });
            }

            reportModel.Content = JsonSerializer.Serialize(contentObj);

            _context.Reports.Add(reportModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Report generated" });
        }

        [Route("new/forEmployee")]
        [HttpPost]
        public async Task<ActionResult> PostReportForEmployee(ReportCreateDto reportDto)
        {
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
                //contentObj = GenerateFeedingPlanReport();
                return BadRequest(new { message = "Report type not supported yet." });
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

        [Route("delete/{id}")]
        [HttpDelete]
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
    }

    
}
