# SDHC CMF(暂定名)
## 项目历史
大约是从15年左右开始接外包工作. 这类工作都有一个特点就是没有什么特别复杂的逻辑或者功能.但是琐碎的地方很多. 需求改变的频繁. 很可能昨天说一个功能然后一直催着要,做出来给客人看之后客人就会提出和之前完全相反的逻辑需求. 刚开始的几个项目就是各种硬写. 痛苦异常. 后来发现必须的想办法找一个框架. 当时的想法是使用CMS框架. 利用Content的上下继承关系来模拟表的结构. 因为朋友是.net背景. 所以市场上大片的php框架就只能跳过了. .net下的框架就只有不多的选择了. 在尝试了一些框架后最后使用的就是UmbracoCms. 并且在Umbraco的帮助下完成了大部分的项目.  

但是随着项目的增长. Umbraco的弊端开始暴露. <br/>
* 客户反映后台管理页面永不明白. 大部分的内容都是客人不需要或者很难想明白的
* 因为是cms, 页面中的属性都是需要与数据库连接的. 有时候家里网络不好.添加一个text input就要花费数分钟. 
* 还是因为cms, 大部分功能很可能客户都不需要. 但是这部分功能又增加了初次启动的时间. 客户和其他项目比较会觉得好慢.
* 后台管理界面是一个angular的spa. 如果客人需要其他的管理功能. 反而需要花费更多的时间去编写对应功能的管理页面.

## 实际需求
综上所述, 对于一个一般的外包而言. 大部分的功能都是CRUD. 在带上一些独有的商用逻辑. 对应的策略就是.
* 通用的CRUD. 并且根据Model自动产生管理界面的表单
* 基本的用户管理功能

## 实现思路
* 使用 asp.net mvc 基本的脚手架
* 使用 Entity Framework 作为orm
* 想办法根据定义的entity产生对应的crud表单
* 做一个控制器, 根据url判断需要哪个model,然后将model 和对应的cshtml渲染.

## 预期结果
* 简化开发流程
* 确定需求,定义Entity
* 完成商业逻辑
* 上前端出工

# 安装方法
* 创建一个新的 ASP.NET Web Application (.Net Framework). Framework 版本 4.6.2
* 选择MVC, 选择Individual Usar Account.
* 下载nuget 包 SDHC.CMS.E
* 将 App_start 中的 IdentityConfig.cs, Startup.Auth.cs删除
* 在dbcontent的类 加入IContent接口. 并使用SDHCUser
```
public class ApplicationDbContext : IdentityDbContext<SDHCUser>, IContent
```
* 建立3个model (BaseContent,BaseSelect,SDHCUser) 并放入dbcontext
```
public class SCHCContent : BaseContent
public class SDHCBascSelect : BaseSelect
public class MyUser: SDHCUser

//在dbcontext中加入
public DbSet<BaseContent> Contents { get; set; }
public DbSet<BaseSelect> Selects { get; set; }
public DbSet<MyUser> MyUsers { get; set; }
public DbSet<SCHCContent> SCHCContents { get; set; }

```
* 在Global.asax 的start方法的最开始位置加入E.Init();
* 在Startup.cs 开始处加入
```
SDHCStartup.Init<ApplicationDbContext, SCHCContent, SDHCBascSelect, MyUser>(
    app, () => ApplicationDbContext.Create(), HostingEnvironment.MapPath("/"));
```
* Account,manager 控制其中默认使用的是ApplicationUser 要换成使用的user. 在这里使用的是MyUser
* 运行ef data migration
## 使用方法

所有的Entity的key都是Long, user和role直接使用了微软的所有key是string. <br>
Entity 分为三类. 
* 基础的Entity. 继承了基础的IInt64Key 与IDisplayName
* Content. 这类Entity有父子继承的关系.做内容时候用, 其中content的title则会被转换为url '/'与' '将会被转换为'_'其余特殊字符将会被转义
* Select 这类作为列表选项出现在基础与content中
使用 ContentManager,ModelManager,SelectManager 来进行crud操作
* 命名空间SDHC.Controllers下的SDHCPageController 为默认的获取content内容的控制器.
* ContentPageUrl 设置了默认路径 比如/pages/123, 则会 读取第一个title为123 且没有父元素的content. 如果重名将会读取displayorder最小的一个.


## 设置.
### 管理员设置
web.config中的setting中加入对应的设置,如果未设置则为默认值. 在程序中使用G.[property]调用

后台权限对应方法的权限


| 属性                  | 类型       |控制器         |方法  |默认值|
| -------------         |:----:     | ---:          | -----:| ----:| 
| ContentIndex          | string    |Content        |Index |""    |
| ContentCreate         | string    |Content        |Create , PreCreate|""|
| ContentEdit           | string    |Content        |Edit |""|
| ContentSort           | string    |Content        |Sort |""|
| ContentDelete         | string    |Content        |Delete |""|
| ModelManagementIndex  | string    |ModelManagement|Index |""|
| ModelManagementCreate | string    |ModelManagement|Create |""|
| ModelManagementEdit   | string    |ModelManagement|Edit |""|
| ModelManagementDelete | string    |ModelManagement|Delete |""|
| RolesIndex            | string    |Roles          |Index |""|
| RolesRoleList         | string    |Roles          |RoleList |""|
| RolesCreateRole       | string    |Roles          |CreateRole |""|
| RolesDeleteRole       | string    |Roles          |DeleteRole |""|
| RolesCreateUser       | string    |Roles          |CreateUser |""|
| RolesEditUser         | string    |Roles          |EditUser|""|

一般属性

| 属性                  | 类型       |内容         |默认值|
| -------------         |:----:     | -------:| ----:| 
|UseContentRouter | bool| 是否使用 内容通配符 | false|
|AdminFree | bool |后台是否关闭 | false|
|AdminTitle | string |后台 title |""|
|AdminCopyright | string |后台版权 |""|
|AdminPath | string| 后台路径 | "Admin"|
|SuperUserRole | string| 超级用户role | "Admin"|
|AdminRole | string| 默认Admin Role | "Admin"|
|UserNameIsNotEmail | bool |email为用户名 | true|
|DefaultLanguage | int| 默认语言 | 0
|ContentViewPath | string |Content Model对应的views的目录(根目录为Views) | ""|
|ContentPageUrl | string |content通配符的默认路由名 | "pages"|

环境设置
在静态方法E中 加入property,与[Config] filter. 这样在对应web.config中的setting中加入对应的设置.