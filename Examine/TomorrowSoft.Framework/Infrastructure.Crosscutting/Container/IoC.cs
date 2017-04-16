using System.Collections.Generic;
using System;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Configuration;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.NetFramework.Exceptions;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Resources;
using System.Linq;

namespace TomorrowSoft.Framework.Infrastructure.Crosscutting.Container
{
    public static class IoC
    {
        private static readonly IUnityContainer UnityContainer;
        static IoC()
        {

            UnityContainer = new UnityContainer();

            Register();
        }

        /// <summary>
        /// 遍历命名空间，自动注册
        /// </summary>
        public static void Register()
        {
            //Aop配置
            UnityContainer.AddNewExtension<Interception>();
            var interception = UnityContainer.Configure<Interception>();

            List<string> types = new List<string>();
            var appSettings = ConfigurationManager.AppSettings;
            string[] appKeys = appSettings.AllKeys;
            foreach (var appKey in appKeys)
            {
                if (appKey.Substring(appKey.Length - 3, 3) == "IoC")
                {
                    types.Add(appKey);
                }

            }
            foreach (var type in types)
            {
                string value = ConfigurationManager.AppSettings[type];
                RegisterType(value, interception);
            }
        }

        public static void RegisterType(string assemblyName, Interception interception)
        {
            var types = Assembly.Load(assemblyName).GetTypes();
            foreach (var type in types)
            {

                var attrs = Attribute.GetCustomAttributes(type);
                foreach (var attr in attrs)
                {
                    if (attr is RegisterToContainerAttribute)
                    {
                        var attribute = attr as RegisterToContainerAttribute;
                        var baseTypes = new List<Type>();
                        if (attribute.RegisterType != null)
                        {
                            baseTypes.Add(attribute.RegisterType);
                        }
                        else if (type.GetInterfaces().Any())
                        {
                            baseTypes.AddRange(type.GetInterfaces());
                        }
                        if (type.BaseType != null && type.BaseType != typeof(Object))
                        {
                            var bt = type.BaseType;
                            while (bt.BaseType != null && bt.BaseType != typeof (Object))
                                bt = bt.BaseType;
                            baseTypes.Add(bt);
                        }
                        if(baseTypes.Count <= 0)
                            throw new FrameworkException(string.Format("类型\"{0}\"没有基类型", type));
                        foreach (var baseType in baseTypes)
                        {
                            if (string.IsNullOrEmpty(attribute.Name))
                                Current.RegisterType(baseType, type);
                            else
                                Current.RegisterType(baseType, type, attribute.Name);
                        }
                    }
                }
            }
        }

        public static IUnityContainer Current
        {
            get { return UnityContainer; }
        }

        public static T Get<T>()
        {
            try
            {
                return UnityContainer.Resolve<T>();
            }
            catch (Exception e)
            {
                throw new FrameworkException(e.Message + "," + typeof(T));
            }
        }

        public static void AopRegister<TFrom, TTo>() where TTo : TFrom
        {
            IoC.Current.RegisterType<TFrom, TTo>()
                .Configure<Interception>()
                .SetInterceptorFor<TFrom>(new InterfaceInterceptor());
        }
    }
}