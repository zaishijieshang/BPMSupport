using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace BPMTaskDispatch.Win.Domain.Jober
{

    public class Jobs : ConfigurationSection
    {
        private static readonly ConfigurationProperty s_property = new ConfigurationProperty(string.Empty, typeof(NameTypeCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public NameTypeCollection KeyValues
        {
            get
            {
                return (NameTypeCollection)base[s_property];
            }
        }
    }

    [ConfigurationCollection(typeof(NameTypeSetting))]
    public class NameTypeCollection : ConfigurationElementCollection        // 自定义一个集合
    {
        // 基本上，所有的方法都只要简单地调用基类的实现就可以了。

        public NameTypeCollection()
            : base(StringComparer.OrdinalIgnoreCase)    // 忽略大小写
        {
        }

        // 其实关键就是这个索引器。但它也是调用基类的实现，只是做下类型转就行了。
        new public NameTypeSetting this[string name]
        {
            get
            {
                return (NameTypeSetting)base.BaseGet(name);
            }
        }

        // 下面二个方法中抽象类中必须要实现的。
        protected override ConfigurationElement CreateNewElement()
        {
            return new NameTypeSetting();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NameTypeSetting)element).Name;
        }

        // 说明：如果不需要在代码中修改集合，可以不实现Add, Clear, Remove
        public void Add(NameTypeSetting setting)
        {
            this.BaseAdd(setting);
        }
        public void Clear()
        {
            base.BaseClear();
        }
        public void Remove(string name)
        {
            base.BaseRemove(name);
        }
    }

    public class NameTypeSetting : ConfigurationElement    // 集合中的每个元素
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return this["type"].ToString(); }
            set { this["type"] = value; }
        }  

        [ConfigurationProperty("desc", IsRequired = true)]
        public string Desc
        {
            get { return this["desc"].ToString(); }
            set { this["desc"] = value; }
        } 
        [ConfigurationProperty("cron", IsRequired = true)]
        public string Cron
        {
            get { return this["cron"].ToString(); }
            set { this["cron"] = value; }
        }
    }

}
