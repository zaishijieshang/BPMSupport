using System.Configuration;
using System.Xml;

namespace BPMTaskDispatch.Win.Domain.Util
{
    public class ConfigSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            return section;
        }
    }
}
