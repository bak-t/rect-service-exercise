using System.Collections;
using static RectExercise.Data.Implementation.EF.Utilities.BatchingUtility;

namespace RectExercise.Data.Tests.EF.Utilities.BatchingUtility
{
    [TestClass]
    public class WithBatchAsyncTests
    {
        private readonly Fixture _fixture;
        private readonly BatchCallLogger<Guid, int> _callLogger;
        private static readonly BatchCallLogger<Guid, int>.LogItem[] _emptyLog = new BatchCallLogger<Guid, int>.LogItem[0];

        public WithBatchAsyncTests()
        {
            _fixture = new();
            _callLogger = new(_fixture);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-2)]
        public async Task Should_call_query_func_with_whole_sequence_when_batch_size_zero_or_negative(int batchSize)
        {
            // Arrange
            var inputSeq = _fixture.CreateMany<Guid>(3).ToList();

            // Act
            var result = await WithBatchAsync(inputSeq, batchSize, _callLogger.Callback).ToListAsync();

            // Assert
            Assert.AreEqual(1, _callLogger.Log.Count);
            var callData = _callLogger.Log[0];
            var passedSeq = callData.InputBatch;
            Assert.IsNotNull(passedSeq);
            CollectionAssert.AreEquivalent(inputSeq, (ICollection)passedSeq);
            CollectionAssert.AreEquivalent(result, callData.OutputBatch.ToList());
        }

        [TestMethod]
        public async Task Should_call_query_func_for_each_item_when_batch_size_is_1()
        {
            // Arrange
            var inputSeq = _fixture.CreateMany<Guid>(3).ToList();

            // Act
            var result = await WithBatchAsync(inputSeq, 1, _callLogger.Callback).ToListAsync();

            // Assert
            Assert.AreEqual(inputSeq.Count, _callLogger.Log.Count);
            CollectionAssert.AreEqual(
                _emptyLog,
                _callLogger.Log.Where(x => x.InputBatch.Count != 1).ToList());
            CollectionAssert.AreEquivalent(
                result,
                _callLogger.Log.SelectMany(x => x.OutputBatch).ToList());
        }

        [TestMethod]
        public async Task Should_call_query_func_for_each_batch_when_batch_size_is_greater_than_1()
        {
            // Arrange
            const int BatchSize = 2;
            const int InputSeqSize = 3;
            var ExpectedCallsCount = (int)Math.Ceiling((decimal)InputSeqSize / BatchSize);

            var inputSeq = _fixture.CreateMany<Guid>(InputSeqSize).ToList();

            // Act
            var result = await WithBatchAsync(inputSeq, BatchSize, _callLogger.Callback).ToListAsync();

            // Assert
            Assert.AreEqual(ExpectedCallsCount, _callLogger.Log.Count);
            CollectionAssert.AreEquivalent(
                result,
                _callLogger.Log.SelectMany(x => x.OutputBatch).ToList());
        }

        class BatchCallLogger<TInputItem, TResultItem>
        {
            private readonly Fixture _fixture;
            private readonly List<LogItem> _log = new();

            public BatchCallLogger(Fixture fixture)
            {
                _fixture = fixture;
            }

            public IReadOnlyList<LogItem> Log => _log;

            public Task<IEnumerable<TResultItem>> Callback(IReadOnlyList<TInputItem> input)
            {
                var logItem = new LogItem(input.ToList(), _fixture.CreateMany<TResultItem>());
                _log.Add(logItem);
                return Task.FromResult(logItem.OutputBatch);
            }

            public record LogItem(
                IReadOnlyList<TInputItem> InputBatch,
                IEnumerable<TResultItem> OutputBatch);
        }
    }
}