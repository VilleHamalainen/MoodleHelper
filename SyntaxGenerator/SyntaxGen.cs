using SyntaxGenerator.Models;

namespace SyntaxGenerator
{
    public static class SyntaxGen
    {
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
        public static string CreateShortAnswer(List<AnswerOption> answers, string feedback, bool isCaseSensitive, decimal percentCorrect, int maxPoints = 1)
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

            // If no percent for a correct answer is given, set it to 100
            if (percentCorrect == 0)
            {
                percentCorrect = 100;
            }

            for (int i = 0; i < answers.Count; i++)
            {
                AnswerOption answer = answers[i];

                if (answer.IsCorrect == true)
                {
                    if (string.IsNullOrWhiteSpace(answer.Feedback))
                    {
                        question += $"%{percentCorrect}%{answer.Text}";
                    }

                    else
                    {
                        question += $"%{percentCorrect}%{answer.Text}#{answer.Feedback}";
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

            if (string.IsNullOrWhiteSpace(feedback))
            {
                return question += "}";
            }

            return question += $"*#{feedback}" + "}";
        }

        /// <summary>
        /// Creates the syntax for a MULTICHOICE question type
        /// </summary>
        /// <param name="answers">A list of the correct answers</param>
        /// <param name="isRandomized">Whether or not the answers are scrambled</param>
        /// <param name="isVertical">Are the answer options presented vertically (true), horizontally (false) or in a dropbox (null)</param>
        /// <param name="maxPoints">The amount of points given</param>
        /// <returns>A string with the syntax</returns>
        public static string CreateMultiChoice(List<AnswerOption> answers, bool isRandomized, bool? isVertical, decimal percentCorrect, int maxPoints = 1)
        {
            string question = "{";
            question += $"{maxPoints}:";
            
            if (isRandomized == true)
            {
                question += isVertical switch
                {
                    true => "MULTICHOICE_VS:",
                    false => "MULTICHOICE_HS:",
                    _ => "MULTICHOICE_S:"
                };
            }
            
            else
            {
                question += isVertical switch
                {
                    true => "MULTICHOICE_V:",
                    false => "MULTICHOICE_H:",
                    _ => "MULTICHOICE:"
                };
            }

            // If no percent for a correct answer is given, set it to 100
            if (percentCorrect == 0)
            {
                percentCorrect = 100;
            }

            for (int i = 0; i < answers.Count; i++)
            {
                AnswerOption answer = answers[i];

                if (answer.IsCorrect == true)
                {
                    if (string.IsNullOrWhiteSpace(answer.Feedback))
                    {
                        question += $"%{percentCorrect}%{answer.Text}";
                    }

                    else
                    {
                        question += $"%{percentCorrect}%{answer.Text}#{answer.Feedback}";
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

                // Add a wavy line for the next answer option if not the last answer
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
        /// <param name="maxPoints">The amount of points given</param>
        /// <returns>A string with the syntax</returns>
        public static string CreateMultiResponse(List<AnswerOption> answers, bool isRandomized, bool isVertical, decimal percentCorrect, int maxPoints = 1)
        {
            string question = "{";
            question += $"{maxPoints}:";

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

            // If no percent for a correct answer is given, set it to 100
            if (percentCorrect == 0)
            {
                percentCorrect = 100;
            }

            for (int i = 0; i < answers.Count; i++)
            {
                AnswerOption answer = answers[i];

                if (answer.IsCorrect == true)
                {
                    if (string.IsNullOrWhiteSpace(answer.Feedback))
                    {
                        question += $"%{percentCorrect}%{answer.Text}";
                    }

                    else
                    {
                        question += $"%{percentCorrect}%{answer.Text}#{answer.Feedback}";
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