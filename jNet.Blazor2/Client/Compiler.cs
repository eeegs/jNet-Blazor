using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace jNet.Blazor2.Client
{
    public class Compiler
    {
        readonly HttpClient client;
        readonly Task initializationTask;
        readonly List<MetadataReference> references = new();

        public Compiler(HttpClient httpClient)
		{
            client = httpClient;

            initializationTask = InitializeInternal();
        }

        async Task InitializeInternal()
        {
            var response = await client.GetFromJsonAsync<BlazorBoot>("_framework/blazor.boot.json");

            //var asm = AppDomain.CurrentDomain.GetAssemblies();
            //asm = asm.Where(x => !x.IsDynamic).ToArray();
            var assemblies = await Task.WhenAll(response.resources.assembly.Keys.Select(x => client.GetAsync($"_framework/{x}")));

            foreach (var a in assemblies)
            {
                using (var task = await a.Content.ReadAsStreamAsync())
                {
                    references.Add(MetadataReference.CreateFromStream(task));
                }
            }
        }

		class BlazorBoot
		{
			public bool cacheBootResources { get; set; }
			public object[]? config { get; set; }
			public bool debugBuild { get; set; }
			public string? entryAssembly { get; set; }
			public bool linkerEnabled { get; set; }
			public Resources? resources { get; set; }
		}

		class Resources
		{
			public Dictionary<string, string>? assembly { get; set; }
			public Dictionary<string, string>? pdb { get; set; }
			public Dictionary<string, string>? runtime { get; set; }
		}


		public Task WhenReady(Func<Task> action)
        {
            if (initializationTask.Status != TaskStatus.RanToCompletion)
            {
                return initializationTask.ContinueWith(x => action());
            }
            else
            {
                return action();
            }
        }

        public (bool success, Assembly? asm) LoadSource(string source)
        {
            var compilation = CSharpCompilation.Create("DynamicCode")
                .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
                .AddReferences(references)
                .AddSyntaxTrees(CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Preview)));

            ImmutableArray<Diagnostic> diagnostics = compilation.GetDiagnostics();

            bool error = false;
            foreach (Diagnostic diag in diagnostics)
            {
                switch (diag.Severity)
                {
                    case DiagnosticSeverity.Info:
                        Console.WriteLine(diag.ToString());
                        break;
                    case DiagnosticSeverity.Warning:
                        Console.WriteLine(diag.ToString());
                        break;
                    case DiagnosticSeverity.Error:
                        error = true;
                        Console.WriteLine(diag.ToString());
                        break;
                }
            }

            if (error)
            {
                return (false, null);
            }

            using (var outputAssembly = new MemoryStream())
            {
                compilation.Emit(outputAssembly);
                return (true, Assembly.Load(outputAssembly.ToArray()));
            }
        }

        public string Format(string source)
        {
            var tree = CSharpSyntaxTree.ParseText(source);
            var root = tree.GetRoot();
            var normalized = root.NormalizeWhitespace();
            return normalized.ToString();
        }
    }
}
