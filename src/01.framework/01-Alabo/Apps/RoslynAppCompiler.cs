using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alabo.Apps
{
    /// <summary>
    ///     Class RoslynAppCompiler.
    /// </summary>
    public class RoslynAppCompiler : IDynamicAppCompiler
    {
        /// <summary>
        ///     The source parse options
        /// </summary>
        private static readonly CSharpParseOptions SourceParseOptions =
            CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp6);

        /// <summary>
        ///     The references
        /// </summary>
        private readonly IList<MetadataReference> _references = new List<MetadataReference>();

        /// <summary>
        ///     The source trees
        /// </summary>
        private readonly IList<SyntaxTree> _sourceTrees = new List<SyntaxTree>();

        /// <summary>
        ///     The usings
        /// </summary>
        private readonly IList<string> _usings = new List<string>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoslynAppCompiler" /> class.
        /// </summary>
        /// <param name="savePath">The save path.</param>
        /// <param name="assemblyFileName">Name of the assembly file.</param>
        public RoslynAppCompiler(string savePath, string assemblyFileName)
        {
            SavePath = savePath;
            AssemblyName = assemblyFileName;
        }

        /// <summary>
        ///     Gets the name of the assembly.
        /// </summary>
        public string AssemblyName { get; }

        /// <summary>
        ///     Gets the save path.
        /// </summary>
        public string SavePath { get; }

        /// <summary>
        ///     Adds the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="encoding">The encoding.</param>
        public IDynamicAppCompiler AddFile(string path, Encoding encoding)
        {
            if (!File.Exists(path)) throw new FileNotFoundException("file not found.", path);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(stream, encoding))
                {
                    AddSource(path, sr.ReadToEnd());
                }
            }

            return this;
        }

        /// <summary>
        ///     Adds the source.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="sourceCode">The source code.</param>
        public IDynamicAppCompiler AddSource(string fileName, string sourceCode)
        {
            var tree = SyntaxFactory.ParseSyntaxTree(sourceCode, SourceParseOptions, fileName);
            _sourceTrees.Add(tree);
            return this;
        }

        /// <summary>
        ///     Adds the reference.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        public IDynamicAppCompiler AddReference(string assemblyPath)
        {
            if (!File.Exists(assemblyPath)) throw new FileNotFoundException("file not found.", assemblyPath);

            var reference = MetadataReference.CreateFromFile(assemblyPath);
            _references.Add(reference);
            return this;
        }

        /// <summary>
        ///     Adds the using.
        /// </summary>
        /// <param name="usingString">The using string.</param>
        public IDynamicAppCompiler AddUsing(string usingString)
        {
            if (string.IsNullOrWhiteSpace(usingString)) throw new ArgumentNullException(nameof(usingString));

            _usings.Add(usingString);
            return this;
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        public CompilerResult Build()
        {
            var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                .WithOverflowChecks(true)
                .WithOptimizationLevel(OptimizationLevel.Release)
                .WithUsings(_usings);
            var compilation = CSharpCompilation.Create(AssemblyName,
                _sourceTrees,
                _references,
                compilationOptions);
            var result = compilation.Emit(Path.Combine(SavePath, $"{AssemblyName}.dll"));
            return new CompilerResult(result.Success,
                string.Join(Environment.NewLine, result.Diagnostics.Select(e => e.GetMessage())));
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            //throw new NotImplementedException();
            //do nothing
        }
    }
}