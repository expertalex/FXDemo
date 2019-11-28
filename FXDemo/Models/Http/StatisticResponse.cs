using System;
namespace FXDemo.Models.Http
{
    public class CardResponse : GeneralStatisiticResponce
    {
        public string TeamName { get; set; }
    }

    public class YelowCardResponse : CardResponse
    {
    }

    public class RedCardResponse : CardResponse
    {
    }

    public class MinuteResponse : GeneralStatisiticResponce
    {

    }
}
