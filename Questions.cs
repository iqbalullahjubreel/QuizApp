using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Question
{

    public string Type { get; set; }
    public string Questions { get; set; }
    public string Answer { get; set; }
    public int Count { get; set; }

    public Question()
    {

    }
    public Question(string type, string questions, string answer)
    {
        Type = type;
        Questions = questions;
        Answer = answer;
    }

    //public override string ToString()
    //{
    //    return $"{Type}\t{Questions}\t{Answer}";
    //}
}

