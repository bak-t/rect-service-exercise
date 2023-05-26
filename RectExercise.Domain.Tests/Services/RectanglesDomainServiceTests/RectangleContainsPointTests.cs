using RectExercise.Domain.Contract.Models;
using RectExercise.Domain.Implementation.Services;

namespace RectExercise.Domain.Tests.Services.RectanglesDomainServiceTests
{
    [TestClass]
    public class RectangleContainsPointTests
    {
        private readonly RectanglesDomainService _sut = new RectanglesDomainService();

        [TestMethod]
        public void Should_return_true_for_point_inside_rectangle()
        {
            var rect = new Rectangle { Left = 1, Top = 2, Right = 5, Bottom = 10 };

            Assert.IsTrue(_sut.RectangleContainsPoint(rect, 3, 8));
        }

        [TestMethod]
        [DataRow(0, 1)]
        [DataRow(6, 11)]
        [DataRow(4, 11)]
        [DataRow(6, 9)]
        public void Should_return_false_for_point_outside_rectangle(int x, int y)
        {
            var rect = new Rectangle { Left = 1, Top = 2, Right = 5, Bottom = 10 };

            Assert.IsFalse(_sut.RectangleContainsPoint(rect, x, y));
        }

        [TestMethod]
        [DataRow(1, 2, DisplayName = "Top left corner")]
        [DataRow(5, 10, DisplayName = "Bottom right corner")]
        [DataRow(5, 2, DisplayName = "Top right corner")]
        [DataRow(1, 10, DisplayName = "Bottom left corner")]
        [DataRow(1, 3, DisplayName = "Left border")]
        [DataRow(5, 9, DisplayName = "Right border")]
        [DataRow(2, 2, DisplayName = "Top border")]
        [DataRow(2, 10, DisplayName = "Bottom border")]
        public void Should_return_true_for_point_rectangle_border(int x, int y)
        {
            var rect = new Rectangle { Left = 1, Top = 2, Right = 5, Bottom = 10 };

            Assert.IsTrue(_sut.RectangleContainsPoint(rect, x, y));
        }

        [TestMethod]
        public void Should_throw_exception_when_rectangle_is_null()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => _sut.RectangleContainsPoint(null, 1, 2));
            Assert.AreEqual("rect", exception.ParamName);
        }
    }
}