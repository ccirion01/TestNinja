using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TestNinja.Mocking
{
    public class HousekeeperService
    {
        private readonly IHousekeeperRepository _repo;
        private readonly IStatementGenerator _statementGenerator;
        private readonly IEmailHelper _emailHelper;
        private readonly IXtraMessageBox _xtraMessageBox;

        public HousekeeperService(
            IHousekeeperRepository repo,
            IStatementGenerator statementGenerator,
            IEmailHelper emailHelper,
            IXtraMessageBox xtraMessageBox)
        {
            _repo = repo;
            _statementGenerator = statementGenerator;
            _emailHelper = emailHelper;
            _xtraMessageBox = xtraMessageBox;
        }        

        public void SendStatementEmails(DateTime statementDate)
        {
            var housekeepers = _repo.GetAll();

            foreach (var housekeeper in housekeepers)
            {
                if (string.IsNullOrWhiteSpace(housekeeper.Email))
                    continue;

                try
                {
                    var statementFilename = _statementGenerator.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

                    if (string.IsNullOrWhiteSpace(statementFilename))
                        continue;

                    var emailAddress = housekeeper.Email;
                    var emailBody = housekeeper.StatementEmailBody;

                    try
                    {
                        _emailHelper.EmailFile(emailAddress, emailBody, statementFilename,
                            string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
                    }
                    catch (Exception e)
                    {
                        _xtraMessageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress),
                            MessageBoxButtons.OK);
                    }
                }
                catch (Exception e)
                {
                    _xtraMessageBox.Show(e.Message, string.Format("Save statement failure for: {0}", housekeeper.FullName),
                        MessageBoxButtons.OK);
                }                
            }
        }
    }

    public enum MessageBoxButtons
    {
        OK
    }    

    public class MainForm
    {
        public bool HousekeeperStatementsSending { get; set; }
    }

    public class DateForm
    {
        public DateForm(string statementDate, object endOfLastMonth)
        {
        }

        public DateTime Date { get; set; }

        public DialogResult ShowDialog()
        {
            return DialogResult.Abort;
        }
    }

    public enum DialogResult
    {
        Abort,
        OK
    }

    public class SystemSettingsHelper
    {
        public static string EmailSmtpHost { get; set; }
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailFromEmail { get; set; }
        public static string EmailFromName { get; set; }
    }

    public class Housekeeper
    {
        public string Email { get; set; }
        public int Oid { get; set; }
        public string FullName { get; set; }
        public string StatementEmailBody { get; set; }
    }

    public class HousekeeperStatementReport
    {
        public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
        {
        }

        public bool HasData { get; set; }

        public void CreateDocument()
        {
        }

        public void ExportToPdf(string filename)
        {
        }
    }
}