namespace RectExercise.Data.Implementation.EF.Utilities
{
    public static class BatchingUtility
    {
        public static IAsyncEnumerable<TResultItem> WithBatchAsync<TInputItem, TResultItem>(
            IEnumerable<TInputItem> input,
            int batchSize,
            Func<IReadOnlyList<TInputItem>, Task<IEnumerable<TResultItem>>> executeQueryFunc,
            CancellationToken cancellationToken = default)
        {
            if (batchSize <= 0)
            {
                var queryArg = input switch
                {
                    IReadOnlyList<TInputItem> list => list,
                    _ => input.ToList()
                };
                return executeQueryFunc(queryArg).ToAsyncEnumerable(cancellationToken);
            }

            return input
                .Chunk(batchSize)
                .ToAsyncEnumerable()
                .SelectManyAwait(async batch => (await executeQueryFunc(batch)).ToAsyncEnumerable());
        }

        private static async IAsyncEnumerable<TResultItem> ToAsyncEnumerable<TResultItem>(this Task<IEnumerable<TResultItem>> input, CancellationToken cancellationToken)
        {
            var items = await input;
            foreach (var item in items)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }
    }
}
