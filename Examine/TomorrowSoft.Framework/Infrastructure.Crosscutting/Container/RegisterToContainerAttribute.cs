using System;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Container
{
    public class RegisterToContainerAttribute:Attribute
    {
        public Type RegisterType { get; private set; }

        public string Name { get; private set; }

        public RegisterToContainerAttribute() : this(null, null)
        {
        }

        public RegisterToContainerAttribute(Type register_type) : this(register_type, null)
        {
        }

        public RegisterToContainerAttribute(string name) : this(null, name)
        {
        }

        public RegisterToContainerAttribute(Type registerType, string name)
        {
            this.RegisterType = registerType;
            this.Name = name;
        }
    }
}