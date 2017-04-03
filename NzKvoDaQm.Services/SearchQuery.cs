namespace NzKvoDaQm.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using NzKvoDaQm.Data;
    using NzKvoDaQm.Models.EntityModels;
    using NzKvoDaQm.Services.SearchConstraints;

    public class SearchQuery : ISearchQuery
    {
        private const string ConstraintsPattern = "(?<=\\s)([\\u0400-\\u04FF]+)\\:\\\"([\\u0400-\\u04FF\\,\\.\\w\\d\\s]+)\\\"";

        private static readonly Type searchConstraintInterfaceType = typeof(ISearchConstraint);
        private static readonly Type[] searchConstraintsTypes;

        private readonly Regex regex = new Regex(ConstraintsPattern, RegexOptions.Compiled);
        
        private readonly IDbContext context;
        private readonly string[] wordsToSearchFor;
        private readonly ISearchConstraint[] searchConstraints;

        static SearchQuery()
        {
            searchConstraintsTypes = Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(
                    t => t.IsClass && t.GetInterfaces()
                                          .Contains(searchConstraintInterfaceType))
                .ToArray();
        }

        public SearchQuery(IDbContext context, string query)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException("query");
            }
            
            this.context = context;
            this.wordsToSearchFor = Regex.Replace(query, ConstraintsPattern, "")
                .Split(new char[] {}, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim().ToUpper())
                .ToArray();
            this.searchConstraints = this.ExtractSearchConstraints(query);
        }
        
        private ISearchConstraint[] ExtractSearchConstraints(string query)
        {
            var constraintsMatches = this.regex.Matches(query);
            var constraints = new List<ISearchConstraint>();

            foreach (Match constraintsMatch in constraintsMatches)
            {
                var constraintName = constraintsMatch.Groups[1].Value;
                var constraintValue = constraintsMatch.Groups[2].Value;

                var type = searchConstraintsTypes.First(t => t.Name.ToUpper() == constraintName.ToUpper());

                if (type == null)
                {
                    continue;
                }

                var instance = (ISearchConstraint)Activator.CreateInstance(type, constraintValue);
                constraints.Add(instance);
            }

            return constraints.ToArray();
        }

        public Recipe[] GetResults()
        {
            var recipesContainingSelectedWords =
                this.context.Recipes.Where(r => this.wordsToSearchFor.Any(w => r.Title.ToUpper().Contains(w)))
                    .ToList()
                    .Where(r => this.searchConstraints.All(s => s.IsAllowed(r)))
                    .ToArray();

            return recipesContainingSelectedWords;
        }
    }
}