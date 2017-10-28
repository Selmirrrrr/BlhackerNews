namespace BlhackerNews.Helpers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BlhackerNews.Services;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Jobs;

    [Config(typeof(FastAndDirtyConfig))]
    public class Benchmark
    {
        private NewsService _newsService;
        public Benchmark()
        {
            _newsService = new NewsService();
        }

        [Benchmark]
        public void GetLastNews()
        {
            var result = _newsService.GetLastNews().Result.ToList();
        }
    }

    public class FastAndDirtyConfig : ManualConfig
    {
    public FastAndDirtyConfig()
    {
        Add(DefaultConfig.Instance); // *** add default loggers, reporters etc? ***

        Add(Job.Default
            .WithWarmupCount(3)     // 3 warmup iteration
            .WithTargetCount(3)     // 3 target iteration
        );
    }
}
}
