﻿using SyntaxGenerator.Models;

namespace SyntaxGenerator
{
    public static class SyntaxGen
    {
        // Link to the Moodle documentation (in Finnish): https://docs.moodle.org/3x/fi/Aukkotehtävät

        /// <summary>
        /// Creates the syntax for a NUMERICAL question type
        /// </summary>
        /// <param name="correctAnswer">The correct answer</param>
        /// <param name="margin">The margin of error</param>
        /// <param name="maxPoints">The amount of points given</param>
        /// <returns>A string with the syntax</returns>
        public static string CreateNumerical(decimal correctAnswer, string feedback, decimal margin = 0, int maxPoints = 1)
        {
            string question = "{";

            // Check if feedback is empty and then create the question
            if (string.IsNullOrWhiteSpace(feedback))
            {
                question += $"{maxPoints}:NUMERICAL:%100%{correctAnswer}:{margin}";
            }

            else
            {
                question += $"{maxPoints}:NUMERICAL:%100%{correctAnswer}:{margin}#{feedback}";
            }

            return question += "}";
        }

        /// <summary>
        /// Creates the syntax for a SHORTANSWER question type
        /// </summary>
        /// <param name="isCaseSensitive">If the answer check is case sensitive</param>
        /// <param name="answers">A list of the correct answers</param>
        /// <param name="maxPoints">The amount of points given</param>
        /// <returns>A string with the syntax</returns>
        public static string CreateShortAnswer(List<AnswerOption> answers, string? feedback, bool isCaseSensitive, int maxPoints = 1)
        {
            string question = "{";
            question += $"{maxPoints}:";
            
            // Add the question type to the question
            if (isCaseSensitive == true)
            {
                question += "SHORTANSWER_C:";
            }
            
            else
            {
                question += "SHORTANSWER:";
            }

            // Go through all the answers (use for instead of foreach to not add ~ to the last answer option)
            for (int i = 0; i < answers.Count; i++)
            {
                AnswerOption answer = answers[i];

                // If current answer is correct
                if (answer.IsCorrect == true)
                {
                    // If the feedback is empty
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
                    // If the feedback is empty
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
        public static string CreateMultiChoice(List<AnswerOption> answers, bool isRandomized, bool? isVertical, int maxPoints = 1)
        {
            string question = "{";
            question += $"{maxPoints}:";
            
            // Add the question type to the question
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

            // Go through all the answers (use for instead of foreach to not add ~ to the last answer option)
            for (int i = 0; i < answers.Count; i++)
            {
                AnswerOption answer = answers[i];

                // If current answer is correct
                if (answer.IsCorrect == true)
                {
                    // If the feedback is empty
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
                    // If the feedback is empty
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
        /// <param name="pointsPerAnswer">The amount of points given</param>
        /// <returns>A string with the syntax</returns>
        public static string CreateMultiResponse(List<AnswerOption> answers, bool isRandomized, bool isVertical, int pointsPerAnswer = 1)
        {
            string question = "{";

            // Calculate the total amount of points so we can calculate the percentage of points per answer
            int totalPoints = answers.Count(answer => answer.IsCorrect) * pointsPerAnswer;

            // Add the total amount of points to the question
            question += $"{totalPoints}:";

            // Add the question type to the question
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

            // Calculate the percentage of points per answer
            double percentPerQuestion = Math.Round((double)pointsPerAnswer / totalPoints * 100);

            for (int i = 0; i < answers.Count; i++)
            {
                AnswerOption answer = answers[i];

                // If current answer is correct
                if (answer.IsCorrect == true)
                {
                    // If the feedback is empty
                    if (string.IsNullOrWhiteSpace(answer.Feedback))
                    {
                        question += $"%{percentPerQuestion}%{answer.Text}";
                    }

                    else
                    {
                        question += $"%{percentPerQuestion}%{answer.Text}#{answer.Feedback}";
                    }
                }

                else
                {
                    // If the feedback is empty
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
    }
}