using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Text;

namespace BPMTaskDispatch.Extend.AD
{
    //    下面列举一些域中存在的属性：


    //distinguishedname；objectclass；pwdlastset；userprincipalname与mail一样；memberof；objectguid；instancetype；codepage；whenchanged；samaccountname；cn；usncreated；sn（姓）；accountexpires；usnchanged；displayname；description；useraccountcontrol（权限码）；whencreated；givenName（名）；samaccounttype；objectcategory；countrycode；primarygroupid；objectsid；title（职务）；department（部门）


    // 备注：
    //.   在ASP.NET中专用属性：   
    //              获取服务器电脑名：Page.Server.ManchineName   
    //              获取用户信息：Page.User   
    //              获取客户端电脑名：Page.Request.UserHostName   
    //              获取客户端电脑IP：Page.Request.UserHostAddress   
    //.   在网络编程中的通用方法：   
    //              获取当前电脑名：static   System.Net.Dns.GetHostName()   
    //              根据电脑名取出全部IP地址：static   System.Net.Dns.Resolve(电脑名).AddressList   
    //              也可根据IP地址取出电脑名：static   System.Net.Dns.Resolve(IP地址).HostName   
    //.   系统环境类的通用属性：   
    //              当前电脑名：static   System.Environment.MachineName   
    //              当前电脑所属网域：static   System.Environment.UserDomainName   
    //              当前电脑用户：static   System.Environment.UserName


    public class LdapAuthentication
    {
        private string _path;
        private string _filterAttribute;


        public LdapAuthentication(string path)
        {
            _path = path;
        }


        /// <summary>
        /// 判断是否域用户
        /// </summary>
        /// <param name="domain">域名</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;


