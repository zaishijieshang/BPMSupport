using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BPMTaskDispatch.Win.Domain.Jober
{
    public class BuilderJob
    {
        public static IJob CreateJobByName(string JobName)
        {
            IJob _IJob = null;
            try
            {
                Type type = Type.GetType(JobName);
                if (type != null)
                {
                    _IJob = Activator.CreateInstance(type) as IJob;
                }
                else
                {
                    Assembly assembly = Assembly.Load("BPMTaskDispatch.Job");//typeof(Program).Assembly;
                    _IJob = assembly.CreateInstance(string.Format("BPMTaskDispatch.Job.{0}", JobName)) as IJob;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _IJob;
        }

        public static IJob CreateJobByType(string JobType)
        {
            IJob _IJob = null;
            if (JobType.Contains(","))
            {
                try
                {
                    string[] arrType = JobType.Split(',');
                    //Assembly assembly = Assembly.Load(arrType[1]);
                    string path = string.Format("{0}\\{1}\\{2}.dll", Environment.CurrentDirectory, "JobLib", arrType[1]);
                    Assembly assembly = Assembly.LoadFile(path);
                    _IJob = assembly.CreateInstance(arrType[0]) as IJob;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return _IJob;
        }
    }
}
/*
其中着重讲解以下Assembly.LoadFile 与 Assembly.LoadFrom的区别

1、Assembly.LoadFile只载入相应的dll文件，比如Assembly.LoadFile("abc.dll")，则载入abc.dll，假如abc.dll中引用了def.dll的话，def.dll并不会被载入。

Assembly.LoadFrom则不一样，它会载入dll文件及其引用的其他dll，比如上面的例子，def.dll也会被载入。

2、用Assembly.LoadFrom载入一个Assembly时，会先检查前面是否已经载入过相同名字的Assembly，比如abc.dll有两个版本(版本1在目录1下，版本2放在目录2下)，程序一开始时载入了版本1，当使用Assembly.LoadFrom("2\\abc.dll")载入版本2时，不能载入，而是返回版本1。Assembly.LoadFile的话则不会做这样的检查，比如上面的例子换成Assembly.LoadFile的话，则能正确载入版本2。

LoadFile:加载指定路径上的程序集文件的内容。LoadFrom: 根据程序集的文件名加载程序集文件的内容。

 

最后是调用方法

Assembly outerAsm = Assembly.LoadFrom(@"urPath\MyDLL.dll");

调用DLL类中的方法

Type type = outerAsm.GetType("MyDLL.MyClass");//调用类型
MethodInfo method = type.GetMethod("MyVoid");//调用方法



//如果需要传参数

object[] paramertors = new object[] { "3087", "2005" };//参数集合
object test = method.Invoke(null, paramertors);//Invoke调用方法

调用DLL中窗体

Type outerForm = outerAsm.GetType("MyForm", false);//找到指定窗口
(Activator.CreateInstance(outerForm) as Form).Show();//转换成窗体类，显示
*/
