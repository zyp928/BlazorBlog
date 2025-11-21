# BlazorBlog - 基于 .NET 9 和 Blazor 的全功能博客系统

BlazorBlog 是一个现代化的、功能丰富的博客平台，构建于 .NET 9 和 Blazor Server 之上，使用 PostgreSQL 作为数据库。它旨在为 IT 技术社区提供一个高性能、互动性强的交流平台。

## ✨ 核心特性

### 👤 用户系统
- **多方式登录**：支持用户名、邮箱或手机号登录。
- **角色管理**：内置用户、管理员、版主等角色权限控制。
- **个人中心**：支持修改个人资料、头像、简介等。
- **社交互动**：关注/粉丝系统，用户动态追踪。

### 📝 内容管理
- **Markdown 编辑器**：支持实时预览的 Markdown 文章撰写体验。
- **分类与标签**：支持多级分类和多标签管理，方便内容组织。
- **文章状态**：支持草稿、发布、归档等状态管理。
- **SEO 优化**：内置 SEO 字段支持，友好的 URL 结构。

### 💬 互动功能
- **评论系统**：支持多级评论回复、置顶评论。
- **点赞与收藏**：支持文章和评论的点赞，以及文章收藏功能。
- **通知中心**：实时接收评论、点赞、关注等系统通知。

### 📊 统计分析
- **浏览记录**：记录文章浏览历史和统计数据。
- **热门内容**：基于浏览量、点赞数等维度的热门文章推荐。

## 🛠️ 技术栈

- **前端/后端框架**：[.NET 9](https://dotnet.microsoft.com/) + [Blazor Server](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
- **数据库**：[PostgreSQL](https://www.postgresql.org/)
- **ORM**：[Entity Framework Core](https://docs.microsoft.com/ef/core/)
- **UI 组件**：基于 Bootstrap 风格的自定义 Blazor 组件
- **Markdown 渲染**：Markdig

## 🚀 快速开始

### 环境要求
- .NET 9.0 SDK 或更高版本
- PostgreSQL 13 或更高版本

### 1. 获取代码
```bash
git clone https://github.com/zyp928/BlazorBlog.git
cd BlazorBlog
```

### 2. 数据库配置
本项目包含完整的数据库设计文档和 SQL 脚本。请按以下步骤初始化数据库：

1. 创建数据库 `blazor_blog`。
2. 运行 `database/database_schema.sql` 脚本创建表结构。
3. （可选）运行 `database/insert_admin_user.sql` 插入初始管理员用户。

```bash
# 示例命令
createdb blazor_blog
psql -d blazor_blog -f database/database_schema.sql
```

### 3. 配置连接字符串
打开 `appsettings.json` 文件，修改 `ConnectionStrings` 节点下的 `DefaultConnection`，填入你的 PostgreSQL 连接信息：

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=blazor_blog;Username=your_username;Password=your_password"
}
```

### 4. 运行项目
在项目根目录下运行以下命令启动应用：

```bash
dotnet run
```

启动后，访问 `https://localhost:7193` (或控制台显示的端口) 即可浏览网站。

## 📂 项目结构

- **Components/**: Blazor 组件（Pages, Layouts, Shared 等）
- **Controllers/**: API 控制器（用于部分 API 接口）
- **Data/**: 数据库上下文 (DbContext)
- **Models/**: 实体模型定义
- **Services/**: 业务逻辑服务层
- **database/**: 数据库设计文档和 SQL 脚本
- **wwwroot/**: 静态资源文件 (CSS, JS, Images)

## 📄 许可证

[MIT License](LICENSE)
