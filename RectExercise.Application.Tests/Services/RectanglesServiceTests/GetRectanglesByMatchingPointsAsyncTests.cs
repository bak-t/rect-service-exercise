using RectExercise.Application.Contract.DTO;
using RectExercise.Application.Implementation.Services;
using RectExercise.Data.Contract.Repositories;
using RectExercise.Domain.Contract.Models;
using RectExercise.Domain.Contract.Services;

namespace RectExercise.Application.Tests.Services.RectanglesServiceTests
{
    [TestClass]
    public class GetRectanglesByMatchingPointsAsyncTests
    {
        private readonly RectanglesService _sut;
        private readonly Mock<IRectanglesRepository> _rectanglesRepositoryMock = new();
        private readonly Mock<IRectanglesDomainService> _rectangleDomainServiceMock = new();
        private readonly Fixture _fixture = new();

        public GetRectanglesByMatchingPointsAsyncTests()
        {
            _sut = new RectanglesService(_rectanglesRepositoryMock.Object, _rectangleDomainServiceMock.Object);

            _rectanglesRepositoryMock
                .Setup(x => x.GetRectanglesByMatchingPointsAsync(
                    It.IsAny<IReadOnlyList<PointDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Rectangle[0]);
        }

        [TestMethod]
        public async Task Should_return_empty_matches_for_non_matching_points()
        {
            // Arrange
            var points = _fixture.CreateMany<PointDto>().ToList();

            // Act
            var result = (await _sut.GetRectanglesByMatchingPointsAsync(points, CancellationToken.None)).ToList();

            // Assert
            Assert.AreEqual(points.Count, result.Count);
            CollectionAssert.AreEquivalent(points, result.Select(x => x.Point).ToList());
            Assert.IsFalse(result.Any(x => x.MatchingRectangles?.Count > 0), "All MatchingRectangles properties must be empty when there are no matches");
        }

        [TestMethod]
        public async Task Should_return_proper_matches_for_corresponding_points()
        {
            // Arrange
            var points = _fixture.CreateMany<PointDto>(3).ToList();
            var firstMatchingRects = _fixture.CreateMany<Rectangle>(3).ToList();
            var lastMatchingRects = _fixture.CreateMany<Rectangle>(1).ToList();

            _rectanglesRepositoryMock
                .Setup(x => x.GetRectanglesByMatchingPointsAsync(
                    It.IsAny<IReadOnlyList<PointDto>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(firstMatchingRects.Concat(lastMatchingRects).ToList());

            SetupMatchingRectanglesInDomainService(points[0], firstMatchingRects);
            SetupMatchingRectanglesInDomainService(points[2], lastMatchingRects);

            // Act
            var result = (await _sut.GetRectanglesByMatchingPointsAsync(points, CancellationToken.None)).ToList();

            // Assert
            Assert.AreEqual(points.Count, result.Count);
            AssertPointHasMatchingRectangleInResult(points[0], firstMatchingRects);
            AssertPointHasMatchingRectangleInResult(points[1], new Rectangle[0]);
            AssertPointHasMatchingRectangleInResult(points[2], lastMatchingRects);

            void AssertPointHasMatchingRectangleInResult(PointDto point, IReadOnlyList<Rectangle> rectangles)
            {
                var matchForPoint = result.FirstOrDefault(x => x.Point == point);
                Assert.IsNotNull(matchForPoint, $"Point {point} doesn't have correspoding match in result");
                CollectionAssert.AreEquivalent(
                    rectangles.Select(x => x.Id).ToList(),
                    matchForPoint.MatchingRectangles.Select(x => x.Id).ToList());
            }
        }

        [TestMethod]
        public async Task Should_throw_exception_on_null_points_list()
        {
            var exception = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.GetRectanglesByMatchingPointsAsync(null, CancellationToken.None));
            Assert.AreEqual("points", exception.ParamName);
        }

        [TestMethod]
        public async Task Should_throw_exception_on_empty_points_list()
        {
            var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _sut.GetRectanglesByMatchingPointsAsync(new PointDto[0], CancellationToken.None));
            Assert.AreEqual("points", exception.ParamName);
        }

        private void SetupMatchingRectanglesInDomainService(PointDto point, IReadOnlyList<Rectangle> rectangles)
        {
            foreach (var rect in rectangles)
            {
                _rectangleDomainServiceMock
                    .Setup(x => x.RectangleContainsPoint(It.Is<Rectangle>(a => a.Id == rect.Id), point.X, point.Y))
                    .Returns(true);
            }
        }
    }
}