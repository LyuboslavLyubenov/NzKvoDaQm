namespace NzKvoDaQm.Extensions
{

    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    using NzKvoDaQm.Models.EntityModels;

    public static class HtmlHelperExtensions
    {

        private static Dictionary<QuantityMeasurementType, string> transations =
            new Dictionary<QuantityMeasurementType, string>()
            {
                {
                    QuantityMeasurementType.Dessertspoons,
                    "Чаени лъжици"
                },
                {
                    QuantityMeasurementType.Grams,
                    "Грама"
                },
                {
                    QuantityMeasurementType.Kilograms,
                    "Килограма"
                },
                {
                    QuantityMeasurementType.Liters,
                    "Литра"
                },
                {
                    QuantityMeasurementType.Milliliters,
                    "Милилитра"
                },
                {
                    QuantityMeasurementType.Tablespoons,
                    "Супени лъжици"
                }
            };

        public static string TranslateQuantityMeasurementTypes(this HtmlHelper htmlHelper, QuantityMeasurementType quantityMeasurementType)
        {
            return transations[quantityMeasurementType];
        }
    }
}