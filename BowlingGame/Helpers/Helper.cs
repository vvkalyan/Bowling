using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BowlingGame
{
    public class SeparatedQueryStringValueProvider : QueryStringValueProvider
    {
        private readonly string _key;
        private readonly string _separator;
        private readonly IQueryCollection _values;

        public SeparatedQueryStringValueProvider(IQueryCollection values, string separator)
            : this(null, values, separator)
        {
        }

        public SeparatedQueryStringValueProvider(string key, IQueryCollection values, string separator)
            : base(BindingSource.Query, values, CultureInfo.InvariantCulture)
        {
            _key = key;
            _values = values;
            _separator = separator;
        }

        public override ValueProviderResult GetValue(string key)
        {
            var result = base.GetValue(key);

            if (_key != null && _key != key)
            {
                return result;
            }

            if (result != ValueProviderResult.None && result.Values.Any(x => x.IndexOf(_separator, StringComparison.OrdinalIgnoreCase) > 0))
            {
                var splitValues = new StringValues(result.Values
                    .SelectMany(x => x.Split(new[] { _separator }, StringSplitOptions.None)).ToArray());
                return new ValueProviderResult(splitValues, result.Culture);
            }

            return result;
        }
    }

    public class SeparatedQueryStringValueProviderFactory : IValueProviderFactory
    {
        private readonly string _separator=",";
        private readonly string _key="scores";

        public SeparatedQueryStringValueProviderFactory(string separator) : this(null, separator)
        {
        }

        public SeparatedQueryStringValueProviderFactory(string key, string separator)
        {
            _key = key;
            _separator = separator;
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0, new SeparatedQueryStringValueProvider(_key, context.ActionContext.HttpContext.Request.Query, _separator));
            return Task.CompletedTask;
        }
    }



    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ArrayInputAttribute : ActionFilterAttribute
    {
        private readonly string parameterName;

        public ArrayInputAttribute(string paramName)
        {
            this.parameterName = paramName;
            this.Separator = ',';
        }

        public char Separator { get; set; }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            //if (!actionContext.ActionArguments.ContainsKey(this.parameterName))
            //{
            //    return;
            //}
            //else
            //{
            //    var parameters = actionContext.ActionArguments.FirstOrDefault().Value.ToString();
            //    actionContext.ActionArguments[this.parameterName] = parameters; // parameters.Split(this.Separator).Select(int.Parse).ToArray();
            //}

            //if (actionContext.RouteData.Values.ContainsKey(this.parameterName))
            //{
            //    parameters = (string)actionContext.RouteData.Values[this.parameterName];
            //}
            //else if (actionContext.HttpContext.Request.QueryString.ToString().Contains(this.parameterName))
            //{
            //    parameters = actionContext.HttpContext.Request.QueryString.ToString();
            //}
            //    if (parameters.Contains(this.Separator))
            //{
            //    actionContext.ActionArguments[this.parameterName] = parameters.Split(this.Separator).Select(int.Parse).ToArray();
            //}

        }
    }


}
