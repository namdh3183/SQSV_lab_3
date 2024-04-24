using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SQSV_lab_3.Controllers;
using SQSV_lab_3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<DbSet<BOOK>> _mockSet;
        private Mock<Model1> _mockContext;
        private BookController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var data = new List<BOOK>
            {
                new BOOK {
                    ID = 1,
                    TITLE = "Test Title 1",
                    AUTHOR = "Test Author 1",
                    GENRE = "Test Genre 1"
                },
                new BOOK {
                    ID = 2,
                    TITLE = "Test Title 2",
                    AUTHOR = "Test Author 2",
                    GENRE = "Test Genre 2" },
                new BOOK {
                    ID = 3,
                    TITLE = "Test Title 3",
                    AUTHOR = "Test Author 3",
                    GENRE = "Test Genre 3"},

            }.AsQueryable();

            _mockSet = new Mock<DbSet<BOOK>>();
            _mockSet.As<IQueryable<BOOK>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<BOOK>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<BOOK>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<BOOK>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext = new Mock<Model1>();
            _mockContext.Setup(c => c.BOOKs).Returns(_mockSet.Object);
            _mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => data.FirstOrDefault(d => d.ID == (int)ids[0]));

            _controller = new BookController(_mockContext.Object);
        }

        [TestMethod]
        public void Get_ReturnsAllBooks()
        {
            var result = _controller.Get();
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void Get_WithId_ReturnsCorrectBook()
        {
            var result = _controller.Get(1);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Post_AddsNewBook()
        {
            var newBook = new BOOK
            {
                TITLE = "Test Title 4",
                AUTHOR = "Test Author 4",
                GENRE = "Test Genre 4"
            };
            _controller.Post(newBook);

            _mockSet.Verify(m => m.Add(It.IsAny<BOOK>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Put_UpdatesExistingBook()
        {
            var existingBook = new BOOK
            {
                ID = 5,
                TITLE = "Test Title 5",
                AUTHOR = "Test Author 5",
                GENRE = "Test Genre 5"
            };

            _controller.Put(existingBook.ID, existingBook);

            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }




        [TestMethod]
        public void Delete_RemovesBook()
        {
            var existingBook = new BOOK
            {
                ID = 6,
                TITLE = "Test Title 6",
                AUTHOR = "Test Author 6",
                GENRE = "Test Genre 6"
            };
            _controller.Delete(existingBook.ID);

            _mockSet.Verify(m => m.Remove(It.IsAny<BOOK>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