                DirectorySearcher search = new DirectorySearcher(entry)
                {
                    Filter = "(SAMAccountName=" + username + ")"
                };
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }
            return true;
        }


        /// <summary>
        /// 根据用户名获取所属组名
        /// </summary>
        /// <returns></returns>
        public string GetGroupByUser()
        {
            DirectorySearcher search = new DirectorySearcher(_path)
            {
                Filter = "(cn=" + _filterAttribute + ")"
            };
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();


            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;


                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }


        /// <summary>
        /// 获取组用户
        /// </summary>
        /// <param name="Groupname">组名</param>
        /// <returns></returns>
        public string[] GetUsersForGroup(string Groupname)
        {
            DirectorySearcher ds = new DirectorySearcher(_path)
            {
                Filter = "(&(objectClass=group)(cn=" + Groupname + "))"
            };
            ds.PropertiesToLoad.Add("member");
            SearchResult r = ds.FindOne();


            if (r.Properties["member"] == null)
            {
                return (null);
            }


            string[] results = new string[r.Properties["member"].Count];
            for (int i = 0; i < r.Properties["member"].Count; i++)
            {
                string theGroupPath = r.Properties["member"][i].ToString();
                results[i] = theGroupPath.Substring(3, theGroupPath.IndexOf(",") - 3);
            }
            return (results);
        }


        /// <summary>
        /// 获取用户所属组
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public string[] GetGroupsForUser(string username)
        {
            DirectorySearcher ds = new DirectorySearcher(_path)
            {
                Filter = "(&(sAMAccountName=" + username + "))"
            };
            ds.PropertiesToLoad.Add("memberof");
            SearchResult r = ds.FindOne();


            if (r.Properties["memberof"].Count == 0)
            {
                return (null);
            }


            string[] results = new string[r.Properties["memberof"].Count];
            for (int i = 0; i < r.Properties["memberof"].Count; i++)
            {
                string theGroupPath = r.Properties["memberof"][i].ToString();
                results[i] = theGroupPath.Substring(3, theGroupPath.IndexOf(",") - 3);
            }
            return (results);
        }


        /// <summary>
        /// 根据用户名取得所属组名
        /// </summary>
        public string[] GetAllGroupsForUser(string username)
        {
            DirectorySearcher ds = new DirectorySearcher(_path)
            {
                Filter = "(&(sAMAccountName=" + username + "))"
            };
            ds.PropertiesToLoad.Add("memberof");
            SearchResult r = ds.FindOne();
            if (r.Properties["memberof"] == null)
            {
                return (null);
            }
            string[] results = new string[r.Properties["memberof"].Count + 1];
            for (int i = 0; i < r.Properties["memberof"].Count; i++)
            {
                string theGroupPath = r.Properties["memberof"][i].ToString();
                results[i] = theGroupPath.Substring(3, theGroupPath.IndexOf(",") - 3);
            }
            results[r.Properties["memberof"].Count] = "All";//All组属于任何人,在AD之外定义了一个组，以便分配用户权限
            return (results);
        }


        /// <summary>
        /// 获取组用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>


        public string GetUserDisplayName(string username)
        {
            string results = string.Empty;
            DirectorySearcher ds = new DirectorySearcher(_path)
            {
                Filter = "(&(objectClass=user)(sAMAccountName=" + username + "))"
            };
            ds.PropertiesToLoad.Add("DisplayName");
            SearchResult r = ds.FindOne();
            if (r != null)
            {
                results = r.GetDirectoryEntry().InvokeGet("DisplayName").ToString();
            }
            return (results);
        }




        /// <summary>
        /// 根据CN获取组description
        /// </summary>
        public string GetAdGroupDescription(string prefix)
        {
            string results;
            DirectorySearcher groupsDS = new DirectorySearcher(_path)
            {
                Filter = "(&(objectClass=group)(CN=" + prefix + "*))"
            };
            groupsDS.PropertiesToLoad.Add("cn");
            SearchResult sr = groupsDS.FindOne();
            results = sr.GetDirectoryEntry().InvokeGet("description").ToString();
            return (results);
        }


        /// <summary>
        /// 根据CN获取组信息
        /// </summary>
        public DataTable GetAdGroupInfo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("URL", typeof(string));
            dt.Columns.Add("cn", typeof(string));
            dt.Columns.Add("Description", typeof(string));


            DirectorySearcher searcher = new DirectorySearcher(_path)
            {
                Filter = "(&(objectClass=group))"
            };
            //searcher.SearchScope = SearchScope.Subtree;
            //searcher.Sort = new SortOption("description", System.DirectoryServices.SortDirection.Ascending);
            searcher.PropertiesToLoad.AddRange(new string[] { "cn", "description" });
            SearchResultCollection results = searcher.FindAll();
            if (results.Count == 0)
            {
                return (null);
            }
            else
            {
                foreach (SearchResult result in results)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = result.Path.ToString();
                    dr[1] = result.GetDirectoryEntry().InvokeGet("cn").ToString();
                    if (result.GetDirectoryEntry().InvokeGet("Description") != null)
                    {
                        dr[2] = result.GetDirectoryEntry().InvokeGet("Description").ToString();
                    }
                    else
                    {
                        dr[2] = result.GetDirectoryEntry().InvokeGet("cn").ToString();
                    }

                    dt.Rows.Add(dr);
                }
                dt.DefaultView.Sort = "description ASC";
                return dt;
            }


        }


        /// <summary>
        /// 根据CN获取登陆名
        /// </summary>
        public string getAccountName(string cn)
        {
            foreach (string path in _path.Split(','))
            {
                DirectorySearcher ds = new DirectorySearcher(path)
                {
                    Filter = "(&(objectClass=user)(cn=*" + cn + "*))"
                };
                ds.PropertiesToLoad.Add("sAMAccountName");
                SearchResult r = ds.FindOne();
                if (r != null)
                {
                    return r.GetDirectoryEntry().InvokeGet("sAMAccountName").ToString();
                }
            }
            return null;
        }




        /// <summary>
        /// 生成用户数据表
        /// </summary>
        public DataTable adUserlist(string groupname)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cn", typeof(string));
            dt.Columns.Add("sAMAccountName", typeof(string));
            string[] groupmember = GetUsersForGroup(groupname);
            if (groupmember.Length == 0)
            {
                return null;
            }
            else
            {
                foreach (string member in groupmember)
                {
                    if (IsAccountActive(getAccountControl(getAccountName(member))))
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = member.ToString();
                        dr[1] = getAccountName(member);
                        dt.Rows.Add(dr);
                    }
                }
                return dt;


            }
        }






        /// <summary>
        /// 生成指定的用户信息数据表
        /// </summary>
        public DataTable adUserlist()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("memberof", typeof(string));
            dt.Columns.Add("cn", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("Mail", typeof(string));
            dt.Columns.Add("samaccountname", typeof(string));
            dt.Columns.Add("whencreated", typeof(string));
            dt.Columns.Add("title", typeof(string));
            dt.Columns.Add("department", typeof(string));
            DirectorySearcher searcher = new DirectorySearcher(_path)
            {
                //searcher.Filter = "(description=ADPJ*)";
                Filter = "(description=ADPL*)"
            };
            searcher.PropertiesToLoad.AddRange(new string[] { "memberof", "cn", "description", "name", "Mail", "samaccountname", "whencreated", "title", "department" });
            SearchResultCollection results = searcher.FindAll();


            if (results.Count == 0)
            {
                return (null);
            }
            else
            {
                foreach (SearchResult result in results)
                {


                    DataRow dr = dt.NewRow();
                    //dr[0] = result.Path.ToString();
                    if (result.GetDirectoryEntry().InvokeGet("memberof") != null)
                    {
                        dr[0] = result.GetDirectoryEntry().InvokeGet("memberof").ToString();
                    }
                    else
                    {
                        dr[0] = "";
                    }

                    if (result.GetDirectoryEntry().InvokeGet("cn") != null)
                    {
                        dr[1] = result.GetDirectoryEntry().InvokeGet("cn").ToString();
                    }
                    else
                    {
                        dr[1] = "";
                    }

                    if (result.GetDirectoryEntry().InvokeGet("Description") != null)
                    {
                        dr[2] = result.GetDirectoryEntry().InvokeGet("Description").ToString();
                    }
                    else
                    {
                        dr[2] = result.GetDirectoryEntry().InvokeGet("cn").ToString();
                    }

                    if (result.GetDirectoryEntry().InvokeGet("name") != null)
                    {
                        dr[3] = result.GetDirectoryEntry().InvokeGet("name").ToString();
                    }
                    else
                    {
                        dr[3] = "";
                    }

                    if (result.GetDirectoryEntry().InvokeGet("Mail") != null)
                    {
                        dr[4] = result.GetDirectoryEntry().InvokeGet("Mail").ToString();
                    }
                    else
                    {
                        dr[4] = "";
                    }

                    if (result.GetDirectoryEntry().InvokeGet("samaccountname") != null)
                    {
                        dr[5] = result.GetDirectoryEntry().Properties["samaccountname"].Value.ToString();
                    }
                    else
                    {
                        dr[5] = "";
                    }

                    if (result.GetDirectoryEntry().InvokeGet("whencreated") != null)
                    {
                        dr[6] = result.GetDirectoryEntry().Properties["whencreated"].Value.ToString();
                    }
                    else
                    {
                        dr[6] = "";
                    }

                    if (result.GetDirectoryEntry().InvokeGet("title") != null)
                    {
                        dr[7] = result.GetDirectoryEntry().Properties["title"].Value.ToString();
                    }
                    else
                    {
                        dr[7] = "";
                    }

                    if (result.GetDirectoryEntry().InvokeGet("department") != null)
                    {
                        dr[8] = result.GetDirectoryEntry().Properties["department"].Value.ToString();
                    }
                    else
                    {
                        dr[8] = "";
                    }

                    dt.Rows.Add(dr);
                }
                dt.DefaultView.Sort = "description ASC";
                return dt;
            }
        }


        ///// <summary>
        ///// 根据组名生成USER列表
        ///// </summary>
        //public void adUserlistbox(ListBox results, string groupName)
        //{
        //    results.Items.Clear();
        //    DataTable dt = adUserlist(groupName);
        //    if (dt != null)
        //    {
        //        results.DataSource = dt;
        //        results.DataTextField = dt.Columns[0].Caption;
        //        results.DataValueField = dt.Columns[1].Caption;
        //        results.DataBind();
        //    }
        //}


        //public void adGrouplistbox(ListBox results)
        //{
        //    results.Items.Clear();
        //    DataTable dt = GetAdGroupInfo();
        //    DataRow dr = dt.NewRow();
        //    dr[1] = "All";
        //    dr[2] = "All";
        //    dt.Rows.Add(dr);
        //    results.DataSource = dt;
        //    results.DataTextField = dt.Columns[2].Caption;
        //    results.DataValueField = dt.Columns[1].Caption;
        //    results.DataBind();
        //}


        //public void aduserGrouplist(DropDownList results)
        //{
        //    results.Items.Clear();
        //    DataTable dt = GetAdGroupInfo();
        //    results.DataSource = dt;
        //    results.DataTextField = dt.Columns[2].Caption;
        //    results.DataValueField = dt.Columns[1].Caption;
        //    results.DataBind();
        //}




        /// <summary>
        /// 获取权限码
        /// </summary>
        public int getAccountControl(string accountName)
        {
            int results;
            DirectorySearcher ds = new DirectorySearcher(_path)
            {
                Filter = "(&(objectClass=user)(sAMAccountName=" + accountName + "))"
            };
            ds.PropertiesToLoad.Add("userAccountControl");
            try
            {
                SearchResult r = ds.FindOne();
                results = Convert.ToInt32(r.GetDirectoryEntry().InvokeGet("userAccountControl"));
                return results;
            }
            catch
            {
                return 0;
            }


        }


        /// <summary>
        /// 判断用户是否有效
        /// </summary>
        public bool IsAccountActive(int userAccountControl)
        {
            int ADS_UF_ACCOUNTDISABLE = 0X0002;
            int userAccountControl_Disabled = Convert.ToInt32(ADS_UF_ACCOUNTDISABLE);
            int flagExists = userAccountControl & userAccountControl_Disabled;
            if (flagExists > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public DirectoryEntry GetDirectoryEntryByAccount(string sAMAccountName)
        {
            DirectorySearcher deSearch = new DirectorySearcher(_path)
            {
                Filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + sAMAccountName + "))"
            };
            // deSearch.SearchScope = SearchScope.Subtree;


            try
            {
                SearchResult result = deSearch.FindOne();
                if (result == null)
                { return null; }
                DirectoryEntry de = new DirectoryEntry(_path);
                return de;
            }
            catch
            {
                //throw;
                return null;
            }
        }


        //-------------------------------- 另外增加一个读取用户信息的方法：


        ///   <summary> 
        ///   读取AD用户信息 
        ///   </summary> 
        ///   <param   name= "ADUsername "> 用户 </param> 
        ///   <param   name= "ADPassword "> 密码 </param> 
        ///   <param   name= "domain "> 域名 </param> 
        ///   <returns> </returns> 
        public System.Collections.SortedList AdUserInfo(string ADUsername, string ADPassword, string domain)
        {
            System.DirectoryServices.DirectorySearcher src;
            string ADPath = "LDAP:// " + domain;//   "ou=总公司,DC=abc,DC=com,DC=cn ";   + ",ou=总公司 " 
            System.Collections.SortedList sl = new System.Collections.SortedList();
            string domainAndUsername = domain + @"" + ADUsername;
            System.DirectoryServices.DirectoryEntry de = new System.DirectoryServices.DirectoryEntry(ADPath, domainAndUsername, ADPassword);


            src = new System.DirectoryServices.DirectorySearcher(de)
            {
                Filter = "(SAMAccountName=" + ADUsername + ")",
                PageSize = 10000,//   此参数可以任意设置，但不能不设置，如不设置读取AD数据为0~999条数据，设置后可以读取大于1000条数据。 
                                 //   src.SizeLimit   =   2000; 
                SearchScope = System.DirectoryServices.SearchScope.Subtree
            };
            try
            {
                foreach (System.DirectoryServices.SearchResult res in src.FindAll())
                {
                    sl.Add(res.GetDirectoryEntry().Properties["Name"].Value, res.GetDirectoryEntry().InvokeGet("Description"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get   Ad   Info ", ex);
            }
            return sl;
        }
    }
}
