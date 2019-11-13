using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace EmsEntities
{
    /// <summary>
    /// Validation attribute class to validate a field for required based on dependent property value/s
    /// </summary>
    /// <remarks>Author: Kim Kavita</remarks>
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// instance of required attribute class
        /// </summary>
        private RequiredAttribute innerAttribute = new RequiredAttribute();

        /// <summary>
        /// dependent property variable
        /// </summary>
        private string dependentProperty;

        /// <summary>
        /// object array of targetValue name
        /// </summary>
        private object[] targetValue;

        /// <summary>
        /// Initializes a new instance of the RequiredIfAttribute class
        /// </summary>
        /// <remarks>Author: Henry Heeralal</remarks>
        /// <param name="dependentProperty">name of dependent property</param>
        /// <param name="targetValue">name of target Value</param>
        public RequiredIfAttribute(string dependentProperty, params object[] targetValue)
        {
            this.dependentProperty = dependentProperty;
            this.targetValue = targetValue;
            this.ErrorMessage = "Required";
        }

        /// <summary>
        /// This method will yield a new client validation rule -required if and add two new parameter attribute - dependent property, target value
        /// </summary>
        /// <remarks>Author: Henry Heeralal</remarks>
        /// <param name="metadata">instance of metadata</param>
        /// <param name="context">instance of controllerContext</param>
        /// <returns>List of ModelClientValidationRule</returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "requiredif",
            };

            string depProp = this.BuildDependentPropertyId(metadata, context as ViewContext);

            //// find the value on the control we depend on;
            //// if it's a bool, format it javascript style 
            //// (the default is True or False!)

            StringBuilder sb = new StringBuilder();

            foreach (var obj in this.targetValue)
            {
                string targetValue = (obj ?? string.Empty).ToString();

                if (obj.GetType() == typeof(bool))
                {
                    targetValue = targetValue.ToLower();
                }

                sb.AppendFormat("|{0}", targetValue);
            }

            rule.ValidationParameters.Add("dependentproperty", depProp);
            rule.ValidationParameters.Add("targetvalue", sb.ToString().TrimStart('|'));

            yield return rule;
        }

        /// <summary>
        /// This method will validate the context
        /// </summary>
        /// <remarks>Author: Henry Heeralal</remarks>
        /// <param name="value">object value</param>
        /// <param name="validationContext">instance of validation context</param>
        /// <returns>validation result class object</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var containerType = validationContext.ObjectType;
            var obj = validationContext.ObjectInstance;
            var field = containerType.GetProperty(this.dependentProperty);

            if (field != null)
            {
                var dependentvalue = field.GetValue(obj, null);

                foreach (var obj1 in this.targetValue)
                {
                    //// compare the value against the target value
                    if ((dependentvalue == null && this.targetValue == null) ||
                        (dependentvalue != null && dependentvalue.Equals(obj1)))
                    {
                        //// match => means we should try validating this field
                        if (!this.innerAttribute.IsValid(value))
                        {
                            //// validation failed - return an error
                            return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// This method will build the dependent property attribute which would be render as html attribute
        /// </summary>
        /// <remarks>Author: Henry Heeralal</remarks>
        /// <param name="metadata">instance of metadata</param>
        /// <param name="viewContext">instance of ViewContext</param>
        /// <returns>string type dependent property</returns>
        private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext viewContext)
        {
            //// build the ID of the property
            string depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(this.dependentProperty);
            //// unfortunately this will have the name of the current field appended to the beginning,
            //// because the TemplateInfo's context has had this fieldname appended to it. Instead, we
            //// want to get the context as though it was one level higher (i.e. outside the current property,
            //// which is the containing object (our Person), and hence the same level as the dependent property.
            var thisField = metadata.PropertyName + "_";

            if (depProp.StartsWith(thisField))
            {
                //// strip it off again
                depProp = depProp.Substring(thisField.Length);
            }
            else
            {
                // written/added by kavita on july,02,2013
                var thisFieldWithName = metadata.ContainerType.Name + "_";
                if (depProp.StartsWith(thisFieldWithName))
                {
                    //// strip it off again
                    //// do code here
                    depProp = depProp.Replace("_" + thisField, "_");
                }
            }

            return depProp;
        }
    }
}