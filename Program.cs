using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.CSharp;
using Mono.Cecil;
using System.Threading;
using System.Linq;

namespace IL2Workshop
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = 
@"public static class Workshop {
    public static float fib(int n, float a = 0, float b = 1) {
		if (n == 0)
            return b;
        return fib(n - 1, b, a + b);
    }
    
    public static void Main() {
        //Console.WriteLine(fib(10));
    }
}";

            var references = new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            };

            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Release);
            var compilation = CSharpCompilation.Create(
                "Fib",
                new[] { SyntaxFactory.ParseSyntaxTree(source) },
                references,
                options);
            var result = compilation.Emit("Fib.dll");

            var module = ModuleDefinition.ReadModule("Fib.dll");
            var method = module.GetType("Workshop").Methods.First(m => m.Name == "fib");
            foreach (var instr in method.Body.Instructions)
                Console.WriteLine(instr);
        }
    }
}
