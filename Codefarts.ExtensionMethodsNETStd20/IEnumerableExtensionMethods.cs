namespace System.Collections
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

#if !PORTABLE && !NET35
    using System.Collections.Concurrent;
#endif

    public static partial class IEnumerableExtensionMethods
    {
#if !PORTABLE && !NET35
        public static void ForceParallel<TSource>(this IEnumerable<TSource> source, Action<TSource> action, int forcedDegreeOfParallelism)
        {
            if (forcedDegreeOfParallelism <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(forcedDegreeOfParallelism));
            }

            var partitions = Partitioner
                .Create(source, EnumerablePartitionerOptions.NoBuffering) // Stripped partitioning.
                .GetPartitions(forcedDegreeOfParallelism).ToList();
            using (var countdownEvent = new CountdownEvent(forcedDegreeOfParallelism))
            {
                partitions.ForEach(partition => new Thread(() =>
                {
                    try
                    {
                        using (partition)
                        {
                            while (partition.MoveNext())
                            {
                                action(partition.Current);
                            }
                        }
                    }
                    finally
                    {
                        countdownEvent.Signal();
                    }
                }).Start());
                countdownEvent.Wait();
            }
        }    
#endif
    }
}