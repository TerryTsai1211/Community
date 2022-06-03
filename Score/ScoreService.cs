namespace ScoreSample
{
    public class ScoreService
    {
        private readonly Dictionary<string, string> _userInput;

        public ScoreService(Dictionary<string, string> userInput)
        {
            _userInput = userInput;
        }

        #region 驗證相關
        public (bool isValid, string errorMessage) RuleValidProcess()
        {
            bool isValid = true;
            string errorMessage = string.Empty;

            List<Func<string, string, (bool isValid, string errorMessage)>> rulers = new List<Func<string, string, (bool isValid, string errorMessage)>>();
            rulers.Add(IsScoreEmpty);
            rulers.Add(IsScoreInt);
            rulers.Add(IsScoreInRange);
            
            foreach (KeyValuePair<string, string> ui in _userInput)
            {
                rulers.All(r =>
                {
                    var result = r.Invoke(ui.Key, ui.Value);
                    if (result.isValid == false)
                        errorMessage += result.errorMessage + Environment.NewLine;

                    return result.isValid;
                });
            }

            isValid = string.IsNullOrWhiteSpace(errorMessage);

            return (isValid, errorMessage);
        }

        Func<string, string, (bool isValid, string errorMessage)> IsScoreEmpty => (subjectName, score) =>
        {
            bool isValid = true;
            string errorMessae = string.Empty;

            if (string.IsNullOrWhiteSpace(score))
            {
                isValid = false;
                errorMessae = $"{subjectName} 不得為空值";
            }

            return (isValid, errorMessae);
        };

        Func<string, string, (bool isValid, string errorMessage)> IsScoreInt => (subjectName, score) =>
        {
            bool isValid = true;
            string errorMessae = string.Empty;

            if (int.TryParse(score, out int result) == false)
            {
                isValid = false;
                errorMessae = $"{subjectName} 不是整數";
            }

            return (isValid, errorMessae);
        };

        Func<string, string, (bool isValid, string errorMessage)> IsScoreInRange => (subjectName, score) =>
        {
            bool isValid = true;
            string errorMessae = string.Empty;
            int minScoreLimit = 0;
            int maxScoreLimit = 100;

            if (int.TryParse(score, out int result)
                && (result < minScoreLimit || result > maxScoreLimit))
            {
                isValid = false;
                errorMessae = $"{subjectName} 分數範圍必須在 {minScoreLimit} - {maxScoreLimit} 間";
            }

            return (isValid, errorMessae);
        };

        #endregion

        #region 最大分數、最小分數

        public (string minSubject, string maxSubject) SubjectProcess()
        {
            List<Subject> subjects = SubjectMapper();
            string minSubject = GetMinSubject(subjects);
            string maxSubject = GetMaxSubject(subjects);
            return (minSubject, maxSubject);
        }

        private List<Subject> SubjectMapper()
        {
            return _userInput.
                Select(ui => new Subject()
                {
                    SubjectName = ui.Key,
                    Score = ScoreParse(ui.Value)
                })
                .ToList();

            int ScoreParse(string userInputScore)
            {
                if (int.TryParse(userInputScore, out int result) == false)
                    throw new ArgumentException($"{userInputScore} 無法轉換為整數");

                return result;
            }
        }

        private string GetMinSubject(List<Subject> subjects)
        {
            string scoreInfo = "最低分：";
            int minScore = subjects.Min(s => s.Score);
            string text = GetSubject(subjects, minScore, scoreInfo);
            return text;
        }

        private string GetMaxSubject(List<Subject> subjects)
        {
            string scoreInfo = "最高分：";
            int minScore = subjects.Max(s => s.Score);
            string text = GetSubject(subjects, minScore, scoreInfo);
            return text;
        }

        private string GetSubject(List<Subject> subjects, int score, string initText)
        {
            return subjects
                .Where(s => s.Score == score)
                .Aggregate(
                initText,
                (current, s) => current + s.SubjectName + ",",
                result => result.Substring(0, result.Length - 1) + $"：{score}");
        }

        #endregion
    }
}
