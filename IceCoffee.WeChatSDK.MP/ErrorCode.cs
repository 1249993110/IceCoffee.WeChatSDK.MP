﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IceCoffee.WeChatSDK.MP
{
    /// <summary>
    /// 公众号返回码（JSON）
    /// </summary>
    public enum ErrorCode
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        SenparcWeixinSDK配置错误 = -99,

        系统繁忙此时请开发者稍候再试 = -1,
        请求成功 = 0,
        获取access_token时AppSecret错误或者access_token无效 = 40001,
        不合法的凭证类型 = 40002,
        不合法的OpenID = 40003,
        不合法的媒体文件类型 = 40004,
        不合法的文件类型 = 40005,
        不合法的文件大小 = 40006,
        不合法的媒体文件id = 40007,
        不合法的消息类型_40008 = 40008,
        不合法的图片文件大小 = 40009,
        不合法的语音文件大小 = 40010,
        不合法的视频文件大小 = 40011,
        不合法的缩略图文件大小 = 40012,
        不合法的APPID = 40013,
        不合法的access_token = 40014,
        不合法的菜单类型 = 40015,
        不合法的按钮个数1 = 40016,
        不合法的按钮个数2 = 40017,
        不合法的按钮名字长度 = 40018,
        不合法的按钮KEY长度 = 40019,
        不合法的按钮URL长度 = 40020,
        不合法的菜单版本号 = 40021,
        不合法的子菜单级数 = 40022,
        不合法的子菜单按钮个数 = 40023,
        不合法的子菜单按钮类型 = 40024,
        不合法的子菜单按钮名字长度 = 40025,
        不合法的子菜单按钮KEY长度 = 40026,
        不合法的子菜单按钮URL长度 = 40027,
        不合法的自定义菜单使用用户 = 40028,
        不合法的oauth_code = 40029,
        不合法的refresh_token = 40030,
        不合法的openid列表 = 40031,
        不合法的openid列表长度 = 40032,
        不合法的请求字符不能包含uxxxx格式的字符 = 40033,
        不合法的参数 = 40035,

        //小程序、 公众号都有
        template_id不正确 = 40037,

        不合法的请求格式 = 40038,
        不合法的URL长度 = 40039,
        不合法的分组id = 40050,
        分组名字不合法 = 40051,
        appsecret不正确 = 40125,//invalid appsecret
        调用接口的IP地址不在白名单中 = 40164,//GitHub#2166 https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Get_access_token.html 

        小程序Appid不存在 = 40166,

        缺少access_token参数 = 41001,
        缺少appid参数 = 41002,
        缺少refresh_token参数 = 41003,
        缺少secret参数 = 41004,
        缺少多媒体文件数据 = 41005,
        缺少media_id参数 = 41006,
        缺少子菜单数据 = 41007,
        缺少oauth_code = 41008,
        缺少openid = 41009,

        //小程序
        form_id不正确_或者过期 = 41028,
        form_id已被使用 = 41029,
        page不正确 = 41030,

        access_token超时 = 42001,
        refresh_token超时 = 42002,
        oauth_code超时 = 42003,
        需要GET请求 = 43001,
        需要POST请求 = 43002,
        需要HTTPS请求 = 43003,
        需要接收者关注 = 43004,

        /// <summary>
        /// [小程序订阅消息]用户拒绝接受消息，如果用户之前曾经订阅过，则表示用户取消了订阅关系
        /// </summary>
        用户拒绝接受消息 = 43101,

        需要好友关系 = 43005,
        多媒体文件为空 = 44001,
        POST的数据包为空 = 44002,
        图文消息内容为空 = 44003,
        文本消息内容为空 = 44004,
        多媒体文件大小超过限制 = 45001,
        消息内容超过限制 = 45002,
        标题字段超过限制 = 45003,
        描述字段超过限制 = 45004,
        链接字段超过限制 = 45005,
        图片链接字段超过限制 = 45006,
        语音播放时间超过限制 = 45007,
        图文消息超过限制 = 45008,
        接口调用超过限制 = 45009,
        创建菜单个数超过限制 = 45010,
        回复时间超过限制 = 45015,
        系统分组不允许修改 = 45016,
        分组名字过长 = 45017,
        分组数量超过上限 = 45018,
        超出响应数量限制 = 45047,//out of response count limit，一般只允许连续接收20条客服消息


        不存在媒体数据 = 46001,
        不存在的菜单版本 = 46002,
        不存在的菜单数据 = 46003,
        解析JSON_XML内容错误 = 47001,

        /// <summary>
        /// [小程序订阅消息]模板参数不准确，可能为空或者不满足规则，errmsg会提示具体是哪个字段出错
        /// </summary>
        模板参数不准确 = 47003,

        api功能未授权 = 48001,
        用户未授权该api = 50001,
        参数错误invalid_parameter = 61451,
        无效客服账号invalid_kf_account = 61452,
        客服帐号已存在kf_account_exsited = 61453,

        //创建标签 错误返回信息
        标签名非法请注意不能和其他标签重名 = 45157,
        标签名长度超过30个字节 = 45158,
        创建的标签数过多请注意不能超过100个 = 45056,



        /// <summary>
        /// 客服帐号名长度超过限制(仅允许10个英文字符，不包括@及@后的公众号的微信号)(invalid kf_acount length)
        /// </summary>
        客服帐号名长度超过限制 = 61454,
        /// <summary>
        /// 客服帐号名包含非法字符(仅允许英文+数字)(illegal character in kf_account)
        /// </summary>
        客服帐号名包含非法字符 = 61455,
        /// <summary>
        ///  	客服帐号个数超过限制(10个客服账号)(kf_account count exceeded)
        /// </summary>
        客服帐号个数超过限制 = 61456,
        无效头像文件类型invalid_file_type = 61457,
        系统错误system_error = 61450,
        日期格式错误 = 61500,
        日期范围错误 = 61501,

        //新加入的一些类型，以下文字根据P2P项目格式组织，非官方文字
        发送消息失败_48小时内用户未互动 = 10706,
        发送消息失败_该用户已被加入黑名单_无法向此发送消息 = 62751,
        发送消息失败_对方关闭了接收消息 = 10703,
        对方不是粉丝 = 10700,
        没有留言权限 = 88000,//without comment privilege
        该图文不存在 = 88001,//msg_data is not exists
        文章存在敏感信息 = 88002,//the article is limit for safety
        精选评论数已达上限 = 88003,//elected comment upper limit
        已被用户删除_无法精选 = 88004,//comment was deleted by user
        已经回复过了 = 88005,//already reply
                       //88006暂时留空，未找到
        回复超过长度限制或为0 = 88007,//reply content beyond max len or content len is zero
        该评论不存在 = 88008,//comment is not exists
        获取评论数目不合法 = 88010,//count range error. cout <= 0 or count > 50

        //开放平台

        该公众号_小程序已经绑定了开放平台帐号 = 89000,//account has bound open，该公众号/小程序已经绑定了开放平台帐号
        该主体已有任务执行中_距上次任务24h后再试 = 89249,//  task running
        内部错误 = 89247,//    inner error
        无效微信号 = 86004,//   invalid wechat
        法人姓名与微信号不一致 = 61070,// name, wechat name not in accordance
        企业代码类型无效_请选择正确类型填写 = 89248,//  invalid code type
        未找到该任务 = 89250,//  task not found
        待法人人脸核身校验 = 89251,//   legal person checking
        法人_企业信息一致性校验中 = 89252,//   front checking
        缺少参数 = 89253,//    lack of some params
        第三方权限集不全_补全权限集全网发布后生效 = 89254,//   lack of some component rights
        已下发的模板消息法人并未确认且已超时_24h_未进行身份证校验 = 100001,
        已下发的模板消息法人并未确认且已超时_24h_未进行人脸识别校验 = 100002,
        已下发的模板消息法人并未确认且已超时_24h = 100003,
        工商数据返回_企业已注销 = 101,
        工商数据返回_企业不存在或企业信息未更新 = 102,
        工商数据返回_企业法定代表人姓名不一致 = 103,
        工商数据返回_企业法定代表人身份证号码不一致 = 104,
        法定代表人身份证号码_工商数据未更新_请5_15个工作日之后尝试 = 105,
        工商数据返回_企业信息或法定代表人信息不一致 = 1000,
        名称格式不合法 = 53010,
        名称检测命中频率限制 = 53011,
        禁止使用该名称 = 53012,
        公众号_名称与已有公众号名称重复_小程序_该名称与已有小程序名称重复 = 53013,
        公众号_公众号已有_名称A_时_需与该帐号相同主体才可申请_名称A_小程序_小程序已有_名称A_时_需与该帐号相同主体才可申请_名称A_ = 53014,
        公众号_该名称与已有小程序名称重复_需与该小程序帐号相同主体才可申请_小程序_该名称与已有公众号名称重复_需与该公众号帐号相同主体才可申请 = 53015,
        公众号_该名称与已有多个小程序名称重复_暂不支持申请_小程序_该名称与已有多个公众号名称重复_暂不支持申请 = 53016,
        公众号_小程序已有_名称A_时_需与该帐号相同主体才可申请_名称A_小程序_公众号已有_名称A_时_需与该帐号相同主体才可申请_名称A = 53017,
        名称命中微信号 = 53018,
        名称在保护期内 = 53019,


        //小程序代码管理返回码
        不是由第三方代小程序进行调用 = 86000,
        不存在第三方的已经提交的代码 = 86001,
        标签格式错误 = 85006,
        页面路径错误 = 85007,
        类目填写错误 = 85008,
        已经有正在审核的版本 = 85009,
        item_list有项目为空 = 85010,
        标题填写错误 = 85011,
        无效的审核id = 85012,
        没有审核版本 = 85019,
        审核状态未满足发布 = 85020,
        状态不可变 = 85021,
        action非法 = 85022,
        审核列表填写的项目数不在1到5以内 = 85023,
        小程序没有线上版本_不能进行灰度 = 85079,
        小程序提交的审核未审核通过 = 85080,
        无效的发布比例 = 85081,
        当前的发布比例需要比之前设置的高 = 85082,
        小程序提审数量已达本月上限 = 85085,
        提交代码审核之前需提前上传代码 = 85086,
        小程序已使用_api_navigateToMiniProgram_请声明跳转_appid_列表后再次提交 = 85087,
        小程序还未设置昵称_头像_简介_请先设置完后再重新提交 = 86002,
        现网已经在灰度发布_不能进行版本回退 = 87011,
        该版本不能回退_可能的原因_1_无上一个线上版用于回退_2_此版本为已回退版本_不能回退_3_此版本为回退功能上线之前的版本_不能回退 = 87012,
        版本输入错误 = 85015,

        #region Open v4.7.101 添加“开放平台-代码管理-加急审核”接口

        系统不稳定_请稍后再试_如多次失败请通过社区反馈 = 89401,
        该审核单不在待审核队列_请检查是否已提交审核或已审完 = 89402,
        本单属于平台不支持加急种类_请等待正常审核流程 = 89403,
        本单已加速成功_请勿重复提交 = 89404,
        本月加急额度不足_请提升提审质量以获取更多额度 = 89405,

        #endregion
        /// <summary>
        /// 小程序为“签名错误”。对应公众号： 87009, “errmsg” : “reply is not exists” //该回复不存在
        /// </summary>
        签名错误 = 87009,
        //小程序MsgSecCheck接口
        内容含有违法违规内容 = 87014,

        //小程序地点管理返回码
        POST参数非法 = 20002,
        该经营资质已添加_请勿重复添加 = 92000,
        附近地点添加数量达到上线_无法继续添加 = 92002,
        地点已被其它小程序占用 = 92003,
        附近功能被封禁 = 92004,
        地点正在审核中 = 92005,
        地点正在展示小程序 = 92006,
        地点审核失败 = 92007,
        程序未展示在该地点 = 92008,
        小程序未上架或不可见 = 92009,
        地点不存在 = 93010,
        个人类型小程序不可用 = 93011,

        //小程序普通链接二维码返回码
        链接错误 = 85066,
        测试链接不是子链接 = 85068,
        校验文件失败 = 85069,
        个人类型小程序无法设置二维码规则 = 85070,
        已添加该链接_请勿重复添加 = 85071,
        该链接已被占用 = 85072,
        二维码规则已满 = 85073,
        小程序未发布_小程序必须先发布代码才可以发布二维码跳转规则 = 85074,
        个人类型小程序无法设置二维码规则1 = 85075,

        //门店小程序返回码
        需要补充相应资料_填写org_code和other_files参数 = 85024,
        管理员手机登记数量已超过上限 = 85025,
        该微信号已绑定5个管理员 = 85026,
        管理员身份证已登记过5次 = 85027,
        该主体登记数量已超过上限 = 85028,
        商家名称已被占用 = 85029,
        不能使用该名称 = 85031,
        该名称在侵权投诉保护期 = 85032,
        名称包含违规内容或微信等保留字 = 85033,
        商家名称在改名15天保护期内 = 85034,
        需与该帐号相同主体才可申请 = 85035,
        介绍中含有虚假混淆内容 = 85036,
        头像或者简介修改达到每个月上限 = 85049,
        没有权限 = 43104,
        正在审核中_请勿重复提交 = 85050,
        请先成功创建门店后再调用 = 85053,
        临时mediaid无效 = 85056,

        输入参数有误 = 40097,
        门店不存在 = 65115,
        该门店状态不允许更新 = 65118,
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
