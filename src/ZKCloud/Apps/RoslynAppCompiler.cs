using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ZKCloud.Apps
{
    public class RoslynAppCompiler : IDynamicAppCompiler {
        private static readonly CSharpParseOptions _sourceParseOptions = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp6);

        private IList<string> _usings = new List<string>();

        private IList<MetadataReference> _references = new List<MetadataReference>();

        private IList<SyntaxTree> _sourceTrees = new List<SyntaxTree>();

        public string AssemblyName { get; private set; }

        public string SavePath { get; private set; }


        public RoslynAppCompiler(string savePath, string assemblyFileName) {
            SavePath = savePath;
            AssemblyName = assemblyFileName;
        }

        public IDynamicAppCompiler AddFile(string path, Encoding encoding) {
            if (!File.Exists(path))
                throw new FileNotFoundException("file not found.", path);
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                using (var sr = new StreamReader(stream, encoding)) {
                    AddSource(path, sr.ReadToEnd());
                }
            }
            return this;
        }

        public IDynamicAppCompiler AddSource(string fileName, string sourceCode) {
            var tree = SyntaxFactory.ParseSyntaxTree(sourceCode, _sourceParseOptions, fileName);
            _sourceTrees.Add(tree);
            return this;
        }

        public IDynamicAppCompiler AddReference(string assemblyPath) {
            if (!File.Exists(assemblyPath))
                throw new FileNotFoundException("file not found.", assemblyPath);
            var reference = MetadataReference.CreateFromFile(assemblyPath);
            _references.Add(reference);
            return this;
        }

        public IDynamicAppCompiler AddUsing(string usingString) {
            if (string.IsNullOrWhiteSpace(usingString))
                throw new ArgumentNullException(nameof(usingString));
            _usings.Add(usingString);
            return this;
        }

        public CompilerResult Build() {
            CSharpCompilationOptions compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                    .WithOverflowChecks(true)
                    .WithOptimizationLevel(OptimizationLevel.Release)
                    .WithUsings(_usings);
            CSharpCompilation compilation = CSharpCompilation.Create(AssemblyName,
                _sourceTrees,
                _references,
                compilationOptions);
            var result = compilation.Emit(Path.Combine(SavePath, $"{AssemblyName}.dll"));
            return new CompilerResult(result.Success, string.Join(Environment.NewLine, result.Diagnostics.Select(e => e.GetMessage())));
        }

        public void Dispose() {
            //throw new NotImplementedException();
            //do nothing
        }
    }
}
