using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingGame.Helpers
{   
    /// <summary>
    /// Validates the input scores (Basic Validation - To do) 
    /// </summary>
    public class Validation
    {  
          /// <summary>
         ///  Input Scores Validation
         /// </summary>
         /// <param name="inputScores"></param>
        /// <param name="frameNo"></param>
        /// <returns></returns>
        public static string CheckScoreValidity(string inputScores, int? frameNo)
        {
            try
            {
                var scores = inputScores.Split(',').ToList();
                if (scores.Count() > 21)
                    return "No of throws cannot be greater than 21";
                if (scores.Count() < 11) 
                    return "No of throws cannot be less than than 11";
                if (frameNo != null && int.TryParse(frameNo.ToString(), out int fnumber) && (fnumber < 0 || fnumber > 10))
                {
                    return "Invalid Frame No";
                }
                foreach (var item in scores)
                {
                    int.TryParse(item, out int number);
                    if (item.Trim() == "," || item.Trim() == "" || (number >= 0 && number <= 10))
                    {
                    }
                    else
                    {
                        return "Invalid Number/Characters in the input string"; 
                    }
                }
                return string.Empty;
            }
            catch(Exception ex)
            {
                //Add logging to do
                return ex.ToString();
            }
        }
    }
}
