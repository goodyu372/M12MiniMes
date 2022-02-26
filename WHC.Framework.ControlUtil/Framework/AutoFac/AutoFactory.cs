using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WHC.Framework.ControlUtil;
using WHC.Framework.Commons;
using Autofac;
using Autofac.Configuration;

namespace WHC.Framework.ControlUtil
{
    /// <summary>
    /// 使用AutoFac的通用工厂类，通过配置文件autofac.config读取相关信息
    /// </summary>
    public class AutoFactory
    {
        //普通局部变量
        private static object syncRoot = new Object();
        //工厂类的单例
        private static AutoFactory instance = null;
        //配置文件
        private const string configurationFile = "autofac.config";

        /// <summary>
        /// IOC的容器，可调用来获取对应接口实例。
        /// </summary>
        public IContainer Container { get; set; }

        /// <summary>
        /// IOC容器工厂类的单例
        /// </summary>
        public static AutoFactory Instatnce
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new AutoFactory();

                            //初始化相关的注册接口
                            var builder = new ContainerBuilder();
                            //从配置文件注册相关的接口处理
                            builder.RegisterModule(new ConfigurationSettingsReader("autofac", configurationFile));
                            instance.Container = builder.Build();
                        }
                    }
                }
                return instance;
            }
        }

        ///// <summary>
        ///// 测试的接口
        ///// </summary>
        //public void Test()
        //{
        //    var handler = AutoFactory.Instatnce.Container.Resolve<ITestHandler>();
        //    handler.Test("测试");
        //}
    }
}
