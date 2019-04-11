使用SDHC.CMF
1. 将 App_start 中的 IdentityConfig.cs, Startup.Auth.cs删除
2. 在dbcontent的类 加入IContent接口.
3. 建立3个model (BaseContent,BaseSelect,SDHCUser) 并放入dbcontext
4. 在Global.asax 的start方法的最开始位置加入E.Init();
5, 在Startup.cs 开始处加入
SDHCStartup.Init<[dbcontext], [basetype for basecontent], [basetype for baseselect], [basetype for sdhcuser]>(
        app, [Action 创建新的dbcontext 例如 () => ApplicationDbContext.Create()], HostingEnvironment.MapPath("/"));
例如 将ConfigureAuth(app); 替换为

SDHCStartup.Init<ApplicationDbContext, SCHCContent, SDHCBascSelect, MyUser>(
        app, () => ApplicationDbContext.Create(), HostingEnvironment.MapPath("/"));

6. 运行ef data migration
7. account/manager 控制器需要添加对应的命名空间

设置.
管理员设置
web.config中的setting中加入对应的设置,如果未设置则为默认值. 在程序中使用G.[property]调用
后台权限对应方法的权限
ContentIndex- 控制器:Content方法Index 默认值""
ContentCreate- 控制器:Content方法Create , PreCreate 默认值""
ContentEdit- 控制器:Content方法Edit 默认值""
ContentSort- 控制器:Content方法Sort 默认值""
ContentDelete- 控制器:Content方法Delete 默认值""
ModelManagementIndex- 控制器:ModelManagement方法Index 默认值""
ModelManagementCreate- 控制器:ModelManagement方法Create 默认值""
ModelManagementEdit- 控制器:ModelManagement方法Edit 默认值""
ModelManagementDelete- 控制器:ModelManagement方法Delete 默认值""
RolesIndex- 控制器:Roles方法Index 默认值""
RolesRoleList- 控制器:Roles方法RoleList 默认值""
RolesCreateRole- 控制器:Roles方法CreateRole 默认值""
RolesDeleteRole- 控制器:Roles方法DeleteRole 默认值""
RolesCreateUser- 控制器:Roles方法CreateUser 默认值""
RolesEditUser- 控制器:Roles方法EditUser 默认值""

UseContentRouter - bool 是否使用 内容通配符 默认值 false
AdminFree - bool 后台是否关闭 默认值 false
AdminTitle - string 后台 title ""
AdminCopyright - string 后台版权 ""
AdminPath - string 后台路径 默认值 "Admin"
SuperUserRole - string 超级用户role 默认值 "Admin"
AdminRole - string 默认Admin Role 默认值 "Admin"
UserNameIsNotEmail - bool email为用户名 默认值 true
DefaultLanguage - int 默认语言 默认值 0
ContentViewPath - string Content Model对应的views的目录(根目录为Views) 默认值 ""
ContentPageUrl - string content通配符的默认路由名 默认值 "pages"






环境设置
在静态方法E中 加入property,与[Config] filter. 这样在对应web.config中的setting中加入对应的设置.
