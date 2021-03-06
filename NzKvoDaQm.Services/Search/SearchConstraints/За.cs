﻿namespace NzKvoDaQm.Services.Search.SearchConstraints
{

    using System;
    using System.Linq;

    using NzKvoDaQm.Models.EntityModels;

    public class За : ISearchConstraint
    {
        private readonly string[] AllowedTimeTypes = new string[] {"минути", "мин", "часа"};
        private readonly string InvalidTimeFormatExceptionMessage;

        private readonly int timeNumber;
        private readonly string timeTypeUpper;

        /// <summary>
        /// Ограничава рецептите до някакъв интервал за готвене
        /// </summary>
        /// <param name="time">Максимум време за сготвяне. Формат: number минути/мин/часа/</param>
        public За(string time)
        {
            this.InvalidTimeFormatExceptionMessage = $"Not supported time. Must be in format: {{number}} {string.Join(",", this.AllowedTimeTypes)}. Example: 15 минути";

            var timeParams = time.Split(
                new char[]
                {
                },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(
                    param => param.Trim()
                                 .ToUpper())
                .Where(param => !string.IsNullOrWhiteSpace(param))
                .ToArray();

            if (timeParams.Length != 2)
            {
                throw new ArgumentException(this.InvalidTimeFormatExceptionMessage);
            }

            int timeNumber;
            var isValidTimeNumber = int.TryParse(timeParams[0], out timeNumber);

            if (!isValidTimeNumber || timeNumber <= 0)
            {
                throw new ArgumentException(this.InvalidTimeFormatExceptionMessage);
            }
            
            var timeType = timeParams[1];

            if (this.AllowedTimeTypes.Contains(timeType))
            {
                throw new ArgumentException(this.InvalidTimeFormatExceptionMessage);
            }

            this.timeNumber = timeNumber;
            this.timeTypeUpper = timeType;
        }
        
        public bool IsAllowed(Recipe recipe)
        {
            var currentTime = DateTime.Now;
            var allowedTo = currentTime;
            var timeToCookRecipe = allowedTo.AddMinutes(recipe.MinutesRequiredToCook);

            if (this.timeTypeUpper == "минути".ToUpper() || this.timeTypeUpper == "мин".ToUpper())
            {
                allowedTo = currentTime.AddMinutes(this.timeNumber);
            }
            else if (this.timeTypeUpper == "часа".ToUpper() || this.timeTypeUpper == "час".ToUpper())
            {
                allowedTo = currentTime.AddHours(this.timeNumber);
            }
            
            return timeToCookRecipe <= allowedTo;
        }
    }
}