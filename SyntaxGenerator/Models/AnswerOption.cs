namespace SyntaxGenerator.Models
{
    public class AnswerOption
    {
        public string? Text { get; set; }
        public bool IsCorrect { get; set; }
        public string? Feedback { get; set; }

        public AnswerOption()
        {
            
        }

        public AnswerOption(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }

        public AnswerOption(string text, bool isCorrect, string feedback)
        {
            Text = text;
            IsCorrect = isCorrect;
            Feedback = feedback;
        }
    }
}