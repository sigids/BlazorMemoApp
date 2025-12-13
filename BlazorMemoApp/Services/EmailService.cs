using System.Net;
using System.Net.Mail;
using System.Text;
using BlazorMemoApp.Data;
using BlazorMemoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMemoApp.Services;

public class EmailService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IServiceProvider serviceProvider, ILogger<EmailService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<bool> SendMemoApprovalNotificationAsync(MemoHeaderModel memo, string recipientEmail)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            var settings = await db.EmailSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                _logger.LogWarning("Email settings not configured");
                return false;
            }

            var subject = $"Memo {memo.MemoNo} approved";
            var body = BuildMemoDetailHtml(memo);

            using var client = new SmtpClient(settings.SmtpHost, settings.SmtpPort)
            {
                Credentials = new NetworkCredential(settings.Username, settings.Password),
                EnableSsl = settings.EnableSsl
            };

            var message = new MailMessage
            {
                From = new MailAddress(settings.SenderEmail, settings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(recipientEmail);

            await client.SendMailAsync(message);
            _logger.LogInformation("Approval notification sent for memo {MemoNo} to {Email}", memo.MemoNo, recipientEmail);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send approval notification for memo {MemoNo}", memo.MemoNo);
            return false;
        }
    }

    private static string BuildMemoDetailHtml(MemoHeaderModel memo)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<html><body>");
        sb.AppendLine($"<h2>Memo {memo.MemoNo} has been approved</h2>");
        sb.AppendLine($"<p><strong>Memo Date:</strong> {memo.MemoDate:yyyy-MM-dd}</p>");
        sb.AppendLine($"<p><strong>PO Number:</strong> {memo.PONumber}</p>");
        sb.AppendLine($"<p><strong>Garment Qty:</strong> {memo.GmtQty}</p>");
        sb.AppendLine($"<p><strong>FOB Rate:</strong> {memo.GmtFobRate}</p>");
        sb.AppendLine($"<p><strong>Approval Date:</strong> {memo.ApproveDate:yyyy-MM-dd HH:mm}</p>");
        
        if (memo.Details != null && memo.Details.Any())
        {
            sb.AppendLine("<h3>Memo Details</h3>");
            sb.AppendLine("<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse;'>");
            sb.AppendLine("<tr style='background-color: #f0f0f0;'>");
            sb.AppendLine("<th>Article</th><th>Color</th><th>Size</th><th>Price</th><th>Currency</th><th>Unit</th>");
            sb.AppendLine("<th>BOM Qty</th><th>Avail Stock</th><th>MCQ Qty</th><th>Purchase Qty</th>");
            sb.AppendLine("<th>BOM Amount</th><th>Purchase Amount</th><th>MCQ Amount</th><th>Diff</th>");
            sb.AppendLine("</tr>");

            foreach (var detail in memo.Details)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{detail.Article}</td>");
                sb.AppendLine($"<td>{detail.Color}</td>");
                sb.AppendLine($"<td>{detail.Size}</td>");
                sb.AppendLine($"<td style='text-align: right;'>{detail.Price:N2}</td>");
                sb.AppendLine($"<td>{detail.Currency}</td>");
                sb.AppendLine($"<td>{detail.Unit}</td>");
                sb.AppendLine($"<td style='text-align: right;'>{detail.BOMQty:N0}</td>");
                sb.AppendLine($"<td style='text-align: right;'>{detail.AvailStockQty:N0}</td>");
                sb.AppendLine($"<td style='text-align: right;'>{detail.MCQQty:N0}</td>");
                sb.AppendLine($"<td style='text-align: right;'>{detail.PurchaseQty:N0}</td>");
                sb.AppendLine($"<td style='text-align: right;'>{detail.BOMAmount:N2}</td>");
                sb.AppendLine($"<td style='text-align: right;'>{detail.PurchaseAmount:N2}</td>");
                sb.AppendLine($"<td style='text-align: right;'>{detail.MCQAmount:N2}</td>");
                sb.AppendLine($"<td style='text-align: right;'>{detail.Diff:N2}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
        }

        sb.AppendLine("<br/><p>This is an automated notification. Please do not reply to this email.</p>");
        sb.AppendLine("</body></html>");
        return sb.ToString();
    }
}
