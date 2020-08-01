using System;
using System.Reflection;

namespace BPMTaskDispatch.Win.Domain.Util
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullName">命名空间.类型名</param>
        /// <param name="assemblyName">程序集</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string fullName, string assemblyName)
        {
            string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
            Type o = Type.GetType(path);//加载类型
            object obj = Activator.CreateInstance(o, true);//根据类型创建实例
            return (T)obj;//类型转换并返回
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                //此为第一种写法
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }
    }
}

/*“反射”其实就是利用程序集的元数据信息。 反射可以有很多方法，编写程序时请先导入 System.Reflection 命名空间。

1、假设你要反射一个 DLL 中的类，并且没有引用它（即未知的类型）： 
Assembly assembly = Assembly.LoadFile("程序集路径，不能是相对路径"); // 加载程序集（EXE 或 DLL） 
dynamic obj = assembly.CreateInstance("类的完全限定名（即包括命名空间）"); // 创建类的实例 

2、若要反射当前项目中的类（即当前项目已经引用它了）可以为：

Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
dynamic obj = assembly.CreateInstance("类的完全限定名（即包括命名空间）"); // 创建类的实例，返回为 object 类型，需要强制类型转换

3、也可以为：

Type type = Type.GetType("类的完全限定名");
dynamic obj = type.Assembly.CreateInstance(type); 

4、不同程序集的话，则要装载调用，代码如下:
System.Reflection.Assembly.Load("程序集名称（不含文件后缀名）").CreateInstance("命名空间.类名", false);
如:
dynamic o = System.Reflection.Assembly.Load("MyDll").CreateInstance("MyNameSpace.A", false);



注意：由于要用到dynamic ，需要把target 改为4.0 ，如果编译时出现“找不到编译动态表达式所需的一个或多个类型。是否缺少引用?”的错误，是因为缺少一个引用，在项目里引用Miscorsoft.CSharp类库，添加后就能编译成功。

======================================================= 
补充：
1）反射创建某个类的实例时，必须保证使用类的完全限定名（命名空间 + 类名）。Type.GetType 方法返回 null 则意味搜索元数据中的相关信息失败（反射失败），请确保反射时使用类的完全限定名。
2）反射功能十分强大，没有什么不能实现的。若实现“跨程序集”，请使用第一种方法创建类的实例，并反射该实例的字段、属性、方法、事件... 然后动态调用之。
*/