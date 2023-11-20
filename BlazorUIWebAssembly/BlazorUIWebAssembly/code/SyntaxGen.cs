using SyntaxGenerator.Models;

namespace SyntaxGenerator
{
	public class SyntaxGen
	{
		public SyntaxGen()
		{

		}

		/// <summary>
		/// Creates the syntax for a NUMERICAL question type
		/// </summary>
		/// <param name="correctAnswer">The correct answer</param>
		/// <param name="margin">The margin of error</param>
		/// <param name="maxPoints">The amount of points given</param>
		/// <returns>A string with the syntax</returns>
		public static string CreateNumerical(decimal correctAnswer, decimal margin = 0, int maxPoints = 1)
		{
			string question = "{";
			question += $"{maxPoints}:NUMERICAL:={correctAnswer}:{margin}";
			return question += "}";
		}

		/// <summary>
		/// Creates the syntax for a SHORTANSWER question type
		/// </summary>
		/// <param name="isCaseSensitive">If the answer check is case sensitive</param>
		/// <param name="answers">A list of the correct answers</param>
		/// <param name="maxPoints">The amount of points given</param>
		/// <returns>A string with the syntax</returns>
		public static string CreateShortAnswer(List<AnswerOption> answers, string feedback, bool isCaseSensitive, int maxPoints = 1)
		{
			string question = "{";
			question += $"{maxPoints}:";

			if (isCaseSensitive == true)
			{
				question += "SHORTANSWER_C:";
			}

			else
			{
				question += "SHORTANSWER:";
			}

			for (int i = 0; i < answers.Count; i++)
			{
				AnswerOption answer = answers[i];

				if (answer.IsCorrect == true)
				{
					if (string.IsNullOrWhiteSpace(answer.Feedback))
					{
						question += $"%100%{answer.Text}";
					}

					else
					{
						question += $"%100%{answer.Text}#{answer.Feedback}";
					}
				}

				else
				{
					if (string.IsNullOrWhiteSpace(answer.Feedback))
					{
						question += $"{answer.Text}";
					}

					else
					{
						question += $"{answer.Text}#{answer.Feedback}";
					}
				}

				if ((i + 1) != answers.Count)
				{
					question += "~";
				}
			}

			return question += $"*#{feedback}" + "}";
		}

		/// <summary>
		/// Creates the syntax for a MULTICHOICE question type
		/// </summary>
		/// <param name="answers">A list of the correct answers</param>
		/// <param name="isRandomized">Whether or not the answers are scrambled</param>
		/// <param name="isVertical">Are the answer options vertical (true), horizontal (false) or in a dropbox (null)</param>
		/// <param name="maxPoints">The amount of points given</param>
		/// <returns>A string with the syntax</returns>
		public static string CreateMultiChoice(List<AnswerOption> answers, bool isRandomized, bool? isVertical, int maxPoints = 1)
		{
			string question = "{";
			question += $"{maxPoints}:";

			if (isRandomized == true)
			{
				switch (isVertical)
				{
					case true:
						question += "MULTICHOICE_VS:";
						break;

					case false:
						question += "MULTICHOICE_HS:";
						break;

					default:
						question += "MULTICHOICE_S:";
						break;
				}
			}

			else
			{
				switch (isVertical)
				{
					case true:
						question += "MULTICHOICE_V:";
						break;

					case false:
						question += "MULTICHOICE_H:";
						break;

					default:
						question += "MULTICHOICE:";
						break;
				}
			}

			for (int i = 0; i < answers.Count; i++)
			{
				AnswerOption answer = answers[i];

				if (answer.IsCorrect == true)
				{
					if (string.IsNullOrWhiteSpace(answer.Feedback))
					{
						question += $"={answer.Text}";
					}

					else
					{
						question += $"={answer.Text}#{answer.Feedback}";
					}
				}

				else
				{
					if (string.IsNullOrWhiteSpace(answer.Feedback))
					{
						question += $"{answer.Text}";
					}

					else
					{
						question += $"{answer.Text}#{answer.Feedback}";
					}
				}

				if ((i + 1) != answers.Count)
				{
					question += "~";
				}
			}

			return question += "}";
		}

		/// <summary>
		/// Creates the syntax for a MULTIRESPONSE question type
		/// </summary>
		/// <param name="answers">A list of the correct answers</param>
		/// <param name="isRandomized">Whether or not the answers are scrambled</param>
		/// <param name="isVertical">Are the answer options vertical (true) or horizontal (false)</param>
		/// <param name="maxPpoints">The amount of points given</param>
		/// <returns>A string with the syntax</returns>
		public static string CreateMultiResponse(List<AnswerOption> answers, bool isRandomized, bool isVertical, int maxPpoints = 1)
		{
			string question = "{";
			question += $"{maxPpoints}:";

			if (isRandomized == true)
			{
				switch (isVertical)
				{
					case true:
						question += "MULTIRESPONSE_S:";
						break;

					case false:
						question += "MULTIRESPONSE_HS:";
						break;
				}
			}

			else
			{
				switch (isVertical)
				{
					case true:
						question += "MULTIRESPONSE:";
						break;

					case false:
						question += "MULTIRESPONSE_H:";
						break;
				}
			}

			for (int i = 0; i < answers.Count; i++)
			{
				AnswerOption answer = answers[i];

				if (answer.IsCorrect == true)
				{
					if (string.IsNullOrWhiteSpace(answer.Feedback))
					{
						question += $"={answer.Text}";
					}

					else
					{
						question += $"={answer.Text}#{answer.Feedback}";
					}
				}

				else
				{
					if (string.IsNullOrWhiteSpace(answer.Feedback))
					{
						question += $"{answer.Text}";
					}

					else
					{
						question += $"{answer.Text}#{answer.Feedback}";
					}
				}

				if ((i + 1) != answers.Count)
				{
					question += "~";
				}
			}

			return question += "}";
		}
	}
}