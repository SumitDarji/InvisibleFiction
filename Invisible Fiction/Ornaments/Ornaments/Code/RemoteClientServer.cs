using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Ornaments.Code
{
    public class RemoteClientServerAttribute : RemoteAttribute
    {
        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            //FIND THE CONTROLLER
            Type controller = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(type => type.Name.ToLower() == string.Format("{0}Controller", this.RouteData["controller"].ToString()).ToLower());


            if (controller != null)
            {
                //FIND THE ACTION
                MethodInfo action = controller.GetMethods().FirstOrDefault(method => method.Name.ToLower() == this.RouteData["action"].ToString().ToLower());

                if (action != null)
                {
                    //CHECK OTHER PROPERTY AND ASSIGNED THE VALUE IF OTHER PROPERTY IS FROM MODEL PASSED 
                    var otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);
                    if (otherProperty == null)
                        return new ValidationResult(String.Format("Unknown property: {0}.", OtherPropertyName));
                    var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);


                    //LevelsInsertModel model = (LevelsInsertModel)validationContext.ObjectInstance;
                    //int id = model.id;

                    object instance = Activator.CreateInstance(controller);
                    object response = action.Invoke(instance, new object[] { value, otherPropertyValue });

                    if (response is JsonResult)
                    {
                        object jsonData = ((JsonResult)response).Data;

                        if (jsonData is bool)
                        {
                            return (bool)jsonData ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
                        }
                    }
                }
            }

            return new ValidationResult(this.ErrorMessage);
        }

        public RemoteClientServerAttribute(string routeName)
            : base(routeName)
        {
        }

        public RemoteClientServerAttribute(string action, string controller)
            : base(action, controller)
        {
        }

        public RemoteClientServerAttribute(string action, string controller,
            string areaName)
            : base(action, controller, areaName)
        {
        }

        public string OtherPropertyName { get; set; }
    }

    public class BooleanRequiredAttribute : ValidationAttribute, System.Web.Mvc.IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value is bool)
                return (bool)value;
            else
                return true;
        }

        public IEnumerable<System.Web.Mvc.ModelClientValidationRule> GetClientValidationRules(
             System.Web.Mvc.ModelMetadata metadata,
             System.Web.Mvc.ControllerContext context)
        {
            yield return new System.Web.Mvc.ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "booleanrequired"
            };
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NotEqualToAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} cannot be same as {1}.";

        public string OtherProperty { get; private set; }
        public string OtherPropertyName { get; private set; }

        public NotEqualToAttribute(
            string otherProperty,
            string otherPropertyName)
            : base(DefaultErrorMessage)
        {
            OtherProperty = otherProperty;
            OtherPropertyName = otherPropertyName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);

                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                if (value.Equals(otherPropertyValue))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;

        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ValidationType = "notequalto",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
            };

            rule.ValidationParameters.Add("otherproperty", OtherProperty);
            rule.ValidationParameters.Add("otherpropertyname", OtherPropertyName);

            yield return rule;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherPropertyName);
        }
    }
}