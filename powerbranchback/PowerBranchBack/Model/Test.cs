using System.Collections.Generic;

namespace PowerBranchBack.Model
{
    public class Test
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public virtual List<TestQuestion> TestQuestions { get; set; }
        public ApplicationUser User { get; set; }

        public void CalculateResult()
        {
            var TempResult = 0;
            foreach (var testQuestion in TestQuestions)
            {
                if (testQuestion.UserAnswerId == testQuestion.Question.RightAnswerId)
                {
                    TempResult++;
                }
            }
            Result = TempResult;
        }
    }
}
