using Microsoft.EntityFrameworkCore;
using BlazorBlog.Models;

namespace BlazorBlog.Data;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }
    
    // DbSet 定义
    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Like> Likes => Set<Like>();
    public DbSet<Favorite> Favorites => Set<Favorite>();
    public DbSet<Follow> Follows => Set<Follow>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // ===========================
        // 映射 C# 属性名到数据库列名（snake_case）
        // ===========================
        
        // User 实体配置
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(50);
            entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(255);
            entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(20);
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash").HasMaxLength(255);
            entity.Property(e => e.AvatarUrl).HasColumnName("avatar_url").HasMaxLength(500);
            entity.Property(e => e.Nickname).HasColumnName("nickname").HasMaxLength(100);
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.GithubUrl).HasColumnName("github_url").HasMaxLength(255);
            entity.Property(e => e.WebsiteUrl).HasColumnName("website_url").HasMaxLength(255);
            entity.Property(e => e.IsEmailVerified).HasColumnName("is_email_verified");
            entity.Property(e => e.IsPhoneVerified).HasColumnName("is_phone_verified");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Role).HasColumnName("role").HasMaxLength(20);
            entity.Property(e => e.FollowersCount).HasColumnName("followers_count");
            entity.Property(e => e.FollowingCount).HasColumnName("following_count");
            entity.Property(e => e.PostsCount).HasColumnName("posts_count");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.LastLoginAt).HasColumnName("last_login_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            
            // 索引
            entity.HasIndex(e => e.Username);
            entity.HasIndex(e => e.Email);
            entity.HasIndex(e => e.Phone);
        });
        
        // Post 实体配置
        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("posts");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Title).HasColumnName("title").HasMaxLength(200);
            entity.Property(e => e.Slug).HasColumnName("slug").HasMaxLength(250);
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CoverImageUrl).HasColumnName("cover_image_url").HasMaxLength(500);
            entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(20);
            entity.Property(e => e.Visibility).HasColumnName("visibility").HasMaxLength(20);
            entity.Property(e => e.IsFeatured).HasColumnName("is_featured");
            entity.Property(e => e.IsOriginal).HasColumnName("is_original");
            entity.Property(e => e.SourceUrl).HasColumnName("source_url").HasMaxLength(500);
            entity.Property(e => e.MetaDescription).HasColumnName("meta_description").HasMaxLength(300);
            entity.Property(e => e.MetaKeywords).HasColumnName("meta_keywords").HasMaxLength(200);
            entity.Property(e => e.ViewsCount).HasColumnName("views_count");
            entity.Property(e => e.LikesCount).HasColumnName("likes_count");
            entity.Property(e => e.CommentsCount).HasColumnName("comments_count");
            entity.Property(e => e.FavoritesCount).HasColumnName("favorites_count");
            entity.Property(e => e.SharesCount).HasColumnName("shares_count");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.PublishedAt).HasColumnName("published_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            
            // 关系
            entity.HasOne(e => e.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // 索引
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Status);
        });
        
        // Category 实体配置
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50);
            entity.Property(e => e.Slug).HasColumnName("slug").HasMaxLength(60);
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Icon).HasColumnName("icon").HasMaxLength(50);
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.PostsCount).HasColumnName("posts_count");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            
            // 自引用关系
            entity.HasOne(e => e.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.SetNull);
            
            // 索引
            entity.HasIndex(e => e.Slug).IsUnique();
        });
        
        // Tag 实体配置
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("tags");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(50);
            entity.Property(e => e.Slug).HasColumnName("slug").HasMaxLength(60);
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Color).HasColumnName("color").HasMaxLength(20);
            entity.Property(e => e.PostsCount).HasColumnName("posts_count");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            
            // 索引
            entity.HasIndex(e => e.Name).IsUnique();
        });
        
        // Comment 实体配置
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("comments");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(20);
            entity.Property(e => e.IsPinned).HasColumnName("is_pinned");
            entity.Property(e => e.LikesCount).HasColumnName("likes_count");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            
            // 关系
            entity.HasOne(e => e.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // 自引用关系（评论回复）
            entity.HasOne(e => e.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Like 实体配置
        modelBuilder.Entity<Like>(entity =>
        {
            entity.ToTable("likes");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TargetType).HasColumnName("target_type").HasMaxLength(20);
            entity.Property(e => e.TargetId).HasColumnName("target_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            
            // 关系
            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // 唯一索引
            entity.HasIndex(e => new { e.UserId, e.TargetType, e.TargetId }).IsUnique();
        });
        
        // Favorite 实体配置
        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.ToTable("favorites");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            
            // 关系
            entity.HasOne(e => e.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Post)
                .WithMany(p => p.Favorites)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // 唯一索引
            entity.HasIndex(e => new { e.UserId, e.PostId }).IsUnique();
        });
        
        // Follow 实体配置
        modelBuilder.Entity<Follow>(entity =>
        {
            entity.ToTable("follows");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FollowerId).HasColumnName("follower_id");
            entity.Property(e => e.FollowingId).HasColumnName("following_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            
            // 关系（注意避免循环引用）
            entity.HasOne(e => e.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(e => e.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.FollowingUser)
                .WithMany(u => u.Followers)
                .HasForeignKey(e => e.FollowingId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // 唯一索引
            entity.HasIndex(e => new { e.FollowerId, e.FollowingId }).IsUnique();
        });
        
        // ===========================
        // 多对多关系配置
        // ===========================
        
        // Post <-> Category (多对多)
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Posts)
            .UsingEntity<Dictionary<string, object>>(
                "post_categories",
                j => j.HasOne<Category>().WithMany().HasForeignKey("category_id").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Post>().WithMany().HasForeignKey("post_id").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("post_categories");
                    j.HasKey("post_id", "category_id");
                    j.Property<DateTime>("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                }
            );
        
        // Post <-> Tag (多对多)
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Tags)
            .WithMany(t => t.Posts)
            .UsingEntity<Dictionary<string, object>>(
                "post_tags",
                j => j.HasOne<Tag>().WithMany().HasForeignKey("tag_id").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Post>().WithMany().HasForeignKey("post_id").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("post_tags");
                    j.HasKey("post_id", "tag_id");
                    j.Property<DateTime>("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                }
            );
    }
}
