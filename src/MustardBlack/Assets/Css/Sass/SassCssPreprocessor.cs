using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MustardBlack.Assets.Css.Sass
{
	public sealed class SassCssPreprocessor : ICssPreprocessor
	{
		const string sassCompilerSeparatorColorRed = ".sass-compiler-separator{color:red}";

		static readonly Regex fileMatch = new Regex(@"(\.sass|\.scss)$", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1));

		public Regex FileMatch => fileMatch;

		public AssetProcessingResult Process(string input, string mixins = null)
		{
			if (string.IsNullOrWhiteSpace(input))
				return new AssetProcessingResult(AssetProcessingResult.CompilationStatus.Skipped, string.Empty);

			var sassBuilder = new StringBuilder();

			if (!string.IsNullOrEmpty(mixins))
			{
				// This exists because our LessCompiler this was ported from doesnt let us omit the mixins after input compilation.
				// Maybe we can be cleaner with the Sass here
				sassBuilder.Append(sassCompilerSeparatorColorRed);
			}

			sassBuilder.Append(input);

			var cssCompilationResult = SassCompiler.TryCompile(sassBuilder.ToString(), mixins);

			if (cssCompilationResult.Status != AssetProcessingResult.CompilationStatus.Success || string.IsNullOrEmpty(mixins))
				return cssCompilationResult;

			var css = cssCompilationResult.Result.Substring(cssCompilationResult.Result.IndexOf(sassCompilerSeparatorColorRed) + sassCompilerSeparatorColorRed.Length);
			return new AssetProcessingResult(AssetProcessingResult.CompilationStatus.Success, css);
		}
	}
}
