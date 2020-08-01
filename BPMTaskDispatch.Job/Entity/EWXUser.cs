using System;
using System.Collections;

namespace BPMTaskDispatch.Job.Entity
{
    public class EWXUser
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 对返回码的文本描述内容
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        ///必须, 成员UserID。对应管理端的帐号，企业内必须唯一。不区分大小写，长度为1~64个字节
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        ///必须, 成员名称。长度为1~64个utf8字符
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 成员别名。长度1~32个utf8字符
        /// </summary>
        public string alias { get; set; }
        /// <summary>
        /// 手机号码。企业内必须唯一，mobile/email二者不能同时为空
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        ///必须, 成员所属部门id列表,不超过20个
        /// </summary>
        public ArrayList department { get; set; }
        /// <summary>
        /// 部门内的排序值，默认为0，成员次序以创建时间从小到大排列。数量必须和department一致，数值越大排序越前面。有效的值范围是[0, 2^32)
        /// </summary>
        public ArrayList order { get; set; }
        /// <summary>
        /// 职务信息。长度为0~128个字符
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 性别。1表示男性，2表示女性
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 邮箱。长度6~64个字节，且为有效的email格式。企业内必须唯一，mobile/email二者不能同时为空
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 上级字段，标识是否为上级。在审批等应用里可以用来标识上级审批人
        /// </summary>
        public int isleader { get; set; }
        /// <summary>
        /// 启用/禁用成员。1表示启用成员，0表示禁用成员
        /// </summary>
        public int enable { get; set; }
        /// <summary>
        /// 成员头像的mediaid，通过素材管理接口上传图片获得的mediaid
        /// </summary>
        public string avatar_mediaid { get; set; }
        /// <summary>
        /// 座机。32字节以内，由纯数字或’-‘号组成。
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 自定义字段。自定义字段需要先在WEB管理端添加，见扩展属性添加方法，否则忽略未知属性的赋值。
        /// 自定义字段name长度不能多于16个utf8字符，value不能多于32个utf8字符，超过将被截断
        /// </summary>
        public attrs extattr { get; set; }
        /// <summary>
        /// 是否邀请该成员使用企业微信（将通过微信服务通知或短信或邮件下发邀请，每天自动下发一次，最多持续3个工作日），默认值为true。
        /// </summary>
        public bool to_invite { get; set; }
        /// <summary>
        /// 对外职务，如果设置了该值，则以此作为对外展示的职务，否则以position来展示。长度12个汉字内
        /// </summary>
        public string external_position { get; set; }
        /// <summary>
        ///成员对外属性，字段详情见对外属性: https://work.weixin.qq.com/api/doc#13450
        /// </summary>
        public object external_profile { get; set; }
    }

    public class attrs
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class external_attr
    {
        public int type { get; set; }
        public string name { get; set; }

    }
 
    public class text 
    {
        public string value { get; set; }
    }
    public class web 
    {
        public string url { get; set; }
        public string title { get; set; }
    }
    public class miniprogram
    {
        public string appid { get; set; }
        public string pagepath { get; set; }
        public string title { get; set; }
    }
}
