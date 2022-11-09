using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
   [TestFixture]
    public class HousekeeperServiceTests_SendStatementEmails
    {
        #region SetUp
        private HousekeeperService _service;
        private Mock<IHousekeeperRepository> _repo;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailHelper> _emailHelper;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private DateTime _statementDate = DateTime.Today;
        private string _statementFileName;
        private IQueryable<Housekeeper> _housekeepers;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IHousekeeperRepository>();

            _statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator.Setup(sg => sg.SaveStatement(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>())
            ).Returns(() => _statementFileName);

            _emailHelper = new Mock<IEmailHelper>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();
            _service = new HousekeeperService(
                _repo.Object,
                _statementGenerator.Object,
                _emailHelper.Object,
                _xtraMessageBox.Object);

            _housekeepers = new List<Housekeeper>()
            {
                new Housekeeper()
                {
                    Oid = 1,
                    Email = "housekeeper1@email.com",
                    FullName = "Housekeeper 1",
                    StatementEmailBody = "Email body for Housekeeper 1"
                },
                new Housekeeper()
                {
                    Oid = 2,
                    Email = "housekeeper2@email.com",
                    FullName = "Housekeeper 2",
                    StatementEmailBody = "Email body for Housekeeper 2"
                }
            }.AsQueryable();
            _repo.Setup(r => r.GetAll()).Returns(_housekeepers);
        }
        #endregion

        #region Test Methods
        [Test]
        public void WhenCalled_SaveStatement()
        {
            //Act
            _service.SendStatementEmails(_statementDate);

            //Assert
            foreach (var h in _housekeepers)
            {
                _statementGenerator.Verify(sg => sg.SaveStatement(h.Oid, h.FullName, _statementDate));
            }
        }

        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void HousekeeperHasNoEmail_ShouldNotSaveStatement(string value)
        {
            //Arrange
            var h = _housekeepers.First();
            h.Email = value;

            //Act
            _service.SendStatementEmails(_statementDate);

            //Assert
            _statementGenerator.Verify(sg => sg.SaveStatement(h.Oid, h.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void WhenCalled_EmailStatement()
        {
            //Act
            _service.SendStatementEmails(_statementDate);

            //Assert
            foreach (var h in _housekeepers)
            {
                _emailHelper.Verify(eh => eh.EmailFile(
                    h.Email,
                    h.StatementEmailBody,
                    _statementFileName,
                    It.IsAny<string>()) //we don't use the exact same string as it might change in the future.
                );
            }
        }

        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void NoStatementFileName_ShouldNotEmailStatement(string value)
        {
            //Arrange
            _statementFileName = value;

            //Act
            _service.SendStatementEmails(_statementDate);

            //Assert
            _emailHelper.Verify(eh => eh.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);
        }

        [Test]
        public void SaveStatementFails_DisplayAMessageBox()
        {
            //Arrange
            _statementGenerator.Setup(sg => sg.SaveStatement(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>())
            ).Throws<Exception>();

            //Act
            _service.SendStatementEmails(_statementDate);

            //Assert
            VerifyMessageBoxWasDisplayed();
        }

        [Test]
        public void SendEmailFails_DisplayAMessageBox()
        {
            //Arrange
            _emailHelper.Setup(eh => eh.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())
            ).Throws<Exception>();

            //Act
            _service.SendStatementEmails(_statementDate);

            //Assert
            VerifyMessageBoxWasDisplayed();
        } 
        #endregion

        #region Helpers
        private void VerifyMessageBoxWasDisplayed()
        {
            _xtraMessageBox.Verify(mb => mb.Show(
                It.IsAny<string>(),
                It.IsAny<string>(),
                MessageBoxButtons.OK)
            , Times.Exactly(_housekeepers.Count()));
        } 
        #endregion
    }
}